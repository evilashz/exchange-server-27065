using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200045C RID: 1116
	[Cmdlet("Get", "UMMailboxPolicy", DefaultParameterSetName = "Identity")]
	public sealed class GetUMPolicy : GetMailboxPolicyBase<UMMailboxPolicy>
	{
		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06002775 RID: 10101 RVA: 0x0009BCEC File Offset: 0x00099EEC
		// (set) Token: 0x06002776 RID: 10102 RVA: 0x0009BD03 File Offset: 0x00099F03
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

		// Token: 0x06002777 RID: 10103 RVA: 0x0009BD18 File Offset: 0x00099F18
		internal override IConfigurationSession CreateConfigurationSession(ADSessionSettings sessionSettings)
		{
			if (sessionSettings == null)
			{
				throw new ArgumentNullException("sessionSettings");
			}
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, string.IsNullOrEmpty(base.DomainController) ? null : base.NetCredential, sessionSettings, 242, "CreateConfigurationSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxPolicies\\UMMailboxPolicyTask.cs");
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x0009BD78 File Offset: 0x00099F78
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.UMDialPlan != null)
			{
				UMDialPlan umdialPlan = (UMDialPlan)base.GetDataObject<UMDialPlan>(this.UMDialPlan, this.ConfigurationSession, this.RootId, new LocalizedString?(Strings.NonExistantDialPlan(this.UMDialPlan.ToString())), new LocalizedString?(Strings.MultipleDialplansWithSameId(this.UMDialPlan.ToString())));
				this.dialplanFilter = new ComparisonFilter(ComparisonOperator.Equal, UMMailboxPolicySchema.UMDialPlan, umdialPlan.Id);
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06002779 RID: 10105 RVA: 0x0009BDF4 File Offset: 0x00099FF4
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

		// Token: 0x04001DA0 RID: 7584
		private ComparisonFilter dialplanFilter;
	}
}
