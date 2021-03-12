using System;
using System.IO;
using Microsoft.Win32;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000D8 RID: 216
	internal class WatsonRegKeyReportAction : WatsonReportAction
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x000197BD File Offset: 0x000179BD
		public WatsonRegKeyReportAction(string keyName) : base(keyName, false)
		{
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x000197C7 File Offset: 0x000179C7
		public override string ActionName
		{
			get
			{
				return "Registry Value";
			}
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x000197D0 File Offset: 0x000179D0
		public override string Evaluate(WatsonReport watsonReport)
		{
			string registryValue = WatsonRegKeyReportAction.GetRegistryValue(base.Expression);
			string text;
			if (registryValue == null)
			{
				text = base.Expression + " not found.";
			}
			else
			{
				text = base.Expression + "=" + registryValue;
			}
			watsonReport.LogExtraData(text);
			return text;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001981C File Offset: 0x00017A1C
		private static string GetRegistryValue(string fullPath)
		{
			RegistryKey registryKey;
			if (fullPath.StartsWith("HKLM"))
			{
				registryKey = Registry.LocalMachine;
			}
			else if (fullPath.StartsWith("HKCU"))
			{
				registryKey = Registry.CurrentUser;
			}
			else if (fullPath.StartsWith("HKCR"))
			{
				registryKey = Registry.ClassesRoot;
			}
			else if (fullPath.StartsWith("HKCC"))
			{
				registryKey = Registry.CurrentConfig;
			}
			else
			{
				if (!fullPath.StartsWith("HKPD"))
				{
					return null;
				}
				registryKey = Registry.PerformanceData;
			}
			string path = fullPath.Substring("HKxx\\".Length);
			string directoryName = Path.GetDirectoryName(path);
			string fileName = Path.GetFileName(path);
			RegistryKey registryKey2 = null;
			string result;
			try
			{
				registryKey2 = registryKey.OpenSubKey(directoryName);
				object value = registryKey2.GetValue(fileName);
				if (value == null)
				{
					result = null;
				}
				else
				{
					result = value.ToString();
				}
			}
			finally
			{
				if (registryKey2 != null)
				{
					registryKey2.Close();
				}
			}
			return result;
		}
	}
}
