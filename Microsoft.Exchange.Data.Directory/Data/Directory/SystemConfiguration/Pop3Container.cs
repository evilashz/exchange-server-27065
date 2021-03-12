using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200053D RID: 1341
	[ObjectScope(ConfigScopes.Server)]
	[Serializable]
	public class Pop3Container : Container
	{
		// Token: 0x17001332 RID: 4914
		// (get) Token: 0x06003C3C RID: 15420 RVA: 0x000E6734 File Offset: 0x000E4934
		internal override ADObjectSchema Schema
		{
			get
			{
				return Pop3Container.schema;
			}
		}

		// Token: 0x17001333 RID: 4915
		// (get) Token: 0x06003C3D RID: 15421 RVA: 0x000E673B File Offset: 0x000E493B
		internal override string MostDerivedObjectClass
		{
			get
			{
				return Pop3Container.mostDerivedClass;
			}
		}

		// Token: 0x06003C3E RID: 15422 RVA: 0x000E6744 File Offset: 0x000E4944
		internal static ADObjectId GetBaseContainer(ITopologyConfigurationSession dataSession)
		{
			ADObjectId relativePath = new ADObjectId("CN=Protocols");
			return dataSession.FindLocalServer().Id.GetDescendantId(relativePath);
		}

		// Token: 0x040028C3 RID: 10435
		private static Pop3ContainerSchema schema = ObjectSchema.GetInstance<Pop3ContainerSchema>();

		// Token: 0x040028C4 RID: 10436
		private static string mostDerivedClass = "msExchProtocolCfgPOPContainer";
	}
}
