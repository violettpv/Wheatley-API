using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WheatleyAPI.Data;
using WheatleyAPI.Repositories;
using request = WheatleyAPI.Requests.Requests;
using WheatleyAPI.Requests;
using WheatleyAPI.DataTransferObjects;

namespace WheatleyAPI.Controllers
{
    [ApiController] // атрибут вказує що даний клас є контролером
    [Route("api")] // шлях (URI) контролера 
    public class DictionaryController: ControllerBase 
    {
        private UserRepository repository = new UserRepository(); // об'єкт класу репозиторію для маніпуляції над даними

        [HttpPost("adduser")] // атрибут вказує на який метод-http реагує метод + URI методу  
        public IActionResult AddUser([FromBody] User new_user) // атрибут [FromBody] ці дані беруться із тіла запиту
        {
            if (new_user == null) return BadRequest();
            if (new_user.user_id == null || repository.UserExists(new_user.user_id) == true) return BadRequest();
            repository.AddUser(new_user);
            return Ok();
        }

        [HttpDelete("deleteuser/{user_id}")]
        public IActionResult DeleteUser(string user_id)
        {
            if (user_id == null || repository.UserExists(user_id) == false) return BadRequest();
            repository.DeleteUser(user_id);
            return Ok();
        }

        [HttpPost("addword/{user_id}/{word}")]
        public IActionResult AddWord(string user_id, string word)
        {
            if (user_id == null || repository.UserExists(user_id) == false) return BadRequest();
            if (word == null || repository.WordExists(user_id, word) == true) return BadRequest();
            repository.AddWord(user_id, word);
            return Ok();
        }

        [HttpDelete("deleteword/{user_id}/{word}")]
        public IActionResult DeleteWord(string user_id, string word)
        {
            if (user_id == null || repository.UserExists(user_id) == false) return BadRequest();
            if(word == null || repository.WordExists(user_id, word) == false) return BadRequest();
            repository.DeleteWord(user_id, word);
            return Ok();
        }

        // -----
        [HttpGet("getdef/{word}")]
        public IActionResult GetDefinition(string word)
        {
            if (word == null || word == "") return BadRequest();
            List<ThesaurusDesignation> th_des; // об'єкти на який серіалізовані дані отримані із публ.АРІ
            UrbanDesignation ur_des; 
            DesignationDto des; // об'єкт даних, які повертаються у тілі відповіді
            try 
            { // якщо у тезаурусі немає слова, то відбувається помилка при десеріалізації => catch (запит на urbanD)
                th_des = request.ThesaurusDesignation(word);
                des = new DesignationDto // заповнення моделі даних, які повертаються
                {
                    dictionary = "th",
                    word = th_des[0].meta.id,
                    designation = th_des[0].shortdef[0],
                    fl = th_des[0].fl,
                    synonyms = th_des[0].meta.syns,
                    antonyms = th_des[0].meta.ants,
                    offensive = th_des[0].meta.offensive
                };
            }
            catch
            {
                ur_des = request.UrbanDesignation(word);
                des = new DesignationDto
                {
                    dictionary = "ur",
                    word = ur_des.list[0].word,
                    designation = ur_des.list[0].definition,
                    example = ur_des.list[0].example
                };
            }
            return Ok(des);
        }

        [HttpGet("savedwords/{user_id}")]
        public IActionResult GetSavedWords(string user_id)
        {
            if (user_id == null || repository.UserExists(user_id) == false) return BadRequest();
            List<string> saved = repository.GetUserWords(user_id);
            SavedWordsDto words = new SavedWordsDto
            {
                saved_words = saved
            };
            return Ok(words);
        }
    }
}
