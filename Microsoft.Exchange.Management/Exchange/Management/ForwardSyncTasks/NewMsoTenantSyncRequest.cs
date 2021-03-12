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
	// Token: 0x0200036F RID: 879
	[Cmdlet("New", "MsoTenantSyncRequest", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class NewMsoTenantSyncRequest : SetSystemConfigurationObjectTask<OrganizationIdParameter, MsoTenantSyncRequest, ExchangeConfigurationUnit>
	{
		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06001ECC RID: 7884 RVA: 0x00085704 File Offset: 0x00083904
		// (set) Token: 0x06001ECD RID: 7885 RVA: 0x0008570C File Offset: 0x0008390C
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
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

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06001ECE RID: 7886 RVA: 0x00085715 File Offset: 0x00083915
		// (set) Token: 0x06001ECF RID: 7887 RVA: 0x0008571D File Offset: 0x0008391D
		[Parameter(Mandatory = false)]
		public SwitchParameter Full { get; set; }

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06001ED0 RID: 7888 RVA: 0x00085726 File Offset: 0x00083926
		// (set) Token: 0x06001ED1 RID: 7889 RVA: 0x0008572E File Offset: 0x0008392E
		[Parameter(Mandatory = false)]
		public SwitchParameter Force { get; set; }

		// Token: 0x06001ED2 RID: 7890 RVA: 0x00085737 File Offset: 0x00083937
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is InvalidMainStreamCookieException;
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x00085750 File Offset: 0x00083950
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				string syncType = this.Full ? "Full" : "Partial";
				return Strings.ConfirmationMessageStartMsoFullSync(this.Identity.ToString(), syncType);
			}
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x00085788 File Offset: 0x00083988
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (string.IsNullOrEmpty(this.DataObject.ExternalDirectoryOrganizationId))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorNotMsoOrganization(this.DataObject.OrganizationId.ToString())), (ErrorCategory)1000, null);
			}
			this.recipientFullSyncCookieManager = new MsoRecipientFullSyncCookieManager(Guid.Parse(this.DataObject.ExternalDirectoryOrganizationId));
			this.companyFullSyncCookieManager = new MsoCompanyFullSyncCookieManager(Guid.Parse(this.DataObject.ExternalDirectoryOrganizationId));
			if (!this.Force && (this.recipientFullSyncCookieManager.ReadCookie() != null || this.companyFullSyncCookieManager.ReadCookie() != null))
			{
				base.WriteError(new ADObjectAlreadyExistsException(Strings.ErrorFullSyncInProgress(this.DataObject.ExternalDirectoryOrganizationId)), (ErrorCategory)1000, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x00085860 File Offset: 0x00083A60
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			string requestor = this.GetRequestor();
			if (this.Full)
			{
				if (base.ShouldProcess(this.ConfirmationMessage))
				{
					this.recipientFullSyncCookieManager.WriteInitialSyncCookie(TenantSyncType.Full, requestor);
					this.companyFullSyncCookieManager.WriteInitialSyncCookie(TenantSyncType.Full, requestor);
				}
			}
			else
			{
				this.recipientFullSyncCookieManager.WriteInitialSyncCookie(TenantSyncType.Partial, requestor);
				this.companyFullSyncCookieManager.WriteInitialSyncCookie(TenantSyncType.Partial, requestor);
			}
			MsoTenantCookieContainer organization = (MsoTenantCookieContainer)base.DataSession.Read<MsoTenantCookieContainer>(this.DataObject.Identity);
			MsoTenantSyncRequest sendToPipeline = new MsoTenantSyncRequest(organization, this.recipientFullSyncCookieManager.LastCookie, this.companyFullSyncCookieManager.LastCookie);
			base.WriteObject(sendToPipeline);
			TaskLogger.LogExit();
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x00085910 File Offset: 0x00083B10
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

		// Token: 0x04001949 RID: 6473
		private MsoRecipientFullSyncCookieManager recipientFullSyncCookieManager;

		// Token: 0x0400194A RID: 6474
		private MsoCompanyFullSyncCookieManager companyFullSyncCookieManager;
	}
}
