using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using netCoreWorkshop.Entities;

namespace netCoreWorkshop.API
{
    [Route("/api/articles")]
    public class ArticlesApiController : Controller
    {
        [HttpGet]
        public IActionResult Get() => Ok(Article.DataSource);
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var article = Article.DataSource.Where(a => a.Id == id).FirstOrDefault();

            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            article.Id = (Article.DataSource.Count > 0)?Article.DataSource.Last().Id + 1 : 1;
            Article.DataSource.Add(new Article { Title = article.Title,  Id = article.Id});

            return CreatedAtAction(nameof(Create), new { id = article.Title }, article);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var article = Article.DataSource.Where(m => m.Id == id).FirstOrDefault();

            if (article == null)
            {
                return NotFound();
            }

            Article.DataSource.Remove(article);

            return Ok("Ok");
        }

    }
}