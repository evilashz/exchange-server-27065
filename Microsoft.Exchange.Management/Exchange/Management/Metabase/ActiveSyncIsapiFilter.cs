using System;
using System.DirectoryServices;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x0200049A RID: 1178
	internal static class ActiveSyncIsapiFilter
	{
		// Token: 0x060029DC RID: 10716 RVA: 0x000A6501 File Offset: 0x000A4701
		public static void Install(DirectoryEntry virtualDirectory)
		{
			IsapiFilterCommon.CreateFilter(virtualDirectory, "Exchange ActiveSync ISAPI Filter", ActiveSyncIsapiFilter.FilterDirectory, ActiveSyncIsapiFilter.ExtensionBinary);
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x000A6519 File Offset: 0x000A4719
		public static void InstallForCafe(DirectoryEntry virtualDirectory)
		{
			IsapiFilterCommon.CreateFilter(virtualDirectory, "Exchange ActiveSync ISAPI Filter", "FrontEnd\\HttpProxy\\bin", ActiveSyncIsapiFilter.ExtensionBinary);
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x000A6531 File Offset: 0x000A4731
		public static void Uninstall(DirectoryEntry virtualDirectory)
		{
			IsapiFilterCommon.Uninstall(virtualDirectory, "Exchange ActiveSync ISAPI Filter");
		}

		// Token: 0x04001E7D RID: 7805
		private const string FilterName = "Exchange ActiveSync ISAPI Filter";

		// Token: 0x04001E7E RID: 7806
		private const string CafeFilterDirectory = "FrontEnd\\HttpProxy\\bin";

		// Token: 0x04001E7F RID: 7807
		private static readonly string ExtensionBinary = "AirFilter.dll";

		// Token: 0x04001E80 RID: 7808
		private static readonly string FilterDirectory = "ClientAccess\\sync\\bin";
	}
}
