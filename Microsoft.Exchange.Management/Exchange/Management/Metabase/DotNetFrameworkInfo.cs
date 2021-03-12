using System;
using System.IO;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x020004BB RID: 1211
	internal static class DotNetFrameworkInfo
	{
		// Token: 0x06002A2E RID: 10798 RVA: 0x000A84E8 File Offset: 0x000A66E8
		static DotNetFrameworkInfo()
		{
			string name = string.Format("Software\\Microsoft\\ASP.NET\\{0}.0", Environment.Version.ToString(3));
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name))
			{
				DotNetFrameworkInfo.aspNetIsapiDllPath = Path.GetFullPath((string)registryKey.GetValue("DllFullPath"));
				DotNetFrameworkInfo.frameworkInstallDir = (string)registryKey.GetValue("Path");
			}
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06002A2F RID: 10799 RVA: 0x000A8578 File Offset: 0x000A6778
		public static string AspNetIsapiDllPath
		{
			get
			{
				return DotNetFrameworkInfo.aspNetIsapiDllPath;
			}
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06002A30 RID: 10800 RVA: 0x000A857F File Offset: 0x000A677F
		public static string FrameworkInstallDir
		{
			get
			{
				return DotNetFrameworkInfo.frameworkInstallDir;
			}
		}

		// Token: 0x04001F85 RID: 8069
		private static string aspNetIsapiDllPath = string.Empty;

		// Token: 0x04001F86 RID: 8070
		private static string frameworkInstallDir = string.Empty;
	}
}
