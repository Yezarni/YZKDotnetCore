using System.Drawing.Text;
using Yzk.share;
using Yzk.WinFormsApp.Model;
using Yzk.WinFormsApp.Queries;

namespace Yzk.WinFormsApp
{
    public partial class FrmBlog : Form
    {
        private readonly DapperService _dapperService;
        public FrmBlog()
        {
            
            InitializeComponent();
            _dapperService = new DapperService(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        }

       private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BlogModel blog = new BlogModel();
                blog.BlogTitle = txtTitle.Text.Trim();
                blog.BlogAuthor = txtAuthor.Text.Trim();
                blog.BlogContent = txtContent.Text.Trim();

               int result = _dapperService.Execute(BlogQuery.BlogCreate, blog);
                string message = result > 0 ? "Saving Successful." : "Saving Fail";
              
                MessageBox.Show (message, "Blog", MessageBoxButtons.OK , result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error );
                if (result > 0 )
                ClearControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
                
        }

        private void ClearControl()
        {
            txtTitle.Clear();
            txtAuthor.Clear();
            txtContent.Clear();

            txtContent.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
        }
    }
}
