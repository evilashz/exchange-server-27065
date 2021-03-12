using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Security;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Net.DiagnosticsAggregation;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Configuration;
using Microsoft.Exchange.Transport.DiagnosticsAggregationService;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Probes
{
	// Token: 0x02000261 RID: 609
	public class DiagnosticsAggregationWebServiceProbe : ProbeWorkItem
	{
		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x0003BF74 File Offset: 0x0003A174
		private static ConfigurationLoader<TransportSettingsConfiguration, TransportSettingsConfiguration.Builder> TransportSettings
		{
			get
			{
				if (DiagnosticsAggregationWebServiceProbe.transportSettings == null)
				{
					lock (DiagnosticsAggregationWebServiceProbe.locker)
					{
						if (DiagnosticsAggregationWebServiceProbe.transportSettings == null)
						{
							ConfigurationLoader<TransportSettingsConfiguration, TransportSettingsConfiguration.Builder> configurationLoader = new ConfigurationLoader<TransportSettingsConfiguration, TransportSettingsConfiguration.Builder>(TimeSpan.Zero);
							configurationLoader.Load();
							DiagnosticsAggregationWebServiceProbe.transportSettings = configurationLoader;
						}
					}
				}
				return DiagnosticsAggregationWebServiceProbe.transportSettings;
			}
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0003BFD8 File Offset: 0x0003A1D8
		protected override void DoWork(CancellationToken cancellationToken)
		{
			string uri = string.Format(CultureInfo.InvariantCulture, DiagnosticsAggregationHelper.DiagnosticsAggregationEndpointFormat, new object[]
			{
				"localhost",
				DiagnosticsAggregationWebServiceProbe.TransportSettings.Cache.TransportSettings.DiagnosticsAggregationServicePort
			});
			using (DiagnosticsAggregationServiceClient diagnosticsAggregationServiceClient = new DiagnosticsAggregationServiceClient(this.GetWebServiceBinding(), new EndpointAddress(uri)))
			{
				LocalViewRequest localViewRequest = new LocalViewRequest(RequestType.Queues);
				localViewRequest.QueueLocalViewRequest = new QueueLocalViewRequest();
				try
				{
					LocalViewResponse localView = diagnosticsAggregationServiceClient.GetLocalView(localViewRequest);
					if (localView.QueueLocalViewResponse == null)
					{
						throw new ApplicationException("GetLocalView returned with a null QueueLocalViewResponse");
					}
				}
				catch (Exception)
				{
					diagnosticsAggregationServiceClient.Abort();
					throw;
				}
				ProbeResult result = base.Result;
				result.ExecutionContext += "GetLocalView succeeded. ";
				AggregatedViewRequest aggregatedViewRequest = new AggregatedViewRequest(RequestType.Queues, new List<string>
				{
					Environment.MachineName
				}, 1U);
				aggregatedViewRequest.QueueAggregatedViewRequest = new QueueAggregatedViewRequest(QueueDigestGroupBy.NextHopDomain, DetailsLevel.Normal, null);
				try
				{
					AggregatedViewResponse aggregatedView = diagnosticsAggregationServiceClient.GetAggregatedView(aggregatedViewRequest);
					if (aggregatedView.QueueAggregatedViewResponse == null)
					{
						throw new ApplicationException("GetAggregatedView returned with a null QueueAggregatedViewResponse");
					}
				}
				catch (Exception)
				{
					diagnosticsAggregationServiceClient.Abort();
					throw;
				}
				ProbeResult result2 = base.Result;
				result2.ExecutionContext += "GetAggregatedView succeeded. ";
			}
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x0003C130 File Offset: 0x0003A330
		private Binding GetWebServiceBinding()
		{
			return new NetTcpBinding
			{
				Security = 
				{
					Transport = 
					{
						ProtectionLevel = ProtectionLevel.EncryptAndSign,
						ClientCredentialType = TcpClientCredentialType.Windows
					},
					Message = 
					{
						ClientCredentialType = MessageCredentialType.Windows
					}
				},
				MaxReceivedMessageSize = (long)((int)ByteQuantifiedSize.FromMB(5UL).ToBytes()),
				OpenTimeout = this.timeout,
				CloseTimeout = this.timeout,
				SendTimeout = this.timeout,
				ReceiveTimeout = this.timeout
			};
		}

		// Token: 0x040009C7 RID: 2503
		private static object locker = new object();

		// Token: 0x040009C8 RID: 2504
		private static ConfigurationLoader<TransportSettingsConfiguration, TransportSettingsConfiguration.Builder> transportSettings;

		// Token: 0x040009C9 RID: 2505
		private readonly EnhancedTimeSpan timeout = EnhancedTimeSpan.FromSeconds(10.0);
	}
}
