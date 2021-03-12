using System;
using System.Security;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Win32;

namespace Microsoft.Exchange.InfoWorker.Common.ELC
{
	// Token: 0x020001A7 RID: 423
	internal static class Globals
	{
		// Token: 0x06000B2D RID: 2861 RVA: 0x0002F5B4 File Offset: 0x0002D7B4
		internal static object ReadRegKey(string parameterRegistryKeyPath, string nameOfRegKey)
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(parameterRegistryKeyPath, false))
				{
					if (registryKey != null)
					{
						object value = registryKey.GetValue(nameOfRegKey);
						if (value != null)
						{
							return value;
						}
					}
				}
			}
			catch (SecurityException)
			{
			}
			catch (ObjectDisposedException)
			{
			}
			return null;
		}

		// Token: 0x0400084B RID: 2123
		public const string ElcAssistantName = "ElcAssistant";

		// Token: 0x0400084C RID: 2124
		internal const string ElcConfigurationXSOClass = "ELC";

		// Token: 0x0400084D RID: 2125
		internal const string ElcTagConfigurationXSOClass = "MRM";

		// Token: 0x0400084E RID: 2126
		internal const string MRMFolderVerifyerClass = "MRMFolder";

		// Token: 0x0400084F RID: 2127
		internal const string AutoTagSettingConfigurationXSOClass = "MRM.AutoTag.Setting";

		// Token: 0x04000850 RID: 2128
		internal const string AutoTagModelConfigurationXSOClass = "MRM.AutoTag.Model";

		// Token: 0x04000851 RID: 2129
		internal const string ElcConfigurationStoreClass = "IPM.Configuration.ELC";

		// Token: 0x04000852 RID: 2130
		internal const string MrmConfigurationStoreClass = "IPM.Configuration.MRM";

		// Token: 0x04000853 RID: 2131
		internal const string AutoTagModelConfigurationStoreClass = "IPM.Configuration.MRM.AutoTag.Model";

		// Token: 0x04000854 RID: 2132
		internal const string AutoTagSettingConfigurationStoreClass = "IPM.Configuration.MRM.AutoTag.Setting";

		// Token: 0x04000855 RID: 2133
		internal const UserConfigurationTypes ElcConfigurationTypes = UserConfigurationTypes.Stream | UserConfigurationTypes.XML | UserConfigurationTypes.Dictionary;

		// Token: 0x04000856 RID: 2134
		internal const int QueryResultBatchSize = 100;

		// Token: 0x04000857 RID: 2135
		internal static readonly string UnlimitedHoldDuration = "Unlimited";

		// Token: 0x04000858 RID: 2136
		internal static readonly int HRESULT_FROM_WIN32_DS_OPERATIONS_ERROR = -2147016672;

		// Token: 0x04000859 RID: 2137
		internal static readonly string BlankComment = " ";

		// Token: 0x0400085A RID: 2138
		internal static readonly string ElcRootFolderClass = "IPF.Note.OutlookHomepage";
	}
}
