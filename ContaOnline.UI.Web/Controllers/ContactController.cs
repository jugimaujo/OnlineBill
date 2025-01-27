using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using OnlineBill.Domain.Interfaces;
using OnlineBill.Domain.Models;
using OnlineBill.UI.Web.Code;
using OnlineBill.UI.Web.Models;

namespace OnlineBill.UI.Web.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly IAppHelper _appHelper;
        private readonly IToastNotification _toastrService;
        private string? loggedUserId;

        public ContactController(IContactRepository contactRepository, IAppHelper appHelper, IToastNotification toastrService)
        {
            _contactRepository = contactRepository;
            _toastrService = toastrService;
            _appHelper = appHelper;
        }

        // GET: ContactController
        public IActionResult Index()
        {
            loggedUserId = _appHelper.GetLoggedUser();

            var contactList = _contactRepository.GetAll(loggedUserId);

            return View(contactList);
        }

        // GET: ContactController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ContactViewModel contactViewModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(contactViewModel.Name))
                {
                    loggedUserId = _appHelper.GetLoggedUser();

                    Contact contact = contactViewModel.ConvertToContact(loggedUserId);

                    _contactRepository.Add(contact);

                    return RedirectToAction(nameof(Index));
                }

                return BadRequest(contactViewModel);
            }
            catch
            {
                return View(contactViewModel);
            }
        }

        // GET: ContactController/Edit
        public IActionResult Edit(string id)
        {
            var contact = _contactRepository.GetById(id);
            var contactViewModel = new ContactViewModel
            {
                Name = contact.Name,
                PrimaryTelephoneNumber = contact.PrimaryTelephoneNumber,
                SecondaryTelephoneNumber = contact.SecondaryTelephoneNumber,
                Email = contact.Email,
                Type = contact.Type
            };

            if (contact is Person)
            {
                contactViewModel.RG = ((Person)contact).RG;
                contactViewModel.CPF = ((Person)contact).CPF;
                contactViewModel.BirthDate = ((Person)contact).BirthDate;
            }
            else if (contact is Company)
            {
                contactViewModel.CNPJ = ((Company)contact).CNPJ;
            }

            return View(contactViewModel);
        }

        // POST: ContactController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ContactViewModel contactViewModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(contactViewModel.Name))
                {
                    var contactDTO = new Contact();

                    if (contactViewModel.Type == JuristicNaturalPerson.NaturalPerson)
                    {
                        contactDTO = new Person
                        {
                            CPF = contactViewModel.CPF,
                            RG = contactViewModel.RG,
                            BirthDate = contactViewModel.BirthDate
                        };
                    }
                    else if (contactViewModel.Type == JuristicNaturalPerson.JuristicPerson)
                    {
                        contactDTO = new Company
                        {
                            CNPJ = contactViewModel.CNPJ
                        };
                    }

                    contactDTO.Id = contactViewModel.Id;
                    contactDTO.Name = contactViewModel.Name;
                    contactDTO.PrimaryTelephoneNumber = contactViewModel.PrimaryTelephoneNumber;
                    contactDTO.SecondaryTelephoneNumber = contactViewModel.SecondaryTelephoneNumber;
                    contactDTO.Email = contactViewModel.Email;
                    contactDTO.Type = contactViewModel.Type;

                    _contactRepository.Update(contactDTO);

                    return RedirectToAction(nameof(Index));
                }

                return View(contactViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: ContactController/Delete/
        public IActionResult Delete(string id)
        {
            var contact = _contactRepository.GetById(id);

            if(!_appHelper.IsDeletable(contact))
            {
                _toastrService.AddErrorToastMessage("Não é possível excluir esse Contato, pois ele está sendo usado em suas contas.",
                new ToastrOptions { Title = "Erro ao excluir" });

                return RedirectToAction(nameof(Index));
            }

            var contactViewModel = new ContactViewModel
            {
                Name = contact.Name,
                PrimaryTelephoneNumber = contact.PrimaryTelephoneNumber,
                SecondaryTelephoneNumber = contact.SecondaryTelephoneNumber,
                Email = contact.Email,
                Type = contact.Type
            };

            if (contact is Person)
            {
                contactViewModel.RG = ((Person)contact).RG;
                contactViewModel.CPF = ((Person)contact).CPF;
                contactViewModel.BirthDate = ((Person)contact).BirthDate;
            }
            else if (contact is Company)
            {
                contactViewModel.CNPJ = ((Company)contact).CNPJ;
            }

            return View(contactViewModel);
        }

        // POST: ContactController/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                _contactRepository.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
