using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace multiPlayer_shooter
{
    enum MessageTypes
    {
        getid = 0,
        move,
        id
    }
    class Message
    {
        public MessageTypes type;
        public string body;
        public Message(MessageTypes m, params string[] msg)
        {
            type = m;
            body = "";
            foreach (string s in msg)
            {
                body += " " + s;
            }
        }
    }
    class Game : GameWindow
    {
        const int maxPlayers = 2;
        static int id = 0;
        static Entity en;
        static TcpClient connection;

        #region OpenTK functions

        public Game(int width, int height) : base(width, height)
        {
            en = new Entity(new Vector2(400.0f, 300.0f), 5, new Color4(255, 0, 0, 255));
            Keyboard.KeyDown += Keyboard_KeyDown;
            connection = new TcpClient("localhost", 8091);

            connection.GetStream().Write(Encoding.ASCII.GetBytes("getid"), 0, Encoding.ASCII.GetBytes("getid").Length);

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(0, 0, 255, 255);


            en.draw(this);
            this.SwapBuffers();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                en.UP(10);
                connection.GetStream().Write(Encoding.ASCII.GetBytes("Test"), 0, Encoding.ASCII.GetBytes("Test").Length);

            }
            if (e.Key == Key.S)
            {
                en.DOWN(10);
            }
            if (e.Key == Key.A)
            {
                en.LEFT(10);
            }
            if (e.Key == Key.D)
            {
                en.RIGHT(10);
            }
        }

        #endregion
    }
}
