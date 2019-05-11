using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterProject.Core.Map;
using TwitterProject.Model.Option;

namespace TwitterProject.Map.Option
{
   public  class AppUserMap:CoreMap<AppUser>
    {
        public AppUserMap()
        {
            ToTable("dbo.Users");
            Property(x => x.FirstName).HasMaxLength(50).IsOptional();
            Property(x => x.LastName).HasMaxLength(50).IsOptional();
            Property(x => x.Email).HasMaxLength(100).IsOptional();
            Property(x => x.UserName).HasMaxLength(50).IsRequired();
            Property(x => x.Password).HasMaxLength(20).IsRequired();
            Property(x => x.Bio).HasMaxLength(200).IsOptional();
            Property(x => x.UserImage).IsOptional();
            Property(x => x.XSmallUserImage).IsOptional();
            Property(x => x.CruptedUserImage).IsOptional();
            Property(x => x.Follow).IsOptional();
            Property(x => x.Follower).IsOptional();
            Property(x => x.PhoneNumber).HasMaxLength(25).IsOptional();

            HasMany(x => x.Comments).
               WithRequired(x => x.AppUser).
               HasForeignKey(x => x.AppUserID).WillCascadeOnDelete(false);

            HasMany(x => x.Likes).
                WithRequired(x => x.AppUser).
                HasForeignKey(x => x.AppUserID).WillCascadeOnDelete(false);

            HasMany(x => x.Tweets).
                WithRequired(x => x.AppUser).
                HasForeignKey(x => x.AppUserID).WillCascadeOnDelete(false);

        }
    }
}
