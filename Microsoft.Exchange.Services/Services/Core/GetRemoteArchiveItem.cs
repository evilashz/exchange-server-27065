using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000324 RID: 804
	internal sealed class GetRemoteArchiveItem : SingleStepServiceCommand<GetItemRequest, GetItemResponse>
	{
		// Token: 0x060016BB RID: 5819 RVA: 0x000785B4 File Offset: 0x000767B4
		public GetRemoteArchiveItem(CallContext callContext, GetItemRequest request) : base(callContext, request)
		{
			ServiceCommandBase.ThrowIfNull(request, "remoteArchiveRequest", "GetRemoteArchiveItem::GetRemoteArchiveItem");
			this.itemIds = request.Ids;
			this.responseShape = Global.ResponseShapeResolver.GetResponseShape<ItemResponseShape>(base.Request.ShapeName, base.Request.ItemShape, null);
			this.archiveService = ((IRemoteArchiveRequest)request).ArchiveService;
			ServiceCommandBase.ThrowIfNullOrEmpty<BaseItemId>(this.itemIds, "this.itemIds", "GetRemoteArchiveItem::GetRemoteArchiveItem");
			ServiceCommandBase.ThrowIfNull(this.responseShape, "this.responseShape", "GetRemoteArchiveItem::GetRemoteArchiveItem");
			ServiceCommandBase.ThrowIfNull(this.archiveService, "this.archiveService", "GetRemoteArchiveItem::GetRemoteArchiveItem");
			this.archiveService.Timeout = EwsClientHelper.RemoteEwsClientTimeout;
			this.GetItemFunc = new Func<GetItemType, GetItemResponseType>(this.archiveService.GetItem);
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x00078680 File Offset: 0x00076880
		// (set) Token: 0x060016BD RID: 5821 RVA: 0x00078688 File Offset: 0x00076888
		internal Func<GetItemType, GetItemResponseType> GetItemFunc { get; set; }

		// Token: 0x060016BE RID: 5822 RVA: 0x000786B8 File Offset: 0x000768B8
		internal override ServiceResult<GetItemResponse> Execute()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			GetItemRequest getItemRequest = new GetItemRequest();
			getItemRequest.Ids = this.itemIds;
			getItemRequest.ItemShape = this.responseShape;
			GetItemResponse getItemResponse;
			try
			{
				GetItemType getItemType = EwsClientHelper.Convert<GetItemRequest, GetItemType>(getItemRequest);
				Exception ex = null;
				GetItemResponseType getItemResponseType = null;
				bool flag = EwsClientHelper.ExecuteEwsCall(delegate
				{
					getItemResponseType = this.GetItemFunc(getItemType);
				}, out ex);
				if (flag)
				{
					getItemResponse = EwsClientHelper.Convert<GetItemResponseType, GetItemResponse>(getItemResponseType);
				}
				else
				{
					ServiceError error = new ServiceError((CoreResources.IDs)4106572054U, Microsoft.Exchange.Services.Core.Types.ResponseCodeType.ErrorInternalServerError, 0, ExchangeVersion.Exchange2012);
					ExTraceGlobals.SearchTracer.TraceError<string, string>((long)this.GetHashCode(), "[GetRemoteArchiveItem::ExecuteRemoteArchiveGetItem] Get item against URL {0} failed with error: {1}.", this.archiveService.Url, ex.Message);
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericError(RequestDetailsLogger.Current, "FailedGetRemoteArchiveItem", ex.Message);
					getItemResponse = new GetItemResponse();
					getItemResponse.AddResponse(getItemResponse.CreateResponseMessage<Microsoft.Exchange.Services.Core.Types.ItemType>(ServiceResultCode.Error, error, null));
				}
			}
			finally
			{
				stopwatch.Stop();
				CallContext.Current.ProtocolLog.AppendGenericInfo("GetRemoteArchiveItemProcessingTime", stopwatch.ElapsedMilliseconds);
			}
			return new ServiceResult<GetItemResponse>(getItemResponse);
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x000787E4 File Offset: 0x000769E4
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return base.Result.Value;
		}

		// Token: 0x04000F56 RID: 3926
		private const string ProcessingTimeLogField = "GetRemoteArchiveItemProcessingTime";

		// Token: 0x04000F57 RID: 3927
		private const string FailureMessageLogField = "FailedGetRemoteArchiveItem";

		// Token: 0x04000F58 RID: 3928
		private BaseItemId[] itemIds;

		// Token: 0x04000F59 RID: 3929
		private ItemResponseShape responseShape;

		// Token: 0x04000F5A RID: 3930
		private ExchangeServiceBinding archiveService;
	}
}
