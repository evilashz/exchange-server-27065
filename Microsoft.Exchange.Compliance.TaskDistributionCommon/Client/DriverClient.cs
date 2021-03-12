using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Contract;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Client
{
	// Token: 0x02000006 RID: 6
	public class DriverClient : DriverClientBase
	{
		// Token: 0x06000005 RID: 5 RVA: 0x0000233E File Offset: 0x0000053E
		public DriverClient(string hostName, string certificateSubject)
		{
			this.endpointAddress = WcfUtility.GetInterServiceEndpointAddress(hostName);
			this.binding = WcfUtility.CreateInterServiceBinding();
			this.thumbprint = WcfUtility.GetThumbprint(certificateSubject);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002369 File Offset: 0x00000569
		private DriverClient.WcfProxyClient ProxyClient
		{
			get
			{
				if (this.proxyClient == null)
				{
					this.CreateProxy();
				}
				return this.proxyClient;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000025F4 File Offset: 0x000007F4
		public override async Task<ComplianceMessage> GetResponseAsync(ComplianceMessage message)
		{
			byte[] messageBlob = ComplianceSerializer.Serialize<ComplianceMessage>(ComplianceMessage.Description, message);
			byte[] responseBlob = await this.TakeActionWithRetryOnCommunicationException<byte[]>(async () => await this.ProxyClient.ProcessMessageAsync(messageBlob), true);
			return ComplianceSerializer.DeSerialize<ComplianceMessage>(ComplianceMessage.Description, responseBlob);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002828 File Offset: 0x00000A28
		private async Task<TResult> TakeActionWithRetryOnCommunicationException<TResult>(Func<Task<TResult>> action, bool firstTry)
		{
			TResult response = default(TResult);
			bool recreatedProxy = false;
			try
			{
				response = await action();
			}
			catch (Exception ex)
			{
				if (!(ex is CommunicationException) && !(ex is TimeoutException))
				{
					throw;
				}
				this.CreateProxy();
				recreatedProxy = true;
			}
			TResult result;
			if (recreatedProxy && firstTry)
			{
				result = await this.TakeActionWithRetryOnCommunicationException<TResult>(action, false);
			}
			else
			{
				result = response;
			}
			return result;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002880 File Offset: 0x00000A80
		private void CreateProxy()
		{
			if (this.proxyClient != null)
			{
				try
				{
					this.proxyClient.Close();
				}
				catch (Exception ex)
				{
					this.proxyClient.Abort();
					if (!(ex is CommunicationException) && !(ex is TimeoutException))
					{
						throw;
					}
				}
			}
			DriverClient.WcfProxyClient wcfProxyClient = new DriverClient.WcfProxyClient(this.binding, this.endpointAddress);
			wcfProxyClient.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindByThumbprint, this.thumbprint);
			wcfProxyClient.Open();
			this.proxyClient = wcfProxyClient;
		}

		// Token: 0x0400000A RID: 10
		private readonly string thumbprint;

		// Token: 0x0400000B RID: 11
		private readonly Binding binding;

		// Token: 0x0400000C RID: 12
		private readonly EndpointAddress endpointAddress;

		// Token: 0x0400000D RID: 13
		private volatile DriverClient.WcfProxyClient proxyClient;

		// Token: 0x02000008 RID: 8
		private class WcfProxyClient : ClientBase<IMessageProcessor>, IMessageProcessor
		{
			// Token: 0x0600000B RID: 11 RVA: 0x00002914 File Offset: 0x00000B14
			public WcfProxyClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
			{
			}

			// Token: 0x0600000C RID: 12 RVA: 0x0000291E File Offset: 0x00000B1E
			public Task<byte[]> ProcessMessageAsync(byte[] message)
			{
				return base.Channel.ProcessMessageAsync(message);
			}
		}
	}
}
