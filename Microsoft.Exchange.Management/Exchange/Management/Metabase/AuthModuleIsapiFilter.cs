using System;
using System.DirectoryServices;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x0200049B RID: 1179
	internal static class AuthModuleIsapiFilter
	{
		// Token: 0x060029E0 RID: 10720 RVA: 0x000A6554 File Offset: 0x000A4754
		public static void Install(DirectoryEntry virtualDirectory)
		{
			string iiswebsitePath = IsapiFilterCommon.GetIISWebsitePath(virtualDirectory);
			bool flag;
			IsapiFilterCommon.CreateFilter(iiswebsitePath, "Microsoft.Exchange.AuthModuleFilter ISAPI Filter", AuthModuleIsapiFilter.FilterDirectory, AuthModuleIsapiFilter.ExtensionBinary, true, out flag);
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x000A6580 File Offset: 0x000A4780
		public static void Uninstall(DirectoryEntry virtualDirectory)
		{
			IsapiFilterCommon.Uninstall(virtualDirectory, "Microsoft.Exchange.AuthModuleFilter ISAPI Filter");
		}

		// Token: 0x04001E81 RID: 7809
		private const string FilterName = "Microsoft.Exchange.AuthModuleFilter ISAPI Filter";

		// Token: 0x04001E82 RID: 7810
		private static readonly string ExtensionBinary = "Microsoft.Exchange.AuthModuleFilter.dll";

		// Token: 0x04001E83 RID: 7811
		private static readonly string FilterDirectory = "bin";
	}
}
