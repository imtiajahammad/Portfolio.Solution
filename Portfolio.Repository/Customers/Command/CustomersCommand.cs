﻿using Dapper;
using Portfolio.ViewModel.Customers.Request;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Portfolio.Repository.Customers.Command
{
    public class CustomersCommand:ICustomersCommand
    {
        private readonly IDbConnection _dbConnection;
        private readonly IDbTransaction _dbTransaction;
        public CustomersCommand(IDbTransaction dbTransaction, IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _dbTransaction = dbTransaction;
        }
        public void Add(CustomersRequest customersViewModel, long userId)
        {
            var param = new DynamicParameters();
            param.Add("@FirstName", customersViewModel.FirstName);
            param.Add("@LastName", customersViewModel.LastName);
            param.Add("@MobileNo", customersViewModel.MobileNo);
            param.Add("@LandlineNo", customersViewModel.LandlineNo);
            param.Add("@EmailId", customersViewModel.EmailId);
            param.Add("@Street", customersViewModel.Street);
            param.Add("@City", customersViewModel.City);
            param.Add("@State", customersViewModel.State);
            param.Add("@Pincode", customersViewModel.Pincode);
            param.Add("@CreatedBy", userId);
            _dbConnection.Execute("Usp_InsertCustomer", param, _dbTransaction, 0, CommandType.StoredProcedure);
        }
    }
}
