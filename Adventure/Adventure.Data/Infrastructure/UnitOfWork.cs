using System.Data.Objects;


namespace Adventure.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
		public ObjectContext Context { get; set; }

		public UnitOfWork()
		{
			Context = new Adventure.Data.AdventureEntities();
		}

		public void Commit()
		{
			Context.SaveChanges();
		}
		
		public bool LazyLoadingEnabled
		{
			get { return Context.ContextOptions.LazyLoadingEnabled; }
			set { Context.ContextOptions.LazyLoadingEnabled = value;}
		}

		public bool ProxyCreationEnabled
		{
			get { return Context.ContextOptions.ProxyCreationEnabled; }
			set { Context.ContextOptions.ProxyCreationEnabled = value; }
		}
		
		public string ConnectionString
		{
			get { return Context.Connection.ConnectionString; }
			set { Context.Connection.ConnectionString = value; }


		}

    }
}
