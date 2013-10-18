using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AutomatedDiskCleaner
{
    public partial class FrmScan : Form
    {
        #region Local Variables
        int maxbytes = 0;
        int totalsizegain = 0;
        int Read = 0;
        #endregion

        public static List<FileInfo> fileList
        {
            get;
            set;
        }

        public FrmScan(DirectoryInfo DirPath)
        {
            InitializeComponent();
            ScanFiles(DirPath);
        }

        public void ScanFiles(DirectoryInfo DirPath)
        {
            lblScanFiles.Text = DirPath.FullName;
            lblScanFiles.Refresh();
            if (!DirPath.Exists)
            {
                return;
            }
            foreach (FileInfo file in DirPath.GetFiles())
            {
                lblScanFiles.Text = file.FullName;
                FrmScan.fileList.Add(file);
            }
        }
    }
}
