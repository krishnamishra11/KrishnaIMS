﻿using IMS.BusinessLayer.Interfaces;
using IMS.IMSExceptions;
using IMS.Models;
using IMS.Repository.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

namespace IMS.BusinessLayer
{
    public class BLVendor: IBLVendor
    {
        private readonly IVendorRepository _repository;
        
        public BLVendor(IVendorRepository repository)
        {
            _repository = repository;
        }
        public void Add(Vendor vendor)
        {
            if (!IsCommunicationExists(vendor))
            {
                throw new NoMediumExists();
            }
            
            _repository.Add(vendor);
        }

        public void Edit(Vendor vendor)
        {

       
            if (!IsCommunicationExists(vendor))
            {
                throw new NoMediumExists();
            }

            _repository.Edit(vendor);

        }

        public Vendor FindById(int Id)
        {
            var vendor = _repository.FindById(Id);
            return vendor;
        }

        public IEnumerable FindByName(string Name)
        {
            return _repository.FindByName(Name);

        }

        public IEnumerable<Vendor> GetVendors()
        {
            return _repository.GetVendors();
        }

        public void Remove(int Id)
        {
            _repository.Remove(Id);
        }

        private bool IsCommunicationExists(Vendor vendor)
        {

            if (!String.IsNullOrWhiteSpace(vendor.Email) || !String.IsNullOrWhiteSpace(vendor.Fax) || !String.IsNullOrWhiteSpace(vendor.Mobile))
            {
                return true;
            }
            else
                return false;
            
        }

    }


}
