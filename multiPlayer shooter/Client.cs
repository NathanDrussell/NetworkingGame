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
    class Game : GameWindow
    {
        const int maxPlayers = 2;
        static int id = -1;
        static Entity en;
        static TcpClient connection;

        #region OpenTK functions

        public Game(int width, int height) : base(width, height)
        {
            en = new Entity(new Vector2(400.0f, 300.0f), 5, new Color4(255, 0, 0, 255));
            Keyboard.KeyDown += Keyboard_KeyDown;
            connection = new TcpClient("localhost", 8091);

            send(new Message(id, Constants.MessageTypes.getid));

            Thread t = new Thread(() => RecData());
            t.Start();
        }
        #region dont want to see
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
        #endregion

        private void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                en.UP(10);
                send(new Message(id, Constants.MessageTypes.move, en.center.X.ToString(), en.center.Y.ToString()));
            }
            if (e.Key == Key.S)
            {
                en.DOWN(10);
                send(new Message(id, Constants.MessageTypes.move, en.center.X.ToString(), en.center.Y.ToString()));
            }
            if (e.Key == Key.A)
            {
                en.LEFT(10);
                send(new Message(id, Constants.MessageTypes.move, en.center.X.ToString(), en.center.Y.ToString()));
            }
            if (e.Key == Key.D)
            {
                en.RIGHT(10);
                send(new Message(id, Constants.MessageTypes.move, en.center.X.ToString(), en.center.Y.ToString()));
            }
        }

        private void send(Message m)
        {
            connection.GetStream().Write(m.getData(), 0, m.getData().Length);
        }

        private void RecData()
        {
            while (true)
            {
                byte[] responseData = new byte[1024];
                int size = connection.GetStream().Read(responseData, 0, 1024);
                parseData(Encoding.ASCII.GetString(responseData));

            }
        }

        private void parseData(string message)
        {
            Utilities.Log(message);

            string[] arr = message.Split(' '.ToString().ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            Constants.MessageTypes type = Constants.TypeFromStrings[arr[2]];
            int tempid = -1 ;
            if (int.TryParse(arr[1], out tempid))
            {
                if (tempid == id)
                {
                    if (type == Constants.MessageTypes.id)
                    {
                        id = int.Parse(arr[3]);
                        Console.Title = "Client " + id;
                        this.Title = "Client " + id;
                    }
                }
            }
            else
            {
                if (type == Constants.MessageTypes.move)
                {
                    en.center.X = int.Parse(arr[3]);
                    en.center.Y = int.Parse(arr[4]);
                }
            }
        }
        #endregion
    }
}