using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200008D RID: 141
	public abstract class SetTenantADTaskBase<TIdentity, TPublicObject, TDataObject> : SetObjectWithIdentityTaskBase<TIdentity, TPublicObject, TDataObject> where TIdentity : IIdentityParameter, new() where TPublicObject : IConfigurable, new() where TDataObject : IConfigurable, new()
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0001571A File Offset: 0x0001391A
		// (set) Token: 0x060005A4 RID: 1444 RVA: 0x00015722 File Offset: 0x00013922
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

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0001572B File Offset: 0x0001392B
		internal ADSessionSettings SessionSettings
		{
			get
			{
				return this.sessionSettings;
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00015734 File Offset: 0x00013934
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			this.ResolveCurrentOrgIdBasedOnIdentity(this.Identity);
			this.sessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			base.InternalStateReset();
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00015781 File Offset: 0x00013981
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.ResolveCurrentOrgIdBasedOnIdentity(this.Identity);
			TaskLogger.LogExit();
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x000157A4 File Offset: 0x000139A4
		protected override void InternalEndProcessing()
		{
			base.InternalEndProcessing();
			this.sessionSettings = null;
		}

		// Token: 0x04000130 RID: 304
		private ADSessionSettings sessionSettings;
	}
}
