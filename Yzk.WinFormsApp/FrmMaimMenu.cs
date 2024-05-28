using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yzk.WinFormsApp
{
    public partial class FrmMaimMenu : Form
    {
        public FrmMaimMenu()
        {
            InitializeComponent();
        }

        private void blogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBlog frm = new FrmBlog();
            frm.ShowDialog();
            // frm.Show();
        }

        private void newBlogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBlogList frm = new FrmBlogList();
            frm.ShowDialog();
        }
    }
}
