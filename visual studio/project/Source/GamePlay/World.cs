using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public SquareGrid Grid;
        public Vector2 Offset;
        public Basic2D Darkness;

        public World(params Vector2[] chestsLocation)
        {
            Levels = new Searching[]
            { 
                new Searching("2D\\Backgrounds\\searchFirstLevel", "2D\\Interface\\Searching\\First\\", new string[] { "flashlight" }), 
                new Searching("2D\\Backgrounds\\searchSecondLevel", "2D\\Interface\\Searching\\Second\\", new string[] { "phone", "notebook" }),
                new Searching("2D\\Backgrounds\\searchThirdLevel", "2D\\Interface\\Searching\\Third\\", new string[] { "redKey" }),
                new Searching("2D\\Backgrounds\\searchFourLevel", "2D\\Interface\\Searching\\Four\\", new string[] { "verevka" }),
            };

            Hero = new Hero("2D\\hero", new Vector2(100, 150), new Vector2(64, 88));

            foreach (var level in Levels)
            {
                Chests = chestsLocation.Select(position => new Chest("2D\\Objects\\chest", position, new Vector2(64, 64), level)).ToArray();
            }
            
            Offset = new Vector2(0, 0);
            Grid = new SquareGrid(new Vector2(20, 20), new Vector2(-100, -100), new Vector2(GeneralVariable.WindowWidth + 200, GeneralVariable.WindowHeight + 200));
            Darkness = new Basic2D("2D\\Backgrounds\\darkness", new Vector2(400, 315), new Vector2(800, 700));
        }

        public void Update()
        {
            Hero.Update();
            Hero.ChestsCollision(new Vector2(48, 48), Chests);
            Hero.EnemyCollision(new Vector2(64, 64), Enemies.Select(enemy => enemy.Position));
            
            foreach (var enemy in Enemies)
                enemy.Update(Hero);
            foreach (var chest in Chests)
                chest.Update();

            if (Grid != null) Grid.Update(Offset);
            if (GeneralVariable.Keyboard.State.IsKeyDown(Keys.G)) Grid.ShowGrid = !Grid.ShowGrid;
        }

        public void Draw()
        {
            Grid.DrawGrid(Offset);
            
            foreach (var enemy in Enemies)
                enemy.Draw(Offset);

            foreach (var chest in Chests)
                chest.Draw(Offset);

            Hero.Draw(Offset);

            if (!Levels[0].objectIsFind) Darkness.Draw(Offset);
        }

        public void CreateEnemies(List<Vector2> positions, Vector2 dimension)
        {
            var enemies = new List<Enemy>();
            for (int i = 0; i < positions.Count; i++)
            {
                enemies.Add(new Enemy("2D\\Enemies\\enemy", positions[i], dimension));
            }
            Enemies = enemies.ToArray();
        }
    }
}
