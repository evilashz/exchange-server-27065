using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B77 RID: 2935
	[ServiceContract(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public interface IEWSContract
	{
		// Token: 0x06005323 RID: 21283
		[XmlSerializerFormat]
		[OperationContract(Action = "*", ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginConvertId(ConvertIdSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005324 RID: 21284
		ConvertIdSoapResponse EndConvertId(IAsyncResult result);

		// Token: 0x06005325 RID: 21285
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginCreateUnifiedMailbox(CreateUnifiedMailboxSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005326 RID: 21286
		CreateUnifiedMailboxSoapResponse EndCreateUnifiedMailbox(IAsyncResult result);

		// Token: 0x06005327 RID: 21287
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginUploadItems(UploadItemsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005328 RID: 21288
		UploadItemsSoapResponse EndUploadItems(IAsyncResult result);

		// Token: 0x06005329 RID: 21289
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginExportItems(ExportItemsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600532A RID: 21290
		ExportItemsSoapResponse EndExportItems(IAsyncResult result);

		// Token: 0x0600532B RID: 21291
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginGetFolder(GetFolderSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600532C RID: 21292
		GetFolderSoapResponse EndGetFolder(IAsyncResult result);

		// Token: 0x0600532D RID: 21293
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginCreateFolder(CreateFolderSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600532E RID: 21294
		CreateFolderSoapResponse EndCreateFolder(IAsyncResult result);

		// Token: 0x0600532F RID: 21295
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginDeleteFolder(DeleteFolderSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005330 RID: 21296
		DeleteFolderSoapResponse EndDeleteFolder(IAsyncResult result);

		// Token: 0x06005331 RID: 21297
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginEmptyFolder(EmptyFolderSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005332 RID: 21298
		EmptyFolderSoapResponse EndEmptyFolder(IAsyncResult result);

		// Token: 0x06005333 RID: 21299
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginUpdateFolder(UpdateFolderSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005334 RID: 21300
		UpdateFolderSoapResponse EndUpdateFolder(IAsyncResult result);

		// Token: 0x06005335 RID: 21301
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginMoveFolder(MoveFolderSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005336 RID: 21302
		MoveFolderSoapResponse EndMoveFolder(IAsyncResult result);

		// Token: 0x06005337 RID: 21303
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginCopyFolder(CopyFolderSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005338 RID: 21304
		CopyFolderSoapResponse EndCopyFolder(IAsyncResult result);

		// Token: 0x06005339 RID: 21305
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginCreateFolderPath(CreateFolderPathSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600533A RID: 21306
		CreateFolderPathSoapResponse EndCreateFolderPath(IAsyncResult result);

		// Token: 0x0600533B RID: 21307
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginFindItem(FindItemSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600533C RID: 21308
		FindItemSoapResponse EndFindItem(IAsyncResult result);

		// Token: 0x0600533D RID: 21309
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginFindFolder(FindFolderSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600533E RID: 21310
		FindFolderSoapResponse EndFindFolder(IAsyncResult result);

		// Token: 0x0600533F RID: 21311
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginGetItem(GetItemSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005340 RID: 21312
		GetItemSoapResponse EndGetItem(IAsyncResult result);

		// Token: 0x06005341 RID: 21313
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginCreateItem(CreateItemSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005342 RID: 21314
		CreateItemSoapResponse EndCreateItem(IAsyncResult result);

		// Token: 0x06005343 RID: 21315
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginDeleteItem(DeleteItemSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005344 RID: 21316
		DeleteItemSoapResponse EndDeleteItem(IAsyncResult result);

		// Token: 0x06005345 RID: 21317
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginUpdateItem(UpdateItemSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005346 RID: 21318
		UpdateItemSoapResponse EndUpdateItem(IAsyncResult result);

		// Token: 0x06005347 RID: 21319
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginUpdateItemInRecoverableItems(UpdateItemInRecoverableItemsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005348 RID: 21320
		UpdateItemInRecoverableItemsSoapResponse EndUpdateItemInRecoverableItems(IAsyncResult result);

		// Token: 0x06005349 RID: 21321
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginSendItem(SendItemSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600534A RID: 21322
		SendItemSoapResponse EndSendItem(IAsyncResult result);

		// Token: 0x0600534B RID: 21323
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginMoveItem(MoveItemSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600534C RID: 21324
		MoveItemSoapResponse EndMoveItem(IAsyncResult result);

		// Token: 0x0600534D RID: 21325
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginCopyItem(CopyItemSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600534E RID: 21326
		CopyItemSoapResponse EndCopyItem(IAsyncResult result);

		// Token: 0x0600534F RID: 21327
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginArchiveItem(ArchiveItemSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005350 RID: 21328
		ArchiveItemSoapResponse EndArchiveItem(IAsyncResult result);

		// Token: 0x06005351 RID: 21329
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginCreateAttachment(CreateAttachmentSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005352 RID: 21330
		CreateAttachmentSoapResponse EndCreateAttachment(IAsyncResult result);

		// Token: 0x06005353 RID: 21331
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginDeleteAttachment(DeleteAttachmentSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005354 RID: 21332
		DeleteAttachmentSoapResponse EndDeleteAttachment(IAsyncResult result);

		// Token: 0x06005355 RID: 21333
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginGetAttachment(GetAttachmentSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005356 RID: 21334
		GetAttachmentSoapResponse EndGetAttachment(IAsyncResult result);

		// Token: 0x06005357 RID: 21335
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginGetClientAccessToken(GetClientAccessTokenSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005358 RID: 21336
		GetClientAccessTokenSoapResponse EndGetClientAccessToken(IAsyncResult result);

		// Token: 0x06005359 RID: 21337
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginResolveNames(ResolveNamesSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600535A RID: 21338
		ResolveNamesSoapResponse EndResolveNames(IAsyncResult result);

		// Token: 0x0600535B RID: 21339
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginExpandDL(ExpandDLSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600535C RID: 21340
		ExpandDLSoapResponse EndExpandDL(IAsyncResult result);

		// Token: 0x0600535D RID: 21341
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginGetServerTimeZones(GetServerTimeZonesSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600535E RID: 21342
		GetServerTimeZonesSoapResponse EndGetServerTimeZones(IAsyncResult result);

		// Token: 0x0600535F RID: 21343
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginCreateManagedFolder(CreateManagedFolderSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005360 RID: 21344
		CreateManagedFolderSoapResponse EndCreateManagedFolder(IAsyncResult result);

		// Token: 0x06005361 RID: 21345
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginSubscribe(SubscribeSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005362 RID: 21346
		SubscribeSoapResponse EndSubscribe(IAsyncResult result);

		// Token: 0x06005363 RID: 21347
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginUnsubscribe(UnsubscribeSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005364 RID: 21348
		UnsubscribeSoapResponse EndUnsubscribe(IAsyncResult result);

		// Token: 0x06005365 RID: 21349
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginGetEvents(GetEventsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005366 RID: 21350
		GetEventsSoapResponse EndGetEvents(IAsyncResult result);

		// Token: 0x06005367 RID: 21351
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginGetStreamingEvents(GetStreamingEventsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005368 RID: 21352
		GetStreamingEventsSoapResponse EndGetStreamingEvents(IAsyncResult result);

		// Token: 0x06005369 RID: 21353
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginSyncFolderHierarchy(SyncFolderHierarchySoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600536A RID: 21354
		SyncFolderHierarchySoapResponse EndSyncFolderHierarchy(IAsyncResult result);

		// Token: 0x0600536B RID: 21355
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginSyncFolderItems(SyncFolderItemsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600536C RID: 21356
		SyncFolderItemsSoapResponse EndSyncFolderItems(IAsyncResult result);

		// Token: 0x0600536D RID: 21357
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginGetDelegate(GetDelegateSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600536E RID: 21358
		GetDelegateSoapResponse EndGetDelegate(IAsyncResult result);

		// Token: 0x0600536F RID: 21359
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginAddDelegate(AddDelegateSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005370 RID: 21360
		AddDelegateSoapResponse EndAddDelegate(IAsyncResult result);

		// Token: 0x06005371 RID: 21361
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginRemoveDelegate(RemoveDelegateSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005372 RID: 21362
		RemoveDelegateSoapResponse EndRemoveDelegate(IAsyncResult result);

		// Token: 0x06005373 RID: 21363
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginUpdateDelegate(UpdateDelegateSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005374 RID: 21364
		UpdateDelegateSoapResponse EndUpdateDelegate(IAsyncResult result);

		// Token: 0x06005375 RID: 21365
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginCreateUserConfiguration(CreateUserConfigurationSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005376 RID: 21366
		CreateUserConfigurationSoapResponse EndCreateUserConfiguration(IAsyncResult result);

		// Token: 0x06005377 RID: 21367
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginDeleteUserConfiguration(DeleteUserConfigurationSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005378 RID: 21368
		DeleteUserConfigurationSoapResponse EndDeleteUserConfiguration(IAsyncResult result);

		// Token: 0x06005379 RID: 21369
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginGetUserConfiguration(GetUserConfigurationSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600537A RID: 21370
		GetUserConfigurationSoapResponse EndGetUserConfiguration(IAsyncResult result);

		// Token: 0x0600537B RID: 21371
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginUpdateUserConfiguration(UpdateUserConfigurationSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600537C RID: 21372
		UpdateUserConfigurationSoapResponse EndUpdateUserConfiguration(IAsyncResult result);

		// Token: 0x0600537D RID: 21373
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginGetServiceConfiguration(GetServiceConfigurationSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600537E RID: 21374
		GetServiceConfigurationSoapResponse EndGetServiceConfiguration(IAsyncResult result);

		// Token: 0x0600537F RID: 21375
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginGetMailTips(GetMailTipsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005380 RID: 21376
		GetMailTipsSoapResponse EndGetMailTips(IAsyncResult result);

		// Token: 0x06005381 RID: 21377
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginPlayOnPhone(PlayOnPhoneSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005382 RID: 21378
		PlayOnPhoneSoapResponse EndPlayOnPhone(IAsyncResult result);

		// Token: 0x06005383 RID: 21379
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginGetPhoneCallInformation(GetPhoneCallInformationSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005384 RID: 21380
		GetPhoneCallInformationSoapResponse EndGetPhoneCallInformation(IAsyncResult result);

		// Token: 0x06005385 RID: 21381
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginDisconnectPhoneCall(DisconnectPhoneCallSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005386 RID: 21382
		DisconnectPhoneCallSoapResponse EndDisconnectPhoneCall(IAsyncResult result);

		// Token: 0x06005387 RID: 21383
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginCreateUMPrompt(CreateUMPromptSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005388 RID: 21384
		CreateUMPromptSoapResponse EndCreateUMPrompt(IAsyncResult result);

		// Token: 0x06005389 RID: 21385
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginDeleteUMPrompts(DeleteUMPromptsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600538A RID: 21386
		DeleteUMPromptsSoapResponse EndDeleteUMPrompts(IAsyncResult result);

		// Token: 0x0600538B RID: 21387
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginGetUMPrompt(GetUMPromptSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600538C RID: 21388
		GetUMPromptSoapResponse EndGetUMPrompt(IAsyncResult result);

		// Token: 0x0600538D RID: 21389
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginGetUMPromptNames(GetUMPromptNamesSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600538E RID: 21390
		GetUMPromptNamesSoapResponse EndGetUMPromptNames(IAsyncResult result);

		// Token: 0x0600538F RID: 21391
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginGetUserAvailability(GetUserAvailabilitySoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005390 RID: 21392
		GetUserAvailabilitySoapResponse EndGetUserAvailability(IAsyncResult result);

		// Token: 0x06005391 RID: 21393
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginGetUserOofSettings(GetUserOofSettingsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005392 RID: 21394
		GetUserOofSettingsSoapResponse EndGetUserOofSettings(IAsyncResult result);

		// Token: 0x06005393 RID: 21395
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginSetUserOofSettings(SetUserOofSettingsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005394 RID: 21396
		SetUserOofSettingsSoapResponse EndSetUserOofSettings(IAsyncResult result);

		// Token: 0x06005395 RID: 21397
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginGetSharingMetadata(GetSharingMetadataSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005396 RID: 21398
		GetSharingMetadataSoapResponse EndGetSharingMetadata(IAsyncResult result);

		// Token: 0x06005397 RID: 21399
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginRefreshSharingFolder(RefreshSharingFolderSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005398 RID: 21400
		RefreshSharingFolderSoapResponse EndRefreshSharingFolder(IAsyncResult result);

		// Token: 0x06005399 RID: 21401
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginGetSharingFolder(GetSharingFolderSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600539A RID: 21402
		GetSharingFolderSoapResponse EndGetSharingFolder(IAsyncResult result);

		// Token: 0x0600539B RID: 21403
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginSetTeamMailbox(SetTeamMailboxSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600539C RID: 21404
		SetTeamMailboxSoapResponse EndSetTeamMailbox(IAsyncResult result);

		// Token: 0x0600539D RID: 21405
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginUnpinTeamMailbox(UnpinTeamMailboxSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600539E RID: 21406
		UnpinTeamMailboxSoapResponse EndUnpinTeamMailbox(IAsyncResult result);

		// Token: 0x0600539F RID: 21407
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginGetRoomLists(GetRoomListsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053A0 RID: 21408
		GetRoomListsSoapResponse EndGetRoomLists(IAsyncResult result);

		// Token: 0x060053A1 RID: 21409
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginGetRooms(GetRoomsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053A2 RID: 21410
		GetRoomsSoapResponse EndGetRooms(IAsyncResult result);

		// Token: 0x060053A3 RID: 21411
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginGetReminders(GetRemindersSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053A4 RID: 21412
		GetRemindersSoapResponse EndGetReminders(IAsyncResult result);

		// Token: 0x060053A5 RID: 21413
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginPerformReminderAction(PerformReminderActionSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053A6 RID: 21414
		PerformReminderActionSoapResponse EndPerformReminderAction(IAsyncResult result);

		// Token: 0x060053A7 RID: 21415
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginFindMessageTrackingReport(FindMessageTrackingReportSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053A8 RID: 21416
		FindMessageTrackingReportSoapResponse EndFindMessageTrackingReport(IAsyncResult result);

		// Token: 0x060053A9 RID: 21417
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginGetMessageTrackingReport(GetMessageTrackingReportSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053AA RID: 21418
		GetMessageTrackingReportSoapResponse EndGetMessageTrackingReport(IAsyncResult result);

		// Token: 0x060053AB RID: 21419
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginFindConversation(FindConversationSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053AC RID: 21420
		FindConversationSoapResponse EndFindConversation(IAsyncResult result);

		// Token: 0x060053AD RID: 21421
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginFindPeople(FindPeopleSoapRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053AE RID: 21422
		FindPeopleSoapResponse EndFindPeople(IAsyncResult result);

		// Token: 0x060053AF RID: 21423
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginGetPersona(GetPersonaSoapRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053B0 RID: 21424
		GetPersonaSoapResponse EndGetPersona(IAsyncResult result);

		// Token: 0x060053B1 RID: 21425
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginApplyConversationAction(ApplyConversationActionSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053B2 RID: 21426
		ApplyConversationActionSoapResponse EndApplyConversationAction(IAsyncResult result);

		// Token: 0x060053B3 RID: 21427
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginGetInboxRules(GetInboxRulesSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053B4 RID: 21428
		GetInboxRulesSoapResponse EndGetInboxRules(IAsyncResult result);

		// Token: 0x060053B5 RID: 21429
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginUpdateInboxRules(UpdateInboxRulesSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053B6 RID: 21430
		UpdateInboxRulesSoapResponse EndUpdateInboxRules(IAsyncResult result);

		// Token: 0x060053B7 RID: 21431
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginMarkAllItemsAsRead(MarkAllItemsAsReadSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053B8 RID: 21432
		MarkAllItemsAsReadSoapResponse EndMarkAllItemsAsRead(IAsyncResult result);

		// Token: 0x060053B9 RID: 21433
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginMarkAsJunk(MarkAsJunkSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053BA RID: 21434
		MarkAsJunkSoapResponse EndMarkAsJunk(IAsyncResult result);

		// Token: 0x060053BB RID: 21435
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginExecuteDiagnosticMethod(ExecuteDiagnosticMethodSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053BC RID: 21436
		ExecuteDiagnosticMethodSoapResponse EndExecuteDiagnosticMethod(IAsyncResult result);

		// Token: 0x060053BD RID: 21437
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginFindMailboxStatisticsByKeywords(FindMailboxStatisticsByKeywordsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053BE RID: 21438
		FindMailboxStatisticsByKeywordsSoapResponse EndFindMailboxStatisticsByKeywords(IAsyncResult result);

		// Token: 0x060053BF RID: 21439
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginGetSearchableMailboxes(GetSearchableMailboxesSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053C0 RID: 21440
		GetSearchableMailboxesSoapResponse EndGetSearchableMailboxes(IAsyncResult result);

		// Token: 0x060053C1 RID: 21441
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginSearchMailboxes(SearchMailboxesSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053C2 RID: 21442
		SearchMailboxesSoapResponse EndSearchMailboxes(IAsyncResult result);

		// Token: 0x060053C3 RID: 21443
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginGetDiscoverySearchConfiguration(GetDiscoverySearchConfigurationSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053C4 RID: 21444
		GetDiscoverySearchConfigurationSoapResponse EndGetDiscoverySearchConfiguration(IAsyncResult result);

		// Token: 0x060053C5 RID: 21445
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginGetHoldOnMailboxes(GetHoldOnMailboxesSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053C6 RID: 21446
		GetHoldOnMailboxesSoapResponse EndGetHoldOnMailboxes(IAsyncResult result);

		// Token: 0x060053C7 RID: 21447
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginSetHoldOnMailboxes(SetHoldOnMailboxesSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053C8 RID: 21448
		SetHoldOnMailboxesSoapResponse EndSetHoldOnMailboxes(IAsyncResult result);

		// Token: 0x060053C9 RID: 21449
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginGetNonIndexableItemStatistics(GetNonIndexableItemStatisticsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053CA RID: 21450
		GetNonIndexableItemStatisticsSoapResponse EndGetNonIndexableItemStatistics(IAsyncResult result);

		// Token: 0x060053CB RID: 21451
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginGetNonIndexableItemDetails(GetNonIndexableItemDetailsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053CC RID: 21452
		GetNonIndexableItemDetailsSoapResponse EndGetNonIndexableItemDetails(IAsyncResult result);

		// Token: 0x060053CD RID: 21453
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginGetPasswordExpirationDate(GetPasswordExpirationDateSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053CE RID: 21454
		GetPasswordExpirationDateSoapResponse EndGetPasswordExpirationDate(IAsyncResult result);

		// Token: 0x060053CF RID: 21455
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginGetClientExtension(GetClientExtensionSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053D0 RID: 21456
		GetClientExtensionSoapResponse EndGetClientExtension(IAsyncResult result);

		// Token: 0x060053D1 RID: 21457
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginSetClientExtension(SetClientExtensionSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053D2 RID: 21458
		SetClientExtensionSoapResponse EndSetClientExtension(IAsyncResult result);

		// Token: 0x060053D3 RID: 21459
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginGetEncryptionConfiguration(GetEncryptionConfigurationSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053D4 RID: 21460
		GetEncryptionConfigurationSoapResponse EndGetEncryptionConfiguration(IAsyncResult result);

		// Token: 0x060053D5 RID: 21461
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginSetEncryptionConfiguration(SetEncryptionConfigurationSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053D6 RID: 21462
		SetEncryptionConfigurationSoapResponse EndSetEncryptionConfiguration(IAsyncResult result);

		// Token: 0x060053D7 RID: 21463
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginGetAppManifests(GetAppManifestsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053D8 RID: 21464
		GetAppManifestsSoapResponse EndGetAppManifests(IAsyncResult result);

		// Token: 0x060053D9 RID: 21465
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginInstallApp(InstallAppSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053DA RID: 21466
		InstallAppSoapResponse EndInstallApp(IAsyncResult result);

		// Token: 0x060053DB RID: 21467
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginUninstallApp(UninstallAppSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053DC RID: 21468
		UninstallAppSoapResponse EndUninstallApp(IAsyncResult result);

		// Token: 0x060053DD RID: 21469
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginDisableApp(DisableAppSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053DE RID: 21470
		DisableAppSoapResponse EndDisableApp(IAsyncResult result);

		// Token: 0x060053DF RID: 21471
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginGetAppMarketplaceUrl(GetAppMarketplaceUrlSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053E0 RID: 21472
		GetAppMarketplaceUrlSoapResponse EndGetAppMarketplaceUrl(IAsyncResult result);

		// Token: 0x060053E1 RID: 21473
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginAddAggregatedAccount(AddAggregatedAccountSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053E2 RID: 21474
		AddAggregatedAccountSoapResponse EndAddAggregatedAccount(IAsyncResult result);

		// Token: 0x060053E3 RID: 21475
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginAddDistributionGroupToImList(AddDistributionGroupToImListSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053E4 RID: 21476
		AddDistributionGroupToImListSoapResponse EndAddDistributionGroupToImList(IAsyncResult result);

		// Token: 0x060053E5 RID: 21477
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginAddImContactToGroup(AddImContactToGroupSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053E6 RID: 21478
		AddImContactToGroupSoapResponse EndAddImContactToGroup(IAsyncResult result);

		// Token: 0x060053E7 RID: 21479
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginRemoveImContactFromGroup(RemoveImContactFromGroupSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053E8 RID: 21480
		RemoveImContactFromGroupSoapResponse EndRemoveImContactFromGroup(IAsyncResult result);

		// Token: 0x060053E9 RID: 21481
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginAddImGroup(AddImGroupSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053EA RID: 21482
		AddImGroupSoapResponse EndAddImGroup(IAsyncResult result);

		// Token: 0x060053EB RID: 21483
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginAddNewImContactToGroup(AddNewImContactToGroupSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053EC RID: 21484
		AddNewImContactToGroupSoapResponse EndAddNewImContactToGroup(IAsyncResult result);

		// Token: 0x060053ED RID: 21485
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginAddNewTelUriContactToGroup(AddNewTelUriContactToGroupSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053EE RID: 21486
		AddNewTelUriContactToGroupSoapResponse EndAddNewTelUriContactToGroup(IAsyncResult result);

		// Token: 0x060053EF RID: 21487
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginGetImItemList(GetImItemListSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053F0 RID: 21488
		GetImItemListSoapResponse EndGetImItemList(IAsyncResult result);

		// Token: 0x060053F1 RID: 21489
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginGetImItems(GetImItemsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053F2 RID: 21490
		GetImItemsSoapResponse EndGetImItems(IAsyncResult result);

		// Token: 0x060053F3 RID: 21491
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginRemoveContactFromImList(RemoveContactFromImListSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053F4 RID: 21492
		RemoveContactFromImListSoapResponse EndRemoveContactFromImList(IAsyncResult result);

		// Token: 0x060053F5 RID: 21493
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginRemoveDistributionGroupFromImList(RemoveDistributionGroupFromImListSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053F6 RID: 21494
		RemoveDistributionGroupFromImListSoapResponse EndRemoveDistributionGroupFromImList(IAsyncResult result);

		// Token: 0x060053F7 RID: 21495
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginRemoveImGroup(RemoveImGroupSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053F8 RID: 21496
		RemoveImGroupSoapResponse EndRemoveImGroup(IAsyncResult result);

		// Token: 0x060053F9 RID: 21497
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginSetImGroup(SetImGroupSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053FA RID: 21498
		SetImGroupSoapResponse EndSetImGroup(IAsyncResult result);

		// Token: 0x060053FB RID: 21499
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginSetImListMigrationCompleted(SetImListMigrationCompletedSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053FC RID: 21500
		SetImListMigrationCompletedSoapResponse EndSetImListMigrationCompleted(IAsyncResult result);

		// Token: 0x060053FD RID: 21501
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginGetConversationItems(GetConversationItemsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060053FE RID: 21502
		GetConversationItemsSoapResponse EndGetConversationItems(IAsyncResult result);

		// Token: 0x060053FF RID: 21503
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginGetUserRetentionPolicyTags(GetUserRetentionPolicyTagsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005400 RID: 21504
		GetUserRetentionPolicyTagsSoapResponse EndGetUserRetentionPolicyTags(IAsyncResult result);

		// Token: 0x06005401 RID: 21505
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginStartFindInGALSpeechRecognition(StartFindInGALSpeechRecognitionSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005402 RID: 21506
		StartFindInGALSpeechRecognitionSoapResponse EndStartFindInGALSpeechRecognition(IAsyncResult result);

		// Token: 0x06005403 RID: 21507
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginCompleteFindInGALSpeechRecognition(CompleteFindInGALSpeechRecognitionSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005404 RID: 21508
		CompleteFindInGALSpeechRecognitionSoapResponse EndCompleteFindInGALSpeechRecognition(IAsyncResult result);

		// Token: 0x06005405 RID: 21509
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginCreateUMCallDataRecord(CreateUMCallDataRecordSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005406 RID: 21510
		CreateUMCallDataRecordSoapResponse EndCreateUMCallDataRecord(IAsyncResult result);

		// Token: 0x06005407 RID: 21511
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		[XmlSerializerFormat]
		IAsyncResult BeginGetUMCallDataRecords(GetUMCallDataRecordsSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005408 RID: 21512
		GetUMCallDataRecordsSoapResponse EndGetUMCallDataRecords(IAsyncResult result);

		// Token: 0x06005409 RID: 21513
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true)]
		IAsyncResult BeginGetUMCallSummary(GetUMCallSummarySoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600540A RID: 21514
		GetUMCallSummarySoapResponse EndGetUMCallSummary(IAsyncResult result);

		// Token: 0x0600540B RID: 21515
		[OperationContract(ReplyAction = "*", AsyncPattern = true, Name = "GetUserPhoto")]
		[XmlSerializerFormat]
		IAsyncResult BeginGetUserPhotoData(GetUserPhotoSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600540C RID: 21516
		GetUserPhotoSoapResponse EndGetUserPhotoData(IAsyncResult result);

		// Token: 0x0600540D RID: 21517
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true, Name = "InitUMMailbox")]
		IAsyncResult BeginInitUMMailbox(InitUMMailboxSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600540E RID: 21518
		InitUMMailboxSoapResponse EndInitUMMailbox(IAsyncResult result);

		// Token: 0x0600540F RID: 21519
		[OperationContract(ReplyAction = "*", AsyncPattern = true, Name = "ResetUMMailbox")]
		[XmlSerializerFormat]
		IAsyncResult BeginResetUMMailbox(ResetUMMailboxSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005410 RID: 21520
		ResetUMMailboxSoapResponse EndResetUMMailbox(IAsyncResult result);

		// Token: 0x06005411 RID: 21521
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true, Name = "ValidateUMPin")]
		IAsyncResult BeginValidateUMPin(ValidateUMPinSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005412 RID: 21522
		ValidateUMPinSoapResponse EndValidateUMPin(IAsyncResult result);

		// Token: 0x06005413 RID: 21523
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true, Name = "SaveUMPin")]
		IAsyncResult BeginSaveUMPin(SaveUMPinSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005414 RID: 21524
		SaveUMPinSoapResponse EndSaveUMPin(IAsyncResult result);

		// Token: 0x06005415 RID: 21525
		[OperationContract(ReplyAction = "*", AsyncPattern = true, Name = "GetUMPin")]
		[XmlSerializerFormat]
		IAsyncResult BeginGetUMPin(GetUMPinSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005416 RID: 21526
		GetUMPinSoapResponse EndGetUMPin(IAsyncResult result);

		// Token: 0x06005417 RID: 21527
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true, Name = "GetClientIntent")]
		IAsyncResult BeginGetClientIntent(GetClientIntentSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005418 RID: 21528
		GetClientIntentSoapResponse EndGetClientIntent(IAsyncResult result);

		// Token: 0x06005419 RID: 21529
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true, Name = "GetUMSubscriberCallAnsweringData")]
		IAsyncResult BeginGetUMSubscriberCallAnsweringData(GetUMSubscriberCallAnsweringDataSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600541A RID: 21530
		GetUMSubscriberCallAnsweringDataSoapResponse EndGetUMSubscriberCallAnsweringData(IAsyncResult result);

		// Token: 0x0600541B RID: 21531
		[OperationContract(ReplyAction = "*", AsyncPattern = true, Name = "UpdateMailboxAssociation")]
		[XmlSerializerFormat]
		IAsyncResult BeginUpdateMailboxAssociation(UpdateMailboxAssociationSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600541C RID: 21532
		UpdateMailboxAssociationSoapResponse EndUpdateMailboxAssociation(IAsyncResult result);

		// Token: 0x0600541D RID: 21533
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true, Name = "UpdateGroupMailbox")]
		IAsyncResult BeginUpdateGroupMailbox(UpdateGroupMailboxSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600541E RID: 21534
		UpdateGroupMailboxSoapResponse EndUpdateGroupMailbox(IAsyncResult result);

		// Token: 0x0600541F RID: 21535
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true, Name = "PostModernGroupItem")]
		IAsyncResult BeginPostModernGroupItem(PostModernGroupItemSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005420 RID: 21536
		PostModernGroupItemSoapResponse EndPostModernGroupItem(IAsyncResult result);

		// Token: 0x06005421 RID: 21537
		[OperationContract(ReplyAction = "*", AsyncPattern = true, Name = "PerformInstantSearch")]
		[XmlSerializerFormat]
		IAsyncResult BeginPerformInstantSearch(PerformInstantSearchSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005422 RID: 21538
		PerformInstantSearchSoapResponse EndPerformInstantSearch(IAsyncResult result);

		// Token: 0x06005423 RID: 21539
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true, Name = "EndInstantSearchSession")]
		IAsyncResult BeginEndInstantSearchSession(EndInstantSearchSessionSoapRequest soapRequest, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005424 RID: 21540
		EndInstantSearchSessionSoapResponse EndEndInstantSearchSession(IAsyncResult result);

		// Token: 0x06005425 RID: 21541
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "*", AsyncPattern = true, Name = "GetUserUnifiedGroups")]
		IAsyncResult BeginGetUserUnifiedGroups(GetUserUnifiedGroupsSoapRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005426 RID: 21542
		GetUserUnifiedGroupsSoapResponse EndGetUserUnifiedGroups(IAsyncResult result);

		// Token: 0x06005427 RID: 21543
		[OperationContract(ReplyAction = "*", AsyncPattern = true, Name = "GetClutterState")]
		[XmlSerializerFormat]
		IAsyncResult BeginGetClutterState(GetClutterStateSoapRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06005428 RID: 21544
		GetClutterStateSoapResponse EndGetClutterState(IAsyncResult result);

		// Token: 0x06005429 RID: 21545
		[OperationContract(ReplyAction = "*", AsyncPattern = true, Name = "SetClutterState")]
		[XmlSerializerFormat]
		IAsyncResult BeginSetClutterState(SetClutterStateSoapRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x0600542A RID: 21546
		SetClutterStateSoapResponse EndSetClutterState(IAsyncResult result);
	}
}
