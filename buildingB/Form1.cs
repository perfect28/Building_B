using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace buildingB
{
    public partial class Form1 : Form
    {

        const int MAXN = 100;
        String path = "";

        int cnt_num_south = 0;
        int[] hash_south = new int[1000];
        Room[] rooms_south = new Room[MAXN];

        int cnt_num_north = 0;
        int[] hash_north = new int[1000];
        Room[] rooms_north = new Room[MAXN];

        public Form1()
        {
            InitializeComponent();

            Init();

            path = Application.StartupPath + "\\south.txt";
            read_data(path,rooms_south,hash_south,ref cnt_num_south);

            path = Application.StartupPath + "\\north.txt";
            read_data(path, rooms_north, hash_north, ref cnt_num_north);
        }

        private void Init()
        {
            for(int i=0;i<1000;i++)
            {
                hash_north[i] = -1;
                hash_south[i] = -1;
            }
        }

        public struct Point
        {
            public int x, y;
        }
        public class Room
        {
            int root_num;

            public int Num
            {
                get { return root_num; }
                set { root_num = value; }
            }

            int step_num;

            public int Step_num
            {
                get { return step_num; }
                set { step_num = value; }
            }

            Point[] path = new Point[MAXN];

            public Point[] Path
            {
                get { return path; }
                set { path = value; }
            }

            public Room(int num)
            {
                this.root_num = num;
            }
           
            public void set_path(int n,Point[] m)
            {
                for(int i=1;i<=n;i++)
                    path[i]=m[i];
            }
        }

        private void read_data(String path,Room[] rooms,int[] hash,ref int cnt_num)
        {    
            //String path = Application.StartupPath + "\\south.txt";
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                //获取房间号
                int num = int.Parse(line);
                rooms[++cnt_num] = new Room(num);
                hash[num] = cnt_num;
                //获取路径个数
                line = sr.ReadLine();
                int n = int.Parse(line);
                rooms[cnt_num].Step_num = n;

                //获取路径
                Point[] road = new Point[MAXN];
                for(int i=1;i<=n;i++)
                {
                    line = sr.ReadLine();
                    string[] messages = line.Split(new char[] {' '});
                    Point now;
                    now.x = int.Parse(messages[0]);
                    now.y = int.Parse(messages[1]);
                    road[i]=now;
                }
                rooms[cnt_num].set_path(n, road);
            }
        }
        
        int get_dis(int x1,int y1,int x2,int y2)
        {
            return (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox.SelectedIndex)
	        {
                case 0:
                    this.pic_dorm.Image = Image.FromFile(Application.StartupPath + "\\picture\\south_Bamboo.jpg");
                    break;
                case 1:
                    this.pic_dorm.Image = Image.FromFile(Application.StartupPath + "\\picture\\south_Begonia.jpg"); 
                    break;
                case 2:
                    this.pic_dorm.Image = Image.FromFile(Application.StartupPath + "\\picture\\south_Clove.jpg"); 
                    break;
		        default:
                    break;
	        }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Graphics g = this.pictureBox1.CreateGraphics();
            Graphics g = Graphics.FromImage(pic_south.Image);
            int num = int.Parse(txt_num_south.Text);
            int id = hash_south[num];

            if (id == -1) 
            {
                MessageBox.Show("抱歉，需要寻找的教室不存在~");
            }
            else
            {
                SolidBrush brush = new SolidBrush(Color.Red);
                float LineWidth = 5.0f;
                Pen pen = new Pen(brush, LineWidth);

                for (int i = 1; i < rooms_south[id].Step_num; i++)
                {
                    int x1 = rooms_south[id].Path[i].x;
                    int y1 = rooms_south[id].Path[i].y;
                    int x2 = rooms_south[id].Path[i + 1].x;
                    int y2 = rooms_south[id].Path[i + 1].y;
                    //g.FillEllipse(myBrush, new Rectangle(x, y, 20, 20));
                    g.DrawLine(pen, x1, y1, x2, y2);//坐标
                    pic_south.Refresh();

                    int len = get_dis(x1, y1, x2, y2);
                    Thread.Sleep((int)(len / 100));
                }
                g.Dispose();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.pic_south.Image = Image.FromFile(Application.StartupPath + "\\picture\\Building_south_small.png");
            this.pic_south.Refresh();
        }



        private void button5_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(pic_north.Image);
            int num = int.Parse(txt_num_north.Text);
            int id = hash_north[num];

            if (id == -1)
            {
                MessageBox.Show("抱歉，需要寻找的教室不存在~");
            }
            else
            {
                SolidBrush brush = new SolidBrush(Color.Blue);
                float LineWidth = 5.0f;
                Pen pen = new Pen(brush, LineWidth);

                for (int i = 1; i < rooms_north[id].Step_num; i++)
                {
                    int x1 = rooms_north[id].Path[i].x;
                    int y1 = rooms_north[id].Path[i].y;
                    int x2 = rooms_north[id].Path[i + 1].x;
                    int y2 = rooms_north[id].Path[i + 1].y;
                    //g.FillEllipse(myBrush, new Rectangle(x, y, 20, 20));

                    g.DrawLine(pen, x1, y1, x2, y2);//坐标
                    pic_north.Refresh();

                    int len = get_dis(x1, y1, x2, y2);
                    Thread.Sleep((int)len / 100);
                }
                g.Dispose();
            }
        }

        private void paint_line()
        {
            throw new NotImplementedException();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.pic_north.Image = Image.FromFile(Application.StartupPath + "\\picture\\Building_north_small.png");
            this.pic_north.Refresh();
        }
    }
}
