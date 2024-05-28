﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yzk.share;
using Yzk.WinFormsApp.Model;

namespace Yzk.WinFormsApp
{
    public partial class FrmBlogList : Form
    {

        private readonly DapperService _dapperService;
        public FrmBlogList()
        {

            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
            _dapperService = new DapperService(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        }


            private void FrmBlogList_Load(object sender, EventArgs e)
        {
          List<BlogModel> lst =  _dapperService.Query<BlogModel>("select * from tbl_blog");
            dgvData.DataSource = lst;
        }
    }
}
