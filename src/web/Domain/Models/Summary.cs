﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Calme.Domain.Models
{
    public class Summary
    {
        public IEnumerable<string> Uncategorised { get; }
        public decimal Total { get; }
        public int Count { get; }
        public DateTime Date { get; }
        public IEnumerable<Expense> Expenses { get; }
        
        public Summary(IEnumerable<ExpenseTransaction> transactions)
        {
            
            Uncategorised = transactions
                .Where(t => t.Category == Category.Uncategorized)
                .Select(t => t.Description)
                .Distinct()
                .OrderBy(t => t);

            Date = transactions.Last().Date;
            Total = transactions.Sum(t => t.PaidOut);
            Count = transactions.Count();
            Expenses = transactions
                .OrderBy(t => t.Category)
                .GroupBy(t => t.Category)
                .Select(i => new Expense(i.Key.ToString(), i.Sum(s => s.PaidOut)));
            

        }

    }
}