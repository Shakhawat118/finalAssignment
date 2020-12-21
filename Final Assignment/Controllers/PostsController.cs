using Final_Assignment.Models;
using Final_Assignment.Repositories;
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
            if (post == null)
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
            postRepo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
        CommentRepository commentRepo = new CommentRepository();
        [Route("{id}/comment")]
        public IHttpActionResult Getcomment(int id)
        {
            List<Comment> comments = commentRepo.GetAll().Where<Comment>(x => x.PostID == id).ToList();
            return Ok(comments);
        }
    }
}
