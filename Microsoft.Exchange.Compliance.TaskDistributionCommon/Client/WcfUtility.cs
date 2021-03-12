using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Client
{
	// Token: 0x0200000D RID: 13
	internal static class WcfUtility
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00003118 File Offset: 0x00001318
		public static Binding CreateIntraServiceBinding(bool enablePortSharing = true)
		{
			return new NetTcpBinding
			{
				Security = 
				{
					Mode = SecurityMode.Transport,
					Transport = 
					{
						ClientCredentialType = TcpClientCredentialType.Windows
					}
				},
				MaxReceivedMessageSize = 524288L,
				MaxBufferPoolSize = 1048576L,
				PortSharingEnabled = enablePortSharing
			};
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003168 File Offset: 0x00001368
		public static Binding CreateInterServiceBinding()
		{
			return new WSHttpBinding
			{
				Security = 
				{
					Mode = SecurityMode.Transport,
					Transport = 
					{
						ClientCredentialType = HttpClientCredentialType.Certificate
					}
				},
				MaxReceivedMessageSize = 524288L,
				MaxBufferPoolSize = 1048576L
			};
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000031B4 File Offset: 0x000013B4
		public static EndpointAddress GetBackendServerEndpointAddress(string server)
		{
			string uri = string.Format("net.tcp://{0}/complianceservice", server);
			return new EndpointAddress(uri);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000031D4 File Offset: 0x000013D4
		public static EndpointAddress GetInterServiceEndpointAddress(string host)
		{
			string uri = string.Format("https://{0}/complianceservice", host);
			return new EndpointAddress(uri);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000031FC File Offset: 0x000013FC
		public static string GetThumbprint(string certificateSubject)
		{
			X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
			string thumbprint;
			try
			{
				x509Store.Open(OpenFlags.ReadOnly);
				X509Certificate2Collection source = x509Store.Certificates.Find(X509FindType.FindBySubjectDistinguishedName, certificateSubject, true);
				X509Certificate2 x509Certificate = (from X509Certificate2 cert in source
				orderby cert.NotAfter descending
				select cert).FirstOrDefault<X509Certificate2>();
				if (x509Certificate == null)
				{
					throw new InvalidOperationException("Unable to load certificate.");
				}
				thumbprint = x509Certificate.Thumbprint;
			}
			finally
			{
				x509Store.Close();
			}
			return thumbprint;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003284 File Offset: 0x00001484
		public static byte[][] GetMessageBlobs(IEnumerable<ComplianceMessage> messages)
		{
			int num = messages.Count<ComplianceMessage>();
			int num2 = 0;
			byte[][] array = new byte[num][];
			foreach (ComplianceMessage inputObject in messages)
			{
				byte[] array2 = ComplianceSerializer.Serialize<ComplianceMessage>(ComplianceMessage.Description, inputObject);
				array[num2] = array2;
				num2++;
			}
			return array;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000348C File Offset: 0x0000168C
		public static IEnumerable<ComplianceMessage> GetMessagesFromBlobs(IEnumerable<byte[]> blobs)
		{
			if (blobs != null)
			{
				foreach (byte[] blob in blobs)
				{
					yield return ComplianceSerializer.DeSerialize<ComplianceMessage>(ComplianceMessage.Description, blob);
				}
			}
			yield break;
		}
	}
}
