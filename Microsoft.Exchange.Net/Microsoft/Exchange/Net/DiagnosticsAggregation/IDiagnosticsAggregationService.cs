using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Net.DiagnosticsAggregation
{
	// Token: 0x02000843 RID: 2115
	[ServiceContract(Namespace = "http://schemas.microsoft.com/exchange/services/2010/DiagnosticsAggregation")]
	internal interface IDiagnosticsAggregationService
	{
		// Token: 0x06002D1E RID: 11550
		[FaultContract(typeof(DiagnosticsAggregationFault))]
		[OperationContract]
		LocalViewResponse GetLocalView(LocalViewRequest request);

		// Token: 0x06002D1F RID: 11551
		[OperationContract]
		[FaultContract(typeof(DiagnosticsAggregationFault))]
		AggregatedViewResponse GetAggregatedView(AggregatedViewRequest request);
	}
}
