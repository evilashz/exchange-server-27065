using System;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.InfoWorker.Availability;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.DispatchPipe.Ews;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B79 RID: 2937
	[MessageInspectorBehavior]
	[ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class EWSService : IEWSContract, IEWSStreamingContract
	{
		// Token: 0x06005430 RID: 21552 RVA: 0x0010D929 File Offset: 0x0010BB29
		public IAsyncResult BeginConvertId(ConvertIdSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<ConvertIdResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005431 RID: 21553 RVA: 0x0010D953 File Offset: 0x0010BB53
		public ConvertIdSoapResponse EndConvertId(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<ConvertIdSoapResponse, ConvertIdResponse>(result, (ConvertIdResponse body) => new ConvertIdSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005432 RID: 21554 RVA: 0x0010D978 File Offset: 0x0010BB78
		public IAsyncResult BeginUploadItems(UploadItemsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<UploadItemsResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005433 RID: 21555 RVA: 0x0010D9A3 File Offset: 0x0010BBA3
		public UploadItemsSoapResponse EndUploadItems(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<UploadItemsSoapResponse, UploadItemsResponse>(result, (UploadItemsResponse body) => new UploadItemsSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005434 RID: 21556 RVA: 0x0010D9C8 File Offset: 0x0010BBC8
		public IAsyncResult BeginExportItems(ExportItemsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<ExportItemsResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005435 RID: 21557 RVA: 0x0010D9F3 File Offset: 0x0010BBF3
		public ExportItemsSoapResponse EndExportItems(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<ExportItemsSoapResponse, ExportItemsResponse>(result, (ExportItemsResponse body) => new ExportItemsSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005436 RID: 21558 RVA: 0x0010DA18 File Offset: 0x0010BC18
		public IAsyncResult BeginGetFolder(GetFolderSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetFolderResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005437 RID: 21559 RVA: 0x0010DA43 File Offset: 0x0010BC43
		public GetFolderSoapResponse EndGetFolder(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetFolderSoapResponse, GetFolderResponse>(result, (GetFolderResponse body) => new GetFolderSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005438 RID: 21560 RVA: 0x0010DA68 File Offset: 0x0010BC68
		public IAsyncResult BeginCreateFolder(CreateFolderSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<CreateFolderResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005439 RID: 21561 RVA: 0x0010DA93 File Offset: 0x0010BC93
		public CreateFolderSoapResponse EndCreateFolder(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<CreateFolderSoapResponse, CreateFolderResponse>(result, (CreateFolderResponse body) => new CreateFolderSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600543A RID: 21562 RVA: 0x0010DAB8 File Offset: 0x0010BCB8
		public IAsyncResult BeginCreateUnifiedMailbox(CreateUnifiedMailboxSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<CreateUnifiedMailboxResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600543B RID: 21563 RVA: 0x0010DAE3 File Offset: 0x0010BCE3
		public CreateUnifiedMailboxSoapResponse EndCreateUnifiedMailbox(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<CreateUnifiedMailboxSoapResponse, CreateUnifiedMailboxResponse>(result, (CreateUnifiedMailboxResponse body) => new CreateUnifiedMailboxSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600543C RID: 21564 RVA: 0x0010DB08 File Offset: 0x0010BD08
		public IAsyncResult BeginDeleteFolder(DeleteFolderSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<DeleteFolderResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600543D RID: 21565 RVA: 0x0010DB33 File Offset: 0x0010BD33
		public DeleteFolderSoapResponse EndDeleteFolder(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<DeleteFolderSoapResponse, DeleteFolderResponse>(result, (DeleteFolderResponse body) => new DeleteFolderSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600543E RID: 21566 RVA: 0x0010DB58 File Offset: 0x0010BD58
		public IAsyncResult BeginEmptyFolder(EmptyFolderSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<EmptyFolderResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600543F RID: 21567 RVA: 0x0010DB83 File Offset: 0x0010BD83
		public EmptyFolderSoapResponse EndEmptyFolder(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<EmptyFolderSoapResponse, EmptyFolderResponse>(result, (EmptyFolderResponse body) => new EmptyFolderSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005440 RID: 21568 RVA: 0x0010DBA8 File Offset: 0x0010BDA8
		public IAsyncResult BeginUpdateFolder(UpdateFolderSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<UpdateFolderResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005441 RID: 21569 RVA: 0x0010DBD3 File Offset: 0x0010BDD3
		public UpdateFolderSoapResponse EndUpdateFolder(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<UpdateFolderSoapResponse, UpdateFolderResponse>(result, (UpdateFolderResponse body) => new UpdateFolderSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005442 RID: 21570 RVA: 0x0010DBF8 File Offset: 0x0010BDF8
		public IAsyncResult BeginMoveFolder(MoveFolderSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<MoveFolderResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005443 RID: 21571 RVA: 0x0010DC23 File Offset: 0x0010BE23
		public MoveFolderSoapResponse EndMoveFolder(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<MoveFolderSoapResponse, MoveFolderResponse>(result, (MoveFolderResponse body) => new MoveFolderSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005444 RID: 21572 RVA: 0x0010DC48 File Offset: 0x0010BE48
		public IAsyncResult BeginCopyFolder(CopyFolderSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<CopyFolderResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005445 RID: 21573 RVA: 0x0010DC73 File Offset: 0x0010BE73
		public CopyFolderSoapResponse EndCopyFolder(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<CopyFolderSoapResponse, CopyFolderResponse>(result, (CopyFolderResponse body) => new CopyFolderSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005446 RID: 21574 RVA: 0x0010DC98 File Offset: 0x0010BE98
		public IAsyncResult BeginFindItem(FindItemSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<FindItemResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005447 RID: 21575 RVA: 0x0010DCC3 File Offset: 0x0010BEC3
		public FindItemSoapResponse EndFindItem(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<FindItemSoapResponse, FindItemResponse>(result, (FindItemResponse body) => new FindItemSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005448 RID: 21576 RVA: 0x0010DCE8 File Offset: 0x0010BEE8
		public IAsyncResult BeginFindFolder(FindFolderSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<FindFolderResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005449 RID: 21577 RVA: 0x0010DD13 File Offset: 0x0010BF13
		public FindFolderSoapResponse EndFindFolder(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<FindFolderSoapResponse, FindFolderResponse>(result, (FindFolderResponse body) => new FindFolderSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600544A RID: 21578 RVA: 0x0010DD38 File Offset: 0x0010BF38
		public IAsyncResult BeginGetItem(GetItemSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetItemResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600544B RID: 21579 RVA: 0x0010DD63 File Offset: 0x0010BF63
		public GetItemSoapResponse EndGetItem(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetItemSoapResponse, GetItemResponse>(result, (GetItemResponse body) => new GetItemSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600544C RID: 21580 RVA: 0x0010DD88 File Offset: 0x0010BF88
		public IAsyncResult BeginCreateFolderPath(CreateFolderPathSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<CreateFolderPathResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600544D RID: 21581 RVA: 0x0010DDB3 File Offset: 0x0010BFB3
		public CreateFolderPathSoapResponse EndCreateFolderPath(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<CreateFolderPathSoapResponse, CreateFolderPathResponse>(result, (CreateFolderPathResponse body) => new CreateFolderPathSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600544E RID: 21582 RVA: 0x0010DDD8 File Offset: 0x0010BFD8
		public IAsyncResult BeginCreateItem(CreateItemSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<CreateItemResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600544F RID: 21583 RVA: 0x0010DE03 File Offset: 0x0010C003
		public CreateItemSoapResponse EndCreateItem(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<CreateItemSoapResponse, CreateItemResponse>(result, (CreateItemResponse body) => new CreateItemSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005450 RID: 21584 RVA: 0x0010DE28 File Offset: 0x0010C028
		public IAsyncResult BeginDeleteItem(DeleteItemSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<DeleteItemResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005451 RID: 21585 RVA: 0x0010DE53 File Offset: 0x0010C053
		public DeleteItemSoapResponse EndDeleteItem(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<DeleteItemSoapResponse, DeleteItemResponse>(result, (DeleteItemResponse body) => new DeleteItemSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005452 RID: 21586 RVA: 0x0010DE78 File Offset: 0x0010C078
		public IAsyncResult BeginUpdateItem(UpdateItemSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<UpdateItemResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005453 RID: 21587 RVA: 0x0010DEA3 File Offset: 0x0010C0A3
		public UpdateItemSoapResponse EndUpdateItem(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<UpdateItemSoapResponse, UpdateItemResponse>(result, (UpdateItemResponse body) => new UpdateItemSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005454 RID: 21588 RVA: 0x0010DEC8 File Offset: 0x0010C0C8
		public IAsyncResult BeginUpdateItemInRecoverableItems(UpdateItemInRecoverableItemsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<UpdateItemInRecoverableItemsResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005455 RID: 21589 RVA: 0x0010DEF3 File Offset: 0x0010C0F3
		public UpdateItemInRecoverableItemsSoapResponse EndUpdateItemInRecoverableItems(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<UpdateItemInRecoverableItemsSoapResponse, UpdateItemInRecoverableItemsResponse>(result, (UpdateItemInRecoverableItemsResponse body) => new UpdateItemInRecoverableItemsSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005456 RID: 21590 RVA: 0x0010DF18 File Offset: 0x0010C118
		public IAsyncResult BeginSendItem(SendItemSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<SendItemResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005457 RID: 21591 RVA: 0x0010DF43 File Offset: 0x0010C143
		public SendItemSoapResponse EndSendItem(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<SendItemSoapResponse, SendItemResponse>(result, (SendItemResponse body) => new SendItemSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005458 RID: 21592 RVA: 0x0010DF68 File Offset: 0x0010C168
		public IAsyncResult BeginMoveItem(MoveItemSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<MoveItemResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005459 RID: 21593 RVA: 0x0010DF93 File Offset: 0x0010C193
		public MoveItemSoapResponse EndMoveItem(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<MoveItemSoapResponse, MoveItemResponse>(result, (MoveItemResponse body) => new MoveItemSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600545A RID: 21594 RVA: 0x0010DFB8 File Offset: 0x0010C1B8
		public IAsyncResult BeginCopyItem(CopyItemSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<CopyItemResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600545B RID: 21595 RVA: 0x0010DFE3 File Offset: 0x0010C1E3
		public CopyItemSoapResponse EndCopyItem(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<CopyItemSoapResponse, CopyItemResponse>(result, (CopyItemResponse body) => new CopyItemSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600545C RID: 21596 RVA: 0x0010E008 File Offset: 0x0010C208
		public IAsyncResult BeginArchiveItem(ArchiveItemSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<ArchiveItemResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600545D RID: 21597 RVA: 0x0010E033 File Offset: 0x0010C233
		public ArchiveItemSoapResponse EndArchiveItem(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<ArchiveItemSoapResponse, ArchiveItemResponse>(result, (ArchiveItemResponse body) => new ArchiveItemSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600545E RID: 21598 RVA: 0x0010E058 File Offset: 0x0010C258
		public IAsyncResult BeginCreateAttachment(CreateAttachmentSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<CreateAttachmentResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600545F RID: 21599 RVA: 0x0010E083 File Offset: 0x0010C283
		public CreateAttachmentSoapResponse EndCreateAttachment(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<CreateAttachmentSoapResponse, CreateAttachmentResponse>(result, (CreateAttachmentResponse body) => new CreateAttachmentSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005460 RID: 21600 RVA: 0x0010E0A8 File Offset: 0x0010C2A8
		public IAsyncResult BeginDeleteAttachment(DeleteAttachmentSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<DeleteAttachmentResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005461 RID: 21601 RVA: 0x0010E0D3 File Offset: 0x0010C2D3
		public DeleteAttachmentSoapResponse EndDeleteAttachment(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<DeleteAttachmentSoapResponse, DeleteAttachmentResponse>(result, (DeleteAttachmentResponse body) => new DeleteAttachmentSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005462 RID: 21602 RVA: 0x0010E0F8 File Offset: 0x0010C2F8
		public IAsyncResult BeginGetAttachment(GetAttachmentSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetAttachmentResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005463 RID: 21603 RVA: 0x0010E123 File Offset: 0x0010C323
		public GetAttachmentSoapResponse EndGetAttachment(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetAttachmentSoapResponse, GetAttachmentResponse>(result, (GetAttachmentResponse body) => new GetAttachmentSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005464 RID: 21604 RVA: 0x0010E148 File Offset: 0x0010C348
		public IAsyncResult BeginGetClientAccessToken(GetClientAccessTokenSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetClientAccessTokenResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005465 RID: 21605 RVA: 0x0010E173 File Offset: 0x0010C373
		public GetClientAccessTokenSoapResponse EndGetClientAccessToken(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetClientAccessTokenSoapResponse, GetClientAccessTokenResponse>(result, (GetClientAccessTokenResponse body) => new GetClientAccessTokenSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005466 RID: 21606 RVA: 0x0010E198 File Offset: 0x0010C398
		public IAsyncResult BeginResolveNames(ResolveNamesSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<ResolveNamesResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005467 RID: 21607 RVA: 0x0010E1C3 File Offset: 0x0010C3C3
		public ResolveNamesSoapResponse EndResolveNames(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<ResolveNamesSoapResponse, ResolveNamesResponse>(result, (ResolveNamesResponse body) => new ResolveNamesSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005468 RID: 21608 RVA: 0x0010E1E8 File Offset: 0x0010C3E8
		public IAsyncResult BeginExpandDL(ExpandDLSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<ExpandDLResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005469 RID: 21609 RVA: 0x0010E213 File Offset: 0x0010C413
		public ExpandDLSoapResponse EndExpandDL(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<ExpandDLSoapResponse, ExpandDLResponse>(result, (ExpandDLResponse body) => new ExpandDLSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600546A RID: 21610 RVA: 0x0010E238 File Offset: 0x0010C438
		public IAsyncResult BeginGetServerTimeZones(GetServerTimeZonesSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetServerTimeZonesResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600546B RID: 21611 RVA: 0x0010E263 File Offset: 0x0010C463
		public GetServerTimeZonesSoapResponse EndGetServerTimeZones(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetServerTimeZonesSoapResponse, GetServerTimeZonesResponse>(result, (GetServerTimeZonesResponse body) => new GetServerTimeZonesSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600546C RID: 21612 RVA: 0x0010E288 File Offset: 0x0010C488
		public IAsyncResult BeginCreateManagedFolder(CreateManagedFolderSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<CreateManagedFolderResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600546D RID: 21613 RVA: 0x0010E2B3 File Offset: 0x0010C4B3
		public CreateManagedFolderSoapResponse EndCreateManagedFolder(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<CreateManagedFolderSoapResponse, CreateManagedFolderResponse>(result, (CreateManagedFolderResponse body) => new CreateManagedFolderSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600546E RID: 21614 RVA: 0x0010E2D8 File Offset: 0x0010C4D8
		public IAsyncResult BeginSubscribe(SubscribeSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<SubscribeResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600546F RID: 21615 RVA: 0x0010E303 File Offset: 0x0010C503
		public SubscribeSoapResponse EndSubscribe(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<SubscribeSoapResponse, SubscribeResponse>(result, (SubscribeResponse body) => new SubscribeSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005470 RID: 21616 RVA: 0x0010E328 File Offset: 0x0010C528
		public IAsyncResult BeginUnsubscribe(UnsubscribeSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<UnsubscribeResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005471 RID: 21617 RVA: 0x0010E353 File Offset: 0x0010C553
		public UnsubscribeSoapResponse EndUnsubscribe(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<UnsubscribeSoapResponse, UnsubscribeResponse>(result, (UnsubscribeResponse body) => new UnsubscribeSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005472 RID: 21618 RVA: 0x0010E378 File Offset: 0x0010C578
		public IAsyncResult BeginGetEvents(GetEventsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetEventsResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005473 RID: 21619 RVA: 0x0010E3A3 File Offset: 0x0010C5A3
		public GetEventsSoapResponse EndGetEvents(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetEventsSoapResponse, GetEventsResponse>(result, (GetEventsResponse body) => new GetEventsSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005474 RID: 21620 RVA: 0x0010E3C8 File Offset: 0x0010C5C8
		public IAsyncResult BeginGetClientExtension(GetClientExtensionSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetClientExtensionResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005475 RID: 21621 RVA: 0x0010E3F3 File Offset: 0x0010C5F3
		public GetClientExtensionSoapResponse EndGetClientExtension(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetClientExtensionSoapResponse, GetClientExtensionResponse>(result, (GetClientExtensionResponse body) => new GetClientExtensionSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005476 RID: 21622 RVA: 0x0010E418 File Offset: 0x0010C618
		public IAsyncResult BeginSetClientExtension(SetClientExtensionSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<SetClientExtensionResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005477 RID: 21623 RVA: 0x0010E443 File Offset: 0x0010C643
		public SetClientExtensionSoapResponse EndSetClientExtension(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<SetClientExtensionSoapResponse, SetClientExtensionResponse>(result, (SetClientExtensionResponse body) => new SetClientExtensionSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005478 RID: 21624 RVA: 0x0010E468 File Offset: 0x0010C668
		public IAsyncResult BeginGetEncryptionConfiguration(GetEncryptionConfigurationSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetEncryptionConfigurationResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005479 RID: 21625 RVA: 0x0010E493 File Offset: 0x0010C693
		public GetEncryptionConfigurationSoapResponse EndGetEncryptionConfiguration(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetEncryptionConfigurationSoapResponse, GetEncryptionConfigurationResponse>(result, (GetEncryptionConfigurationResponse body) => new GetEncryptionConfigurationSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600547A RID: 21626 RVA: 0x0010E4B8 File Offset: 0x0010C6B8
		public IAsyncResult BeginSetEncryptionConfiguration(SetEncryptionConfigurationSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<SetEncryptionConfigurationResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600547B RID: 21627 RVA: 0x0010E4E3 File Offset: 0x0010C6E3
		public SetEncryptionConfigurationSoapResponse EndSetEncryptionConfiguration(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<SetEncryptionConfigurationSoapResponse, SetEncryptionConfigurationResponse>(result, (SetEncryptionConfigurationResponse body) => new SetEncryptionConfigurationSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600547C RID: 21628 RVA: 0x0010E508 File Offset: 0x0010C708
		public IAsyncResult BeginGetAppManifests(GetAppManifestsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetAppManifestsResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600547D RID: 21629 RVA: 0x0010E533 File Offset: 0x0010C733
		public GetAppManifestsSoapResponse EndGetAppManifests(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetAppManifestsSoapResponse, GetAppManifestsResponse>(result, (GetAppManifestsResponse body) => new GetAppManifestsSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600547E RID: 21630 RVA: 0x0010E558 File Offset: 0x0010C758
		public IAsyncResult BeginInstallApp(InstallAppSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<InstallAppResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600547F RID: 21631 RVA: 0x0010E583 File Offset: 0x0010C783
		public InstallAppSoapResponse EndInstallApp(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<InstallAppSoapResponse, InstallAppResponse>(result, (InstallAppResponse body) => new InstallAppSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005480 RID: 21632 RVA: 0x0010E5A8 File Offset: 0x0010C7A8
		public IAsyncResult BeginUninstallApp(UninstallAppSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<UninstallAppResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005481 RID: 21633 RVA: 0x0010E5D3 File Offset: 0x0010C7D3
		public UninstallAppSoapResponse EndUninstallApp(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<UninstallAppSoapResponse, UninstallAppResponse>(result, (UninstallAppResponse body) => new UninstallAppSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005482 RID: 21634 RVA: 0x0010E5F8 File Offset: 0x0010C7F8
		public IAsyncResult BeginDisableApp(DisableAppSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<DisableAppResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005483 RID: 21635 RVA: 0x0010E623 File Offset: 0x0010C823
		public DisableAppSoapResponse EndDisableApp(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<DisableAppSoapResponse, DisableAppResponse>(result, (DisableAppResponse body) => new DisableAppSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005484 RID: 21636 RVA: 0x0010E648 File Offset: 0x0010C848
		public IAsyncResult BeginGetAppMarketplaceUrl(GetAppMarketplaceUrlSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetAppMarketplaceUrlResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005485 RID: 21637 RVA: 0x0010E673 File Offset: 0x0010C873
		public GetAppMarketplaceUrlSoapResponse EndGetAppMarketplaceUrl(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetAppMarketplaceUrlSoapResponse, GetAppMarketplaceUrlResponse>(result, (GetAppMarketplaceUrlResponse body) => new GetAppMarketplaceUrlSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005486 RID: 21638 RVA: 0x0010E698 File Offset: 0x0010C898
		public IAsyncResult BeginAddAggregatedAccount(AddAggregatedAccountSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<AddAggregatedAccountResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005487 RID: 21639 RVA: 0x0010E6C3 File Offset: 0x0010C8C3
		public AddAggregatedAccountSoapResponse EndAddAggregatedAccount(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<AddAggregatedAccountSoapResponse, AddAggregatedAccountResponse>(result, (AddAggregatedAccountResponse body) => new AddAggregatedAccountSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005488 RID: 21640 RVA: 0x0010E6E8 File Offset: 0x0010C8E8
		public IAsyncResult BeginGetStreamingEvents(GetStreamingEventsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetStreamingEventsResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005489 RID: 21641 RVA: 0x0010E6F8 File Offset: 0x0010C8F8
		public GetStreamingEventsSoapResponse EndGetStreamingEvents(IAsyncResult result)
		{
			ServiceAsyncResult<GetStreamingEventsResponse> serviceAsyncResult = EWSService.GetServiceAsyncResult<GetStreamingEventsResponse>(result);
			GetStreamingEventsSoapResponse getStreamingEventsSoapResponse = new GetStreamingEventsSoapResponse();
			getStreamingEventsSoapResponse.Body = serviceAsyncResult.Data;
			PerformanceMonitor.UpdateTotalCompletedRequestsCount();
			return getStreamingEventsSoapResponse;
		}

		// Token: 0x0600548A RID: 21642 RVA: 0x0010E724 File Offset: 0x0010C924
		public IAsyncResult BeginSyncFolderHierarchy(SyncFolderHierarchySoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<SyncFolderHierarchyResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600548B RID: 21643 RVA: 0x0010E74F File Offset: 0x0010C94F
		public SyncFolderHierarchySoapResponse EndSyncFolderHierarchy(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<SyncFolderHierarchySoapResponse, SyncFolderHierarchyResponse>(result, (SyncFolderHierarchyResponse body) => new SyncFolderHierarchySoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600548C RID: 21644 RVA: 0x0010E774 File Offset: 0x0010C974
		public IAsyncResult BeginSyncFolderItems(SyncFolderItemsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<SyncFolderItemsResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600548D RID: 21645 RVA: 0x0010E79F File Offset: 0x0010C99F
		public SyncFolderItemsSoapResponse EndSyncFolderItems(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<SyncFolderItemsSoapResponse, SyncFolderItemsResponse>(result, (SyncFolderItemsResponse body) => new SyncFolderItemsSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600548E RID: 21646 RVA: 0x0010E7C4 File Offset: 0x0010C9C4
		public IAsyncResult BeginGetDelegate(GetDelegateSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetDelegateResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x0600548F RID: 21647 RVA: 0x0010E7EF File Offset: 0x0010C9EF
		public GetDelegateSoapResponse EndGetDelegate(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetDelegateSoapResponse, GetDelegateResponseMessage>(result, (GetDelegateResponseMessage body) => new GetDelegateSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005490 RID: 21648 RVA: 0x0010E814 File Offset: 0x0010CA14
		public IAsyncResult BeginAddDelegate(AddDelegateSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<AddDelegateResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06005491 RID: 21649 RVA: 0x0010E83F File Offset: 0x0010CA3F
		public AddDelegateSoapResponse EndAddDelegate(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<AddDelegateSoapResponse, AddDelegateResponseMessage>(result, (AddDelegateResponseMessage body) => new AddDelegateSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005492 RID: 21650 RVA: 0x0010E864 File Offset: 0x0010CA64
		public IAsyncResult BeginRemoveDelegate(RemoveDelegateSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<RemoveDelegateResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06005493 RID: 21651 RVA: 0x0010E88F File Offset: 0x0010CA8F
		public RemoveDelegateSoapResponse EndRemoveDelegate(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<RemoveDelegateSoapResponse, RemoveDelegateResponseMessage>(result, (RemoveDelegateResponseMessage body) => new RemoveDelegateSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005494 RID: 21652 RVA: 0x0010E8B4 File Offset: 0x0010CAB4
		public IAsyncResult BeginUpdateDelegate(UpdateDelegateSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<UpdateDelegateResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06005495 RID: 21653 RVA: 0x0010E8DF File Offset: 0x0010CADF
		public UpdateDelegateSoapResponse EndUpdateDelegate(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<UpdateDelegateSoapResponse, UpdateDelegateResponseMessage>(result, (UpdateDelegateResponseMessage body) => new UpdateDelegateSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005496 RID: 21654 RVA: 0x0010E904 File Offset: 0x0010CB04
		public IAsyncResult BeginCreateUserConfiguration(CreateUserConfigurationSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<CreateUserConfigurationResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005497 RID: 21655 RVA: 0x0010E92F File Offset: 0x0010CB2F
		public CreateUserConfigurationSoapResponse EndCreateUserConfiguration(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<CreateUserConfigurationSoapResponse, CreateUserConfigurationResponse>(result, (CreateUserConfigurationResponse body) => new CreateUserConfigurationSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005498 RID: 21656 RVA: 0x0010E954 File Offset: 0x0010CB54
		public IAsyncResult BeginDeleteUserConfiguration(DeleteUserConfigurationSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<DeleteUserConfigurationResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005499 RID: 21657 RVA: 0x0010E97F File Offset: 0x0010CB7F
		public DeleteUserConfigurationSoapResponse EndDeleteUserConfiguration(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<DeleteUserConfigurationSoapResponse, DeleteUserConfigurationResponse>(result, (DeleteUserConfigurationResponse body) => new DeleteUserConfigurationSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600549A RID: 21658 RVA: 0x0010E9A4 File Offset: 0x0010CBA4
		public IAsyncResult BeginGetUserConfiguration(GetUserConfigurationSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetUserConfigurationResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600549B RID: 21659 RVA: 0x0010E9CF File Offset: 0x0010CBCF
		public GetUserConfigurationSoapResponse EndGetUserConfiguration(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetUserConfigurationSoapResponse, GetUserConfigurationResponse>(result, (GetUserConfigurationResponse body) => new GetUserConfigurationSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600549C RID: 21660 RVA: 0x0010E9F4 File Offset: 0x0010CBF4
		public IAsyncResult BeginUpdateUserConfiguration(UpdateUserConfigurationSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<UpdateUserConfigurationResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600549D RID: 21661 RVA: 0x0010EA1F File Offset: 0x0010CC1F
		public UpdateUserConfigurationSoapResponse EndUpdateUserConfiguration(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<UpdateUserConfigurationSoapResponse, UpdateUserConfigurationResponse>(result, (UpdateUserConfigurationResponse body) => new UpdateUserConfigurationSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600549E RID: 21662 RVA: 0x0010EA44 File Offset: 0x0010CC44
		public IAsyncResult BeginGetServiceConfiguration(GetServiceConfigurationSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			ExTraceGlobals.GetOrganizationConfigurationCallTracer.TraceDebug((long)this.GetHashCode(), "WcfService.GetServiceConfiguration called");
			return soapRequest.Body.Submit<GetServiceConfigurationResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x0600549F RID: 21663 RVA: 0x0010EA87 File Offset: 0x0010CC87
		public GetServiceConfigurationSoapResponse EndGetServiceConfiguration(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetServiceConfigurationSoapResponse, GetServiceConfigurationResponseMessage>(result, (GetServiceConfigurationResponseMessage body) => new GetServiceConfigurationSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054A0 RID: 21664 RVA: 0x0010EAAC File Offset: 0x0010CCAC
		public IAsyncResult BeginGetMailTips(GetMailTipsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			ExTraceGlobals.GetMailTipsCallTracer.TraceDebug((long)this.GetHashCode(), "WcfService.GetMailTips called");
			return soapRequest.Body.Submit<GetMailTipsResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x060054A1 RID: 21665 RVA: 0x0010EAEF File Offset: 0x0010CCEF
		public GetMailTipsSoapResponse EndGetMailTips(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetMailTipsSoapResponse, GetMailTipsResponseMessage>(result, (GetMailTipsResponseMessage body) => new GetMailTipsSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054A2 RID: 21666 RVA: 0x0010EB14 File Offset: 0x0010CD14
		public IAsyncResult BeginPlayOnPhone(PlayOnPhoneSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<PlayOnPhoneResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x060054A3 RID: 21667 RVA: 0x0010EB3F File Offset: 0x0010CD3F
		public PlayOnPhoneSoapResponse EndPlayOnPhone(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<PlayOnPhoneSoapResponse, PlayOnPhoneResponseMessage>(result, (PlayOnPhoneResponseMessage body) => new PlayOnPhoneSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054A4 RID: 21668 RVA: 0x0010EB64 File Offset: 0x0010CD64
		public IAsyncResult BeginGetPhoneCallInformation(GetPhoneCallInformationSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetPhoneCallInformationResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x060054A5 RID: 21669 RVA: 0x0010EB8F File Offset: 0x0010CD8F
		public GetPhoneCallInformationSoapResponse EndGetPhoneCallInformation(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetPhoneCallInformationSoapResponse, GetPhoneCallInformationResponseMessage>(result, (GetPhoneCallInformationResponseMessage body) => new GetPhoneCallInformationSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054A6 RID: 21670 RVA: 0x0010EBB4 File Offset: 0x0010CDB4
		public IAsyncResult BeginDisconnectPhoneCall(DisconnectPhoneCallSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<DisconnectPhoneCallResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x060054A7 RID: 21671 RVA: 0x0010EBDF File Offset: 0x0010CDDF
		public DisconnectPhoneCallSoapResponse EndDisconnectPhoneCall(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<DisconnectPhoneCallSoapResponse, DisconnectPhoneCallResponseMessage>(result, (DisconnectPhoneCallResponseMessage body) => new DisconnectPhoneCallSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054A8 RID: 21672 RVA: 0x0010EC04 File Offset: 0x0010CE04
		public IAsyncResult BeginGetUMPrompt(GetUMPromptSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetUMPromptResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x060054A9 RID: 21673 RVA: 0x0010EC2F File Offset: 0x0010CE2F
		public GetUMPromptSoapResponse EndGetUMPrompt(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetUMPromptSoapResponse, GetUMPromptResponseMessage>(result, (GetUMPromptResponseMessage body) => new GetUMPromptSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054AA RID: 21674 RVA: 0x0010EC54 File Offset: 0x0010CE54
		public IAsyncResult BeginGetUMPromptNames(GetUMPromptNamesSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetUMPromptNamesResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x060054AB RID: 21675 RVA: 0x0010EC7F File Offset: 0x0010CE7F
		public GetUMPromptNamesSoapResponse EndGetUMPromptNames(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetUMPromptNamesSoapResponse, GetUMPromptNamesResponseMessage>(result, (GetUMPromptNamesResponseMessage body) => new GetUMPromptNamesSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054AC RID: 21676 RVA: 0x0010ECA4 File Offset: 0x0010CEA4
		public IAsyncResult BeginDeleteUMPrompts(DeleteUMPromptsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<DeleteUMPromptsResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x060054AD RID: 21677 RVA: 0x0010ECCF File Offset: 0x0010CECF
		public DeleteUMPromptsSoapResponse EndDeleteUMPrompts(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<DeleteUMPromptsSoapResponse, DeleteUMPromptsResponseMessage>(result, (DeleteUMPromptsResponseMessage body) => new DeleteUMPromptsSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054AE RID: 21678 RVA: 0x0010ECF4 File Offset: 0x0010CEF4
		public IAsyncResult BeginCreateUMPrompt(CreateUMPromptSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<CreateUMPromptResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x060054AF RID: 21679 RVA: 0x0010ED1F File Offset: 0x0010CF1F
		public CreateUMPromptSoapResponse EndCreateUMPrompt(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<CreateUMPromptSoapResponse, CreateUMPromptResponseMessage>(result, (CreateUMPromptResponseMessage body) => new CreateUMPromptSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054B0 RID: 21680 RVA: 0x0010ED44 File Offset: 0x0010CF44
		public IAsyncResult BeginGetUserAvailability(GetUserAvailabilitySoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			object obj;
			if (EwsOperationContextBase.Current.IncomingMessageProperties.TryGetValue("DefaultFreeBusyAccessOnly", out obj) && obj is bool)
			{
				soapRequest.Body.DefaultFreeBusyAccessOnly = (bool)obj;
			}
			return soapRequest.Body.Submit<GetUserAvailabilityResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060054B1 RID: 21681 RVA: 0x0010EDAB File Offset: 0x0010CFAB
		public GetUserAvailabilitySoapResponse EndGetUserAvailability(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetUserAvailabilitySoapResponse, GetUserAvailabilityResponse>(result, (GetUserAvailabilityResponse body) => new GetUserAvailabilitySoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054B2 RID: 21682 RVA: 0x0010EDD0 File Offset: 0x0010CFD0
		public IAsyncResult BeginGetUserOofSettings(GetUserOofSettingsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetUserOofSettingsResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060054B3 RID: 21683 RVA: 0x0010EDFB File Offset: 0x0010CFFB
		public GetUserOofSettingsSoapResponse EndGetUserOofSettings(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetUserOofSettingsSoapResponse, GetUserOofSettingsResponse>(result, (GetUserOofSettingsResponse body) => new GetUserOofSettingsSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054B4 RID: 21684 RVA: 0x0010EE20 File Offset: 0x0010D020
		public IAsyncResult BeginSetUserOofSettings(SetUserOofSettingsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<SetUserOofSettingsResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060054B5 RID: 21685 RVA: 0x0010EE4B File Offset: 0x0010D04B
		public SetUserOofSettingsSoapResponse EndSetUserOofSettings(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<SetUserOofSettingsSoapResponse, SetUserOofSettingsResponse>(result, (SetUserOofSettingsResponse body) => new SetUserOofSettingsSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054B6 RID: 21686 RVA: 0x0010EE70 File Offset: 0x0010D070
		public IAsyncResult BeginGetSharingMetadata(GetSharingMetadataSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetSharingMetadataResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x060054B7 RID: 21687 RVA: 0x0010EE9B File Offset: 0x0010D09B
		public GetSharingMetadataSoapResponse EndGetSharingMetadata(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetSharingMetadataSoapResponse, GetSharingMetadataResponseMessage>(result, (GetSharingMetadataResponseMessage body) => new GetSharingMetadataSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054B8 RID: 21688 RVA: 0x0010EEC0 File Offset: 0x0010D0C0
		public IAsyncResult BeginRefreshSharingFolder(RefreshSharingFolderSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<RefreshSharingFolderResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x060054B9 RID: 21689 RVA: 0x0010EEEB File Offset: 0x0010D0EB
		public RefreshSharingFolderSoapResponse EndRefreshSharingFolder(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<RefreshSharingFolderSoapResponse, RefreshSharingFolderResponseMessage>(result, (RefreshSharingFolderResponseMessage body) => new RefreshSharingFolderSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054BA RID: 21690 RVA: 0x0010EF10 File Offset: 0x0010D110
		public IAsyncResult BeginGetSharingFolder(GetSharingFolderSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetSharingFolderResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x060054BB RID: 21691 RVA: 0x0010EF3B File Offset: 0x0010D13B
		public GetSharingFolderSoapResponse EndGetSharingFolder(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetSharingFolderSoapResponse, GetSharingFolderResponseMessage>(result, (GetSharingFolderResponseMessage body) => new GetSharingFolderSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054BC RID: 21692 RVA: 0x0010EF60 File Offset: 0x0010D160
		public IAsyncResult BeginSetTeamMailbox(SetTeamMailboxSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<SetTeamMailboxResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x060054BD RID: 21693 RVA: 0x0010EF8B File Offset: 0x0010D18B
		public SetTeamMailboxSoapResponse EndSetTeamMailbox(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<SetTeamMailboxSoapResponse, SetTeamMailboxResponseMessage>(result, (SetTeamMailboxResponseMessage body) => new SetTeamMailboxSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054BE RID: 21694 RVA: 0x0010EFB0 File Offset: 0x0010D1B0
		public IAsyncResult BeginUnpinTeamMailbox(UnpinTeamMailboxSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<UnpinTeamMailboxResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x060054BF RID: 21695 RVA: 0x0010EFDB File Offset: 0x0010D1DB
		public UnpinTeamMailboxSoapResponse EndUnpinTeamMailbox(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<UnpinTeamMailboxSoapResponse, UnpinTeamMailboxResponseMessage>(result, (UnpinTeamMailboxResponseMessage body) => new UnpinTeamMailboxSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054C0 RID: 21696 RVA: 0x0010F000 File Offset: 0x0010D200
		public IAsyncResult BeginGetRoomLists(GetRoomListsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetRoomListsResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060054C1 RID: 21697 RVA: 0x0010F02B File Offset: 0x0010D22B
		public GetRoomListsSoapResponse EndGetRoomLists(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetRoomListsSoapResponse, GetRoomListsResponse>(result, (GetRoomListsResponse body) => new GetRoomListsSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054C2 RID: 21698 RVA: 0x0010F050 File Offset: 0x0010D250
		public IAsyncResult BeginGetRooms(GetRoomsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetRoomsResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060054C3 RID: 21699 RVA: 0x0010F07B File Offset: 0x0010D27B
		public GetRoomsSoapResponse EndGetRooms(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetRoomsSoapResponse, GetRoomsResponse>(result, (GetRoomsResponse body) => new GetRoomsSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054C4 RID: 21700 RVA: 0x0010F0A0 File Offset: 0x0010D2A0
		public IAsyncResult BeginGetReminders(GetRemindersSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetRemindersResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060054C5 RID: 21701 RVA: 0x0010F0CB File Offset: 0x0010D2CB
		public GetRemindersSoapResponse EndGetReminders(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetRemindersSoapResponse, GetRemindersResponse>(result, (GetRemindersResponse body) => new GetRemindersSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054C6 RID: 21702 RVA: 0x0010F0F0 File Offset: 0x0010D2F0
		public IAsyncResult BeginPerformReminderAction(PerformReminderActionSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<PerformReminderActionResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060054C7 RID: 21703 RVA: 0x0010F11B File Offset: 0x0010D31B
		public PerformReminderActionSoapResponse EndPerformReminderAction(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<PerformReminderActionSoapResponse, PerformReminderActionResponse>(result, (PerformReminderActionResponse body) => new PerformReminderActionSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054C8 RID: 21704 RVA: 0x0010F140 File Offset: 0x0010D340
		public IAsyncResult BeginFindMessageTrackingReport(FindMessageTrackingReportSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<FindMessageTrackingReportResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x060054C9 RID: 21705 RVA: 0x0010F16B File Offset: 0x0010D36B
		public FindMessageTrackingReportSoapResponse EndFindMessageTrackingReport(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<FindMessageTrackingReportSoapResponse, FindMessageTrackingReportResponseMessage>(result, (FindMessageTrackingReportResponseMessage body) => new FindMessageTrackingReportSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054CA RID: 21706 RVA: 0x0010F190 File Offset: 0x0010D390
		public IAsyncResult BeginGetMessageTrackingReport(GetMessageTrackingReportSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetMessageTrackingReportResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x060054CB RID: 21707 RVA: 0x0010F1BB File Offset: 0x0010D3BB
		public GetMessageTrackingReportSoapResponse EndGetMessageTrackingReport(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetMessageTrackingReportSoapResponse, GetMessageTrackingReportResponseMessage>(result, (GetMessageTrackingReportResponseMessage body) => new GetMessageTrackingReportSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054CC RID: 21708 RVA: 0x0010F1E0 File Offset: 0x0010D3E0
		public IAsyncResult BeginFindConversation(FindConversationSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<FindConversationResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x060054CD RID: 21709 RVA: 0x0010F20B File Offset: 0x0010D40B
		public FindConversationSoapResponse EndFindConversation(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<FindConversationSoapResponse, FindConversationResponseMessage>(result, (FindConversationResponseMessage body) => new FindConversationSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054CE RID: 21710 RVA: 0x0010F230 File Offset: 0x0010D430
		public IAsyncResult BeginFindPeople(FindPeopleSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<FindPeopleResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x060054CF RID: 21711 RVA: 0x0010F25B File Offset: 0x0010D45B
		public FindPeopleSoapResponse EndFindPeople(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<FindPeopleSoapResponse, FindPeopleResponseMessage>(result, (FindPeopleResponseMessage body) => new FindPeopleSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054D0 RID: 21712 RVA: 0x0010F280 File Offset: 0x0010D480
		public IAsyncResult BeginGetPersona(GetPersonaSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetPersonaResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x060054D1 RID: 21713 RVA: 0x0010F2AB File Offset: 0x0010D4AB
		public GetPersonaSoapResponse EndGetPersona(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetPersonaSoapResponse, GetPersonaResponseMessage>(result, (GetPersonaResponseMessage body) => new GetPersonaSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054D2 RID: 21714 RVA: 0x0010F2D0 File Offset: 0x0010D4D0
		public IAsyncResult BeginApplyConversationAction(ApplyConversationActionSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<ApplyConversationActionResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060054D3 RID: 21715 RVA: 0x0010F2FB File Offset: 0x0010D4FB
		public ApplyConversationActionSoapResponse EndApplyConversationAction(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<ApplyConversationActionSoapResponse, ApplyConversationActionResponse>(result, (ApplyConversationActionResponse body) => new ApplyConversationActionSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054D4 RID: 21716 RVA: 0x0010F320 File Offset: 0x0010D520
		public IAsyncResult BeginGetInboxRules(GetInboxRulesSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetInboxRulesResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060054D5 RID: 21717 RVA: 0x0010F34B File Offset: 0x0010D54B
		public GetInboxRulesSoapResponse EndGetInboxRules(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetInboxRulesSoapResponse, GetInboxRulesResponse>(result, (GetInboxRulesResponse body) => new GetInboxRulesSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054D6 RID: 21718 RVA: 0x0010F370 File Offset: 0x0010D570
		public IAsyncResult BeginUpdateInboxRules(UpdateInboxRulesSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<UpdateInboxRulesResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060054D7 RID: 21719 RVA: 0x0010F39B File Offset: 0x0010D59B
		public UpdateInboxRulesSoapResponse EndUpdateInboxRules(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<UpdateInboxRulesSoapResponse, UpdateInboxRulesResponse>(result, (UpdateInboxRulesResponse body) => new UpdateInboxRulesSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054D8 RID: 21720 RVA: 0x0010F3C0 File Offset: 0x0010D5C0
		public IAsyncResult BeginMarkAllItemsAsRead(MarkAllItemsAsReadSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<MarkAllItemsAsReadResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060054D9 RID: 21721 RVA: 0x0010F3EB File Offset: 0x0010D5EB
		public MarkAllItemsAsReadSoapResponse EndMarkAllItemsAsRead(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<MarkAllItemsAsReadSoapResponse, MarkAllItemsAsReadResponse>(result, (MarkAllItemsAsReadResponse body) => new MarkAllItemsAsReadSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054DA RID: 21722 RVA: 0x0010F410 File Offset: 0x0010D610
		public IAsyncResult BeginMarkAsJunk(MarkAsJunkSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<MarkAsJunkResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060054DB RID: 21723 RVA: 0x0010F43B File Offset: 0x0010D63B
		public MarkAsJunkSoapResponse EndMarkAsJunk(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<MarkAsJunkSoapResponse, MarkAsJunkResponse>(result, (MarkAsJunkResponse body) => new MarkAsJunkSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054DC RID: 21724 RVA: 0x0010F460 File Offset: 0x0010D660
		public IAsyncResult BeginGetConversationItems(GetConversationItemsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetConversationItemsResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060054DD RID: 21725 RVA: 0x0010F48B File Offset: 0x0010D68B
		public GetConversationItemsSoapResponse EndGetConversationItems(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetConversationItemsSoapResponse, GetConversationItemsResponse>(result, (GetConversationItemsResponse body) => new GetConversationItemsSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054DE RID: 21726 RVA: 0x0010F4B0 File Offset: 0x0010D6B0
		public IAsyncResult BeginExecuteDiagnosticMethod(ExecuteDiagnosticMethodSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<ExecuteDiagnosticMethodResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060054DF RID: 21727 RVA: 0x0010F4C0 File Offset: 0x0010D6C0
		public ExecuteDiagnosticMethodSoapResponse EndExecuteDiagnosticMethod(IAsyncResult result)
		{
			ServiceAsyncResult<ExecuteDiagnosticMethodResponse> serviceAsyncResult = EWSService.GetServiceAsyncResult<ExecuteDiagnosticMethodResponse>(result);
			ExecuteDiagnosticMethodSoapResponse executeDiagnosticMethodSoapResponse = new ExecuteDiagnosticMethodSoapResponse();
			executeDiagnosticMethodSoapResponse.Body = serviceAsyncResult.Data;
			PerformanceMonitor.UpdateTotalCompletedRequestsCount();
			return executeDiagnosticMethodSoapResponse;
		}

		// Token: 0x060054E0 RID: 21728 RVA: 0x0010F4EC File Offset: 0x0010D6EC
		public IAsyncResult BeginFindMailboxStatisticsByKeywords(FindMailboxStatisticsByKeywordsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<FindMailboxStatisticsByKeywordsResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060054E1 RID: 21729 RVA: 0x0010F517 File Offset: 0x0010D717
		public FindMailboxStatisticsByKeywordsSoapResponse EndFindMailboxStatisticsByKeywords(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<FindMailboxStatisticsByKeywordsSoapResponse, FindMailboxStatisticsByKeywordsResponse>(result, (FindMailboxStatisticsByKeywordsResponse body) => new FindMailboxStatisticsByKeywordsSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054E2 RID: 21730 RVA: 0x0010F53C File Offset: 0x0010D73C
		public IAsyncResult BeginGetSearchableMailboxes(GetSearchableMailboxesSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetSearchableMailboxesResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060054E3 RID: 21731 RVA: 0x0010F567 File Offset: 0x0010D767
		public GetSearchableMailboxesSoapResponse EndGetSearchableMailboxes(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetSearchableMailboxesSoapResponse, GetSearchableMailboxesResponse>(result, (GetSearchableMailboxesResponse body) => new GetSearchableMailboxesSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054E4 RID: 21732 RVA: 0x0010F58C File Offset: 0x0010D78C
		public IAsyncResult BeginSearchMailboxes(SearchMailboxesSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<SearchMailboxesResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060054E5 RID: 21733 RVA: 0x0010F5B7 File Offset: 0x0010D7B7
		public SearchMailboxesSoapResponse EndSearchMailboxes(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<SearchMailboxesSoapResponse, SearchMailboxesResponse>(result, (SearchMailboxesResponse body) => new SearchMailboxesSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054E6 RID: 21734 RVA: 0x0010F5DC File Offset: 0x0010D7DC
		public IAsyncResult BeginGetDiscoverySearchConfiguration(GetDiscoverySearchConfigurationSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetDiscoverySearchConfigurationResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060054E7 RID: 21735 RVA: 0x0010F607 File Offset: 0x0010D807
		public GetDiscoverySearchConfigurationSoapResponse EndGetDiscoverySearchConfiguration(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetDiscoverySearchConfigurationSoapResponse, GetDiscoverySearchConfigurationResponse>(result, (GetDiscoverySearchConfigurationResponse body) => new GetDiscoverySearchConfigurationSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054E8 RID: 21736 RVA: 0x0010F62C File Offset: 0x0010D82C
		public IAsyncResult BeginGetHoldOnMailboxes(GetHoldOnMailboxesSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetHoldOnMailboxesResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060054E9 RID: 21737 RVA: 0x0010F657 File Offset: 0x0010D857
		public GetHoldOnMailboxesSoapResponse EndGetHoldOnMailboxes(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetHoldOnMailboxesSoapResponse, GetHoldOnMailboxesResponse>(result, (GetHoldOnMailboxesResponse body) => new GetHoldOnMailboxesSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054EA RID: 21738 RVA: 0x0010F67C File Offset: 0x0010D87C
		public IAsyncResult BeginSetHoldOnMailboxes(SetHoldOnMailboxesSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<SetHoldOnMailboxesResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060054EB RID: 21739 RVA: 0x0010F6A7 File Offset: 0x0010D8A7
		public SetHoldOnMailboxesSoapResponse EndSetHoldOnMailboxes(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<SetHoldOnMailboxesSoapResponse, SetHoldOnMailboxesResponse>(result, (SetHoldOnMailboxesResponse body) => new SetHoldOnMailboxesSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054EC RID: 21740 RVA: 0x0010F6CC File Offset: 0x0010D8CC
		public IAsyncResult BeginGetNonIndexableItemStatistics(GetNonIndexableItemStatisticsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetNonIndexableItemStatisticsResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060054ED RID: 21741 RVA: 0x0010F6F7 File Offset: 0x0010D8F7
		public GetNonIndexableItemStatisticsSoapResponse EndGetNonIndexableItemStatistics(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetNonIndexableItemStatisticsSoapResponse, GetNonIndexableItemStatisticsResponse>(result, (GetNonIndexableItemStatisticsResponse body) => new GetNonIndexableItemStatisticsSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054EE RID: 21742 RVA: 0x0010F71C File Offset: 0x0010D91C
		public IAsyncResult BeginGetNonIndexableItemDetails(GetNonIndexableItemDetailsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetNonIndexableItemDetailsResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060054EF RID: 21743 RVA: 0x0010F747 File Offset: 0x0010D947
		public GetNonIndexableItemDetailsSoapResponse EndGetNonIndexableItemDetails(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetNonIndexableItemDetailsSoapResponse, GetNonIndexableItemDetailsResponse>(result, (GetNonIndexableItemDetailsResponse body) => new GetNonIndexableItemDetailsSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054F0 RID: 21744 RVA: 0x0010F76C File Offset: 0x0010D96C
		public IAsyncResult BeginGetPasswordExpirationDate(GetPasswordExpirationDateSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetPasswordExpirationDateResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060054F1 RID: 21745 RVA: 0x0010F797 File Offset: 0x0010D997
		public GetPasswordExpirationDateSoapResponse EndGetPasswordExpirationDate(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetPasswordExpirationDateSoapResponse, GetPasswordExpirationDateResponse>(result, (GetPasswordExpirationDateResponse body) => new GetPasswordExpirationDateSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054F2 RID: 21746 RVA: 0x0010F7BC File Offset: 0x0010D9BC
		public IAsyncResult BeginAddDistributionGroupToImList(AddDistributionGroupToImListSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<AddDistributionGroupToImListResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x060054F3 RID: 21747 RVA: 0x0010F7E7 File Offset: 0x0010D9E7
		public AddDistributionGroupToImListSoapResponse EndAddDistributionGroupToImList(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<AddDistributionGroupToImListSoapResponse, AddDistributionGroupToImListResponseMessage>(result, (AddDistributionGroupToImListResponseMessage body) => new AddDistributionGroupToImListSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054F4 RID: 21748 RVA: 0x0010F80C File Offset: 0x0010DA0C
		public IAsyncResult BeginAddImContactToGroup(AddImContactToGroupSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<AddImContactToGroupResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x060054F5 RID: 21749 RVA: 0x0010F837 File Offset: 0x0010DA37
		public AddImContactToGroupSoapResponse EndAddImContactToGroup(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<AddImContactToGroupSoapResponse, AddImContactToGroupResponseMessage>(result, (AddImContactToGroupResponseMessage body) => new AddImContactToGroupSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054F6 RID: 21750 RVA: 0x0010F85C File Offset: 0x0010DA5C
		public IAsyncResult BeginRemoveImContactFromGroup(RemoveImContactFromGroupSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<RemoveImContactFromGroupResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x060054F7 RID: 21751 RVA: 0x0010F887 File Offset: 0x0010DA87
		public RemoveImContactFromGroupSoapResponse EndRemoveImContactFromGroup(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<RemoveImContactFromGroupSoapResponse, RemoveImContactFromGroupResponseMessage>(result, (RemoveImContactFromGroupResponseMessage body) => new RemoveImContactFromGroupSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054F8 RID: 21752 RVA: 0x0010F8AC File Offset: 0x0010DAAC
		public IAsyncResult BeginAddImGroup(AddImGroupSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<AddImGroupResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x060054F9 RID: 21753 RVA: 0x0010F8D7 File Offset: 0x0010DAD7
		public AddImGroupSoapResponse EndAddImGroup(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<AddImGroupSoapResponse, AddImGroupResponseMessage>(result, (AddImGroupResponseMessage body) => new AddImGroupSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054FA RID: 21754 RVA: 0x0010F8FC File Offset: 0x0010DAFC
		public IAsyncResult BeginAddNewImContactToGroup(AddNewImContactToGroupSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<AddNewImContactToGroupResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x060054FB RID: 21755 RVA: 0x0010F927 File Offset: 0x0010DB27
		public AddNewImContactToGroupSoapResponse EndAddNewImContactToGroup(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<AddNewImContactToGroupSoapResponse, AddNewImContactToGroupResponseMessage>(result, (AddNewImContactToGroupResponseMessage body) => new AddNewImContactToGroupSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054FC RID: 21756 RVA: 0x0010F94C File Offset: 0x0010DB4C
		public IAsyncResult BeginAddNewTelUriContactToGroup(AddNewTelUriContactToGroupSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<AddNewTelUriContactToGroupResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x060054FD RID: 21757 RVA: 0x0010F977 File Offset: 0x0010DB77
		public AddNewTelUriContactToGroupSoapResponse EndAddNewTelUriContactToGroup(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<AddNewTelUriContactToGroupSoapResponse, AddNewTelUriContactToGroupResponseMessage>(result, (AddNewTelUriContactToGroupResponseMessage body) => new AddNewTelUriContactToGroupSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x060054FE RID: 21758 RVA: 0x0010F99C File Offset: 0x0010DB9C
		public IAsyncResult BeginGetImItemList(GetImItemListSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetImItemListResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x060054FF RID: 21759 RVA: 0x0010F9C7 File Offset: 0x0010DBC7
		public GetImItemListSoapResponse EndGetImItemList(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetImItemListSoapResponse, GetImItemListResponseMessage>(result, (GetImItemListResponseMessage body) => new GetImItemListSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005500 RID: 21760 RVA: 0x0010F9EC File Offset: 0x0010DBEC
		public IAsyncResult BeginGetImItems(GetImItemsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetImItemsResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06005501 RID: 21761 RVA: 0x0010FA17 File Offset: 0x0010DC17
		public GetImItemsSoapResponse EndGetImItems(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetImItemsSoapResponse, GetImItemsResponseMessage>(result, (GetImItemsResponseMessage body) => new GetImItemsSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005502 RID: 21762 RVA: 0x0010FA3C File Offset: 0x0010DC3C
		public IAsyncResult BeginRemoveContactFromImList(RemoveContactFromImListSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<RemoveContactFromImListResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06005503 RID: 21763 RVA: 0x0010FA67 File Offset: 0x0010DC67
		public RemoveContactFromImListSoapResponse EndRemoveContactFromImList(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<RemoveContactFromImListSoapResponse, RemoveContactFromImListResponseMessage>(result, (RemoveContactFromImListResponseMessage body) => new RemoveContactFromImListSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005504 RID: 21764 RVA: 0x0010FA8C File Offset: 0x0010DC8C
		public IAsyncResult BeginRemoveDistributionGroupFromImList(RemoveDistributionGroupFromImListSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<RemoveDistributionGroupFromImListResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06005505 RID: 21765 RVA: 0x0010FAB7 File Offset: 0x0010DCB7
		public RemoveDistributionGroupFromImListSoapResponse EndRemoveDistributionGroupFromImList(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<RemoveDistributionGroupFromImListSoapResponse, RemoveDistributionGroupFromImListResponseMessage>(result, (RemoveDistributionGroupFromImListResponseMessage body) => new RemoveDistributionGroupFromImListSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005506 RID: 21766 RVA: 0x0010FADC File Offset: 0x0010DCDC
		public IAsyncResult BeginRemoveImGroup(RemoveImGroupSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<RemoveImGroupResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06005507 RID: 21767 RVA: 0x0010FB07 File Offset: 0x0010DD07
		public RemoveImGroupSoapResponse EndRemoveImGroup(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<RemoveImGroupSoapResponse, RemoveImGroupResponseMessage>(result, (RemoveImGroupResponseMessage body) => new RemoveImGroupSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005508 RID: 21768 RVA: 0x0010FB2C File Offset: 0x0010DD2C
		public IAsyncResult BeginSetImGroup(SetImGroupSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<SetImGroupResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06005509 RID: 21769 RVA: 0x0010FB57 File Offset: 0x0010DD57
		public SetImGroupSoapResponse EndSetImGroup(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<SetImGroupSoapResponse, SetImGroupResponseMessage>(result, (SetImGroupResponseMessage body) => new SetImGroupSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600550A RID: 21770 RVA: 0x0010FB7C File Offset: 0x0010DD7C
		public IAsyncResult BeginSetImListMigrationCompleted(SetImListMigrationCompletedSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<SetImListMigrationCompletedResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x0600550B RID: 21771 RVA: 0x0010FBA7 File Offset: 0x0010DDA7
		public SetImListMigrationCompletedSoapResponse EndSetImListMigrationCompleted(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<SetImListMigrationCompletedSoapResponse, SetImListMigrationCompletedResponseMessage>(result, (SetImListMigrationCompletedResponseMessage body) => new SetImListMigrationCompletedSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600550C RID: 21772 RVA: 0x0010FBCC File Offset: 0x0010DDCC
		public IAsyncResult BeginGetUserRetentionPolicyTags(GetUserRetentionPolicyTagsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetUserRetentionPolicyTagsResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600550D RID: 21773 RVA: 0x0010FBF7 File Offset: 0x0010DDF7
		public GetUserRetentionPolicyTagsSoapResponse EndGetUserRetentionPolicyTags(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetUserRetentionPolicyTagsSoapResponse, GetUserRetentionPolicyTagsResponse>(result, (GetUserRetentionPolicyTagsResponse body) => new GetUserRetentionPolicyTagsSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600550E RID: 21774 RVA: 0x0010FC1C File Offset: 0x0010DE1C
		public IAsyncResult BeginStartFindInGALSpeechRecognition(StartFindInGALSpeechRecognitionSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<StartFindInGALSpeechRecognitionResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x0600550F RID: 21775 RVA: 0x0010FC47 File Offset: 0x0010DE47
		public StartFindInGALSpeechRecognitionSoapResponse EndStartFindInGALSpeechRecognition(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<StartFindInGALSpeechRecognitionSoapResponse, StartFindInGALSpeechRecognitionResponseMessage>(result, (StartFindInGALSpeechRecognitionResponseMessage body) => new StartFindInGALSpeechRecognitionSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005510 RID: 21776 RVA: 0x0010FC6C File Offset: 0x0010DE6C
		public IAsyncResult BeginCompleteFindInGALSpeechRecognition(CompleteFindInGALSpeechRecognitionSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<CompleteFindInGALSpeechRecognitionResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06005511 RID: 21777 RVA: 0x0010FC97 File Offset: 0x0010DE97
		public CompleteFindInGALSpeechRecognitionSoapResponse EndCompleteFindInGALSpeechRecognition(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<CompleteFindInGALSpeechRecognitionSoapResponse, CompleteFindInGALSpeechRecognitionResponseMessage>(result, (CompleteFindInGALSpeechRecognitionResponseMessage body) => new CompleteFindInGALSpeechRecognitionSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005512 RID: 21778 RVA: 0x0010FCBC File Offset: 0x0010DEBC
		public IAsyncResult BeginGetUserPhotoData(GetUserPhotoSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetUserPhotoResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06005513 RID: 21779 RVA: 0x0010FCE7 File Offset: 0x0010DEE7
		public GetUserPhotoSoapResponse EndGetUserPhotoData(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetUserPhotoSoapResponse, GetUserPhotoResponseMessage>(result, (GetUserPhotoResponseMessage body) => new GetUserPhotoSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005514 RID: 21780 RVA: 0x0010FD0C File Offset: 0x0010DF0C
		public IAsyncResult BeginGetClientIntent(GetClientIntentSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetClientIntentResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06005515 RID: 21781 RVA: 0x0010FD37 File Offset: 0x0010DF37
		public GetClientIntentSoapResponse EndGetClientIntent(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetClientIntentSoapResponse, GetClientIntentResponseMessage>(result, (GetClientIntentResponseMessage body) => new GetClientIntentSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005516 RID: 21782 RVA: 0x0010FD5C File Offset: 0x0010DF5C
		public IAsyncResult BeginPerformInstantSearch(PerformInstantSearchSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<PerformInstantSearchResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005517 RID: 21783 RVA: 0x0010FD87 File Offset: 0x0010DF87
		public PerformInstantSearchSoapResponse EndPerformInstantSearch(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<PerformInstantSearchSoapResponse, PerformInstantSearchResponse>(result, (PerformInstantSearchResponse body) => new PerformInstantSearchSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005518 RID: 21784 RVA: 0x0010FDAC File Offset: 0x0010DFAC
		public IAsyncResult BeginEndInstantSearchSession(EndInstantSearchSessionSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<EndInstantSearchSessionResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005519 RID: 21785 RVA: 0x0010FDD7 File Offset: 0x0010DFD7
		public EndInstantSearchSessionSoapResponse EndEndInstantSearchSession(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<EndInstantSearchSessionSoapResponse, EndInstantSearchSessionResponse>(result, (EndInstantSearchSessionResponse body) => new EndInstantSearchSessionSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600551A RID: 21786 RVA: 0x0010FDFC File Offset: 0x0010DFFC
		public IAsyncResult BeginGetUserUnifiedGroups(GetUserUnifiedGroupsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetUserUnifiedGroupsResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x0600551B RID: 21787 RVA: 0x0010FE27 File Offset: 0x0010E027
		public GetUserUnifiedGroupsSoapResponse EndGetUserUnifiedGroups(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetUserUnifiedGroupsSoapResponse, GetUserUnifiedGroupsResponseMessage>(result, (GetUserUnifiedGroupsResponseMessage body) => new GetUserUnifiedGroupsSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600551C RID: 21788 RVA: 0x0010FE4C File Offset: 0x0010E04C
		public IAsyncResult BeginGetClutterState(GetClutterStateSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetClutterStateResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600551D RID: 21789 RVA: 0x0010FE77 File Offset: 0x0010E077
		public GetClutterStateSoapResponse EndGetClutterState(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetClutterStateSoapResponse, GetClutterStateResponse>(result, (GetClutterStateResponse body) => new GetClutterStateSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600551E RID: 21790 RVA: 0x0010FE9C File Offset: 0x0010E09C
		public IAsyncResult BeginSetClutterState(SetClutterStateSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<SetClutterStateResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600551F RID: 21791 RVA: 0x0010FEC7 File Offset: 0x0010E0C7
		public SetClutterStateSoapResponse EndSetClutterState(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<SetClutterStateSoapResponse, SetClutterStateResponse>(result, (SetClutterStateResponse body) => new SetClutterStateSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005520 RID: 21792 RVA: 0x0010FEEC File Offset: 0x0010E0EC
		public IAsyncResult BeginGetUserPhoto(string email, UserPhotoSize size, AsyncCallback callback, object state)
		{
			return new GetUserPhotoRequest(CallContext.Current.CreateWebResponseContext(), email, size, false, false).ValidateAndSubmit<GetUserPhotoResponse>(callback, state);
		}

		// Token: 0x06005521 RID: 21793 RVA: 0x0010FF0C File Offset: 0x0010E10C
		public Stream EndGetUserPhoto(IAsyncResult result)
		{
			ServiceAsyncResult<GetUserPhotoResponse> serviceAsyncResult = (ServiceAsyncResult<GetUserPhotoResponse>)result;
			if (serviceAsyncResult.Data != null && serviceAsyncResult.Data.ResponseMessages.Items != null && serviceAsyncResult.Data.ResponseMessages.Items.Length > 0)
			{
				return ((GetUserPhotoResponseMessage)serviceAsyncResult.Data.ResponseMessages.Items[0]).UserPhotoStream;
			}
			WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
			return new MemoryStream();
		}

		// Token: 0x06005522 RID: 21794 RVA: 0x0010FF85 File Offset: 0x0010E185
		public IAsyncResult BeginGetPeopleICommunicateWith(AsyncCallback callback, object state)
		{
			return new GetPeopleICommunicateWithRequest(CallContext.Current.CreateWebResponseContext()).ValidateAndSubmit<GetPeopleICommunicateWithResponse>(callback, state);
		}

		// Token: 0x06005523 RID: 21795 RVA: 0x0010FFA0 File Offset: 0x0010E1A0
		public Stream EndGetPeopleICommunicateWith(IAsyncResult result)
		{
			ServiceAsyncResult<GetPeopleICommunicateWithResponse> serviceAsyncResult = (ServiceAsyncResult<GetPeopleICommunicateWithResponse>)result;
			if (serviceAsyncResult.Data != null && serviceAsyncResult.Data.ResponseMessages.Items != null && serviceAsyncResult.Data.ResponseMessages.Items.Length > 0)
			{
				return ((GetPeopleICommunicateWithResponseMessage)serviceAsyncResult.Data.ResponseMessages.Items[0]).Stream;
			}
			ExTraceGlobals.PeopleICommunicateWithTracer.TraceError<string>(0L, "Error in EndGetPeopleICommunicateWith - no results.", CallContext.Current.Description);
			WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
			return new MemoryStream();
		}

		// Token: 0x06005524 RID: 21796 RVA: 0x00110034 File Offset: 0x0010E234
		public IAsyncResult BeginCreateUMCallDataRecord(CreateUMCallDataRecordSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<CreateUMCallDataRecordResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06005525 RID: 21797 RVA: 0x0011005F File Offset: 0x0010E25F
		public CreateUMCallDataRecordSoapResponse EndCreateUMCallDataRecord(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<CreateUMCallDataRecordSoapResponse, CreateUMCallDataRecordResponseMessage>(result, (CreateUMCallDataRecordResponseMessage body) => new CreateUMCallDataRecordSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005526 RID: 21798 RVA: 0x00110084 File Offset: 0x0010E284
		public IAsyncResult BeginGetUMCallDataRecords(GetUMCallDataRecordsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetUMCallDataRecordsResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06005527 RID: 21799 RVA: 0x001100AF File Offset: 0x0010E2AF
		public GetUMCallDataRecordsSoapResponse EndGetUMCallDataRecords(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetUMCallDataRecordsSoapResponse, GetUMCallDataRecordsResponseMessage>(result, (GetUMCallDataRecordsResponseMessage body) => new GetUMCallDataRecordsSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005528 RID: 21800 RVA: 0x001100D4 File Offset: 0x0010E2D4
		public IAsyncResult BeginGetUMCallSummary(GetUMCallSummarySoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetUMCallSummaryResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06005529 RID: 21801 RVA: 0x001100FF File Offset: 0x0010E2FF
		public GetUMCallSummarySoapResponse EndGetUMCallSummary(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetUMCallSummarySoapResponse, GetUMCallSummaryResponseMessage>(result, (GetUMCallSummaryResponseMessage body) => new GetUMCallSummarySoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600552A RID: 21802 RVA: 0x00110124 File Offset: 0x0010E324
		public IAsyncResult BeginInitUMMailbox(InitUMMailboxSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<InitUMMailboxResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x0600552B RID: 21803 RVA: 0x0011014F File Offset: 0x0010E34F
		public InitUMMailboxSoapResponse EndInitUMMailbox(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<InitUMMailboxSoapResponse, InitUMMailboxResponseMessage>(result, (InitUMMailboxResponseMessage body) => new InitUMMailboxSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600552C RID: 21804 RVA: 0x00110174 File Offset: 0x0010E374
		public IAsyncResult BeginResetUMMailbox(ResetUMMailboxSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<ResetUMMailboxResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x0600552D RID: 21805 RVA: 0x0011019F File Offset: 0x0010E39F
		public ResetUMMailboxSoapResponse EndResetUMMailbox(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<ResetUMMailboxSoapResponse, ResetUMMailboxResponseMessage>(result, (ResetUMMailboxResponseMessage body) => new ResetUMMailboxSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600552E RID: 21806 RVA: 0x001101C4 File Offset: 0x0010E3C4
		public IAsyncResult BeginValidateUMPin(ValidateUMPinSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<ValidateUMPinResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x0600552F RID: 21807 RVA: 0x001101EF File Offset: 0x0010E3EF
		public ValidateUMPinSoapResponse EndValidateUMPin(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<ValidateUMPinSoapResponse, ValidateUMPinResponseMessage>(result, (ValidateUMPinResponseMessage body) => new ValidateUMPinSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005530 RID: 21808 RVA: 0x00110214 File Offset: 0x0010E414
		public IAsyncResult BeginSaveUMPin(SaveUMPinSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<SaveUMPinResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06005531 RID: 21809 RVA: 0x0011023F File Offset: 0x0010E43F
		public SaveUMPinSoapResponse EndSaveUMPin(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<SaveUMPinSoapResponse, SaveUMPinResponseMessage>(result, (SaveUMPinResponseMessage body) => new SaveUMPinSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005532 RID: 21810 RVA: 0x00110264 File Offset: 0x0010E464
		public IAsyncResult BeginGetUMPin(GetUMPinSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetUMPinResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06005533 RID: 21811 RVA: 0x0011028F File Offset: 0x0010E48F
		public GetUMPinSoapResponse EndGetUMPin(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetUMPinSoapResponse, GetUMPinResponseMessage>(result, (GetUMPinResponseMessage body) => new GetUMPinSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005534 RID: 21812 RVA: 0x001102B4 File Offset: 0x0010E4B4
		public IAsyncResult BeginGetUMSubscriberCallAnsweringData(GetUMSubscriberCallAnsweringDataSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<GetUMSubscriberCallAnsweringDataResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06005535 RID: 21813 RVA: 0x001102DF File Offset: 0x0010E4DF
		public GetUMSubscriberCallAnsweringDataSoapResponse EndGetUMSubscriberCallAnsweringData(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<GetUMSubscriberCallAnsweringDataSoapResponse, GetUMSubscriberCallAnsweringDataResponseMessage>(result, (GetUMSubscriberCallAnsweringDataResponseMessage body) => new GetUMSubscriberCallAnsweringDataSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005536 RID: 21814 RVA: 0x00110304 File Offset: 0x0010E504
		public IAsyncResult BeginUpdateMailboxAssociation(UpdateMailboxAssociationSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<UpdateMailboxAssociationResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005537 RID: 21815 RVA: 0x0011032F File Offset: 0x0010E52F
		public UpdateMailboxAssociationSoapResponse EndUpdateMailboxAssociation(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<UpdateMailboxAssociationSoapResponse, UpdateMailboxAssociationResponse>(result, (UpdateMailboxAssociationResponse body) => new UpdateMailboxAssociationSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x06005538 RID: 21816 RVA: 0x00110354 File Offset: 0x0010E554
		public IAsyncResult BeginUpdateGroupMailbox(UpdateGroupMailboxSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.ValidateAndSubmit<UpdateGroupMailboxResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06005539 RID: 21817 RVA: 0x0011037F File Offset: 0x0010E57F
		public UpdateGroupMailboxSoapResponse EndUpdateGroupMailbox(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<UpdateGroupMailboxSoapResponse, UpdateGroupMailboxResponse>(result, (UpdateGroupMailboxResponse body) => new UpdateGroupMailboxSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600553A RID: 21818 RVA: 0x001103A4 File Offset: 0x0010E5A4
		public IAsyncResult BeginPostModernGroupItem(PostModernGroupItemSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return soapRequest.Body.Submit<PostModernGroupItemResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600553B RID: 21819 RVA: 0x001103CF File Offset: 0x0010E5CF
		public PostModernGroupItemSoapResponse EndPostModernGroupItem(IAsyncResult result)
		{
			return EWSService.CreateSoapResponse<PostModernGroupItemSoapResponse, PostModernGroupItemResponse>(result, (PostModernGroupItemResponse body) => new PostModernGroupItemSoapResponse
			{
				Body = body
			});
		}

		// Token: 0x0600553C RID: 21820 RVA: 0x001103F4 File Offset: 0x0010E5F4
		private static TSoapResponse CreateSoapResponse<TSoapResponse, TSoapResponseBody>(IAsyncResult result, Func<TSoapResponseBody, TSoapResponse> createSoapResponseCallback)
		{
			bool flag = false;
			if (CallContext.Current.AccessingPrincipal != null && ExUserTracingAdaptor.Instance.IsTracingEnabledUser(CallContext.Current.AccessingPrincipal.LegacyDn))
			{
				flag = true;
				BaseTrace.CurrentThreadSettings.EnableTracing();
			}
			TSoapResponse result2;
			try
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>(0L, "Entering End web method for {0}", CallContext.Current.Description);
				ServiceAsyncResult<TSoapResponseBody> serviceAsyncResult = EWSService.GetServiceAsyncResult<TSoapResponseBody>(result);
				TSoapResponse tsoapResponse = createSoapResponseCallback(serviceAsyncResult.Data);
				PerformanceMonitor.UpdateTotalCompletedRequestsCount();
				result2 = tsoapResponse;
			}
			finally
			{
				if (flag)
				{
					BaseTrace.CurrentThreadSettings.DisableTracing();
				}
			}
			return result2;
		}

		// Token: 0x0600553D RID: 21821 RVA: 0x00110490 File Offset: 0x0010E690
		private static ServiceAsyncResult<TSoapResponseBody> GetServiceAsyncResult<TSoapResponseBody>(IAsyncResult result)
		{
			ServiceAsyncResult<TSoapResponseBody> serviceAsyncResult = (ServiceAsyncResult<TSoapResponseBody>)result;
			Exception ex = serviceAsyncResult.CompletionState as Exception;
			if (ex == null)
			{
				return serviceAsyncResult;
			}
			ExTraceGlobals.CommonAlgorithmTracer.TraceError<Exception>(0L, "Request failed with: {0}", ex);
			Exception ex2 = ex;
			if (ex is GrayException)
			{
				ex2 = ex.InnerException;
			}
			LocalizedException ex3 = ex2 as LocalizedException;
			if (ex3 != null)
			{
				throw FaultExceptionUtilities.CreateFault(ex3, FaultParty.Receiver);
			}
			if (EWSService.IsServiceHandledException(ex2))
			{
				throw ex2;
			}
			throw new InternalServerErrorException(ex2);
		}

		// Token: 0x0600553E RID: 21822 RVA: 0x001104FA File Offset: 0x0010E6FA
		private static bool IsServiceHandledException(Exception exception)
		{
			return exception is BailOutException || exception is FaultException;
		}
	}
}
