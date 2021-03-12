using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Security;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Directory.TopologyDiscovery
{
	// Token: 0x020006C4 RID: 1732
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[DebuggerStepThrough]
	internal class TopologyServiceClient : ClientBase<ITopologyClient>, ITopologyClient, ITopologyService
	{
		// Token: 0x06004FDD RID: 20445 RVA: 0x00126BAA File Offset: 0x00124DAA
		protected TopologyServiceClient()
		{
		}

		// Token: 0x06004FDE RID: 20446 RVA: 0x00126BB2 File Offset: 0x00124DB2
		protected TopologyServiceClient(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		// Token: 0x06004FDF RID: 20447 RVA: 0x00126BBB File Offset: 0x00124DBB
		protected TopologyServiceClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06004FE0 RID: 20448 RVA: 0x00126BC5 File Offset: 0x00124DC5
		protected TopologyServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06004FE1 RID: 20449 RVA: 0x00126BCF File Offset: 0x00124DCF
		protected TopologyServiceClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x06004FE2 RID: 20450 RVA: 0x00126BD9 File Offset: 0x00124DD9
		public byte[][] GetExchangeTopology(DateTime currentTopologyTimeStamp, ExchangeTopologyScope topologyScope, bool forceRefresh)
		{
			return base.Channel.GetExchangeTopology(currentTopologyTimeStamp, topologyScope, forceRefresh);
		}

		// Token: 0x06004FE3 RID: 20451 RVA: 0x00126BE9 File Offset: 0x00124DE9
		public ServiceVersion GetServiceVersion()
		{
			return base.Channel.GetServiceVersion();
		}

		// Token: 0x06004FE4 RID: 20452 RVA: 0x00126BF6 File Offset: 0x00124DF6
		public List<TopologyVersion> GetAllTopologyVersions()
		{
			return base.Channel.GetAllTopologyVersions();
		}

		// Token: 0x06004FE5 RID: 20453 RVA: 0x00126C03 File Offset: 0x00124E03
		public List<TopologyVersion> GetTopologyVersions(List<string> partitionFqdns)
		{
			return base.Channel.GetTopologyVersions(partitionFqdns);
		}

		// Token: 0x06004FE6 RID: 20454 RVA: 0x00126C11 File Offset: 0x00124E11
		public List<ServerInfo> GetServersForRole(string partitionFqdn, List<string> currentlyUsedServers, ADServerRole role, int serversRequested, bool forestWideAffinityRequested = false)
		{
			return base.Channel.GetServersForRole(partitionFqdn, currentlyUsedServers, role, serversRequested, forestWideAffinityRequested);
		}

		// Token: 0x06004FE7 RID: 20455 RVA: 0x00126C25 File Offset: 0x00124E25
		public IAsyncResult BeginGetServersForRole(string partitionFqdn, List<string> currentlyUsedServers, ADServerRole role, int serversRequested, bool forestWideAffinityRequested, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginGetServersForRole(partitionFqdn, currentlyUsedServers, role, serversRequested, forestWideAffinityRequested, callback, asyncState);
		}

		// Token: 0x06004FE8 RID: 20456 RVA: 0x00126C3D File Offset: 0x00124E3D
		public List<ServerInfo> EndGetServersForRole(IAsyncResult result)
		{
			return base.Channel.EndGetServersForRole(result);
		}

		// Token: 0x06004FE9 RID: 20457 RVA: 0x00126C4B File Offset: 0x00124E4B
		public IAsyncResult BeginGetServerFromDomainDN(string domainDN, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginGetServerFromDomainDN(domainDN, callback, asyncState);
		}

		// Token: 0x06004FEA RID: 20458 RVA: 0x00126C5B File Offset: 0x00124E5B
		public ServerInfo EndGetServerFromDomainDN(IAsyncResult result)
		{
			return base.Channel.EndGetServerFromDomainDN(result);
		}

		// Token: 0x06004FEB RID: 20459 RVA: 0x00126C69 File Offset: 0x00124E69
		public ServerInfo GetServerFromDomainDN(string domainDN)
		{
			return base.Channel.GetServerFromDomainDN(domainDN);
		}

		// Token: 0x06004FEC RID: 20460 RVA: 0x00126C77 File Offset: 0x00124E77
		public void SetConfigDC(string partitionFqdn, string serverName)
		{
			base.Channel.SetConfigDC(partitionFqdn, serverName);
		}

		// Token: 0x06004FED RID: 20461 RVA: 0x00126C86 File Offset: 0x00124E86
		public IAsyncResult BeginSetConfigDC(string partitionFqdn, string serverName, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginSetConfigDC(partitionFqdn, serverName, callback, asyncState);
		}

		// Token: 0x06004FEE RID: 20462 RVA: 0x00126C98 File Offset: 0x00124E98
		public void EndSetConfigDC(IAsyncResult result)
		{
			base.Channel.EndSetConfigDC(result);
		}

		// Token: 0x06004FEF RID: 20463 RVA: 0x00126CA6 File Offset: 0x00124EA6
		public void ReportServerDown(string partitionFqdn, string serverName, ADServerRole role)
		{
			base.Channel.ReportServerDown(partitionFqdn, serverName, role);
		}

		// Token: 0x06004FF0 RID: 20464 RVA: 0x00126CB6 File Offset: 0x00124EB6
		public void Close(TimeSpan timeOut)
		{
			base.ChannelFactory.Close(timeOut);
		}

		// Token: 0x06004FF1 RID: 20465 RVA: 0x00126CC4 File Offset: 0x00124EC4
		internal void AddRef()
		{
			Interlocked.Increment(ref this.refCount);
		}

		// Token: 0x06004FF2 RID: 20466 RVA: 0x00126CD4 File Offset: 0x00124ED4
		internal void Release()
		{
			if (Interlocked.Decrement(ref this.refCount) == 0L)
			{
				bool flag = false;
				try
				{
					if (base.State != CommunicationState.Faulted)
					{
						base.Close();
						flag = true;
					}
				}
				catch (TimeoutException)
				{
				}
				catch (CommunicationException)
				{
				}
				finally
				{
					if (!flag)
					{
						base.Abort();
					}
					((IDisposable)this).Dispose();
				}
			}
		}

		// Token: 0x06004FF3 RID: 20467 RVA: 0x00126D48 File Offset: 0x00124F48
		internal static TopologyServiceClient CreateClient(string machineName)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("machineName", machineName);
			NetTcpBinding binding = TopologyServiceClient.CreateAndConfigureTopologyServiceBinding(machineName);
			EndpointAddress remoteAddress = TopologyServiceClient.CreateAndConfigureTopologyServiceEndpoint(machineName);
			return new TopologyServiceClient(binding, remoteAddress);
		}

		// Token: 0x06004FF4 RID: 20468 RVA: 0x00126D78 File Offset: 0x00124F78
		internal static NetTcpBinding CreateAndConfigureTopologyServiceBinding(string machineName)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("machineName", machineName);
			return new NetTcpBinding
			{
				Name = "Topology Client - " + machineName,
				TransactionFlow = false,
				TransferMode = TransferMode.Buffered,
				TransactionProtocol = TransactionProtocol.OleTransactions,
				HostNameComparisonMode = HostNameComparisonMode.StrongWildcard,
				ListenBacklog = 50,
				MaxBufferPoolSize = 524288L,
				MaxReceivedMessageSize = 134217728L,
				MaxConnections = 100,
				Security = 
				{
					Mode = SecurityMode.Transport,
					Transport = 
					{
						ClientCredentialType = TcpClientCredentialType.Windows,
						ProtectionLevel = ProtectionLevel.EncryptAndSign
					}
				},
				ReliableSession = 
				{
					Ordered = true,
					InactivityTimeout = ServiceProxyPool<ITopologyClient>.DefaultInactivityTimeout,
					Enabled = false
				},
				SendTimeout = TopologyServiceClient.defaultSendTimeout,
				ReaderQuotas = 
				{
					MaxStringContentLength = 262144
				}
			};
		}

		// Token: 0x06004FF5 RID: 20469 RVA: 0x00126E64 File Offset: 0x00125064
		internal static EndpointAddress CreateAndConfigureTopologyServiceEndpoint(string machineName)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("machineName", machineName);
			string text = string.Format("net.tcp://{0}:890/Microsoft.Exchange.Directory.TopologyService", machineName);
			EndpointAddress result;
			try
			{
				EndpointAddress endpointAddress = new EndpointAddress(text);
				result = endpointAddress;
			}
			catch (UriFormatException ex)
			{
				ExTraceGlobals.TopologyProviderTracer.TraceError<string, string>(0L, "TopologyServiceClient.CreateAndConfigureTopologyServiceEndpoint() - Invalid Server Name {0}.  Exception: {1}", machineName, ex.Message);
				throw new InvalidEndpointAddressException(ex.GetType().Name, text);
			}
			return result;
		}

		// Token: 0x04003672 RID: 13938
		private const string WcfEndpointFormat = "net.tcp://{0}:890/Microsoft.Exchange.Directory.TopologyService";

		// Token: 0x04003673 RID: 13939
		private static readonly TimeSpan defaultSendTimeout = TimeSpan.FromMinutes(2.0);

		// Token: 0x04003674 RID: 13940
		private long refCount;
	}
}
