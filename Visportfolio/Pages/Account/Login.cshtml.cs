﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Visportfolio.Models;

namespace Visportfolio.Pages
{
    public class LoginModel : PageModel
    {

        readonly DatabaseContext _context;
        public IAccountService accountService;
        public LoginModel(IAccountService _accountService, DatabaseContext context)
        {
            this.accountService = _accountService;
            _context = context;
        }


        [Required(ErrorMessage = "Enter User Name")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "Enter User Password"), DataType(DataType.Password)]
        public string Password { get; set; }


        public void OnGet()
        {

        }

        public ActionResult OnPost(string UserName)
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if(accountService.ValidateUserLogin(UserName, Password) == 1)
            {
                //send email with automatic password
                return RedirectToPage("/CreatePortfolio");
            }
            else
            {
                //report error to user
                return Page();
            }
        }
    }
}