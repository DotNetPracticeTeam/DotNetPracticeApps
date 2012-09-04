using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Domain.Entities;
using Adventure.Data.Infrastructure;
using Adventure.Data;
using AutoMapper;
namespace Adventure.Service
{
    public interface IDepartmentService
    {
        dDepartment GetDepartment(int departmentID);
        IEnumerable<dDepartment> GetAll();
        void AddDepartment(dDepartment department);
        void UpdateDepartment(int departmentID, dDepartment department);
        void DeleteDepartment(int departmentID);
    }
    public class DepartmentService:IDepartmentService
    {
        private IDepartmentRepository _repository;
        


        public DepartmentService(IDepartmentRepository departmentrepository)
        {
            
            _repository = departmentrepository;
            Mapper.CreateMap<Department, dDepartment>();
        }

        public dDepartment GetDepartment(int departmentID)
        {
            var department = _repository.Get(dept => dept.DepartmentID == departmentID);
            var pocoDept = Mapper.Map<Department, dDepartment>(department);
            return pocoDept;
        }

        public IEnumerable<dDepartment> GetAll()
        {
            var department = _repository.GetAll();
            var pocoDeptList = Mapper.Map<List<Department>, List<dDepartment>>(department.ToList());
            return pocoDeptList;
        }

        public void AddDepartment(dDepartment department)
        {
            throw new NotImplementedException();
        }

        public void UpdateDepartment(int departmentID, dDepartment department)
        {
            throw new NotImplementedException();
        }

        public void DeleteDepartment(int departmentID)
        {
            throw new NotImplementedException();
        }
    }
}
