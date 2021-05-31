using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WheatleyAPI.Data;

namespace WheatleyAPI.Repositories
{
    public class UserRepository // методи взаємодії з даними(UserData.json)
    { 
        public User GetUserData(string user_id) // отримати всі дані певного юзеру
        {
            UserData data = ReadData();
            User user = data.users.Where(user => user.user_id == user_id).FirstOrDefault(); // пошук юзера із заданим id
            return user;
        }

        public List<string> GetUserWords(string user_id) // отримати список слів юзеру
        {
            return ReadData().users.Where(user => user.user_id == user_id).FirstOrDefault().saved_words;
        }

        public void AddUser(User user) // додати юзера у список users(List<User>)
        {
            UserData data = ReadData();
            if (data.users == null) data.users = new List<User>(); // якщо не існує списку юзерів => new
            data.users.Add(user);
            SaveData(data);
        }

        public void DeleteUser(string user_id) // видаляємо юзера
        {
            UserData data = ReadData();
            User delete_user = data.users.Where(user => user.user_id == user_id).FirstOrDefault();
            data.users.Remove(delete_user);
            SaveData(data);
        }

        public void AddWord(string user_id, string word) // додати слово до списку saved_words(List<string>) певного юзеру(у users)
        {
            UserData data = ReadData();
            User find_user_id = data.users.Where(user => user.user_id == user_id).FirstOrDefault();
            if (find_user_id.saved_words == null) find_user_id.saved_words = new List<string>(); //якщо у юзера не існує списку слів =>new
            find_user_id.saved_words.Add(word);
            SaveData(data);
        }

        public void DeleteWord(string user_id, string word) // видалити слово із списку saved_words
        {
            UserData data = ReadData();
            User find_user_id = data.users.Where(user => user.user_id == user_id).FirstOrDefault();
            find_user_id.saved_words.Remove(word);
            SaveData(data);
        }

        public void ChangeSurname(string user_id, string new_surname) // змінити фамілію
        {
            UserData data = ReadData();
            data.users.Where(user => user.user_id == user_id).FirstOrDefault().user_surname = new_surname;
            SaveData(data);
        }

        public void ChangeName(string user_id, string new_name) // змінити ім'я
        {
            UserData data = ReadData();
            data.users.Where(user => user.user_id == user_id).FirstOrDefault().user_name = new_name;
            SaveData(data);
        } 

        public bool UserExists(string user_id) // перевірка чи існує юзер в даних
        {
            UserData data = ReadData();
            if (data.users == null) return false; // якщо списку юзерів не існує, то і юзеру не існує
            if (data.users.Where(user => user.user_id == user_id).ToList().Count == 0) return false; //якщо довжина списку=0, юзера немає
            return true;
        }

        public bool WordExists(string user_id, string word) // перевірка чи існує слово у юзера
        {
            UserData data = ReadData();
            if (data.users == null) return false; // якщо списку юзерів не існує -> то не існує юзеру -> немає списку, -> слова
            User user = data.users.Where(user => user.user_id == user_id).FirstOrDefault();
            if (user.saved_words == null) return false; // якщо немає списку слів, -> слова
            return user.saved_words.Contains(word); // return true - якщо є слово
        }

        public UserData ReadData() // метод зчитування даних
        {
            string json;
            using (StreamReader sr = new StreamReader("Data\\UserData.json"))
            {
                json = sr.ReadToEnd();
            }
            var data = JsonSerializer.Deserialize<UserData>(json);
            return data;
        }

        public void SaveData(UserData data) // метод перезапису даних
        {
            string json = JsonSerializer.Serialize<UserData>(data);

            // файл перезаписується
            using (StreamWriter sw = new StreamWriter("Data\\UserData.json", false, System.Text.Encoding.Default))
            { 
                sw.WriteLine(json);
            }
        }

    }
}
