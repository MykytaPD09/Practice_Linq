﻿using Newtonsoft.Json;

namespace Practice_Linq
{
    public class Program
    {
        static void Main(string[] args)
        {

            string path = @"../../../data/results_2010.json";

            List<FootballGame> games = ReadFromFileJson(path);

            int test_count = games.Count();
            Console.WriteLine($"Test value = {test_count}.");    // 13049

            Query1(games);
            Query2(games);
            Query3(games);
            Query4(games);
            Query5(games);
            Query6(games);
            Query7(games);
            Query8(games);
            Query9(games);
            Query10(games);

        }


        // Десеріалізація json-файлу у колекцію List<FootballGame>
        static List<FootballGame> ReadFromFileJson(string path)
        {

            var reader = new StreamReader(path);
            string jsondata = reader.ReadToEnd();

            List<FootballGame> games = JsonConvert.DeserializeObject<List<FootballGame>>(jsondata);


            return games;

        }

        static void PrintMatch(FootballGame game)
        {
            Console.WriteLine($"{game.Date.ToShortDateString()} {game.Home_team} - {game.Away_team}" +
                $", Score: {game.Home_score} - {game.Away_score}" +
                $", Country: {game.Country}");
        }


        // Запит 1
        static void Query1(List<FootballGame> games)
        {
            //Query 1: Вивести всі матчі, які відбулися в Україні у 2012 році.

            var selectedGames = games.Where(game => game.Country == "Ukraine" && game.Date.Year == 2012); // Корегуємо запит !!!


            // Перевірка
            Console.WriteLine("\n======================== QUERY 1 ========================");

            foreach (FootballGame game in selectedGames) { PrintMatch(game); }


        }

        // Запит 2
        static void Query2(List<FootballGame> games)
        {
            //Query 2: Вивести Friendly матчі збірної Італії, які вона провела з 2020 року.  

            var selectedGames = games.Where(game => game.Tournament == "Friendly" && (game.Home_team == "Italy" || game.Away_team == "Italy") && game.Date.Year >= 2020); // Корегуємо запит !!!


            // Перевірка
            Console.WriteLine("\n======================== QUERY 2 ========================");

            foreach (FootballGame game in selectedGames) { PrintMatch(game); }


        }

        // Запит 3
        static void Query3(List<FootballGame> games)
        {
            //Query 3: Вивести всі домашні матчі збірної Франції за 2021 рік, де вона зіграла у нічию.

            var selectedGames = games.Where(game => game.Country == "France" && game.Home_team == "France" && game.Date.Year == 2021 && game.Home_score == game.Away_score);   // Корегуємо запит !!!

            // Перевірка
            Console.WriteLine("\n======================== QUERY 3 ========================");

            foreach (FootballGame game in selectedGames) { PrintMatch(game); }


        }

        // Запит 4
        static void Query4(List<FootballGame> games)
        {
            //Query 4: Вивести всі матчі збірної Германії з 2018 року по 2020 рік (включно), в яких вона на виїзді програла.

            var selectedGames = games.Where(game => game.Away_team == "Germany" && game.Date.Year >= 2018 && game.Date.Year <= 2020 && game.Home_score > game.Away_score);   // Корегуємо запит !!!


            // Перевірка
            Console.WriteLine("\n======================== QUERY 4 ========================");

            foreach (FootballGame game in selectedGames) { PrintMatch(game); }


        }

        // Запит 5
        static void Query5(List<FootballGame> games)
        {
            //Query 5: Вивести всі кваліфікаційні матчі (UEFA Euro qualification), які відбулися у Києві чи у Харкові, а також за умови перемоги української збірної.


            var selectedGames = games.Where(game => game.Tournament == "UEFA Euro qualification" &&
                               (game.City == "Kyiv" || game.City == "Kharkiv") &&
                               (game.Home_team == "Ukraine" && game.Home_score > game.Away_score || game.Away_team == "Ukraine" && game.Away_score > game.Home_score));   // Корегуємо запит !!!


            // Перевірка
            Console.WriteLine("\n======================== QUERY 5 ========================");

            foreach (FootballGame game in selectedGames) { PrintMatch(game); }


        }

