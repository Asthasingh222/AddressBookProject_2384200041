using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using RepositoryLayer.Entity;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        User RegisterBL(UserDTO userDTO);
        string LoginBL(UserDTO userDTO);
        bool ForgetPasswordBL(ForgetPasswordDTO forgetPasswordDTO);
        bool ResetPasswordBL(ResetPasswordDTO resetPasswordDTO);
    }
}