﻿using Microsoft.EntityFrameworkCore;
using Data;
using Data.Models;
using Common.Extensions;
using Common.Utils;

namespace SampleData
{
    internal class SampleOrders
    {
        private readonly AppDbContext _db;

        public SampleOrders(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Order> CreateData(int count)
        {
            var rand = new RandomDateTime(DateTime.Now.AddDays(-30), DateTime.Now.AddDays(30));
            var people = _db.People.ToArray();
            var statuses = _db.Statuses.ToArray();

            for (var i = 0; i < count; i++)
            {
                var date = rand.Next();

                var order = new Order
                {
                    Id = i + 1,
                    Created = date,
                    Updated = date,
                    Expiration = i % 3 == 0 ? rand.Next() : null,
                    Number = "SM" + (i + 1).ToString("D4"),
                    Person = people?.ElementAtRandom(),
                    Status = statuses?.ElementAtRandom(),
                    Remarks = "テスト案件",
                    IsClosed = i % 5 == 0,
                };
                
                yield return order;
            }
        }

        public void AddData(int count)
        {
            var data = CreateData(count);
            _db.AddRange(data);
            _db.SaveChanges();
        }

        public void PrintData()
        {
            var orders = _db.Orders
                .Include(o => o.Person)
                .Include(o => o.Status)
                .ToList();

            Console.WriteLine("--- Orders ---");
            Console.WriteLine("番号, 作成, 更新, 担当者, ステータス");
            foreach (var o in orders)
            {
                Console.WriteLine($"{o.Number}, {o.Created}, {o.Updated}, {o.Person?.Name ?? "null"}, {o.Status?.Text ?? "null"}");
            }
            Console.WriteLine("--- /Orders ---");
        }
    }
}