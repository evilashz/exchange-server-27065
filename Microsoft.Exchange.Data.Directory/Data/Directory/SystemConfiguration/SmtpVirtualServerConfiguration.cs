using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005AC RID: 1452
	[ObjectScope(ConfigScopes.Server)]
	[Serializable]
	public sealed class SmtpVirtualServerConfiguration : ADLegacyVersionableObject
	{
		// Token: 0x170015ED RID: 5613
		// (get) Token: 0x06004311 RID: 17169 RVA: 0x000FC342 File Offset: 0x000FA542
		internal override ADObjectSchema Schema
		{
			get
			{
				return SmtpVirtualServerConfiguration.schema;
			}
		}

		// Token: 0x170015EE RID: 5614
		// (get) Token: 0x06004312 RID: 17170 RVA: 0x000FC349 File Offset: 0x000FA549
		internal override string MostDerivedObjectClass
		{
			get
			{
				return SmtpVirtualServerConfiguration.mostDerivedClass;
			}
		}

		// Token: 0x170015EF RID: 5615
		// (get) Token: 0x06004313 RID: 17171 RVA: 0x000FC350 File Offset: 0x000FA550
		// (set) Token: 0x06004314 RID: 17172 RVA: 0x000FC362 File Offset: 0x000FA562
		public string SmtpFqdn
		{
			get
			{
				return (string)this[SmtpVirtualServerConfigurationSchema.SmtpFqdn];
			}
			internal set
			{
				this[SmtpVirtualServerConfigurationSchema.SmtpFqdn] = value;
			}
		}

		// Token: 0x04002D88 RID: 11656
		private static SmtpVirtualServerConfigurationSchema schema = ObjectSchema.GetInstance<SmtpVirtualServerConfigurationSchema>();

		// Token: 0x04002D89 RID: 11657
		private static string mostDerivedClass = "protocolCfgSMTPServer";
	}
}
