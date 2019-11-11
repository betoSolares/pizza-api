using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace pizza_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTController : ControllerBase
    {
        // POST: /api/jwt/{word}
        // Get any type of json and generate a token using JWT
        [HttpPost("{word}")]
        public IActionResult Encrypt(string word, dynamic data)
        {
            if (word.Length > 15)
            {
                if (data is null)
                    return BadRequest(new { error = "You must have to specifie some data." });
                Dictionary<string, object> payload = JsonConvert.DeserializeObject<Dictionary<string, object>>(Convert.ToString(data));
                JwtEncoder encoder = new JwtEncoder(new HMACSHA256Algorithm(),
                                                    new JsonNetSerializer(),
                                                    new JwtBase64UrlEncoder());
                string token = encoder.Encode(payload, word);
                return Ok(new { token });
            }
            return BadRequest(new { error = "The word to use for key has to be more that 15 characters" });
        }
    }
}