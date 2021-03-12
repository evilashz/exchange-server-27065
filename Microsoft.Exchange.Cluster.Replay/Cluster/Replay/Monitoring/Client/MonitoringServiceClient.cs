using System;
using System.Net.Security;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.DagManagement;

namespace Microsoft.Exchange.Cluster.Replay.Monitoring.Client
{
	// Token: 0x020001D8 RID: 472
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MonitoringServiceClient : DisposeTrackableBase
	{
		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x0004C294 File Offset: 0x0004A494
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.MonitoringWcfClientTracer;
			}
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0004C29B File Offset: 0x0004A49B
		public static MonitoringServiceClient Open(string serverName)
		{
			return MonitoringServiceClient.Open(serverName, MonitoringServiceClient.OpenTimeout, MonitoringServiceClient.CloseTimeout, MonitoringServiceClient.SendTimeout, MonitoringServiceClient.ReceiveTimeout);
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x0004C2B8 File Offset: 0x0004A4B8
		public static MonitoringServiceClient Open(string serverName, TimeSpan openTimeout, TimeSpan closeTimeout, TimeSpan sendTimeout, TimeSpan receiveTimeout)
		{
			MonitoringServiceClient monitoringServiceClient = new MonitoringServiceClient();
			EndpointAddress endpointAddress = new EndpointAddress(string.Format("net.tcp://{0}:{1}/Microsoft.Exchange.HA.Monitoring", serverName, RegistryParameters.MonitoringWebServicePort));
			monitoringServiceClient.ServerName = serverName;
			MonitoringServiceClient.Tracer.TraceDebug<EndpointAddress>(0L, "Opening MonitoringServiceClient connection to endpoint: {0}", endpointAddress);
			monitoringServiceClient.m_wcfClient = new InternalMonitoringServiceClient(new NetTcpBinding
			{
				Name = "NetTcpBinding_MonitoringService",
				OpenTimeout = openTimeout,
				CloseTimeout = closeTimeout,
				SendTimeout = sendTimeout,
				ReceiveTimeout = receiveTimeout,
				MaxBufferPoolSize = 16777216L,
				MaxBufferSize = 16777216,
				MaxReceivedMessageSize = 16777216L,
				MaxConnections = 10,
				Security = 
				{
					Mode = SecurityMode.Transport,
					Message = 
					{
						ClientCredentialType = MessageCredentialType.Windows
					},
					Transport = 
					{
						ClientCredentialType = TcpClientCredentialType.Windows,
						ProtectionLevel = ProtectionLevel.EncryptAndSign
					}
				}
			}, endpointAddress);
			return monitoringServiceClient;
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x0004C3A5 File Offset: 0x0004A5A5
		// (set) Token: 0x060012D5 RID: 4821 RVA: 0x0004C3AD File Offset: 0x0004A5AD
		public string ServerName { get; private set; }

		// Token: 0x060012D6 RID: 4822 RVA: 0x0004C3B8 File Offset: 0x0004A5B8
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				InternalMonitoringServiceClient internalMonitoringServiceClient = null;
				lock (this)
				{
					if (this.m_wcfClient != null)
					{
						internalMonitoringServiceClient = this.m_wcfClient;
						this.m_wcfClient = null;
					}
				}
				if (internalMonitoringServiceClient != null)
				{
					internalMonitoringServiceClient.Abort();
				}
			}
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x0004C414 File Offset: 0x0004A614
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MonitoringServiceClient>(this);
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x0004C41C File Offset: 0x0004A61C
		public static Exception HandleException(Action operation)
		{
			Exception result = null;
			try
			{
				operation();
			}
			catch (FaultException<ExceptionDetail> faultException)
			{
				result = faultException;
			}
			catch (FaultException ex)
			{
				result = ex;
			}
			catch (CommunicationException ex2)
			{
				result = ex2;
			}
			catch (TimeoutException ex3)
			{
				result = ex3;
			}
			catch (AggregateException ex4)
			{
				result = ex4.Flatten().InnerExceptions[0];
			}
			return result;
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x0004C4A4 File Offset: 0x0004A6A4
		public ServiceVersion GetVersion()
		{
			return this.m_wcfClient.GetVersion();
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x0004C5F8 File Offset: 0x0004A7F8
		public async Task<ServiceVersion> GetVersionAsync()
		{
			MonitoringServiceClient.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Calling GetVersionAsync() on server '{0}' ...", this.ServerName);
			ServiceVersion version = await this.m_wcfClient.GetVersionAsync();
			MonitoringServiceClient.Tracer.TraceDebug<long, string>((long)this.GetHashCode(), "GetVersionAsync() from server '{1}' returned version of: {0}", version.Version, this.ServerName);
			return version;
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x0004C770 File Offset: 0x0004A970
		public async Task<bool> PublishDagHealthInfoAsync(HealthInfoPersisted healthInfo)
		{
			MonitoringServiceClient.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Calling PublishDagHealthInfoAsync() on server '{0}' ...", this.ServerName);
			await this.m_wcfClient.PublishDagHealthInfoAsync(healthInfo);
			MonitoringServiceClient.Tracer.TraceDebug<string>((long)this.GetHashCode(), "PublishDagHealthInfoAsync() from server '{0}' returned.", this.ServerName);
			return true;
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x0004C7D4 File Offset: 0x0004A9D4
		public Task<Tuple<string, bool>> WPublishDagHealthInfoAsync(HealthInfoPersisted healthInfo)
		{
			return this.RunOperationWithServerNameAsync<bool>((MonitoringServiceClient client) => client.PublishDagHealthInfoAsync(healthInfo));
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x0004C93C File Offset: 0x0004AB3C
		public async Task<DateTime> GetDagHealthInfoUpdateTimeUtcAsync()
		{
			MonitoringServiceClient.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Calling GetDagHealthInfoUpdateTimeUtcAsync() on server '{0}' ...", this.ServerName);
			DateTime lastUpdateTime = await this.m_wcfClient.GetDagHealthInfoUpdateTimeUtcAsync();
			MonitoringServiceClient.Tracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "GetDagHealthInfoUpdateTimeUtcAsync() from server '{1}' returned '{0}'.", lastUpdateTime, this.ServerName);
			return lastUpdateTime;
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public Task<Tuple<string, DateTime>> WGetDagHealthInfoUpdateTimeUtcAsync()
		{
			return this.RunOperationWithServerNameAsync<DateTime>((MonitoringServiceClient client) => client.GetDagHealthInfoUpdateTimeUtcAsync());
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x0004CAF4 File Offset: 0x0004ACF4
		public async Task<HealthInfoPersisted> GetDagHealthInfoAsync()
		{
			MonitoringServiceClient.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Calling GetDagHealthInfoAsync() on server '{0}' ...", this.ServerName);
			HealthInfoPersisted hip = await this.m_wcfClient.GetDagHealthInfoAsync();
			MonitoringServiceClient.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "GetDagHealthInfoAsync() from server '{1}' returned a table of update time '{0}'.", hip.LastUpdateTimeUtcStr, this.ServerName);
			return hip;
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x0004CB42 File Offset: 0x0004AD42
		public Task<Tuple<string, HealthInfoPersisted>> WGetDagHealthInfoAsync()
		{
			return this.RunOperationWithServerNameAsync<HealthInfoPersisted>((MonitoringServiceClient client) => client.GetDagHealthInfoAsync());
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0004CC60 File Offset: 0x0004AE60
		private async Task<Tuple<string, TReturnType>> RunOperationWithServerNameAsync<TReturnType>(Func<MonitoringServiceClient, Task<TReturnType>> remoteOperation)
		{
			TReturnType returnObj = await remoteOperation(this);
			return new Tuple<string, TReturnType>(this.ServerName, returnObj);
		}

		// Token: 0x04000730 RID: 1840
		private const string WcfEndpointFormat = "net.tcp://{0}:{1}/Microsoft.Exchange.HA.Monitoring";

		// Token: 0x04000731 RID: 1841
		public static readonly TimeSpan OpenTimeout = TimeSpan.FromSeconds((double)RegistryParameters.MonitoringWebServiceClientOpenTimeoutInSecs);

		// Token: 0x04000732 RID: 1842
		public static readonly TimeSpan CloseTimeout = TimeSpan.FromSeconds((double)RegistryParameters.MonitoringWebServiceClientCloseTimeoutInSecs);

		// Token: 0x04000733 RID: 1843
		public static readonly TimeSpan SendTimeout = TimeSpan.FromSeconds((double)RegistryParameters.MonitoringWebServiceClientSendTimeoutInSecs);

		// Token: 0x04000734 RID: 1844
		public static readonly TimeSpan ReceiveTimeout = TimeSpan.FromSeconds((double)RegistryParameters.MonitoringWebServiceClientReceiveTimeoutInSecs);

		// Token: 0x04000735 RID: 1845
		private InternalMonitoringServiceClient m_wcfClient;
	}
}
