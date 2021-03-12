using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004C4 RID: 1220
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class IPAllowListConfig : MessageHygieneAgentConfig
	{
		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x0600375F RID: 14175 RVA: 0x000D863C File Offset: 0x000D683C
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x06003760 RID: 14176 RVA: 0x000D8644 File Offset: 0x000D6844
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchMessageHygieneIPAllowListConfig";
			}
		}

		// Token: 0x0400256C RID: 9580
		public const string CanonicalName = "IPAllowListConfig";

		// Token: 0x0400256D RID: 9581
		private const string MostDerivedClass = "msExchMessageHygieneIPAllowListConfig";
	}
}
