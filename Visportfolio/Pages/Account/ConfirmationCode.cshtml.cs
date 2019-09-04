using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Visportfolio.Models;

namespace Visportfolio.Pages.Account
{
    public class ConfirmationCodeModel : PageModel
    {

        readonly DatabaseContext _context;
        public IAccountService accountService;
        public ConfirmationCodeModel(IAccountService _accountService, DatabaseContext context)
        {
            this.accountService = _accountService;
            _context = context;
        }

        public string ConCode { get; set; }
        [Required(ErrorMessage = "Enter Confirmation Code")]
        public string ConfirmCode { get; set; }


        public ActionResult OnPost(string ConfirmCode)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string email = accountService.ValidateUserCode(ConfirmCode);
            if (accountService.UpdateUserValidFlag(email) == 1)
            {
                //change isvalid and start session
                return RedirectToPage("/CreatePortfolio");
            }
            else
            {
                return Page();
            }
        }
    }
}