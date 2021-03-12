using System;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.ThrottlingService
{
	// Token: 0x02000012 RID: 18
	internal static class Strings
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00003944 File Offset: 0x00001B44
		public static LocalizedString UsageText(string processName)
		{
			return new LocalizedString("UsageText", "Ex9DF686", false, true, Strings.ResourceManager, new object[]
			{
				processName
			});
		}

		// Token: 0x0400005D RID: 93
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Data.ThrottlingService.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000013 RID: 19
		private enum ParamIDs
		{
			// Token: 0x0400005F RID: 95
			UsageText
		}
	}
}
