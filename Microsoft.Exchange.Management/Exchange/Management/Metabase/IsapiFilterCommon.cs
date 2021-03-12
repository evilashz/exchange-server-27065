using System;
using System.DirectoryServices;
using System.IO;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x020004CD RID: 1229
	internal static class IsapiFilterCommon
	{
		// Token: 0x06002AA5 RID: 10917 RVA: 0x000AB1B0 File Offset: 0x000A93B0
		internal static bool CreateFilter(DirectoryEntry virtualDirectory, string filterName, string filterDirectory, string extensionBinary)
		{
			bool result = false;
			string iiswebsitePath = IsapiFilterCommon.GetIISWebsitePath(virtualDirectory);
			IsapiFilterCommon.CreateFilter(iiswebsitePath, filterName, filterDirectory, extensionBinary, true, out result);
			return result;
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x000AB1D4 File Offset: 0x000A93D4
		internal static void CreateFilter(string adsiWebSitePath, string filterName, string filterDirectory, string extensionBinary, bool enable, out bool filterCreated)
		{
			filterCreated = false;
			string iisserverName = IsapiFilterCommon.GetIISServerName(adsiWebSitePath);
			using (RegistryKey registryKey = RegistryUtil.OpenRemoteBaseKey(RegistryHive.LocalMachine, iisserverName))
			{
				using (RegistryKey registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup"))
				{
					string text = Path.Combine((string)registryKey2.GetValue("MsiInstallPath"), filterDirectory);
					text = Path.Combine(text, extensionBinary);
					using (DirectoryEntry directoryEntry = IsapiFilter.CreateIsapiFilter(adsiWebSitePath, text, filterName, out filterCreated))
					{
						if (filterCreated)
						{
							directoryEntry.Properties["FilterFlags"].Value = MetabasePropertyTypes.FilterFlags.NotifyOrderMedium;
							directoryEntry.Properties["FilterEnabled"].Value = enable;
							directoryEntry.CommitChanges();
							IisUtility.CommitMetabaseChanges(iisserverName);
						}
					}
				}
			}
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x000AB2D0 File Offset: 0x000A94D0
		internal static void Uninstall(DirectoryEntry virtualDirectory, string filterName)
		{
			string iisserverName = IsapiFilterCommon.GetIISServerName(virtualDirectory);
			string iislocalPath = IsapiFilterCommon.GetIISLocalPath(virtualDirectory);
			string text = null;
			string str = null;
			string text2 = null;
			IisUtility.ParseApplicationRootPath(iislocalPath, ref text, ref str, ref text2);
			IsapiFilter.RemoveIsapiFilter("IIS://" + iisserverName + str, filterName);
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x000AB310 File Offset: 0x000A9510
		internal static string GetIISServerName(string path)
		{
			return path.Substring("IIS://".Length, path.IndexOf("/", "IIS://".Length) - "IIS://".Length);
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x000AB342 File Offset: 0x000A9542
		internal static string GetIISServerName(DirectoryEntry virtualDirectory)
		{
			return IsapiFilterCommon.GetIISServerName(virtualDirectory.Path);
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x000AB34F File Offset: 0x000A954F
		internal static string GetIISWebsitePath(DirectoryEntry virtualDirectory)
		{
			return virtualDirectory.Path.Substring(0, virtualDirectory.Path.IndexOf("/ROOT/", StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x000AB36E File Offset: 0x000A956E
		internal static string GetIISLocalPath(DirectoryEntry virtualDirectory)
		{
			return virtualDirectory.Path.Substring(virtualDirectory.Path.IndexOf("/", "IIS://".Length, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x04001FDE RID: 8158
		private const string MetabaseRoot = "/LM";

		// Token: 0x04001FDF RID: 8159
		private const string AdsiPrefix = "IIS://";
	}
}
