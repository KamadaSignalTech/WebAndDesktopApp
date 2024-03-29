﻿using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleData.Data
{
    // 以下、自動生成コード
    // 1. json文字列をコピー
    // 2. メニューバーより 編集 → 形式を選択して貼り付け
    // 3. JSONをクラスとして貼り付ける

    public class Rootobject
    {
        public Family_Name family_name { get; set; }
        public First_Name first_name { get; set; }
        public Status status { get; set; }
        public Image image { get; set; }
    }

    public class Family_Name
    {
        public Item[] items { get; set; }
    }

    public class Item
    {
        public string name { get; set; }
        public string kana { get; set; }
    }

    public class First_Name
    {
        public Item1[] items { get; set; }
    }

    public class Item1
    {
        public string name { get; set; }
        public string kana { get; set; }
        public string gender { get; set; }
    }

    public class Status
    {
        public string[] items { get; set; }
    }

    public class Image
    {
        public string[] items { get; set; }
    }
}
