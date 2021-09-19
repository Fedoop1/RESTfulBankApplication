using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RESTfulBankApplication.Domain;
using RESTfulBankApplication.Domain.Models;
using static RESTfulBankApplication.Domain.Models.AccountExtenstion;

namespace RESTfulBankApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankController : ControllerBase
    {
        private readonly BankContext context;
        public BankController(BankContext context) => this.context = context;

        [HttpGet("Accounts")]
        public async Task<IActionResult> GetAllAccountsAsync() => 
            new JsonResult(await this.context.Accounts.Select(account => account.AccountId).ToArrayAsync());

        [HttpGet("Accounts/{id}")]
        public async Task<IActionResult> GetConcreteAccountAsync(int id)
        {
            var account = await this.context.Accounts.FindAsync(id);

            if (account is null)
            {
                return NotFound();
            }

            return new JsonResult(new {account.AccountId, account.AccountType, account.MoneyAmount, account.TransactionAmount});
        }

        [HttpGet("Accounts/Loans")]
        public async Task<IActionResult> GetLoansAccountsAsync() => new JsonResult(await
            this.context.Accounts.Where(account => account.AccountType == AccountType.Loan).ToArrayAsync());

        [HttpGet("Accounts/Profiles/{id}")]
        public async Task<IActionResult> GetAccountProfileAsync(int id)
        {
            var profile = await context.AccountProfiles.FindAsync(id);

            if (profile is null)
            {
                return NoContent();
            }

            return new JsonResult(new
            {
                profile.ProfileId, FullName = $"{profile.Account.FirstName}, {profile.Account.LastName}",
                profile.DateOfBirth, profile.Gender
            });
        }

        [HttpPost("Accounts/Profiles/")]
        public async Task<IActionResult> CreateAccountProfile(AccountProfile accountProfile)
        {
            this.context.AccountProfiles.Add(accountProfile);

            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(accountProfile);
            }
            
            return Created($"Accounts/Profiles/{accountProfile.AccountId}", accountProfile);
        }

        [HttpPut("Accounts/Profiles/{id}")]
        public async Task<IActionResult> UpdateAccountProfile(int id)
        {
            var account = await context.AccountProfiles.FindAsync(id);

            if (!await TryUpdateModelAsync(account, "", 
                a => a.Gender, a => a.DateOfBirth))
            {
                return BadRequest(account);
            }

            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                BadRequest(account);
            }
            
            return Ok();
        }

        [HttpPost("Account/Bills/")]
        public async Task<IActionResult> CreateBill(Bill bill)
        {
            this.context.Bills.Add(bill);

            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(bill);
            }
            
            return Created(string.Empty, bill);
        }

        [HttpDelete("Accounts/Payments/{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payee = await this.context.Bills.FindAsync(id);
            this.context.Bills.Remove(payee);

            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NoContent();
            }
            
            return Ok();
        }

        [HttpPost("Accounts/Payee/")]
        public async Task<IActionResult> CreatePayee(Payee payee)
        {
            this.context.Payees.Add(payee);

            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(payee);
            }
            
            return Created(string.Empty, payee);
        }

        [HttpPut("Accounts/Payee/{id}")]
        public async Task<IActionResult> UpdatePayee(int id)
        {
            var payee = await context.Payees.FindAsync(id);

            if (!await TryUpdateModelAsync(payee, "",
                p => p.FirstName, p => p.LastName))
            {
                return BadRequest(payee);
            }

            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(payee);
            }
            
            return Ok();
        }

        [HttpDelete("Accounts/Payee/{id}")]
        public async Task<IActionResult> DeletePayee(int id)
        {
            var payee = await this.context.Payees.FindAsync(id);
            this.context.Payees.Remove(payee);

            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NoContent();
            }
            
            return Ok();
        }

        [HttpPost("Accounts/fd/")]
        public async Task<IActionResult> CreateFixedDeposit(FixedDeposit fd)
        {
            this.context.FixedDeposits.Add(fd);
            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(fd);
            }
            
            return Created(string.Empty, fd);
        }

        [HttpPut("Accounts/fd/{id}")]
        public async Task<IActionResult> UpdateFixedDeposit(int id)
        {
            var fd = await context.FixedDeposits.FindAsync(id);

            if (!await TryUpdateModelAsync(fd, "",
                f => f.DepositAmount, f => f.DepositRate))
            {
                return BadRequest(fd);
            }

            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(fd);
            }
            
            return Ok();
        }
    }
}
