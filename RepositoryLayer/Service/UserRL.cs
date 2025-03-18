using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly AddressBookContext _context;
        private readonly IMapper _mapper;

        public UserRL(AddressBookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public User GetEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public User RegisterRL(User user)
        {
            try
            {
                var data = _context.Users.FirstOrDefault(e => e.Email == user.Email);
                if (data != null)
                {
                    throw new Exception("User Already registered!");
                }

                _context.Users.Add(user);
                _context.SaveChanges();
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User LoginRL(UserDTO userDTO)
        {
            try
            {
                var data = _context.Users.FirstOrDefault(e => e.Email == userDTO.Email);
                if (data != null)
                {
                    return data;
                }
                throw new NullReferenceException();
            }
            catch (NullReferenceException)
            {
                throw;
            }
        }
        public bool UpdatePasswordRL(User user)
        {

            _context.Users.Update(user);
            _context.SaveChanges();
            return true;
        }
    }
}