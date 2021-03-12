using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Availability;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.ExchangeService
{
	// Token: 0x02000DDC RID: 3548
	internal interface IExchangeService : IDisposable
	{
		// Token: 0x06005AB2 RID: 23218
		AddAggregatedAccountResponse AddAggregatedAccount(AddAggregatedAccountRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AB3 RID: 23219
		Task<AddAggregatedAccountResponse> AddAggregatedAccountAsync(AddAggregatedAccountRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AB4 RID: 23220
		IsOffice365DomainResponse IsOffice365Domain(IsOffice365DomainRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AB5 RID: 23221
		Task<IsOffice365DomainResponse> IsOffice365DomainAsync(IsOffice365DomainRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AB6 RID: 23222
		GetAggregatedAccountResponse GetAggregatedAccount(GetAggregatedAccountRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AB7 RID: 23223
		Task<GetAggregatedAccountResponse> GetAggregatedAccountAsync(GetAggregatedAccountRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AB8 RID: 23224
		RemoveAggregatedAccountResponse RemoveAggregatedAccount(RemoveAggregatedAccountRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AB9 RID: 23225
		Task<RemoveAggregatedAccountResponse> RemoveAggregatedAccountAsync(RemoveAggregatedAccountRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005ABA RID: 23226
		SetAggregatedAccountResponse SetAggregatedAccount(SetAggregatedAccountRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005ABB RID: 23227
		Task<SetAggregatedAccountResponse> SetAggregatedAccountAsync(SetAggregatedAccountRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005ABC RID: 23228
		CreateUnifiedMailboxResponse CreateUnifiedMailbox(CreateUnifiedMailboxRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005ABD RID: 23229
		Task<CreateUnifiedMailboxResponse> CreateUnifiedMailboxAsync(CreateUnifiedMailboxRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005ABE RID: 23230
		GetFolderResponse GetFolder(GetFolderRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005ABF RID: 23231
		Task<GetFolderResponse> GetFolderAsync(GetFolderRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AC0 RID: 23232
		FindFolderResponse FindFolder(FindFolderRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AC1 RID: 23233
		Task<FindFolderResponse> FindFolderAsync(FindFolderRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AC2 RID: 23234
		CreateFolderResponse CreateFolder(CreateFolderRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AC3 RID: 23235
		Task<CreateFolderResponse> CreateFolderAsync(CreateFolderRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AC4 RID: 23236
		DeleteFolderResponse DeleteFolder(DeleteFolderRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AC5 RID: 23237
		Task<DeleteFolderResponse> DeleteFolderAsync(DeleteFolderRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AC6 RID: 23238
		UpdateFolderResponse UpdateFolder(UpdateFolderRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AC7 RID: 23239
		Task<UpdateFolderResponse> UpdateFolderAsync(UpdateFolderRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AC8 RID: 23240
		CopyFolderResponse CopyFolder(CopyFolderRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AC9 RID: 23241
		Task<CopyFolderResponse> CopyFolderAsync(CopyFolderRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005ACA RID: 23242
		MoveFolderResponse MoveFolder(MoveFolderRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005ACB RID: 23243
		Task<MoveFolderResponse> MoveFolderAsync(MoveFolderRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005ACC RID: 23244
		GetFavoritesResponse GetFavoriteFolders(ExecutionOption executionOption = null);

		// Token: 0x06005ACD RID: 23245
		Task<GetFavoritesResponse> GetFavoriteFoldersAsync(ExecutionOption executionOption = null);

		// Token: 0x06005ACE RID: 23246
		UpdateFavoriteFolderResponse UpdateFavoriteFolder(UpdateFavoriteFolderRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005ACF RID: 23247
		Task<UpdateFavoriteFolderResponse> UpdateFavoriteFolderAsync(UpdateFavoriteFolderRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AD0 RID: 23248
		GetItemResponse GetItem(GetItemRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AD1 RID: 23249
		Task<GetItemResponse> GetItemAsync(GetItemRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AD2 RID: 23250
		FindItemResponse FindItem(FindItemRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AD3 RID: 23251
		Task<FindItemResponse> FindItemAsync(FindItemRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AD4 RID: 23252
		IDisposableResponse<CreateItemResponse> CreateItem(CreateItemRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AD5 RID: 23253
		Task<IDisposableResponse<CreateItemResponse>> CreateItemAsync(CreateItemRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AD6 RID: 23254
		DeleteItemResponse DeleteItem(DeleteItemRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AD7 RID: 23255
		Task<DeleteItemResponse> DeleteItemAsync(DeleteItemRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AD8 RID: 23256
		CopyItemResponse CopyItem(CopyItemRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AD9 RID: 23257
		Task<CopyItemResponse> CopyItemAsync(CopyItemRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005ADA RID: 23258
		MoveItemResponse MoveItem(MoveItemRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005ADB RID: 23259
		Task<MoveItemResponse> MoveItemAsync(MoveItemRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005ADC RID: 23260
		SendItemResponse SendItem(SendItemRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005ADD RID: 23261
		Task<SendItemResponse> SendItemAsync(SendItemRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005ADE RID: 23262
		GetConversationItemsResponse GetConversationItems(GetConversationItemsRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005ADF RID: 23263
		Task<GetThreadedConversationItemsResponse> GetThreadedConversationItemsAsync(GetThreadedConversationItemsRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AE0 RID: 23264
		GetThreadedConversationItemsResponse GetThreadedConversationItems(GetThreadedConversationItemsRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AE1 RID: 23265
		Task<GetConversationItemsResponse> GetConversationItemsAsync(GetConversationItemsRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AE2 RID: 23266
		FindConversationResponseMessage FindConversation(FindConversationRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AE3 RID: 23267
		Task<FindConversationResponseMessage> FindConversationAsync(FindConversationRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AE4 RID: 23268
		SyncFolderHierarchyResponse SyncFolderHierarchy(SyncFolderHierarchyRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AE5 RID: 23269
		Task<SyncFolderHierarchyResponse> SyncFolderHierarchyAsync(SyncFolderHierarchyRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AE6 RID: 23270
		SyncFolderItemsResponse SyncFolderItems(SyncFolderItemsRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AE7 RID: 23271
		Task<SyncFolderItemsResponse> SyncFolderItemsAsync(SyncFolderItemsRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AE8 RID: 23272
		SyncCalendarResponse SyncCalendar(SyncCalendarParameters request, ExecutionOption executionOption = null);

		// Token: 0x06005AE9 RID: 23273
		Task<SyncCalendarResponse> SyncCalendarAsync(SyncCalendarParameters request, ExecutionOption executionOption = null);

		// Token: 0x06005AEA RID: 23274
		SyncConversationResponseMessage SyncConversation(SyncConversationRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AEB RID: 23275
		Task<SyncConversationResponseMessage> SyncConversationAsync(SyncConversationRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AEC RID: 23276
		GetModernGroupResponse GetModernGroup(GetModernGroupRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AED RID: 23277
		Task<GetModernGroupResponse> GetModernGroupAsync(GetModernGroupRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AEE RID: 23278
		UpdateItemResponse UpdateItem(UpdateItemRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AEF RID: 23279
		Task<UpdateItemResponse> UpdateItemAsync(UpdateItemRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AF0 RID: 23280
		GetUserPhotoResponse GetUserPhoto(GetUserPhotoRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AF1 RID: 23281
		Task<GetUserPhotoResponse> GetUserPhotoAsync(GetUserPhotoRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AF2 RID: 23282
		GetPeopleICommunicateWithResponse GetPeopleICommunicateWith(GetPeopleICommunicateWithRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AF3 RID: 23283
		Task<GetPeopleICommunicateWithResponse> GetPeopleICommunicateWithAsync(GetPeopleICommunicateWithRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AF4 RID: 23284
		ResolveNamesResponse ResolveNames(ResolveNamesRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AF5 RID: 23285
		Task<ResolveNamesResponse> ResolveNamesAsync(ResolveNamesRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AF6 RID: 23286
		ApplyConversationActionResponse ApplyConversationAction(ApplyConversationActionRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AF7 RID: 23287
		Task<ApplyConversationActionResponse> ApplyConversationActionAsync(ApplyConversationActionRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AF8 RID: 23288
		GetCalendarEventResponse GetCalendarEvent(GetCalendarEventRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AF9 RID: 23289
		Task<GetCalendarEventResponse> GetCalendarEventAsync(GetCalendarEventRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AFA RID: 23290
		GetCalendarViewResponse GetCalendarView(GetCalendarViewRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AFB RID: 23291
		Task<GetCalendarViewResponse> GetCalendarViewAsync(GetCalendarViewRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AFC RID: 23292
		CancelCalendarEventResponse CancelCalendarEvent(CancelCalendarEventRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AFD RID: 23293
		Task<CancelCalendarEventResponse> CancelCalendarEventAsync(CancelCalendarEventRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AFE RID: 23294
		CreateCalendarEventResponse CreateCalendarEvent(CreateCalendarEventRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005AFF RID: 23295
		Task<CreateCalendarEventResponse> CreateCalendarEventAsync(CreateCalendarEventRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B00 RID: 23296
		RespondToCalendarEventResponse RespondToCalendarEvent(RespondToCalendarEventRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B01 RID: 23297
		Task<RespondToCalendarEventResponse> RespondToCalendarEventAsync(RespondToCalendarEventRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B02 RID: 23298
		ExpandCalendarEventResponse ExpandCalendarEvent(ExpandCalendarEventRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B03 RID: 23299
		Task<ExpandCalendarEventResponse> ExpandCalendarEventAsync(ExpandCalendarEventRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B04 RID: 23300
		RefreshGALContactsFolderResponse RefreshGALContactsFolder(RefreshGALContactsFolderRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B05 RID: 23301
		Task<RefreshGALContactsFolderResponse> RefreshGALContactsFolderAsync(RefreshGALContactsFolderRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B06 RID: 23302
		UpdateCalendarEventResponse UpdateCalendarEvent(UpdateCalendarEventRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B07 RID: 23303
		Task<UpdateCalendarEventResponse> UpdateCalendarEventAsync(UpdateCalendarEventRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B08 RID: 23304
		DeleteCalendarEventResponse DeleteCalendarEvent(DeleteCalendarEventRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B09 RID: 23305
		Task<DeleteCalendarEventResponse> DeleteCalendarEventAsync(DeleteCalendarEventRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B0A RID: 23306
		ForwardCalendarEventResponse ForwardCalendarEvent(ForwardCalendarEventRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B0B RID: 23307
		Task<ForwardCalendarEventResponse> ForwardCalendarEventAsync(ForwardCalendarEventRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B0C RID: 23308
		IDisposableResponse<GetAttachmentResponse> GetAttachment(GetAttachmentRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B0D RID: 23309
		Task<IDisposableResponse<GetAttachmentResponse>> GetAttachmentAsync(GetAttachmentRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B0E RID: 23310
		IDisposableResponse<CreateAttachmentResponse> CreateAttachment(CreateAttachmentRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B0F RID: 23311
		Task<IDisposableResponse<CreateAttachmentResponse>> CreateAttachmentAsync(CreateAttachmentRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B10 RID: 23312
		DeleteAttachmentResponse DeleteAttachment(DeleteAttachmentRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B11 RID: 23313
		Task<DeleteAttachmentResponse> DeleteAttachmentAsync(DeleteAttachmentRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B12 RID: 23314
		ProvisionResponse Provision(ProvisionRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B13 RID: 23315
		Task<ProvisionResponse> ProvisionAsync(ProvisionRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B14 RID: 23316
		FindPeopleResponseMessage FindPeople(FindPeopleRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B15 RID: 23317
		Task<FindPeopleResponseMessage> FindPeopleAsync(FindPeopleRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B16 RID: 23318
		SyncAutoCompleteRecipientsResponseMessage SyncAutoCompleteRecipients(SyncAutoCompleteRecipientsRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B17 RID: 23319
		Task<SyncAutoCompleteRecipientsResponseMessage> SyncAutoCompleteRecipientsAsync(SyncAutoCompleteRecipientsRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B18 RID: 23320
		GetPersonaResponseMessage GetPersona(GetPersonaRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B19 RID: 23321
		Task<GetPersonaResponseMessage> GetPersonaAsync(GetPersonaRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B1A RID: 23322
		ConversationFeedLoader GetConversationFeedLoader(ExTimeZone timezone);

		// Token: 0x06005B1B RID: 23323
		Guid GetMailboxGuid();

		// Token: 0x06005B1C RID: 23324
		Guid GetConversationGuidFromEwsId(string ewsId);

		// Token: 0x06005B1D RID: 23325
		GetComplianceConfigurationResponseMessage GetComplianceConfiguration(GetComplianceConfigurationRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B1E RID: 23326
		Guid GetMailboxGuidFromEwsId(string ewsId);

		// Token: 0x06005B1F RID: 23327
		string GetEwsIdFromConversationGuid(Guid conversationGuid, Guid mailboxGuid);

		// Token: 0x06005B20 RID: 23328
		void GetItemMidFidDateFromEwsId(string ewsId, out long mid, out long fid, out ExDateTime date);

		// Token: 0x06005B21 RID: 23329
		string GetEwsIdFromItemMidFidDate(long mid, long fid, ExDateTime date, Guid mailboxGuid);

		// Token: 0x06005B22 RID: 23330
		bool GetFolderFidAndMailboxFromEwsId(string ewsId, out long fid, out Guid mailboxGuid);

		// Token: 0x06005B23 RID: 23331
		long GetFolderFidFromEwsId(string ewsId);

		// Token: 0x06005B24 RID: 23332
		string GetEwsIdFromFolderFid(long fid, Guid mailboxGuid);

		// Token: 0x06005B25 RID: 23333
		string GetDistinguishedFolderIdFromEwsId(string ewsId);

		// Token: 0x06005B26 RID: 23334
		Guid GetAttachmentGuidFromEwsId(string ewsId, long mid, long fid);

		// Token: 0x06005B27 RID: 23335
		string GetEwsIdFromAttachmentGuid(Guid attachmentGuid, long mid, long fid, Guid mailboxGuid);

		// Token: 0x06005B28 RID: 23336
		SubscribeToConversationChangesResponseMessage SubscribeToConversationChanges(SubscribeToConversationChangesRequest request, Action<ConversationNotification> callback, ExecutionOption executionOption = null);

		// Token: 0x06005B29 RID: 23337
		Task<SubscribeToConversationChangesResponseMessage> SubscribeToConversationChangesAsync(SubscribeToConversationChangesRequest request, Action<ConversationNotification> callback, ExecutionOption executionOption = null);

		// Token: 0x06005B2A RID: 23338
		void SetCallContextFromActionInfo(string actionQueueId, string protocol, string deviceType, string actionId, bool IsOutlookService);

		// Token: 0x06005B2B RID: 23339
		void DisableDupDetection();

		// Token: 0x06005B2C RID: 23340
		bool TryGetResponse<T>(out T results);

		// Token: 0x06005B2D RID: 23341
		void SetResponse<T>(T result, Exception exception);

		// Token: 0x06005B2E RID: 23342
		bool? GetIsDuplicatedAction();

		// Token: 0x06005B2F RID: 23343
		bool GetReturningSavedResult();

		// Token: 0x06005B30 RID: 23344
		bool GetResultSaved();

		// Token: 0x06005B31 RID: 23345
		QuotedTextResult ParseQuotedText(string msg, bool reorderMsgs);

		// Token: 0x06005B32 RID: 23346
		IOutlookServiceStorage GetOutlookServiceStorage();

		// Token: 0x06005B33 RID: 23347
		SubscribeToCalendarChangesResponseMessage SubscribeToCalendarChanges(SubscribeToCalendarChangesRequest request, Action<CalendarChangeNotificationType> callback, ExecutionOption executionOption = null);

		// Token: 0x06005B34 RID: 23348
		Task<SubscribeToCalendarChangesResponseMessage> SubscribeToCalendarChangesAsync(SubscribeToCalendarChangesRequest request, Action<CalendarChangeNotificationType> callback, ExecutionOption executionOption = null);

		// Token: 0x06005B35 RID: 23349
		GetCalendarFoldersResponse GetCalendarFolders(ExecutionOption executionOption = null);

		// Token: 0x06005B36 RID: 23350
		IHtmlReader GetHtmlReader(string html);

		// Token: 0x06005B37 RID: 23351
		PerformInstantSearchResponse PerformInstantSearch(PerformInstantSearchRequest request, Action<InstantSearchPayloadType> searchPayloadCallback, ExecutionOption executionOption = null);

		// Token: 0x06005B38 RID: 23352
		Task<PerformInstantSearchResponse> PerformInstantSearchAsync(PerformInstantSearchRequest request, Action<InstantSearchPayloadType> searchPayloadCallback, ExecutionOption executionOption = null);

		// Token: 0x06005B39 RID: 23353
		EndInstantSearchSessionResponse EndInstantSearchSession(string deviceId, string sessionId, ExecutionOption executionOption = null);

		// Token: 0x06005B3A RID: 23354
		Task<EndInstantSearchSessionResponse> EndInstantSearchSessionAsync(string deviceId, string sessionId, ExecutionOption executionOption = null);

		// Token: 0x06005B3B RID: 23355
		string GetBodyWithQuotedText(string messageBody, List<string> messageIds, List<string> messageBodyColl);

		// Token: 0x06005B3C RID: 23356
		MasterCategoryListActionResponse GetMasterCategoryList(GetMasterCategoryListRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B3D RID: 23357
		GetUserAvailabilityResponse GetUserAvailability(GetUserAvailabilityRequest request, ExecutionOption executionOption = null);

		// Token: 0x06005B3E RID: 23358
		SubscribeToHierarchyChangesResponseMessage SubscribeToHierarchyChanges(SubscribeToHierarchyChangesRequest request, Action<HierarchyNotification> callback, ExecutionOption executionOption = null);

		// Token: 0x06005B3F RID: 23359
		Task<SubscribeToHierarchyChangesResponseMessage> SubscribeToHierarchyChangesAsync(SubscribeToHierarchyChangesRequest request, Action<HierarchyNotification> callback, ExecutionOption executionOption = null);

		// Token: 0x06005B40 RID: 23360
		SubscribeToMessageChangesResponseMessage SubscribeToMessageChanges(SubscribeToMessageChangesRequest request, Action<MessageNotification> callback, ExecutionOption executionOption = null);

		// Token: 0x06005B41 RID: 23361
		Task<SubscribeToMessageChangesResponseMessage> SubscribeToMessageChangesAsync(SubscribeToMessageChangesRequest request, Action<MessageNotification> callback, ExecutionOption executionOption = null);

		// Token: 0x06005B42 RID: 23362
		void SetRequestTimeZoneId(string timeZoneId);

		// Token: 0x06005B43 RID: 23363
		ExDateTime GetOriginalStartDateFromEwsId(string ewsId);

		// Token: 0x06005B44 RID: 23364
		PatternedRecurrence ConvertToPatternedRecurrence(Recurrence value);
	}
}
