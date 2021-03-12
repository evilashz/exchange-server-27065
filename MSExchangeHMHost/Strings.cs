using System;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.ActiveMonitoring
{
	// Token: 0x02000006 RID: 6
	internal static class Strings
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002804 File Offset: 0x00000A04
		public static LocalizedString UsageText(string processName)
		{
			return new LocalizedString("UsageText", Strings.ResourceManager, new object[]
			{
				processName
			});
		}

		// Token: 0x04000035 RID: 53
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.ActiveMonitoring.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000007 RID: 7
		private enum ParamIDs
		{
			// Token: 0x04000037 RID: 55
			UsageText
		}
	}
}
