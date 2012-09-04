using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppToTestMoq
{
    class Program
    {

        static void Main(string[] args)
        {

            List<question> qs = new List<question>();
            List<answer> anss = new List<answer>();
            List<answer> ansb = new List<answer>();

            answer a = new answer(){AnswerDesc="abc",AnswerId=11};
            answer a1 = new answer(){AnswerDesc=null,AnswerId=0};
            answer a2 = new answer(){AnswerDesc="pqr",AnswerId=13};
            answer a3 = new answer(){AnswerDesc="abc",AnswerId=14};
            anss.Add(a);
            anss.Add(a1);
            anss.Add(a2);
            anss.Add(a3);


            
            answer b1 = new answer() { AnswerDesc = "bbc", AnswerId = 21 };
            answer b2 = new answer() { AnswerDesc = null, AnswerId = 0 };
            answer b3 = new answer() { AnswerDesc = "owp", AnswerId = 23 };
            
            ansb.Add(b1);
            ansb.Add(b2);
            ansb.Add(b3);

            question q1 = new question() { id = 1, ans = anss };
            question q2 = new question() { id = 2, ans = ansb };

            qs.Add(q1);
            qs.Add(q2);

            Func<answer, bool> UpdateAns = x => { if (string.IsNullOrEmpty(x.AnswerDesc)) { x.AnswerDesc = "zoop"; return true; } else { return false; };};


            var list = qs.Select(d => { d.ans.ForEach(x => UpdateAns(x)); return d; }).ToList();




            //var list = qs.Select(d => { d.ans.ForEach(x => { if (string.IsNullOrEmpty(x.AnswerDesc)) x.AnswerDesc = "jjop"; }); return d; }).ToList();


            Func<int, bool> myFunc = x =>x == 5;




            bool result = myFunc(4); // returns false of course



            var ss = anss;


                           
                        









        }
    }

    public class question
    {
        public int id { get; set; }
        public List<answer> ans { get; set; }
    }

    public class answer
    {
        public int AnswerId { get; set; }
        public string AnswerDesc { get; set; }
    }





    public interface IRepository
    {
        void Add(ICustomer cust);
        IQueryable<ICustomer> GetAll();
        ICustomer Get(int code);
        void Delete(int code);

    }

    public class Repository : IRepository
    {
        private IQueryable<ICustomer> _data;
        public Repository(IQueryable<ICustomer> data)
        {
            _data = data;

        }
        public void Add(ICustomer cust)
        {
            
        }

        public IQueryable<ICustomer> GetAll()
        {
            return _data.AsQueryable();
        }

        public ICustomer Get(int code)
        {
            return _data.Where(x => x.Code == code).SingleOrDefault();
        }

        public void Delete(int code)
        {
            
        }
    }

    public interface IService
    {
        void AddCustomer(ICust cust);
        void DeleteCustomer(int code);
        IQueryable<ICust> GetCustAll();
        ICust GetCust(int code);

    }

     public interface ICust
    {
        int Code { get; set; }
        string Name { get; set; }
    }
    public class Cust : ICust
    {
        public int Code { get; set; }
        public string Name { get; set; }
    }

    public interface ICustomer
    {
        int Code { get; set; }
        string Name { get; set; }
    }
    public class Customer : ICustomer
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public IQueryable<ICustomer> Data()
        {
            List<ICustomer> custs=new List<ICustomer>(){
                new Customer{Code=1,Name="ABCD"},
                new Customer{Code=2,Name="PQRST"},
                new Customer{Code=3,Name="LMNOP"},
                new Customer{Code=4,Name="WXYZ"}};

            return custs.AsQueryable();
        }
    }

    public class CustomerService : IService
    {
        private IRepository _repo;
        
        public CustomerService(IRepository repo)
        {
            _repo = repo;

        }


        public void AddCustomer(ICust cust)
        {
            ICustomer _cust = new Customer();
            _cust.Code=cust.Code;
            _cust.Name=cust.Name;
            _repo.Add(_cust);
        }

        public void DeleteCustomer(int code)
        {
            _repo.Delete(code);
        }

        public IQueryable<ICust> GetCustAll()
        {
            var custlist = _repo.GetAll().Select(x=> new Cust {Code=x.Code,Name=x.Name}).AsQueryable();;
            return custlist;
        }

        public ICust GetCust(int code)
        {
            var cus=_repo.Get(1);

            return new Cust { Code = cus.Code, Name = cus.Name };
        }
    }

}
