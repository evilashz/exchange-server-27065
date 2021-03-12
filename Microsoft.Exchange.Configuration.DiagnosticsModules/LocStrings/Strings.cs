using System;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.Configuration.DiagnosticsModules.LocStrings
{
	// Token: 0x02000006 RID: 6
	internal static class Strings
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002639 File Offset: 0x00000839
		public static string UnhandledException(string error)
		{
			return string.Format(Strings.ResourceManager.GetString("UnhandledException"), error);
		}

		// Token: 0x0400000C RID: 12
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.Configuration.DiagnosticsModules.LocStrings.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000007 RID: 7
		private enum ParamIDs
		{
			// Token: 0x0400000E RID: 14
			UnhandledException
		}
	}
}
