using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using IoTDemo.API.Models;

namespace IoTDemo.API.Migrations
{
    [DbContext(typeof(IoTDemoDbContext))]
    [Migration("20170503082447_DbGeneration")]
    partial class DbGeneration
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

                    b.Property<int>("IoTDataNameId");

                    b.Property<float>("Value");

                    b.HasKey("Id");

                    b.HasIndex("IoTDataNameId");

                    b.ToTable("IoTData");
                });

            modelBuilder.Entity("IoTDemo.API.Models.IoTDataName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IoTKeyId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("IoTKeyId");

                    b.ToTable("IoTDataNames");
                });

            modelBuilder.Entity("IoTDemo.API.Models.IoTKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Enabled");

                    b.Property<Guid>("Key");

                    b.Property<string>("User");

                    b.HasKey("Id");

                    b.ToTable("IoTKeys");
                });

            modelBuilder.Entity("IoTDemo.API.Models.IoTData", b =>
                {
                    b.HasOne("IoTDemo.API.Models.IoTDataName", "IoTDataName")
                        .WithMany()
                        .HasForeignKey("IoTDataNameId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("IoTDemo.API.Models.IoTDataName", b =>
                {
                    b.HasOne("IoTDemo.API.Models.IoTKey", "IoTKey")
                        .WithMany()
                        .HasForeignKey("IoTKeyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
