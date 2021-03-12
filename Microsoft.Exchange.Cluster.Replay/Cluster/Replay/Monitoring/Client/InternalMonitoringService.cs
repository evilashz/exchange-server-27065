using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Microsoft.Exchange.Cluster.Replay.Monitoring.Client
{
	// Token: 0x020001D3 RID: 467
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[ServiceContract(Namespace = "http://Microsoft.Exchange.HA.Monitoring/InternalMonitoringService/v1/", ConfigurationName = "Microsoft.Exchange.Cluster.Replay.Monitoring.Client.InternalMonitoringService")]
	internal interface InternalMonitoringService
	{
		// Token: 0x060012AE RID: 4782
		[OperationContract(Action = "http://Microsoft.Exchange.HA.Monitoring/InternalMonitoringService/v1/InternalMonitoringService/GetVersion", ReplyAction = "http://Microsoft.Exchange.HA.Monitoring/InternalMonitoringService/v1/InternalMonitoringService/GetVersionResponse")]
		ServiceVersion GetVersion();

		// Token: 0x060012AF RID: 4783
		[OperationContract(Action = "http://Microsoft.Exchange.HA.Monitoring/InternalMonitoringService/v1/InternalMonitoringService/GetVersion", ReplyAction = "http://Microsoft.Exchange.HA.Monitoring/InternalMonitoringService/v1/InternalMonitoringService/GetVersionResponse")]
		Task<ServiceVersion> GetVersionAsync();

		// Token: 0x060012B0 RID: 4784
		[OperationContract(Action = "http://Microsoft.Exchange.HA.Monitoring/InternalMonitoringService/v1/InternalMonitoringService/PublishDagHealthInfo", ReplyAction = "http://Microsoft.Exchange.HA.Monitoring/InternalMonitoringService/v1/InternalMonitoringService/PublishDagHealthInfoResponse")]
		void PublishDagHealthInfo(HealthInfoPersisted healthInfo);

		// Token: 0x060012B1 RID: 4785
		[OperationContract(Action = "http://Microsoft.Exchange.HA.Monitoring/InternalMonitoringService/v1/InternalMonitoringService/PublishDagHealthInfo", ReplyAction = "http://Microsoft.Exchange.HA.Monitoring/InternalMonitoringService/v1/InternalMonitoringService/PublishDagHealthInfoResponse")]
		Task PublishDagHealthInfoAsync(HealthInfoPersisted healthInfo);

		// Token: 0x060012B2 RID: 4786
		[OperationContract(Action = "http://Microsoft.Exchange.HA.Monitoring/InternalMonitoringService/v1/InternalMonitoringService/GetDagHealthInfoUpdateTimeUtc", ReplyAction = "http://Microsoft.Exchange.HA.Monitoring/InternalMonitoringService/v1/InternalMonitoringService/GetDagHealthInfoUpdateTimeUtcResponse")]
		DateTime GetDagHealthInfoUpdateTimeUtc();

		// Token: 0x060012B3 RID: 4787
		[OperationContract(Action = "http://Microsoft.Exchange.HA.Monitoring/InternalMonitoringService/v1/InternalMonitoringService/GetDagHealthInfoUpdateTimeUtc", ReplyAction = "http://Microsoft.Exchange.HA.Monitoring/InternalMonitoringService/v1/InternalMonitoringService/GetDagHealthInfoUpdateTimeUtcResponse")]
		Task<DateTime> GetDagHealthInfoUpdateTimeUtcAsync();

		// Token: 0x060012B4 RID: 4788
		[OperationContract(Action = "http://Microsoft.Exchange.HA.Monitoring/InternalMonitoringService/v1/InternalMonitoringService/GetDagHealthInfo", ReplyAction = "http://Microsoft.Exchange.HA.Monitoring/InternalMonitoringService/v1/InternalMonitoringService/GetDagHealthInfoResponse")]
		HealthInfoPersisted GetDagHealthInfo();

		// Token: 0x060012B5 RID: 4789
		[OperationContract(Action = "http://Microsoft.Exchange.HA.Monitoring/InternalMonitoringService/v1/InternalMonitoringService/GetDagHealthInfo", ReplyAction = "http://Microsoft.Exchange.HA.Monitoring/InternalMonitoringService/v1/InternalMonitoringService/GetDagHealthInfoResponse")]
		Task<HealthInfoPersisted> GetDagHealthInfoAsync();
	}
}
