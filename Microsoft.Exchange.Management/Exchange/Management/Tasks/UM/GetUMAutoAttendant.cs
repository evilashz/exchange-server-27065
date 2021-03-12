using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D27 RID: 3367
	[Cmdlet("Get", "UMAutoAttendant", DefaultParameterSetName = "Identity")]
	public sealed class GetUMAutoAttendant : GetMultitenancySystemConfigurationObjectTask<UMAutoAttendantIdParameter, UMAutoAttendant>
	{
		// Token: 0x1700280D RID: 10253
		// (get) Token: 0x06008122 RID: 33058 RVA: 0x00210675 File Offset: 0x0020E875
		// (set) Token: 0x06008123 RID: 33059 RVA: 0x0021068C File Offset: 0x0020E88C
		[Parameter(Mandatory = false)]
		public UMDialPlanIdParameter UMDialPlan
		{
			get
			{
				return (UMDialPlanIdParameter)base.Fields["UMDialPlan"];
			}
			set
			{
				base.Fields["UMDialPlan"] = value;
			}
		}

		// Token: 0x06008124 RID: 33060 RVA: 0x002106A0 File Offset: 0x0020E8A0
		internal override IConfigurationSession CreateConfigurationSession(ADSessionSettings sessionSettings)
		{
			if (sessionSettings == null)
			{
				throw new ArgumentNullException("sessionSettings");
			}
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, string.IsNullOrEmpty(base.DomainController) ? null : base.NetCredential, sessionSettings, 66, "CreateConfigurationSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\um\\get_umautoattendant.cs");
		}

		// Token: 0x06008125 RID: 33061 RVA: 0x002106FC File Offset: 0x0020E8FC
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.UMDialPlan != null)
			{
				UMDialPlan umdialPlan = (UMDialPlan)base.GetDataObject<UMDialPlan>(this.UMDialPlan, this.ConfigurationSession, this.RootId, new LocalizedString?(Strings.NonExistantDialPlan(this.UMDialPlan.ToString())), new LocalizedString?(Strings.MultipleDialplansWithSameId(this.UMDialPlan.ToString())));
				this.dialplanFilter = new ComparisonFilter(ComparisonOperator.Equal, UMAutoAttendantSchema.UMDialPlan, umdialPlan.Id);
			}
		}

		// Token: 0x1700280E RID: 10254
		// (get) Token: 0x06008126 RID: 33062 RVA: 0x00210778 File Offset: 0x0020E978
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

		// Token: 0x1700280F RID: 10255
		// (get) Token: 0x06008127 RID: 33063 RVA: 0x002107B0 File Offset: 0x0020E9B0
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06008128 RID: 33064 RVA: 0x002107B4 File Offset: 0x0020E9B4
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			UMAutoAttendant umautoAttendant = dataObject as UMAutoAttendant;
			if (umautoAttendant != null && !string.IsNullOrEmpty(umautoAttendant.DefaultMailboxLegacyDN))
			{
				IRecipientSession recipientSessionScopedToOrganization = Utility.GetRecipientSessionScopedToOrganization(umautoAttendant.OrganizationId, true);
				umautoAttendant.DefaultMailbox = (recipientSessionScopedToOrganization.FindByLegacyExchangeDN(umautoAttendant.DefaultMailboxLegacyDN) as ADUser);
			}
			base.WriteResult(dataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x04003F1D RID: 16157
		private ComparisonFilter dialplanFilter;
	}
}
