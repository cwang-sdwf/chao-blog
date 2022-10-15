﻿using System;
using System.Collections.Generic;

namespace Chao_Blog.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }
        public Level UserLevel { get; set; }
        // public ICollection<Resume> Resumes { get; set; }


    }

}

