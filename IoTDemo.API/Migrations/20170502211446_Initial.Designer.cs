using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using IoTDemo.API.Models;

namespace IoTDemo.API.Migrations
{
    [DbContext(typeof(IoTDemoDbContext))]
    [Migration("20170502211446_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IoTDemo.API.Models.IoTData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int>("NameId");

                    b.Property<float>("Value");

                    b.HasKey("Id");

                    b.ToTable("IoTData");
                });
        }
    }
}
