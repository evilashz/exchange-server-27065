using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000096 RID: 150
	public abstract class SetSingletonSystemConfigurationObjectTask<TDataObject> : SetSingletonObjectTaskBase<TDataObject> where TDataObject : ADObject, new()
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00016520 File Offset: 0x00014720
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x00016528 File Offset: 0x00014728
		[Parameter]
		public new Fqdn DomainController
		{
			get
			{
				return base.DomainController;
			}
			set
			{
				base.DomainController = value;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x00016531 File Offset: 0x00014731
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x00016548 File Offset: 0x00014748
		internal virtual AccountPartitionIdParameter AccountPartition
		{
			get
			{
				return (AccountPartitionIdParameter)base.Fields["AccountPartition"];
			}
			set
			{
				base.Fields["AccountPartition"] = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x0001655B File Offset: 0x0001475B
		internal ADSessionSettings SessionSettings
		{
			get
			{
				return this.sessionSettings;
			}
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00016563 File Offset: 0x00014763
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			base.CurrentOrganizationId = this.ResolveCurrentOrganization();
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001657C File Offset: 0x0001477C
		protected override void InternalStateReset()
		{
			if (this.AccountPartition != null)
			{
				PartitionId partitionId = RecipientTaskHelper.ResolvePartitionId(this.AccountPartition, new Task.TaskErrorLoggingDelegate(base.WriteError));
				this.sessionSettings = ADSessionSettings.FromAccountPartitionRootOrgScopeSet(partitionId);
			}
			else
			{
				this.sessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			}
			base.InternalStateReset();
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x000165E1 File Offset: 0x000147E1
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, this.SessionSettings, 912, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\SetAdObjectTask.cs");
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001660F File Offset: 0x0001480F
		protected virtual OrganizationId ResolveCurrentOrganization()
		{
			return base.CurrentOrganizationId ?? base.ExecutingUserOrganizationId;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00016621 File Offset: 0x00014821
		protected override void InternalEndProcessing()
		{
			base.InternalEndProcessing();
			this.sessionSettings = null;
		}

		// Token: 0x04000135 RID: 309
		private ADSessionSettings sessionSettings;
	}
}
