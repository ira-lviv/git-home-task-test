﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Models.Models;
using WebApi.ViewModels;

namespace WebApi.Authorization
{
    public class SameStudentRequirementAuthorizationHandler : AuthorizationHandler<SameStudentRequirement, Student>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            SameStudentRequirement requirement,
            Student student)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }

            if (context.User.Identity?.Name == student.Email)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
