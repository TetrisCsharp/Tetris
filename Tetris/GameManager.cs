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
        private int speed; // pour le reste
        private bool finishGame;
        private bool finishWithPiece;
        private Thread threadKey;
        private Thread threadDisplay;
        private Thread threadDelete;
        private Thread threadClientServer;
        private int linesDestroyed;


        //connection serveur
        private TcpClient _client;
        private StreamReader _sReader;
        private StreamWriter _sWriter;
        //connection serveur


        public GameManager(int refresh, int speed, String ipAddress, int portNum)
        {
            _client = new TcpClient();
            _client.Connect(ipAddress, portNum);
            
            this.grille = new Grille(12,8);
            this.refresh = refresh;
            this.speed = speed;
            this.finishGame = false;
            this.finishWithPiece = false;
            this.threadDisplay = new Thread(() => display(this));
            this.threadDelete = new Thread(() => deleteLine());
            this.threadKey = new Thread(() => KeyPressed(grille));
            this.setGameManager();
          
        }

        public void startGame()
        {

            threadKey.Start();
            threadDisplay.Start();
            _sReader = new StreamReader(_client.GetStream(), Encoding.ASCII);
            _sWriter = new StreamWriter(_client.GetStream(), Encoding.ASCII);

            
            new Thread(Reception).Start();
            

            while (!grille.grilleFull())
            {
                //écoute du serveur
                new Thread(Reception).Start();


                addPiece(grille);

                while (!grille.verifBelowPiece())
                {
                   
                    if (grille.beforeTheEnd()) break;
                    goDown(grille);
                    if (!grille.verifBelowPiece()) grille.suppressionPiece(); //line  new Thread(()=>AskServer("line")).Start();
                    grille.deleteLine(this);

                    
                }
                //requete serveur

                //askpiece  new Thread(()=>AskServer("askpiece")).Start();

            }
            Console.Clear();
            Console.WriteLine("GAME OVER !!!");

        }

        private void Reception()
        {
            
             String sDataIncomming = _sReader.ReadLine();
             Console.WriteLine(sDataIncomming);
            //Fonction rajouter une ligne

            
        }
        private void AskServer(string req)
        {
            
             String sData = req;
            // Console.Write("&gt; ");


            // sData = Console.ReadLine();
            //sData = "askpiece";
            //sData = "line";
            //sData = "finished";
          //...
             _sWriter.WriteLine(sData);
             _sWriter.Flush();

            
        }

        public void deleteLine()
        {
            while (true)
            {
                grille.deleteLine(this);
                
            }
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

        public void addPiece(Grille grille)
        {
            //Ajout Piece
            grille.AjoutPiece(new Piece());
        
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
           Thread.Sleep(this.speed);
        
        }

        public Grille Grille { get { return this.grille; } }
        public int Speed
        {
            get { return this.speed; }
            set { this.speed = value; }
        }

        public void setGameManager()
        {
            this.grille.setGameManager(this);
        }
    }


}


