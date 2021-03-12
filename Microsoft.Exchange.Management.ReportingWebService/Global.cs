using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Web;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000021 RID: 33
	public class Global : HttpApplication
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00003962 File Offset: 0x00001B62
		static Global()
		{
			AppDomain.CurrentDomain.AssemblyResolve += Global.CurrentDomain_AssemblyResolve;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000039A0 File Offset: 0x00001BA0
		public static bool GetAppSettingAsBool(string key, bool defaultValue)
		{
			string value = ConfigurationManager.AppSettings[key];
			bool result;
			if (!string.IsNullOrEmpty(value) && bool.TryParse(value, out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000039D0 File Offset: 0x00001BD0
		public static int GetAppSettingAsInteger(string key, int defaultValue)
		{
			string text = ConfigurationManager.AppSettings[key];
			int result;
			if (!string.IsNullOrEmpty(text) && int.TryParse(text, out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003A00 File Offset: 0x00001C00
		private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			AssemblyName assemblyName = new AssemblyName(args.Name);
			foreach (string path in Global.BinSearchFolders)
			{
				string text = Path.Combine(path, assemblyName.Name) + ".dll";
				if (File.Exists(text))
				{
					return Assembly.LoadFrom(text);
				}
			}
			return null;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003A68 File Offset: 0x00001C68
		private void Application_Start(object sender, EventArgs e)
		{
			this.InitializeWatsonReporting();
			this.InitializePerformanceCounter();
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003A78 File Offset: 0x00001C78
		private void InitializeWatsonReporting()
		{
			bool appSettingAsBool = Global.GetAppSettingAsBool("WatsonEnabled", true);
			ServiceDiagnostics.InitializeWatsonReporting(appSettingAsBool);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003A98 File Offset: 0x00001C98
		private void InitializePerformanceCounter()
		{
			Globals.InitializeMultiPerfCounterInstance("RWS");
			foreach (ExPerformanceCounter exPerformanceCounter in RwsPerfCounters.AllCounters)
			{
				exPerformanceCounter.RawValue = 0L;
			}
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				RwsPerfCounters.PID.RawValue = (long)currentProcess.Id;
			}
		}

		// Token: 0x0400004C RID: 76
		private const string AppSettingSendWatsonReport = "WatsonEnabled";

		// Token: 0x0400004D RID: 77
		private static readonly string[] BinSearchFolders = ConfigurationManager.AppSettings["BinSearchFolders"].Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
	}
}
