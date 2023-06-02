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
    public class Hero : Basic2D // Здесь мы указываем, что класс наследует нашу основную оболочку Basic2D
    {
        // При этом мы здесь не пишем все эти texture, position, dimension. Так как мы сказали, что класс наследует 2д, то они у него 
        // существуют, к ним можно обращаться, но указывать дважды их смысла нет.

        public float Speed; 
        public int HP;
        public double Score;
        public bool EnemyCollisionFlag;
        public bool ChestCollisionFlag;
        private bool damageFlag;

        public Hero(string path, Vector2 pos, Vector2 dims) : base(path, pos, dims) // здесь и происходит присвоение. Мы передаем в конструктор класса
                                                                                    // какие то значения пути к картинке, позиции и размера, и он сам присвает их полям texture,
                                                                                    // position и dimension. Мы это не пишем, но компилятор все делает.
                                                                                    // base - ключевое слово наследуемого класса. То есть, написав base(path, pos, dims)
                                                                                    // мы грубо говоря написали Basic2D(path, pos, dims) и т.д. 
        {
            Speed = 3f; HP = 100; Score = 0;
        }


        public override void Update()
        {
            if (GeneralVariable.Keyboard.State.IsKeyDown(Keys.A))
                Position.X -= Speed;
            if (GeneralVariable.Keyboard.State.IsKeyDown(Keys.D))
                Position.X += Speed;
            if (GeneralVariable.Keyboard.State.IsKeyDown(Keys.W))
                Position.Y -= Speed;
            if (GeneralVariable.Keyboard.State.IsKeyDown(Keys.S))
                Position.Y += Speed;

            if (Position.X < 0) Position.X = 0;
            if (Position.Y < 0) Position.Y = 0;
            if (Position.X > GeneralVariable.WindowWidth - SizeTexture.X / 2)
                Position.X = GeneralVariable.WindowWidth - SizeTexture.X / 2;
            if (Position.Y > GeneralVariable.WindowHeight - SizeTexture.Y / 2)
                Position.Y = GeneralVariable.WindowHeight - SizeTexture.Y / 2;

            damageFlag = !EnemyCollisionFlag ? true : false;
            ScoreUpdate();
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
                    if (damageFlag) HP -= 5;
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

        public override void Draw()
        {
            if (EnemyCollisionFlag) base.Draw(Color.Red);
            else base.Draw();
        }
    }
}
