using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OracleTester
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            this.Text += " " + global.Version;
            config.Load();
            txtTNSString.Text = config.instance.LastTNS;
            txtQuery.Text = config.instance.LastQuery;

            splitContainer1.DoubleBuffered(true);
            dgv.DoubleBuffered(true);
        }

        DataSet ds2 = new DataSet();
        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                config.instance.LastTNS = txtTNSString.Text;
                config.Save();
                using (OleDbConnection oracleConnection = new OleDbConnection(txtTNSString.Text))
                {
                    oracleConnection.Open();
                    MessageBox.Show("Success", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n\n" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnCheckList_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
@"<OLEDB 연결 방식 체크리스트>

1. Oracle client [관리자 모드] 설치 (Instant 모드 안됨)

2. ODAC 설치 (OLEDB 드라이버 필수 설치)

3. 재부팅.

4. 재부팅 해도 안되면 오라클 홈 디렉토리의 BIN폴더에 가서 OraOLEDB11.dll 파일을 regsvr32를 통해 등록

※ TNS방식일 경우 홈 디렉토리\network\admin 폴더 tnsnames.ora 파일 수정

※ TNS방식일 경우 sqlnet.ora 파일 수정
");
        }

        private void txtQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                RunQuery();
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            RunQuery();
        }

        private void RunQuery()
        {
            try
            {
                string strQuery = txtQuery.Text;
                if (txtQuery.SelectionLength > 0)
                {
                    strQuery = txtQuery.SelectedText;
                }

                config.instance.LastQuery = txtQuery.Text;
                config.Save();

                using (OleDbConnection oracleConnection = new OleDbConnection(txtTNSString.Text))
                {
                    oracleConnection.Open();

                    OleDbCommand cmdRow2 = new OleDbCommand(strQuery, oracleConnection);
                    //cmdRow.CommandTimeout = 20;
                    OleDbDataAdapter da = new OleDbDataAdapter(cmdRow2);

                    dgv.DataSource = null;
                    dgv.Columns.Clear();
                    lbRowsCount.Text = "Total Rows: 0";
                    ds2?.Clear();

                    da.Fill(ds2);
                    DataTable dt2 = ds2.Tables[0];
                    dgv.DataSource = dt2;

                    lbRowsCount.Text = "Total Rows: " + dgv.Rows.Count.ToString("N0");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n\n" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
