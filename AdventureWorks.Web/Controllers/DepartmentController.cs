using System.Collections.Generic;
using System.Web.Mvc;
using AdventureWorks.Services.HumanResources;
using Microsoft.ApplicationInsights;

namespace AdventureWorks.Web.Controllers
{
    public class DepartmentsController : Controller
    {

        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: Departments
        public ActionResult Index()
        {
            TelemetryClient telemetry = new TelemetryClient();
            telemetry.TrackPageView("Loading Departments page");
            var departmentGroups = _departmentService.GetDepartments();

            return View(departmentGroups);
        }

        // GET: Departments/Employees/{id}
        public ActionResult Employees(int id)
        {
            TelemetryClient telemetry = new TelemetryClient();
            var properties = new Dictionary<string, string>
                    {{"employeeId", id.ToString()}};

            telemetry.TrackEvent("Loading employee page", properties);
            var departmentEmployees = _departmentService.GetDepartmentEmployees(id);
            var departmentInfo = _departmentService.GetDepartmentInfo(id);

            ViewBag.Title = "Employees in " + departmentInfo.Name + " Department";

            return View(departmentEmployees);
        }
    }
}
