using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace duper{
	class Duper{

		private string cwd, filename;
		private List<string> currentDups;

		public Duper(string cwd, string filename){
			this.cwd = cwd;
			this.filename = filename;
			this.currentDups = getCurrentDups();
		}

		public void dupe(){
			bool has1 = currentDups.Contains(filename + ".001")
				,has2 = currentDups.Contains(filename + ".002")
				,has3 = currentDups.Contains(filename + ".003")
				,has4 = currentDups.Contains(filename + ".004")
				,has5 = currentDups.Contains(filename + ".005");

			if(has5 && currentDups.Count == 5) File.Delete(cwd + "\\" + currentDups[4]);
			if(has4) File.Copy(cwd + "\\" + filename + ".004", cwd + "\\" + filename + ".005", true);
			if(has3) File.Copy(cwd + "\\" + filename + ".003", cwd + "\\" + filename + ".004", true);
			if(has2) File.Copy(cwd + "\\" + filename + ".002", cwd + "\\" + filename + ".003", true);
			if(has1) File.Copy(cwd + "\\" + filename + ".001", cwd + "\\" + filename + ".002", true);
			File.Copy(cwd + "\\" + filename, cwd + "\\" + filename + ".001", true);
		}

		private List<string> getCurrentDups(){
			List<string> currentDups = new List<string>();

			foreach(string filepath in Directory.GetFiles(cwd).OrderBy(f => f)){
				string _filename = Path.GetFileName(filepath);
				if(_filename.Contains(filename) && _filename != filename) currentDups.Add(_filename);
			}

			return currentDups;
		}

    }
}