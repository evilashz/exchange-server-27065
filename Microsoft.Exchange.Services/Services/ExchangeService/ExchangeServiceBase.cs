using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters.Recurrence;
using Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Availability;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Conversations;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Services.ExchangeService
{
	// Token: 0x02000DE1 RID: 3553
	internal abstract class ExchangeServiceBase : DisposeTrackableBase, IExchangeService, IDisposable
	{
		// Token: 0x06005B55 RID: 23381 RVA: 0x0011A950 File Offset: 0x00118B50
		public AddAggregatedAccountResponse AddAggregatedAccount(AddAggregatedAccountRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<AddAggregatedAccountResponse>(() => new AddAggregatedAccount(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B56 RID: 23382 RVA: 0x0011A9A4 File Offset: 0x00118BA4
		public Task<AddAggregatedAccountResponse> AddAggregatedAccountAsync(AddAggregatedAccountRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<AddAggregatedAccountResponse>(() => new AddAggregatedAccount(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B57 RID: 23383 RVA: 0x0011A9F8 File Offset: 0x00118BF8
		public IsOffice365DomainResponse IsOffice365Domain(IsOffice365DomainRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<IsOffice365DomainResponse>(() => new IsOffice365Domain(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B58 RID: 23384 RVA: 0x0011AA4C File Offset: 0x00118C4C
		public Task<IsOffice365DomainResponse> IsOffice365DomainAsync(IsOffice365DomainRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<IsOffice365DomainResponse>(() => new IsOffice365Domain(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B59 RID: 23385 RVA: 0x0011AAA0 File Offset: 0x00118CA0
		public GetAggregatedAccountResponse GetAggregatedAccount(GetAggregatedAccountRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<GetAggregatedAccountResponse>(() => new GetAggregatedAccount(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B5A RID: 23386 RVA: 0x0011AAF4 File Offset: 0x00118CF4
		public Task<GetAggregatedAccountResponse> GetAggregatedAccountAsync(GetAggregatedAccountRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<GetAggregatedAccountResponse>(() => new GetAggregatedAccount(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B5B RID: 23387 RVA: 0x0011AB48 File Offset: 0x00118D48
		public RemoveAggregatedAccountResponse RemoveAggregatedAccount(RemoveAggregatedAccountRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<RemoveAggregatedAccountResponse>(() => new RemoveAggregatedAccount(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B5C RID: 23388 RVA: 0x0011AB9C File Offset: 0x00118D9C
		public Task<RemoveAggregatedAccountResponse> RemoveAggregatedAccountAsync(RemoveAggregatedAccountRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<RemoveAggregatedAccountResponse>(() => new RemoveAggregatedAccount(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B5D RID: 23389 RVA: 0x0011ABF0 File Offset: 0x00118DF0
		public SetAggregatedAccountResponse SetAggregatedAccount(SetAggregatedAccountRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<SetAggregatedAccountResponse>(() => new SetAggregatedAccount(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B5E RID: 23390 RVA: 0x0011AC44 File Offset: 0x00118E44
		public Task<SetAggregatedAccountResponse> SetAggregatedAccountAsync(SetAggregatedAccountRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<SetAggregatedAccountResponse>(() => new SetAggregatedAccount(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B5F RID: 23391 RVA: 0x0011AC98 File Offset: 0x00118E98
		public CreateUnifiedMailboxResponse CreateUnifiedMailbox(CreateUnifiedMailboxRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<CreateUnifiedMailboxResponse>(() => new CreateUnifiedMailbox(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B60 RID: 23392 RVA: 0x0011ACEC File Offset: 0x00118EEC
		public Task<CreateUnifiedMailboxResponse> CreateUnifiedMailboxAsync(CreateUnifiedMailboxRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<CreateUnifiedMailboxResponse>(() => new CreateUnifiedMailbox(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B61 RID: 23393 RVA: 0x0011AD40 File Offset: 0x00118F40
		public GetFolderResponse GetFolder(GetFolderRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<GetFolderResponse>(() => new GetFolder(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B62 RID: 23394 RVA: 0x0011AD94 File Offset: 0x00118F94
		public Task<GetFolderResponse> GetFolderAsync(GetFolderRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<GetFolderResponse>(() => new GetFolder(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B63 RID: 23395 RVA: 0x0011ADE8 File Offset: 0x00118FE8
		public FindFolderResponse FindFolder(FindFolderRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<FindFolderResponse>(() => new FindFolder(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B64 RID: 23396 RVA: 0x0011AE3C File Offset: 0x0011903C
		public Task<FindFolderResponse> FindFolderAsync(FindFolderRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<FindFolderResponse>(() => new FindFolder(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B65 RID: 23397 RVA: 0x0011AE90 File Offset: 0x00119090
		public CreateFolderResponse CreateFolder(CreateFolderRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<CreateFolderResponse>(() => new CreateFolder(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B66 RID: 23398 RVA: 0x0011AEE4 File Offset: 0x001190E4
		public Task<CreateFolderResponse> CreateFolderAsync(CreateFolderRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<CreateFolderResponse>(() => new CreateFolder(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B67 RID: 23399 RVA: 0x0011AF38 File Offset: 0x00119138
		public DeleteFolderResponse DeleteFolder(DeleteFolderRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<DeleteFolderResponse>(() => new DeleteFolder(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B68 RID: 23400 RVA: 0x0011AF8C File Offset: 0x0011918C
		public Task<DeleteFolderResponse> DeleteFolderAsync(DeleteFolderRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<DeleteFolderResponse>(() => new DeleteFolder(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B69 RID: 23401 RVA: 0x0011AFE0 File Offset: 0x001191E0
		public UpdateFolderResponse UpdateFolder(UpdateFolderRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<UpdateFolderResponse>(() => new UpdateFolder(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B6A RID: 23402 RVA: 0x0011B034 File Offset: 0x00119234
		public Task<UpdateFolderResponse> UpdateFolderAsync(UpdateFolderRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<UpdateFolderResponse>(() => new UpdateFolder(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B6B RID: 23403 RVA: 0x0011B088 File Offset: 0x00119288
		public CopyFolderResponse CopyFolder(CopyFolderRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<CopyFolderResponse>(() => new CopyFolder(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B6C RID: 23404 RVA: 0x0011B0DC File Offset: 0x001192DC
		public Task<CopyFolderResponse> CopyFolderAsync(CopyFolderRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<CopyFolderResponse>(() => new CopyFolder(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B6D RID: 23405 RVA: 0x0011B130 File Offset: 0x00119330
		public MoveFolderResponse MoveFolder(MoveFolderRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<MoveFolderResponse>(() => new MoveFolder(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B6E RID: 23406 RVA: 0x0011B184 File Offset: 0x00119384
		public Task<MoveFolderResponse> MoveFolderAsync(MoveFolderRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<MoveFolderResponse>(() => new MoveFolder(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B6F RID: 23407 RVA: 0x0011B1C5 File Offset: 0x001193C5
		public GetFavoritesResponse GetFavoriteFolders(ExecutionOption executionOption = null)
		{
			return this.InvokeOwsServiceCommand<GetFavoritesResponse>(() => new GetFavorites(this.CallContext), executionOption, true);
		}

		// Token: 0x06005B70 RID: 23408 RVA: 0x0011B1E8 File Offset: 0x001193E8
		public Task<GetFavoritesResponse> GetFavoriteFoldersAsync(ExecutionOption executionOption = null)
		{
			return this.InvokeOwsServiceCommandAsync<GetFavoritesResponse>(() => new GetFavorites(this.CallContext), executionOption, true);
		}

		// Token: 0x06005B71 RID: 23409 RVA: 0x0011B220 File Offset: 0x00119420
		public UpdateFavoriteFolderResponse UpdateFavoriteFolder(UpdateFavoriteFolderRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeOwsServiceCommand<UpdateFavoriteFolderResponse>(() => new UpdateFavoriteFolder(this.CallContext, request), executionOption, true);
		}

		// Token: 0x06005B72 RID: 23410 RVA: 0x0011B278 File Offset: 0x00119478
		public Task<UpdateFavoriteFolderResponse> UpdateFavoriteFolderAsync(UpdateFavoriteFolderRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeOwsServiceCommandAsync<UpdateFavoriteFolderResponse>(() => new UpdateFavoriteFolder(this.CallContext, request), executionOption, true);
		}

		// Token: 0x06005B73 RID: 23411 RVA: 0x0011B2D0 File Offset: 0x001194D0
		public GetItemResponse GetItem(GetItemRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<GetItemResponse>(() => new GetItem(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B74 RID: 23412 RVA: 0x0011B324 File Offset: 0x00119524
		public Task<GetItemResponse> GetItemAsync(GetItemRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<GetItemResponse>(() => new GetItem(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B75 RID: 23413 RVA: 0x0011B378 File Offset: 0x00119578
		public FindItemResponse FindItem(FindItemRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<FindItemResponse>(() => new FindItem(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B76 RID: 23414 RVA: 0x0011B3CC File Offset: 0x001195CC
		public Task<FindItemResponse> FindItemAsync(FindItemRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<FindItemResponse>(() => new FindItem(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B77 RID: 23415 RVA: 0x0011B41C File Offset: 0x0011961C
		public IDisposableResponse<CreateItemResponse> CreateItem(CreateItemRequest request, ExecutionOption executionOption = null)
		{
			DisposableResponse<CreateItemResponse> response = null;
			try
			{
				response = new DisposableResponse<CreateItemResponse>(new CreateItem(this.CallContext, request), null);
				response.Response = this.InvokeServiceCommand<CreateItemResponse>(() => (CreateItem)response.Command, executionOption);
			}
			catch (Exception)
			{
				if (response != null)
				{
					response.Dispose();
					response = null;
				}
				throw;
			}
			return response;
		}

		// Token: 0x06005B78 RID: 23416 RVA: 0x0011B4CC File Offset: 0x001196CC
		public Task<IDisposableResponse<CreateItemResponse>> CreateItemAsync(CreateItemRequest request, ExecutionOption executionOption = null)
		{
			return Task<IDisposableResponse<CreateItemResponse>>.Factory.StartNew(() => this.CreateItem(request, executionOption));
		}

		// Token: 0x06005B79 RID: 23417 RVA: 0x0011B52C File Offset: 0x0011972C
		public DeleteItemResponse DeleteItem(DeleteItemRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<DeleteItemResponse>(() => new DeleteItem(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B7A RID: 23418 RVA: 0x0011B580 File Offset: 0x00119780
		public Task<DeleteItemResponse> DeleteItemAsync(DeleteItemRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<DeleteItemResponse>(() => new DeleteItem(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B7B RID: 23419 RVA: 0x0011B5D4 File Offset: 0x001197D4
		public CopyItemResponse CopyItem(CopyItemRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<CopyItemResponse>(() => new CopyItem(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B7C RID: 23420 RVA: 0x0011B628 File Offset: 0x00119828
		public Task<CopyItemResponse> CopyItemAsync(CopyItemRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<CopyItemResponse>(() => new CopyItem(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B7D RID: 23421 RVA: 0x0011B67C File Offset: 0x0011987C
		public MoveItemResponse MoveItem(MoveItemRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<MoveItemResponse>(() => new MoveItem(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B7E RID: 23422 RVA: 0x0011B6D0 File Offset: 0x001198D0
		public Task<MoveItemResponse> MoveItemAsync(MoveItemRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<MoveItemResponse>(() => new MoveItem(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B7F RID: 23423 RVA: 0x0011B724 File Offset: 0x00119924
		public SendItemResponse SendItem(SendItemRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<SendItemResponse>(() => new SendItem(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B80 RID: 23424 RVA: 0x0011B778 File Offset: 0x00119978
		public Task<SendItemResponse> SendItemAsync(SendItemRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<SendItemResponse>(() => new SendItem(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B81 RID: 23425 RVA: 0x0011B7CC File Offset: 0x001199CC
		public GetConversationItemsResponse GetConversationItems(GetConversationItemsRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<GetConversationItemsResponse>(() => new GetModernConversationItems(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B82 RID: 23426 RVA: 0x0011B820 File Offset: 0x00119A20
		public Task<GetConversationItemsResponse> GetConversationItemsAsync(GetConversationItemsRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<GetConversationItemsResponse>(() => new GetModernConversationItems(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B83 RID: 23427 RVA: 0x0011B874 File Offset: 0x00119A74
		public GetThreadedConversationItemsResponse GetThreadedConversationItems(GetThreadedConversationItemsRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<GetThreadedConversationItemsResponse>(() => new GetThreadedConversationItems(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B84 RID: 23428 RVA: 0x0011B8C8 File Offset: 0x00119AC8
		public Task<GetThreadedConversationItemsResponse> GetThreadedConversationItemsAsync(GetThreadedConversationItemsRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<GetThreadedConversationItemsResponse>(() => new GetThreadedConversationItems(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B85 RID: 23429 RVA: 0x0011B91C File Offset: 0x00119B1C
		public FindConversationResponseMessage FindConversation(FindConversationRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<FindConversationResponseMessage>(() => new FindConversation(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B86 RID: 23430 RVA: 0x0011B970 File Offset: 0x00119B70
		public Task<FindConversationResponseMessage> FindConversationAsync(FindConversationRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<FindConversationResponseMessage>(() => new FindConversation(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B87 RID: 23431 RVA: 0x0011B9C4 File Offset: 0x00119BC4
		public SyncCalendarResponse SyncCalendar(SyncCalendarParameters request, ExecutionOption executionOption = null)
		{
			return this.InvokeOwsServiceCommand<SyncCalendarResponse>(() => new SyncCalendar(this.CallContext, request), executionOption, false);
		}

		// Token: 0x06005B88 RID: 23432 RVA: 0x0011BA1C File Offset: 0x00119C1C
		public Task<SyncCalendarResponse> SyncCalendarAsync(SyncCalendarParameters request, ExecutionOption executionOption = null)
		{
			return this.InvokeOwsServiceCommandAsync<SyncCalendarResponse>(() => new SyncCalendar(this.CallContext, request), executionOption, false);
		}

		// Token: 0x06005B89 RID: 23433 RVA: 0x0011BA74 File Offset: 0x00119C74
		public SyncFolderHierarchyResponse SyncFolderHierarchy(SyncFolderHierarchyRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<SyncFolderHierarchyResponse>(() => new SyncFolderHierarchy(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B8A RID: 23434 RVA: 0x0011BAC8 File Offset: 0x00119CC8
		public Task<SyncFolderHierarchyResponse> SyncFolderHierarchyAsync(SyncFolderHierarchyRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<SyncFolderHierarchyResponse>(() => new SyncFolderHierarchy(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B8B RID: 23435 RVA: 0x0011BB1C File Offset: 0x00119D1C
		public SyncFolderItemsResponse SyncFolderItems(SyncFolderItemsRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<SyncFolderItemsResponse>(() => new SyncFolderItems(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B8C RID: 23436 RVA: 0x0011BB70 File Offset: 0x00119D70
		public Task<SyncFolderItemsResponse> SyncFolderItemsAsync(SyncFolderItemsRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<SyncFolderItemsResponse>(() => new SyncFolderItems(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B8D RID: 23437 RVA: 0x0011BBC4 File Offset: 0x00119DC4
		public SyncConversationResponseMessage SyncConversation(SyncConversationRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<SyncConversationResponseMessage>(() => new SyncConversation(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B8E RID: 23438 RVA: 0x0011BC18 File Offset: 0x00119E18
		public Task<SyncConversationResponseMessage> SyncConversationAsync(SyncConversationRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<SyncConversationResponseMessage>(() => new SyncConversation(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B8F RID: 23439 RVA: 0x0011BC6C File Offset: 0x00119E6C
		public GetPersonaModernGroupMembershipResponse GetPersonaModernGroupMembership(GetPersonaModernGroupMembershipRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeOwsServiceCommand<GetPersonaModernGroupMembershipResponse>(() => new GetPersonaModernGroupMembership(this.CallContext, request), executionOption, false);
		}

		// Token: 0x06005B90 RID: 23440 RVA: 0x0011BCC4 File Offset: 0x00119EC4
		public GetModernGroupResponse GetModernGroup(GetModernGroupRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeOwsServiceCommand<GetModernGroupResponse>(() => new GetModernGroup(this.CallContext, request), executionOption, false);
		}

		// Token: 0x06005B91 RID: 23441 RVA: 0x0011BD1C File Offset: 0x00119F1C
		public Task<GetModernGroupResponse> GetModernGroupAsync(GetModernGroupRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeOwsServiceCommandAsync<GetModernGroupResponse>(() => new GetModernGroup(this.CallContext, request), executionOption, false);
		}

		// Token: 0x06005B92 RID: 23442 RVA: 0x0011BD74 File Offset: 0x00119F74
		public UpdateItemResponse UpdateItem(UpdateItemRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<UpdateItemResponse>(() => new UpdateItem(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B93 RID: 23443 RVA: 0x0011BDC8 File Offset: 0x00119FC8
		public Task<UpdateItemResponse> UpdateItemAsync(UpdateItemRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<UpdateItemResponse>(() => new UpdateItem(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B94 RID: 23444 RVA: 0x0011BE24 File Offset: 0x0011A024
		public GetUserPhotoResponse GetUserPhoto(GetUserPhotoRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<GetUserPhotoResponse>(() => new GetUserPhoto(this.CallContext, request, NullTracer.Instance), executionOption);
		}

		// Token: 0x06005B95 RID: 23445 RVA: 0x0011BE80 File Offset: 0x0011A080
		public Task<GetUserPhotoResponse> GetUserPhotoAsync(GetUserPhotoRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<GetUserPhotoResponse>(() => new GetUserPhoto(this.CallContext, request, NullTracer.Instance), executionOption);
		}

		// Token: 0x06005B96 RID: 23446 RVA: 0x0011BED4 File Offset: 0x0011A0D4
		public GetPeopleICommunicateWithResponse GetPeopleICommunicateWith(GetPeopleICommunicateWithRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<GetPeopleICommunicateWithResponse>(() => new GetPeopleICommunicateWith(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B97 RID: 23447 RVA: 0x0011BF28 File Offset: 0x0011A128
		public Task<GetPeopleICommunicateWithResponse> GetPeopleICommunicateWithAsync(GetPeopleICommunicateWithRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<GetPeopleICommunicateWithResponse>(() => new GetPeopleICommunicateWith(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B98 RID: 23448 RVA: 0x0011BF7C File Offset: 0x0011A17C
		public ResolveNamesResponse ResolveNames(ResolveNamesRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<ResolveNamesResponse>(() => new ResolveNames(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B99 RID: 23449 RVA: 0x0011BFD0 File Offset: 0x0011A1D0
		public Task<ResolveNamesResponse> ResolveNamesAsync(ResolveNamesRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<ResolveNamesResponse>(() => new ResolveNames(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B9A RID: 23450 RVA: 0x0011C024 File Offset: 0x0011A224
		public ApplyConversationActionResponse ApplyConversationAction(ApplyConversationActionRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<ApplyConversationActionResponse>(() => new ApplyConversationAction(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B9B RID: 23451 RVA: 0x0011C078 File Offset: 0x0011A278
		public Task<ApplyConversationActionResponse> ApplyConversationActionAsync(ApplyConversationActionRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<ApplyConversationActionResponse>(() => new ApplyConversationAction(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B9C RID: 23452 RVA: 0x0011C0CC File Offset: 0x0011A2CC
		public GetCalendarEventResponse GetCalendarEvent(GetCalendarEventRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<GetCalendarEventResponse>(() => new GetCalendarEvent(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B9D RID: 23453 RVA: 0x0011C120 File Offset: 0x0011A320
		public Task<GetCalendarEventResponse> GetCalendarEventAsync(GetCalendarEventRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<GetCalendarEventResponse>(() => new GetCalendarEvent(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B9E RID: 23454 RVA: 0x0011C174 File Offset: 0x0011A374
		public GetCalendarViewResponse GetCalendarView(GetCalendarViewRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<GetCalendarViewResponse>(() => new GetCalendarView(this.CallContext, request), executionOption);
		}

		// Token: 0x06005B9F RID: 23455 RVA: 0x0011C1C8 File Offset: 0x0011A3C8
		public Task<GetCalendarViewResponse> GetCalendarViewAsync(GetCalendarViewRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<GetCalendarViewResponse>(() => new GetCalendarView(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BA0 RID: 23456 RVA: 0x0011C21C File Offset: 0x0011A41C
		public CancelCalendarEventResponse CancelCalendarEvent(CancelCalendarEventRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<CancelCalendarEventResponse>(() => new CancelCalendarEvent(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BA1 RID: 23457 RVA: 0x0011C270 File Offset: 0x0011A470
		public Task<CancelCalendarEventResponse> CancelCalendarEventAsync(CancelCalendarEventRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<CancelCalendarEventResponse>(() => new CancelCalendarEvent(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BA2 RID: 23458 RVA: 0x0011C2C4 File Offset: 0x0011A4C4
		public CreateCalendarEventResponse CreateCalendarEvent(CreateCalendarEventRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<CreateCalendarEventResponse>(() => new CreateCalendarEvent(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BA3 RID: 23459 RVA: 0x0011C318 File Offset: 0x0011A518
		public Task<CreateCalendarEventResponse> CreateCalendarEventAsync(CreateCalendarEventRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<CreateCalendarEventResponse>(() => new CreateCalendarEvent(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BA4 RID: 23460 RVA: 0x0011C36C File Offset: 0x0011A56C
		public RespondToCalendarEventResponse RespondToCalendarEvent(RespondToCalendarEventRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<RespondToCalendarEventResponse>(() => new RespondToCalendarEvent(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BA5 RID: 23461 RVA: 0x0011C3C0 File Offset: 0x0011A5C0
		public Task<RespondToCalendarEventResponse> RespondToCalendarEventAsync(RespondToCalendarEventRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<RespondToCalendarEventResponse>(() => new RespondToCalendarEvent(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BA6 RID: 23462 RVA: 0x0011C414 File Offset: 0x0011A614
		public ExpandCalendarEventResponse ExpandCalendarEvent(ExpandCalendarEventRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<ExpandCalendarEventResponse>(() => new ExpandCalendarEvent(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BA7 RID: 23463 RVA: 0x0011C468 File Offset: 0x0011A668
		public Task<ExpandCalendarEventResponse> ExpandCalendarEventAsync(ExpandCalendarEventRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<ExpandCalendarEventResponse>(() => new ExpandCalendarEvent(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BA8 RID: 23464 RVA: 0x0011C4BC File Offset: 0x0011A6BC
		public RefreshGALContactsFolderResponse RefreshGALContactsFolder(RefreshGALContactsFolderRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<RefreshGALContactsFolderResponse>(() => new RefreshGALContactsFolderCommand(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BA9 RID: 23465 RVA: 0x0011C510 File Offset: 0x0011A710
		public Task<RefreshGALContactsFolderResponse> RefreshGALContactsFolderAsync(RefreshGALContactsFolderRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<RefreshGALContactsFolderResponse>(() => new RefreshGALContactsFolderCommand(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BAA RID: 23466 RVA: 0x0011C564 File Offset: 0x0011A764
		public UpdateCalendarEventResponse UpdateCalendarEvent(UpdateCalendarEventRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<UpdateCalendarEventResponse>(() => new UpdateCalendarEvent(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BAB RID: 23467 RVA: 0x0011C5B8 File Offset: 0x0011A7B8
		public Task<UpdateCalendarEventResponse> UpdateCalendarEventAsync(UpdateCalendarEventRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<UpdateCalendarEventResponse>(() => new UpdateCalendarEvent(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BAC RID: 23468 RVA: 0x0011C60C File Offset: 0x0011A80C
		public DeleteCalendarEventResponse DeleteCalendarEvent(DeleteCalendarEventRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<DeleteCalendarEventResponse>(() => new DeleteCalendarEvent(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BAD RID: 23469 RVA: 0x0011C660 File Offset: 0x0011A860
		public Task<DeleteCalendarEventResponse> DeleteCalendarEventAsync(DeleteCalendarEventRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<DeleteCalendarEventResponse>(() => new DeleteCalendarEvent(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BAE RID: 23470 RVA: 0x0011C6B4 File Offset: 0x0011A8B4
		public ForwardCalendarEventResponse ForwardCalendarEvent(ForwardCalendarEventRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<ForwardCalendarEventResponse>(() => new ForwardCalendarEvent(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BAF RID: 23471 RVA: 0x0011C708 File Offset: 0x0011A908
		public Task<ForwardCalendarEventResponse> ForwardCalendarEventAsync(ForwardCalendarEventRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<ForwardCalendarEventResponse>(() => new ForwardCalendarEvent(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BB0 RID: 23472 RVA: 0x0011C760 File Offset: 0x0011A960
		public Task<IDisposableResponse<GetAttachmentResponse>> GetAttachmentAsync(GetAttachmentRequest request, ExecutionOption executionOption = null)
		{
			return Task<IDisposableResponse<GetAttachmentResponse>>.Factory.StartNew(() => this.GetAttachment(request, executionOption));
		}

		// Token: 0x06005BB1 RID: 23473 RVA: 0x0011C7B8 File Offset: 0x0011A9B8
		public IDisposableResponse<GetAttachmentResponse> GetAttachment(GetAttachmentRequest request, ExecutionOption executionOption = null)
		{
			DisposableResponse<GetAttachmentResponse> response = null;
			try
			{
				response = new DisposableResponse<GetAttachmentResponse>(new GetAttachment(this.CallContext, request), null);
				response.Response = this.InvokeServiceCommand<GetAttachmentResponse>(() => (GetAttachment)response.Command, executionOption);
			}
			catch (Exception)
			{
				if (response != null)
				{
					response.Dispose();
					response = null;
				}
				throw;
			}
			return response;
		}

		// Token: 0x06005BB2 RID: 23474 RVA: 0x0011C860 File Offset: 0x0011AA60
		public IDisposableResponse<CreateAttachmentResponse> CreateAttachment(CreateAttachmentRequest request, ExecutionOption executionOption = null)
		{
			DisposableResponse<CreateAttachmentResponse> response = null;
			try
			{
				response = new DisposableResponse<CreateAttachmentResponse>(new CreateAttachment(this.CallContext, request), null);
				response.Response = this.InvokeServiceCommand<CreateAttachmentResponse>(() => (CreateAttachment)response.Command, executionOption);
			}
			catch (Exception)
			{
				if (response != null)
				{
					response.Dispose();
					response = null;
				}
				throw;
			}
			return response;
		}

		// Token: 0x06005BB3 RID: 23475 RVA: 0x0011C910 File Offset: 0x0011AB10
		public Task<IDisposableResponse<CreateAttachmentResponse>> CreateAttachmentAsync(CreateAttachmentRequest request, ExecutionOption executionOption = null)
		{
			return Task<IDisposableResponse<CreateAttachmentResponse>>.Factory.StartNew(() => this.CreateAttachment(request, executionOption));
		}

		// Token: 0x06005BB4 RID: 23476 RVA: 0x0011C970 File Offset: 0x0011AB70
		public DeleteAttachmentResponse DeleteAttachment(DeleteAttachmentRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<DeleteAttachmentResponse>(() => new DeleteAttachment(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BB5 RID: 23477 RVA: 0x0011C9C4 File Offset: 0x0011ABC4
		public Task<DeleteAttachmentResponse> DeleteAttachmentAsync(DeleteAttachmentRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<DeleteAttachmentResponse>(() => new DeleteAttachment(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BB6 RID: 23478 RVA: 0x0011CA18 File Offset: 0x0011AC18
		public ProvisionResponse Provision(ProvisionRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<ProvisionResponse>(() => new Provision(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BB7 RID: 23479 RVA: 0x0011CA6C File Offset: 0x0011AC6C
		public Task<ProvisionResponse> ProvisionAsync(ProvisionRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<ProvisionResponse>(() => new Provision(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BB8 RID: 23480 RVA: 0x0011CAC0 File Offset: 0x0011ACC0
		public FindPeopleResponseMessage FindPeople(FindPeopleRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<FindPeopleResponseMessage>(() => new FindPeople(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BB9 RID: 23481 RVA: 0x0011CB14 File Offset: 0x0011AD14
		public Task<FindPeopleResponseMessage> FindPeopleAsync(FindPeopleRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<FindPeopleResponseMessage>(() => new FindPeople(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BBA RID: 23482 RVA: 0x0011CB68 File Offset: 0x0011AD68
		public SyncAutoCompleteRecipientsResponseMessage SyncAutoCompleteRecipients(SyncAutoCompleteRecipientsRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<SyncAutoCompleteRecipientsResponseMessage>(() => new SyncAutoCompleteRecipients(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BBB RID: 23483 RVA: 0x0011CBBC File Offset: 0x0011ADBC
		public Task<SyncAutoCompleteRecipientsResponseMessage> SyncAutoCompleteRecipientsAsync(SyncAutoCompleteRecipientsRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<SyncAutoCompleteRecipientsResponseMessage>(() => new SyncAutoCompleteRecipients(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BBC RID: 23484 RVA: 0x0011CC10 File Offset: 0x0011AE10
		public GetPersonaResponseMessage GetPersona(GetPersonaRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<GetPersonaResponseMessage>(() => new GetPersona(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BBD RID: 23485 RVA: 0x0011CC64 File Offset: 0x0011AE64
		public Task<GetPersonaResponseMessage> GetPersonaAsync(GetPersonaRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<GetPersonaResponseMessage>(() => new GetPersona(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BBE RID: 23486 RVA: 0x0011CC98 File Offset: 0x0011AE98
		public ConversationFeedLoader GetConversationFeedLoader(ExTimeZone timezone)
		{
			return new ConversationFeedLoader(this.CallContext.SessionCache.GetMailboxIdentityMailboxSession(), timezone);
		}

		// Token: 0x06005BBF RID: 23487 RVA: 0x0011CCB0 File Offset: 0x0011AEB0
		public Guid GetMailboxGuid()
		{
			HttpContext.Current = this.CallContext.HttpContext;
			return this.CallContext.SessionCache.GetMailboxIdentityMailboxSession().MailboxGuid;
		}

		// Token: 0x06005BC0 RID: 23488 RVA: 0x0011CCD8 File Offset: 0x0011AED8
		public Guid GetConversationGuidFromEwsId(string ewsId)
		{
			ConversationId conversationId = IdConverter.EwsIdToConversationId(ewsId);
			return new Guid(conversationId.GetBytes());
		}

		// Token: 0x06005BC1 RID: 23489 RVA: 0x0011CD18 File Offset: 0x0011AF18
		public GetComplianceConfigurationResponseMessage GetComplianceConfiguration(GetComplianceConfigurationRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<GetComplianceConfigurationResponseMessage>(() => new GetComplianceConfiguration(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BC2 RID: 23490 RVA: 0x0011CD4C File Offset: 0x0011AF4C
		public Guid GetMailboxGuidFromEwsId(string ewsId)
		{
			IdHeaderInformation idHeaderInformation = ServiceIdConverter.ConvertFromConcatenatedId(ewsId, BasicTypes.Item, null);
			return new Guid(idHeaderInformation.MailboxId.MailboxGuid);
		}

		// Token: 0x06005BC3 RID: 23491 RVA: 0x0011CD74 File Offset: 0x0011AF74
		public string GetEwsIdFromConversationGuid(Guid conversationGuid, Guid mailboxGuid)
		{
			ConversationId conversationId = ConversationId.Create(conversationGuid);
			return IdConverter.ConversationIdToEwsId(mailboxGuid, conversationId);
		}

		// Token: 0x06005BC4 RID: 23492 RVA: 0x0011CD90 File Offset: 0x0011AF90
		public void GetItemMidFidDateFromEwsId(string ewsId, out long mid, out long fid, out ExDateTime date)
		{
			IdHeaderInformation idHeaderInformation = ServiceIdConverter.ConvertFromConcatenatedId(ewsId, BasicTypes.Item, null);
			StoreObjectId storeObjectId = idHeaderInformation.ToStoreObjectId();
			MailboxSession mailboxSessionByMailboxId = this.CallContext.SessionCache.GetMailboxSessionByMailboxId(new MailboxId(new Guid(idHeaderInformation.MailboxId.MailboxGuid)));
			IdConverter idConverter = mailboxSessionByMailboxId.IdConverter;
			mid = idConverter.GetMidFromMessageId(storeObjectId);
			fid = idConverter.GetFidFromId(storeObjectId);
			OccurrenceStoreObjectId occurrenceStoreObjectId = storeObjectId as OccurrenceStoreObjectId;
			if (occurrenceStoreObjectId != null)
			{
				date = occurrenceStoreObjectId.OccurrenceId;
				return;
			}
			date = ExDateTime.MinValue;
		}

		// Token: 0x06005BC5 RID: 23493 RVA: 0x0011CE14 File Offset: 0x0011B014
		public string GetEwsIdFromItemMidFidDate(long mid, long fid, ExDateTime date, Guid mailboxGuid)
		{
			MailboxSession mailboxSessionByMailboxId = this.CallContext.SessionCache.GetMailboxSessionByMailboxId(new MailboxId(mailboxGuid));
			IdConverter idConverter = mailboxSessionByMailboxId.IdConverter;
			StoreObjectId storeObjectId = idConverter.CreateMessageId(fid, mid);
			if (date != ExDateTime.MinValue)
			{
				storeObjectId = new OccurrenceStoreObjectId(storeObjectId.ProviderLevelItemId, date);
			}
			return StoreId.StoreIdToEwsId(mailboxGuid, storeObjectId);
		}

		// Token: 0x06005BC6 RID: 23494 RVA: 0x0011CE6C File Offset: 0x0011B06C
		public bool GetFolderFidAndMailboxFromEwsId(string ewsId, out long fid, out Guid mailboxGuid)
		{
			bool result;
			try
			{
				IdHeaderInformation idHeaderInformation = ServiceIdConverter.ConvertFromConcatenatedId(ewsId, BasicTypes.Item, null);
				if (!Guid.TryParse(idHeaderInformation.MailboxId.MailboxGuid, out mailboxGuid))
				{
					fid = 0L;
					result = false;
				}
				else
				{
					StoreObjectId storeObjectId = idHeaderInformation.ToStoreObjectId();
					MailboxSession mailboxSessionByMailboxId = this.CallContext.SessionCache.GetMailboxSessionByMailboxId(new MailboxId(new Guid(idHeaderInformation.MailboxId.MailboxGuid)));
					IdConverter idConverter = mailboxSessionByMailboxId.IdConverter;
					fid = idConverter.GetFidFromId(storeObjectId);
					result = true;
				}
			}
			catch
			{
				mailboxGuid = Guid.Empty;
				fid = 0L;
				result = false;
			}
			return result;
		}

		// Token: 0x06005BC7 RID: 23495 RVA: 0x0011CF08 File Offset: 0x0011B108
		public long GetFolderFidFromEwsId(string ewsId)
		{
			IdHeaderInformation idHeaderInformation = ServiceIdConverter.ConvertFromConcatenatedId(ewsId, BasicTypes.Item, null);
			StoreObjectId storeObjectId = idHeaderInformation.ToStoreObjectId();
			MailboxSession mailboxSessionByMailboxId = this.CallContext.SessionCache.GetMailboxSessionByMailboxId(new MailboxId(new Guid(idHeaderInformation.MailboxId.MailboxGuid)));
			IdConverter idConverter = mailboxSessionByMailboxId.IdConverter;
			return idConverter.GetFidFromId(storeObjectId);
		}

		// Token: 0x06005BC8 RID: 23496 RVA: 0x0011CF5C File Offset: 0x0011B15C
		public string GetEwsIdFromFolderFid(long fid, Guid mailboxGuid)
		{
			MailboxSession mailboxSessionByMailboxId = this.CallContext.SessionCache.GetMailboxSessionByMailboxId(new MailboxId(mailboxGuid));
			IdConverter idConverter = mailboxSessionByMailboxId.IdConverter;
			StoreObjectId storeId = idConverter.CreateFolderId(fid);
			return StoreId.StoreIdToEwsId(mailboxGuid, storeId);
		}

		// Token: 0x06005BC9 RID: 23497 RVA: 0x0011CF98 File Offset: 0x0011B198
		public string GetDistinguishedFolderIdFromEwsId(string ewsId)
		{
			IdHeaderInformation idHeaderInformation = ServiceIdConverter.ConvertFromConcatenatedId(ewsId, BasicTypes.Item, null);
			StoreObjectId folderId = idHeaderInformation.ToStoreObjectId();
			MailboxSession mailboxSessionByMailboxId = this.CallContext.SessionCache.GetMailboxSessionByMailboxId(new MailboxId(new Guid(idHeaderInformation.MailboxId.MailboxGuid)));
			if (EWSSettings.DistinguishedFolderIdNameDictionary == null)
			{
				EWSSettings.DistinguishedFolderIdNameDictionary = new DistinguishedFolderIdNameDictionary();
			}
			return EWSSettings.DistinguishedFolderIdNameDictionary.Get(folderId, mailboxSessionByMailboxId);
		}

		// Token: 0x06005BCA RID: 23498 RVA: 0x0011CFF8 File Offset: 0x0011B1F8
		public Guid GetAttachmentGuidFromEwsId(string ewsId, long mid, long fid)
		{
			long num;
			long num2;
			ExDateTime exDateTime;
			this.GetItemMidFidDateFromEwsId(ewsId, out num, out num2, out exDateTime);
			if (mid != num || fid != num2)
			{
				throw new InvalidOperationException("Item id mismatch.");
			}
			List<AttachmentId> list = new List<AttachmentId>();
			ServiceIdConverter.ConvertFromConcatenatedId(ewsId, BasicTypes.ItemOrAttachment, list);
			if (list.Count > 1)
			{
				throw new InvalidOperationException("Nested attachments not supported.");
			}
			byte[] array = list[0].ToByteArray();
			if (array.Length != 18)
			{
				throw new InvalidOperationException("Unexpected attachment key.");
			}
			byte[] array2 = new byte[16];
			Array.Copy(array, 2, array2, 0, 16);
			return new Guid(array2);
		}

		// Token: 0x06005BCB RID: 23499 RVA: 0x0011D088 File Offset: 0x0011B288
		public string GetEwsIdFromAttachmentGuid(Guid attachmentGuid, long mid, long fid, Guid mailboxGuid)
		{
			MailboxId mailboxId = new MailboxId(mailboxGuid);
			MailboxSession mailboxSessionByMailboxId = this.CallContext.SessionCache.GetMailboxSessionByMailboxId(mailboxId);
			IdConverter idConverter = mailboxSessionByMailboxId.IdConverter;
			StoreId storeId = idConverter.CreateMessageId(fid, mid);
			byte[] array = attachmentGuid.ToByteArray();
			short num = (short)array.Length;
			byte[] array2 = new byte[(int)(2 + num)];
			int num2 = 0;
			num2 += ExBitConverter.Write(num, array2, num2);
			array.CopyTo(array2, num2);
			AttachmentId item = AttachmentId.Deserialize(array2);
			return IdConverter.GetConcatenatedId(storeId, mailboxId, new List<AttachmentId>
			{
				item
			}).Id;
		}

		// Token: 0x06005BCC RID: 23500 RVA: 0x0011D14C File Offset: 0x0011B34C
		public SubscribeToConversationChangesResponseMessage SubscribeToConversationChanges(SubscribeToConversationChangesRequest request, Action<ConversationNotification> callback, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<SubscribeToConversationChangesResponseMessage>(() => new SubscribeToConversationChanges(this.CallContext, request, callback), executionOption);
		}

		// Token: 0x06005BCD RID: 23501 RVA: 0x0011D1B0 File Offset: 0x0011B3B0
		public Task<SubscribeToConversationChangesResponseMessage> SubscribeToConversationChangesAsync(SubscribeToConversationChangesRequest request, Action<ConversationNotification> callback, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<SubscribeToConversationChangesResponseMessage>(() => new SubscribeToConversationChanges(this.CallContext, request, callback), executionOption);
		}

		// Token: 0x06005BCE RID: 23502 RVA: 0x0011D1EB File Offset: 0x0011B3EB
		public void SetCallContextFromActionInfo(string actionQueueId, string protocol, string deviceType, string actionId, bool IsOutlookService)
		{
			this.CallContext.SetCallContextFromActionInfo(actionQueueId, protocol, deviceType, actionId, IsOutlookService);
		}

		// Token: 0x06005BCF RID: 23503 RVA: 0x0011D1FF File Offset: 0x0011B3FF
		public void DisableDupDetection()
		{
			this.CallContext.DisableDupDetection();
		}

		// Token: 0x06005BD0 RID: 23504 RVA: 0x0011D20C File Offset: 0x0011B40C
		public bool TryGetResponse<T>(out T results)
		{
			return this.CallContext.TryGetResponse<T>(out results);
		}

		// Token: 0x06005BD1 RID: 23505 RVA: 0x0011D21A File Offset: 0x0011B41A
		public void SetResponse<T>(T result, Exception exception)
		{
			this.CallContext.SetResponse<T>(result, exception);
		}

		// Token: 0x06005BD2 RID: 23506 RVA: 0x0011D229 File Offset: 0x0011B429
		public bool? GetIsDuplicatedAction()
		{
			return this.CallContext.IsDuplicatedAction;
		}

		// Token: 0x06005BD3 RID: 23507 RVA: 0x0011D236 File Offset: 0x0011B436
		public bool GetReturningSavedResult()
		{
			return this.CallContext.ReturningSavedResult;
		}

		// Token: 0x06005BD4 RID: 23508 RVA: 0x0011D243 File Offset: 0x0011B443
		public bool GetResultSaved()
		{
			return this.CallContext.ResultSaved;
		}

		// Token: 0x06005BD5 RID: 23509 RVA: 0x0011D250 File Offset: 0x0011B450
		public QuotedTextResult ParseQuotedText(string msg, bool reorderMsgs)
		{
			return QuotedText.ParseHtmlQuotedText(msg, reorderMsgs);
		}

		// Token: 0x06005BD6 RID: 23510 RVA: 0x0011D25C File Offset: 0x0011B45C
		public IOutlookServiceStorage GetOutlookServiceStorage()
		{
			HttpContext.Current = this.CallContext.HttpContext;
			MailboxSession mailboxIdentityMailboxSession = this.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			return OutlookServiceStorage.Create(mailboxIdentityMailboxSession);
		}

		// Token: 0x06005BD7 RID: 23511 RVA: 0x0011D2B8 File Offset: 0x0011B4B8
		public SubscribeToCalendarChangesResponseMessage SubscribeToCalendarChanges(SubscribeToCalendarChangesRequest request, Action<CalendarChangeNotificationType> callback, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<SubscribeToCalendarChangesResponseMessage>(() => new SubscribeToCalendarChanges(this.CallContext, request, callback), executionOption);
		}

		// Token: 0x06005BD8 RID: 23512 RVA: 0x0011D31C File Offset: 0x0011B51C
		public Task<SubscribeToCalendarChangesResponseMessage> SubscribeToCalendarChangesAsync(SubscribeToCalendarChangesRequest request, Action<CalendarChangeNotificationType> callback, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<SubscribeToCalendarChangesResponseMessage>(() => new SubscribeToCalendarChanges(this.CallContext, request, callback), executionOption);
		}

		// Token: 0x06005BD9 RID: 23513 RVA: 0x0011D364 File Offset: 0x0011B564
		public GetCalendarFoldersResponse GetCalendarFolders(ExecutionOption executionOption = null)
		{
			return this.InvokeOwsServiceCommand<GetCalendarFoldersResponse>(() => new GetCalendarFoldersCommand(this.CallContext), executionOption, false);
		}

		// Token: 0x06005BDA RID: 23514 RVA: 0x0011D37A File Offset: 0x0011B57A
		public IHtmlReader GetHtmlReader(string html)
		{
			return new HtmlReaderWrapper(html);
		}

		// Token: 0x06005BDB RID: 23515 RVA: 0x0011D3A8 File Offset: 0x0011B5A8
		public PerformInstantSearchResponse PerformInstantSearch(PerformInstantSearchRequest request, Action<InstantSearchPayloadType> searchPayloadCallback, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<PerformInstantSearchResponse>(() => new PerformInstantSearch(this.CallContext, searchPayloadCallback, request), executionOption);
		}

		// Token: 0x06005BDC RID: 23516 RVA: 0x0011D40C File Offset: 0x0011B60C
		public Task<PerformInstantSearchResponse> PerformInstantSearchAsync(PerformInstantSearchRequest request, Action<InstantSearchPayloadType> searchPayloadCallback, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<PerformInstantSearchResponse>(() => new PerformInstantSearch(this.CallContext, searchPayloadCallback, request), executionOption);
		}

		// Token: 0x06005BDD RID: 23517 RVA: 0x0011D468 File Offset: 0x0011B668
		public EndInstantSearchSessionResponse EndInstantSearchSession(string deviceId, string sessionId, ExecutionOption executionOption = null)
		{
			EndInstantSearchSessionRequest request = new EndInstantSearchSessionRequest();
			request.DeviceId = deviceId;
			request.SessionId = sessionId;
			return this.InvokeServiceCommand<EndInstantSearchSessionResponse>(() => new EndInstantSearchSession(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BDE RID: 23518 RVA: 0x0011D4D8 File Offset: 0x0011B6D8
		public Task<EndInstantSearchSessionResponse> EndInstantSearchSessionAsync(string deviceId, string sessionId, ExecutionOption executionOption = null)
		{
			EndInstantSearchSessionRequest request = new EndInstantSearchSessionRequest();
			request.DeviceId = deviceId;
			request.SessionId = sessionId;
			return this.InvokeServiceCommandAsync<EndInstantSearchSessionResponse>(() => new EndInstantSearchSession(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BDF RID: 23519 RVA: 0x0011D528 File Offset: 0x0011B728
		public string GetBodyWithQuotedText(string messageBody, List<string> messageIds, List<string> messageBodyColl)
		{
			return QuotedTextBuilder.GetBodyWithQuotedText(this.CallContext, messageBody, messageIds, messageBodyColl);
		}

		// Token: 0x06005BE0 RID: 23520 RVA: 0x0011D558 File Offset: 0x0011B758
		public MasterCategoryListActionResponse GetMasterCategoryList(GetMasterCategoryListRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeOwsServiceCommand<MasterCategoryListActionResponse>(() => new GetMasterCategoryListCommand(this.CallContext, request), executionOption, false);
		}

		// Token: 0x06005BE1 RID: 23521 RVA: 0x0011D5B0 File Offset: 0x0011B7B0
		public GetUserAvailabilityResponse GetUserAvailability(GetUserAvailabilityRequest request, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<GetUserAvailabilityResponse>(() => new GetUserAvailability(this.CallContext, request), executionOption);
		}

		// Token: 0x06005BE2 RID: 23522 RVA: 0x0011D60C File Offset: 0x0011B80C
		public SubscribeToHierarchyChangesResponseMessage SubscribeToHierarchyChanges(SubscribeToHierarchyChangesRequest request, Action<HierarchyNotification> callback, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<SubscribeToHierarchyChangesResponseMessage>(() => new SubscribeToHierarchyChanges(this.CallContext, request, callback), executionOption);
		}

		// Token: 0x06005BE3 RID: 23523 RVA: 0x0011D670 File Offset: 0x0011B870
		public Task<SubscribeToHierarchyChangesResponseMessage> SubscribeToHierarchyChangesAsync(SubscribeToHierarchyChangesRequest request, Action<HierarchyNotification> callback, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<SubscribeToHierarchyChangesResponseMessage>(() => new SubscribeToHierarchyChanges(this.CallContext, request, callback), executionOption);
		}

		// Token: 0x06005BE4 RID: 23524 RVA: 0x0011D6D4 File Offset: 0x0011B8D4
		public SubscribeToMessageChangesResponseMessage SubscribeToMessageChanges(SubscribeToMessageChangesRequest request, Action<MessageNotification> callback, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommand<SubscribeToMessageChangesResponseMessage>(() => new SubscribeToMessageChanges(this.CallContext, request, callback), executionOption);
		}

		// Token: 0x06005BE5 RID: 23525 RVA: 0x0011D738 File Offset: 0x0011B938
		public Task<SubscribeToMessageChangesResponseMessage> SubscribeToMessageChangesAsync(SubscribeToMessageChangesRequest request, Action<MessageNotification> callback, ExecutionOption executionOption = null)
		{
			return this.InvokeServiceCommandAsync<SubscribeToMessageChangesResponseMessage>(() => new SubscribeToMessageChanges(this.CallContext, request, callback), executionOption);
		}

		// Token: 0x06005BE6 RID: 23526 RVA: 0x0011D774 File Offset: 0x0011B974
		public void SetRequestTimeZoneId(string timeZoneId)
		{
			if (!string.IsNullOrEmpty(timeZoneId))
			{
				ExTimeZone exTimeZone = null;
				ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(timeZoneId, out exTimeZone);
				if (exTimeZone != null)
				{
					EWSSettings.RequestTimeZone = exTimeZone;
				}
			}
		}

		// Token: 0x06005BE7 RID: 23527 RVA: 0x0011D7A4 File Offset: 0x0011B9A4
		public ExDateTime GetOriginalStartDateFromEwsId(string ewsId)
		{
			IdHeaderInformation idHeaderInformation = ServiceIdConverter.ConvertFromConcatenatedId(ewsId, BasicTypes.Item, null);
			StoreObjectId storeObjectId = idHeaderInformation.ToStoreObjectId();
			OccurrenceStoreObjectId occurrenceStoreObjectId = storeObjectId as OccurrenceStoreObjectId;
			if (occurrenceStoreObjectId != null)
			{
				return occurrenceStoreObjectId.OccurrenceId;
			}
			return ExDateTime.MinValue;
		}

		// Token: 0x06005BE8 RID: 23528 RVA: 0x0011D7D8 File Offset: 0x0011B9D8
		public PatternedRecurrence ConvertToPatternedRecurrence(Recurrence value)
		{
			RecurrenceConverter recurrenceConverter = new RecurrenceConverter(value.CreatedExTimeZone);
			return recurrenceConverter.Convert(value);
		}

		// Token: 0x170014DF RID: 5343
		// (get) Token: 0x06005BE9 RID: 23529 RVA: 0x0011D7F8 File Offset: 0x0011B9F8
		// (set) Token: 0x06005BEA RID: 23530 RVA: 0x0011D800 File Offset: 0x0011BA00
		protected CallContext CallContext { get; set; }

		// Token: 0x170014E0 RID: 5344
		// (get) Token: 0x06005BEB RID: 23531 RVA: 0x0011D809 File Offset: 0x0011BA09
		// (set) Token: 0x06005BEC RID: 23532 RVA: 0x0011D811 File Offset: 0x0011BA11
		protected IActivityScope ActivityScope { get; set; }

		// Token: 0x06005BED RID: 23533 RVA: 0x0011D81A File Offset: 0x0011BA1A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ExchangeServiceBase>(this);
		}

		// Token: 0x06005BEE RID: 23534 RVA: 0x0011D8A4 File Offset: 0x0011BAA4
		protected virtual TResponse InvokeServiceCommand<TResponse>(Func<ServiceCommandBase> commandCreator, ExecutionOption executionOption)
		{
			ExecutionOption executionOption2 = executionOption ?? ExecutionOption.Default;
			TResponse response = default(TResponse);
			this.CallWithExceptionHandling(executionOption2, delegate
			{
				HttpContext.Current = this.CallContext.HttpContext;
				ServiceCommandBase serviceCommandBase = commandCreator();
				if (serviceCommandBase.PreExecute())
				{
					for (int i = 0; i < serviceCommandBase.StepCount; i++)
					{
						this.CallContext.Budget.CheckOverBudget();
						TaskExecuteResult taskExecuteResult = serviceCommandBase.ExecuteStep();
						if (taskExecuteResult == TaskExecuteResult.ProcessingComplete)
						{
							break;
						}
					}
				}
				response = (TResponse)((object)serviceCommandBase.PostExecute());
			});
			ExchangeServiceHelper.CheckResponse(response as IExchangeWebMethodResponse, executionOption2);
			return response;
		}

		// Token: 0x06005BEF RID: 23535 RVA: 0x0011D92C File Offset: 0x0011BB2C
		protected virtual Task<TResponse> InvokeServiceCommandAsync<TResponse>(Func<ServiceCommandBase> commandCreator, ExecutionOption executionOption)
		{
			return Task<TResponse>.Factory.StartNew(() => this.InvokeServiceCommand<TResponse>(commandCreator, executionOption));
		}

		// Token: 0x06005BF0 RID: 23536 RVA: 0x0011D9C4 File Offset: 0x0011BBC4
		protected virtual TResponse InvokeOwsServiceCommand<TResponse>(Func<ServiceCommand<TResponse>> commandCreator, ExecutionOption executionOption, bool throwOnNullResponse = false)
		{
			ExecutionOption executionOption2 = executionOption ?? ExecutionOption.Default;
			TResponse response = default(TResponse);
			this.CallWithExceptionHandling(executionOption2, delegate
			{
				ServiceCommand<TResponse> serviceCommand = commandCreator();
				HttpContext.Current = this.CallContext.HttpContext;
				this.CallContext.Budget.CheckOverBudget();
				response = serviceCommand.Execute();
			});
			if (response == null && throwOnNullResponse)
			{
				throw new ExchangeServiceResponseException(CoreResources.ExchangeServiceResponseErrorNoResponse);
			}
			return response;
		}

		// Token: 0x06005BF1 RID: 23537 RVA: 0x0011DA58 File Offset: 0x0011BC58
		protected virtual Task<TResponse> InvokeOwsServiceCommandAsync<TResponse>(Func<ServiceCommand<TResponse>> commandCreator, ExecutionOption executionOption, bool throwOnNullResponse = false)
		{
			return Task<TResponse>.Factory.StartNew(() => this.InvokeOwsServiceCommand<TResponse>(commandCreator, executionOption, throwOnNullResponse));
		}

		// Token: 0x06005BF2 RID: 23538 RVA: 0x0011DAA0 File Offset: 0x0011BCA0
		protected void CallWithExceptionHandling(ExecutionOption executionOption, Action action)
		{
			if (executionOption.WrapExecutionExceptions)
			{
				try
				{
					action();
					return;
				}
				catch (ServicePermanentException innerException)
				{
					throw new ExchangeServicePermanentException(innerException);
				}
				catch (StoragePermanentException innerException2)
				{
					throw new ExchangeServicePermanentException(innerException2);
				}
				catch (StorageTransientException innerException3)
				{
					throw new ExchangeServiceTransientException(innerException3);
				}
				catch (FaultException ex)
				{
					throw new ExchangeServicePermanentException(new LocalizedString(ex.Message), ex);
				}
			}
			action();
		}

		// Token: 0x04003215 RID: 12821
		public static readonly string RenewTag = 3841U.ToString();
	}
}
