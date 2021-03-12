using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x0200091D RID: 2333
	internal class StandardWorkflow : WorkflowBase, IDisposable
	{
		// Token: 0x060052F8 RID: 21240 RVA: 0x00156CE8 File Offset: 0x00154EE8
		public StandardWorkflow(ILogger logger, IUserInterface ui, HybridConfiguration hybridConfigurationObject, Func<IOnPremisesSession> createOnPremisesSession, Func<ITenantSession> createTenantSession)
		{
			base.AddOverhead(1);
			base.AddOverhead(1);
			base.AddTask(new TenantDetectionTask());
			base.AddTask(new UpgradeConfigurationFrom14Task(ExchangeObjectVersion.Exchange2012));
			base.AddTask(new GlobalPrereqTask());
			base.AddTask(new RecipientTask());
			base.AddTask(new OrganizationRelationshipTask());
			base.AddTask(new OnOffSettingsTask());
			base.AddTask(new MailFlowTask());
			base.AddTask(new MRSProxyTask());
			base.AddTask(new IOCConfigurationTask());
			LocalizedString hybridActivityEstablish = HybridStrings.HybridActivityEstablish;
			LocalizedString localizedString = HybridStrings.HybridConnectingToOnPrem;
			ui.WriteVerbose(localizedString);
			ui.WriteProgessIndicator(hybridActivityEstablish, localizedString, base.PercentCompleted);
			try
			{
				this.onPremisesSession = createOnPremisesSession();
			}
			catch (Exception ex)
			{
				if (ex is CouldNotResolveServerException || ex is CouldNotOpenRunspaceException)
				{
					throw new CouldNotCreateOnPremisesSessionException(ex, ex);
				}
				throw ex;
			}
			base.UpdateProgress(1);
			logger.LogInformation(HybridStrings.HybridInfoConnectedToOnPrem);
			localizedString = HybridStrings.HybridConnectingToTenant;
			ui.WriteVerbose(localizedString);
			ui.WriteProgessIndicator(hybridActivityEstablish, localizedString, base.PercentCompleted);
			try
			{
				this.tenantSession = createTenantSession();
			}
			catch (Exception ex2)
			{
				if (ex2 is CouldNotResolveServerException || ex2 is CouldNotOpenRunspaceException)
				{
					throw new CouldNotCreateTenantSessionException(ex2, ex2);
				}
				throw ex2;
			}
			base.UpdateProgress(1);
			logger.LogInformation(HybridStrings.HybridInfoConnectedToTenant);
			this.TaskContext = new TaskContext(ui, logger, hybridConfigurationObject, this.onPremisesSession, this.tenantSession);
			this.TaskContext.Parameters.Set<List<Uri>>("_onPremAcceptedTokenIssuerUris", Configuration.OnPremiseAcceptedTokenIssuerUriList);
			this.TaskContext.Parameters.Set<List<Uri>>("_tenantAcceptedTokenIssuerUris", Configuration.TenantAcceptedTokenIssuerUriList);
		}

		// Token: 0x170018E3 RID: 6371
		// (get) Token: 0x060052F9 RID: 21241 RVA: 0x00156E9C File Offset: 0x0015509C
		// (set) Token: 0x060052FA RID: 21242 RVA: 0x00156EA4 File Offset: 0x001550A4
		public ITaskContext TaskContext { get; private set; }

		// Token: 0x060052FB RID: 21243 RVA: 0x00156EAD File Offset: 0x001550AD
		public void Dispose()
		{
			if (this.onPremisesSession != null)
			{
				this.onPremisesSession.Dispose();
				this.onPremisesSession = null;
			}
			if (this.tenantSession != null)
			{
				this.tenantSession.Dispose();
				this.tenantSession = null;
			}
		}

		// Token: 0x0400302D RID: 12333
		private const int OnPremisesSessionWeight = 1;

		// Token: 0x0400302E RID: 12334
		private const int TenantSessionWeight = 1;

		// Token: 0x0400302F RID: 12335
		private IOnPremisesSession onPremisesSession;

		// Token: 0x04003030 RID: 12336
		private ITenantSession tenantSession;
	}
}
