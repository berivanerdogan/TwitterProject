using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterProject.Core.Map;
using TwitterProject.Model.Option;

namespace TwitterProject.Map.Option
{
 public class LikeMap:CoreMap<Like>
    {
        public LikeMap()
        {
            ToTable("dbo.Likes");
            Property(x => x.LikeNumber).IsOptional();


            HasRequired(x => x.AppUser).
                WithMany(x => x.Likes).
                HasForeignKey(x => x.AppUserID).WillCascadeOnDelete(false);

            HasRequired(x => x.Tweet).
                WithMany(x => x.Likes).
                HasForeignKey(x => x.TweetID).WillCascadeOnDelete(false);
            
        }
    }
}
