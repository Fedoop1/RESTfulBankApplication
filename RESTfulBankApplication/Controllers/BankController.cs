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

            return new JsonResult(new {account.AccountId, account.AccountType});
        }

        [HttpGet("Accounts/Loans")]
        public async Task<IActionResult> GetLoansAccountsAsync() => new JsonResult(await
            this.context.Accounts.Where(account => account.AccountType == AccountType.Loan).ToArrayAsync());

        [HttpGet("Accounts/Profiles/{id}")]
        public async Task<IActionResult> GetAccountProfileAsync(int id)
        {
            var profile = await context.Accounts.FindAsync(id);

            if (profile is null)
            {
                return NotFound();
            }

            return new JsonResult(new {profile.AccountId, profile.FirstName, profile.TransactionAmount});
        }

        [HttpPost("Accounts/Profiles/")]
        public async Task<IActionResult> CreateAccountProfile([Bind("FirstName,LastName,AccountType")]Account account)
        {
            if (account is null || !ModelState.IsValid)
            {
                return BadRequest(account);
            }

            await this.context.Accounts.AddAsync(account);

            await this.context.SaveChangesAsync();
            return Created($"Accounts/Profiles/{account.AccountId}", account);
        }

        [HttpPut("Accounts/Profiles/{id}")]
        public async Task<IActionResult> UpdateAccountProfile(int id, Account account)
        {
            if (account is null || account.AccountId != id || !ModelState.IsValid)
            {
                return BadRequest(account);
            }

            if (!await TryUpdateModelAsync(account, "", 
                a => a.FirstName, a => a.LastName, a => a.AccountType))
            {
                return BadRequest(account);
            }

            await this.context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("Account/Bills/")]
        public async Task<IActionResult> CreateBill([Bind("BillSum,AccountId")] Bill bill)
        {
            if (bill is null || !ModelState.IsValid)
            {
                return BadRequest(bill);
            }

            await this.context.Bills.AddAsync(bill);

            await this.context.SaveChangesAsync();
            return Created(string.Empty, bill);
        }

        [HttpDelete("Accounts/Payments/{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payee = await this.context.Bills.FindAsync(id);
            this.context.Bills.Remove(payee);

            await this.context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("Accounts/Payee/")]
        public async Task<IActionResult> CreatePayee([Bind("FirstName,LastName")] Payee payee)
        {
            if (payee is null || !ModelState.IsValid)
            {
                return BadRequest(payee);
            }

            await this.context.Payees.AddAsync(payee);

            await this.context.SaveChangesAsync();
            return Created(string.Empty, payee);
        }

        [HttpPut("Accounts/Payee/{id}")]
        public async Task<IActionResult> UpdatePayee(int id, Payee payee)
        {
            if (payee is null || payee.PayeeId != id || !ModelState.IsValid)
            {
                return BadRequest(payee);
            }

            if (!await TryUpdateModelAsync(payee, "",
                p => p.FirstName, p => p.LastName))
            {
                return BadRequest(payee);
            }

            await this.context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("Accounts/Payee/{id}")]
        public async Task<IActionResult> DeletePayee(int id)
        {
            var payee = await this.context.Payees.FindAsync(id);
            this.context.Payees.Remove(payee);

            await this.context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("Accounts/fd/")]
        public async Task<IActionResult> CreateFixedDeposit([Bind("DepositRate,DepositAmount,AccountId")] FixedDeposit fd)
        {
            if (fd is null || !ModelState.IsValid)
            {
                return BadRequest(fd);
            }

            await this.context.FixedDeposits.AddAsync(fd);

            await this.context.SaveChangesAsync();
            return Created(string.Empty, fd);
        }

        [HttpPut("Accounts/fd/{id}")]
        public async Task<IActionResult> UpdateFixedDeposit(int id, FixedDeposit fd)
        {
            if (fd is null || fd.DepositId != id || !ModelState.IsValid)
            {
                return BadRequest(fd);
            }

            if (!await TryUpdateModelAsync(fd, "",
                f => f.DepositAmount, f => f.DepositRate))
            {
                return BadRequest(fd);
            }

            await this.context.SaveChangesAsync();
            return Ok();
        }
    }
}
