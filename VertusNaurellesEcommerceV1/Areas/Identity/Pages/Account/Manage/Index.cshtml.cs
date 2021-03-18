using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VertusNaurellesEcommerceV1.Models;

namespace VertusNaurellesEcommerceV1.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public IndexModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }



        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]

        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Tel")]
            [Range(010000000, +9999999999, ErrorMessage = "le champ {0} est non valid")]
            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }
            public string Username { get; set; }
            [Display(Name = "Adresse")]

            public string Address { get; set; }
            [Display(Name = "Ville")]
          

            public string City { get; set; }
            [Display(Name = "Pays")]
            public string Country { get; set; }
            [Display(Name = "Code postal")]
            public int CodeZip { get; set; }
        }
        private async Task LoadAsync(AppUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var UpdateUser = await _userManager.GetUserAsync(User);

            //Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Address = UpdateUser.Address,
                City =  UpdateUser.City,
                Country = UpdateUser.Country,
                CodeZip = UpdateUser.CodeZip

            };

        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Impossible de charger l'utilisateur avec l'ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Impossible de charger l'utilisateur avec l'ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Erreur inattendue lors de la tentative de définition du numéro de téléphone.";
                    return RedirectToPage();
                }
            }
           
            user.Address = Input.Address;
            user.Country = user.Country;
            user.CodeZip = Input.CodeZip;
            user.City = Input.City;
            user.FirstName = user.FirstName;
            user.LastName = user.LastName;
            IdentityResult result = await _userManager.UpdateAsync(user);



          
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Votre profil a été mis a jour.";
            return RedirectToPage();
        }
    }
}
