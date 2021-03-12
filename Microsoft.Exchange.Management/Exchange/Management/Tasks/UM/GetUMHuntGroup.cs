using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D21 RID: 3361
	[Cmdlet("Get", "UMHuntGroup", DefaultParameterSetName = "Identity")]
	public sealed class GetUMHuntGroup : GetMultitenancySystemConfigurationObjectTask<UMHuntGroupIdParameter, UMHuntGroup>
	{
		// Token: 0x17002800 RID: 10240
		// (get) Token: 0x060080FB RID: 33019 RVA: 0x0020FDEB File Offset: 0x0020DFEB
		// (set) Token: 0x060080FC RID: 33020 RVA: 0x0020FE02 File Offset: 0x0020E002
		[Parameter(Mandatory = false)]
		public UMDialPlanIdParameter UMDialPlan
		{
			get
			{
				return (UMDialPlanIdParameter)base.Fields[UMHuntGroupSchema.UMDialPlan];
			}
			set
			{
				base.Fields[UMHuntGroupSchema.UMDialPlan] = value;
			}
		}

		// Token: 0x17002801 RID: 10241
		// (get) Token: 0x060080FD RID: 33021 RVA: 0x0020FE15 File Offset: 0x0020E015
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060080FE RID: 33022 RVA: 0x0020FE18 File Offset: 0x0020E018
		internal override IConfigurationSession CreateConfigurationSession(ADSessionSettings sessionSettings)
		{
			if (sessionSettings == null)
			{
				throw new ArgumentNullException("sessionSettings");
			}
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, string.IsNullOrEmpty(base.DomainController) ? null : base.NetCredential, sessionSettings, 73, "CreateConfigurationSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\um\\GetUMHuntGroup.cs");
		}

		// Token: 0x060080FF RID: 33023 RVA: 0x0020FE74 File Offset: 0x0020E074
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.UMDialPlan != null)
			{
				UMDialPlan umdialPlan = (UMDialPlan)base.GetDataObject<UMDialPlan>(this.UMDialPlan, this.ConfigurationSession, this.RootId, new LocalizedString?(Strings.NonExistantDialPlan(this.UMDialPlan.ToString())), new LocalizedString?(Strings.MultipleDialplansWithSameId(this.UMDialPlan.ToString())));
				this.dialplanFilter = new ComparisonFilter(ComparisonOperator.Equal, UMHuntGroupSchema.UMDialPlan, umdialPlan.Id);
			}
		}

		// Token: 0x17002802 RID: 10242
		// (get) Token: 0x06008100 RID: 33024 RVA: 0x0020FEF0 File Offset: 0x0020E0F0
		protected override QueryFilter InternalFilter
		{
			get
			{
				QueryFilter queryFilter = base.InternalFilter;
				if (this.UMDialPlan != null)
				{
					queryFilter = QueryFilter.AndTogether(new QueryFilter[]
					{
						queryFilter,
						this.dialplanFilter
					});
				}
				return queryFilter;
			}
		}

		// Token: 0x04003F19 RID: 16153
		private ComparisonFilter dialplanFilter;
	}
}
