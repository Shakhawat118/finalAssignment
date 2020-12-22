using Final_Assignment.Models;
using Inventory_with_Repository_Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Final_Assignment.Controllers
{
    [RoutePrefix("api/post")]
    public class PostsController : ApiController
    {
        PostRepository postRepo = new PostRepository();
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(postRepo.GetAll());
        }
        [Route("{id}", Name = "GetPostById")]

        public IHttpActionResult Get(int id)
        {
            var post = postRepo.Get(id);
            if(post == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(postRepo.Get(id));
        }
        [Route("")]
        public IHttpActionResult Post(Post post)
        {
            postRepo.Insert(post);
            string uri = Url.Link("GetPostById", new { id = post.PostID });
            return Created(uri, post);
        }

        [Route("{id}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] Post post)
        { 
            post.PostID = id;
            postRepo.Update(post);
            return Ok(post);
        } 

        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            postRepo.DeletePost(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        DescribeRepository CmntRepo = new DescribeRepository();
        [Route("{id}/Comments")]
        public IHttpActionResult GetComment(int id)
        {
            List<Comment> comments = CmntRepo.GetAll().Where<Comment>(x => x.PostID == id).ToList();
            return Ok(comments);
        }

        [Route("{id}/comments/{cid}", Name = "GetCommentById")]
        public IHttpActionResult GetComment(int id, int cid)
        {
            Comment com = CmntRepo.GetAll().Where<Comment>(x => x.CommentID == cid && x.PostID == id).FirstOrDefault();
            if (com == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return Ok(com);
            }
        }

        [Route("{id}/comments")]
        public IHttpActionResult PostComment(int id, Comment com)
        {
            com.PostID = id;
            CmntRepo.Insert(com);
            string uri = Url.Link("GetCommentById", new { id = com.PostID, cid = com.CommentID });
            return Created(uri, com);
        }

        [Route("{id}/comments/{cid}")]
        public IHttpActionResult Put([FromUri] int id, [FromUri] int cid, [FromBody] Comment com)
        {
            com.CommentID = cid;
            com.PostID = id;
            CmntRepo.Update(com);
            return Ok(com);
        }

        [Route("{id}/comments/{cid}")]
        public IHttpActionResult DeleteComment(int id, int cid)
        {
            Comment comment = CmntRepo.GetAll().Where<Comment>(x => x.CommentID == cid && x.PostID == id).FirstOrDefault();
            if (comment == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            CmntRepo.Delete(cid);
            return StatusCode(HttpStatusCode.NoContent);
        }
     
    }
}
