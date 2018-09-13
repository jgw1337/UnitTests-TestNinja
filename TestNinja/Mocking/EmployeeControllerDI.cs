namespace TestNinja.Mocking
{
    public class EmployeeControllerDI
    {
        private IEmployeeStorage _empStore;

        public EmployeeControllerDI(IEmployeeStorage empStore = null)
        {
            _empStore = empStore ?? new EmployeeStorage();
        }

        public ActionResult DeleteEmployee(int id)
        {
            _empStore.Delete(id);
            return RedirectToAction("Employees");
        }

        private ActionResult RedirectToAction(string employees)
        {
            return new RedirectResult();
        }
    }
}