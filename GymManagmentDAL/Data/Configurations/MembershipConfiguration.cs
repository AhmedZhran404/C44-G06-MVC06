using GymManagmentDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.Configurations
{
    public class MembershipConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.Ignore(x => x.Id);

            builder.Property(x => x.CreatedAt)
                   .HasColumnName("StartDate")
                   .HasDefaultValueSql("GETDATE()");


            builder.HasOne(x => x.Plan)
                   .WithMany(x => x.PlanMembers)
                   .HasForeignKey(x => x.PlanId);

            builder.HasOne(x => x.Member)
                   .WithMany(x => x.MemberPlans)
                   .HasForeignKey(x => x.MemberId);

            builder.HasKey(x => new { x.PlanId, x.MemberId });

        }
    }
}
