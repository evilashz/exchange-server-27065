using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000488 RID: 1160
	[ObjectScope(ConfigScopes.Server)]
	[Serializable]
	public class Imap4Container : Container
	{
		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x060034A5 RID: 13477 RVA: 0x000D1896 File Offset: 0x000CFA96
		internal override ADObjectSchema Schema
		{
			get
			{
				return Imap4Container.schema;
			}
		}

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x060034A6 RID: 13478 RVA: 0x000D189D File Offset: 0x000CFA9D
		internal override string MostDerivedObjectClass
		{
			get
			{
				return Imap4Container.mostDerivedClass;
			}
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x000D18A4 File Offset: 0x000CFAA4
		internal static ADObjectId GetBaseContainer(ITopologyConfigurationSession dataSession)
		{
			ADObjectId relativePath = new ADObjectId("CN=Protocols");
			return dataSession.FindLocalServer().Id.GetDescendantId(relativePath);
		}

		// Token: 0x040023DD RID: 9181
		private static Imap4ContainerSchema schema = ObjectSchema.GetInstance<Imap4ContainerSchema>();

		// Token: 0x040023DE RID: 9182
		private static string mostDerivedClass = "msExchProtocolCfgIMAPContainer";
	}
}
