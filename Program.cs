using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Windows.Forms;

namespace duper{
	class Program{
		
		public static void Main(string[] args){
			string parentProcessName = getParentProcessName();
			string outputMethod = parentProcessName == "cmd" || parentProcessName == "powershell" || parentProcessName == "devenv" ? "console" : "msgbox";

			if(args.Length != 1){
				if(outputMethod == "console"){
					Console.WriteLine("Invalid arguments");
					Console.Read();
				}
				else MessageBox.Show("Invalid arguments");
				return;
			}

			string cwd = Path.GetDirectoryName(args[0]) != String.Empty ? Path.GetDirectoryName(args[0]) : Directory.GetCurrentDirectory();
			string filename = Path.GetFileName(args[0]);

			Duper duper = new Duper(cwd, filename);
			duper.dupe();
        }

		private static string getParentProcessName(){
			int myId = Process.GetCurrentProcess().Id;
			string query = string.Format("SELECT ParentProcessId FROM Win32_Process WHERE ProcessId = {0}", myId);
			ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", query);
			ManagementObjectCollection.ManagementObjectEnumerator results = searcher.Get().GetEnumerator();
			results.MoveNext();
			ManagementObject queryObj = (ManagementObject)results.Current;
			uint parentId = (uint)queryObj["ParentProcessId"];
			Process parent = Process.GetProcessById((int)parentId);

			return parent.ProcessName.ToLower();
		}
		
	}
}