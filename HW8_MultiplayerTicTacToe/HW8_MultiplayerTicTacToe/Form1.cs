﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace HW8_MultiplayerTicTacToe
{
    public partial class Form1 : Form
    {
        CheckGame cg = new CheckGame();
        char[,] Board = new char[3,3];
        char FinalVal;
        int LocalValue1;
        int LocalValue2;
        TcpClient Player;

        public Form1()
        {
            InitializeComponent();
            txt_IpConnection.Text = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToString();
            //Listen();
        }    

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (Return(textBox1, 0, 0))
            {
                if (IsFin() == false)
                {
                    string val = Board[0,0].ToString();
                    SendMessage( Player ,"0", "0", val);
                    //Listen();
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {            
            if (Return(textBox2, 0, 1))
            {
                if (IsFin() == false)
                {
                    string val = Board[0,1].ToString();
                    SendMessage(Player, "0", "1", val);
                    //Listen();
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {           
            if (Return(textBox3, 0, 2))
            {
                if (IsFin() == false)
                {
                    string val = Board[0,2].ToString();
                    SendMessage(Player, "0", "2", val);
                    //Listen();
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (Return(textBox4, 1, 0))
            {
                if (IsFin() == false)
                {
                    string val = Board[1,0].ToString();
                    SendMessage(Player, "1", "0", val);
                    //Listen();
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            bool goodVal = Return(textBox5, 1, 1);
            if (goodVal == true)
            {
                if (IsFin() == false)
                {
                    string val = Board[1,1].ToString();
                    SendMessage(Player, "1", "1", val);
                    //Listen();
                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            bool goodVal = Return(textBox6, 1, 2);
            if (goodVal == true)
            {
                if (IsFin() == false)
                {
                    string val = Board[1,2].ToString();
                    SendMessage(Player, "1", "2", val);
                    //Listen();
                }
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            bool goodVal = Return(textBox7, 2, 0);
            if (goodVal == true)
            {
                if (IsFin() == false)
                {
                    string val = Board[2, 0].ToString();
                    SendMessage(Player, "2", "0", val);
                    //Listen();
                }

            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            bool goodVal = Return(textBox8, 2, 1);
            if (goodVal == true)
            {
                if (IsFin() == false)
                {
                    string val = Board[2,1].ToString();
                    SendMessage(Player, "2", "1", val);
                    //Listen();
                }
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            bool goodVal = Return(textBox9, 2, 2);
            if (goodVal == true)
            {
                if (IsFin() == false)
                {
                    string val = Board[2,2].ToString();
                    SendMessage(Player, "2", "2", val);
                    //Listen();
                }
            }            
        }

        private bool Return(TextBox Box, int loc1, int loc2)
        {
            string input = Box.Text;
            input = input.ToUpper();

            if ((input == "O") || (input == "X"))
            {
                Box.Enabled = false;
                Box.Text = input;
                Board[loc1, loc2] = char.Parse(input);
                return true;
            }
            else
            {
                MessageBox.Show("Enter either an X or an O");
                Box.Text = "";
                return false;
            }
            
        }

        bool IsFull()
        {
            List<TextBox> Boxes = TextBoxes();
            for(int i = 0; i < Boxes.Count; i++)
            {
                if (Boxes[i].Text == "")
                    return false;
            }

            return true;
        }

        bool IsFin()
        {
            List<TextBox> Box = TextBoxes();
            if (IsFull())
            {
                if (cg.CheckBoard(Board, ref FinalVal))
                {
                    label1.Text = "Game Over";
                    GrayBoxes(Box, false);
                    MessageBox.Show($"{FinalVal}'s Win");
                    return true;
                }
                else
                {
                    label1.Text = "Game Over";
                    return true;
                }
            }
            else
            {
                if (cg.CheckBoard(Board, ref FinalVal))
                {
                    label1.Text = "Game Over";
                    GrayBoxes(Box, false);
                    MessageBox.Show($"{FinalVal}'s Win");
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        void GrayBoxes(List<TextBox> Boxes, bool enable)
        {
            for (int i = 0; i< Boxes.Count; i++)
            {
                Boxes[i].Enabled = enable;
                if(enable == true)
                {
                    Boxes[i].Text = "";                    
                }
            }
            Board = new char[3, 3];
        }

        List<TextBox> TextBoxes()
        {
            List<TextBox> retBoxes = new List<TextBox>
            {
                textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9 
            };

            return retBoxes;
        }

        async void Listen()
        {
            try
            {
                Player = new TcpClient(txt_IpConnection.Text, 5555);
            }
            catch
            {
                AddMessage("No listener found, opening listener.");
                TcpListener listener = new TcpListener(Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork), 5555);
                listener.Start();
                Player = await listener.AcceptTcpClientAsync();
                await Task.Factory.StartNew(() => ListenForPacket(Player));
                listener.Stop();
                return;
            }
            AddMessage("Listener found, connection successful.");
            await Task.Factory.StartNew(() => ListenForPacket(Player));
        }

        private void AddMessage(string inVal)
        {
            List<TextBox> Boxes = TextBoxes();
            this.Invoke(new MethodInvoker(delegate
            {
                if (inVal.Length == 1)
                { 
                    //lbl_Connection.Text = (inVal);
                    //Boxes[(LocalValue1 * 3) + LocalValue2].Text = inVal ;
                    Return(Boxes[(LocalValue1 * 3) + LocalValue2], LocalValue1, LocalValue2);
                }
                else
                {
                    if(inVal.Length == 2)
                    {
                        char[] values = inVal.ToCharArray();
                        LocalValue1 = int.Parse(values[0].ToString());
                        LocalValue2 = int.Parse(values[1].ToString());
                    }
                    lbl_Connection.Text = inVal;
                }
            }));
        }

        private void ListenForPacket(TcpClient connection)
        {
            NetworkStream stream = connection.GetStream();
            List<string> resul = new List<string>();
            while (true)
            {
                byte[] bytesToRead = new byte[connection.ReceiveBufferSize];
                int bytesRead = stream.Read(bytesToRead, 0, connection.ReceiveBufferSize);
                string result = ASCIIEncoding.ASCII.GetString(bytesToRead, 0, bytesRead);
                if (result != "")
                {
                    resul.Add(result);
                    for(int i = 0; i < resul.Count; i++)
                    {
                        AddMessage(result);
                    }                    
                }
            }
        }

        private void SendMessage(TcpClient Connection, string Loc1, string Loc2, string Val)
        {
            string sender = (Val + Loc1 + Loc2 );
            byte[] bytesToSend = Encoding.ASCII.GetBytes(sender);
            Connection.GetStream().Write(bytesToSend, 0, bytesToSend.Count() - 1);
        }

        private void btn_ResetGame_Click(object sender, EventArgs e)
        {
            List<TextBox> Box = TextBoxes();
            label1.Text = "Tic Tac Toe";
            GrayBoxes( Box, true);
        }

        private void btn_TestConnection_Click(object sender, EventArgs e)
        {
            Listen();
            //SendMessage(Player , DateTime.Now.ToString(), "", "");
            if (Player != null) { 
            SendMessage(Player, "Connection Success", "", ".");
                 }
        }
    }
}
