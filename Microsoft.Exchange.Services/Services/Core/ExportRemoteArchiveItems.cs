using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002E7 RID: 743
	internal sealed class ExportRemoteArchiveItems : SingleStepServiceCommand<ExportItemsRequest, ExportItemsResponse>
	{
		// Token: 0x060014C1 RID: 5313 RVA: 0x00067C38 File Offset: 0x00065E38
		public ExportRemoteArchiveItems(CallContext callContext, ExportItemsRequest request) : base(callContext, request)
		{
			ServiceCommandBase.ThrowIfNull(request, "remoteArchiveRequest", "ExportRemoteArchiveItems::ExportRemoteArchiveItems");
			this.ids = request.Ids;
			this.archiveService = ((IRemoteArchiveRequest)request).ArchiveService;
			ServiceCommandBase.ThrowIfNull(this.ids, "this.ids", "ExportRemoteArchiveItems::ExportRemoteArchiveItems");
			ServiceCommandBase.ThrowIfNull(this.archiveService, "this.archiveService", "ExportRemoteArchiveItems::ExportRemoteArchiveItems");
			this.archiveService.Timeout = EwsClientHelper.RemoteEwsClientTimeout;
			this.ExportItemsFunc = new Func<ExportItemsType, ExportItemsResponseType>(this.archiveService.ExportItems);
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x00067CC8 File Offset: 0x00065EC8
		// (set) Token: 0x060014C3 RID: 5315 RVA: 0x00067CD0 File Offset: 0x00065ED0
		internal Func<ExportItemsType, ExportItemsResponseType> ExportItemsFunc { get; set; }

		// Token: 0x060014C4 RID: 5316 RVA: 0x00067D00 File Offset: 0x00065F00
		internal override ServiceResult<ExportItemsResponse> Execute()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			ExportItemsRequest exportItemsRequest = new ExportItemsRequest();
			exportItemsRequest.Ids = this.ids;
			ExportItemsResponse exportItemsResponse;
			try
			{
				ExportItemsType exportItemsType = EwsClientHelper.Convert<ExportItemsRequest, ExportItemsType>(exportItemsRequest);
				Exception ex = null;
				ExportItemsResponseType exportItemsResponseType = null;
				bool flag = EwsClientHelper.ExecuteEwsCall(delegate
				{
					exportItemsResponseType = this.ExportItemsFunc(exportItemsType);
				}, out ex);
				if (flag)
				{
					exportItemsResponse = EwsClientHelper.Convert<ExportItemsResponseType, ExportItemsResponse>(exportItemsResponseType);
				}
				else
				{
					ServiceError error = new ServiceError(CoreResources.IDs.ErrorExportRemoteArchiveItemsFailed, Microsoft.Exchange.Services.Core.Types.ResponseCodeType.ErrorInternalServerError, 0, ExchangeVersion.Exchange2012);
					ExTraceGlobals.SearchTracer.TraceError<string, string>((long)this.GetHashCode(), "[ExportRemoteArchiveItems::ExecuteRemoteArchiveExportItems] Export items against URL {0} failed with error: {1}.", this.archiveService.Url, ex.Message);
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericError(RequestDetailsLogger.Current, "FailedExportRemoteArchiveItems", ex.Message);
					exportItemsResponse = new ExportItemsResponse();
					exportItemsResponse.AddResponse(exportItemsResponse.CreateResponseMessage<ExportItemsResponseMessage>(ServiceResultCode.Error, error, null));
				}
			}
			finally
			{
				stopwatch.Stop();
				RequestDetailsLogger.Current.AppendGenericInfo("ExportRemoteArchiveItemsProcessingTime", stopwatch.ElapsedMilliseconds);
			}
			return new ServiceResult<ExportItemsResponse>(exportItemsResponse);
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x00067E1C File Offset: 0x0006601C
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return base.Result.Value;
		}

		// Token: 0x04000E02 RID: 3586
		private const string ProcessingTimeLogField = "ExportRemoteArchiveItemsProcessingTime";

		// Token: 0x04000E03 RID: 3587
		private const string FailureMessageLogField = "FailedExportRemoteArchiveItems";

		// Token: 0x04000E04 RID: 3588
		private XmlNodeArray ids;

		// Token: 0x04000E05 RID: 3589
		private ExchangeServiceBinding archiveService;
	}
}
