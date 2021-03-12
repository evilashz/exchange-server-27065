using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004C6 RID: 1222
	[Serializable]
	public sealed class IPAllowListProvider : IPListProvider
	{
		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x06003772 RID: 14194 RVA: 0x000D8779 File Offset: 0x000D6979
		internal override ADObjectId ParentPath
		{
			get
			{
				return IPAllowListProvider.parentPath;
			}
		}

		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x06003773 RID: 14195 RVA: 0x000D8780 File Offset: 0x000D6980
		internal override string MostDerivedObjectClass
		{
			get
			{
				return IPAllowListProvider.mostDerivedClass;
			}
		}

		// Token: 0x0400256F RID: 9583
		private static string mostDerivedClass = "msExchMessageHygieneIPAllowListProvider";

		// Token: 0x04002570 RID: 9584
		private static ADObjectId parentPath = new ADObjectId("CN=IPAllowListProviderConfig,CN=Message Hygiene,CN=Transport Settings");
	}
}
