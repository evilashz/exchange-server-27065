using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200005E RID: 94
	public abstract class GetSingletonSystemConfigurationObjectTask<TDataObject> : GetTaskBase<TDataObject> where TDataObject : ADObject, new()
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000EC4D File Offset: 0x0000CE4D
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x0000EC55 File Offset: 0x0000CE55
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

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000EC5E File Offset: 0x0000CE5E
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x0000EC75 File Offset: 0x0000CE75
		protected virtual AccountPartitionIdParameter AccountPartition
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

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000EC88 File Offset: 0x0000CE88
		internal ADSessionSettings SessionSettings
		{
			get
			{
				return this.sessionSettings;
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000EC90 File Offset: 0x0000CE90
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

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000ECF5 File Offset: 0x0000CEF5
		protected virtual OrganizationId ResolveCurrentOrganization()
		{
			return base.CurrentOrganizationId ?? base.ExecutingUserOrganizationId;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000ED07 File Offset: 0x0000CF07
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			base.CurrentOrganizationId = this.ResolveCurrentOrganization();
			TaskLogger.LogExit();
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000ED25 File Offset: 0x0000CF25
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, this.SessionSettings, 839, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\GetAdObjectTask.cs");
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000ED53 File Offset: 0x0000CF53
		protected override void InternalEndProcessing()
		{
			base.InternalEndProcessing();
			this.sessionSettings = null;
		}

		// Token: 0x040000F9 RID: 249
		private ADSessionSettings sessionSettings;
	}
}
