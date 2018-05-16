using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpyandPlaybackTestTool.SpyPlaybackObjects;
namespace SpyandPlaybackTestTool.Views
{
    public partial class ItemForm : Form
    {
        public static string information { get; set; } 
        public static string totalitem { get; set; }
        public  List<string> itemList = new List<string>();
        public ItemForm()
        {
            InitializeComponent();
         
          
        }

        private void ItemForm_Load(object sender, EventArgs e)
        {
            label1.Text = information;
            label2.Text = "Total items: "+ totalitem;
            foreach (string item in itemList)
            {
                listBox1.Items.Add(item);
            }
        }

    }
}
