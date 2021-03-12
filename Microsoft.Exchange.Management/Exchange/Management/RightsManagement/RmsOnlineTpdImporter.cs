using System;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.RightsManagementServices.Online;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x02000745 RID: 1861
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RmsOnlineTpdImporter : ITpdImporter
	{
		// Token: 0x1700140E RID: 5134
		// (get) Token: 0x060041E4 RID: 16868 RVA: 0x0010CD79 File Offset: 0x0010AF79
		// (set) Token: 0x060041E5 RID: 16869 RVA: 0x0010CD81 File Offset: 0x0010AF81
		public Uri IntranetLicensingUrl { get; private set; }

		// Token: 0x1700140F RID: 5135
		// (get) Token: 0x060041E6 RID: 16870 RVA: 0x0010CD8A File Offset: 0x0010AF8A
		// (set) Token: 0x060041E7 RID: 16871 RVA: 0x0010CD92 File Offset: 0x0010AF92
		public Uri ExtranetLicensingUrl { get; private set; }

		// Token: 0x17001410 RID: 5136
		// (get) Token: 0x060041E8 RID: 16872 RVA: 0x0010CD9B File Offset: 0x0010AF9B
		// (set) Token: 0x060041E9 RID: 16873 RVA: 0x0010CDA3 File Offset: 0x0010AFA3
		public Uri IntranetCertificationUrl { get; private set; }

		// Token: 0x17001411 RID: 5137
		// (get) Token: 0x060041EA RID: 16874 RVA: 0x0010CDAC File Offset: 0x0010AFAC
		// (set) Token: 0x060041EB RID: 16875 RVA: 0x0010CDB4 File Offset: 0x0010AFB4
		public Uri ExtranetCertificationUrl { get; private set; }

		// Token: 0x060041EC RID: 16876 RVA: 0x0010CDBD File Offset: 0x0010AFBD
		public RmsOnlineTpdImporter(Uri rmsOnlineKeySharingLocation, string authenticationCertificateSubjectName)
		{
			RmsUtil.ThrowIfParameterNull(rmsOnlineKeySharingLocation, "rmsOnlineKeySharingLocation");
			RmsUtil.ThrowIfStringParameterNullOrEmpty(authenticationCertificateSubjectName, "authenticationCertificateSubjectName");
			this.rmsOnlineKeySharingLocation = rmsOnlineKeySharingLocation;
			this.authenticationCertificateSubjectName = authenticationCertificateSubjectName;
		}

		// Token: 0x060041ED RID: 16877 RVA: 0x0010CDEC File Offset: 0x0010AFEC
		public TrustedDocDomain Import(Guid externalDirectoryOrgId)
		{
			RmsUtil.ThrowIfGuidEmpty(externalDirectoryOrgId, "externalDirectoryOrgId");
			X509Certificate2 authenticationCertificate = this.LoadAuthenticationCertificate();
			this.ThrowIfAuthenticationCertificateIsInvalid(authenticationCertificate);
			ITenantManagementService tenantManagementService = this.CreateRmsOnlineWebServiceProxy(authenticationCertificate);
			TrustedDocDomain result;
			try
			{
				TenantInfo[] tenantInfo = tenantManagementService.GetTenantInfo(new string[]
				{
					externalDirectoryOrgId.ToString()
				});
				RmsUtil.ThrowIfTenantInfoisNull(tenantInfo, externalDirectoryOrgId);
				RmsUtil.ThrowIfZeroOrMultipleTenantInfoObjectsReturned(tenantInfo, externalDirectoryOrgId);
				RmsUtil.ThrowIfErrorInfoObjectReturned(tenantInfo[0], externalDirectoryOrgId);
				RmsUtil.ThrowIfTenantInfoDoesNotIncludeActiveTPD(tenantInfo[0], externalDirectoryOrgId);
				RmsUtil.ThrowIfTpdDoesNotIncludeKeyInformation(tenantInfo[0].ActivePublishingDomain, externalDirectoryOrgId);
				RmsUtil.ThrowIfTpdDoesNotIncludeSLC(tenantInfo[0].ActivePublishingDomain, externalDirectoryOrgId);
				RmsUtil.ThrowIfTpdDoesNotIncludeTemplates(tenantInfo[0].ActivePublishingDomain, externalDirectoryOrgId);
				RmsUtil.ThrowIfTenantInfoDoesNotIncludeLicensingUrls(tenantInfo[0], externalDirectoryOrgId);
				RmsUtil.ThrowIfTenantInfoDoesNotIncludeCertificationUrls(tenantInfo[0], externalDirectoryOrgId);
				this.IntranetLicensingUrl = RMUtil.ConvertUriToLicenseLocationDistributionPoint(tenantInfo[0].LicensingIntranetDistributionPointUrl);
				this.ExtranetLicensingUrl = RMUtil.ConvertUriToLicenseLocationDistributionPoint(tenantInfo[0].LicensingExtranetDistributionPointUrl);
				this.IntranetCertificationUrl = RMUtil.ConvertUriToLicenseLocationDistributionPoint(tenantInfo[0].CertificationIntranetDistributionPointUrl);
				this.ExtranetCertificationUrl = RMUtil.ConvertUriToLicenseLocationDistributionPoint(tenantInfo[0].CertificationExtranetDistributionPointUrl);
				result = RmsUtil.ConvertFromRmsOnlineTrustedDocDomain(tenantInfo[0].ActivePublishingDomain);
			}
			catch (FaultException<ArgumentException> innerException)
			{
				throw new ImportTpdException("Caught FaultException<ArgumentException> while obtaining TPD from RMS Online", innerException);
			}
			catch (CommunicationException innerException2)
			{
				throw new ImportTpdException("Unable to communicate with RMS Online key sharing web service", innerException2);
			}
			catch (TimeoutException innerException3)
			{
				throw new ImportTpdException("The TPD import request to the RMS Online key sharing web service has timed out", innerException3);
			}
			return result;
		}

		// Token: 0x060041EE RID: 16878 RVA: 0x0010CF4C File Offset: 0x0010B14C
		public virtual X509Certificate2 LoadAuthenticationCertificate()
		{
			X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
			X509Certificate2 result;
			try
			{
				x509Store.Open(OpenFlags.OpenExistingOnly);
				result = this.ReadAuthenticationCertificateFromStore(x509Store);
			}
			catch (CryptographicException innerException)
			{
				throw new ImportTpdException("Caught CryptographicException when attempting to load RMS Online authentication certificate", innerException);
			}
			catch (SecurityException innerException2)
			{
				throw new ImportTpdException("Caught SecurityException when attempting to load RMS Online authentication certificate", innerException2);
			}
			finally
			{
				x509Store.Close();
			}
			return result;
		}

		// Token: 0x060041EF RID: 16879 RVA: 0x0010CFBC File Offset: 0x0010B1BC
		protected virtual ITenantManagementService CreateRmsOnlineWebServiceProxy(X509Certificate2 authenticationCertificate)
		{
			TenantManagementServiceClient tenantManagementServiceClient = new TenantManagementServiceClient(new WSHttpBinding
			{
				SendTimeout = RmsOnlineConstants.SendTimeout,
				ReceiveTimeout = RmsOnlineConstants.ReceiveTimeout,
				ReaderQuotas = RmsOnlineConstants.ReaderQuotas,
				MaxReceivedMessageSize = RmsOnlineConstants.MaxReceivedMessageSize,
				Name = RmsOnlineConstants.BindingName,
				Security = RmsOnlineConstants.Security
			}, new EndpointAddress(this.rmsOnlineKeySharingLocation, new AddressHeader[0]));
			RmsUtil.ThrowIfClientCredentialsIsNull(tenantManagementServiceClient);
			if (tenantManagementServiceClient.ClientCredentials != null)
			{
				tenantManagementServiceClient.ClientCredentials.ClientCertificate.Certificate = authenticationCertificate;
			}
			return tenantManagementServiceClient;
		}

		// Token: 0x17001412 RID: 5138
		// (get) Token: 0x060041F0 RID: 16880 RVA: 0x0010D049 File Offset: 0x0010B249
		protected virtual string AuthenticationCertificateSubjectDistinguishedName
		{
			get
			{
				return this.authenticationCertificateSubjectName;
			}
		}

		// Token: 0x17001413 RID: 5139
		// (get) Token: 0x060041F1 RID: 16881 RVA: 0x0010D051 File Offset: 0x0010B251
		protected virtual bool AcceptValidAuthenticationCertificateOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060041F2 RID: 16882 RVA: 0x0010D054 File Offset: 0x0010B254
		protected virtual X509Certificate2 ReadAuthenticationCertificateFromStore(X509Store store)
		{
			X509Certificate2Collection certificates = store.Certificates;
			RmsUtil.ThrowIfCertificateCollectionIsNullOrEmpty(certificates, "X509Store returned a null or empty certificate collection; unable to load the RMS Online authentication certificate");
			X509Certificate2Collection x509Certificate2Collection = certificates.Find(X509FindType.FindBySubjectDistinguishedName, this.AuthenticationCertificateSubjectDistinguishedName, this.AcceptValidAuthenticationCertificateOnly);
			RmsUtil.ThrowIfCertificateCollectionIsNullOrEmpty(x509Certificate2Collection, string.Format("X509Store was unable to find the RMS Online authentication certificate with distinguished name '{0}'", this.AuthenticationCertificateSubjectDistinguishedName));
			return x509Certificate2Collection[0];
		}

		// Token: 0x060041F3 RID: 16883 RVA: 0x0010D0A4 File Offset: 0x0010B2A4
		protected virtual void ThrowIfAuthenticationCertificateIsInvalid(X509Certificate2 authenticationCertificate)
		{
			if (authenticationCertificate == null)
			{
				throw new ImportTpdException("X.509 authentication certificate is not valid for the RMS Online service", null);
			}
		}

		// Token: 0x04002985 RID: 10629
		private readonly Uri rmsOnlineKeySharingLocation;

		// Token: 0x04002986 RID: 10630
		private readonly string authenticationCertificateSubjectName;
	}
}
