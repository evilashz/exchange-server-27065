using System;
using System.IO;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x020002EF RID: 751
	internal sealed class AdamServiceSettings
	{
		// Token: 0x060019C7 RID: 6599 RVA: 0x00072DB0 File Offset: 0x00070FB0
		public AdamServiceSettings(string instanceName, string dataFilesPath, string logFilesPath, int ldapPort, int sslPort)
		{
			this.instanceName = instanceName;
			this.dataFilesPath = dataFilesPath;
			this.logFilesPath = logFilesPath;
			this.ldapPort = ldapPort;
			this.sslPort = sslPort;
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x00072DDD File Offset: 0x00070FDD
		private AdamServiceSettings()
		{
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x060019C9 RID: 6601 RVA: 0x00072DE5 File Offset: 0x00070FE5
		public string InstanceName
		{
			get
			{
				return this.instanceName;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x060019CA RID: 6602 RVA: 0x00072DED File Offset: 0x00070FED
		public string DataFilesPath
		{
			get
			{
				return this.dataFilesPath;
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x060019CB RID: 6603 RVA: 0x00072DF5 File Offset: 0x00070FF5
		public string LogFilesPath
		{
			get
			{
				return this.logFilesPath;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x060019CC RID: 6604 RVA: 0x00072DFD File Offset: 0x00070FFD
		public int LdapPort
		{
			get
			{
				return this.ldapPort;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x060019CD RID: 6605 RVA: 0x00072E05 File Offset: 0x00071005
		public int SslPort
		{
			get
			{
				return this.sslPort;
			}
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x00072E10 File Offset: 0x00071010
		public static bool GetSettingsExist(string instanceName)
		{
			bool result;
			using (RegistryKey registryKey = AdamServiceSettings.GetAdamServiceSubKey(instanceName).Open())
			{
				result = (null != registryKey);
			}
			return result;
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x00072E50 File Offset: 0x00071050
		public static AdamServiceSettings GetFromRegistry(string instanceName)
		{
			AdamServiceSettings result;
			using (RegistryKey registryKey = AdamServiceSettings.GetAdamServiceSubKey(instanceName).Open())
			{
				string text = registryKey.GetValue("DataFilesPath") as string;
				string text2 = registryKey.GetValue("LogFilesPath") as string;
				int num = (int)registryKey.GetValue("LdapPort");
				int num2 = (int)registryKey.GetValue("SslPort");
				result = new AdamServiceSettings(instanceName, text, text2, num, num2);
			}
			return result;
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x00072EDC File Offset: 0x000710DC
		public static void DeleteFromRegistry(string instanceName)
		{
			RegistrySubKey adamServiceSubKey = AdamServiceSettings.GetAdamServiceSubKey(instanceName);
			adamServiceSubKey.DeleteTreeIfExist();
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\EdgeTransportRole\\AdamSettings"))
			{
				if (registryKey != null && registryKey.SubKeyCount == 0)
				{
					Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\EdgeTransportRole\\AdamSettings");
				}
			}
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x00072F3C File Offset: 0x0007113C
		public void SaveToRegistry()
		{
			RegistrySubKey adamServiceSubKey = AdamServiceSettings.GetAdamServiceSubKey(this.InstanceName);
			adamServiceSubKey.DeleteTreeIfExist();
			using (RegistryKey registryKey = adamServiceSubKey.Create())
			{
				registryKey.SetValue("DataFilesPath", this.DataFilesPath);
				registryKey.SetValue("LogFilesPath", this.LogFilesPath);
				registryKey.SetValue("LdapPort", this.LdapPort);
				registryKey.SetValue("SslPort", this.SslPort);
			}
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x00072FCC File Offset: 0x000711CC
		private static RegistrySubKey GetAdamServiceSubKey(string instanceName)
		{
			return new RegistrySubKey(Registry.LocalMachine, Path.Combine("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\EdgeTransportRole\\AdamSettings", instanceName));
		}

		// Token: 0x04000B22 RID: 2850
		public const string ExchangeGatewayRoleSettingsRegPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\EdgeTransportRole";

		// Token: 0x04000B23 RID: 2851
		public const string AdamServiceSettingsKey = "AdamSettings";

		// Token: 0x04000B24 RID: 2852
		public const string AdamSettingsRegKeyPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\EdgeTransportRole\\AdamSettings";

		// Token: 0x04000B25 RID: 2853
		private const string DataFilesPathRegValueName = "DataFilesPath";

		// Token: 0x04000B26 RID: 2854
		private const string LogFilesPathRegValueName = "LogFilesPath";

		// Token: 0x04000B27 RID: 2855
		private const string LdapPortRegValueName = "LdapPort";

		// Token: 0x04000B28 RID: 2856
		private const string SslPortRegValueName = "SslPort";

		// Token: 0x04000B29 RID: 2857
		private readonly string instanceName;

		// Token: 0x04000B2A RID: 2858
		private readonly string dataFilesPath;

		// Token: 0x04000B2B RID: 2859
		private readonly string logFilesPath;

		// Token: 0x04000B2C RID: 2860
		private readonly int ldapPort;

		// Token: 0x04000B2D RID: 2861
		private readonly int sslPort;
	}
}
