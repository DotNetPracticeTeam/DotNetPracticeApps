//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.EntityClient;

namespace Adventure.Data
{
    public partial class AdventureEntities : ObjectContext
    {
        public const string ConnectionString = "name=AdventureEntities";
        public const string ContainerName = "AdventureEntities";
    
        #region Constructors
    
        public AdventureEntities()
            : base(ConnectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public AdventureEntities(string connectionString)
            : base(connectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public AdventureEntities(EntityConnection connection)
            : base(connection, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        #endregion
    
        #region ObjectSet Properties
    
        public ObjectSet<Department> Departments
        {
            get { return _departments  ?? (_departments = CreateObjectSet<Department>("Departments")); }
        }
        private ObjectSet<Department> _departments;
    
        public ObjectSet<Employee> Employees
        {
            get { return _employees  ?? (_employees = CreateObjectSet<Employee>("Employees")); }
        }
        private ObjectSet<Employee> _employees;
    
        public ObjectSet<Product> Products
        {
            get { return _products  ?? (_products = CreateObjectSet<Product>("Products")); }
        }
        private ObjectSet<Product> _products;
    
        public ObjectSet<PurchaseOrderDetail> PurchaseOrderDetails
        {
            get { return _purchaseOrderDetails  ?? (_purchaseOrderDetails = CreateObjectSet<PurchaseOrderDetail>("PurchaseOrderDetails")); }
        }
        private ObjectSet<PurchaseOrderDetail> _purchaseOrderDetails;
    
        public ObjectSet<PurchaseOrderHeader> PurchaseOrderHeaders
        {
            get { return _purchaseOrderHeaders  ?? (_purchaseOrderHeaders = CreateObjectSet<PurchaseOrderHeader>("PurchaseOrderHeaders")); }
        }
        private ObjectSet<PurchaseOrderHeader> _purchaseOrderHeaders;
    
        public ObjectSet<ShipMethod> ShipMethods
        {
            get { return _shipMethods  ?? (_shipMethods = CreateObjectSet<ShipMethod>("ShipMethods")); }
        }
        private ObjectSet<ShipMethod> _shipMethods;
    
        public ObjectSet<Vendor> Vendors
        {
            get { return _vendors  ?? (_vendors = CreateObjectSet<Vendor>("Vendors")); }
        }
        private ObjectSet<Vendor> _vendors;

        #endregion
    }
}
