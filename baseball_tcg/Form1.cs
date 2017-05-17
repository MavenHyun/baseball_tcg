using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace baseball_tcg
{
    public partial class Form1 : Form
    {
        Dictionary<int, Card> card_database = new Dictionary<int, Card>();
        Stack<Pitch> mdeck = new Stack<Pitch>(50);
        List<Card> bdeck = new List<Card>(10);
        Queue<Fielder> pdeck = new Queue<Fielder>(9);
        Card[] defense = new Card[10];  

        public Form1()
        {

            InitializeComponent();
            Board.load_database(card_database);
            Board.load_deck(card_database, mdeck, bdeck, pdeck, defense);
            
        }
    }

    public static class Board
    {
        public static void load_database(this Dictionary<int, Card> db)
        {
            string fnp = "resource/card_database.xml";
            XmlReader reader;
            try
            {
                XmlReaderSettings setting = new XmlReaderSettings();
                setting.IgnoreComments = true;
                setting.IgnoreWhitespace = true;
                reader = XmlReader.Create(fnp, setting);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            while (reader.Read())
            {
                if (reader.Name.CompareTo("Pitch") == 0 && reader.NodeType == XmlNodeType.Element)
                {
                    Pitch temp = new Pitch(reader.GetAttribute("pname"), int.Parse(reader.GetAttribute("id")), int.Parse(reader.GetAttribute("power")));
                    db.Add(temp.card_id, temp);
                }
                else if (reader.Name.CompareTo("Fielder") == 0 && reader.NodeType == XmlNodeType.Element)
                {
                    bool[] pos2 = { true, false, false, false, false, false, false, false, false, false };
                    char[] pos = reader.GetAttribute("pos").ToCharArray();
                    foreach (char c in pos) pos2[int.Parse(c.ToString())] = true;
                    Fielder temp = new Fielder(reader.GetAttribute("pname"), int.Parse(reader.GetAttribute("id")), reader.GetAttribute("team"),
                        int.Parse(reader.GetAttribute("bats")), pos2, int.Parse(reader.GetAttribute("bp")), int.Parse(reader.GetAttribute("con")),
                        int.Parse(reader.GetAttribute("def")), 0);
                    db.Add(temp.card_id, temp);    
                }
                else if (reader.Name.CompareTo("Pitcher") == 0 && reader.NodeType == XmlNodeType.Element)
                {
                    bool[] arsenal2 = { true, false, false, false, false, false, false, false, false, false };
                    char[] arsenal = reader.GetAttribute("arsenal").ToCharArray();
                    foreach (char c in arsenal) arsenal2[int.Parse(c.ToString())] = true;
                    Pitcher temp = new Pitcher(reader.GetAttribute("pname"), int.Parse(reader.GetAttribute("id")), reader.GetAttribute("team"),
                        int.Parse(reader.GetAttribute("throws")), arsenal2, int.Parse(reader.GetAttribute("pp")), int.Parse(reader.GetAttribute("stamina")), 0);
                    db.Add(temp.card_id, temp);
                }
            }
            reader.Close();
        }

        public static void load_deck(this Dictionary<int, Card> db, Stack<Pitch> mdeck, List<Card> bdeck, Queue<Fielder> pdeck, Card[] defense)
        {
            string fnp = "resource/deck.xml";
            XmlReader reader;
            try
            {
                XmlReaderSettings setting = new XmlReaderSettings();
                setting.IgnoreComments = true;
                setting.IgnoreWhitespace = true;
                reader = XmlReader.Create(fnp, setting);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            while (reader.Read())
            {
                if (reader.Name.CompareTo("MDeck") == 0 && reader.NodeType == XmlNodeType.Element)
                {
                    for (int i = 0; i < int.Parse(reader.GetAttribute("num")); i++) mdeck.Push((Pitch)db[int.Parse(reader.GetAttribute("id"))]);
                }
                else if (reader.Name.CompareTo("PDeck") == 0 && reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.GetAttribute("pos") == "1") defense[int.Parse(reader.GetAttribute("pos"))] = db[int.Parse(reader.GetAttribute("id"))];
                    else
                    {
                        defense[int.Parse(reader.GetAttribute("pos"))] = db[int.Parse(reader.GetAttribute("id"))];
                        int check = int.Parse(reader.GetAttribute("id"));
                        pdeck.Enqueue((Fielder)db[int.Parse(reader.GetAttribute("id"))]);
                    }
                }
                else if (reader.Name.CompareTo("BDeck") == 0 && reader.NodeType == XmlNodeType.Element) bdeck.Add(db[int.Parse(reader.GetAttribute("id"))]);
            }
            reader.Close();
        }
    }
}
