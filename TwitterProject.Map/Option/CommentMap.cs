using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterProject.Core.Map;
using TwitterProject.Model.Option;

namespace TwitterProject.Map.Option
{
    public class CommentMap:CoreMap<Comment>
    {
        public CommentMap()
        {
            ToTable("dbo.Comments");
            Property(x => x.Content).IsOptional();

            HasRequired(x => x.AppUser).
                WithMany(x => x.Comments).
                HasForeignKey(x => x.AppUserID).WillCascadeOnDelete(false);

            HasRequired(x => x.Tweet).
                WithMany(x => x.Comments).
                HasForeignKey(x => x.TweetID).WillCascadeOnDelete(false);
        }
    }
}
