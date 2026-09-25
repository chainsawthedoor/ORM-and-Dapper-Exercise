using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace ORM_Dapper;

public class DapperDepartmentRepository : IDepartmentRepository
{
    // Field or local variable for making queries in database
    private readonly IDbConnection _connection;
    public DapperDepartmentRepository(IDbConnection connection)
    {
        //Constructor
        _connection = connection;
    }



    public IEnumerable<Department> GetAllDepartments()
    {
        var depos = _connection.Query<Department>("SELECT * FROM departments");

        return depos;
    }

    public void InsertDepartment(string newDepartmentName)
    {
        //Parameteratized query
                                                                  //@ = a variable (value) to give this row of value a name
        _connection.Execute("INSERT INTO DEPARTMENTS (Name) VALUES (@departmentName);", new {departmentName = newDepartmentName});
                                                                                      //Anonymous type (think lamdia)
    }

}