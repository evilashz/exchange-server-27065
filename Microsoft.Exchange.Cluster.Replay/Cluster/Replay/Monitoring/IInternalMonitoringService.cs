using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Cluster.Replay.Monitoring
{
	// Token: 0x020001D6 RID: 470
	[ServiceContract(Name = "InternalMonitoringService", Namespace = "http://Microsoft.Exchange.HA.Monitoring/InternalMonitoringService/v1/")]
	internal interface IInternalMonitoringService
	{
		// Token: 0x060012C3 RID: 4803
		[OperationContract]
		ServiceVersion GetVersion();

		// Token: 0x060012C4 RID: 4804
		[OperationContract]
		void PublishDagHealthInfo(HealthInfoPersisted healthInfo);

		// Token: 0x060012C5 RID: 4805
		[OperationContract]
		DateTime GetDagHealthInfoUpdateTimeUtc();

		// Token: 0x060012C6 RID: 4806
		[OperationContract]
		HealthInfoPersisted GetDagHealthInfo();
	}
}
