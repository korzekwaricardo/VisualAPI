using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftware.Desafio.Application.DTOs;

namespace VisualSoftware.Desafio.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDto dto);
        Task RegisterAsync(RegisterUserDto dto);
    }
}
