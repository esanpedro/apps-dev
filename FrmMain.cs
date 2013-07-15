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
    public partial class FrmMain : Form
    {
        #region Local Variables
        int maxbytes = 0;
        int totalsizegain = 0;
        int Read = 0;
        #endregion
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            ckbRecycleBin.Checked = true;
            ckbDownloadedProgs.Checked = true;
            ckbCookies.Checked = true;
            ckbOfflineWeb.Checked = true;
            ckbTempIntFiles.Checked = true;
            ckbTemp.Checked = true;
            ckbHistory.Checked = true;
            ckbFavorites.Checked = false;
            ckbRecentDocs.Checked = true;
            listview();
            btnSelectAll_Files.Text = "Select All";
            listViewDrives.View = View.Details;
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {                
                    ListViewItem lviDrives = new ListViewItem(
                            new string[] { drive.Name, drive.DriveFormat, (drive.TotalSize / 1024 / 1024 / 1024).ToString() + " GB", (drive.TotalFreeSpace / 1024 / 1024 / 1024).ToString() + " GB" });
                    listViewDrives.Items.Add(lviDrives);
                    listViewDrives.Items[0].Checked = true;
                }
                else
                {
                    return;
                }
            }
            
            
        }
        
        private void toolStripButtonScan_Click(object sender, EventArgs e)
        {          
            grbListView.Visible = true;
            grbInfo.Visible = true;
            grbIncFolders.Visible = false;  
        }

        private void toolStripButtonOptions_Click(object sender, EventArgs e)
        {
            grbListView.Visible = false;
            grbInfo.Visible = false;
            grbIncFolders.Visible = true;

        }

        #region Options Menu
        private void btnDefault_Click(object sender, EventArgs e)
        {
            ckbRecycleBin.Checked = true;
            ckbDownloadedProgs.Checked = true;
            ckbCookies.Checked = true;
            ckbOfflineWeb.Checked = true;
            ckbTempIntFiles.Checked = true;
            ckbTemp.Checked = true;
            ckbHistory.Checked = true;
            ckbFavorites.Checked = false;
            ckbRecentDocs.Checked = true;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            ckbRecycleBin.Checked = true;
            ckbDownloadedProgs.Checked = true;
            ckbCookies.Checked = true;
            ckbOfflineWeb.Checked = true;
            ckbTempIntFiles.Checked = true;
            ckbTemp.Checked = true;
            ckbHistory.Checked = true;
            ckbFavorites.Checked = true;
            ckbRecentDocs.Checked = true;
        }

        private void btnUnSelectAll_Click(object sender, EventArgs e)
        {
            ckbRecycleBin.Checked = false;
            ckbDownloadedProgs.Checked = false;
            ckbCookies.Checked = false;
            ckbOfflineWeb.Checked = false;
            ckbTempIntFiles.Checked = false;
            ckbTemp.Checked = false;
            ckbHistory.Checked = false;
            ckbFavorites.Checked = false;
            ckbRecentDocs.Checked = false;
        }
        #endregion
        
        private void btnStart_Click(object sender, EventArgs e)
        {
            FrmMain frm_main = new FrmMain();
            toolStripButtonScan.Enabled = false;
            btnStart.Enabled = false;
            lsvFile.Items.Clear();
            ClearAll();
            lbltotalsizegain.Visible = true;
            for (int item = 0; item < listViewDrives.Items.Count; item++)
            {
                if (listViewDrives.Items[item].Checked)
                {
                    if (ckbRecycleBin.Checked)
                    {

                        ScanSubFoldersAndFiles(listViewDrives.Items[item].Text + @"RECYCLED", 7);
                    }
                    if (ckbDownloadedProgs.Checked)
                    {
                        ScanSubFoldersAndFiles(listViewDrives.Items[item].Text + @"WINDOWS\Downloaded Program Files", 1);
                    }
                    if (ckbCookies.Checked)
                    {
                        ScanSubFoldersAndFiles(listViewDrives.Items[item].Text + @"WINDOWS\Cookies", 2);
                    }
                    if (ckbOfflineWeb.Checked)
                    {
                        ScanSubFoldersAndFiles(listViewDrives.Items[item].Text + @"WINDOWS\Offline Web Pages", 3);
                    }
                    if (ckbTempIntFiles.Checked)
                    {
                        ScanSubFoldersAndFiles(listViewDrives.Items[item].Text + @"WINDOWS\Temporary Internet Files", 4);
                    }
                    if (ckbTemp.Checked)
                    {
                        ScanSubFoldersAndFiles(listViewDrives.Items[item].Text + @"WINDOWS\Temp", 4);
                    }
                    if (ckbHistory.Checked)
                    {
                        ScanSubFoldersAndFiles(listViewDrives.Items[item].Text + @"WINDOWS\History", 5);
                    }
                    if (ckbFavorites.Checked) 
                    {
                        ScanSubFoldersAndFiles(listViewDrives.Items[item].Text + @"WINDOWS\Favorites", 6);
                    }
                    if (ckbRecentDocs.Checked)
                    {
                        ScanSubFoldersAndFiles(listViewDrives.Items[item].Text + @"WINDOWS\Recent", 4);
                    }
                }
            }
            toolStripButtonScan.Enabled = true;
            btnStart.Enabled = true;
            btnClear.Visible = true;
            lbltotalsizegain.Text = "Total size gain: " + (totalsizegain / 1024).ToString() + " Kilobytes";
            lblFilename.Text = "Done Scanning...";
        }

        private void ClearAll()
        {
            lbltotalsizegain.Visible = true;
            lblFN.Visible = false;
            lblFName.Text = "";
            lblLoc.Visible = false;
            lblLocation.Text = "";
            lblSiz.Visible = false;
            lblSize.Text = "";
            lsvFile.Clear();
            listview();
            btnStart.Focus();
            btnClear.Visible = false;
            pcbIcon.Visible = false;
            lbltotalsizegain.Visible = false;
            totalsizegain = 0;
        }
        private void listview()
        {
            lsvFile.View = View.Details;
            lsvFile.Columns.Add("Files", 280, HorizontalAlignment.Left);
            lsvFile.Columns.Add("Size", 120, HorizontalAlignment.Left);  
            lsvFile.Columns.Add("Location", 150, HorizontalAlignment.Left);     
        }
        
        private void ScanSubFoldersAndFiles(string path, long FileType)
        {
            DirectoryInfo DirPath = new DirectoryInfo(path);
            if (!DirPath.Exists) 
            {
                return;
            }
            foreach (FileInfo file in DirPath.GetFiles())
            {
                maxbytes = 0;
                lblFilename.Text = Path.Combine(path, file.Name);
                lblFilename.Refresh();
                maxbytes += (int)file.Length;
                prgFileRead.Maximum = maxbytes;
                Read += (int)file.Length;
                Read /= 1024;
                prgFileRead.Step = Read;
                prgFileRead.PerformStep();
                totalsizegain += maxbytes;
                    ListViewItem lvi = new ListViewItem(
                        new string[] { file.Name, maxbytes.ToString() + " Bytes", path});
                    lsvFile.Items.Add(lvi);
                
            }
            foreach (DirectoryInfo subFolder in DirPath.GetDirectories())
            {
                ScanSubFoldersAndFiles(Path.Combine(path,subFolder.Name), FileType);
            }
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to \n'Clean' the selected documents?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                ClearAll();
            }
        }

        private void lsvFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbltotalsizegain.Visible = false;
            if (this.lsvFile.SelectedItems.Count > 0)
            {
                lblFN.Visible = true;
                lblLoc.Visible = true;
                lblSiz.Visible = true;
                pcbIcon.Visible = true;
                lblFName.Text = lsvFile.SelectedItems[0].Text;
                lblLocation.Text = lsvFile.SelectedItems[0].SubItems[2].Text;
                lblSize.Text = lsvFile.SelectedItems[0].SubItems[1].Text;
                string FileLocation = lsvFile.SelectedItems[0].SubItems[2].Text + @"\" + lblFName.Text;
                Icon fileIcon = Icon.ExtractAssociatedIcon(FileLocation);
                pcbIcon.Image = Bitmap.FromHicon(new Icon(fileIcon, new Size(64, 64)).Handle);
                lblFName.Refresh();
            }
        }

        private void btnSelectAll_Files_Click(object sender, EventArgs e)
        {
            if (lsvFile.Items.Count != 0)
            {
                if (btnSelectAll_Files.Text == "Select All")
                {
                    foreach (ListViewItem item in lsvFile.Items)
                    {
                        item.Checked = true;
                    }
                    btnSelectAll_Files.Text = "UnSelect All";
                }
                else
                {
                    foreach (ListViewItem item in lsvFile.Items)
                    {
                        item.Checked = false;
                    }
                    btnSelectAll_Files.Text = "Select All";
                }
            }
        }

        private void toolStripBtnSheduler_Click(object sender, EventArgs e)
        {
            FrmScheduler frm_sched = new FrmScheduler();
            frm_sched.Visible = true;
        }
