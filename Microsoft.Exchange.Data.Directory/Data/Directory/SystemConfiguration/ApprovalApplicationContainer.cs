using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003A6 RID: 934
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class ApprovalApplicationContainer : ADConfigurationObject
	{
		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06002AFD RID: 11005 RVA: 0x000B30D8 File Offset: 0x000B12D8
		internal override ADObjectSchema Schema
		{
			get
			{
				return ApprovalApplicationContainer.schema;
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06002AFE RID: 11006 RVA: 0x000B30DF File Offset: 0x000B12DF
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ApprovalApplicationContainer.mostDerivedClass;
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06002AFF RID: 11007 RVA: 0x000B30E6 File Offset: 0x000B12E6
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06002B00 RID: 11008 RVA: 0x000B30ED File Offset: 0x000B12ED
		// (set) Token: 0x06002B01 RID: 11009 RVA: 0x000B30FF File Offset: 0x000B12FF
		internal ADObjectId RetentionPolicy
		{
			get
			{
				return (ADObjectId)this[ApprovalApplicationContainerSchema.RetentionPolicy];
			}
			set
			{
				this[ApprovalApplicationContainerSchema.RetentionPolicy] = value;
			}
		}

		// Token: 0x040019CD RID: 6605
		private static ApprovalApplicationContainerSchema schema = ObjectSchema.GetInstance<ApprovalApplicationContainerSchema>();

		// Token: 0x040019CE RID: 6606
		private static string mostDerivedClass = "msExchApprovalApplicationContainer";

		// Token: 0x040019CF RID: 6607
		public static readonly string DefaultName = "Approval Applications";
	}
}
