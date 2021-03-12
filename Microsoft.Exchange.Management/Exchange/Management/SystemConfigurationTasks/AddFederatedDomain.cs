using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009D7 RID: 2519
	[Cmdlet("Add", "FederatedDomain", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class AddFederatedDomain : SetFederatedOrganizationIdBase
	{
		// Token: 0x17001AF1 RID: 6897
		// (get) Token: 0x06005A1D RID: 23069 RVA: 0x00179B34 File Offset: 0x00177D34
		// (set) Token: 0x06005A1E RID: 23070 RVA: 0x00179B4B File Offset: 0x00177D4B
		[Parameter(Mandatory = true)]
		public SmtpDomain DomainName
		{
			get
			{
				return base.Fields["DomainName"] as SmtpDomain;
			}
			set
			{
				base.Fields["DomainName"] = value;
			}
		}

		// Token: 0x06005A1F RID: 23071 RVA: 0x00179B5E File Offset: 0x00177D5E
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			this.ValidateParameters();
			bool hasErrors = base.HasErrors;
		}

		// Token: 0x06005A20 RID: 23072 RVA: 0x00179B84 File Offset: 0x00177D84
		protected override void InternalProcessRecord()
		{
			this.ProvisionSTS();
			AcceptedDomain acceptedDomain = (AcceptedDomain)base.DataSession.Read<AcceptedDomain>(this.matchedAcceptedDomain.Id);
			acceptedDomain.FederatedOrganizationLink = this.DataObject.Id;
			acceptedDomain.PendingFederatedAccountNamespace = false;
			acceptedDomain.PendingFederatedDomain = false;
			base.DataSession.Save(acceptedDomain);
		}

		// Token: 0x06005A21 RID: 23073 RVA: 0x00179BE0 File Offset: 0x00177DE0
		private void ValidateParameters()
		{
			if (this.DomainName == null || string.IsNullOrEmpty(this.DomainName.Domain))
			{
				base.WriteError(new NoAccountNamespaceException(), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			if (this.DataObject.DelegationTrustLink == null || this.DataObject.AccountNamespace == null || string.IsNullOrEmpty(this.DataObject.AccountNamespace.Domain))
			{
				base.WriteError(new NoTrustConfiguredException(), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			ADObjectId deletedObjectsContainer = base.GlobalConfigSession.DeletedObjectsContainer;
			ADObjectId adobjectId = ADObjectIdResolutionHelper.ResolveDN(this.DataObject.DelegationTrustLink);
			if (adobjectId.Parent.Equals(deletedObjectsContainer))
			{
				base.WriteError(new NoTrustConfiguredException(), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			this.federationTrust = base.GlobalConfigSession.Read<FederationTrust>(adobjectId);
			if (this.federationTrust == null)
			{
				base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorFederationTrustNotFound(adobjectId.ToDNString())), ErrorCategory.ObjectNotFound, null);
			}
			this.matchedAcceptedDomain = base.GetAcceptedDomain(this.DomainName, false);
			if (this.matchedAcceptedDomain.FederatedOrganizationLink != null && !this.matchedAcceptedDomain.FederatedOrganizationLink.Parent.Equals(deletedObjectsContainer))
			{
				base.WriteError(new DomainAlreadyFederatedException(this.DomainName.Domain), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005A22 RID: 23074 RVA: 0x00179D3C File Offset: 0x00177F3C
		private void ProvisionSTS()
		{
			FederationProvision federationProvision = FederationProvision.Create(this.federationTrust, this);
			try
			{
				federationProvision.OnAddFederatedDomain(this.DomainName);
			}
			catch (LocalizedException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidResult, null);
			}
		}

		// Token: 0x17001AF2 RID: 6898
		// (get) Token: 0x06005A23 RID: 23075 RVA: 0x00179D80 File Offset: 0x00177F80
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetFederatedDomain(this.DomainName.Domain, base.CurrentOrgContainerId.Name);
			}
		}

		// Token: 0x040033B0 RID: 13232
		private AcceptedDomain matchedAcceptedDomain;

		// Token: 0x040033B1 RID: 13233
		private FederationTrust federationTrust;
	}
}
