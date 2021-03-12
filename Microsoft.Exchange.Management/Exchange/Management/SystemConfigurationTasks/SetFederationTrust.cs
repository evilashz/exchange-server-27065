using System;
using System.Management.Automation;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.FederationProvisioning;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009F7 RID: 2551
	[Cmdlet("Set", "FederationTrust", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetFederationTrust : SetSystemConfigurationObjectTask<FederationTrustIdParameter, FederationTrust>
	{
		// Token: 0x17001B4D RID: 6989
		// (get) Token: 0x06005B3E RID: 23358 RVA: 0x0017DEF0 File Offset: 0x0017C0F0
		// (set) Token: 0x06005B3F RID: 23359 RVA: 0x0017DEF8 File Offset: 0x0017C0F8
		[Parameter(Mandatory = true, ParameterSetName = "ApplicationUri", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "PublishFederationCertificate", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public override FederationTrustIdParameter Identity
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

		// Token: 0x17001B4E RID: 6990
		// (get) Token: 0x06005B40 RID: 23360 RVA: 0x0017DF01 File Offset: 0x0017C101
		// (set) Token: 0x06005B41 RID: 23361 RVA: 0x0017DF09 File Offset: 0x0017C109
		[Parameter(Mandatory = true, ParameterSetName = "ApplicationUri")]
		public string ApplicationUri { get; set; }

		// Token: 0x17001B4F RID: 6991
		// (get) Token: 0x06005B42 RID: 23362 RVA: 0x0017DF12 File Offset: 0x0017C112
		// (set) Token: 0x06005B43 RID: 23363 RVA: 0x0017DF1A File Offset: 0x0017C11A
		[Parameter(Mandatory = true, ParameterSetName = "PublishFederationCertificate")]
		public SwitchParameter PublishFederationCertificate { get; set; }

		// Token: 0x17001B50 RID: 6992
		// (get) Token: 0x06005B44 RID: 23364 RVA: 0x0017DF23 File Offset: 0x0017C123
		// (set) Token: 0x06005B45 RID: 23365 RVA: 0x0017DF2B File Offset: 0x0017C12B
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public string Thumbprint { get; set; }

		// Token: 0x17001B51 RID: 6993
		// (get) Token: 0x06005B46 RID: 23366 RVA: 0x0017DF34 File Offset: 0x0017C134
		// (set) Token: 0x06005B47 RID: 23367 RVA: 0x0017DF3C File Offset: 0x0017C13C
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public Uri MetadataUrl { get; set; }

		// Token: 0x17001B52 RID: 6994
		// (get) Token: 0x06005B48 RID: 23368 RVA: 0x0017DF45 File Offset: 0x0017C145
		// (set) Token: 0x06005B49 RID: 23369 RVA: 0x0017DF4D File Offset: 0x0017C14D
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public SwitchParameter RefreshMetadata { get; set; }

		// Token: 0x17001B53 RID: 6995
		// (get) Token: 0x06005B4A RID: 23370 RVA: 0x0017DF58 File Offset: 0x0017C158
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				LocalizedString result;
				if (this.RefreshMetadata)
				{
					result = Strings.ConfirmationMessageSetFederationTrust4(this.DataObject.Name, this.MetadataUrlToUse.AbsoluteUri);
				}
				else if (null != this.MetadataUrl && !string.IsNullOrEmpty(this.MetadataUrl.AbsoluteUri))
				{
					if (this.Thumbprint != null)
					{
						result = Strings.ConfirmationMessageSetFederationTrust1(this.DataObject.Name, this.Thumbprint, this.MetadataUrl.AbsoluteUri);
					}
					else
					{
						result = Strings.ConfirmationMessageSetFederationTrust3(this.DataObject.Name, this.MetadataUrl.AbsoluteUri);
					}
				}
				else if (this.Thumbprint != null)
				{
					result = Strings.ConfirmationMessageSetFederationTrust2(this.DataObject.Name, this.Thumbprint);
				}
				else if (!string.IsNullOrEmpty(this.ApplicationUri))
				{
					result = Strings.ConfirmationMessageSetFederationTrust5(this.DataObject.Name, this.ApplicationUri);
				}
				else
				{
					result = LocalizedString.Empty;
				}
				return result;
			}
		}

		// Token: 0x06005B4B RID: 23371 RVA: 0x0017E04C File Offset: 0x0017C24C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				base.InternalValidate();
				if (!base.HasErrors)
				{
					this.InternalValidateInternal();
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06005B4C RID: 23372 RVA: 0x0017E08C File Offset: 0x0017C28C
		private void InternalValidateInternal()
		{
			if (!string.IsNullOrEmpty(this.Thumbprint) && this.DataObject.ApplicationUri == null)
			{
				base.WriteError(new CannotUpdateCertificateWhenFederationNotProvisionedException(), ErrorCategory.InvalidArgument, null);
			}
			if (this.PublishFederationCertificate && string.IsNullOrEmpty(this.DataObject.OrgNextPrivCertificate))
			{
				base.WriteError(new NoNextCertificateException(), ErrorCategory.InvalidArgument, null);
			}
			if (this.MetadataUrl != null)
			{
				if (!this.MetadataUrl.IsAbsoluteUri)
				{
					base.WriteError(new MetadataMustBeAbsoluteUrlException(), ErrorCategory.InvalidArgument, null);
				}
				if (this.RefreshMetadata)
				{
					base.WriteError(new RefreshMetadataOptionNotAllowedException(), ErrorCategory.InvalidArgument, null);
				}
			}
			if (this.MetadataUrlChanged || this.RefreshMetadata)
			{
				this.UpdateFederationMetadata();
			}
			if (!string.IsNullOrEmpty(this.Thumbprint))
			{
				try
				{
					this.ValidateNextCertificate();
				}
				catch (LocalizedException exception)
				{
					base.WriteError(exception, ErrorCategory.InvalidArgument, null);
				}
			}
			if (this.ApplicationUri != null && !Uri.TryCreate(this.ApplicationUri, UriKind.RelativeOrAbsolute, out this.applicationUri))
			{
				base.WriteError(new InvalidApplicationUriException(Strings.ErrorInvalidApplicationUri(this.ApplicationUri)), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06005B4D RID: 23373 RVA: 0x0017E1BC File Offset: 0x0017C3BC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				this.InternalProcessRecordInternal();
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06005B4E RID: 23374 RVA: 0x0017E1EC File Offset: 0x0017C3EC
		private void InternalProcessRecordInternal()
		{
			if (this.PublishFederationCertificate)
			{
				FederationProvision federationProvision = FederationProvision.Create(this.DataObject, this);
				try
				{
					federationProvision.OnPublishFederationCertificate(this.DataObject);
				}
				catch (LocalizedException exception)
				{
					base.WriteError(exception, ErrorCategory.InvalidResult, null);
				}
			}
			if (null != this.applicationUri)
			{
				this.DataObject.ApplicationUri = this.applicationUri;
			}
			if (this.Thumbprint != null)
			{
				if (!StringComparer.InvariantCultureIgnoreCase.Equals(this.DataObject.OrgNextPrivCertificate, this.Thumbprint))
				{
					this.DataObject.OrgNextCertificate = this.nextCertificate;
					this.DataObject.OrgNextPrivCertificate = this.Thumbprint;
					try
					{
						FederationCertificate.PushCertificate(new Task.TaskProgressLoggingDelegate(base.WriteProgress), new Task.TaskWarningLoggingDelegate(this.WriteWarning), this.Thumbprint);
					}
					catch (InvalidOperationException exception2)
					{
						base.WriteError(exception2, ErrorCategory.InvalidArgument, null);
					}
					catch (LocalizedException exception3)
					{
						base.WriteError(exception3, ErrorCategory.InvalidArgument, null);
					}
					if (this.DataObject.NamespaceProvisioner == FederationTrust.NamespaceProvisionerType.LiveDomainServices2)
					{
						this.WriteWarning(Strings.UpdateManageDelegation2ProvisioningInDNS);
					}
				}
				else
				{
					base.WriteVerbose(Strings.IgnoringSameNextCertificate);
				}
			}
			if (this.PublishFederationCertificate)
			{
				this.DataObject.OrgPrevCertificate = this.DataObject.OrgCertificate;
				this.DataObject.OrgPrevPrivCertificate = this.DataObject.OrgPrivCertificate;
				this.DataObject.OrgCertificate = this.DataObject.OrgNextCertificate;
				this.DataObject.OrgPrivCertificate = this.DataObject.OrgNextPrivCertificate;
				this.DataObject.OrgNextCertificate = null;
				this.DataObject.OrgNextPrivCertificate = null;
				if (this.DataObject.NamespaceProvisioner == FederationTrust.NamespaceProvisionerType.LiveDomainServices2)
				{
					this.WriteWarning(Strings.PublishManageDelegation2ProvisioningInDNS);
				}
			}
			if (this.partnerFederationMetadata != null)
			{
				try
				{
					LivePartnerFederationMetadata.InitializeDataObjectFromMetadata(this.DataObject, this.partnerFederationMetadata, new WriteWarningDelegate(this.WriteWarning));
				}
				catch (FederationMetadataException exception4)
				{
					base.WriteError(exception4, ErrorCategory.MetadataError, null);
				}
			}
			base.InternalProcessRecord();
		}

		// Token: 0x06005B4F RID: 23375 RVA: 0x0017E404 File Offset: 0x0017C604
		private void ValidateNextCertificate()
		{
			this.Thumbprint = FederationCertificate.UnifyThumbprintFormat(this.Thumbprint);
			this.nextCertificate = FederationCertificate.GetExchangeFederationCertByThumbprint(this.Thumbprint, new WriteVerboseDelegate(base.WriteVerbose));
			ExchangeCertificate exchangeCertificate = new ExchangeCertificate(this.nextCertificate);
			FederationCertificate.ValidateCertificate(exchangeCertificate, this.IsDatacenter);
			this.ValidateUniqueSki(exchangeCertificate, this.DataObject.OrgPrevCertificate);
			this.ValidateUniqueSki(exchangeCertificate, this.DataObject.OrgCertificate);
		}

		// Token: 0x06005B50 RID: 23376 RVA: 0x0017E47C File Offset: 0x0017C67C
		private void ValidateUniqueSki(ExchangeCertificate nextExchangeCertificate, X509Certificate2 otherCertificate)
		{
			if (otherCertificate != null)
			{
				ExchangeCertificate exchangeCertificate = new ExchangeCertificate(otherCertificate);
				if (StringComparer.InvariantCultureIgnoreCase.Equals(nextExchangeCertificate.SubjectKeyIdentifier, exchangeCertificate.SubjectKeyIdentifier))
				{
					throw new FederationCertificateInvalidException(Strings.ErrorCertificateSKINotUnique(nextExchangeCertificate.Thumbprint, exchangeCertificate.Thumbprint, nextExchangeCertificate.SubjectKeyIdentifier));
				}
			}
		}

		// Token: 0x06005B51 RID: 23377 RVA: 0x0017E4C8 File Offset: 0x0017C6C8
		private void UpdateFederationMetadata()
		{
			try
			{
				this.partnerFederationMetadata = LivePartnerFederationMetadata.LoadFrom(this.MetadataUrlToUse, new WriteVerboseDelegate(base.WriteVerbose));
			}
			catch (FederationMetadataException exception)
			{
				base.WriteError(exception, ErrorCategory.MetadataError, null);
			}
		}

		// Token: 0x17001B54 RID: 6996
		// (get) Token: 0x06005B52 RID: 23378 RVA: 0x0017E514 File Offset: 0x0017C714
		private Uri MetadataUrlToUse
		{
			get
			{
				if (this.MetadataUrl == null)
				{
					return this.DataObject.TokenIssuerMetadataEpr;
				}
				return this.MetadataUrl;
			}
		}

		// Token: 0x17001B55 RID: 6997
		// (get) Token: 0x06005B53 RID: 23379 RVA: 0x0017E536 File Offset: 0x0017C736
		private bool MetadataUrlChanged
		{
			get
			{
				return !StringComparer.OrdinalIgnoreCase.Equals(this.MetadataUrlToUse.AbsoluteUri, this.DataObject.TokenIssuerMetadataEpr.AbsoluteUri);
			}
		}

		// Token: 0x17001B56 RID: 6998
		// (get) Token: 0x06005B54 RID: 23380 RVA: 0x0017E560 File Offset: 0x0017C760
		private bool IsDatacenter
		{
			get
			{
				bool result = false;
				try
				{
					result = Datacenter.IsMicrosoftHostedOnly(true);
				}
				catch (CannotDetermineExchangeModeException)
				{
				}
				return result;
			}
		}

		// Token: 0x040033EE RID: 13294
		private Uri applicationUri;

		// Token: 0x040033EF RID: 13295
		private X509Certificate2 nextCertificate;

		// Token: 0x040033F0 RID: 13296
		private PartnerFederationMetadata partnerFederationMetadata;
	}
}
