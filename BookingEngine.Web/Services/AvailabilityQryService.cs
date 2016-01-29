using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookingEngine.Data;
using BookingEngine.Data.Entities;
using BookingEngine.Web.Models;

namespace BookingEngine.Web.Services
{
    public class AvailabilityQryService
    {
        private IUnitOfWorkFactory _factory;

        public AvailabilityQryService()
        {

        }
        public AvailabilityQryService(IUnitOfWorkFactory f)
        {
            _factory = f;

        }
    }
}