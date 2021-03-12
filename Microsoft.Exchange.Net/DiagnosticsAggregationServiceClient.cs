using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Exchange.Net.DiagnosticsAggregation;

// Token: 0x0200082E RID: 2094
[DebuggerStepThrough]
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
internal class DiagnosticsAggregationServiceClient : ClientBase<global::IDiagnosticsAggregationService>, global::IDiagnosticsAggregationService
{
	// Token: 0x06002C5E RID: 11358 RVA: 0x00064C7E File Offset: 0x00062E7E
	public DiagnosticsAggregationServiceClient()
	{
	}

	// Token: 0x06002C5F RID: 11359 RVA: 0x00064C86 File Offset: 0x00062E86
	public DiagnosticsAggregationServiceClient(string endpointConfigurationName) : base(endpointConfigurationName)
	{
	}

	// Token: 0x06002C60 RID: 11360 RVA: 0x00064C8F File Offset: 0x00062E8F
	public DiagnosticsAggregationServiceClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
	{
	}

	// Token: 0x06002C61 RID: 11361 RVA: 0x00064C99 File Offset: 0x00062E99
	public DiagnosticsAggregationServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
	{
	}

	// Token: 0x06002C62 RID: 11362 RVA: 0x00064CA3 File Offset: 0x00062EA3
	public DiagnosticsAggregationServiceClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
	{
	}

	// Token: 0x06002C63 RID: 11363 RVA: 0x00064CAD File Offset: 0x00062EAD
	public LocalViewResponse GetLocalView(LocalViewRequest request)
	{
		return base.Channel.GetLocalView(request);
	}

	// Token: 0x06002C64 RID: 11364 RVA: 0x00064CBB File Offset: 0x00062EBB
	public IAsyncResult BeginGetLocalView(LocalViewRequest request, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginGetLocalView(request, callback, asyncState);
	}

	// Token: 0x06002C65 RID: 11365 RVA: 0x00064CCB File Offset: 0x00062ECB
	public LocalViewResponse EndGetLocalView(IAsyncResult result)
	{
		return base.Channel.EndGetLocalView(result);
	}

	// Token: 0x06002C66 RID: 11366 RVA: 0x00064CD9 File Offset: 0x00062ED9
	public AggregatedViewResponse GetAggregatedView(AggregatedViewRequest request)
	{
		return base.Channel.GetAggregatedView(request);
	}

	// Token: 0x06002C67 RID: 11367 RVA: 0x00064CE7 File Offset: 0x00062EE7
	public IAsyncResult BeginGetAggregatedView(AggregatedViewRequest request, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginGetAggregatedView(request, callback, asyncState);
	}

	// Token: 0x06002C68 RID: 11368 RVA: 0x00064CF7 File Offset: 0x00062EF7
	public AggregatedViewResponse EndGetAggregatedView(IAsyncResult result)
	{
		return base.Channel.EndGetAggregatedView(result);
	}
}
