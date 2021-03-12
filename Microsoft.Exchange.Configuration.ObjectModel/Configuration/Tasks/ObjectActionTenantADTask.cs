using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200003E RID: 62
	public abstract class ObjectActionTenantADTask<TIdentity, TDataObject> : ObjectActionTask<TIdentity, TDataObject> where TIdentity : IIdentityParameter, new() where TDataObject : IConfigurable, new()
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000BBCF File Offset: 0x00009DCF
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000BBD7 File Offset: 0x00009DD7
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

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000BBE0 File Offset: 0x00009DE0
		internal ADSessionSettings SessionSettings
		{
			get
			{
				return this.sessionSettings;
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000BBE8 File Offset: 0x00009DE8
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			this.ResolveCurrentOrgIdBasedOnIdentity(this.Identity);
			TaskLogger.LogExit();
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000BC08 File Offset: 0x00009E08
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			this.ResolveCurrentOrgIdBasedOnIdentity(this.Identity);
			this.sessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			base.InternalStateReset();
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000BC55 File Offset: 0x00009E55
		protected override void InternalEndProcessing()
		{
			base.InternalEndProcessing();
			this.sessionSettings = null;
		}

		// Token: 0x040000B9 RID: 185
		private ADSessionSettings sessionSettings;
	}
}
