using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200046C RID: 1132
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class HostedConnectionFilterPolicy : ADConfigurationObject
	{
		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06003256 RID: 12886 RVA: 0x000CBC42 File Offset: 0x000C9E42
		internal override ADObjectSchema Schema
		{
			get
			{
				return HostedConnectionFilterPolicy.schema;
			}
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06003257 RID: 12887 RVA: 0x000CBC49 File Offset: 0x000C9E49
		internal override ADObjectId ParentPath
		{
			get
			{
				return HostedConnectionFilterPolicy.parentPath;
			}
		}

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06003258 RID: 12888 RVA: 0x000CBC50 File Offset: 0x000C9E50
		internal override string MostDerivedObjectClass
		{
			get
			{
				return HostedConnectionFilterPolicy.ldapName;
			}
		}

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06003259 RID: 12889 RVA: 0x000CBC57 File Offset: 0x000C9E57
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x0600325A RID: 12890 RVA: 0x000CBC5E File Offset: 0x000C9E5E
		// (set) Token: 0x0600325B RID: 12891 RVA: 0x000CBC70 File Offset: 0x000C9E70
		[Parameter]
		public new string AdminDisplayName
		{
			get
			{
				return (string)this[ADConfigurationObjectSchema.AdminDisplayName];
			}
			set
			{
				this[ADConfigurationObjectSchema.AdminDisplayName] = value;
			}
		}

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x0600325C RID: 12892 RVA: 0x000CBC7E File Offset: 0x000C9E7E
		// (set) Token: 0x0600325D RID: 12893 RVA: 0x000CBC90 File Offset: 0x000C9E90
		public bool IsDefault
		{
			get
			{
				return (bool)this[HostedConnectionFilterPolicySchema.IsDefault];
			}
			internal set
			{
				this[HostedConnectionFilterPolicySchema.IsDefault] = value;
			}
		}

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x0600325E RID: 12894 RVA: 0x000CBCA3 File Offset: 0x000C9EA3
		// (set) Token: 0x0600325F RID: 12895 RVA: 0x000CBCB5 File Offset: 0x000C9EB5
		[Parameter]
		public MultiValuedProperty<IPRange> IPAllowList
		{
			get
			{
				return (MultiValuedProperty<IPRange>)this[HostedConnectionFilterPolicySchema.IPAllowList];
			}
			set
			{
				this[HostedConnectionFilterPolicySchema.IPAllowList] = value;
			}
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06003260 RID: 12896 RVA: 0x000CBCC3 File Offset: 0x000C9EC3
		// (set) Token: 0x06003261 RID: 12897 RVA: 0x000CBCD5 File Offset: 0x000C9ED5
		[Parameter]
		public MultiValuedProperty<IPRange> IPBlockList
		{
			get
			{
				return (MultiValuedProperty<IPRange>)this[HostedConnectionFilterPolicySchema.IPBlockList];
			}
			set
			{
				this[HostedConnectionFilterPolicySchema.IPBlockList] = value;
			}
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x06003262 RID: 12898 RVA: 0x000CBCE3 File Offset: 0x000C9EE3
		// (set) Token: 0x06003263 RID: 12899 RVA: 0x000CBCF5 File Offset: 0x000C9EF5
		[Parameter]
		public bool EnableSafeList
		{
			get
			{
				return (bool)this[HostedConnectionFilterPolicySchema.EnableSafeList];
			}
			set
			{
				this[HostedConnectionFilterPolicySchema.EnableSafeList] = value;
			}
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06003264 RID: 12900 RVA: 0x000CBD08 File Offset: 0x000C9F08
		// (set) Token: 0x06003265 RID: 12901 RVA: 0x000CBD1A File Offset: 0x000C9F1A
		[Parameter]
		public DirectoryBasedEdgeBlockMode DirectoryBasedEdgeBlockMode
		{
			get
			{
				return (DirectoryBasedEdgeBlockMode)this[HostedConnectionFilterPolicySchema.DirectoryBasedEdgeBlockMode];
			}
			set
			{
				this[HostedConnectionFilterPolicySchema.DirectoryBasedEdgeBlockMode] = (int)value;
			}
		}

		// Token: 0x0400229C RID: 8860
		private static readonly string ldapName = "msExchHostedConnectionFilterPolicy";

		// Token: 0x0400229D RID: 8861
		private static readonly ADObjectId parentPath = new ADObjectId("CN=Hosted Connection Filter,CN=Transport Settings");

		// Token: 0x0400229E RID: 8862
		private static readonly HostedConnectionFilterPolicySchema schema = ObjectSchema.GetInstance<HostedConnectionFilterPolicySchema>();
	}
}
