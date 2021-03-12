using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200027B RID: 635
	[ServiceContract(Namespace = "ECP", Name = "ClientPerformance")]
	public interface IClientPerformanceService
	{
		// Token: 0x060029CA RID: 10698
		[OperationContract]
		string ReportWatson(ClientWatson report);

		// Token: 0x060029CB RID: 10699
		[OperationContract]
		bool LogClientDatapoint(Datapoint[] datapoints);
	}
}
