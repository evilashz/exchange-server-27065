using System;
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
	// Token: 0x0200036A RID: 874
	public class MsoSyncService : SyncService
	{
		// Token: 0x06001E8F RID: 7823 RVA: 0x00084AD8 File Offset: 0x00082CD8
		public static ServiceEndpoint GetMsoEndpoint()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 66, "GetMsoEndpoint", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\ForwardSync\\MsoSyncService.cs");
			ServiceEndpointContainer endpointContainer = topologyConfigurationSession.GetEndpointContainer();
			return endpointContainer.GetEndpoint("MSOSyncEndpoint");
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x00084B18 File Offset: 0x00082D18
		protected override DirectorySyncClient CreateService()
		{
			ServiceEndpoint serviceEndpoint = null;
			try
			{
				serviceEndpoint = MsoSyncService.GetMsoEndpoint();
			}
			catch (Exception innerException)
			{
				throw new CouldNotCreateMsoSyncServiceException(Strings.CouldNotGetMsoEndpoint, innerException);
			}
			DirectorySyncClient result;
			try
			{
				EndpointAddress remoteAddress = new EndpointAddress(serviceEndpoint.Uri, new AddressHeader[0]);
				result = new DirectorySyncClient(new WSHttpBinding(SecurityMode.Transport)
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
				}, remoteAddress)
				{
					ClientCredentials = 
					{
						ClientCertificate = 
						{
							Certificate = TlsCertificateInfo.FindFirstCertWithSubjectDistinguishedName(serviceEndpoint.CertificateSubject)
						}
					}
				};
			}
			catch (Exception ex)
			{
				throw new CouldNotCreateMsoSyncServiceException(ex.Message, ex);
			}
			return result;
		}

		// Token: 0x04001930 RID: 6448
		public const string MSOSyncEndpointName = "MSOSyncEndpoint";

		// Token: 0x04001931 RID: 6449
		private const string SyncWSBindingName = "SyncServiceBinding";

		// Token: 0x04001932 RID: 6450
		private const int DefaultMaxBufferPoolSize = 5242880;

		// Token: 0x04001933 RID: 6451
		private const int DefaultMaxReceivedMessageSize = 5242880;
	}
}
