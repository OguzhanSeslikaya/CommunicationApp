using Communication.Entity;
using Communication.Entity.Models.Company;
using Communication.Entity.Models.File.LocalStorage;
using Communication.Entity.Models.User.Identity;
using Communication.Entity.Models.User.UserPosts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.DataAccess.Contexts
{
    public class CommunicationAppDBContext : IdentityDbContext<AppUser>
    {
        public DbSet<Group> groups { get; set; }
        public DbSet<Endpoint> endpoints { get; set; }
        public DbSet<UserPost> posts { get; set; }
        public DbSet<GroupUser> groupUser { get; set; }
        public DbSet<PostFile> postFiles { get; set; }
        public DbSet<CallFile> callFiles { get; set; }
        public DbSet<Call> calls { get; set; }
        public DbSet<RequestToJoinGroup> requestsToJoinGroup { get; set; }
        public DbSet<GroupUserMessage> groupUserMessages { get; set; }

        public CommunicationAppDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<GroupUser>().HasOne(a => a.user);
            builder.Entity<GroupUser>().HasOne(a => a.group);

            builder.Entity<AppUser>().HasMany(a => a.posts).WithOne(a => a.user);

            builder.Entity<UserPost>().HasOne(a => a.postFile);
            builder.Entity<UserPost>().HasOne(a => a.user).WithMany(a => a.posts);
            builder.Entity<UserPost>().HasOne(a => a.group);

            builder.Entity<RequestToJoinGroup>().HasOne(a => a.group);
            builder.Entity<RequestToJoinGroup>().HasOne(a => a.user);

            builder.Entity<GroupUserMessage>().HasOne(a => a.sender);
            builder.Entity<GroupUserMessage>().HasOne(a => a.receiver);
            builder.Entity<GroupUserMessage>().HasOne(a => a.group);

            builder.Entity<Call>().HasOne(a => a.called);
            builder.Entity<Call>().HasOne(a => a.caller);
            builder.Entity<Call>().HasMany(a => a.callFiles).WithOne(a => a.call);
            builder.Entity<Call>().HasOne(a => a.group);


            builder.Entity<Group>().HasAlternateKey(a => a.name);

            

            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.createdDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }

}
