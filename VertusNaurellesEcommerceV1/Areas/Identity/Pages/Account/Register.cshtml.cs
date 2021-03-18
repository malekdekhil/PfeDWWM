using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using VertusNaurellesEcommerceV1.Models;

namespace VertusNaurellesEcommerceV1.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
          
            [Required(ErrorMessage = "Champ requis")]
            [Display(Name = "Nom")]
            public string FirstName { get; set; }
            [Required(ErrorMessage = "Champ requis")]
            [Display(Name = "Prenom")]
            public string LastName { get; set; }
            [Required(ErrorMessage = "Champ requis")]
            [Display(Name = "Adresse")]
            public string Address { get; set; }
            [Required(ErrorMessage = "Champ requis")]

            [Display(Name = "Ville")]
            public string City { get; set; }
            [Required(ErrorMessage = "Champ requis")]

            [Display(Name = "Code postal")]
            [Range(01000, 99999, ErrorMessage = "Code postal non valid")]
            [DataType(DataType.PhoneNumber,ErrorMessage ="")]
            public int CodeZip { get; set; }
            [Required(ErrorMessage = "Champ requis")]
            [Display(Name = "Pays")]
            public string Country { get; set; }
            [Required(ErrorMessage = "Champ requis")]
            [Display(Name = "Email")]
            [EmailAddress(ErrorMessage = "E-mail non valide.")]

            public string Email { get; set; }

            [Required(ErrorMessage = "Champ requis")]
            [StringLength(100, ErrorMessage = "Le {0} doit contenir au moins {2} et au maximum {1} caractères.", MinimumLength = 6)]
            [DataType(DataType.Password)]

            [Display(Name = "Mot de passe")]
            public string Password { get; set; }
            [Required(ErrorMessage = "Champ requis")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirmation du mot de passe")]
            [Compare("Password", ErrorMessage = "Le mot de passe de confirmation ne correspondent pas.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Address = Input.Address,
                    City = Input.City,
                    CodeZip = Input.CodeZip,
                    Country = Input.Country,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName
                    

                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("L'utilisateur a créé un nouveau compte avec mot de passe.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirmation de votre e-mail",
                        $"Veuillez confirmer votre compte par <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>cliquez ici</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);


                }
            }

            return Page();
        }
    }
}
