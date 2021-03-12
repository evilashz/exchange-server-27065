using System;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies
{
	// Token: 0x0200000B RID: 11
	internal static class HygieneRulesStrings
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002B34 File Offset: 0x00000D34
		public static LocalizedString CannotSetHeader(string name, string value)
		{
			return new LocalizedString("CannotSetHeader", HygieneRulesStrings.ResourceManager, new object[]
			{
				name,
				value
			});
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002B60 File Offset: 0x00000D60
		public static LocalizedString InvalidHeaderName(string name)
		{
			return new LocalizedString("InvalidHeaderName", HygieneRulesStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002B88 File Offset: 0x00000D88
		public static LocalizedString InvalidAddress(string address)
		{
			return new LocalizedString("InvalidAddress", HygieneRulesStrings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x04000017 RID: 23
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.MessagingPolicies.HygieneRulesStrings", typeof(HygieneRulesStrings).GetTypeInfo().Assembly);

		// Token: 0x0200000C RID: 12
		private enum ParamIDs
		{
			// Token: 0x04000019 RID: 25
			CannotSetHeader,
			// Token: 0x0400001A RID: 26
			InvalidHeaderName,
			// Token: 0x0400001B RID: 27
			InvalidAddress
		}
	}
}
