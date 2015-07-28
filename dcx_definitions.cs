using System.Collections;
using System;
using System.IO.Ports;

namespace DCX_Serial_Test
{
	
	public class dcx
	{
		//public SerialPort controlport;

		public static byte[] init = {0xF0,0x00,0x20,0x32,0x00,0x0E,0x3F,0x04,0x00,0xF7};
		//Kanalauswahl (channel)
		public static byte setup 		= 0x00;		// Setup
		public static byte ch_a 		= 0x01;		// Input A
		public static byte ch_b 		= 0x02;		// Input B
		public static byte ch_c 		= 0x03;		// Input C
		public static byte ch_sum 		= 0x04;		// Input Summe
		public static byte out_1 		= 0x05;		// Ausgang 1
		public static byte out_2 		= 0x06;		// Ausgang 2
		public static byte out_3 		= 0x07;		// Ausgang 3
		public static byte out_4 		= 0x08;		// Ausgang 4
		public static byte out_5 		= 0x09;		// Ausgang 5
		public static byte out_6 		= 0x0A;		// Ausgang 6

		//Setup-Parameter (dcx_setup)
		public static byte setup_sumtype 	= 0x02;		// Summentyp; Daten: 0-6 = A, B, C, A+B, A+C, B+C
		public static byte setup_sum_on 	= 0x03;		// Summe An/Aus
		public static byte setup_c_line		= 0x04;		// Eingang C = Line / Mic
		public static byte setup_out_conf 	= 0x05;		// Ausgangs-Config; Daten: 0-3 = MONO, LMHLMH, LLMMHH, LHLHLH
		public static byte setup_st_link	= 0x06;		// Stereo-Link An/Aus
		public static byte setup_in_link  	= 0x07;		// Input-Link; Daten: off, a+b, a+b+c, a+b+c+sum
		public static byte setup_delay_link = 0x08;		// Delay-Link An/Aus	
		public static byte setup_xover_link	= 0x09;		// Crossover-Link An/Aus
		public static byte setup_delay_corr	= 0x0A;		// Input Delay An/Aus
		public static byte setup_air_temp	= 0x0B;		// Temperatur; Daten: -20° - +50° = 0...70
		public static byte setup_delay_unit	= 0x14;		// Delay-Maßeinheit: mm / inch
		public static byte setup_mute_outs	= 0x15;		// Ausgänge stummschalten beim Start
		public static byte setup_sum_gain_a	= 0x16;		// Summen-Gain Kanal A; Daten 0-300 = -15 - +15
		public static byte setup_sum_gain_b	= 0x17;		// Summen-Gain Kanal B; Daten 0-300 = -15 - +15
		public static byte setup_sum_gain_c	= 0x18;		// Summen-Gain Kanal C; Daten 0-300 = -15 - +15

