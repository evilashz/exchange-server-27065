using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000688 RID: 1672
	[Cmdlet("Get", "ManagementScope", DefaultParameterSetName = "Identity")]
	public sealed class GetManagementScope : GetMultitenancySystemConfigurationObjectTask<ManagementScopeIdParameter, ManagementScope>
	{
		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x06003B31 RID: 15153 RVA: 0x000FBDEC File Offset: 0x000F9FEC
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}

		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x06003B32 RID: 15154 RVA: 0x000FBDEF File Offset: 0x000F9FEF
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x06003B33 RID: 15155 RVA: 0x000FBDF2 File Offset: 0x000F9FF2
		protected override QueryFilter InternalFilter
		{
			get
			{
				return base.OptionalIdentityData.AdditionalFilter;
			}
		}

		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x06003B34 RID: 15156 RVA: 0x000FBDFF File Offset: 0x000F9FFF
		// (set) Token: 0x06003B35 RID: 15157 RVA: 0x000FBE28 File Offset: 0x000FA028
		[Parameter(Mandatory = false)]
		public SwitchParameter Orphan
		{
			get
			{
				return (SwitchParameter)(base.Fields["Orphan"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Orphan"] = value;
				if (value.ToBool())
				{
					base.OptionalIdentityData.AdditionalFilter = QueryFilter.AndTogether(new QueryFilter[]
					{
						base.OptionalIdentityData.AdditionalFilter,
						GetManagementScope.orphanedFilter
					});
				}
			}
		}

		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x06003B36 RID: 15158 RVA: 0x000FBE82 File Offset: 0x000FA082
		// (set) Token: 0x06003B37 RID: 15159 RVA: 0x000FBE9C File Offset: 0x000FA09C
		[Parameter(Mandatory = false)]
		public bool Exclusive
		{
			get
			{
				return (bool)base.Fields["Exclusive"];
			}
			set
			{
				base.Fields["Exclusive"] = value;
				base.OptionalIdentityData.AdditionalFilter = QueryFilter.AndTogether(new QueryFilter[]
				{
					base.OptionalIdentityData.AdditionalFilter,
					new ComparisonFilter(ComparisonOperator.Equal, ManagementScopeSchema.Exclusive, value)
				});
			}
		}

		// Token: 0x06003B38 RID: 15160 RVA: 0x000FBEF9 File Offset: 0x000FA0F9
		public static void StampQueryFilterOnManagementScope(ManagementScope managementScope)
		{
			ExchangeRunspaceConfiguration.TryStampQueryFilterOnManagementScope(managementScope);
		}

		// Token: 0x040026BF RID: 9919
		private static QueryFilter orphanedFilter = new AndFilter(new QueryFilter[]
		{
			new NotFilter(new ExistsFilter(ManagementScopeSchema.RecipientWriteScopeBL)),
			new NotFilter(new ExistsFilter(ManagementScopeSchema.ConfigWriteScopeBL))
		});
	}
}
