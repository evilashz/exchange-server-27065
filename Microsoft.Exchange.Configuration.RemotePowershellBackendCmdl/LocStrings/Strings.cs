using System;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.Configuration.RemotePowershellBackendCmdletProxy.LocStrings
{
	// Token: 0x02000007 RID: 7
	internal static class Strings
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000241C File Offset: 0x0000061C
		public static string ErrorWhenParsingCommonAccessToken(string message)
		{
			return string.Format(Strings.ResourceManager.GetString("ErrorWhenParsingCommonAccessToken"), message);
		}

		// Token: 0x0400000C RID: 12
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.Configuration.RemotePowershellBackendCmdletProxy.LocStrings.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000008 RID: 8
		private enum ParamIDs
		{
			// Token: 0x0400000E RID: 14
			ErrorWhenParsingCommonAccessToken
		}
	}
}