		//In/Out-Parameter (dcx_ch_a - dcx_ch_c, dcx_ch_sum, dcx_out_1 - dcx_out_6)
		public static byte ch_gain		= 0x02;		// Gain; Daten: 0-300 = -15 - +15
		public static byte ch_mute		= 0x03;		// Stummschalten
		public static byte ch_delay_on	= 0x04;		// Delay An/Aus
		public static byte ch_delay		= 0x05;		// Delay; Daten: 0-4000 (0-200m step 5cm) 
		public static byte ch_eq_on		= 0x06;		// EQ An/Aus
		public static byte ch_eq_num	= 0x07;		// EQ Nummer; Daten: 0-9
		public static byte ch_eq_index	= 0x08;		// ?
		//Dynamics
		public static byte ch_dyn_att	= 0x09;		// Dynamics Attack; Daten: 0-109 = 0-200mS
		public static byte ch_dyn_rel	= 0x0A;		// Dynamics Release; Daten: 0-251 = 20-4000mS
		public static byte ch_dyn_ratio	= 0x0B;		// Dynamics Ratio; Daten: 0-15 = 1:1,1 - 1:inf
		public static byte ch_dyn_thre	= 0x0C;		// Dynamics Threshold; Daten: 0-600 = -60 - 0dB
		public static byte ch_dyn_on	= 0x0D;		// Dynamics An/Aus
		public static byte ch_dyn_freq	= 0x0E;		// Dynamics Frequenz; Daten: 0-320 = 20Hz-20kHz
		public static byte ch_dyn_q		= 0x0F;		// Dynamics Q; Daten: 0-40 = 0,1 - 10
		public static byte ch_dyn_gain	= 0x10;		// Dynamics Gain; Daten: 0-300 = -15dB - +15dB
		public static byte ch_dyn_type	= 0x11;		// Dynamics Typ; Daten:  (0:low shelve, 1:bandpass, 2:hi shelve) 
		public static byte ch_dyn_slope = 0x12;		// Dynamics Slope; Daten: 0 = 6dB, 1 = 12dB
		//Equalizer
		public static byte ch_eq1_freq	= 0x13;		// EQ #1 Frequenz; Daten 0-320 = 20Hz-20kHz
		public static byte ch_eq1_q		= 0x14;		// EQ #1 Q; Daten: 0-40 = 0,1-10
		public static byte ch_eq1_gain	= 0x15;		// EQ #1 Gain; Daten: 0-300 = -15dB - +15dB
		public static byte ch_eq1_type  = 0x16;		// EQ #1 Typ; Daten:  (0:low shelve,1:bandpass,2:hi shelve)
		public static byte ch_eq1_slope = 0x17;		// EQ #1 Slope; Daten: 0 = 6dB, 1 = 12dB

		public static byte ch_eq2_freq	= 0x18;		// EQ #2 Frequenz; Daten 0-320 = 20Hz-20kHz
		public static byte ch_eq2_q		= 0x19;		// EQ #2 Q; Daten: 0-40 = 0,1-10
		public static byte ch_eq2_gain	= 0x1A;		// EQ #2 Gain; Daten: 0-300 = -15dB - +15dB
		public static byte ch_eq2_type  = 0x1B;		// EQ #2 Typ; Daten:  (0:low shelve,1:bandpass,2:hi shelve)
		public static byte ch_eq2_slope = 0x1C;		// EQ #2 Slope; Daten: 0 = 6dB, 1 = 12dB

		public static byte ch_eq3_freq	= 0x1D;		// EQ #3 Frequenz; Daten 0-320 = 20Hz-20kHz
		public static byte ch_eq3_q		= 0x1E;		// EQ #3 Q; Daten: 0-40 = 0,1-10
		public static byte ch_eq3_gain	= 0x1F;		// EQ #3 Gain; Daten: 0-300 = -15dB - +15dB
		public static byte ch_eq3_type  = 0x20;		// EQ #3 Typ; Daten:  (0:low shelve,1:bandpass,2:hi shelve)
		public static byte ch_eq3_slope = 0x21;		// EQ #3 Slope; Daten: 0 = 6dB, 1 = 12dB

		public static byte ch_eq4_freq	= 0x22;		// EQ #4 Frequenz; Daten 0-320 = 20Hz-20kHz
		public static byte ch_eq4_q		= 0x23;		// EQ #4 Q; Daten: 0-40 = 0,1-10
		public static byte ch_eq4_gain	= 0x24;		// EQ #4 Gain; Daten: 0-300 = -15dB - +15dB
		public static byte ch_eq4_type  = 0x25;		// EQ #4 Typ; Daten:  (0:low shelve,1:bandpass,2:hi shelve)
		public static byte ch_eq4_slope = 0x26;		// EQ #4 Slope; Daten: 0 = 6dB, 1 = 12dB

