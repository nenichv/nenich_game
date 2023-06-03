using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace superagent
{
    public class World
    {
        public Hero Hero;
        public Chest[] Chests;
        public Searching[] Levels;
        public Vector2 Offset;
        public Basic2D Darkness;
        public List<Enemy> Enemies = new();
        public List<Projectiles> Projectiles = new();

        public World(params Vector2[] chestsLocation)
        {
            Levels = new Searching[]
            { 
                new Searching("2D\\Backgrounds\\searchFirstLevel", "2D\\Interface\\Searching\\First\\", new string[] { "flashlight" }), 
                new Searching("2D\\Backgrounds\\searchSecondLevel", "2D\\Interface\\Searching\\Second\\", new string[] { "phone", "notebook" }),
                new Searching("2D\\Backgrounds\\searchThirdLevel", "2D\\Interface\\Searching\\Third\\", new string[] { "redKey" }),
                new Searching("2D\\Backgrounds\\searchFourLevel", "2D\\Interface\\Searching\\Four\\", new string[] { "verevka" }),
            };

            foreach (var level in Levels)
            {
                Chests = chestsLocation.Select(position => new Chest("2D\\Objects\\chest", position, new Vector2(64, 64), level)).ToArray();
            }
            
            Offset = new Vector2(0, 0);
            Darkness = new Basic2D("2D\\Backgrounds\\darkness", new Vector2(400, 315), new Vector2(800, 700));
            Hero = new Hero("2D\\Objects\\heroNew", new Vector2(100, 150), new Vector2(64, 88));
            GameControl.PassProjectile = AddProjectile;
        }

        public void Update()
        {
            Hero.Update();
            Hero.ChestsCollision(new Vector2(48, 48), Chests);
            Hero.EnemyCollision(new Vector2(64, 64), Enemies.Select(enemy => enemy.Position));
            
            foreach (var chest in Chests)
                chest.Update();

            for (var drone = 0; drone < Projectiles.Count; drone++)
            {
                Projectiles[drone].Update(Offset, Enemies);
                if (Projectiles[drone].droneIsDone)
                {
                    Projectiles.RemoveAt(drone);
                    drone--;
                }
            }

            for (var enemy = 0; enemy < Enemies.Count; enemy++)
            {
                Enemies[enemy].Update(Hero);
                if (Enemies[enemy].unitIsDead)
                {
                    Enemies.RemoveAt(enemy);
                    enemy--;
                }
            }
        }

        public void Draw()
        {
            foreach (var enemy in Enemies)
                enemy.Draw(Offset);

            foreach (var chest in Chests)
                chest.Draw(Offset);

            Hero.Draw(Offset);

            for (var drone = 0; drone < Projectiles.Count; drone++)
                Projectiles[drone].Draw(Offset);

            if (!Levels[0].objectIsFind) Darkness.Draw(Offset);
        }

        public void CreateEnemies(List<Vector2> positionsEnemy, Vector2 dimension)
        {
            for (int position = 0; position < positionsEnemy.Count; position++)
                Enemies.Add(new Enemy("2D\\Enemies\\enemy", positionsEnemy[position], dimension));
        }

        public virtual void AddProjectile(object element)
        {
            Projectiles.Add((Projectiles)element);
        }
    }
}
