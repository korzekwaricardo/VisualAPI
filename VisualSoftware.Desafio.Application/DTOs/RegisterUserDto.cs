using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftware.Desafio.Application.DTOs
{
    public record RegisterUserDto(
    string Username,
    string Email,
    string Password,
    string Role
);
}
