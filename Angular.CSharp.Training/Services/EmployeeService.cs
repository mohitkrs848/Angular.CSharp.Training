using Angular.CSharp.Training.Data;
using Angular.CSharp.Training.Data.Repository;
using Angular.CSharp.Training.Models;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Web;

namespace Angular.CSharp.Training.Agents
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger logger;

        public EmployeeService(IEmployeeRepository employeeRepository, ILogger logger)
        {
            _employeeRepository = employeeRepository;
            this.logger = logger;
        }

        public async Task CreateEmployee(Employee employee)
        {
            try
            {
                await _employeeRepository.CreateEmoployee(employee);
                logger.Information($"Employee created successfully");
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Error(ex, $"Error creating employee");
                throw; // Rethrow the exception
            }
        }

        public async Task DeleteEmployee(int id)
        {
            try
            {
                await _employeeRepository.DeleteEmployee(id);
                logger.Information($"Employee with ID {id} deleted successfully");
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Error(ex, $"Error deleting employee with ID {id}");
                throw; // Rethrow the exception
            }
        }

        public async Task UpdateEmployee(Employee employee)
        {
            try
            {
                await _employeeRepository.UpdateEmployee(employee);
                logger.Information($"Employee with ID {employee.Id} updated successfully");
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Error(ex, $"Error updating employee with ID {employee.Id}");
                throw; // Rethrow the exception
            }
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeById(id);
                logger.Information($"Employee with ID {id} retrieved successfully");
                return employee;
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Error(ex, $"Error getting employee with ID {id}");
                throw; // Rethrow the exception
            }
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            try
            {
                var employees = await _employeeRepository.GetAllEmployees();
                logger.Information($"All employees retrieved successfully");
                return employees;
            }
            catch (Exception ex)
            {
                // Log the exception
                Log.Error(ex, $"Error getting all employees");
                throw; // Rethrow the exception
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByFilter(string email, int employeeId)
        {
            try
            {
                var employees = await _employeeRepository.GetEmployeesByFilter(email, employeeId);
                logger.Information($"Employees retrieved successfully by filter");
                return employees;
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Error(ex, $"Error getting employees by filter");
                throw; // Rethrow the exception
            }
        }

        public async Task<IEnumerable<Employee>> SearchEmployees(string query)
        {
            try
            {
                var employees = await _employeeRepository.SearchEmployees(query);
                logger.Information($"Employees searched successfully");
                return employees;
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Error(ex, $"Error searching employees");
                throw; // Rethrow the exception
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDepartment(string department)
        {
            try
            {
                var employees = await _employeeRepository.GetEmployeesByDepartment(department);
                logger.Information($"Employees retrieved successfully by department");
                return employees;
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Error(ex, $"Error getting employees by department");
                throw; // Rethrow the exception
            }
        }

        public async Task<int> GenerateEmployeeId()
        {
            try
            {
                var employeeId = await _employeeRepository.GenerateEmployeeId();
                logger.Information($"Employee ID generated successfully");
                return employeeId;
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Error(ex, $"Error generating employee ID");
                throw; // Rethrow the exception
            }
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees(string department, string designation, int? age, decimal? salaryMin, decimal? salaryMax, string location, string status, int? projectId)
        {
            try
            {
                var employees = await _employeeRepository.GetAllEmployees(department, designation, age, salaryMin, salaryMax, location, status, projectId);
                logger.Information($"All employees retrieved successfully with filters");
                return employees;
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Error(ex, $"Error getting all employees with filters");
                throw; // Rethrow the exception
            }
        }

        public async Task<object> GetDistinctValues()
        {
            try
            {
                var distinctValues = await _employeeRepository.GetDistinctValues();
                logger.Information($"Distinct values retrieved successfully");
                return distinctValues;
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Error(ex, $"Error getting distinct values");
                throw; // Rethrow the exception
            }
        }

        public async Task<bool> CheckEmailExistence(string email, int? id)
        {
            try
            {
                var emailExists = await _employeeRepository.CheckEmailExistence(email, id);
                logger.Information($"Email existence checked successfully");
                return emailExists;
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Error(ex, $"Error checking email existence");
                throw; // Rethrow the exception
            }
        }
    }
}