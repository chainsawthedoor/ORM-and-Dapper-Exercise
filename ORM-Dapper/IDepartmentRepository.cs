namespace ORM_Dapper;

public interface IDepartmentRepository
{
    //Saying we need a method called GetAllDepartments that returns a collection
    // That conforms to IEnumerable<T>
    IEnumerable<Department> GetAllDepartments();
    void InsertDepartment(string newDepartmentName);
}