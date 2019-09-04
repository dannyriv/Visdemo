using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Visportfolio.Models;

namespace Visportfolio.Pages
{
    public class RegisterModel : PageModel
    {
        readonly DatabaseContext _context;
        public IAccountService accountService;
        public RegisterModel(IAccountService _accountService, DatabaseContext context)
        {
            this.accountService = _accountService;
            _context = context;
        }

        [BindProperty]
        public VisUser _VisUser { get; set; }
        [BindProperty]
        public Email mails { get; set; }

        [Required(ErrorMessage = "Enter User Password"), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        // Generate a random password    
        public string RandomCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }

        // Generate a random string with a given size    
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        // Generate a random number between two numbers    
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public ActionResult OnPost()
        {
            _VisUser.ConfirmationCode = RandomCode();
            var visUser = _VisUser;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if(visUser.UserPassword.CompareTo(ConfirmPassword) == 1)
            {

                visUser.UserId = 0;
                var result = _context.Add(visUser);
                _context.SaveChanges();

                SendEmail();

                TempData["ConCode"] = visUser.ConfirmationCode;

                return RedirectToPage("/Account/ConfirmationCode");
            }

            return Page();
        }

        public async Task SendEmail()
        {
            using(var smtp = new SmtpClient())
            {
                mails.To = _VisUser.Email;
                mails.From = "danny@Vispires.com";
                mails.Subject = "Registration Confirmation Code";
                mails.Body = _VisUser.ConfirmationCode;

                smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                smtp.PickupDirectoryLocation = @"c:\temp";
                var msg = new MailMessage
                {
                    Body = mails.Body,
                    Subject = mails.Subject,
                    From = new MailAddress(mails.From)
                };
                msg.To.Add(mails.To);
                await smtp.SendMailAsync(msg);

            }
        }
    }
}