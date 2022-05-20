using ConsultarCep.Models;
using ConsultarCep.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace ConsultarCep.Controllers
{
    public class CepController : Controller
    {
        private readonly ICepRepository _cepRepository;

        public CepController(ICepRepository cepRepository)
        {
            _cepRepository = cepRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string cep)
        {

            if (cep == null)
            {
                return NotFound();
            }

            string cepFormat = cep.Replace("-", "");

            if (cepFormat.Length > 8)
            {
                throw new Exception("CEP inválido! ");
            }

            string viaCEPUrl = "https://viacep.com.br/ws/" + cepFormat + "/json/";

            try
            {
                using (var client = new WebClient())
                {
                    var res = client.DownloadString(viaCEPUrl);
                    var viaCep = JsonSerializer.Deserialize<CepModel>(res);
                    var cepExists = _cepRepository.GetById(cepFormat);
                    if (cepExists == null)
                    {
                        Create(viaCep);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var finalCep = _cepRepository.GetById(cepFormat);
            if (finalCep == null)
            {
                return NotFound();
            }

            return View(finalCep);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CepModel cep)
        {
            if (ModelState.IsValid)
            {
                _cepRepository.Add(cep);
                return RedirectToAction(nameof(Index));
            }

            return View(cep);
        }
    }
}
