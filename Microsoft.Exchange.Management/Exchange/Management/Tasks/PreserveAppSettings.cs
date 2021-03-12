using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200062C RID: 1580
	[Cmdlet("Preserve", "AppSettings")]
	[LocDescription(Strings.IDs.PreserveAppSettingsTask)]
	public sealed class PreserveAppSettings : Task
	{
		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x060037D3 RID: 14291 RVA: 0x000E7B98 File Offset: 0x000E5D98
		// (set) Token: 0x060037D4 RID: 14292 RVA: 0x000E7BA0 File Offset: 0x000E5DA0
		[Parameter(Mandatory = true)]
		public string RoleInstallPath
		{
			get
			{
				return this.roleInstallPath;
			}
			set
			{
				this.roleInstallPath = value;
			}
		}

		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x060037D5 RID: 14293 RVA: 0x000E7BA9 File Offset: 0x000E5DA9
		// (set) Token: 0x060037D6 RID: 14294 RVA: 0x000E7BB1 File Offset: 0x000E5DB1
		[Parameter(Mandatory = true)]
		public string ConfigFileName
		{
			get
			{
				return this.configFileName;
			}
			set
			{
				this.configFileName = value;
			}
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x000E7BBC File Offset: 0x000E5DBC
		protected override void InternalProcessRecord()
		{
			string text = Path.Combine(this.roleInstallPath, this.configFileName);
			string sourceFileName = text + ".template";
			if (!File.Exists(text))
			{
				File.Copy(sourceFileName, text);
				return;
			}
			string destFileName = text + ".old";
			string exePath = Path.Combine(this.roleInstallPath, Path.GetFileNameWithoutExtension(text));
			Configuration configuration = ConfigurationManager.OpenExeConfiguration(exePath);
			foreach (string key in PreserveAppSettings.preservedSettingNames)
			{
				if (configuration.AppSettings.Settings[key] != null)
				{
					this.settings[key] = configuration.AppSettings.Settings[key].Value;
				}
			}
			File.Copy(text, destFileName, true);
			File.Copy(sourceFileName, text, true);
			Configuration configuration2 = ConfigurationManager.OpenExeConfiguration(exePath);
			foreach (KeyValuePair<string, string> keyValuePair in this.settings)
			{
				if (!string.IsNullOrEmpty(keyValuePair.Value))
				{
					configuration2.AppSettings.Settings[keyValuePair.Key].Value = keyValuePair.Value;
				}
			}
			configuration2.Save();
		}

		// Token: 0x040025B7 RID: 9655
		private static string[] preservedSettingNames = new string[]
		{
			"MaxIoThreadsPerCPU",
			"ConnectionCacheSize",
			"TemporaryStoragePath",
			"ThrottlingTimeoutInSeconds",
			"MaxConnectionRatePerMinute"
		};

		// Token: 0x040025B8 RID: 9656
		private string roleInstallPath;

		// Token: 0x040025B9 RID: 9657
		private string configFileName;

		// Token: 0x040025BA RID: 9658
		private Dictionary<string, string> settings = new Dictionary<string, string>(PreserveAppSettings.preservedSettingNames.Length);
	}
}
