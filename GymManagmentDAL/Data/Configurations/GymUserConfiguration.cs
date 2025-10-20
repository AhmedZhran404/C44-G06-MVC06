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
    public class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {


            // Name And Email And Phone Congiguration
            builder.Property(x => x.Name)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

            builder.Property(x => x.Email)
                   .HasColumnType("varchar")
                   .HasMaxLength(100);

            builder.Property(x => x.Phone)
                   .HasColumnType("varchar")
                   .HasMaxLength(11);


            // Address Propert Configuration
            builder.OwnsOne(x => x.Address, address =>
            {

                address.Property(x => x.BuildingNumber)
                       .HasColumnName("BuildingNumber")
                       .HasColumnType("varchar")
                       .HasMaxLength(30);

                address.Property(x => x.Street)
                       .HasColumnName("Street")
                       .HasColumnType("varchar")
                       .HasMaxLength(30);

                address.Property(x => x.City)
                       .HasColumnName("City")
                       .HasColumnType("varchar")
                       .HasMaxLength(30);
            });


            // Email And Phone Must Be Unique
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Phone).IsUnique();

            // Database Constrian
            builder.ToTable(x =>
            {
                x.HasCheckConstraint("GymUserEmailConstrain", "Email Like '_%@_%._%'");
                //x.HasCheckConstraint("GymUserPhoneConstrain", "Phone Like '01' and Phone Not Like '%[^0-9]%'");

                x.HasCheckConstraint("GymUserPhoneConstrain", "Phone LIKE '01%' AND Phone NOT LIKE '%[^0-9]%'");

            });

        }
    }
}
