﻿using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
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
        //private const int _edit = 1;
        //private const int _delete = 2;
        public FrmBlogList()
        {

            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
            _dapperService = new DapperService(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        }


        private void FrmBlogList_Load(object sender, EventArgs e)
        {
            BlogList();
        }

        private void BlogList()
        {
            List<BlogModel> lst = _dapperService.Query<BlogModel>("select * from tbl_blog");
            dgvData.DataSource = lst;
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //int columnIndex = e.ColumnIndex;
            //int rowIndex = e.RowIndex;

            if (e.RowIndex == -1) return;

            var blogId = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["colId"].Value);

            if (e.ColumnIndex == (int)EnumFormControlType.Edit)
            {
                
                FrmBlog frm = new FrmBlog(blogId);
                frm.ShowDialog();
                BlogList();
            }
            else if (e.ColumnIndex == (int)EnumFormControlType.Delete)
            {
                var dialogresult = MessageBox.Show("Are you sure want to delete it?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogresult == DialogResult.Yes) return;
                 DeleteBlog(blogId);      
            }
            //EnumFormControlType enumFormControlType = EnumFormControlType.None;
            //switch (enumFormControlType)
            //{
            //    case EnumFormControlType.None:
            //        break;
            //    case EnumFormControlType.Edit:
            //        break;
            //    case EnumFormControlType.Delete;
            //        break;
            //    default:
            //        break;

            //}
         

        }
        private void DeleteBlog(int id)
        {
            string query = @"Delete From [dbo].[Tbl_Blog] WHERE [BlogId] = @BlogId ";
            int result = _dapperService.Execute(query, new { BlogId = id });

            string message = result > 0 ? "deleting successful." : "deleting failed.";
            MessageBox.Show(message);
            BlogList();
        }
    }
}