        // Запит 6
        static void Query6(List<FootballGame> games)
        {
            //Query 6: Вивести всі матчі останнього чемпіоната світу з футболу (FIFA World Cup), починаючи з чвертьфіналів (тобто останні 8 матчів).
            //Матчі мають відображатися від фіналу до чвертьфіналів (тобто у зворотній послідовності).

            var selectedGames = games.Where(game => game.Tournament == "FIFA World Cup").OrderByDescending(game => game.Date).Take(8);   // Корегуємо запит !!!


            // Перевірка
            Console.WriteLine("\n======================== QUERY 6 ========================");

            foreach (FootballGame game in selectedGames) { PrintMatch(game); }


        }

        // Запит 7
        static void Query7(List<FootballGame> games)
        {
            //Query 7: Вивести перший матч у 2023 році, в якому збірна України виграла.

            FootballGame game = games.Where(game => game.Date.Year == 2023 && ((game.Home_team == "Ukraine" && game.Home_score > game.Away_score) || (game.Away_team == "Ukraine" && game.Away_score > game.Home_score))).OrderBy(game => game.Date).First();   // Корегуємо запит !!!


            // Перевірка
            Console.WriteLine("\n======================== QUERY 7 ========================");

            PrintMatch(game);


        }

        // Запит 8
        static void Query8(List<FootballGame> games)
        {
            //Query 8: Перетворити всі матчі Євро-2012 (UEFA Euro), які відбулися в Україні, на матчі з наступними властивостями:
            // MatchYear - рік матчу, Team1 - назва приймаючої команди, Team2 - назва гостьової команди, Goals - сума всіх голів за матч

            var selectedGames = games.Where(game => game.Tournament == "UEFA Euro" && game.Country == "Ukraine")
                .Select(game => new
                {
                    MatchYear = game.Date.Year,
                    Team1 = game.Home_team,
                    Team2 = game.Away_team,
                    Goals = game.Home_score + game.Away_score
                });   // Корегуємо запит !!!

            // Перевірка
            Console.WriteLine("\n======================== QUERY 8 ========================");

            foreach (var game in selectedGames)
            {
                Console.WriteLine($"{game.MatchYear} {game.Team1} - {game.Team2}, Goals: {game.Goals}");
            }


        }


        // Запит 9
        static void Query9(List<FootballGame> games)
        {
            //Query 9: Перетворити всі матчі UEFA Nations League у 2023 році на матчі з наступними властивостями:
            // MatchYear - рік матчу, Game - назви обох команд через дефіс (першою - Home_team), Result - результат для першої команди (Win, Loss, Draw)

            var selectedGames = games.Where(game => game.Tournament == "UEFA Nations League" && game.Date.Year == 2023)
                .Select(game => new
                {
                    MatchYear = game.Date.Year,
                    Game = $"{game.Home_team}-{game.Away_team}",
                    Result = GetResultForTeam1(game)
                });   // Корегуємо запит !!!

            // Перевірка
            Console.WriteLine("\n======================== QUERY 9 ========================");

            foreach (var game in selectedGames)
            {
                Console.WriteLine($"{game.MatchYear} {game.Game}, Result for team1: {game.Result}");
            }


        }

        static string GetResultForTeam1(FootballGame game)                                             
        {
            return game.Home_score > game.Away_score ? "Win" : (game.Home_score < game.Away_score ? "Loss" : "Draw");
        }

        // Запит 10
        static void Query10(List<FootballGame> games)
        {
            //Query 10: Вивести з 5-го по 10-тий (включно) матчі Gold Cup, які відбулися у липні 2023 р.

            var selectedGames = games.Where(game => game.Tournament == "Gold Cup" && game.Date.Year == 2023 && game.Date.Month == 7).Skip(4).Take(6);    // Корегуємо запит !!!

            // Перевірка
            Console.WriteLine("\n======================== QUERY 10 ========================");

            foreach (FootballGame game in selectedGames) { PrintMatch(game); }


        }

    }
}