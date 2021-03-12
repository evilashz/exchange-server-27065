using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020008FC RID: 2300
	[ServiceContract]
	public interface IJsonServiceContract
	{
		// Token: 0x06003FB8 RID: 16312
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginConvertId(ConvertIdJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FB9 RID: 16313
		ConvertIdJsonResponse EndConvertId(IAsyncResult result);

		// Token: 0x06003FBA RID: 16314
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginUploadItems(UploadItemsJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FBB RID: 16315
		UploadItemsJsonResponse EndUploadItems(IAsyncResult result);

		// Token: 0x06003FBC RID: 16316
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginExportItems(ExportItemsJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FBD RID: 16317
		ExportItemsJsonResponse EndExportItems(IAsyncResult result);

		// Token: 0x06003FBE RID: 16318
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		IAsyncResult BeginGetFolder(GetFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FBF RID: 16319
		GetFolderJsonResponse EndGetFolder(IAsyncResult result);

		// Token: 0x06003FC0 RID: 16320
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginCreateFolder(CreateFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FC1 RID: 16321
		CreateFolderJsonResponse EndCreateFolder(IAsyncResult result);

		// Token: 0x06003FC2 RID: 16322
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginDeleteFolder(DeleteFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FC3 RID: 16323
		DeleteFolderJsonResponse EndDeleteFolder(IAsyncResult result);

		// Token: 0x06003FC4 RID: 16324
		[OperationContract(AsyncPattern = true)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginEmptyFolder(EmptyFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FC5 RID: 16325
		EmptyFolderJsonResponse EndEmptyFolder(IAsyncResult result);

		// Token: 0x06003FC6 RID: 16326
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		IAsyncResult BeginUpdateFolder(UpdateFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FC7 RID: 16327
		UpdateFolderJsonResponse EndUpdateFolder(IAsyncResult result);

		// Token: 0x06003FC8 RID: 16328
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginMoveFolder(MoveFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FC9 RID: 16329
		MoveFolderJsonResponse EndMoveFolder(IAsyncResult result);

		// Token: 0x06003FCA RID: 16330
		[OperationContract(AsyncPattern = true)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginCopyFolder(CopyFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FCB RID: 16331
		CopyFolderJsonResponse EndCopyFolder(IAsyncResult result);

		// Token: 0x06003FCC RID: 16332
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginFindItem(FindItemJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FCD RID: 16333
		FindItemJsonResponse EndFindItem(IAsyncResult result);

		// Token: 0x06003FCE RID: 16334
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginFindFolder(FindFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FCF RID: 16335
		FindFolderJsonResponse EndFindFolder(IAsyncResult result);

		// Token: 0x06003FD0 RID: 16336
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		[OfflineClient(Queued = false)]
		IAsyncResult BeginGetItem(GetItemJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FD1 RID: 16337
		GetItemJsonResponse EndGetItem(IAsyncResult result);

		// Token: 0x06003FD2 RID: 16338
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		IAsyncResult BeginPostModernGroupItem(PostModernGroupItemJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FD3 RID: 16339
		PostModernGroupItemJsonResponse EndPostModernGroupItem(IAsyncResult result);

		// Token: 0x06003FD4 RID: 16340
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		IAsyncResult BeginUpdateAndPostModernGroupItem(UpdateAndPostModernGroupItemJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FD5 RID: 16341
		UpdateAndPostModernGroupItemJsonResponse EndUpdateAndPostModernGroupItem(IAsyncResult result);

		// Token: 0x06003FD6 RID: 16342
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		IAsyncResult BeginCreateResponseFromModernGroup(CreateResponseFromModernGroupJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FD7 RID: 16343
		CreateResponseFromModernGroupJsonResponse EndCreateResponseFromModernGroup(IAsyncResult result);

		// Token: 0x06003FD8 RID: 16344
		[OperationContract(AsyncPattern = true)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginCreateItem(CreateItemJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FD9 RID: 16345
		CreateItemJsonResponse EndCreateItem(IAsyncResult result);

		// Token: 0x06003FDA RID: 16346
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = true)]
		IAsyncResult BeginDeleteItem(DeleteItemJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FDB RID: 16347
		DeleteItemJsonResponse EndDeleteItem(IAsyncResult result);

		// Token: 0x06003FDC RID: 16348
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginUpdateItem(UpdateItemJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FDD RID: 16349
		UpdateItemJsonResponse EndUpdateItem(IAsyncResult result);

		// Token: 0x06003FDE RID: 16350
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginSendItem(SendItemJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FDF RID: 16351
		SendItemJsonResponse EndSendItem(IAsyncResult result);

		// Token: 0x06003FE0 RID: 16352
		[OfflineClient(Queued = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginMoveItem(MoveItemJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FE1 RID: 16353
		MoveItemJsonResponse EndMoveItem(IAsyncResult result);

		// Token: 0x06003FE2 RID: 16354
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		[OfflineClient(Queued = false)]
		IAsyncResult BeginCopyItem(CopyItemJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FE3 RID: 16355
		CopyItemJsonResponse EndCopyItem(IAsyncResult result);

		// Token: 0x06003FE4 RID: 16356
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginCreateAttachment(CreateAttachmentJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FE5 RID: 16357
		CreateAttachmentJsonResponse EndCreateAttachment(IAsyncResult result);

		// Token: 0x06003FE6 RID: 16358
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginDeleteAttachment(DeleteAttachmentJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FE7 RID: 16359
		DeleteAttachmentJsonResponse EndDeleteAttachment(IAsyncResult result);

		// Token: 0x06003FE8 RID: 16360
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		[OfflineClient(Queued = false)]
		IAsyncResult BeginGetAttachment(GetAttachmentJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FE9 RID: 16361
		GetAttachmentJsonResponse EndGetAttachment(IAsyncResult result);

		// Token: 0x06003FEA RID: 16362
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetClientAccessToken(GetClientAccessTokenJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FEB RID: 16363
		GetClientAccessTokenJsonResponse EndGetClientAccessToken(IAsyncResult result);

		// Token: 0x06003FEC RID: 16364
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginResolveNames(ResolveNamesJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FED RID: 16365
		ResolveNamesJsonResponse EndResolveNames(IAsyncResult result);

		// Token: 0x06003FEE RID: 16366
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginExpandDL(ExpandDLJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FEF RID: 16367
		ExpandDLJsonResponse EndExpandDL(IAsyncResult result);

		// Token: 0x06003FF0 RID: 16368
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetServerTimeZones(GetServerTimeZonesJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FF1 RID: 16369
		GetServerTimeZonesJsonResponse EndGetServerTimeZones(IAsyncResult result);

		// Token: 0x06003FF2 RID: 16370
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginCreateManagedFolder(CreateManagedFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FF3 RID: 16371
		CreateManagedFolderJsonResponse EndCreateManagedFolder(IAsyncResult result);

		// Token: 0x06003FF4 RID: 16372
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginSubscribe(SubscribeJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FF5 RID: 16373
		SubscribeJsonResponse EndSubscribe(IAsyncResult result);

		// Token: 0x06003FF6 RID: 16374
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginUnsubscribe(UnsubscribeJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FF7 RID: 16375
		UnsubscribeJsonResponse EndUnsubscribe(IAsyncResult result);

		// Token: 0x06003FF8 RID: 16376
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetEvents(GetEventsJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FF9 RID: 16377
		GetEventsJsonResponse EndGetEvents(IAsyncResult result);

		// Token: 0x06003FFA RID: 16378
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		[OfflineClient(Queued = false)]
		IAsyncResult BeginSyncFolderHierarchy(SyncFolderHierarchyJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FFB RID: 16379
		SyncFolderHierarchyJsonResponse EndSyncFolderHierarchy(IAsyncResult result);

		// Token: 0x06003FFC RID: 16380
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginSyncFolderItems(SyncFolderItemsJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FFD RID: 16381
		SyncFolderItemsJsonResponse EndSyncFolderItems(IAsyncResult result);

		// Token: 0x06003FFE RID: 16382
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetDelegate(GetDelegateJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06003FFF RID: 16383
		GetDelegateJsonResponse EndGetDelegate(IAsyncResult result);

		// Token: 0x06004000 RID: 16384
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginAddDelegate(AddDelegateJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004001 RID: 16385
		AddDelegateJsonResponse EndAddDelegate(IAsyncResult result);

		// Token: 0x06004002 RID: 16386
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginRemoveDelegate(RemoveDelegateJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004003 RID: 16387
		RemoveDelegateJsonResponse EndRemoveDelegate(IAsyncResult result);

		// Token: 0x06004004 RID: 16388
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginUpdateDelegate(UpdateDelegateJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004005 RID: 16389
		UpdateDelegateJsonResponse EndUpdateDelegate(IAsyncResult result);

		// Token: 0x06004006 RID: 16390
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginCreateUserConfiguration(CreateUserConfigurationJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004007 RID: 16391
		CreateUserConfigurationJsonResponse EndCreateUserConfiguration(IAsyncResult result);

		// Token: 0x06004008 RID: 16392
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginDeleteUserConfiguration(DeleteUserConfigurationJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004009 RID: 16393
		DeleteUserConfigurationJsonResponse EndDeleteUserConfiguration(IAsyncResult result);

		// Token: 0x0600400A RID: 16394
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		[OfflineClient(Queued = false)]
		IAsyncResult BeginGetUserConfiguration(GetUserConfigurationJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600400B RID: 16395
		GetUserConfigurationJsonResponse EndGetUserConfiguration(IAsyncResult result);

		// Token: 0x0600400C RID: 16396
		[OfflineClient(Queued = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginUpdateUserConfiguration(UpdateUserConfigurationJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600400D RID: 16397
		UpdateUserConfigurationJsonResponse EndUpdateUserConfiguration(IAsyncResult result);

		// Token: 0x0600400E RID: 16398
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetServiceConfiguration(GetServiceConfigurationJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600400F RID: 16399
		GetServiceConfigurationJsonResponse EndGetServiceConfiguration(IAsyncResult result);

		// Token: 0x06004010 RID: 16400
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		[OfflineClient(Queued = false)]
		IAsyncResult BeginGetMailTips(GetMailTipsJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004011 RID: 16401
		GetMailTipsJsonResponse EndGetMailTips(IAsyncResult result);

		// Token: 0x06004012 RID: 16402
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		IAsyncResult BeginPlayOnPhone(PlayOnPhoneJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004013 RID: 16403
		PlayOnPhoneJsonResponse EndPlayOnPhone(IAsyncResult result);

		// Token: 0x06004014 RID: 16404
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetPhoneCallInformation(GetPhoneCallInformationJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004015 RID: 16405
		GetPhoneCallInformationJsonResponse EndGetPhoneCallInformation(IAsyncResult result);

		// Token: 0x06004016 RID: 16406
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginDisconnectPhoneCall(DisconnectPhoneCallJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004017 RID: 16407
		DisconnectPhoneCallJsonResponse EndDisconnectPhoneCall(IAsyncResult result);

		// Token: 0x06004018 RID: 16408
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetUserAvailability(GetUserAvailabilityJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004019 RID: 16409
		GetUserAvailabilityJsonResponse EndGetUserAvailability(IAsyncResult result);

		// Token: 0x0600401A RID: 16410
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetUserOofSettings(GetUserOofSettingsJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600401B RID: 16411
		GetUserOofSettingsJsonResponse EndGetUserOofSettings(IAsyncResult result);

		// Token: 0x0600401C RID: 16412
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginSetUserOofSettings(SetUserOofSettingsJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600401D RID: 16413
		SetUserOofSettingsJsonResponse EndSetUserOofSettings(IAsyncResult result);

		// Token: 0x0600401E RID: 16414
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetSharingMetadata(GetSharingMetadataJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600401F RID: 16415
		GetSharingMetadataJsonResponse EndGetSharingMetadata(IAsyncResult result);

		// Token: 0x06004020 RID: 16416
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginRefreshSharingFolder(RefreshSharingFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004021 RID: 16417
		RefreshSharingFolderJsonResponse EndRefreshSharingFolder(IAsyncResult result);

		// Token: 0x06004022 RID: 16418
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginGetSharingFolder(GetSharingFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004023 RID: 16419
		GetSharingFolderJsonResponse EndGetSharingFolder(IAsyncResult result);

		// Token: 0x06004024 RID: 16420
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginGetReminders(GetRemindersJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004025 RID: 16421
		GetRemindersJsonResponse EndGetReminders(IAsyncResult result);

		// Token: 0x06004026 RID: 16422
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = true)]
		IAsyncResult BeginPerformReminderAction(PerformReminderActionJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004027 RID: 16423
		PerformReminderActionJsonResponse EndPerformReminderAction(IAsyncResult result);

		// Token: 0x06004028 RID: 16424
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetRoomLists(GetRoomListsJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004029 RID: 16425
		GetRoomListsJsonResponse EndGetRoomLists(IAsyncResult result);

		// Token: 0x0600402A RID: 16426
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginGetRooms(GetRoomsJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600402B RID: 16427
		GetRoomsJsonResponse EndGetRooms(IAsyncResult result);

		// Token: 0x0600402C RID: 16428
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginFindMessageTrackingReport(FindMessageTrackingReportJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600402D RID: 16429
		FindMessageTrackingReportJsonResponse EndFindMessageTrackingReport(IAsyncResult result);

		// Token: 0x0600402E RID: 16430
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetMessageTrackingReport(GetMessageTrackingReportJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600402F RID: 16431
		GetMessageTrackingReportJsonResponse EndGetMessageTrackingReport(IAsyncResult result);

		// Token: 0x06004030 RID: 16432
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginFindConversation(FindConversationJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004031 RID: 16433
		FindConversationJsonResponse EndFindConversation(IAsyncResult result);

		// Token: 0x06004032 RID: 16434
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		[OfflineClient(Queued = false)]
		IAsyncResult BeginSyncConversation(SyncConversationJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004033 RID: 16435
		SyncConversationJsonResponse EndSyncConversation(IAsyncResult result);

		// Token: 0x06004034 RID: 16436
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		[OfflineClient(Queued = true)]
		IAsyncResult BeginApplyConversationAction(ApplyConversationActionJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004035 RID: 16437
		ApplyConversationActionJsonResponse EndApplyConversationAction(IAsyncResult result);

		// Token: 0x06004036 RID: 16438
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetInboxRules(GetInboxRulesJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004037 RID: 16439
		GetInboxRulesJsonResponse EndGetInboxRules(IAsyncResult result);

		// Token: 0x06004038 RID: 16440
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginFindPeople(FindPeopleJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004039 RID: 16441
		FindPeopleJsonResponse EndFindPeople(IAsyncResult result);

		// Token: 0x0600403A RID: 16442
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginSyncAutoCompleteRecipients(SyncAutoCompleteRecipientsJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600403B RID: 16443
		SyncAutoCompleteRecipientsJsonResponse EndSyncAutoCompleteRecipients(IAsyncResult result);

		// Token: 0x0600403C RID: 16444
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginSyncPeople(SyncPeopleJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600403D RID: 16445
		SyncPeopleJsonResponse EndSyncPeople(IAsyncResult result);

		// Token: 0x0600403E RID: 16446
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetPersona(GetPersonaJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600403F RID: 16447
		GetPersonaJsonResponse EndGetPersona(IAsyncResult result);

		// Token: 0x06004040 RID: 16448
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginUpdateInboxRules(UpdateInboxRulesJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004041 RID: 16449
		UpdateInboxRulesJsonResponse EndUpdateInboxRules(IAsyncResult result);

		// Token: 0x06004042 RID: 16450
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginExecuteDiagnosticMethod(ExecuteDiagnosticMethodJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004043 RID: 16451
		ExecuteDiagnosticMethodJsonResponse EndExecuteDiagnosticMethod(IAsyncResult result);

		// Token: 0x06004044 RID: 16452
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginFindMailboxStatisticsByKeywords(FindMailboxStatisticsByKeywordsJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004045 RID: 16453
		FindMailboxStatisticsByKeywordsJsonResponse EndFindMailboxStatisticsByKeywords(IAsyncResult result);

		// Token: 0x06004046 RID: 16454
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetSearchableMailboxes(GetSearchableMailboxesJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004047 RID: 16455
		GetSearchableMailboxesJsonResponse EndGetSearchableMailboxes(IAsyncResult result);

		// Token: 0x06004048 RID: 16456
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginSearchMailboxes(SearchMailboxesJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004049 RID: 16457
		SearchMailboxesJsonResponse EndSearchMailboxes(IAsyncResult result);

		// Token: 0x0600404A RID: 16458
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginGetHoldOnMailboxes(GetHoldOnMailboxesJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600404B RID: 16459
		GetHoldOnMailboxesJsonResponse EndGetHoldOnMailboxes(IAsyncResult result);

		// Token: 0x0600404C RID: 16460
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginSetHoldOnMailboxes(SetHoldOnMailboxesJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600404D RID: 16461
		SetHoldOnMailboxesJsonResponse EndSetHoldOnMailboxes(IAsyncResult result);

		// Token: 0x0600404E RID: 16462
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginGetPasswordExpirationDate(GetPasswordExpirationDateJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600404F RID: 16463
		GetPasswordExpirationDateJsonResponse EndGetPasswordExpirationDate(IAsyncResult result);

		// Token: 0x06004050 RID: 16464
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		IAsyncResult BeginMarkAllItemsAsRead(MarkAllItemsAsReadJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004051 RID: 16465
		MarkAllItemsAsReadJsonResponse EndMarkAllItemsAsRead(IAsyncResult result);

		// Token: 0x06004052 RID: 16466
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = true)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginMarkAsJunk(MarkAsJunkJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004053 RID: 16467
		MarkAsJunkJsonResponse EndMarkAsJunk(IAsyncResult result);

		// Token: 0x06004054 RID: 16468
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginAddDistributionGroupToImList(AddDistributionGroupToImListJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004055 RID: 16469
		AddDistributionGroupToImListJsonResponse EndAddDistributionGroupToImList(IAsyncResult result);

		// Token: 0x06004056 RID: 16470
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginAddImContactToGroup(AddImContactToGroupJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004057 RID: 16471
		AddImContactToGroupJsonResponse EndAddImContactToGroup(IAsyncResult result);

		// Token: 0x06004058 RID: 16472
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginRemoveImContactFromGroup(RemoveImContactFromGroupJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004059 RID: 16473
		RemoveImContactFromGroupJsonResponse EndRemoveImContactFromGroup(IAsyncResult result);

		// Token: 0x0600405A RID: 16474
		[OperationContract(AsyncPattern = true)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginAddImGroup(AddImGroupJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600405B RID: 16475
		AddImGroupJsonResponse EndAddImGroup(IAsyncResult result);

		// Token: 0x0600405C RID: 16476
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginAddNewImContactToGroup(AddNewImContactToGroupJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600405D RID: 16477
		AddNewImContactToGroupJsonResponse EndAddNewImContactToGroup(IAsyncResult result);

		// Token: 0x0600405E RID: 16478
		[OperationContract(AsyncPattern = true)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginAddNewTelUriContactToGroup(AddNewTelUriContactToGroupJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600405F RID: 16479
		AddNewTelUriContactToGroupJsonResponse EndAddNewTelUriContactToGroup(IAsyncResult result);

		// Token: 0x06004060 RID: 16480
		[OperationContract(AsyncPattern = true)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginGetImItemList(GetImItemListJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004061 RID: 16481
		GetImItemListJsonResponse EndGetImItemList(IAsyncResult result);

		// Token: 0x06004062 RID: 16482
		[OperationContract(AsyncPattern = true)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginGetImItems(GetImItemsJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004063 RID: 16483
		GetImItemsJsonResponse EndGetImItems(IAsyncResult result);

		// Token: 0x06004064 RID: 16484
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginRemoveContactFromImList(RemoveContactFromImListJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004065 RID: 16485
		RemoveContactFromImListJsonResponse EndRemoveContactFromImList(IAsyncResult result);

		// Token: 0x06004066 RID: 16486
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginRemoveDistributionGroupFromImList(RemoveDistributionGroupFromImListJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004067 RID: 16487
		RemoveDistributionGroupFromImListJsonResponse EndRemoveDistributionGroupFromImList(IAsyncResult result);

		// Token: 0x06004068 RID: 16488
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		IAsyncResult BeginRemoveImGroup(RemoveImGroupJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004069 RID: 16489
		RemoveImGroupJsonResponse EndRemoveImGroup(IAsyncResult result);

		// Token: 0x0600406A RID: 16490
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginSetImGroup(SetImGroupJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600406B RID: 16491
		SetImGroupJsonResponse EndSetImGroup(IAsyncResult result);

		// Token: 0x0600406C RID: 16492
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		[OfflineClient(Queued = false)]
		IAsyncResult BeginSetImListMigrationCompleted(SetImListMigrationCompletedJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600406D RID: 16493
		SetImListMigrationCompletedJsonResponse EndSetImListMigrationCompleted(IAsyncResult result);

		// Token: 0x0600406E RID: 16494
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginGetConversationItemsDiagnostics(GetConversationItemsDiagnosticsJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600406F RID: 16495
		GetConversationItemsDiagnosticsJsonResponse EndGetConversationItemsDiagnostics(IAsyncResult result);

		// Token: 0x06004070 RID: 16496
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetConversationItems(GetConversationItemsJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004071 RID: 16497
		GetConversationItemsJsonResponse EndGetConversationItems(IAsyncResult result);

		// Token: 0x06004072 RID: 16498
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetThreadedConversationItems(GetThreadedConversationItemsJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004073 RID: 16499
		GetThreadedConversationItemsJsonResponse EndGetThreadedConversationItems(IAsyncResult result);

		// Token: 0x06004074 RID: 16500
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginGetModernConversationAttachments(GetModernConversationAttachmentsJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004075 RID: 16501
		GetModernConversationAttachmentsJsonResponse EndGetModernConversationAttachments(IAsyncResult result);

		// Token: 0x06004076 RID: 16502
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginGetUserRetentionPolicyTags(GetUserRetentionPolicyTagsJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004077 RID: 16503
		GetUserRetentionPolicyTagsJsonResponse EndGetUserRetentionPolicyTags(IAsyncResult result);

		// Token: 0x06004078 RID: 16504
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		AddSharedCalendarResponse AddSharedCalendar(AddSharedCalendarRequest request);

		// Token: 0x06004079 RID: 16505
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		ClearMobileDeviceResponse ClearMobileDevice(ClearMobileDeviceRequest request);

		// Token: 0x0600407A RID: 16506
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		CalendarActionFolderIdResponse SubscribeInternalCalendar(SubscribeInternalCalendarRequest request);

		// Token: 0x0600407B RID: 16507
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		[OfflineClient(Queued = false)]
		CalendarActionFolderIdResponse SubscribeInternetCalendar(SubscribeInternetCalendarRequest request);

		// Token: 0x0600407C RID: 16508
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		GetCalendarSharingRecipientInfoResponse GetCalendarSharingRecipientInfo(GetCalendarSharingRecipientInfoRequest request);

		// Token: 0x0600407D RID: 16509
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		GetCalendarSharingPermissionsResponse GetCalendarSharingPermissions(GetCalendarSharingPermissionsRequest request);

		// Token: 0x0600407E RID: 16510
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		CalendarActionResponse SetCalendarSharingPermissions(SetCalendarSharingPermissionsRequest request);

		// Token: 0x0600407F RID: 16511
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		SetCalendarPublishingResponse SetCalendarPublishing(SetCalendarPublishingRequest request);

		// Token: 0x06004080 RID: 16512
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		CalendarShareInviteResponse SendCalendarSharingInvite(CalendarShareInviteRequest request);

		// Token: 0x06004081 RID: 16513
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		ExtensibilityContext GetExtensibilityContext(GetExtensibilityContextParameters request);

		// Token: 0x06004082 RID: 16514
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
		[OperationContract]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		bool AddBuddy(Buddy buddy);

		// Token: 0x06004083 RID: 16515
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		[OperationContract]
		GetBuddyListResponse GetBuddyList();

		// Token: 0x06004084 RID: 16516
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginFindTrendingConversation(FindTrendingConversationJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004085 RID: 16517
		FindConversationJsonResponse EndFindTrendingConversation(IAsyncResult result);

		// Token: 0x06004086 RID: 16518
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginFindPlaces(FindPlacesRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004087 RID: 16519
		Persona[] EndFindPlaces(IAsyncResult result);

		// Token: 0x06004088 RID: 16520
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = true)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		DeletePlaceJsonResponse DeletePlace(DeletePlaceRequest request);

		// Token: 0x06004089 RID: 16521
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		CalendarActionResponse AddEventToMyCalendar(AddEventToMyCalendarRequest request);

		// Token: 0x0600408A RID: 16522
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = true)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		bool AddTrustedSender(Microsoft.Exchange.Services.Core.Types.ItemId itemId);

		// Token: 0x0600408B RID: 16523
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		GetFavoritesResponse GetFavorites();

		// Token: 0x0600408C RID: 16524
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		GetPersonaModernGroupMembershipJsonResponse GetPersonaModernGroupMembership(GetPersonaModernGroupMembershipJsonRequest request);

		// Token: 0x0600408D RID: 16525
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		GetModernGroupsJsonResponse GetModernGroups();

		// Token: 0x0600408E RID: 16526
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		GetModernGroupJsonResponse GetModernGroup(GetModernGroupJsonRequest request);

		// Token: 0x0600408F RID: 16527
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		GetModernGroupJsonResponse GetRecommendedModernGroup(GetModernGroupJsonRequest request);

		// Token: 0x06004090 RID: 16528
		[OperationContract]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		SetModernGroupPinStateJsonResponse SetModernGroupPinState(string smtpAddress, bool isPinned);

		// Token: 0x06004091 RID: 16529
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		SetModernGroupMembershipJsonResponse SetModernGroupMembership(SetModernGroupMembershipJsonRequest request);

		// Token: 0x06004092 RID: 16530
		[OperationContract]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		bool SetModernGroupSubscription();

		// Token: 0x06004093 RID: 16531
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		GetModernGroupUnseenItemsJsonResponse GetModernGroupUnseenItems(GetModernGroupUnseenItemsJsonRequest request);

		// Token: 0x06004094 RID: 16532
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		UpdateFavoriteFolderResponse UpdateFavoriteFolder(UpdateFavoriteFolderRequest request);

		// Token: 0x06004095 RID: 16533
		[OfflineClient(Queued = false)]
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		UpdateMasterCategoryListResponse UpdateMasterCategoryList(UpdateMasterCategoryListRequest request);

		// Token: 0x06004096 RID: 16534
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		[OfflineClient(Queued = false)]
		MasterCategoryListActionResponse GetMasterCategoryList(GetMasterCategoryListRequest request);

		// Token: 0x06004097 RID: 16535
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		GetTaskFoldersResponse GetTaskFolders();

		// Token: 0x06004098 RID: 16536
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		TaskFolderActionFolderIdResponse CreateTaskFolder(string newTaskFolderName, string parentGroupGuid);

		// Token: 0x06004099 RID: 16537
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		TaskFolderActionFolderIdResponse RenameTaskFolder(Microsoft.Exchange.Services.Core.Types.ItemId itemId, string newTaskFolderName);

		// Token: 0x0600409A RID: 16538
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		TaskFolderActionResponse DeleteTaskFolder(Microsoft.Exchange.Services.Core.Types.ItemId itemId);

		// Token: 0x0600409B RID: 16539
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginExecuteEwsProxy(EwsProxyRequestParameters request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600409C RID: 16540
		EwsProxyResponse EndExecuteEwsProxy(IAsyncResult result);

		// Token: 0x0600409D RID: 16541
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		SaveExtensionSettingsResponse SaveExtensionSettings(SaveExtensionSettingsParameters request);

		// Token: 0x0600409E RID: 16542
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		LoadExtensionCustomPropertiesResponse LoadExtensionCustomProperties(LoadExtensionCustomPropertiesParameters request);

		// Token: 0x0600409F RID: 16543
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		[OfflineClient(Queued = false)]
		SaveExtensionCustomPropertiesResponse SaveExtensionCustomProperties(SaveExtensionCustomPropertiesParameters request);

		// Token: 0x060040A0 RID: 16544
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		Persona UpdatePersona(UpdatePersonaJsonRequest request);

		// Token: 0x060040A1 RID: 16545
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = true)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		DeletePersonaJsonResponse DeletePersona(Microsoft.Exchange.Services.Core.Types.ItemId personaId, BaseFolderId folderId);

		// Token: 0x060040A2 RID: 16546
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		MaskAutoCompleteRecipientResponse MaskAutoCompleteRecipient(MaskAutoCompleteRecipientRequest request);

		// Token: 0x060040A3 RID: 16547
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		Persona CreatePersona(CreatePersonaJsonRequest request);

		// Token: 0x060040A4 RID: 16548
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		CreateModernGroupResponse CreateModernGroup(CreateModernGroupRequest request);

		// Token: 0x060040A5 RID: 16549
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		UpdateModernGroupResponse UpdateModernGroup(UpdateModernGroupRequest request);

		// Token: 0x060040A6 RID: 16550
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		RemoveModernGroupResponse RemoveModernGroup(RemoveModernGroupRequest request);

		// Token: 0x060040A7 RID: 16551
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		ModernGroupMembershipRequestMessageDetailsResponse ModernGroupMembershipRequestMessageDetails(ModernGroupMembershipRequestMessageDetailsRequest request);

		// Token: 0x060040A8 RID: 16552
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		ValidateModernGroupAliasResponse ValidateModernGroupAlias(ValidateModernGroupAliasRequest request);

		// Token: 0x060040A9 RID: 16553
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		GetModernGroupDomainResponse GetModernGroupDomain();

		// Token: 0x060040AA RID: 16554
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		[OfflineClient(Queued = false)]
		GetPeopleIKnowGraphResponse GetPeopleIKnowGraphCommand(GetPeopleIKnowGraphRequest request);

		// Token: 0x060040AB RID: 16555
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		Microsoft.Exchange.Services.Core.Types.ItemId[] GetPersonaSuggestions(Microsoft.Exchange.Services.Core.Types.ItemId personaId);

		// Token: 0x060040AC RID: 16556
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		[OfflineClient(Queued = false)]
		Persona UnlinkPersona(Microsoft.Exchange.Services.Core.Types.ItemId personaId, Microsoft.Exchange.Services.Core.Types.ItemId contactId);

		// Token: 0x060040AD RID: 16557
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		Persona AcceptPersonaLinkSuggestion(Microsoft.Exchange.Services.Core.Types.ItemId linkToPersonaId, Microsoft.Exchange.Services.Core.Types.ItemId suggestedPersonaId);

		// Token: 0x060040AE RID: 16558
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		Persona LinkPersona(Microsoft.Exchange.Services.Core.Types.ItemId linkToPersonaId, Microsoft.Exchange.Services.Core.Types.ItemId personaIdToBeLinked);

		// Token: 0x060040AF RID: 16559
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		Persona RejectPersonaLinkSuggestion(Microsoft.Exchange.Services.Core.Types.ItemId personaId, Microsoft.Exchange.Services.Core.Types.ItemId suggestedPersonaId);

		// Token: 0x060040B0 RID: 16560
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		SyncCalendarResponse SyncCalendar(SyncCalendarParameters request);

		// Token: 0x060040B1 RID: 16561
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		CalendarActionGroupIdResponse CreateCalendarGroup(string newGroupName);

		// Token: 0x060040B2 RID: 16562
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		CalendarActionGroupIdResponse RenameCalendarGroup(Microsoft.Exchange.Services.Core.Types.ItemId groupId, string newGroupName);

		// Token: 0x060040B3 RID: 16563
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		CalendarActionResponse DeleteCalendarGroup(string groupId);

		// Token: 0x060040B4 RID: 16564
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		CalendarActionFolderIdResponse CreateCalendar(string newCalendarName, string parentGroupGuid, string emailAddress);

		// Token: 0x060040B5 RID: 16565
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		[OfflineClient(Queued = false)]
		CalendarActionFolderIdResponse RenameCalendar(Microsoft.Exchange.Services.Core.Types.ItemId itemId, string newCalendarName);

		// Token: 0x060040B6 RID: 16566
		[OperationContract]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		CalendarActionResponse DeleteCalendar(Microsoft.Exchange.Services.Core.Types.ItemId itemId);

		// Token: 0x060040B7 RID: 16567
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		CalendarActionItemIdResponse SetCalendarColor(Microsoft.Exchange.Services.Core.Types.ItemId itemId, CalendarColor calendarColor);

		// Token: 0x060040B8 RID: 16568
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		CalendarActionResponse MoveCalendar(FolderId calendarToMove, string parentGroupId, FolderId calendarBefore);

		// Token: 0x060040B9 RID: 16569
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		CalendarActionResponse SetCalendarGroupOrder(string groupToPosition, string beforeGroup);

		// Token: 0x060040BA RID: 16570
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		GetCalendarFoldersResponse GetCalendarFolders();

		// Token: 0x060040BB RID: 16571
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		GetCalendarFolderConfigurationResponse GetCalendarFolderConfiguration(GetCalendarFolderConfigurationRequest request);

		// Token: 0x060040BC RID: 16572
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OperationContract]
		EnableAppDataResponse EnableApp(EnableAppDataRequest request);

		// Token: 0x060040BD RID: 16573
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		DisableAppDataResponse DisableApp(DisableAppDataRequest request);

		// Token: 0x060040BE RID: 16574
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OperationContract]
		RemoveAppDataResponse RemoveApp(RemoveAppDataRequest request);

		// Token: 0x060040BF RID: 16575
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract]
		GetCalendarNotificationResponse GetCalendarNotification();

		// Token: 0x060040C0 RID: 16576
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		OptionsResponseBase SetCalendarNotification(SetCalendarNotificationRequest request);

		// Token: 0x060040C1 RID: 16577
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		GetCalendarProcessingResponse GetCalendarProcessing();

		// Token: 0x060040C2 RID: 16578
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		OptionsResponseBase SetCalendarProcessing(SetCalendarProcessingRequest request);

		// Token: 0x060040C3 RID: 16579
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		GetCASMailboxResponse GetCASMailbox();

		// Token: 0x060040C4 RID: 16580
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OperationContract]
		GetCASMailboxResponse GetCASMailbox2(GetCASMailboxRequest request);

		// Token: 0x060040C5 RID: 16581
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		GetMobileDeviceStatisticsResponse GetMobileDeviceStatistics(GetMobileDeviceStatisticsRequest request);

		// Token: 0x060040C6 RID: 16582
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		SetCASMailboxResponse SetCASMailbox(SetCASMailboxRequest request);

		// Token: 0x060040C7 RID: 16583
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract]
		GetConnectedAccountsResponse GetConnectedAccounts(GetConnectedAccountsRequest request);

		// Token: 0x060040C8 RID: 16584
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OperationContract]
		GetConnectSubscriptionResponse GetConnectSubscription(GetConnectSubscriptionRequest request);

		// Token: 0x060040C9 RID: 16585
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OperationContract]
		NewConnectSubscriptionResponse NewConnectSubscription(NewConnectSubscriptionRequest request);

		// Token: 0x060040CA RID: 16586
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract]
		RemoveConnectSubscriptionResponse RemoveConnectSubscription(RemoveConnectSubscriptionRequest request);

		// Token: 0x060040CB RID: 16587
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OperationContract]
		SetConnectSubscriptionResponse SetConnectSubscription(SetConnectSubscriptionRequest request);

		// Token: 0x060040CC RID: 16588
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		GetHotmailSubscriptionResponse GetHotmailSubscription(IdentityRequest request);

		// Token: 0x060040CD RID: 16589
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		OptionsResponseBase SetHotmailSubscription(SetHotmailSubscriptionRequest request);

		// Token: 0x060040CE RID: 16590
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		GetImapSubscriptionResponse GetImapSubscription(IdentityRequest request);

		// Token: 0x060040CF RID: 16591
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		ClearTextMessagingAccountResponse ClearTextMessagingAccount(ClearTextMessagingAccountRequest request);

		// Token: 0x060040D0 RID: 16592
		[OperationContract]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		GetTextMessagingAccountResponse GetTextMessagingAccount(GetTextMessagingAccountRequest request);

		// Token: 0x060040D1 RID: 16593
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		SetTextMessagingAccountResponse SetTextMessagingAccount(SetTextMessagingAccountRequest request);

		// Token: 0x060040D2 RID: 16594
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		CompareTextMessagingVerificationCodeResponse CompareTextMessagingVerificationCode(CompareTextMessagingVerificationCodeRequest request);

		// Token: 0x060040D3 RID: 16595
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		SendTextMessagingVerificationCodeResponse SendTextMessagingVerificationCode(SendTextMessagingVerificationCodeRequest request);

		// Token: 0x060040D4 RID: 16596
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		NewImapSubscriptionResponse NewImapSubscription(NewImapSubscriptionRequest request);

		// Token: 0x060040D5 RID: 16597
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		OptionsResponseBase SetImapSubscription(SetImapSubscriptionRequest request);

		// Token: 0x060040D6 RID: 16598
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		ImportContactListResponse ImportContactList(ImportContactListRequest request);

		// Token: 0x060040D7 RID: 16599
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract]
		DisableInboxRuleResponse DisableInboxRule(DisableInboxRuleRequest request);

		// Token: 0x060040D8 RID: 16600
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OperationContract]
		EnableInboxRuleResponse EnableInboxRule(EnableInboxRuleRequest request);

		// Token: 0x060040D9 RID: 16601
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		GetInboxRuleResponse GetInboxRule(GetInboxRuleRequest request);

		// Token: 0x060040DA RID: 16602
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		NewInboxRuleResponse NewInboxRule(NewInboxRuleRequest request);

		// Token: 0x060040DB RID: 16603
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		RemoveInboxRuleResponse RemoveInboxRule(RemoveInboxRuleRequest request);

		// Token: 0x060040DC RID: 16604
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		SetInboxRuleResponse SetInboxRule(SetInboxRuleRequest request);

		// Token: 0x060040DD RID: 16605
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		GetMailboxResponse GetMailboxByIdentity(IdentityRequest request);

		// Token: 0x060040DE RID: 16606
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		OptionsResponseBase SetMailbox(SetMailboxRequest request);

		// Token: 0x060040DF RID: 16607
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.HttpHeaders)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		GetMailboxAutoReplyConfigurationResponse GetMailboxAutoReplyConfiguration();

		// Token: 0x060040E0 RID: 16608
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		OptionsResponseBase SetMailboxAutoReplyConfiguration(SetMailboxAutoReplyConfigurationRequest request);

		// Token: 0x060040E1 RID: 16609
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.HttpHeaders)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		GetMailboxCalendarConfigurationResponse GetMailboxCalendarConfiguration();

		// Token: 0x060040E2 RID: 16610
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		OptionsResponseBase SetMailboxCalendarConfiguration(SetMailboxCalendarConfigurationRequest request);

		// Token: 0x060040E3 RID: 16611
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		GetMailboxJunkEmailConfigurationResponse GetMailboxJunkEmailConfiguration();

		// Token: 0x060040E4 RID: 16612
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		OptionsResponseBase SetMailboxJunkEmailConfiguration(SetMailboxJunkEmailConfigurationRequest request);

		// Token: 0x060040E5 RID: 16613
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		GetMailboxRegionalConfigurationResponse GetMailboxRegionalConfiguration(GetMailboxRegionalConfigurationRequest request);

		// Token: 0x060040E6 RID: 16614
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		SetMailboxRegionalConfigurationResponse SetMailboxRegionalConfiguration(SetMailboxRegionalConfigurationRequest request);

		// Token: 0x060040E7 RID: 16615
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		GetMailboxMessageConfigurationResponse GetMailboxMessageConfiguration();

		// Token: 0x060040E8 RID: 16616
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		OptionsResponseBase SetMailboxMessageConfiguration(SetMailboxMessageConfigurationRequest request);

		// Token: 0x060040E9 RID: 16617
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract]
		GetMessageCategoryResponse GetMessageCategory();

		// Token: 0x060040EA RID: 16618
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		[OperationContract]
		GetMessageClassificationResponse GetMessageClassification();

		// Token: 0x060040EB RID: 16619
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OperationContract]
		GetAccountInformationResponse GetAccountInformation(GetAccountInformationRequest request);

		// Token: 0x060040EC RID: 16620
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		GetSocialNetworksOAuthInfoResponse GetConnectToSocialNetworksOAuthInfo(GetSocialNetworksOAuthInfoRequest request);

		// Token: 0x060040ED RID: 16621
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		GetPopSubscriptionResponse GetPopSubscription(IdentityRequest request);

		// Token: 0x060040EE RID: 16622
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract]
		NewPopSubscriptionResponse NewPopSubscription(NewPopSubscriptionRequest request);

		// Token: 0x060040EF RID: 16623
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		OptionsResponseBase SetPopSubscription(SetPopSubscriptionRequest request);

		// Token: 0x060040F0 RID: 16624
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		OptionsResponseBase AddActiveRetentionPolicyTags(IdentityCollectionRequest request);

		// Token: 0x060040F1 RID: 16625
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		GetRetentionPolicyTagsResponse GetActiveRetentionPolicyTags(GetRetentionPolicyTagsRequest request);

		// Token: 0x060040F2 RID: 16626
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract]
		GetRetentionPolicyTagsResponse GetAvailableRetentionPolicyTags(GetRetentionPolicyTagsRequest request);

		// Token: 0x060040F3 RID: 16627
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		OptionsResponseBase RemoveActiveRetentionPolicyTags(IdentityCollectionRequest request);

		// Token: 0x060040F4 RID: 16628
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		GetSendAddressResponse GetSendAddress();

		// Token: 0x060040F5 RID: 16629
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.HttpHeaders)]
		[OperationContract]
		GetSubscriptionResponse GetSubscription();

		// Token: 0x060040F6 RID: 16630
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		NewSubscriptionResponse NewSubscription(NewSubscriptionRequest request);

		// Token: 0x060040F7 RID: 16631
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract]
		[OfflineClient(Queued = false)]
		OptionsResponseBase RemoveSubscription(IdentityRequest request);

		// Token: 0x060040F8 RID: 16632
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		SetUserResponse SetUser(SetUserRequest request);

		// Token: 0x060040F9 RID: 16633
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		RemoveMobileDeviceResponse RemoveMobileDevice(RemoveMobileDeviceRequest request);

		// Token: 0x060040FA RID: 16634
		[OperationContract]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		GetUserAvailabilityInternalJsonResponse GetUserAvailabilityInternal(GetUserAvailabilityInternalJsonRequest request);

		// Token: 0x060040FB RID: 16635
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		[OfflineClient(Queued = false)]
		IAsyncResult BeginProvision(ProvisionJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060040FC RID: 16636
		ProvisionJsonResponse EndProvision(IAsyncResult result);

		// Token: 0x060040FD RID: 16637
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetTimeZoneOffsets(GetTimeZoneOffsetsJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060040FE RID: 16638
		GetTimeZoneOffsetsJsonResponse EndGetTimeZoneOffsets(IAsyncResult result);

		// Token: 0x060040FF RID: 16639
		[OperationContract(AsyncPattern = true)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginDeprovision(DeprovisionJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004100 RID: 16640
		DeprovisionJsonResponse EndDeprovision(IAsyncResult result);

		// Token: 0x06004101 RID: 16641
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		bool SendReadReceipt(Microsoft.Exchange.Services.Core.Types.ItemId itemId);

		// Token: 0x06004102 RID: 16642
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		SuiteStorageJsonResponse ProcessSuiteStorage(SuiteStorageJsonRequest request);

		// Token: 0x06004103 RID: 16643
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginGetWeatherForecast(GetWeatherForecastJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004104 RID: 16644
		GetWeatherForecastJsonResponse EndGetWeatherForecast(IAsyncResult result);

		// Token: 0x06004105 RID: 16645
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginFindWeatherLocations(FindWeatherLocationsJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004106 RID: 16646
		FindWeatherLocationsJsonResponse EndFindWeatherLocations(IAsyncResult result);

		// Token: 0x06004107 RID: 16647
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		[OfflineClient(Queued = false)]
		IAsyncResult BeginLogPushNotificationData(LogPushNotificationDataJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004108 RID: 16648
		LogPushNotificationDataJsonResponse EndLogPushNotificationData(IAsyncResult result);

		// Token: 0x06004109 RID: 16649
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		LikeItemResponse LikeItem(LikeItemRequest request);

		// Token: 0x0600410A RID: 16650
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		[OperationContract]
		GetLikersResponseMessage GetLikers(GetLikersRequest request);

		// Token: 0x0600410B RID: 16651
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		GetAggregatedAccountResponse GetAggregatedAccount(GetAggregatedAccountRequest request);

		// Token: 0x0600410C RID: 16652
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		AddAggregatedAccountResponse AddAggregatedAccount(AddAggregatedAccountRequest request);

		// Token: 0x0600410D RID: 16653
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginRequestDeviceRegistrationChallenge(RequestDeviceRegistrationChallengeJsonRequest deviceRegistrationChallengeRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600410E RID: 16654
		RequestDeviceRegistrationChallengeJsonResponse EndRequestDeviceRegistrationChallenge(IAsyncResult result);

		// Token: 0x0600410F RID: 16655
		[OfflineClient(Queued = true)]
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginCancelCalendarEvent(CancelCalendarEventJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004110 RID: 16656
		CancelCalendarEventJsonResponse EndCancelCalendarEvent(IAsyncResult result);

		// Token: 0x06004111 RID: 16657
		[Deprecated(ExchangeVersionType.V2_4)]
		[OperationContract]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		CalendarActionFolderIdResponse EnableBirthdayCalendar();

		// Token: 0x06004112 RID: 16658
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.None)]
		[OperationContract]
		CalendarActionResponse DisableBirthdayCalendar();

		// Token: 0x06004113 RID: 16659
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		CalendarActionResponse RemoveBirthdayEvent(Microsoft.Exchange.Services.Core.Types.ItemId contactId);

		// Token: 0x06004114 RID: 16660
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginGetBirthdayCalendarView(GetBirthdayCalendarViewJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004115 RID: 16661
		GetBirthdayCalendarViewJsonResponse EndGetBirthdayCalendarView(IAsyncResult result);

		// Token: 0x06004116 RID: 16662
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetUserUnifiedGroups(GetUserUnifiedGroupsJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06004117 RID: 16663
		[JsonRequestFormat(Format = JsonRequestFormat.HttpHeaders)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		GetAllowedOptionsResponse GetAllowedOptions(GetAllowedOptionsRequest request);

		// Token: 0x06004118 RID: 16664
		GetUserUnifiedGroupsJsonResponse EndGetUserUnifiedGroups(IAsyncResult result);

		// Token: 0x06004119 RID: 16665
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginGetClutterState(GetClutterStateJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600411A RID: 16666
		GetClutterStateJsonResponse EndGetClutterState(IAsyncResult result);

		// Token: 0x0600411B RID: 16667
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginSetClutterState(SetClutterStateJsonRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600411C RID: 16668
		SetClutterStateJsonResponse EndSetClutterState(IAsyncResult result);
	}
}
