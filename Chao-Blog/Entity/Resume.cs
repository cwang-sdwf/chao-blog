using System;
namespace Chao_Blog.Entity
{
    public class Resume
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set;}
        public string Introduction { get; set; }
        public string Skills { get; set; }
        public string Experiences { get; set; }
        public string Educations { get; set; }
        // public ICollection<Certification> Certifications { get; set; }
        public User User { get; set; }
    }
}
