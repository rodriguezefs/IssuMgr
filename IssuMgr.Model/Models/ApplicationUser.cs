﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IssuMgr.Model.Models {
    public class ApplicationUser {
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public int Id { get; set; }

        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string UserName { get; set; }
    }
}
