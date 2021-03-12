using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000366 RID: 870
	public class MsoOnboardingService : OnboardingService
	{
		// Token: 0x06001E53 RID: 7763 RVA: 0x000839FD File Offset: 0x00081BFD
		public static string GetSiteName()
		{
			return LocalSiteCache.LocalSite.Name;
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x00083A0C File Offset: 0x00081C0C
		public static ServiceEndpoint GetMsoEndpoint()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 71, "GetMsoEndpoint", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\ForwardSync\\MSOOnboardingService.cs");
			ServiceEndpointContainer endpointContainer = topologyConfigurationSession.GetEndpointContainer();
			return endpointContainer.GetEndpoint("MSOSyncEndpoint");
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x00083A4C File Offset: 0x00081C4C
		public static string GetErrorStringForResultcode(ResultCode? result)
		{
			if (result != null && result.Value == ResultCode.Success)
			{
				return string.Empty;
			}
			string result2;
			if (result == null)
			{
				result2 = Strings.SetServiceInstanceMapReturnedNull;
			}
			else
			{
				string description;
				switch (result.Value)
				{
				case ResultCode.PartitionUnavailable:
					description = Strings.SetServiceInstanceMapResultCodePartitionUnavailable;
					break;
				case ResultCode.ObjectNotFound:
					description = Strings.SetServiceInstanceMapResultCodeObjectNotFound;
					break;
				case ResultCode.UnspecifiedError:
					description = Strings.SetServiceInstanceMapResultCodeUnspecifiedError;
					break;
				default:
					description = Strings.SetServiceInstanceMapResultCodeUnknownError;
					break;
				}
				result2 = Strings.SetServiceInstanceMapResultFormat(result.Value.ToString(), description);
			}
			return result2;
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x00083AF8 File Offset: 0x00081CF8
		protected override IFederatedServiceOnboarding CreateService()
		{
			ServiceEndpoint serviceEndpoint = null;
			try
			{
				serviceEndpoint = MsoOnboardingService.GetMsoEndpoint();
			}
			catch (Exception innerException)
			{
				throw new CouldNotCreateMsoOnboardingServiceException(Strings.CouldNotGetMsoEndpoint, innerException);
			}
			IFederatedServiceOnboarding result;
			try
			{
				EndpointAddress remoteAddress = new EndpointAddress(serviceEndpoint.Uri, new AddressHeader[0]);
				FederatedServiceOnboardingClient federatedServiceOnboardingClient = new FederatedServiceOnboardingClient(new WSHttpBinding(SecurityMode.Transport)
				{
					Security = 
					{
						Transport = 
						{
							ClientCredentialType = HttpClientCredentialType.Certificate
						}
					},
					MaxBufferPoolSize = 5242880L,
					MaxReceivedMessageSize = 5242880L
				}, remoteAddress);
				X509Certificate2 certificate = TlsCertificateInfo.FindFirstCertWithSubjectDistinguishedName(serviceEndpoint.CertificateSubject);
				federatedServiceOnboardingClient.ClientCredentials.ClientCertificate.Certificate = certificate;
				result = federatedServiceOnboardingClient;
			}
			catch (Exception ex)
			{
				throw new CouldNotCreateMsoOnboardingServiceException(ex.Message, ex);
			}
			return result;
		}

		// Token: 0x0400191C RID: 6428
		public const string MSOSyncEndpointName = "MSOSyncEndpoint";

		// Token: 0x0400191D RID: 6429
		private const int DefaultMaxBufferPoolSize = 5242880;

		// Token: 0x0400191E RID: 6430
		private const int DefaultMaxReceivedMessageSize = 5242880;
	}
}
