using System.IO;
using System.Windows.Forms;

namespace Adapter
{
    public interface DataGrid
    {
        void SaveTo(string fileName);
    }

    public class SaveToBinaryAdapter : DataGrid
    {
        private DataGridView dg;

        public SaveToBinaryAdapter(DataGridView data)
        {
            dg = data;
        }

        public void SaveTo(string fileName)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.OpenOrCreate)))
            {
                for (int i = 0; i < dg.RowCount; i++)
                {
                    for (int l = 0; l < dg.ColumnCount; l++)
                    {
                        if (dg[l, i].Value != null)
                        {
                            writer.Write(dg[l, i].Value.ToString() + "\t");
                        }
                    }
                    writer.Write("\n");
                }
            }
        }
    }

    public class SaveToTextAdapter : DataGrid
    {
        private DataGridView dg;

        public SaveToTextAdapter(DataGridView data)
        {
            dg = data;
        }

        public void SaveTo(string fileName)
        {
            StreamWriter writer = new StreamWriter(fileName);

            for (int i = 0; i < dg.RowCount; i++)
            {
                for (int l = 0; l < dg.ColumnCount; l++)
                {
                    if (dg[l, i].Value != null)
                    {
                        writer.Write(dg[l, i].Value.ToString() + "\t");
                    }
                }
                writer.WriteLine();
            }
            writer.Close();
        }
    }

    public interface Filee
    {
        void SaveToGrid(string fileName, DataGridView dg);
    }

    public class BinaryAdapter : Filee
    {
       public void SaveToGrid(string fileName, DataGridView dg)
       {
            using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                string line;
                string zap = "";

                int r = 0;
                int c = 0;

                while (reader.PeekChar() > -1)
                {
                    dg.Rows.Add();
                    line = reader.ReadString();
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i].ToString() == "\n")
                        {
                            r++;
                            c = 0;
                        }
                        else if (line[i].ToString() != "\t")
                        {
                            zap += line[i];
                        }
                        else
                        {
                            dg[c, r].Value = zap;
                            c++;
                            zap = "";
                        }
                    }
                }
                for (int i = dg.RowCount-2; i > 0; i--)
                {
                    if (dg[0, i].Value == null && dg[1, i].Value == null
                        && dg[2, i].Value == null && dg[3, i].Value == null
                        && dg[4, i].Value == null)
                        dg.Rows.RemoveAt(i);
                }
            }
        }
    }

    public class TextAdapter : Filee
    {
        public void SaveToGrid(string fileName, DataGridView dg)
        {
            string line;
            string zap = "";

            int r = 0;
            int c = 0;

            StreamReader sr = new StreamReader(fileName);
            line = sr.ReadLine();

            while (line != null)
            {
                dg.Rows.Add();
                for (int i = 0; i < line.Length; i++)
                {
                    if(line[i].ToString() !="\t")
                    {
                        zap += line[i];
                    }
                    else 
                    {
                        dg[c, r].Value = zap;
                       c++;
                        zap = "";
                    }
                }
                line = sr.ReadLine();
                r++;
                c = 0;
            }
            sr.Close();
        }
    }
}
