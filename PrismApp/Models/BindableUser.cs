﻿using Data.Extensions;
using Data.Models;
using Prism.Mvvm;
using System;
using System.Text.RegularExpressions;

namespace PrismApp.Models
{
    public class BindableUser : BindableBase
    {
        #region Id property
        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        #endregion

        #region Account property
        private Account? _account;
        public Account? Account
        {
            get => _account;
            set => SetProperty(ref _account, value);
        }
        #endregion

        #region FirstName property
        private string? _firstName;
        public string? FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }
        #endregion

        #region LastName property
        private string? _lastName;
        public string? LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }
        #endregion

        #region Name property
        private readonly static Regex NameRegex = new Regex(@"^(?<last_name>\w+)\s+(?<first_name>\w+)$");
        public string? Name
        {
            get
            {
                return $"{LastName} {FirstName}".Trim();
            }
            set
            {
                if (value is null)
                {
                    LastName = null;
                    FirstName = null;
                    return;
                }

                var match = NameRegex.Match(value);
                if (match.Success)
                {
                    LastName = match.Groups["last_name"].Value;
                    FirstName = match.Groups["first_name"].Value;
                }
                else
                {
                    LastName = value;
                }
            }
        }
        #endregion

        #region FirstKana property
        private string? _firstKana;
        public string? FirstKana
        {
            get => _firstKana;
            set => SetProperty(ref _firstKana, value);
        }
        #endregion

        #region LastKana property
        private string? _lastKana;
        public string? LastKana
        {
            get => _lastKana;
            set => SetProperty(ref _lastKana, value);
        }
        #endregion

        #region Kana property
        private readonly static Regex KanaRegex = new Regex(@"^(?<last_kana>\w+)\s+(?<first_kana>\w+)$");
        public string? Kana
        {
            get
            {
                return $"{LastKana} {FirstKana}".Trim();
            }
            set
            {
                if (value is null)
                {
                    LastKana = null;
                    FirstKana = null;
                    return;
                }

                var match = KanaRegex.Match(value);
                if (match.Success)
                {
                    LastKana = match.Groups["last_kana"].Value;
                    FirstKana = match.Groups["first_kana"].Value;
                }
                else
                {
                    LastKana = value;
                }
            }
        }
        #endregion

        #region Gender property
        private Gender _gender;
        public Gender Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }
        #endregion

        #region BirthDate property
        private DateTime? _birthDate;
        public DateTime? BirthDate
        {
            get => _birthDate;
            set => SetProperty(ref _birthDate, value);
        }
        #endregion

        #region Image property
        private string? _image;
        public string? Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }
        #endregion

        #region Mapper method
        public static BindableUser FromUser(User user) => user.Map<BindableUser>();

        public static User ToUser(BindableUser user) => user.Map<User>();
        #endregion
    }
}
