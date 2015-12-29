using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace duper{
	[RunInstaller(true)]
	public partial class Installer:System.Configuration.Install.Installer{
		public Installer(){
			InitializeComponent();
		}

		public override void Install(IDictionary savedState){
			base.Install(savedState);
			
			//add registry key
			Microsoft.Win32.RegistryKey key;
			key = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey("*").OpenSubKey("shell").CreateSubKey("Backup with Duper").CreateSubKey("command");
			key.SetValue("(Default)", "%WINDIR%\\System32\\duper.exe %1", Microsoft.Win32.RegistryValueKind.String);
			key.Close();
		}
	}
}