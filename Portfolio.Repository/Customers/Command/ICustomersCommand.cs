using Portfolio.ViewModel.Customers.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Repository.Customers.Command
{
    public interface ICustomersCommand
    {
        void Add(CustomersRequest customersRequest, long userId);
    }
}
