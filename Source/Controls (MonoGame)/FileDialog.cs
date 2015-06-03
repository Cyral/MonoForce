using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MonoForce.Controls
{
    /// <summary>
    /// A file dialog
    /// </summary>
    /// <remarks>
    /// Derived from the sample on Mike Kukta's blog.
    /// </remarks>
    public class FileDialog : Window
    {
        #region Fields
        /// <summary>
        /// The combo box where the current directory is displayed.
        /// </summary>
        ComboBox cmbDirectory;
        /// <summary>
        /// The combo box where the drives are listed. 
        /// </summary>
        ComboBox cmbDrives;
        /// <summary>
        /// The list box where directory files are displayed.
        /// </summary>
        ListBox lstFiles;
        /// <summary>
        /// Stores the full path of the currently selected file.
        /// </summary>
        TextBox txtFileName;
        /// <summary>
        /// Textbox for file extension filter.
        /// </summary>
        TextBox txtFilter;
        /// <summary>
        /// Button to open the selected file.
        /// </summary>
        Button btnOpen;
        /// <summary>
        /// Button to cancel the file open operation.
        /// </summary>
        Button btnCancel;
        /// <summary>
        /// Button used to move up one directory level.
        /// </summary>
        Button btnDirectoryUp;
        /// <summary>
        /// Stores the list of file extensions to display. An 
        /// empty list will display all files. 
        /// </summary>
        EventedList<string> filters;
        /// <summary>
        /// Currently selected folder (full path.)
        /// </summary>
        string directory;
        /// <summary>
        /// Currently selected file.
        /// </summary>
        string file;

        /// <summary>
        /// Flags for showing/hiding hidden and system files and folders.
        /// </summary>
        bool showHiddenFiles = false;
        bool showHiddenFolders = false;
        bool showSystemFiles = false;
        bool showSystemFolders = false;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the list of file extension filters for the dialog. 
        /// </summary>
        public EventedList<string> Filters
        {
            get { return filters; }
        }

        /// <summary>
        /// Gets the name of the selected file. 
        /// </summary>
        public string FileName
        {
            // TODO: Path.Combine from string members.
            get { return txtFileName.Text; }
        }

        public bool ShowHiddenFiles
        {
            get { return showHiddenFiles; }
            set { showHiddenFiles = value; }
        }

        public bool ShowHiddenFolders
        {
            get { return showHiddenFolders; }
            set { showHiddenFolders = value; }
        }

        public bool ShowSystemFiles
        {
            get { return showSystemFiles; }
            set { showSystemFiles = value; }
        }

        public bool ShowSystemFolders
        {
            get { return showSystemFolders; }
            set { showSystemFolders = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of the open file dialog. 
        /// </summary>
        public FileDialog(Manager manager)
            : base(manager)
        {
            filters = new EventedList<string>();
            filters.ItemAdded += new MonoForce.Controls.EventHandler(filters_ItemAdded);
            filters.ItemRemoved += new MonoForce.Controls.EventHandler(filters_ItemRemoved);
        }
        #endregion

        #region Initialize
        /// <summary>
        /// Initializes the dialog and its controls.
        /// </summary>
        public override void Init()
        {
            base.Init();
            Width = 420;
            Height = 375;
            Text = "Open Image";
            ResizeEnd += new MonoForce.Controls.EventHandler(OpenFileDialog_ResizeEnd);
            Resize += new ResizeEventHandler(OpenFileDialog_Resize);
            cmbDrives = new ComboBox(Manager);
            cmbDrives.Init();
            cmbDrives.Left = 0;
            cmbDrives.Top = 0;
            cmbDrives.Width = 60;
            cmbDrives.Height = 25;
            cmbDrives.Text = DriveInfo.GetDrives()[0].Name;
            EnumerateDrives();
            cmbDrives.ItemIndex = 0;
            cmbDrives.ItemIndexChanged += new MonoForce.Controls.EventHandler(cmbDrives_ItemIndexChanged);
            Add(cmbDrives);

            cmbDirectory = new ComboBox(Manager);
            cmbDirectory.Init();
            cmbDirectory.Left = 60;
            cmbDirectory.Top = 0;
            cmbDirectory.Width = 315;
            cmbDirectory.Height = 25;

            // Select the selected drive's root directory.
            cmbDirectory.Text = DriveInfo.GetDrives()[0].RootDirectory.Name;
            directory = DriveInfo.GetDrives()[0].RootDirectory.FullName;
            Add(cmbDirectory);

            btnDirectoryUp = new Button(Manager);
            btnDirectoryUp.Init();
            btnDirectoryUp.Left = 375;
            btnDirectoryUp.Top = 0;
            btnDirectoryUp.Width = 25;
            btnDirectoryUp.Height = 25;
            btnDirectoryUp.Glyph = new Glyph(Manager.Skin.Images["Shared.ArrowUp"].Resource);
            btnDirectoryUp.Glyph.SizeMode = SizeMode.Centered;
            btnDirectoryUp.Glyph.Color = Manager.Skin.Controls["Button"].Layers["Control"].Text.Colors.Enabled;
            btnDirectoryUp.Click += new MonoForce.Controls.EventHandler(btnDirectoryUp_Click);
            Add(btnDirectoryUp);

            lstFiles = new ListBox(Manager);
            lstFiles.Init();
            lstFiles.Left = 0;
            lstFiles.Top = 25;
            lstFiles.Width = 400;
            lstFiles.Height = 250;
            lstFiles.ItemIndex = 0;
            lstFiles.DoubleClick += new MonoForce.Controls.EventHandler(lstFiles_DoubleClick);
            lstFiles.ItemIndexChanged += new MonoForce.Controls.EventHandler(lstFiles_ItemIndexChanged);
            EnumerateDirectories();
            EnumerateFiles();
            Add(lstFiles);

            txtFileName = new TextBox(Manager);
            txtFileName.Init();
            txtFileName.Left = 0;
            txtFileName.Top = 275;
            txtFileName.Width = 300;
            txtFileName.Height = 25;
            Add(txtFileName);

            txtFilter = new TextBox(Manager);
            txtFilter.Init();
            txtFilter.Left = 300;
            txtFilter.Top = 275;
            txtFilter.Width = 100;
            txtFilter.Height = 25;
            txtFilter.KeyPress += new KeyEventHandler(txtFilter_KeyPress);
            Add(txtFilter);

            btnOpen = new Button(Manager);
            btnOpen.Init();
            btnOpen.Left = 200;
            btnOpen.Top = 300;
            btnOpen.Width = 100;
            btnOpen.Height = 25;
            btnOpen.Text = "Open";
            btnOpen.Click += new MonoForce.Controls.EventHandler(btnOpen_Click);
            Add(btnOpen);

            btnCancel = new Button(Manager);
            btnCancel.Init();
            btnCancel.Left = 300;
            btnCancel.Top = 300;
            btnCancel.Width = 100;
            btnCancel.Height = 25;
            btnCancel.Text = "Cancel";
            btnCancel.Click += new MonoForce.Controls.EventHandler(btnCancel_Click);
            Add(btnCancel);
        }
        #endregion

        #region Enumerate Drives
        private void EnumerateDrives()
        {
            // Populate combo box with available drives.
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                cmbDrives.Items.Add(drive.Name);
            }
        }
        #endregion

        #region Enumerate Directories
        private void EnumerateDirectories()
        {
            List<string> dirs = Directory.EnumerateDirectories(directory).ToList<string>();

            foreach (string dir in dirs)
            {
                // Check for hidden and system folders. See if they should be displayed.
                DirectoryInfo info = new DirectoryInfo(dir);
                if ((info.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                {
                    if (showHiddenFolders == false)
                    {
                        continue;
                    }
                }

                if ((info.Attributes & FileAttributes.System) == FileAttributes.System)
                {
                    if (showSystemFolders == false)
                    {
                        continue;
                    }
                }

                int lastSlashIndex = dir.LastIndexOf(Path.DirectorySeparatorChar);
                lstFiles.Items.Add(dir.Substring(lastSlashIndex + 1, dir.Length - lastSlashIndex - 1));
            }
        }
        #endregion

        #region Enumerate Files
        private void EnumerateFiles()
        {
            List<string> files = Directory.EnumerateFiles(directory).ToList<string>();

            foreach (string file in files)
            {
                // Check for hidden and system folders. See if they should be displayed.
                FileInfo info = new FileInfo(file);
                if ((info.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                {
                    if (showHiddenFiles == false)
                    {
                        continue;
                    }
                }

                if ((info.Attributes & FileAttributes.System) == FileAttributes.System)
                {
                    if (showSystemFiles == false)
                    {
                        continue;
                    }
                }

                // Check file extensions?
                if (filters.Count != 0)
                {
                    // Filter files by extension.
                    int index = file.LastIndexOf('.');
                    int length = file.Length;

                    if (index != -1)
                    {
                        string ext = file.Substring(index, length - index);

                        for (int i = 0; i < filters.Count; ++i)
                        {
                            if (ext == filters[i])
                            {
                                int lastSlashIndex = file.LastIndexOf(Path.DirectorySeparatorChar);
                                lstFiles.Items.Add(file.Substring(lastSlashIndex + 1, file.Length - lastSlashIndex - 1));
                            }
                        }
                    }
                }

                else
                {
                    // Nothing to filter. Add all files.
                    int lastSlashIndex = file.LastIndexOf(Path.DirectorySeparatorChar);
                    lstFiles.Items.Add(file.Substring(lastSlashIndex + 1, file.Length - lastSlashIndex - 1));
                }
            }
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Closes the dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnCancel_Click(object sender, MonoForce.Controls.EventArgs e)
        {
            this.Close(ModalResult.None);
        }

        /// <summary>
        /// Closes the dialog if a file was selected. The file name can be retrieved 
        /// from the Closing event. See test app for example. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnOpen_Click(object sender, MonoForce.Controls.EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFileName.Text))
            {
                return;
            }

            else
            {
                this.Close(ModalResult.Ok);
            }
        }

        /// <summary>
        /// Moves up a directory level. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnDirectoryUp_Click(object sender, MonoForce.Controls.EventArgs e)
        {
            // Move up a directory, unless we're at drive root. 
            if (cmbDrives == null || cmbDrives.ItemIndex == -1)
            {
                // Ignore request.
                return;
            }

            int index = cmbDirectory.Text.LastIndexOf(Path.DirectorySeparatorChar);

            if (index == -1)
            {
                // At drive root. Do nothing.
                return;
            }

            directory = cmbDirectory.Text.Substring(0, index) + Path.DirectorySeparatorChar;
            cmbDirectory.Text = directory;
            lstFiles.Items.Clear();

            // Enumerate files and folders in the selected directory.
            EnumerateDirectories();
            EnumerateFiles();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbDrives_ItemIndexChanged(object sender, MonoForce.Controls.EventArgs e)
        {
            // Repopulate the list starting from root.
            if (cmbDrives == null || cmbDrives.ItemIndex == -1)
            {
                // Ignore request.
                return;
            }

            cmbDirectory.Text = DriveInfo.GetDrives()[cmbDrives.ItemIndex].RootDirectory.Name;
            directory = DriveInfo.GetDrives()[cmbDrives.ItemIndex].RootDirectory.FullName;
            lstFiles.Items.Clear();

            // Enumerate files and folders in the selected directory.
            EnumerateDirectories();
            EnumerateFiles();
        }

        void lstFiles_DoubleClick(object sender, MonoForce.Controls.EventArgs e)
        {
            // TODO: If entry is a directory, move to the directory and repopulate the item list.
            string item = lstFiles.Items[lstFiles.ItemIndex].ToString();

            if (Directory.Exists(Path.Combine(cmbDirectory.Text, item)))
            {
                cmbDirectory.Text = Path.Combine(cmbDirectory.Text, item);
                directory = cmbDirectory.Text;

                // Enumerate files and folders in the selected directory.
                lstFiles.Items.Clear();
                lstFiles.ScrollTo(0);
                EnumerateDirectories();
                EnumerateFiles();
            }

            // TODO: If entry is a file, update the text box and select that as the file to open. 
            else if (File.Exists(Path.Combine(cmbDirectory.Text, item)))
            {
                txtFileName.Text = Path.Combine(cmbDirectory.Text, item);
                file = txtFileName.Text;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lstFiles_ItemIndexChanged(object sender, MonoForce.Controls.EventArgs e)
        {
            // Update name in the file name textbox.
            txtFileName.Text = Path.Combine(cmbDirectory.Text, lstFiles.Items[lstFiles.ItemIndex].ToString());
            file = txtFileName.Text;
        }
        #endregion

        #region Resize Event Handlers
        void OpenFileDialog_Resize(object sender, ResizeEventArgs e)
        {
            // Resize the controls so they expand with the dialog. 
            cmbDirectory.Width = (ClientWidth - cmbDirectory.Left - btnDirectoryUp.Width);
            btnDirectoryUp.Left = (ClientWidth - btnDirectoryUp.Width);
            lstFiles.Width = (ClientWidth);
            lstFiles.Height = (ClientHeight - lstFiles.Top - btnOpen.Height - txtFileName.Height);

            txtFileName.Top = (lstFiles.Top + lstFiles.Height);
            txtFileName.Width = (ClientWidth - txtFilter.Width);
            txtFilter.Top = (lstFiles.Top + lstFiles.Height);
            txtFilter.Left = (ClientWidth - txtFilter.Width);

            btnCancel.Top = (ClientHeight - btnCancel.Height);
            btnCancel.Left = (ClientWidth - btnCancel.Width);
            btnOpen.Top = (ClientHeight - btnOpen.Height);
            btnOpen.Left = (btnCancel.Left - btnOpen.Width);
        }


        void OpenFileDialog_ResizeEnd(object sender, MonoForce.Controls.EventArgs e)
        {
            // Resize the controls so they expand with the dialog. 
            cmbDirectory.Width = (ClientWidth - cmbDirectory.Left - btnDirectoryUp.Width);
            btnDirectoryUp.Left = (ClientWidth - btnDirectoryUp.Width);
            lstFiles.Width = (ClientWidth);
            lstFiles.Height = (ClientHeight - lstFiles.Top - btnOpen.Height - txtFileName.Height);
            txtFileName.Top = (lstFiles.Top + lstFiles.Height);
            txtFileName.Width = (ClientWidth - txtFilter.Width);
            txtFilter.Top = (lstFiles.Top + lstFiles.Height);
            txtFilter.Left = (ClientWidth - txtFilter.Width);
            btnCancel.Top = (ClientHeight - btnCancel.Height);
            btnCancel.Left = (ClientWidth - btnCancel.Width);
            btnOpen.Top = (ClientHeight - btnOpen.Height);
            btnOpen.Left = (btnCancel.Left - btnOpen.Width);
        }
        #endregion

        #region Filter List Event Handlers
        void txtFilter_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Microsoft.Xna.Framework.Input.Keys.Enter)
            {
                // TODO: Parse the filter list. 
                string extensions = txtFilter.Text;

                if (!string.IsNullOrEmpty(extensions))
                {
                    filters.Clear();

                    // Semi-colon delimited list. 
                    string[] ext = extensions.Split(';');

                    if (ext.Length != 0)
                    {
                        for (int i = 0; i < ext.Length; ++i)
                        {
                            // Valid extensions start with a '.' and are at least two chars in length. 
                            ext[i] = ext[i].Trim();

                            if (ext[i].Length >= 2 && ext[i][0] == '.')
                            {
                                filters.Add(ext[i]);
                            }
                        }
                    }
                }

                else
                {
                    // No text? No filters. 
                    filters.Clear();
                }
            }
        }

        void filters_ItemRemoved(object sender, MonoForce.Controls.EventArgs e)
        {
            lstFiles.Items.Clear();
            EnumerateDirectories();
            EnumerateFiles();
        }

        void filters_ItemAdded(object sender, MonoForce.Controls.EventArgs e)
        {
            lstFiles.Items.Clear();
            EnumerateDirectories();
            EnumerateFiles();
        }
        #endregion
    }
}
