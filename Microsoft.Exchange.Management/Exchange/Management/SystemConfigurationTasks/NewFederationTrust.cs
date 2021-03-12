using System;
using System.Management.Automation;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.FederationProvisioning;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009EE RID: 2542
	[Cmdlet("New", "FederationTrust", SupportsShouldProcess = true, DefaultParameterSetName = "FederationTrustParameter")]
	public sealed class NewFederationTrust : NewSystemConfigurationObjectTask<FederationTrust>
	{
		// Token: 0x17001B18 RID: 6936
		// (get) Token: 0x06005AB5 RID: 23221 RVA: 0x0017C12F File Offset: 0x0017A32F
		// (set) Token: 0x06005AB6 RID: 23222 RVA: 0x0017C13C File Offset: 0x0017A33C
		[Parameter(Mandatory = false, ParameterSetName = "SkipNamespaceProviderProvisioning")]
		public string ApplicationIdentifier
		{
			get
			{
				return this.DataObject.ApplicationIdentifier;
			}
			set
			{
				this.DataObject.ApplicationIdentifier = value.Trim();
			}
		}

		// Token: 0x17001B19 RID: 6937
		// (get) Token: 0x06005AB7 RID: 23223 RVA: 0x0017C14F File Offset: 0x0017A34F
		// (set) Token: 0x06005AB8 RID: 23224 RVA: 0x0017C15C File Offset: 0x0017A35C
		[Parameter(Mandatory = false, ParameterSetName = "SkipNamespaceProviderProvisioning")]
		public string AdministratorProvisioningId
		{
			get
			{
				return this.DataObject.AdministratorProvisioningId;
			}
			set
			{
				this.DataObject.AdministratorProvisioningId = value.Trim();
			}
		}

		// Token: 0x17001B1A RID: 6938
		// (get) Token: 0x06005AB9 RID: 23225 RVA: 0x0017C16F File Offset: 0x0017A36F
		// (set) Token: 0x06005ABA RID: 23226 RVA: 0x0017C177 File Offset: 0x0017A377
		[Parameter(Mandatory = true, ParameterSetName = "SkipNamespaceProviderProvisioning")]
		public SwitchParameter SkipNamespaceProviderProvisioning { get; set; }

		// Token: 0x17001B1B RID: 6939
		// (get) Token: 0x06005ABB RID: 23227 RVA: 0x0017C180 File Offset: 0x0017A380
		// (set) Token: 0x06005ABC RID: 23228 RVA: 0x0017C188 File Offset: 0x0017A388
		[Parameter(Mandatory = true, ParameterSetName = "SkipNamespaceProviderProvisioning")]
		public string ApplicationUri { get; set; }

		// Token: 0x17001B1C RID: 6940
		// (get) Token: 0x06005ABD RID: 23229 RVA: 0x0017C191 File Offset: 0x0017A391
		// (set) Token: 0x06005ABE RID: 23230 RVA: 0x0017C19E File Offset: 0x0017A39E
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "FederationTrustParameter")]
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "SkipNamespaceProviderProvisioning")]
		public string Thumbprint
		{
			get
			{
				return this.DataObject.OrgPrivCertificate;
			}
			set
			{
				this.DataObject.OrgPrivCertificate = value;
			}
		}

		// Token: 0x17001B1D RID: 6941
		// (get) Token: 0x06005ABF RID: 23231 RVA: 0x0017C1AC File Offset: 0x0017A3AC
		// (set) Token: 0x06005AC0 RID: 23232 RVA: 0x0017C1B9 File Offset: 0x0017A3B9
		[Parameter(Mandatory = false, ParameterSetName = "FederationTrustParameter")]
		[Parameter(Mandatory = false, ParameterSetName = "SkipNamespaceProviderProvisioning")]
		public Uri MetadataUrl
		{
			get
			{
				return this.DataObject.TokenIssuerMetadataEpr;
			}
			set
			{
				this.DataObject.TokenIssuerMetadataEpr = value;
			}
		}

		// Token: 0x17001B1E RID: 6942
		// (get) Token: 0x06005AC1 RID: 23233 RVA: 0x0017C1C7 File Offset: 0x0017A3C7
		// (set) Token: 0x06005AC2 RID: 23234 RVA: 0x0017C1CF File Offset: 0x0017A3CF
		[Parameter(Mandatory = false, ParameterSetName = "FederationTrustParameter")]
		public SwitchParameter UseLegacyProvisioningService { get; set; }

		// Token: 0x17001B1F RID: 6943
		// (get) Token: 0x06005AC3 RID: 23235 RVA: 0x0017C1D8 File Offset: 0x0017A3D8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (null != this.MetadataUrl && !string.IsNullOrEmpty(this.MetadataUrl.AbsoluteUri))
				{
					return Strings.ConfirmationMessageNewFederationTrustWithMetadata(base.Name, FederationTrust.PartnerSTSType.LiveId.ToString(), this.Thumbprint, this.MetadataUrl.AbsoluteUri);
				}
				return Strings.ConfirmationMessageNewFederationTrust(base.Name, FederationTrust.PartnerSTSType.LiveId.ToString(), this.Thumbprint);
			}
		}

		// Token: 0x06005AC4 RID: 23236 RVA: 0x0017C24C File Offset: 0x0017A44C
		internal static bool IsExchangeDataCenter()
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

		// Token: 0x06005AC5 RID: 23237 RVA: 0x0017C278 File Offset: 0x0017A478
		protected override IConfigurable PrepareDataObject()
		{
			FederationTrust federationTrust = (FederationTrust)base.PrepareDataObject();
			federationTrust.SetId(this.ConfigurationSession, base.Name);
			return federationTrust;
		}

		// Token: 0x06005AC6 RID: 23238 RVA: 0x0017C2B8 File Offset: 0x0017A4B8
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			IConfigurable[] array = base.DataSession.Find<FederationTrust>(null, null, true, null);
			if (array != null && array.Length > 0)
			{
				if (this.SkipNamespaceProviderProvisioning)
				{
					if (!Array.Exists<IConfigurable>(array, (IConfigurable federationTrust) => ((FederationTrust)federationTrust).NamespaceProvisioner != FederationTrust.NamespaceProvisionerType.ExternalProcess))
					{
						goto IL_76;
					}
				}
				base.WriteError(new TrustAlreadyDefinedException(), ErrorCategory.InvalidArgument, this.DataObject.Name);
			}
			IL_76:
			this.DataObject.OrgCertificate = this.GetFederatedExchangeCertificates();
			if (this.ApplicationUri != null)
			{
				Uri uri;
				if (!Uri.TryCreate(this.ApplicationUri, UriKind.RelativeOrAbsolute, out uri))
				{
					base.WriteError(new InvalidApplicationUriException(Strings.ErrorInvalidApplicationUri(this.ApplicationUri)), ErrorCategory.InvalidArgument, null);
				}
				if (null != uri)
				{
					this.DataObject.ApplicationUri = uri;
				}
			}
			if (!string.IsNullOrEmpty(this.ApplicationIdentifier))
			{
				base.WriteVerbose(Strings.NewFederationTrustSuccessAppId(FederationTrust.PartnerSTSType.LiveId.ToString(), this.ApplicationIdentifier));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x17001B20 RID: 6944
		// (get) Token: 0x06005AC7 RID: 23239 RVA: 0x0017C3C4 File Offset: 0x0017A5C4
		private FederationTrust.NamespaceProvisionerType NamespaceProvisionerType
		{
			get
			{
				if (this.SkipNamespaceProviderProvisioning)
				{
					return FederationTrust.NamespaceProvisionerType.ExternalProcess;
				}
				if (this.UseLegacyProvisioningService)
				{
					return FederationTrust.NamespaceProvisionerType.LiveDomainServices;
				}
				return FederationTrust.NamespaceProvisionerType.LiveDomainServices2;
			}
		}

		// Token: 0x06005AC8 RID: 23240 RVA: 0x0017C3E8 File Offset: 0x0017A5E8
		protected override void InternalProcessRecord()
		{
			this.DataObject.NamespaceProvisioner = this.NamespaceProvisionerType;
			this.ProvisionSTS();
			try
			{
				FederationCertificate.PushCertificate(new Task.TaskProgressLoggingDelegate(base.WriteProgress), new Task.TaskWarningLoggingDelegate(this.WriteWarning), this.Thumbprint);
			}
			catch (InvalidOperationException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, null);
			}
			catch (LocalizedException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidArgument, null);
			}
			base.InternalProcessRecord();
		}

		// Token: 0x06005AC9 RID: 23241 RVA: 0x0017C46C File Offset: 0x0017A66C
		private X509Certificate2 GetFederatedExchangeCertificates()
		{
			if (!string.IsNullOrEmpty(this.Thumbprint))
			{
				this.Thumbprint = FederationCertificate.UnifyThumbprintFormat(this.Thumbprint);
				try
				{
					X509Certificate2 exchangeFederationCertByThumbprint = FederationCertificate.GetExchangeFederationCertByThumbprint(this.Thumbprint, new WriteVerboseDelegate(base.WriteVerbose));
					if (exchangeFederationCertByThumbprint == null)
					{
						throw new FederationCertificateInvalidException(Strings.ErrorCertificateNotFound(this.Thumbprint));
					}
					FederationCertificate.ValidateCertificate(new ExchangeCertificate(exchangeFederationCertByThumbprint), NewFederationTrust.IsExchangeDataCenter());
					return exchangeFederationCertByThumbprint;
				}
				catch (LocalizedException exception)
				{
					base.WriteError(exception, ErrorCategory.InvalidArgument, null);
					goto IL_7C;
				}
			}
			base.WriteError(new FederationCertificateInvalidException(Strings.ErrorFederationCertificateNotSpecified), ErrorCategory.InvalidOperation, null);
			IL_7C:
			return null;
		}

		// Token: 0x06005ACA RID: 23242 RVA: 0x0017C508 File Offset: 0x0017A708
		private void ProvisionSTS()
		{
			int num = 0;
			num += 30;
			base.WriteProgress(Strings.ProgressActivityNewFederationTrust, Strings.ProgressActivityGetFederationMetadata, num);
			Uri uri = this.MetadataUrl;
			if (uri == null)
			{
				uri = LiveConfiguration.GetLiveIdFederationMetadataEpr(this.NamespaceProvisionerType);
			}
			try
			{
				PartnerFederationMetadata partnerFederationMetadata = LivePartnerFederationMetadata.LoadFrom(uri, new WriteVerboseDelegate(base.WriteVerbose));
				LivePartnerFederationMetadata.InitializeDataObjectFromMetadata(this.DataObject, partnerFederationMetadata, new WriteWarningDelegate(this.WriteWarning));
			}
			catch (FederationMetadataException exception)
			{
				base.WriteError(exception, ErrorCategory.MetadataError, null);
			}
			this.DataObject.TokenIssuerType = FederationTrust.PartnerSTSType.LiveId;
			this.DataObject.MetadataEpr = null;
			this.DataObject.MetadataPutEpr = null;
			this.DataObject.MetadataPollInterval = LiveConfiguration.DefaultFederatedMetadataTimeout;
			num += 30;
			base.WriteProgress(Strings.ProgressActivityNewFederationTrust, Strings.NewFederationTrustProvisioningService(FederationTrust.PartnerSTSType.LiveId.ToString()), num);
			base.WriteVerbose(Strings.NewFederationTrustProvisioningService(FederationTrust.PartnerSTSType.LiveId.ToString()));
			num += 30;
			base.WriteProgress(Strings.ProgressActivityNewFederationTrust, Strings.ProgressActivityCreateAppId, num);
			FederationProvision federationProvision = FederationProvision.Create(this.DataObject, this);
			try
			{
				federationProvision.OnNewFederationTrust(this.DataObject);
			}
			catch (LocalizedException ex)
			{
				base.WriteError(new ProvisioningFederatedExchangeException(ex.Message, ex), ErrorCategory.NotSpecified, null);
			}
			base.WriteProgress(Strings.ProgressActivityNewFederationTrust, Strings.ProgressStatusFinished, 100);
			switch (this.NamespaceProvisionerType)
			{
			case FederationTrust.NamespaceProvisionerType.LiveDomainServices:
				this.WriteWarning(Strings.ManageDelegationProvisioningInDNS(this.DataObject.ApplicationIdentifier));
				return;
			case FederationTrust.NamespaceProvisionerType.LiveDomainServices2:
				this.WriteWarning(Strings.ManageDelegation2ProvisioningInDNS);
				return;
			default:
				return;
			}
		}
	}
}
