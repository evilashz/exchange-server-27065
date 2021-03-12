using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000646 RID: 1606
	[Cmdlet("Clear", "ObsoleteRBACRoles")]
	public sealed class ClearObsoleteRBACRoles : SetupTaskBase
	{
		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x0600383B RID: 14395 RVA: 0x000E89DD File Offset: 0x000E6BDD
		// (set) Token: 0x0600383C RID: 14396 RVA: 0x000E89E5 File Offset: 0x000E6BE5
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true)]
		public override OrganizationIdParameter Organization
		{
			get
			{
				return base.Organization;
			}
			set
			{
				base.Organization = value;
			}
		}

		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x0600383D RID: 14397 RVA: 0x000E89EE File Offset: 0x000E6BEE
		// (set) Token: 0x0600383E RID: 14398 RVA: 0x000E8A05 File Offset: 0x000E6C05
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true)]
		public ServicePlan ServicePlanSettings
		{
			get
			{
				return (ServicePlan)base.Fields["ServicePlanSettings"];
			}
			set
			{
				base.Fields["ServicePlanSettings"] = value;
			}
		}

		// Token: 0x0600383F RID: 14399 RVA: 0x000E8A18 File Offset: 0x000E6C18
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
			List<ExchangeRole> list = new List<ExchangeRole>();
			foreach (ServicePlan.MailboxPlan mailboxPlan in this.ServicePlanSettings.MailboxPlans)
			{
				if (!string.IsNullOrEmpty(mailboxPlan.MailboxPlanIndex))
				{
					dictionary[mailboxPlan.MailboxPlanIndex] = true;
				}
			}
			ADPagedReader<ExchangeRole> adpagedReader = this.configurationSession.FindAllPaged<ExchangeRole>();
			foreach (ExchangeRole exchangeRole in adpagedReader)
			{
				if (exchangeRole.IsRootRole && !string.IsNullOrEmpty(exchangeRole.MailboxPlanIndex) && dictionary.ContainsKey(exchangeRole.MailboxPlanIndex))
				{
					list.Add(exchangeRole);
				}
			}
			ADObjectId descendantId = base.OrgContainerId.GetDescendantId(ExchangeRoleAssignment.RdnContainer);
			foreach (ExchangeRole exchangeRole2 in list)
			{
				this.RemoveRoleTreeAndAssignments(exchangeRole2.Id, descendantId);
			}
		}

		// Token: 0x06003840 RID: 14400 RVA: 0x000E8B74 File Offset: 0x000E6D74
		private void RemoveRoleTreeAndAssignments(ADObjectId roleId, ADObjectId roleAssignmentContainerId)
		{
			TaskLogger.LogEnter(new object[]
			{
				"roleId"
			});
			ExchangeRole[] array = this.configurationSession.Find<ExchangeRole>(roleId, QueryScope.SubTree, null, null, 0);
			if (array.Length > 0)
			{
				ExchangeRole exchangeRole = null;
				foreach (ExchangeRole exchangeRole2 in array)
				{
					base.LogReadObject(exchangeRole2);
					if (exchangeRole2.Id.Equals(roleId))
					{
						exchangeRole = exchangeRole2;
					}
					ADPagedReader<ExchangeRoleAssignment> adpagedReader = this.configurationSession.FindPaged<ExchangeRoleAssignment>(roleAssignmentContainerId, QueryScope.OneLevel, new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.Role, exchangeRole2.Id), null, 0);
					foreach (ExchangeRoleAssignment exchangeRoleAssignment in adpagedReader)
					{
						base.LogReadObject(exchangeRoleAssignment);
						this.configurationSession.Delete(exchangeRoleAssignment);
						base.LogWriteObject(exchangeRoleAssignment);
					}
				}
				this.configurationSession.DeleteTree(exchangeRole, delegate(ADTreeDeleteNotFinishedException de)
				{
					if (de != null)
					{
						base.WriteVerbose(de.LocalizedString);
					}
				});
				base.LogWriteObject(exchangeRole);
			}
			TaskLogger.LogExit();
		}
	}
}
