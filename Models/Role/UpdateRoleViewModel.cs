﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fochso.Models.Role
{
    public class UpdateRoleViewModel
    {
        public int Id { get; set; }

        [ReadOnly(true)]
        public string RoleName { get; set; }

        [MinLength(5, ErrorMessage = "The minimum length acceptable is 5")]
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
