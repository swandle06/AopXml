using System;
using System.Collections.Generic;

namespace AopWikiExporter.Data
{
    public partial class User : IAopWikiIdentifiable
    {
        public User()
        {
            this.AopContributors = new HashSet<AopContributor>();
            this.AopLogs = new HashSet<AopLog>();
            this.Aops = new HashSet<Aop>();
            this.Comments = new HashSet<Comment>();
            this.EventLogs = new HashSet<EventLog>();
            this.RelationshipLogs = new HashSet<RelationshipLog>();
            this.StressorLogs = new HashSet<StressorLog>();
            this.TermRequests = new HashSet<TermRequest>();
            this.Watches = new HashSet<Watch>();
        }

        public int Id { get; set; }
        public DateTime? ConfirmationSentAt { get; set; }
        public string ConfirmationToken { get; set; }
        public DateTime? ConfirmedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CurrentSignInAt { get; set; }
        public string CurrentSignInIp { get; set; }
        public string Email { get; set; }
        public string EncryptedPassword { get; set; }
        public DateTime? LastSignInAt { get; set; }
        public string LastSignInIp { get; set; }
        public DateTime? RememberCreatedAt { get; set; }
        public DateTime? ResetPasswordSentAt { get; set; }
        public string ResetPasswordToken { get; set; }
        public int SignInCount { get; set; }
        public sbyte? Subscribed { get; set; }
        public string UnconfirmedEmail { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<AopContributor> AopContributors { get; set; }
        public virtual ICollection<AopLog> AopLogs { get; set; }
        public virtual ICollection<Aop> Aops { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<EventLog> EventLogs { get; set; }
        public virtual ICollection<RelationshipLog> RelationshipLogs { get; set; }
        public virtual ICollection<StressorLog> StressorLogs { get; set; }
        public virtual ICollection<TermRequest> TermRequests { get; set; }
        public virtual ICollection<Watch> Watches { get; set; }
    }
}
