using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009F2 RID: 2546
	[Cmdlet("Remove", "FederatedDomain", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class RemoveFederatedDomain : SetFederatedOrganizationIdBase
	{
		// Token: 0x17001B40 RID: 6976
		// (get) Token: 0x06005B13 RID: 23315 RVA: 0x0017CDCE File Offset: 0x0017AFCE
		// (set) Token: 0x06005B14 RID: 23316 RVA: 0x0017CDE5 File Offset: 0x0017AFE5
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

		// Token: 0x17001B41 RID: 6977
		// (get) Token: 0x06005B15 RID: 23317 RVA: 0x0017CDF8 File Offset: 0x0017AFF8
		// (set) Token: 0x06005B16 RID: 23318 RVA: 0x0017CE00 File Offset: 0x0017B000
		[Parameter(Mandatory = false)]
		public SwitchParameter Force { get; set; }

		// Token: 0x17001B42 RID: 6978
		// (get) Token: 0x06005B17 RID: 23319 RVA: 0x0017CE09 File Offset: 0x0017B009
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveFederatedDomain(this.DomainName.Domain, base.CurrentOrgContainerId.Name);
			}
		}

		// Token: 0x06005B18 RID: 23320 RVA: 0x0017CE26 File Offset: 0x0017B026
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			this.ValidateParameters();
		}

		// Token: 0x06005B19 RID: 23321 RVA: 0x0017CE44 File Offset: 0x0017B044
		protected override void InternalProcessRecord()
		{
			try
			{
				this.ProcessRemoveFederatedDomainRequest();
			}
			catch (NullReferenceException ex)
			{
				string text = "NoMatchedAcceptedDomain";
				string text2 = "Unknown";
				if (this.matchedAcceptedDomain != null)
				{
					text = this.matchedAcceptedDomain.Id.ToString();
					text2 = ((this.matchedAcceptedDomain.FederatedOrganizationLink != null) ? this.matchedAcceptedDomain.FederatedOrganizationLink.ToString() : "NotFederated");
				}
				StringBuilder stringBuilder = new StringBuilder(1024);
				if (base.FederatedAcceptedDomains != null)
				{
					foreach (ADObjectId adobjectId in base.FederatedAcceptedDomains)
					{
						stringBuilder.AppendFormat("{0};", adobjectId.ToString());
					}
				}
				string message = string.Format("{0}:{1}:{2}:{3}", new object[]
				{
					ex.ToString(),
					text,
					text2,
					stringBuilder.ToString()
				});
				base.WriteError(new LocalizedException(Strings.ExceptionOccured(message)), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
		}

		// Token: 0x06005B1A RID: 23322 RVA: 0x0017CF70 File Offset: 0x0017B170
		private void ProcessRemoveFederatedDomainRequest()
		{
			this.RemoveFederatedDomainFromSTS();
			if (this.DomainName.Equals(this.DataObject.AccountNamespace) && !this.IsDatacenter)
			{
				string domain = this.DataObject.AccountNamespace.Domain;
				base.ZapDanglingDomainTrusts();
				this.DataObject.AccountNamespace = null;
				this.DataObject.DelegationTrustLink = null;
				this.DataObject.Enabled = false;
				if (this.federationTrust != null && null != this.federationTrust.ApplicationUri)
				{
					string text = FederatedOrganizationId.RemoveHybridConfigurationWellKnownSubDomain(this.federationTrust.ApplicationUri.ToString());
					if (text.Equals(domain, StringComparison.InvariantCultureIgnoreCase))
					{
						this.federationTrust.ApplicationUri = null;
						this.federationTrust.ApplicationIdentifier = null;
						base.DataSession.Save(this.federationTrust);
					}
				}
				base.InternalProcessRecord();
				return;
			}
			if (this.matchedAcceptedDomain != null && this.matchedAcceptedDomain.Id != null)
			{
				AcceptedDomain acceptedDomain = (AcceptedDomain)base.DataSession.Read<AcceptedDomain>(this.matchedAcceptedDomain.Id);
				if (acceptedDomain == null)
				{
					this.WriteWarning(Strings.ErrorDomainNameNotAcceptedDomain(this.DomainName.Domain));
					return;
				}
				if (acceptedDomain.FederatedOrganizationLink == null)
				{
					this.WriteWarning(Strings.ErrorDomainIsNotFederated(this.DomainName.Domain));
					return;
				}
				acceptedDomain.FederatedOrganizationLink = null;
				base.DataSession.Save(acceptedDomain);
			}
		}

		// Token: 0x06005B1B RID: 23323 RVA: 0x0017D0CC File Offset: 0x0017B2CC
		private void ValidateParameters()
		{
			if (this.DomainName == null || string.IsNullOrEmpty(this.DomainName.Domain))
			{
				base.WriteError(new NoAccountNamespaceException(), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			if (!this.IsDatacenter && (this.DataObject.DelegationTrustLink == null || this.DataObject.AccountNamespace == null || string.IsNullOrEmpty(this.DataObject.AccountNamespace.Domain)))
			{
				base.WriteError(new NoTrustConfiguredException(), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			if (this.DataObject.DelegationTrustLink == null)
			{
				this.federationTrust = null;
			}
			else
			{
				ADObjectId deletedObjectsContainer = this.ConfigurationSession.DeletedObjectsContainer;
				ADObjectId adobjectId = ADObjectIdResolutionHelper.ResolveDN(this.DataObject.DelegationTrustLink);
				if (adobjectId != null)
				{
					if (adobjectId.Parent.Equals(deletedObjectsContainer))
					{
						this.WriteWarning(Strings.ErrorFederationTrustNotFound(adobjectId.ToDNString()));
						this.federationTrust = null;
					}
					else
					{
						IConfigDataProvider configDataProvider = this.IsDatacenter ? base.GlobalConfigSession : base.DataSession;
						this.federationTrust = (configDataProvider.Read<FederationTrust>(adobjectId) as FederationTrust);
						if (this.federationTrust == null)
						{
							this.WriteWarning(Strings.ErrorFederationTrustNotFound(adobjectId.ToDNString()));
						}
					}
				}
				else
				{
					this.WriteWarning(Strings.ErrorFederationTrustNotFound(this.DataObject.DelegationTrustLink.ToDNString()));
					this.federationTrust = null;
				}
			}
			if (!this.IsDatacenter && this.DomainName.Equals(this.DataObject.AccountNamespace) && 1 < base.FederatedAcceptedDomains.Count)
			{
				base.WriteError(new CannotRemoveAccountNamespaceException(this.DomainName.Domain), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			this.matchedAcceptedDomain = base.GetAcceptedDomain(this.DomainName, true);
			if (this.matchedAcceptedDomain.FederatedOrganizationLink == null && !this.DomainName.Equals(this.DataObject.AccountNamespace))
			{
				if (this.Force || this.IsDatacenter)
				{
					this.WriteWarning(Strings.ErrorDomainIsNotFederated(this.DomainName.Domain));
				}
				else
				{
					base.WriteError(new DomainIsNotFederatedException(this.DomainName.Domain), ErrorCategory.InvalidOperation, this.DataObject.Identity);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005B1C RID: 23324 RVA: 0x0017D300 File Offset: 0x0017B500
		private void RemoveFederatedDomainFromSTS()
		{
			if (this.federationTrust == null)
			{
				return;
			}
			FederationProvision federationProvision = FederationProvision.Create(this.federationTrust, this);
			try
			{
				if (this.DomainName.Equals(this.DataObject.AccountNamespace))
				{
					federationProvision.OnRemoveAccountNamespace(this.DataObject.AccountNamespaceWithWellKnownSubDomain, this.Force);
				}
				else
				{
					federationProvision.OnRemoveFederatedDomain(this.DomainName, this.Force);
				}
			}
			catch (LocalizedException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidResult, null);
			}
		}

		// Token: 0x17001B43 RID: 6979
		// (get) Token: 0x06005B1D RID: 23325 RVA: 0x0017D390 File Offset: 0x0017B590
		private bool IsDatacenter
		{
			get
			{
				return VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled;
			}
		}

		// Token: 0x040033E5 RID: 13285
		private AcceptedDomain matchedAcceptedDomain;

		// Token: 0x040033E6 RID: 13286
		private FederationTrust federationTrust;
	}
}