		public static byte ch_eq5_freq	= 0x27;		// EQ #5 Frequenz; Daten 0-320 = 20Hz-20kHz
		public static byte ch_eq5_q		= 0x28;		// EQ #5 Q; Daten: 0-40 = 0,1-10
		public static byte ch_eq5_gain	= 0x29;		// EQ #5 Gain; Daten: 0-300 = -15dB - +15dB
		public static byte ch_eq5_type  = 0x2A;		// EQ #5 Typ; Daten:  (0:low shelve,1:bandpass,2:hi shelve)
		public static byte ch_eq5_slope = 0x2B;		// EQ #5 Slope; Daten: 0 = 6dB, 1 = 12dB

		public static byte ch_eq6_freq	= 0x2C;		// EQ #6 Frequenz; Daten 0-320 = 20Hz-20kHz
		public static byte ch_eq6_q		= 0x2D;		// EQ #6 Q; Daten: 0-40 = 0,1-10
		public static byte ch_eq6_gain	= 0x2E;		// EQ #6 Gain; Daten: 0-300 = -15dB - +15dB
		public static byte ch_eq6_type  = 0x2F;		// EQ #6 Typ; Daten:  (0:low shelve,1:bandpass,2:hi shelve)
		public static byte ch_eq6_slope = 0x30;		// EQ #6 Slope; Daten: 0 = 6dB, 1 = 12dB

		public static byte ch_eq7_freq	= 0x31;		// EQ #7 Frequenz; Daten 0-320 = 20Hz-20kHz
		public static byte ch_eq7_q		= 0x32;		// EQ #7 Q; Daten: 0-40 = 0,1-10
		public static byte ch_eq7_gain	= 0x33;		// EQ #7 Gain; Daten: 0-300 = -15dB - +15dB
		public static byte ch_eq7_type  = 0x34;		// EQ #7 Typ; Daten:  (0:low shelve,1:bandpass,2:hi shelve)
		public static byte ch_eq7_slope = 0x35;		// EQ #7 Slope; Daten: 0 = 6dB, 1 = 12dB

		public static byte ch_eq8_freq	= 0x36;		// EQ #8 Frequenz; Daten 0-320 = 20Hz-20kHz
		public static byte ch_eq8_q		= 0x37;		// EQ #8 Q; Daten: 0-40 = 0,1-10
		public static byte ch_eq8_gain	= 0x38;		// EQ #8 Gain; Daten: 0-300 = -15dB - +15dB
		public static byte ch_eq8_type  = 0x39;		// EQ #8 Typ; Daten:  (0:low shelve,1:bandpass,2:hi shelve)
		public static byte ch_eq8_slope = 0x3A;		// EQ #8 Slope; Daten: 0 = 6dB, 1 = 12dB

		public static byte ch_eq9_freq	= 0x3B;		// EQ #1 Frequenz; Daten 0-320 = 20Hz-20kHz
		public static byte ch_eq9_q		= 0x3C;		// EQ #1 Q; Daten: 0-40 = 0,1-10
		public static byte ch_eq9_gain	= 0x3D;		// EQ #1 Gain; Daten: 0-300 = -15dB - +15dB
		public static byte ch_eq9_type  = 0x3E;		// EQ #1 Typ; Daten:  (0:low shelve,1:bandpass,2:hi shelve)
		public static byte ch_eq9_slope = 0x3F;		// EQ #1 Slope; Daten: 0 = 6dB, 1 = 12dB

