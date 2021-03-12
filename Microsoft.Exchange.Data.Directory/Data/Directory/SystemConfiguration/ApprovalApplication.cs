using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003A4 RID: 932
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class ApprovalApplication : ADConfigurationObject
	{
		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06002AF3 RID: 10995 RVA: 0x000B305B File Offset: 0x000B125B
		internal override ADObjectSchema Schema
		{
			get
			{
				return ApprovalApplication.schema;
			}
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06002AF4 RID: 10996 RVA: 0x000B3062 File Offset: 0x000B1262
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ApprovalApplication.mostDerivedClass;
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06002AF5 RID: 10997 RVA: 0x000B3069 File Offset: 0x000B1269
		internal override ADObjectId ParentPath
		{
			get
			{
				return ApprovalApplication.ParentPathInternal;
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06002AF6 RID: 10998 RVA: 0x000B3070 File Offset: 0x000B1270
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06002AF7 RID: 10999 RVA: 0x000B3077 File Offset: 0x000B1277
		// (set) Token: 0x06002AF8 RID: 11000 RVA: 0x000B3089 File Offset: 0x000B1289
		internal ADObjectId ELCRetentionPolicyTag
		{
			get
			{
				return (ADObjectId)this[ApprovalApplicationSchema.ELCRetentionPolicyTag];
			}
			set
			{
				this[ApprovalApplicationSchema.ELCRetentionPolicyTag] = value;
			}
		}

		// Token: 0x040019C9 RID: 6601
		private static ApprovalApplicationSchema schema = ObjectSchema.GetInstance<ApprovalApplicationSchema>();

		// Token: 0x040019CA RID: 6602
		private static string mostDerivedClass = "msExchApprovalApplication";

		// Token: 0x040019CB RID: 6603
		public static readonly ADObjectId ParentPathInternal = new ADObjectId("CN=Approval Applications");
	}
}
