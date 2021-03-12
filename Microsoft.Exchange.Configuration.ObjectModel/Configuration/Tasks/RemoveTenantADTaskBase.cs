using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000084 RID: 132
	public abstract class RemoveTenantADTaskBase<TIdentity, TDataObject> : RemoveTaskBase<TIdentity, TDataObject> where TIdentity : IIdentityParameter where TDataObject : IConfigurable, new()
	{
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00014BDC File Offset: 0x00012DDC
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x00014BE4 File Offset: 0x00012DE4
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

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00014BED File Offset: 0x00012DED
		internal ADSessionSettings SessionSettings
		{
			get
			{
				return this.sessionSettings;
			}
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00014BF5 File Offset: 0x00012DF5
		protected override void InternalStateReset()
		{
			this.ResolveCurrentOrgIdBasedOnIdentity(this.Identity);
			this.sessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			base.InternalStateReset();
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00014C32 File Offset: 0x00012E32
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.ResolveCurrentOrgIdBasedOnIdentity(this.Identity);
			TaskLogger.LogExit();
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00014C55 File Offset: 0x00012E55
		protected override void InternalEndProcessing()
		{
			base.InternalEndProcessing();
			this.sessionSettings = null;
		}

		// Token: 0x0400012B RID: 299
		private ADSessionSettings sessionSettings;
	}
}
