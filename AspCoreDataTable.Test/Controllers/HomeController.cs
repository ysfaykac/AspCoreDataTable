﻿using AspCoreDataTable.Core.DataTable.ModelBinder;
using AspCoreDataTable.Core.DataTable.Storage;
using AspCoreDataTable.Core.Extensions;
using AspCoreDataTable.General;
using AspCoreDataTable.Test.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspCoreDataTable.Test.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LoadTable(JQueryDataTablesModel jQueryDataTablesModel)
        {
            try
            {
                List<Person> personList = new List<Person>
                {
                    new Person() {
                        id = Guid.NewGuid(),name="Linda",surname="Estrada",
                        PersonAdress = new PersonAdress
                        {
                            city="ankara",
                            country="Turkey"
                        }

                    },
                    new Person() {id = Guid.NewGuid(),name="George",surname="Davis",
                     PersonAdress = new PersonAdress
                        {
                            city="istanbul",
                            country="Turkey"
                        }},
                    new Person() {id = Guid.NewGuid(),name="Marilyn",surname="Shaw",
                      PersonAdress = new PersonAdress
                        {
                            city="berlin",
                            country="almanya"
                        }},
                    new Person() {id = Guid.NewGuid(),name="Terry",surname="Perez",
                     PersonAdress = new PersonAdress
                        {
                            city="liverpool",
                            country="ingiltere"
                        }},
                    new Person() {id = Guid.NewGuid(),name="Henry",surname="Freeman",
                     PersonAdress = new PersonAdress
                     {
                            city="paris",
                            country="fransa"
                        }},
                    new Person() {id = Guid.NewGuid(),name="ahmet",surname="bolat",
                     PersonAdress = new PersonAdress
                     {
                            city="brüksel",
                            country="belçika"
                        }}
                };

                List<Person> result = null;

                if (!string.IsNullOrEmpty(jQueryDataTablesModel.sSearch))
                {
                    result = personList.Where(t => t.name.StartsWith(jQueryDataTablesModel.sSearch) || t.surname.StartsWith(jQueryDataTablesModel.sSearch)).Skip(jQueryDataTablesModel.iDisplayStart).Take(jQueryDataTablesModel.iDisplayLength).ToList();
                }
                else
                {
                    result = personList.Skip(jQueryDataTablesModel.iDisplayStart).Take(jQueryDataTablesModel.iDisplayLength).ToList();
                }

                var storageObject = jQueryDataTablesModel.columnInfos.DeSerialize<Person>();
                var parser = new DatatableParser<Person>(result, storageObject);
                return Json(parser.Parse(jQueryDataTablesModel, personList.Count, result.Count));

            }
            catch(Exception ex)
            {
                // ignored
            }

            return null;

        }


        [HttpPost]
        public JsonResult LoadTable2([ModelBinder(binderType: typeof(JQueryDataTablesModelBinder))] JQueryDataTablesModel jQueryDataTablesModel)
        {
            try
            {
                List<Person> personList = new List<Person>
                {
                    new Person() {
                        id = Guid.NewGuid(),name="Linda",surname="Estrada",
                        PersonAdress = new PersonAdress
                        {
                            city="ankara",
                            country="Turkey"
                        }

                    },
                    new Person() {id = Guid.NewGuid(),name="George",surname="Davis",
                     PersonAdress = new PersonAdress
                        {
                            city="istanbul",
                            country="Turkey"
                        }},
                    new Person() {id = Guid.NewGuid(),name="Marilyn",surname="Shaw",
                      PersonAdress = new PersonAdress
                        {
                            city="berlin",
                            country="almanya"
                        }},
                    new Person() {id = Guid.NewGuid(),name="Terry",surname="Perez",
                     PersonAdress = new PersonAdress
                        {
                            city="liverpool",
                            country="ingiltere"
                        }},
                    new Person() {id = Guid.NewGuid(),name="Henry",surname="Freeman",
                     PersonAdress = new PersonAdress
                     {
                            city="paris",
                            country="fransa"
                        }},
                    new Person() {id = Guid.NewGuid(),name="ahmet",surname="bolat",
                     PersonAdress = new PersonAdress
                     {
                            city="brüksel",
                            country="belçika"
                        }}
                };

                List<Person> result = null;

                if (!string.IsNullOrEmpty(jQueryDataTablesModel.sSearch))
                {
                    result = personList.Where(t => t.name.StartsWith(jQueryDataTablesModel.sSearch) || t.surname.StartsWith(jQueryDataTablesModel.sSearch)).Skip(jQueryDataTablesModel.iDisplayStart).Take(jQueryDataTablesModel.iDisplayLength).ToList();
                }
                else
                {
                    result = personList.Skip(jQueryDataTablesModel.iDisplayStart).Take(jQueryDataTablesModel.iDisplayLength).ToList();
                }

                var storageObject = jQueryDataTablesModel.columnInfos.DeSerialize<Person>();
                var parser = new DatatableParser<Person>(result, storageObject);
                return Json(parser.Parse(jQueryDataTablesModel, personList.Count, result.Count));

            }
            catch (Exception ex)
            {
                // ignored
            }

            return null;

        }


        [HttpGet]
        public IActionResult AddOrEdit(string id)
        {
            return View("AddOrEdit");
        }
    }
}
