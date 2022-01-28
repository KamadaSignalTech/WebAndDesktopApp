﻿using Common.Extensions;
using Common.Utils;
using Data;
using Data.Models;
using System.Data.Entity;
using System.Text.Json;

namespace SampleData
{
    internal class SampleUsers
    {
        private readonly AppDbContext _db;

        public SampleUsers(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<User> CreateData(int count)
        {
            var obj = Data.RootobjectLoader.Load();
            if (obj == null)
            {
                return Enumerable.Empty<User>();
            }

            var male = 0;
            var female = 0;

            var users = Enumerable.Range(0, count).Select(i =>
            {
                var rand = new RandomDate(DateTime.Now.AddDays(-20000), DateTime.Now.AddDays(8000));
                var family = obj.family_name.items.ElementAtRandom();
                var first = obj.first_name.items.ElementAtRandom();

                var image = first.gender switch
                {
                    "M" => obj.image.items.Where(s => s.Contains("male")).Skip(male++).FirstOrDefault(),
                    "F" => obj.image.items.Where(s => s.Contains("female")).Skip(female++).FirstOrDefault(),
                    _ => ""
                };

                var gender = first.gender switch
                {
                    "M" => Gender.Male,
                    "F" => Gender.Female,
                    "O" => Gender.Other,
                    _ => Gender.Unknown
                };

                var account = new Account
                {
                    Id = "sample" + i,
                    Password = PasswordUtils.GetHashValue("sample" + i),
                };

                var user = new User
                {
                    Account = account,
                    Name = $"{family.name} {first.name}",
                    Kana = $"{family.kana} {first.kana}",
                    Gender = gender,
                    BirthDate = rand.Next(),
                    Image = image,
                };

                return user;
            });

            var adminAccount = new Account
            {
                Id = "admin",
                Password = PasswordUtils.GetHashValue("admin"),
                IsAdmin = true,
            };
            var adminUser = new User
            {
                Account = adminAccount,
                Name = "admin",
                Kana = "admin",
            };

            return users.Prepend(adminUser);
        }

        public void AddData(int count)
        {
            var data = CreateData(count);
            _db.AddRange(data);
            _db.SaveChanges();
        }

        public void PrintData()
        {
            var users = _db.Users.Include(u => u.Account).ToList();

            Console.WriteLine("--- Users ---");
            foreach (var u in users)
            {
                Console.WriteLine($"{u.Account?.Id}: {u.Name} ({u.Kana})");
            }
            Console.WriteLine("--- /Users ---");
        }
    }
}