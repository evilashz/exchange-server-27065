using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200059F RID: 1439
	[ObjectScope(ConfigScopes.Server)]
	[Serializable]
	public class SIPContainer : ADConfigurationObject
	{
		// Token: 0x170015D0 RID: 5584
		// (get) Token: 0x060042CA RID: 17098 RVA: 0x000FB952 File Offset: 0x000F9B52
		internal override ADObjectSchema Schema
		{
			get
			{
				return SIPContainer.schema;
			}
		}

		// Token: 0x170015D1 RID: 5585
		// (get) Token: 0x060042CB RID: 17099 RVA: 0x000FB959 File Offset: 0x000F9B59
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchProtocolCfgSIPContainer";
			}
		}

		// Token: 0x060042CC RID: 17100 RVA: 0x000FB960 File Offset: 0x000F9B60
		internal static ADObjectId GetBaseContainer(ITopologyConfigurationSession dataSession)
		{
			ADObjectId relativePath = new ADObjectId("CN=Protocols");
			return dataSession.FindLocalServer().Id.GetDescendantId(relativePath);
		}

		// Token: 0x04002D64 RID: 11620
		private const string MostDerivedClass = "msExchProtocolCfgSIPContainer";

		// Token: 0x04002D65 RID: 11621
		private static SIPContainerSchema schema = ObjectSchema.GetInstance<SIPContainerSchema>();
	}
}
