using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace superagent
{
    public class Hero : Unit
    {
        public int HP;
        public double Score;
        public bool EnemyCollisionFlag;
        public bool ChestCollisionFlag;
        private bool damageFlag;

        public Hero(string path, Vector2 position, Vector2 size) : base(path, position, size)
        {
            speed = 3f; HP = 100; Score = 0;
        }

        public override void Update()
        {
            if (GeneralVariable.Keyboard.State.IsKeyDown(Keys.A) && Position.X > 100)
                Position.X -= speed;
            if (GeneralVariable.Keyboard.State.IsKeyDown(Keys.D) && Position.X < 700)
                Position.X += speed;
            if (GeneralVariable.Keyboard.State.IsKeyDown(Keys.W) && Position.Y > 150)
                Position.Y -= speed;
            if (GeneralVariable.Keyboard.State.IsKeyDown(Keys.S) && Position.Y < 500)
                Position.Y += speed;

            if (Position.X < 0) Position.X = 100;
            if (Position.Y < 0) Position.Y = 100;
            if (Position.X > GeneralVariable.WindowWidth - SizeTexture.X)
                Position.X = GeneralVariable.WindowWidth - SizeTexture.X;
            if (Position.Y > GeneralVariable.WindowHeight - SizeTexture.Y)
                Position.Y = GeneralVariable.WindowHeight - SizeTexture.Y;

            damageFlag = EnemyCollisionFlag ? false : true;
            ScoreUpdate();

            if (GeneralVariable.Mouse.LeftClick()) 
                GameControl.PassProjectile(
                    new Drone(
                        new Vector2(Position.X, Position.Y), this, 
                        new Vector2(GeneralVariable.Mouse.newMousePosition.X, GeneralVariable.Mouse.newMousePosition.Y)));

            base.Update();
        }

        public bool CollisionUpdate(Vector2 dimension, Vector2 position)
        {
            Rectangle heroRect = new Rectangle((int)Position.X, (int)Position.Y, (int)base.SizeTexture.X, (int)base.SizeTexture.Y);
            Rectangle objectRect = new Rectangle((int)position.X, (int)position.Y, (int)dimension.X, (int)dimension.Y);
            if (heroRect.Intersects(objectRect)) return true;
            return false;
        }

        public void EnemyCollision(Vector2 dimension, IEnumerable<Vector2> positions)
        {
            foreach (var enemy in positions)
            {
                EnemyCollisionFlag = CollisionUpdate(dimension, enemy);
                if (EnemyCollisionFlag)
                {
                    if (damageFlag) HP -= 20;
                    break;
                }
            }
        }

        public void ChestsCollision(Vector2 dimension, Chest[] chests)
        {
            for (int i = 0; i < chests.Length; i++)
            {
                ChestCollisionFlag = CollisionUpdate(dimension, chests[i].Position);
                if (ChestCollisionFlag)
                {
                    GameStateControl.ChestIndex = i; break;
                }
            }
        }

        public void ScoreUpdate()
        {
            if (GeneralVariable.Keyboard.State.IsKeyDown(Keys.E) && ChestCollisionFlag) 
                Score += 10;
        }

        public override void Draw(Vector2 offset)
        {
            if (EnemyCollisionFlag) base.Draw(offset, Color.Red);
            else base.Draw(offset);
        }
    }
}
