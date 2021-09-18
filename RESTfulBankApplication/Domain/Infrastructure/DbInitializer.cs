using System;
using System.Linq;
using RESTfulBankApplication.Domain.Models;

namespace RESTfulBankApplication.Domain.Infrastructure
{
    public static class DbInitializer
    {
        public static void SeedDataBase(BankContext context)
        {
            if (context.Accounts.Any())
            {
                return;
            }

            var accounts = new Account[]
            {
                new() {FirstName = "Nikita", LastName = "Azure", MoneyAmount = 1000},
                new() {FirstName = "Kate", LastName = "Pascal", MoneyAmount = 1000},
                new() {FirstName = "Jack", LastName = "Dot", MoneyAmount = 1000},
                new() {FirstName = "Steve", LastName = "Python", MoneyAmount = 1000},
                new() {FirstName = "Mike", LastName = "Ruby", MoneyAmount = 1000, AccountType = AccountExtenstion.AccountType.Loan},
            };

            context.Accounts.AddRange(accounts);
            context.SaveChanges();

            var accProfiles = new AccountProfile[]
            {
                new() {DateOfBirth = DateTime.Parse("1.1.2000"), Gender = AccountExtenstion.Gender.Male, AccountId = accounts.First(x => x.LastName == "Azure").AccountId},
                new() {DateOfBirth = DateTime.Parse("4.10.1997"), Gender = AccountExtenstion.Gender.Female, AccountId = accounts.First(x => x.LastName == "Pascal").AccountId},
                new() {DateOfBirth = DateTime.Parse("6.1.2003"), Gender = AccountExtenstion.Gender.Male, AccountId = accounts.First(x => x.LastName == "Dot").AccountId},
                new() {DateOfBirth = DateTime.Parse("12.7.2008"), Gender = AccountExtenstion.Gender.Male, AccountId = accounts.First(x => x.LastName == "Python").AccountId},
                new() {DateOfBirth = DateTime.Parse("10.23.2001"), Gender = AccountExtenstion.Gender.Male, AccountId = accounts.First(x => x.LastName == "Ruby").AccountId},
            };

            context.AccountProfiles.AddRange(accProfiles);
            context.SaveChanges();

            var payeers = new Payee[]
            {
                new() {FirstName = "Steve", LastName = "Basic"},
                new() {FirstName = "Garry", LastName = "Arduino"},
                new() {FirstName = "Mokke", LastName = "Cobol"},
                new() {FirstName = "Joe", LastName = "Fortran"},
                new() {FirstName = "Alice", LastName = "Assembler"},
            };

            context.Payees.AddRange(payeers);
            context.SaveChanges();

            var bills = new Bill[]
            {
                new() {BillSum = 131.1, SenderId = accounts.First(x => x.LastName == "Python").AccountId, PayeeId = payeers.First(x => x.LastName == "Basic").PayeeId},
                new() {BillSum = 11.50, SenderId = accounts.First(x => x.LastName == "Python").AccountId, PayeeId = payeers.First(x => x.LastName == "Arduino").PayeeId},
                new() {BillSum = 100, SenderId = accounts.First(x => x.LastName == "Ruby").AccountId, PayeeId = payeers.First(x => x.LastName == "Basic").PayeeId},
                new() {BillSum = 125, SenderId = accounts.First(x => x.LastName == "Pascal").AccountId, PayeeId = payeers.First(x => x.LastName == "Assembler").PayeeId},
                new() {BillSum = 77, SenderId = accounts.First(x => x.LastName == "Dot").AccountId, PayeeId = payeers.First(x => x.LastName == "Fortran").PayeeId},
            };

            context.Bills.AddRange(bills);
            context.SaveChanges();

            var fixedDeposits = new FixedDeposit[]
            {
                new() {DepositAmount = 12000, DepositRate = 5, AccountId = accounts.First(x => x.LastName == "Azure").AccountId},
                new() {DepositAmount = 1000, DepositRate = 3, AccountId = accounts.First(x => x.LastName == "Pascal").AccountId},
                new() {DepositAmount = 5000, DepositRate = 3, AccountId = accounts.First(x => x.LastName == "Dot").AccountId},
                new() {DepositAmount = 12500, DepositRate = 6, AccountId = accounts.First(x => x.LastName == "Azure").AccountId},
                new() {DepositAmount = 7000, DepositRate = 5, AccountId = accounts.First(x => x.LastName == "Ruby").AccountId},
            };

            context.FixedDeposits.AddRange(fixedDeposits);
            context.SaveChanges();
        }
    }
}
