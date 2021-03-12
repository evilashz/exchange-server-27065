using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync.CookieManager;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x0200037C RID: 892
	[Cmdlet("Start", "MsoFullSync", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class StartMsoFullSync : SetSystemConfigurationObjectTask<OrganizationIdParameter, ExchangeConfigurationUnit>
	{
		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06001F41 RID: 8001 RVA: 0x000871A6 File Offset: 0x000853A6
		// (set) Token: 0x06001F42 RID: 8002 RVA: 0x000871AE File Offset: 0x000853AE
		[Parameter(Mandatory = false, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public override OrganizationIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06001F43 RID: 8003 RVA: 0x000871B7 File Offset: 0x000853B7
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageStartMsoFullSync((this.Identity == null) ? base.CurrentOrganizationId.ToString() : this.Identity.ToString(), "full");
			}
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x000871E3 File Offset: 0x000853E3
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(InvalidMainStreamCookieException).IsInstanceOfType(exception);
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x00087208 File Offset: 0x00085408
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (this.Identity == null && (base.CurrentOrganizationId == null || base.CurrentOrganizationId.Equals(OrganizationId.ForestWideOrgId)))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorCouldNotStartFullSyncForFirstOrg), (ErrorCategory)1000, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x00087268 File Offset: 0x00085468
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			base.InternalStateReset();
			if (this.Identity == null)
			{
				OrganizationIdParameter identity = new OrganizationIdParameter(base.CurrentOrgContainerId);
				this.Identity = identity;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x000872A0 File Offset: 0x000854A0
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (string.IsNullOrEmpty(this.DataObject.ExternalDirectoryOrganizationId))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorNotMsoOrganization(this.DataObject.OrganizationId.ToString())), (ErrorCategory)1000, null);
			}
			if (this.DataObject.OrganizationStatus != OrganizationStatus.Active)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorNotActiveOrganization(this.DataObject.OrganizationId.ToString())), (ErrorCategory)1000, null);
			}
			this.recipientFullSyncCookieManager = new MsoRecipientFullSyncCookieManager(Guid.Parse(this.DataObject.ExternalDirectoryOrganizationId));
			this.companyFullSyncCookieManager = new MsoCompanyFullSyncCookieManager(Guid.Parse(this.DataObject.ExternalDirectoryOrganizationId));
			if (this.recipientFullSyncCookieManager.ReadCookie() != null || this.companyFullSyncCookieManager.ReadCookie() != null)
			{
				base.WriteError(new ADObjectAlreadyExistsException(Strings.ErrorFullSyncInProgress(this.DataObject.ExternalDirectoryOrganizationId)), (ErrorCategory)1000, null);
			}
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x0008739C File Offset: 0x0008559C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			string requestor = this.GetRequestor() + " via Start-MsoFullSync";
			this.recipientFullSyncCookieManager.WriteInitialSyncCookie(TenantSyncType.Full, requestor);
			this.companyFullSyncCookieManager.WriteInitialSyncCookie(TenantSyncType.Full, requestor);
			TaskLogger.LogExit();
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x000873E0 File Offset: 0x000855E0
		private string GetRequestor()
		{
			ADObjectId adobjectId;
			if (base.TryGetExecutingUserId(out adobjectId))
			{
				return adobjectId.ToString();
			}
			if (base.ExchangeRunspaceConfig != null)
			{
				if (!string.IsNullOrEmpty(base.ExchangeRunspaceConfig.ExecutingUserDisplayName))
				{
					return base.ExchangeRunspaceConfig.ExecutingUserDisplayName;
				}
				if (!string.IsNullOrEmpty(base.ExchangeRunspaceConfig.IdentityName))
				{
					return base.ExchangeRunspaceConfig.IdentityName;
				}
			}
			return Environment.MachineName;
		}

		// Token: 0x04001961 RID: 6497
		private MsoRecipientFullSyncCookieManager recipientFullSyncCookieManager;

		// Token: 0x04001962 RID: 6498
		private MsoCompanyFullSyncCookieManager companyFullSyncCookieManager;
	}
}
