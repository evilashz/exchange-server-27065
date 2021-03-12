using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.CmdletInfra;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000071 RID: 113
	public abstract class NewTenantADTaskBase<TDataObject> : NewTaskBase<TDataObject> where TDataObject : IConfigurable, new()
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x0000FFEF File Offset: 0x0000E1EF
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x0000FFF7 File Offset: 0x0000E1F7
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

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00010000 File Offset: 0x0000E200
		internal ADSessionSettings SessionSettings
		{
			get
			{
				return this.sessionSettings;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00010008 File Offset: 0x0000E208
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x00010010 File Offset: 0x0000E210
		internal LazilyInitialized<SharedTenantConfigurationState> CurrentOrgState { get; set; }

		// Token: 0x06000479 RID: 1145 RVA: 0x0001001C File Offset: 0x0000E21C
		protected override void InternalStateReset()
		{
			using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "ADSessionSettings.FromCustomScopeSet", LoggerHelper.CmdletPerfMonitors))
			{
				this.sessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			}
			base.InternalStateReset();
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0001009D File Offset: 0x0000E29D
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			base.CurrentOrganizationId = this.ResolveCurrentOrganization();
			this.CurrentOrgState = new LazilyInitialized<SharedTenantConfigurationState>(() => SharedConfiguration.GetSharedConfigurationState(base.CurrentOrganizationId));
			TaskLogger.LogExit();
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000100D2 File Offset: 0x0000E2D2
		protected virtual OrganizationId ResolveCurrentOrganization()
		{
			return base.CurrentOrganizationId ?? base.ExecutingUserOrganizationId;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x000100E4 File Offset: 0x0000E2E4
		protected override void InternalEndProcessing()
		{
			base.InternalEndProcessing();
			this.sessionSettings = null;
		}

		// Token: 0x04000114 RID: 276
		private ADSessionSettings sessionSettings;
	}
}
