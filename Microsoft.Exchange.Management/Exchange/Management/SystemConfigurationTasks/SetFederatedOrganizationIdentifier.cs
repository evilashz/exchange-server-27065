using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009F6 RID: 2550
	[Cmdlet("Set", "FederatedOrganizationIdentifier", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetFederatedOrganizationIdentifier : SetFederatedOrganizationIdBase
	{
		// Token: 0x17001B47 RID: 6983
		// (get) Token: 0x06005B29 RID: 23337 RVA: 0x0017D687 File Offset: 0x0017B887
		// (set) Token: 0x06005B2A RID: 23338 RVA: 0x0017D69E File Offset: 0x0017B89E
		[Parameter(Mandatory = false)]
		public FederationTrustIdParameter DelegationFederationTrust
		{
			get
			{
				return base.Fields["DelegationFederationTrust"] as FederationTrustIdParameter;
			}
			set
			{
				base.Fields["DelegationFederationTrust"] = value;
			}
		}

		// Token: 0x17001B48 RID: 6984
		// (get) Token: 0x06005B2B RID: 23339 RVA: 0x0017D6B1 File Offset: 0x0017B8B1
		// (set) Token: 0x06005B2C RID: 23340 RVA: 0x0017D6C8 File Offset: 0x0017B8C8
		[Parameter(Mandatory = false)]
		public SmtpDomain AccountNamespace
		{
			get
			{
				return base.Fields["AccountNamespace"] as SmtpDomain;
			}
			set
			{
				base.Fields["AccountNamespace"] = value;
			}
		}

		// Token: 0x17001B49 RID: 6985
		// (get) Token: 0x06005B2D RID: 23341 RVA: 0x0017D6DB File Offset: 0x0017B8DB
		// (set) Token: 0x06005B2E RID: 23342 RVA: 0x0017D6F2 File Offset: 0x0017B8F2
		[Parameter(Mandatory = false)]
		public SmtpDomain DefaultDomain
		{
			get
			{
				return base.Fields["DefaultDomain"] as SmtpDomain;
			}
			set
			{
				base.Fields["DefaultDomain"] = value;
			}
		}

		// Token: 0x17001B4A RID: 6986
		// (get) Token: 0x06005B2F RID: 23343 RVA: 0x0017D705 File Offset: 0x0017B905
		// (set) Token: 0x06005B30 RID: 23344 RVA: 0x0017D71C File Offset: 0x0017B91C
		[Parameter(Mandatory = false)]
		public bool Enabled
		{
			get
			{
				return (bool)base.Fields["Enabled"];
			}
			set
			{
				base.Fields["Enabled"] = value;
			}
		}

		// Token: 0x17001B4B RID: 6987
		// (get) Token: 0x06005B31 RID: 23345 RVA: 0x0017D734 File Offset: 0x0017B934
		// (set) Token: 0x06005B32 RID: 23346 RVA: 0x0017D74B File Offset: 0x0017B94B
		[Parameter(Mandatory = false)]
		public SmtpAddress OrganizationContact
		{
			get
			{
				return (SmtpAddress)base.Fields["OrganizationContact"];
			}
			set
			{
				base.Fields["OrganizationContact"] = value;
			}
		}

		// Token: 0x06005B33 RID: 23347 RVA: 0x0017D764 File Offset: 0x0017B964
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			this.noTrustToUpdate = false;
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (base.Fields.IsModified("Enabled"))
			{
				this.DataObject.Enabled = (bool)base.Fields["Enabled"];
			}
			if (base.Fields.IsModified("OrganizationContact"))
			{
				this.DataObject.OrganizationContact = (SmtpAddress)base.Fields["OrganizationContact"];
			}
			this.ValidateOrganizationContactParameter();
			this.ValidateAccountNamespaceParameter();
			this.ValidateDefaultDomainParameter();
			this.ValidateDelegationFederationTrustParameter();
			this.ValidateSetParameters();
			this.ValidateSetParameters();
		}

		// Token: 0x06005B34 RID: 23348 RVA: 0x0017D814 File Offset: 0x0017BA14
		protected override void InternalProcessRecord()
		{
			if (base.Fields.IsModified("DefaultDomain"))
			{
				IEnumerable<AcceptedDomain> enumerable = base.DataSession.FindPaged<AcceptedDomain>(null, base.CurrentOrgContainerId, true, null, 1000);
				foreach (AcceptedDomain acceptedDomain in enumerable)
				{
					if (acceptedDomain.IsDefaultFederatedDomain && (this.matchedDefaultAcceptedDomain == null || !this.matchedDefaultAcceptedDomain.Id.Equals(acceptedDomain.Id)))
					{
						acceptedDomain.IsDefaultFederatedDomain = false;
						base.DataSession.Save(acceptedDomain);
					}
				}
				if (this.matchedDefaultAcceptedDomain != null && !this.matchedDefaultAcceptedDomain.IsDefaultFederatedDomain)
				{
					this.matchedDefaultAcceptedDomain.IsDefaultFederatedDomain = true;
					base.DataSession.Save(this.matchedDefaultAcceptedDomain);
					this.defaultDomainChanged = true;
				}
			}
			if (this.noTrustToUpdate)
			{
				if (this.IsObjectStateChanged())
				{
					base.InternalProcessRecord();
					return;
				}
			}
			else
			{
				base.ZapDanglingDomainTrusts();
				this.ProvisionSTS();
				SmtpDomain smtpDomain = this.AccountNamespace;
				if (this.federationTrust.NamespaceProvisioner == FederationTrust.NamespaceProvisionerType.LiveDomainServices2)
				{
					smtpDomain = new SmtpDomain(FederatedOrganizationId.AddHybridConfigurationWellKnownSubDomain(this.AccountNamespace.Domain));
				}
				this.DataObject.AccountNamespace = smtpDomain;
				this.DataObject.DelegationTrustLink = this.delegationTrustId;
				if (!this.DataObject.IsModified(FederatedOrganizationIdSchema.Enabled))
				{
					this.DataObject.Enabled = true;
				}
				bool flag = false;
				Uri applicationUri = null;
				if (null == this.federationTrust.ApplicationUri)
				{
					if (!Uri.TryCreate(smtpDomain.Domain, UriKind.RelativeOrAbsolute, out applicationUri))
					{
						base.WriteError(new InvalidApplicationUriException(Strings.ErrorInvalidApplicationUri(this.AccountNamespace.Domain)), ErrorCategory.InvalidArgument, this.DataObject.Identity);
					}
					flag = true;
				}
				base.InternalProcessRecord();
				if (flag)
				{
					this.federationTrust.ApplicationUri = applicationUri;
					base.DataSession.Save(this.federationTrust);
				}
				AcceptedDomain acceptedDomain2 = (AcceptedDomain)base.DataSession.Read<AcceptedDomain>(this.matchedAcceptedDomain.Id);
				acceptedDomain2.FederatedOrganizationLink = this.DataObject.Id;
				acceptedDomain2.PendingFederatedAccountNamespace = false;
				acceptedDomain2.PendingFederatedDomain = false;
				base.DataSession.Save(acceptedDomain2);
			}
		}

		// Token: 0x06005B35 RID: 23349 RVA: 0x0017DA54 File Offset: 0x0017BC54
		protected override bool IsObjectStateChanged()
		{
			return this.defaultDomainChanged || base.IsObjectStateChanged();
		}

		// Token: 0x06005B36 RID: 23350 RVA: 0x0017DA68 File Offset: 0x0017BC68
		private void ValidateSetParameters()
		{
			ADObjectId adobjectId = ADObjectIdResolutionHelper.ResolveDN(this.DataObject.DelegationTrustLink);
			if (adobjectId != null && (string.IsNullOrEmpty(adobjectId.DistinguishedName) || adobjectId.Parent.Equals(this.ConfigurationSession.DeletedObjectsContainer)))
			{
				adobjectId = null;
			}
			if (this.DelegationFederationTrust == null && this.AccountNamespace == null)
			{
				if (adobjectId == null)
				{
					base.WriteError(new NoTrustConfiguredException(), ErrorCategory.InvalidOperation, this.DataObject.Identity);
				}
				this.noTrustToUpdate = true;
				return;
			}
			if (this.DelegationFederationTrust != null)
			{
				if (this.AccountNamespace == null)
				{
					if (adobjectId != null)
					{
						return;
					}
					base.WriteError(new NoAccountNamespaceException(), ErrorCategory.InvalidOperation, this.DataObject.Identity);
				}
			}
			else
			{
				base.WriteError(new CannotSpecifyAccountNamespaceWithoutTrustException(), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			if (this.DataObject.AccountNamespace != null)
			{
				string x = FederatedOrganizationId.ContainsHybridConfigurationWellKnownSubDomain(this.DataObject.AccountNamespace.Domain) ? FederatedOrganizationId.AddHybridConfigurationWellKnownSubDomain(this.AccountNamespace.Domain) : this.AccountNamespace.Domain;
				if (!StringComparer.OrdinalIgnoreCase.Equals(x, this.DataObject.AccountNamespace.Domain) && adobjectId != null)
				{
					base.WriteError(new TrustAlreadyDefinedException(), ErrorCategory.InvalidOperation, this.DataObject.Identity);
				}
				if (adobjectId != null && !adobjectId.Equals(this.delegationTrustId))
				{
					base.WriteError(new TrustAlreadyDefinedException(), ErrorCategory.InvalidOperation, this.DataObject.Identity);
				}
			}
			ADObjectId adobjectId2 = this.matchedAcceptedDomain.FederatedOrganizationLink;
			if (adobjectId2 != null && adobjectId2.Parent.Equals(this.ConfigurationSession.DeletedObjectsContainer))
			{
				adobjectId2 = null;
			}
			if (adobjectId2 != null)
			{
				if (adobjectId2.ObjectGuid != this.DataObject.Id.ObjectGuid)
				{
					base.WriteError(new DomainAlreadyFederatedException(this.AccountNamespace.Domain), ErrorCategory.InvalidOperation, this.DataObject.Identity);
				}
				else
				{
					this.noTrustToUpdate = true;
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005B37 RID: 23351 RVA: 0x0017DC44 File Offset: 0x0017BE44
		private void ValidateDelegationFederationTrustParameter()
		{
			if (this.DelegationFederationTrust != null)
			{
				ADObjectId descendantId = base.RootOrgContainerId.GetDescendantId(FederationTrust.FederationTrustsContainer);
				IConfigDataProvider configDataProvider = ((IConfigurationSession)base.DataSession).SessionSettings.PartitionId.ForestFQDN.Equals(TopologyProvider.LocalForestFqdn, StringComparison.OrdinalIgnoreCase) ? base.DataSession : base.RootOrgGlobalConfigSession;
				IEnumerable<FederationTrust> objects = this.DelegationFederationTrust.GetObjects<FederationTrust>(descendantId, configDataProvider);
				ADObjectId identity = null;
				using (IEnumerator<FederationTrust> enumerator = objects.GetEnumerator())
				{
					if (!enumerator.MoveNext())
					{
						base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorFederationTrustNotFound(this.DelegationFederationTrust.ToString())), ErrorCategory.ObjectNotFound, null);
					}
					identity = (ADObjectId)enumerator.Current.Identity;
					if (enumerator.MoveNext())
					{
						base.WriteError(new ManagementObjectAmbiguousException(Strings.ErrorFederationTrustNotUnique(this.DelegationFederationTrust.ToString())), ErrorCategory.InvalidData, null);
					}
				}
				FederationTrust federationTrust = (FederationTrust)configDataProvider.Read<FederationTrust>(identity);
				if (federationTrust == null)
				{
					base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorFederationTrustNotFound(this.delegationTrustId.ToDNString())), ErrorCategory.ObjectNotFound, null);
				}
				this.delegationTrustId = identity;
				this.federationTrust = federationTrust;
			}
		}

		// Token: 0x06005B38 RID: 23352 RVA: 0x0017DD78 File Offset: 0x0017BF78
		private void ValidateAccountNamespaceParameter()
		{
			if (this.AccountNamespace != null)
			{
				this.matchedAcceptedDomain = base.GetAcceptedDomain(this.AccountNamespace, false);
			}
		}

		// Token: 0x06005B39 RID: 23353 RVA: 0x0017DD95 File Offset: 0x0017BF95
		private void ValidateDefaultDomainParameter()
		{
			if (this.DefaultDomain != null)
			{
				this.matchedDefaultAcceptedDomain = base.GetAcceptedDomain(this.DefaultDomain, false);
			}
		}

		// Token: 0x06005B3A RID: 23354 RVA: 0x0017DDB2 File Offset: 0x0017BFB2
		private void ValidateOrganizationContactParameter()
		{
		}

		// Token: 0x06005B3B RID: 23355 RVA: 0x0017DDB4 File Offset: 0x0017BFB4
		private void ProvisionSTS()
		{
			FederationProvision federationProvision = FederationProvision.Create(this.federationTrust, this);
			try
			{
				federationProvision.OnSetFederatedOrganizationIdentifier(this.federationTrust, this.AccountNamespace);
			}
			catch (LocalizedException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidResult, null);
			}
			if (this.federationTrust.ObjectState == ObjectState.Changed)
			{
				base.DataSession.Save(this.federationTrust);
			}
		}

		// Token: 0x17001B4C RID: 6988
		// (get) Token: 0x06005B3C RID: 23356 RVA: 0x0017DE20 File Offset: 0x0017C020
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				LocalizedString result = LocalizedString.Empty;
				if (this.AccountNamespace != null)
				{
					result = Strings.ConfirmationMessageSetFederatedOrganizationIdentifier1(base.CurrentOrgContainerId.Name, this.AccountNamespace.Domain, this.delegationTrustId.Name);
				}
				else if (this.DataObject.IsChanged(FederatedOrganizationIdSchema.OrganizationContact))
				{
					result = Strings.ConfirmationMessageSetFederatedOrganizationIdentifier2(base.CurrentOrgContainerId.Name, this.DataObject.OrganizationContact.ToString());
				}
				else if (this.DataObject.IsChanged(FederatedOrganizationIdSchema.Enabled))
				{
					result = (this.DataObject.Enabled ? Strings.ConfirmationMessageEnableFederatedOrgId(base.CurrentOrgContainerId.Name) : Strings.ConfirmationMessageDisableFederatedOrgId(base.CurrentOrgContainerId.Name));
				}
				return result;
			}
		}

		// Token: 0x040033E8 RID: 13288
		private AcceptedDomain matchedAcceptedDomain;

		// Token: 0x040033E9 RID: 13289
		private AcceptedDomain matchedDefaultAcceptedDomain;

		// Token: 0x040033EA RID: 13290
		private bool defaultDomainChanged;

		// Token: 0x040033EB RID: 13291
		private bool noTrustToUpdate;

		// Token: 0x040033EC RID: 13292
		private ADObjectId delegationTrustId;

		// Token: 0x040033ED RID: 13293
		private FederationTrust federationTrust;
	}
}
