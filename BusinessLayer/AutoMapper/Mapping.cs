using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using AutoMapper;
using RepositoryLayer.Entity;

namespace BusinessLayer.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AddressBookEntry, AddressBookDTO>();
            CreateMap<AddressBookDTO, AddressBookEntry>();
        }
    }
}