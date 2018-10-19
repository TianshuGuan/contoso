using Contoso.ViewModels;
using ContosoDomain.Models;
using ContosoService.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Contoso.Controllers
{
    public class DepartmentController : BaseController
    {
        // GET: Department
        protected readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService service):base(service)
        {
            _departmentService = service;
        }
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            getUserIdentity(userId);
            var departments = _departmentService.GetAllDepartments();
            List<BaseDepartmentDTO> dtos = new List<BaseDepartmentDTO>();
            foreach (var department in departments)
            {
                dtos.Add(new BaseDepartmentDTO(department));
            }
            return View(dtos);
        }

        public ActionResult Detail(string name)
        {
            string userId = User.Identity.GetUserId();
            getUserIdentity(userId);
            var department = _departmentService.GetByName(name);
            DepartmentDTO dto = new DepartmentDTO(department);
            return View(dto);
        }

        public ActionResult Delete(string name)
        {
            var department = _departmentService.GetByName(name);
            DepartmentEditDTO dto = new DepartmentEditDTO(department);
            return View(dto);
        }
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            _departmentService.Delete(Id);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DepartmentDTO dto)
        {
            string userId = User.Identity.GetUserId();
            getUserIdentity(userId);
            Department department = dto.toEntity();
            _departmentService.Create(department);
            return View();
        }

        public ActionResult Edit(string name)
        {
            var department = _departmentService.GetByName(name);
            DepartmentEditDTO dto = new DepartmentEditDTO(department);
            return View(dto);
        }

        [HttpPost]
        public ActionResult Edit(DepartmentEditDTO dto)
        {
            Department department = dto.toEntity();
            _departmentService.Update(department);
            return RedirectToAction("Index");
        }
    }
}