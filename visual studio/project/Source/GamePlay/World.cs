﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;

namespace superagent
{
    // Класс, который обновляют всю игровую составляющую. У нас есть сундуки, враги, игроk. Bнутри этого класса также есть эти злые методы
    //Update и Draw, но в них мы обновляем все сундуки, всех врагов, игрока. По итогу в Main все эти бесчисленные обновления будут стоить нам одной строчки - world.Update();

    public class World
    {
        public Hero Hero;
        public Chest[] Chests;
        public Enemy[] Enemies;
        public Searching[] Levels;

        public World(params Vector2[] chestsLocation)
        {
            Levels = new Searching[]
            { 
                new Searching("2D\\Backgrounds\\searchFirstLevel", "2D\\Interface\\Searching\\First\\", new string[] { "flashlight", "notebook" }), 
                new Searching("2D\\Backgrounds\\searchSecondLevel", "2D\\Interface\\Searching\\Second\\", new string[] { "phone" }),
                new Searching("2D\\Backgrounds\\searchThirdLevel", "2D\\Interface\\Searching\\Third\\", new string[] { "redKey" }),
                new Searching("2D\\Backgrounds\\searchFourLevel", "2D\\Interface\\Searching\\Four\\", new string[] { "verevka" }),
            };
            Hero = new Hero("2D\\hero", new Vector2(100, 150), new Vector2(64, 88));
            foreach (var level in Levels)
            {
                Chests = chestsLocation.Select(position => new Chest("2D\\Objects\\chest", position, new Vector2(64, 64), level)).ToArray();
            }
        }

        public void Update()
        {
            Hero.Update();
            Hero.ChestsCollision(new Vector2(48, 48), Chests);
            Hero.EnemyCollision(new Vector2(64, 64), Enemies.Select(enemy => enemy.Position));
            
            foreach (var enemy in Enemies)
                enemy.Update();
            foreach (var chest in Chests)
                chest.Update();
        }

        public void Draw()
        {
            Hero.Draw();
            foreach (var enemy in Enemies)
                enemy.Draw();
            foreach (var chest in Chests)
                chest.Draw();
        }

        public void CreateEnemies(List<Vector2> positions, Vector2 dimension, List<Func<Vector2, float, Vector2>> movements)
        {
            var enemies = new List<Enemy>();
            for (int i = 0; i < positions.Count; i++)
            {
                enemies.Add(new Enemy("2D\\Enemies\\enemy", positions[i], dimension, movements[i]));
            }
            Enemies = enemies.ToArray();
        }
    }
}
