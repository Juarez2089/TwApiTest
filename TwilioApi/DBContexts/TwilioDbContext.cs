using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TwilioApi.Model;

namespace TwilioApi.DBContexts;

public partial class TwilioDbContext : DbContext
{
    public TwilioDbContext()
    {
    }

    public TwilioDbContext(DbContextOptions<TwilioDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TwlCredential> TwlCredentials { get; set; }

    public virtual DbSet<TwlMessage> TwlMessages { get; set; }

    public virtual DbSet<TwlMessagesSent> TwlMessagesSents { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TwlCredential>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("twl_credentials");

            entity.Property(e => e.CreAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cre_account");
            entity.Property(e => e.CreDt)
                .HasColumnType("datetime")
                .HasColumnName("cre_DT");
            entity.Property(e => e.CreId)
                .ValueGeneratedOnAdd()
                .HasColumnName("cre_id");
            entity.Property(e => e.CreToken)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cre_token");
            entity.Property(e => e.CrePhNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cre_ph_number");
        });

        modelBuilder.Entity<TwlMessage>(entity =>
        {
            entity.HasKey(e => e.MsgId);

            entity.ToTable("twl_messages");

            entity.Property(e => e.MsgId).HasColumnName("msg_ID");
            entity.Property(e => e.MsgCreationDate)
                .HasColumnType("datetime")
                .HasColumnName("msg_creation_date");
            entity.Property(e => e.MsgMessage)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("msg_message");
            entity.Property(e => e.MsgPhoneTo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("msg_phone_to");
        });

        modelBuilder.Entity<TwlMessagesSent>(entity =>
        {
            entity.HasKey(e => e.SmsgId);

            entity.ToTable("twl_messages_sent");

            entity.Property(e => e.SmsgId).HasColumnName("smsg_id");
            entity.Property(e => e.MsgId).HasColumnName("msg_id");
            entity.Property(e => e.SmsgTwilioCode).HasColumnName("smsg_twilio_code");
            entity.Property(e => e.SmsgTwilioMsgStatus)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("smsg_twilio_msg_status");

            entity.HasOne(d => d.Msg).WithMany(p => p.TwlMessagesSents)
                .HasForeignKey(d => d.MsgId)
                .HasConstraintName("FK_twl_messages_sent_twl_messages");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
