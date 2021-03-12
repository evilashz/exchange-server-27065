using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x02000775 RID: 1909
	[Cmdlet("Set", "Notification", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class SetNotification : SetTenantADTaskBase<EwsStoreObjectIdParameter, AsyncOperationNotification, AsyncOperationNotification>
	{
		// Token: 0x17001488 RID: 5256
		// (get) Token: 0x0600437A RID: 17274 RVA: 0x00114CAE File Offset: 0x00112EAE
		// (set) Token: 0x0600437B RID: 17275 RVA: 0x00114CC5 File Offset: 0x00112EC5
		[Parameter(Mandatory = true, ParameterSetName = "Settings")]
		public AsyncOperationType ProcessType
		{
			get
			{
				return (AsyncOperationType)base.Fields["ProcessType"];
			}
			set
			{
				base.Fields["ProcessType"] = value;
			}
		}

		// Token: 0x17001489 RID: 5257
		// (get) Token: 0x0600437C RID: 17276 RVA: 0x00114CDD File Offset: 0x00112EDD
		// (set) Token: 0x0600437D RID: 17277 RVA: 0x00114CF4 File Offset: 0x00112EF4
		[Parameter(Mandatory = true, ParameterSetName = "Identity")]
		[Parameter(Mandatory = true, ParameterSetName = "Settings")]
		public MultiValuedProperty<SmtpAddress> NotificationEmails
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)base.Fields["NotificationEmails"];
			}
			set
			{
				base.Fields["NotificationEmails"] = value;
			}
		}

		// Token: 0x1700148A RID: 5258
		// (get) Token: 0x0600437E RID: 17278 RVA: 0x00114D07 File Offset: 0x00112F07
		// (set) Token: 0x0600437F RID: 17279 RVA: 0x00114D1E File Offset: 0x00112F1E
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x06004380 RID: 17280 RVA: 0x00114D31 File Offset: 0x00112F31
		protected override IConfigDataProvider CreateSession()
		{
			base.CurrentOrganizationId = this.ResolveCurrentOrganization();
			return new AsyncOperationNotificationDataProvider(base.CurrentOrganizationId);
		}

		// Token: 0x1700148B RID: 5259
		// (get) Token: 0x06004381 RID: 17281 RVA: 0x00114D4A File Offset: 0x00112F4A
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetNotification(this.Identity.ToString());
			}
		}

		// Token: 0x06004382 RID: 17282 RVA: 0x00114D5C File Offset: 0x00112F5C
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (base.ParameterSetName == "Settings")
			{
				string id;
				if (AsyncOperationNotificationDataProvider.SettingsObjectIdentityMap.TryGetValue(this.ProcessType, out id))
				{
					this.Identity = new EwsStoreObjectIdParameter(id);
				}
				else
				{
					base.WriteError(new ArgumentException(Strings.ErrorInvalidAsyncNotificationProcessType(this.ProcessType.ToString())), ErrorCategory.InvalidArgument, this.ProcessType);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06004383 RID: 17283 RVA: 0x00114DE0 File Offset: 0x00112FE0
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			AsyncOperationNotification asyncOperationNotification = (AsyncOperationNotification)dataObject;
			MultiValuedProperty<ADRecipientOrAddress> multiValuedProperty = new MultiValuedProperty<ADRecipientOrAddress>();
			if (this.NotificationEmails != null)
			{
				foreach (SmtpAddress smtpAddress in this.NotificationEmails)
				{
					multiValuedProperty.Add(new ADRecipientOrAddress(new Participant(string.Empty, smtpAddress.ToString(), "SMTP")));
				}
			}
			asyncOperationNotification.NotificationEmails = multiValuedProperty;
			if (!AsyncOperationNotification.IsSettingsObjectId(asyncOperationNotification.AlternativeId) && !asyncOperationNotification.IsNotificationEmailFromTaskSent)
			{
				((AsyncOperationNotificationDataProvider)base.DataSession).SendNotificationEmail(asyncOperationNotification, true, null, true);
				asyncOperationNotification.IsNotificationEmailFromTaskSent = true;
			}
			base.StampChangesOn(dataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x00114EB0 File Offset: 0x001130B0
		private OrganizationId ResolveCurrentOrganization()
		{
			OrganizationId result;
			if (this.Organization != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 173, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\StoreTasks\\AsyncOperationNotification\\SetNotification.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
				result = adorganizationalUnit.OrganizationId;
			}
			else
			{
				result = (base.CurrentOrganizationId ?? base.ExecutingUserOrganizationId);
			}
			return result;
		}

		// Token: 0x04002A07 RID: 10759
		private const string NotificationEmailParameter = "NotificationEmails";

		// Token: 0x04002A08 RID: 10760
		private const string OrganizationParameter = "Organization";

		// Token: 0x04002A09 RID: 10761
		private const string ProcessTypeParameter = "ProcessType";

		// Token: 0x04002A0A RID: 10762
		private const string SettingsParameterSet = "Settings";
	}
}
