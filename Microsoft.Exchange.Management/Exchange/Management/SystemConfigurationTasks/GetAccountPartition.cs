using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B18 RID: 2840
	[Cmdlet("Get", "AccountPartition", DefaultParameterSetName = "Identity")]
	public sealed class GetAccountPartition : GetSystemConfigurationObjectTask<AccountPartitionIdParameter, AccountPartition>
	{
		// Token: 0x17001EA6 RID: 7846
		// (get) Token: 0x060064D0 RID: 25808 RVA: 0x001A4C15 File Offset: 0x001A2E15
		// (set) Token: 0x060064D1 RID: 25809 RVA: 0x001A4C1D File Offset: 0x001A2E1D
		[Parameter]
		public SwitchParameter IncludeSecondaryPartitions { private get; set; }

		// Token: 0x17001EA7 RID: 7847
		// (get) Token: 0x060064D2 RID: 25810 RVA: 0x001A4C28 File Offset: 0x001A2E28
		protected override ObjectId RootId
		{
			get
			{
				IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
				return configurationSession.GetOrgContainerId().GetChildId(Microsoft.Exchange.Data.Directory.SystemConfiguration.AccountPartition.AccountForestContainerName);
			}
		}

		// Token: 0x17001EA8 RID: 7848
		// (get) Token: 0x060064D3 RID: 25811 RVA: 0x001A4C54 File Offset: 0x001A2E54
		protected override QueryFilter InternalFilter
		{
			get
			{
				if (this.IncludeSecondaryPartitions)
				{
					return base.InternalFilter;
				}
				QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.NotEqual, AccountPartitionSchema.IsSecondary, true);
				if (base.InternalFilter != null)
				{
					return new AndFilter(new QueryFilter[]
					{
						base.InternalFilter,
						queryFilter
					});
				}
				return queryFilter;
			}
		}
	}
}
