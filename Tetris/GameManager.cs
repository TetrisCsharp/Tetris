using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Tetris_Like
{
    public class GameManager
    {
        
        private Grille grille;
        private int refresh; // pour l'affichage
      
        private bool finishGame;
        private bool finishWithPiece;
        private Thread threadKey;
        private Thread threadDisplay;
        private Thread threadDelete;
        private Thread threadClientServer;
        private int linesDestroyed;

        //Keyboard
        private ConsoleKey up;
        private ConsoleKey down;
        private ConsoleKey left;
        private ConsoleKey right;

        //connection serveur
        private TcpClient _client;
        private StreamReader _sReader;
        private StreamWriter _sWriter;

        //reponse serveur
        private int id;
        private int column;
        private int maxNumberOfLines;
        private int delaySpeed;

        public GameManager(String ipAddress, int portNum)
        {
            _client = new TcpClient();
            _client.Connect(ipAddress, portNum);
            StreamReader reader = new StreamReader(_client.GetStream(), Encoding.ASCII);

            string [] str = splitInfos(reader);
            this.column = Convert.ToInt32(str[0]);
            this.maxNumberOfLines = Convert.ToInt32(str[1]);
            this.delaySpeed = Convert.ToInt32(str[2]);

            //init grille
            this.grille = new Grille(maxNumberOfLines, column);

            this.refresh = 25;
            this.finishGame = false;
            this.finishWithPiece = false;
            this.threadDisplay = new Thread(() => display(this));
       //     this.threadDelete = new Thread(() => deleteLine());
            this.threadKey = new Thread(() => KeyPressed(grille));
            this.setGameManager();
        }

        // récupérer les informations du serveur pour initialiser la grille,delaySpeed
        public string [] splitInfos(StreamReader reader)
        {
            string str = reader.ReadLine();
            string [] str2 = str.Split(' ');
            return str2;
            
        }


        public void deleteLine()
        {
            for (int x = 0; x < this.grille.Tab.GetLength(0); x++)
            {
                if (this.grille.verifDeleteLineOn(x) == true)
                {
                    this.grille.moveElementAbove(x);
                    if (this.delaySpeed > 0) this.delaySpeed -= 10;
                    sendToServer("line"); // "line" send to the server

                }
            }
        }
        public void startGame()
        {
            _sReader = new StreamReader(_client.GetStream(), Encoding.ASCII);
            _sWriter = new StreamWriter(_client.GetStream(), Encoding.ASCII);

            threadKey.Start();
            threadDisplay.Start();

           

            while (!grille.grilleFull())
            {
                new Thread(Reception).Start();

                //écoute du serveur
               // id = askPiece(_sReader,_sWriter);
                new Thread(() => this.id = askPiece(_sReader, _sWriter)).Start();
                //création de la pièce
                addPiece(grille,id);

                while (!grille.verifBelowPiece())
                {
                   
                    if (grille.beforeTheEnd()) break;
                    goDown(grille);
                    if (!grille.verifBelowPiece())
                    {
                        grille.suppressionPiece();
                   
                    }
                    this.deleteLine();
                }
                //requete serveur
            }
            Console.Clear();
            Console.WriteLine("GAME OVER !!!");

        }



        public int askPiece(StreamReader reader,StreamWriter writer)//fonction random sur serveur
        {
            writer.WriteLine("askpiece");
            writer.Flush();
            String sDataIncomming = reader.ReadLine();

            Console.WriteLine(sDataIncomming);
            int id = int.Parse(sDataIncomming);
            return id;
        }

        private void Reception()
        {
             string sDataIncomming = _sReader.ReadLine();
             Console.WriteLine(sDataIncomming);

            if(sDataIncomming == "+1") // ajout de ligne
            {
                new Thread(() => grille.addLine()).Start();
              
            }
            else // partie terminé
            {

            }
        }
        //send line to the other clients or send message of the end of the game
        private void sendToServer(string message)
        {
             String sData = message;
             Console.WriteLine(message);
             _sWriter.WriteLine(sData);




            // sData = Console.ReadLine();
            //sData = "line";
            //sData = "finished";
          //...
            
             _sWriter.Flush();

            
        }

       

        public bool KeyPressed(Grille grille)
        {
            while (true)
            {
                ConsoleKeyInfo cki;
                cki = Console.ReadKey(true);
                this.userCommand(cki);

            }
        }

        //géré par un thread
        public void userCommand(ConsoleKeyInfo cki)
        {
            if (cki.Key == ConsoleKey.LeftArrow && !grille.OutOfLimitLeft() && !grille.verifLeftPiece())  goLeft(grille);
           
            if (cki.Key == ConsoleKey.RightArrow && !grille.OutOfLimitRight() && !grille.verifRightPiece()) goRight(grille);

            if (cki.Key == ConsoleKey.DownArrow && !grille.verifBelowPiece()) goDown(grille);


        }
        //gérer par un thread
        public void CommunicationWithServer()
        {

        }
        public void display(GameManager game)
        {
            while (true)
            {
                game.Grille.AffichageGrille();
                Thread.Sleep(this.refresh);
                Console.SetCursorPosition(0, 0);
                if (grille.grilleFull()) break;
            }

        }

        public void addPiece(Grille grille,int id)
        {
            //Ajout Piece
            grille.AjoutPiece(new Piece(id));
        
        }

        public void goRight(Grille grille)
        {
            grille.suppressionPiece();          
            grille.deplacementPiece(true);
      
        }

        public void goLeft(Grille grille)
        {
            grille.suppressionPiece();
            grille.deplacementPiece(false);
  
        }

        public void goDown(Grille grille)
        {
            grille.suppressionPiece();
            grille.descendrePiece();
           Thread.Sleep(this.delaySpeed);
        
        }

        public void addLine()
        {
            grille.addLine();
        }

        public Grille Grille { get { return this.grille; } }
        public int Speed
        {
            get { return this.delaySpeed; }
            set { this.delaySpeed = value; }
        }

        public void setGameManager()
        {
            this.grille.setGameManager(this);
        }
    }


}


