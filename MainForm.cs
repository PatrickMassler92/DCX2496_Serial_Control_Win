/*
 * Created by SharpDevelop.
 * User: Patrick
 * Date: 27.07.2015
 * Time: 16:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using DCX_Serial_Test;

namespace DCX2496_Serial_Control_Win
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		public string readfile;
		//public int freq1, freq2, freq3, freq4, freq5, freq6, freq7, freq8, freq9;
	
		void Button1Click(object sender, EventArgs e)
		{
			int val1 = (Convert.ToInt16 (textBox1.Text) + 15) * 10;
			int val2 = (Convert.ToInt16 (textBox2.Text) + 15) * 10;
			int val3 = (Convert.ToInt16 (textBox3.Text) + 15) * 10;
			int val4 = (Convert.ToInt16 (textBox4.Text) + 15) * 10;
			int val5 = (Convert.ToInt16 (textBox5.Text) + 15) * 10;
			int val6 = (Convert.ToInt16 (textBox6.Text) + 15) * 10;
			dcx.opencom ("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send (0x00, 0x01, dcx.out_1, dcx.ch_gain, val1);
			dcx.send (0x00, 0x01, dcx.out_2, dcx.ch_gain, val2);
			dcx.send (0x00, 0x01, dcx.out_3, dcx.ch_gain, val3);
			dcx.send (0x00, 0x01, dcx.out_4, dcx.ch_gain, val4);
			dcx.send (0x00, 0x01, dcx.out_5, dcx.ch_gain, val5);
			dcx.send (0x00, 0x01, dcx.out_6, dcx.ch_gain, val6);
			dcx.closeport ();
		}
		void Button2Click(object sender, EventArgs e)
		{
			dcx.opencom ("COM" + (comboBox1.SelectedIndex+1).ToString());
			if (button2.Text == "MUTED!"){dcx.send (0x00, 0x01, dcx.out_1, dcx.ch_mute, 0); button2.Text = "Mute";}
			else {dcx.send (0x00, 0x01, dcx.out_1, dcx.ch_mute, 1); button2.Text = "MUTED!";}
			dcx.closeport ();
		}
		void Button3Click(object sender, EventArgs e)
		{
			dcx.opencom ("COM" + comboBox1.SelectedIndex.ToString());
			if (button3.Text == "MUTED!"){dcx.send (0x00, 0x01, dcx.out_2, dcx.ch_mute, 0); button3.Text = "Mute";}
			else {dcx.send (0x00, 0x01, dcx.out_2, dcx.ch_mute, 1); button3.Text = "MUTED!";}
			dcx.closeport ();
		}
		void Button4Click(object sender, EventArgs e)
		{
			dcx.opencom ("COM" + comboBox1.SelectedIndex.ToString());
			if (button4.Text == "MUTED!"){dcx.send (0x00, 0x01, dcx.out_3, dcx.ch_mute, 0); button4.Text = "Mute";}
			else {dcx.send (0x00, 0x01, dcx.out_3, dcx.ch_mute, 1); button4.Text = "MUTED!";}
			dcx.closeport ();
		}
		void Button5Click(object sender, EventArgs e)
		{
			dcx.opencom ("COM" + comboBox1.SelectedIndex.ToString());
			if (button5.Text == "MUTED!"){dcx.send (0x00, 0x01, dcx.out_4, dcx.ch_mute, 0); button5.Text = "Mute";}
			else {dcx.send (0x00, 0x01, dcx.out_4, dcx.ch_mute, 1); button5.Text = "MUTED!";}
			dcx.closeport ();
		}
		void Button6Click(object sender, EventArgs e)
		{
			dcx.opencom ("COM" + comboBox1.SelectedIndex.ToString());
			if (button6.Text == "MUTED!"){dcx.send (0x00, 0x01, dcx.out_5, dcx.ch_mute, 0); button6.Text = "Mute";}
			else {dcx.send (0x00, 0x01, dcx.out_5, dcx.ch_mute, 1); button6.Text = "MUTED!";}
			dcx.closeport ();
		}
		void Button7Click(object sender, EventArgs e)
		{
			dcx.opencom ("COM" + comboBox1.SelectedIndex.ToString());
			if (button7.Text == "MUTED!"){dcx.send (0x00, 0x01, dcx.out_6, dcx.ch_mute, 0); button7.Text = "Mute";}
			else {dcx.send (0x00, 0x01, dcx.out_6, dcx.ch_mute, 1); button7.Text = "MUTED!";}
			dcx.closeport ();
		}
		
		protected void send_gains (object sender, EventArgs e)
		{
			
		}
		void OpenFileDialog1FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			
			StreamReader s = new StreamReader(openFileDialog1.FileName.ToString());
			while(s.EndOfStream==false)
			{
			readfile = s.ReadLine();
			if(readfile.Contains("Fc")==true)
			   {
				label7.Text += readfile.TrimEnd(' ').TrimStart(' ') + "\r\n";
			   }
			}
			setlabels();
			
		}
		void Button8Click(object sender, EventArgs e)
		{
			if(File.Exists("eq.txt")==true)
			   {
			   		openFileDialog1.FileName = "eq.txt";
			   		StreamReader s = new StreamReader(openFileDialog1.FileName.ToString());
					while(s.EndOfStream==false)
					{
						readfile = s.ReadLine();
						if(readfile.Contains("Fc")==true)
			   			{
							label7.Text += readfile.TrimEnd(' ').TrimStart(' ') + "\r\n";
			   			}
					}
					setlabels();
			   }
			else
				{
				openFileDialog1.ShowDialog();
				}
		}
		public void setlabels()
		{
			int length = label7.Text.Length;
			if(63<length)
			{
				textBox7.Text = label7.Text.Substring(28,6).Trim(' ').Replace(".","");
				textBox8.Text = label7.Text.Substring(44,6).Trim(' ');
				textBox9.Text = label7.Text.Substring(57,6).Trim(' ');
			}
			if(127<length)
			{
				textBox12.Text = label7.Text.Substring(92,6).Trim(' ').Replace(".","");
				textBox11.Text = label7.Text.Substring(108,6).Trim(' ');
				textBox10.Text = label7.Text.Substring(121,6).Trim(' ');
			}
			if(191<length)
			{
				textBox15.Text = label7.Text.Substring(156,6).Trim(' ').Replace(".","");
				textBox14.Text = label7.Text.Substring(172,6).Trim(' ');
				textBox13.Text = label7.Text.Substring(185,6).Trim(' ');
			}
			if(255<length)
			{
				textBox18.Text = label7.Text.Substring(220,6).Trim(' ').Replace(".","");
				textBox17.Text = label7.Text.Substring(236,6).Trim(' ');
				textBox16.Text = label7.Text.Substring(249,6).Trim(' ');
			}
			if(319<length)
			{
				textBox21.Text = label7.Text.Substring(284,6).Trim(' ').Replace(".","");
				textBox20.Text = label7.Text.Substring(300,6).Trim(' ');
				textBox19.Text = label7.Text.Substring(313,6).Trim(' ');
			}
			if(383<length)
			{
				textBox24.Text = label7.Text.Substring(348,6).Trim(' ').Replace(".","");
				textBox23.Text = label7.Text.Substring(364,6).Trim(' ');
				textBox22.Text = label7.Text.Substring(377,6).Trim(' ');
			}
			if(447<length)
			{
				textBox27.Text = label7.Text.Substring(412,6).Trim(' ').Replace(".","");
				textBox26.Text = label7.Text.Substring(428,6).Trim(' ');
				textBox25.Text = label7.Text.Substring(441,6).Trim(' ');
			}
			if(511<length)
			{
				textBox30.Text = label7.Text.Substring(476,6).Trim(' ').Replace(".","");
				textBox29.Text = label7.Text.Substring(492,6).Trim(' ');
				textBox28.Text = label7.Text.Substring(505,6).Trim(' ');
			}
			if(575<length)
			{
				textBox33.Text = label7.Text.Substring(540,6).Trim(' ').Replace(".","");
				textBox32.Text = label7.Text.Substring(556,6).Trim(' ');
				textBox31.Text = label7.Text.Substring(569,6).Trim(' ');
			}
		}
		void Button9Click(object sender, EventArgs e)
		{
			byte output_temp = dcx.out_1;
			//Ausgang bestimmen
			switch(comboBox2.Text)
			{
				case "1": output_temp = dcx.out_1;break;
				case "2": output_temp = dcx.out_2;break;
				case "3": output_temp = dcx.out_3;break;
				case "4": output_temp = dcx.out_4;break;
				case "5": output_temp = dcx.out_5;break;
				case "6": output_temp = dcx.out_6;break;
			}
			
			//Gewählte Com öffnen
			
			//EQ1
			statusStrip1.Text = ("Sende EQ 1");
			MainForm.ActiveForm.Refresh();
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq1_freq, dcx.get_f(Convert.ToInt16(textBox7.Text)));
			dcx.closeport();
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq1_gain, dcx.get_g(Convert.ToDouble(textBox8.Text)));
			dcx.closeport();
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq1_q,    dcx.get_q(Convert.ToDouble(textBox9.Text)));
			dcx.closeport();
			System.Threading.Thread.Sleep(100);
			//EQ2
			statusStrip1.Text = ("Sende EQ 2");
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq2_freq, dcx.get_f(Convert.ToInt16(textBox12.Text)));
			dcx.closeport();
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq2_gain, dcx.get_g(Convert.ToDouble(textBox11.Text)));
			dcx.closeport();
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq2_q,    dcx.get_q(Convert.ToDouble(textBox10.Text)));
			dcx.closeport();
			System.Threading.Thread.Sleep(100);
			//EQ3
			statusStrip1.Text = ("Sende EQ 3");
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq3_freq, dcx.get_f(Convert.ToInt16(textBox15.Text)));
			dcx.closeport();
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq3_gain, dcx.get_g(Convert.ToDouble(textBox14.Text)));
			dcx.closeport();
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq3_q,    dcx.get_q(Convert.ToDouble(textBox13.Text)));
			dcx.closeport();
			System.Threading.Thread.Sleep(100);
			//EQ4
			statusStrip1.Text = ("Sende EQ 4");
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq4_freq, dcx.get_f(Convert.ToInt16(textBox18.Text)));
			dcx.closeport();
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq4_gain, dcx.get_g(Convert.ToDouble(textBox17.Text)));
			dcx.closeport();
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq4_q,    dcx.get_q(Convert.ToDouble(textBox16.Text)));
			dcx.closeport();
			System.Threading.Thread.Sleep(100);
			//EQ5
			statusStrip1.Text = ("Sende EQ 5");
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq5_freq, dcx.get_f(Convert.ToInt16(textBox21.Text)));
			dcx.closeport();
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq5_gain, dcx.get_g(Convert.ToDouble(textBox20.Text)));
			dcx.closeport();
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq5_q,    dcx.get_q(Convert.ToDouble(textBox19.Text)));
			dcx.closeport();
			System.Threading.Thread.Sleep(100);
			//EQ6
			statusStrip1.Text = ("Sende EQ 6");
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq6_freq, dcx.get_f(Convert.ToInt16(textBox24.Text)));
			dcx.closeport();
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq6_gain, dcx.get_g(Convert.ToDouble(textBox23.Text)));
			dcx.closeport();
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq6_q,    dcx.get_q(Convert.ToDouble(textBox22.Text)));
			dcx.closeport();
			System.Threading.Thread.Sleep(100);
			//EQ7
			statusStrip1.Text = ("Sende EQ 7");
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq7_freq, dcx.get_f(Convert.ToInt16(textBox27.Text)));
			dcx.closeport();
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq7_gain, dcx.get_g(Convert.ToDouble(textBox26.Text)));
			dcx.closeport();
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq7_q,    dcx.get_q(Convert.ToDouble(textBox25.Text)));
			dcx.closeport();
			System.Threading.Thread.Sleep(100);
			//EQ8
			statusStrip1.Text = ("Sende EQ 8");
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq8_freq, dcx.get_f(Convert.ToInt16(textBox30.Text)));
			dcx.closeport();
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq8_gain, dcx.get_g(Convert.ToDouble(textBox29.Text)));
			dcx.closeport();
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq8_q,    dcx.get_q(Convert.ToDouble(textBox28.Text)));
			dcx.closeport();
			System.Threading.Thread.Sleep(100);
			//EQ9
			statusStrip1.Text = ("Sende EQ 9");
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq9_freq, dcx.get_f(Convert.ToInt16(textBox33.Text)));
			dcx.closeport();
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq9_gain, dcx.get_g(Convert.ToDouble(textBox32.Text)));
			dcx.closeport();
			dcx.opencom("COM" + (comboBox1.SelectedIndex+1).ToString());
			dcx.send(0x00, 0x01, output_temp, dcx.ch_eq9_q,    dcx.get_q(Convert.ToDouble(textBox31.Text)));
			dcx.closeport();
			statusStrip1.Text = "Bereit";
		}
		
	}
}
