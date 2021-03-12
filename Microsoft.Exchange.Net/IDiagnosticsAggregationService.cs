using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using Microsoft.Exchange.Net.DiagnosticsAggregation;

// Token: 0x0200082C RID: 2092
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
[ServiceContract(Namespace = "http://schemas.microsoft.com/exchange/services/2010/DiagnosticsAggregation", ConfigurationName = "IDiagnosticsAggregationService")]
internal interface IDiagnosticsAggregationService
{
	// Token: 0x06002C58 RID: 11352
	[FaultContract(typeof(DiagnosticsAggregationFault), Action = "http://schemas.microsoft.com/exchange/services/2010/DiagnosticsAggregation/IDiagnosticsAggregationService/GetLocalViewDiagnosticsAggregationFaultFault", Name = "DiagnosticsAggregationFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.Net.DiagnosticsAggregation")]
	[OperationContract(Action = "http://schemas.microsoft.com/exchange/services/2010/DiagnosticsAggregation/IDiagnosticsAggregationService/GetLocalView", ReplyAction = "http://schemas.microsoft.com/exchange/services/2010/DiagnosticsAggregation/IDiagnosticsAggregationService/GetLocalViewResponse")]
	LocalViewResponse GetLocalView(LocalViewRequest request);

	// Token: 0x06002C59 RID: 11353
	[OperationContract(AsyncPattern = true, Action = "http://schemas.microsoft.com/exchange/services/2010/DiagnosticsAggregation/IDiagnosticsAggregationService/GetLocalView", ReplyAction = "http://schemas.microsoft.com/exchange/services/2010/DiagnosticsAggregation/IDiagnosticsAggregationService/GetLocalViewResponse")]
	IAsyncResult BeginGetLocalView(LocalViewRequest request, AsyncCallback callback, object asyncState);

	// Token: 0x06002C5A RID: 11354
	LocalViewResponse EndGetLocalView(IAsyncResult result);

	// Token: 0x06002C5B RID: 11355
	[OperationContract(Action = "http://schemas.microsoft.com/exchange/services/2010/DiagnosticsAggregation/IDiagnosticsAggregationService/GetAggregatedView", ReplyAction = "http://schemas.microsoft.com/exchange/services/2010/DiagnosticsAggregation/IDiagnosticsAggregationService/GetAggregatedViewResponse")]
	[FaultContract(typeof(DiagnosticsAggregationFault), Action = "http://schemas.microsoft.com/exchange/services/2010/DiagnosticsAggregation/IDiagnosticsAggregationService/GetAggregatedViewDiagnosticsAggregationFaultFault", Name = "DiagnosticsAggregationFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.Net.DiagnosticsAggregation")]
	AggregatedViewResponse GetAggregatedView(AggregatedViewRequest request);

	// Token: 0x06002C5C RID: 11356
	[OperationContract(AsyncPattern = true, Action = "http://schemas.microsoft.com/exchange/services/2010/DiagnosticsAggregation/IDiagnosticsAggregationService/GetAggregatedView", ReplyAction = "http://schemas.microsoft.com/exchange/services/2010/DiagnosticsAggregation/IDiagnosticsAggregationService/GetAggregatedViewResponse")]
	IAsyncResult BeginGetAggregatedView(AggregatedViewRequest request, AsyncCallback callback, object asyncState);

	// Token: 0x06002C5D RID: 11357
	AggregatedViewResponse EndGetAggregatedView(IAsyncResult result);
}
