﻿using QuizBot.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QuizBot.Api.Configurations
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.FirstName);
            builder.Property(x => x.LastName);
            builder.Property(x => x.Username);
            builder.Property(x => x.ChatId);
            builder.Property(x => x.IsWinner).HasDefaultValue(false);
            builder.Property(x => x.UserStatus).HasDefaultValue(UserStatus.NewUser);
        }
    }
}
