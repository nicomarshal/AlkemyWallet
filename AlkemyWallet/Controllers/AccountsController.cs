﻿using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlkemyWallet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize("Admin, Regular")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET: api/<AccountController>
        [HttpGet]
        [Authorize("Admin")]
        public async Task<IEnumerable<AccountDTO>> Get()
        {
            return await _accountService.GetAllAsync();
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        [Authorize("Admin")]
        public async Task<AccountDTO> Get(int id)
        {
            return await _accountService.GetByIdAsync(id);
        }

        //// POST api/<AccountController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PatchAsync(int id, AccountUpdateDTO accountDTO)
        {
            var updatedAccount = await _accountService.UpdateAsync(id, accountDTO);
            if (updatedAccount == null)
                return NotFound();
            
            return Ok(updatedAccount);
        }

        //// DELETE api/<AccountController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}


        // POST api/<AccountController>/5
        [HttpPost("{id}")]
        [Authorize(Roles = "Regular")]
        public async Task TransactionAsync (int id, TransactionDTO transactionDTO)
        {   
            var account = await _accountService.GetByIdAsync(id);
            //Obtenemos el User_id del Token de lac uenta logueada
            var userId = int.Parse(User.FindFirst("UserId").Value);

            if (userId == account.User_Id)
            {
                if (transactionDTO.ToAccountId != id)
                {   
                    await _accountService.TransferAsync(id, transactionDTO);
                }
                else
                {
                    //await _accountService.DepositAsync(userId, id, transactionDTO);
                }
            }
        }

        // POST api/<AccountController>/5
        /*[HttpPost("{id}")]
        [Authorize(Roles = "Regular")]
        public async Task DepositAsync (int id, TransactionTransferDTO transactionDTO)
        {     
            await _transactionService.CreateTransactionAsync(id, transactionDTO);
        }*/
    }
}
