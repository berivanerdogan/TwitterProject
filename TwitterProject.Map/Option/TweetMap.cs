using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterProject.Core.Map;
using TwitterProject.Model.Option;

namespace TwitterProject.Map.Option
{
   public class TweetMap:CoreMap<Tweet>
    {
        public TweetMap()
        {
            ToTable("dbo.Tweets");
            Property(x => x.Content).HasMaxLength(140).IsOptional();
            Property(x => x.ImagePath).IsOptional();

            HasMany(x => x.Comments).
                WithRequired(x => x.Tweet).
                HasForeignKey(x => x.TweetID).WillCascadeOnDelete(false);

            HasMany(x => x.Likes).
                WithRequired(x => x.Tweet).
                HasForeignKey(x => x.TweetID).WillCascadeOnDelete(false);

            HasRequired(x => x.AppUser).
                WithMany(x => x.Tweets).
                HasForeignKey(x => x.AppUserID).WillCascadeOnDelete(false);

        }
    }
}
