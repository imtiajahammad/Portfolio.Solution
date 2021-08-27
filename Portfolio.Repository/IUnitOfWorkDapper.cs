using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Repository
{
    public interface IUnitOfWorkDapper
    {
        //ICustomersCommand CustomersCommand { get; }
        bool Commit();
    }
}
