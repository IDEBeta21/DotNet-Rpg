using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace DotNetRPG.API.Dtos.User
{
    public class UserLoginDtoRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } =  string.Empty;
    }

    public class UserLoginValidator : AbstractValidator<UserLoginDtoRequest>
    {
        public UserLoginValidator()
        {
            RuleFor(user => user.Username)
                .NotEmpty().WithSeverity(Severity.Warning).WithMessage("Username should not be null");

            RuleFor(user => user.Password)
                .NotEmpty().WithSeverity(Severity.Warning).WithMessage("Password should not be null");
        }
    }
}