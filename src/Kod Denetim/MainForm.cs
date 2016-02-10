using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using CodeAnalysis.Checks;
using ScintillaNET;

namespace CodeAnalysis
{
    public partial class MainForm : BaseForm
    {
        private List<ITester> Testers = new List<ITester>();
        private string selectednode = "";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // satır numaraları için gerekli
            scintilla1.Margins[0].Width = 16;    
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if(DialogResult.OK != openFileDialog1.ShowDialog())
                return;
            //string fileName = openFileDialog1.FileName;
            //txtFile.Text = fileName;
            //OpenFile(fileName);
            if (openFileDialog1.FileNames.Length == 1)
            {
                txtFile.Text = openFileDialog1.FileName;
            }
            selectednode = "";
            treeView1.Nodes.Clear();
            foreach (var filename in openFileDialog1.FileNames)
            {
                TreeNode node = new TreeNode();
                node = new TreeNode(Path.GetFileName(filename), 0, 0);
                node.Tag = filename;
                treeView1.Nodes.Add(node);
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            OpenFile(selectednode);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            //if (txtFile.Text == "")
            //{
            //    MessageBox.Show("Dosya seçiniz.");
            //    return;
            //}
            if (!ValidatePath(selectednode))
                return;

            Parse();
        }

        private string OpenFileDialog()
        {
            openFileDialog1.ShowDialog();            
            return openFileDialog1.FileName;
        }

        private void OpenFile(string path)
        {
            if (!ValidatePath(path))
                return;
            scintilla1.ReadOnly = false;
            scintilla1.Text = File.ReadAllText(path);
            scintilla1.ReadOnly = true;
        }

        private bool ValidatePath(string path)
        {
            if (path == null)
            {
                MessageBox.Show("Dosya geçerli değil.", "Kod Denetim", MessageBoxButtons.OK);
                return false;
            }
            if (path == "")
            {
                MessageBox.Show("Dosya geçerli değil.", "Kod Denetim", MessageBoxButtons.OK);
                return false;
            }
            char[] invalidPathChars = Path.GetInvalidPathChars();
            char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
            string fileName = Path.GetFileName(path);
            if (path.IndexOfAny(invalidPathChars) != -1 || fileName.IndexOfAny(invalidFileNameChars) != -1)
            {
                MessageBox.Show("Dosya geçerli değil.", "Kod Denetim", MessageBoxButtons.OK);
                return false;
            }
            string ext = Path.GetExtension(path);
            if(!ext.Equals(".cpp", StringComparison.OrdinalIgnoreCase)
                && !ext.Equals(".h", StringComparison.OrdinalIgnoreCase)
                && !ext.Equals(".hpp", StringComparison.OrdinalIgnoreCase)
                && !ext.Equals(".txt", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Sadece c++ ve metin dosyaları.", "Kod Denetim", MessageBoxButtons.OK);
                return false;
            }
            if(!File.Exists(path))
            {
                MessageBox.Show("Dosya bulunmuyor.", "Kod Denetim", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private void PrepTesters()
        {
            Testers = new List<ITester>();
            Testers.Add(new GotoTester());
            Testers.Add(new ParamCountTester());
            Testers.Add(new NestedIfTester());
            Testers.Add(new LongFunctionTester());
            //Testers.Add(new NoCommentTester());
            Testers.Add(new AssignmentInIfTester());
            Testers.Add(new EmptyCatchTester());
        }

        private void Parse()
        {
            using (FileStream filestream = new FileStream(txtFile.Text, FileMode.Open))
            {
                AntlrInputStream inputstream = new AntlrInputStream(filestream);
                CPP14Lexer lexer = new CPP14Lexer(inputstream);
                CommonTokenStream tokens = new CommonTokenStream(lexer);
                CPP14Parser parser = new CPP14Parser(tokens);
                CPP14Parser.TranslationunitContext translationunit = parser.translationunit();
                
                if(translationunit.exception != null)
                {
                    MessageBox.Show(translationunit.exception.Message + "\n\n" + translationunit.exception.StackTrace, "Kod Denetim", MessageBoxButtons.OK);
                    return;
                }

                PrepTesters();
                Walk(translationunit);
                
                dataGridView1.Rows.Clear();
                Testers.ForEach(tester => 
                    {
                        LinkedList<Error> errors = tester.GetErrors();
                        if (errors == null)
                            return;
                        foreach (var error in errors)
                            dataGridView1.Rows.Add(error.LineNumber, error.Message);
                    });
                if(dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Kusurlu kod bulunmadı.", "Kod Denetim", MessageBoxButtons.OK);
                }
                label3.Text = dataGridView1.Rows.Count.ToString();
            }
        }

        private void Walk(IParseTree t)
        {
            if (t is IErrorNode)
            {
                return;
            }
            Testers.ForEach(tester => tester.Test(t));
            for (int i = 0; i < t.ChildCount; i++)
            {                
                Walk(t.GetChild(i));
            }
        }

        private void menuItem11_Click(object sender, EventArgs e)
        {
            btnBrowse_Click(null, null);
        }

        private void menuItem31_Click(object sender, EventArgs e)
        {
            btnRun_Click(null, null);
        }

        private void menuItem51_Click(object sender, EventArgs e)
        {
            About frmAbout = new About();
            frmAbout.ShowDialog();
        }

        private void menuItem12_Click(object sender, EventArgs e)
        {
            btnReload_Click(null, null);
        }

        private int maxLineNumberCharLength;
        private void scintilla_TextChanged(object sender, EventArgs e)
        {
            var maxLineNumberCharLength = scintilla1.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == this.maxLineNumberCharLength)
                return;

            const int padding = 2;
            scintilla1.Margins[0].Width = scintilla1.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            this.maxLineNumberCharLength = maxLineNumberCharLength;
        }

        private void GetDirectories(TreeNode nodeToAddTo, string path)
        {
            if (nodeToAddTo != null)
            {
                nodeToAddTo.Nodes.Clear();
            }
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                TreeNode node = new TreeNode();
                node = new TreeNode(subDir.Name, 1, 1);
                node.Tag = subDir.FullName;
                node.Nodes.Add("");
                if (nodeToAddTo == null)
                    treeView1.Nodes.Add(node);
                else
                    nodeToAddTo.Nodes.Add(node);
            }
            foreach (FileInfo file in dir.GetFiles())
            {
                TreeNode node = new TreeNode();
                node.Text = file.Name;
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
                node.Tag = file.FullName;
                if (nodeToAddTo == null)
                    treeView1.Nodes.Add(node);
                else
                    nodeToAddTo.Nodes.Add(node);
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                if (e.Node.Nodes[0].Text == "")
                {
                    GetDirectories(e.Node, e.Node.Tag.ToString());
                }
            }
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtFile.Text = folderBrowserDialog1.SelectedPath;
                selectednode = "";
                treeView1.Nodes.Clear();
                TreeNode node = new TreeNode();
                node = new TreeNode(Path.GetFileName(txtFile.Text), 1, 1);
                node.Tag = txtFile.Text;
                node.Nodes.Add("");
                treeView1.Nodes.Add(node);
                node.Expand();
                //GetDirectories(node, folderBrowserDialog1.SelectedPath);
            }    
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(treeView1.SelectedNode != null)
            {
                if(treeView1.SelectedNode.ImageIndex == 0)
                {
                    selectednode = treeView1.SelectedNode.Tag.ToString();
                    OpenFile(selectednode);
                }
            }
        }

    }
}