		//Output-Spezifische Parameter
		public static byte ch_out_name 		= 0x40;	// Kanal-Name; Daten: 0-1B = Full Range - Canter High
		public static byte ch_out_source	= 0x41; // Quelle; Daten: 0-3 = A,B,C,Sum
		public static byte ch_out_hp_type 	= 0x42; // Filter-Typ; Daten: 0-10 
														// 0=off,  1=but6,  2=but12, 3=bes12
														// 4=lr12, 5=but18, 6=but24, 7=bes24
														// 8=lr24, 9=but48, 10=lr48
		public static byte ch_out_hp_freq	= 0x43; // Filter-Frequenz; Daten: 0-320 = 20Hz-20kHz
		public static byte ch_out_lp_type	= 0x44; // s.o.
		public static byte ch_out_lp_freq	= 0x45; // s.o.
		public static byte ch_out_lim_on	= 0x46; // Limiter An/Aus
		public static byte ch_out_lim_thre	= 0x47;	// Limiter-Threshold; Daten: 0-240 = -24dB - 0dB
		public static byte ch_out_lim_rel	= 0x48;	// Limiter-Release; Daten: 0-251 = 20 - 4000mS
		public static byte ch_out_polarity	= 0x49;	// Polarität Normal/Invertiert
		public static byte ch_out_phase		= 0x4A;	// Phase; Daten 0-36 = 0-180°
		public static byte ch_out_s_delay	= 0x4B;	// Short Delay; Daten: 0-2000 = 0-4000mm

		//Methoden
		static SerialPort controlport = new SerialPort ("COM1", 38400, Parity.None,8, StopBits.One); //Debug, fester Comport Macbook

		public static void opencom(string port)
		{
		dcx.controlport.PortName = port;
		dcx.controlport.Open ();
		dcx.controlport.Write (init, 0, init.Length);
		}


		public static void send(byte deviceid, byte number, byte channel, byte parameter, int value)
		{
			int vallow;
			int valhigh;
			byte bytelow;
			byte bytehigh;
			int[] tmphigh = new int[8];
			int[] tmplow = new int[8];
			// Sortieren der Wert-Bytes in 2 Arrays zu je 7 Bit
			BitArray temp = new BitArray (new int[] {value});
			int i=0;
			while(i<=6)
			{
				if(temp.Get(i) == true)
				{
					tmplow [6-i] = 1;	
				}
				else if(temp.Get(i) == false)
				{
					tmplow [6-i] = 0;
				}	
				//Console.Write (tmplow[i].ToString ());
				i++;
			}
			//Console.WriteLine("");
			i = 7;
			while(i <= 13)
			{
				if(temp.Get(i) == true)
				{
					tmphigh [i-7] = 1;	
				}
				else if(temp.Get(i) == false)
				{
					tmphigh [i-7] = 0;
				} 	
				//Console.Write (tmphigh[i-7].ToString ());
				i++;
			}
			//Console.WriteLine ("");

			// Konvertieren von Binär in Int
			vallow = tmplow [6] + tmplow [5] * 2 + tmplow [4] * 4 + tmplow [3] * 8 + tmplow [2] * 16 + tmplow [1] * 32 + tmplow [0] * 64;
			valhigh = tmphigh [0] + tmphigh [1] * 2 + tmphigh [2] * 4 + tmphigh [3] * 8 + tmphigh [4] * 16 + tmphigh [5] * 32 + tmphigh [6] * 64;
			// Konvertieren von Int in Bytes 
			bytelow = Convert.ToByte(vallow);
			bytehigh = Convert.ToByte (valhigh);
			// Zusammenstellen der Kommandokette
			byte[] command = 	{0xF0,0x00,0x20,0x32,deviceid,0x0E,0x20,number,channel,parameter,bytehigh,bytelow,0xF7};	
			// Kommando senden
			dcx.controlport.Write (command, 0, command.Length);
		}

		public static void closeport()
		{
		dcx.controlport.Close ();
		}	
		
		public static int get_f(int freq)
		{
			double temp = (320*Math.Log((double)freq))/(3*(Math.Log(2)+Math.Log(5)))-(320*(2*Math.Log(2)+Math.Log(5)))/(3*(Math.Log(2)+Math.Log(5)));
			return (int)Math.Round(temp);
		}
		public static int get_q(double q)
		{
			double temp = (20*Math.Log(10*q))/(Math.Log(2)+Math.Log(5));
			return (int)Math.Round(temp);
		}
		public static int get_g(double gain)
		{
			return (int)Math.Round((gain+15)*10);
		}
	}
}
