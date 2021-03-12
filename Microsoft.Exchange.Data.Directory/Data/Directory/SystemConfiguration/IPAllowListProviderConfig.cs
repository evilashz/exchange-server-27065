using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004C7 RID: 1223
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class IPAllowListProviderConfig : MessageHygieneAgentConfig
	{
		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x06003776 RID: 14198 RVA: 0x000D87AA File Offset: 0x000D69AA
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x06003777 RID: 14199 RVA: 0x000D87B2 File Offset: 0x000D69B2
		internal override string MostDerivedObjectClass
		{
			get
			{
				return IPAllowListProviderConfig.mostDerivedClass;
			}
		}

		// Token: 0x04002571 RID: 9585
		public const string CanonicalName = "IPAllowListProviderConfig";

		// Token: 0x04002572 RID: 9586
		private static string mostDerivedClass = "msExchMessageHygieneIPAllowListProviderConfig";
	}
}
