using Final_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory_with_Repository_Pattern.Repositories
{
    public class PostRepository:Repository<Post>
    {
        DescribeRepository comRepo = new DescribeRepository();
        public void DeletePost(int id)
        {
            List<Comment> comments = comRepo.GetAll().Where<Comment>(x => x.PostID == id).ToList();

            foreach (var item in comments)
            {
                comRepo.Delete(item.CommentID);
            }
            this.Delete(id);
        }
    }
}