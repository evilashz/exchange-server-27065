using System;
using System.IO;
using System.Net;
using System.Runtime.ExceptionServices;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Threading.Tasks;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.InfoWorker.Availability;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020008FF RID: 2303
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	[MessageInspectorBehavior]
	[ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
	public class JsonService : IJsonServiceContract, IO365SuiteServiceContract, IJsonStreamingServiceContract
	{
		// Token: 0x06004122 RID: 16674 RVA: 0x000DC4A4 File Offset: 0x000DA6A4
		public AddSharedCalendarResponse AddSharedCalendar(AddSharedCalendarRequest request)
		{
			return new AddSharedCalendarCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004123 RID: 16675 RVA: 0x000DC4B6 File Offset: 0x000DA6B6
		public CalendarActionFolderIdResponse SubscribeInternalCalendar(SubscribeInternalCalendarRequest request)
		{
			return new SubscribeInternalCalendarCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004124 RID: 16676 RVA: 0x000DC4C8 File Offset: 0x000DA6C8
		public CalendarActionFolderIdResponse SubscribeInternetCalendar(SubscribeInternetCalendarRequest request)
		{
			return new SubscribeInternetCalendarCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004125 RID: 16677 RVA: 0x000DC4DA File Offset: 0x000DA6DA
		public GetCalendarSharingRecipientInfoResponse GetCalendarSharingRecipientInfo(GetCalendarSharingRecipientInfoRequest request)
		{
			return new GetCalendarSharingRecipientInfoCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004126 RID: 16678 RVA: 0x000DC4EC File Offset: 0x000DA6EC
		public GetCalendarSharingPermissionsResponse GetCalendarSharingPermissions(GetCalendarSharingPermissionsRequest request)
		{
			return new GetCalendarSharingPermissionsCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004127 RID: 16679 RVA: 0x000DC4FE File Offset: 0x000DA6FE
		public CalendarActionResponse SetCalendarSharingPermissions(SetCalendarSharingPermissionsRequest request)
		{
			return new SetCalendarSharingPermissionsCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004128 RID: 16680 RVA: 0x000DC510 File Offset: 0x000DA710
		public SetCalendarPublishingResponse SetCalendarPublishing(SetCalendarPublishingRequest request)
		{
			return new SetCalendarPublishingCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004129 RID: 16681 RVA: 0x000DC522 File Offset: 0x000DA722
		public CalendarShareInviteResponse SendCalendarSharingInvite(CalendarShareInviteRequest request)
		{
			return new SendCalendarSharingInviteCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600412A RID: 16682 RVA: 0x000DC534 File Offset: 0x000DA734
		public ExtensibilityContext GetExtensibilityContext(GetExtensibilityContextParameters request)
		{
			return new GetExtensibilityContext(CallContext.Current, request).Execute();
		}

		// Token: 0x0600412B RID: 16683 RVA: 0x000DC546 File Offset: 0x000DA746
		public bool AddBuddy(Buddy buddy)
		{
			return new AddBuddyCommand(CallContext.Current, buddy).Execute();
		}

		// Token: 0x0600412C RID: 16684 RVA: 0x000DC558 File Offset: 0x000DA758
		public GetBuddyListResponse GetBuddyList()
		{
			return new GetBuddyListCommand(CallContext.Current).Execute();
		}

		// Token: 0x0600412D RID: 16685 RVA: 0x000DC569 File Offset: 0x000DA769
		public IAsyncResult BeginFindPlaces(FindPlacesRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return new FindPlaces(CallContext.Current, request, asyncCallback, asyncState).Execute();
		}

		// Token: 0x0600412E RID: 16686 RVA: 0x000DC580 File Offset: 0x000DA780
		public Persona[] EndFindPlaces(IAsyncResult result)
		{
			FindPlacesAsyncResult findPlacesAsyncResult = result as FindPlacesAsyncResult;
			findPlacesAsyncResult.EndTimeoutDetection();
			if (findPlacesAsyncResult.Fault != null)
			{
				throw findPlacesAsyncResult.Fault;
			}
			return findPlacesAsyncResult.Data;
		}

		// Token: 0x0600412F RID: 16687 RVA: 0x000DC5AF File Offset: 0x000DA7AF
		public DeletePlaceJsonResponse DeletePlace(DeletePlaceRequest request)
		{
			new DeletePlaceCommand(CallContext.Current, request).Execute();
			return new DeletePlaceJsonResponse();
		}

		// Token: 0x06004130 RID: 16688 RVA: 0x000DC5C7 File Offset: 0x000DA7C7
		public CalendarActionResponse AddEventToMyCalendar(AddEventToMyCalendarRequest request)
		{
			return new AddEventToMyCalendarCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004131 RID: 16689 RVA: 0x000DC5D9 File Offset: 0x000DA7D9
		public bool AddTrustedSender(Microsoft.Exchange.Services.Core.Types.ItemId itemId)
		{
			return new AddTrustedSender(CallContext.Current, itemId).Execute();
		}

		// Token: 0x06004132 RID: 16690 RVA: 0x000DC5EC File Offset: 0x000DA7EC
		public GetPersonaModernGroupMembershipJsonResponse GetPersonaModernGroupMembership(GetPersonaModernGroupMembershipJsonRequest request)
		{
			return new GetPersonaModernGroupMembershipJsonResponse
			{
				Body = new GetPersonaModernGroupMembership(CallContext.Current, request.Body).Execute()
			};
		}

		// Token: 0x06004133 RID: 16691 RVA: 0x000DC61C File Offset: 0x000DA81C
		public GetModernGroupJsonResponse GetModernGroup(GetModernGroupJsonRequest request)
		{
			return new GetModernGroupJsonResponse
			{
				Body = new GetModernGroup(CallContext.Current, request.Body).Execute()
			};
		}

		// Token: 0x06004134 RID: 16692 RVA: 0x000DC64C File Offset: 0x000DA84C
		public GetModernGroupJsonResponse GetRecommendedModernGroup(GetModernGroupJsonRequest request)
		{
			return new GetModernGroupJsonResponse
			{
				Body = new GetRecommendedModernGroup(CallContext.Current, request.Body).Execute()
			};
		}

		// Token: 0x06004135 RID: 16693 RVA: 0x000DC67C File Offset: 0x000DA87C
		public GetModernGroupsJsonResponse GetModernGroups()
		{
			return new GetModernGroupsJsonResponse
			{
				Body = new GetModernGroups(CallContext.Current).Execute()
			};
		}

		// Token: 0x06004136 RID: 16694 RVA: 0x000DC6A5 File Offset: 0x000DA8A5
		public SetModernGroupPinStateJsonResponse SetModernGroupPinState(string smtpAddress, bool isPinned)
		{
			return new SetModernGroupPinState(CallContext.Current, smtpAddress, isPinned).Execute();
		}

		// Token: 0x06004137 RID: 16695 RVA: 0x000DC6B8 File Offset: 0x000DA8B8
		public SetModernGroupMembershipJsonResponse SetModernGroupMembership(SetModernGroupMembershipJsonRequest request)
		{
			return new SetModernGroupMembershipJsonResponse
			{
				Body = new SetModernGroupMembership(CallContext.Current, request).Execute()
			};
		}

		// Token: 0x06004138 RID: 16696 RVA: 0x000DC6E2 File Offset: 0x000DA8E2
		public bool SetModernGroupSubscription()
		{
			return new SetModernGroupSubscription(CallContext.Current).Execute();
		}

		// Token: 0x06004139 RID: 16697 RVA: 0x000DC6F4 File Offset: 0x000DA8F4
		public GetModernGroupUnseenItemsJsonResponse GetModernGroupUnseenItems(GetModernGroupUnseenItemsJsonRequest request)
		{
			return new GetModernGroupUnseenItemsJsonResponse
			{
				Body = new GetModernGroupUnseenItems(CallContext.Current).Execute()
			};
		}

		// Token: 0x0600413A RID: 16698 RVA: 0x000DC71D File Offset: 0x000DA91D
		public GetFavoritesResponse GetFavorites()
		{
			return new GetFavorites(CallContext.Current).Execute();
		}

		// Token: 0x0600413B RID: 16699 RVA: 0x000DC72E File Offset: 0x000DA92E
		public UpdateFavoriteFolderResponse UpdateFavoriteFolder(UpdateFavoriteFolderRequest request)
		{
			return new UpdateFavoriteFolder(CallContext.Current, request).Execute();
		}

		// Token: 0x0600413C RID: 16700 RVA: 0x000DC740 File Offset: 0x000DA940
		public UpdateMasterCategoryListResponse UpdateMasterCategoryList(UpdateMasterCategoryListRequest request)
		{
			return new UpdateMasterCategoryList(CallContext.Current, request).Execute();
		}

		// Token: 0x0600413D RID: 16701 RVA: 0x000DC752 File Offset: 0x000DA952
		public MasterCategoryListActionResponse GetMasterCategoryList(GetMasterCategoryListRequest request)
		{
			return new GetMasterCategoryListCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600413E RID: 16702 RVA: 0x000DC764 File Offset: 0x000DA964
		public GetTaskFoldersResponse GetTaskFolders()
		{
			return new GetTaskFoldersCommand(CallContext.Current).Execute();
		}

		// Token: 0x0600413F RID: 16703 RVA: 0x000DC775 File Offset: 0x000DA975
		public TaskFolderActionFolderIdResponse CreateTaskFolder(string newTaskFolderName, string parentGroupGuid)
		{
			return new CreateTaskFolderCommand(CallContext.Current, newTaskFolderName, parentGroupGuid).Execute();
		}

		// Token: 0x06004140 RID: 16704 RVA: 0x000DC788 File Offset: 0x000DA988
		public TaskFolderActionFolderIdResponse RenameTaskFolder(Microsoft.Exchange.Services.Core.Types.ItemId itemId, string newTaskFolderName)
		{
			return new RenameTaskFolderCommand(CallContext.Current, itemId, newTaskFolderName).Execute();
		}

		// Token: 0x06004141 RID: 16705 RVA: 0x000DC79B File Offset: 0x000DA99B
		public TaskFolderActionResponse DeleteTaskFolder(Microsoft.Exchange.Services.Core.Types.ItemId itemId)
		{
			return new DeleteTaskFolderCommand(CallContext.Current, itemId).Execute();
		}

		// Token: 0x06004142 RID: 16706 RVA: 0x000DC7AD File Offset: 0x000DA9AD
		public IAsyncResult BeginGetModernConversationAttachments(GetModernConversationAttachmentsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetModernConversationAttachmentsResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06004143 RID: 16707 RVA: 0x000DC7D7 File Offset: 0x000DA9D7
		public GetModernConversationAttachmentsJsonResponse EndGetModernConversationAttachments(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetModernConversationAttachmentsJsonResponse, GetModernConversationAttachmentsResponse>(result, (GetModernConversationAttachmentsResponse body) => new GetModernConversationAttachmentsJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004144 RID: 16708 RVA: 0x000DC7FC File Offset: 0x000DA9FC
		public IAsyncResult BeginFindTrendingConversation(FindTrendingConversationJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<FindConversationResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06004145 RID: 16709 RVA: 0x000DC827 File Offset: 0x000DAA27
		public FindConversationJsonResponse EndFindTrendingConversation(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<FindConversationJsonResponse, FindConversationResponseMessage>(result, (FindConversationResponseMessage body) => new FindConversationJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004146 RID: 16710 RVA: 0x000DC84C File Offset: 0x000DAA4C
		public IAsyncResult BeginPostModernGroupItem(PostModernGroupItemJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<PostModernGroupItemResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06004147 RID: 16711 RVA: 0x000DC877 File Offset: 0x000DAA77
		public PostModernGroupItemJsonResponse EndPostModernGroupItem(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<PostModernGroupItemJsonResponse, PostModernGroupItemResponse>(result, (PostModernGroupItemResponse body) => new PostModernGroupItemJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004148 RID: 16712 RVA: 0x000DC89C File Offset: 0x000DAA9C
		public IAsyncResult BeginUpdateAndPostModernGroupItem(UpdateAndPostModernGroupItemJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<UpdateAndPostModernGroupItemResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06004149 RID: 16713 RVA: 0x000DC8C7 File Offset: 0x000DAAC7
		public UpdateAndPostModernGroupItemJsonResponse EndUpdateAndPostModernGroupItem(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<UpdateAndPostModernGroupItemJsonResponse, UpdateAndPostModernGroupItemResponse>(result, (UpdateAndPostModernGroupItemResponse body) => new UpdateAndPostModernGroupItemJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x0600414A RID: 16714 RVA: 0x000DC8EC File Offset: 0x000DAAEC
		public IAsyncResult BeginCreateResponseFromModernGroup(CreateResponseFromModernGroupJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<CreateResponseFromModernGroupResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600414B RID: 16715 RVA: 0x000DC917 File Offset: 0x000DAB17
		public CreateResponseFromModernGroupJsonResponse EndCreateResponseFromModernGroup(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<CreateResponseFromModernGroupJsonResponse, CreateResponseFromModernGroupResponse>(result, (CreateResponseFromModernGroupResponse body) => new CreateResponseFromModernGroupJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x0600414C RID: 16716 RVA: 0x000DC93C File Offset: 0x000DAB3C
		public IAsyncResult BeginExecuteEwsProxy(EwsProxyRequestParameters request, AsyncCallback asyncCallback, object asyncState)
		{
			return new ExecuteEwsProxy(CallContext.Current, request.Body, request.Token, request.ExtensionId, asyncCallback, asyncState).Execute();
		}

		// Token: 0x0600414D RID: 16717 RVA: 0x000DC964 File Offset: 0x000DAB64
		public EwsProxyResponse EndExecuteEwsProxy(IAsyncResult result)
		{
			ServiceAsyncResult<EwsProxyResponse> serviceAsyncResult = result as ServiceAsyncResult<EwsProxyResponse>;
			return serviceAsyncResult.Data;
		}

		// Token: 0x0600414E RID: 16718 RVA: 0x000DC97E File Offset: 0x000DAB7E
		public SaveExtensionSettingsResponse SaveExtensionSettings(SaveExtensionSettingsParameters request)
		{
			return new SaveExtensionSettings(CallContext.Current, request.ExtensionId, request.ExtensionVersion, request.Settings).Execute();
		}

		// Token: 0x0600414F RID: 16719 RVA: 0x000DC9A1 File Offset: 0x000DABA1
		public LoadExtensionCustomPropertiesResponse LoadExtensionCustomProperties(LoadExtensionCustomPropertiesParameters request)
		{
			return new LoadExtensionCustomProperties(CallContext.Current, request.ExtensionId, request.ItemId).Execute();
		}

		// Token: 0x06004150 RID: 16720 RVA: 0x000DC9BE File Offset: 0x000DABBE
		public SaveExtensionCustomPropertiesResponse SaveExtensionCustomProperties(SaveExtensionCustomPropertiesParameters request)
		{
			return new SaveExtensionCustomProperties(CallContext.Current, request.ExtensionId, request.ItemId, request.CustomProperties).Execute();
		}

		// Token: 0x06004151 RID: 16721 RVA: 0x000DC9E1 File Offset: 0x000DABE1
		public Persona UpdatePersona(UpdatePersonaJsonRequest request)
		{
			return new UpdatePersonaCommand(CallContext.Current, request.Body).Execute();
		}

		// Token: 0x06004152 RID: 16722 RVA: 0x000DC9F8 File Offset: 0x000DABF8
		public DeletePersonaJsonResponse DeletePersona(Microsoft.Exchange.Services.Core.Types.ItemId personaId, BaseFolderId deleteInFolder)
		{
			new DeletePersonaCommand(CallContext.Current, personaId, deleteInFolder).Execute();
			return new DeletePersonaJsonResponse();
		}

		// Token: 0x06004153 RID: 16723 RVA: 0x000DCA11 File Offset: 0x000DAC11
		public MaskAutoCompleteRecipientResponse MaskAutoCompleteRecipient(MaskAutoCompleteRecipientRequest request)
		{
			return new MaskAutoCompleteRecipientCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004154 RID: 16724 RVA: 0x000DCA23 File Offset: 0x000DAC23
		public Persona CreatePersona(CreatePersonaJsonRequest request)
		{
			return new CreatePersonaCommand(CallContext.Current, request.Body).Execute();
		}

		// Token: 0x06004155 RID: 16725 RVA: 0x000DCA3A File Offset: 0x000DAC3A
		public CreateModernGroupResponse CreateModernGroup(CreateModernGroupRequest request)
		{
			return new CreateModernGroupCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004156 RID: 16726 RVA: 0x000DCA4C File Offset: 0x000DAC4C
		public UpdateModernGroupResponse UpdateModernGroup(UpdateModernGroupRequest request)
		{
			return new UpdateModernGroupCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004157 RID: 16727 RVA: 0x000DCA5E File Offset: 0x000DAC5E
		public RemoveModernGroupResponse RemoveModernGroup(RemoveModernGroupRequest request)
		{
			return new RemoveModernGroupCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004158 RID: 16728 RVA: 0x000DCA70 File Offset: 0x000DAC70
		public ModernGroupMembershipRequestMessageDetailsResponse ModernGroupMembershipRequestMessageDetails(ModernGroupMembershipRequestMessageDetailsRequest request)
		{
			return new ModernGroupMembershipRequestMessageDetailsCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004159 RID: 16729 RVA: 0x000DCA82 File Offset: 0x000DAC82
		public ValidateModernGroupAliasResponse ValidateModernGroupAlias(ValidateModernGroupAliasRequest request)
		{
			return new ValidateModernGroupAliasCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600415A RID: 16730 RVA: 0x000DCA94 File Offset: 0x000DAC94
		public GetModernGroupDomainResponse GetModernGroupDomain()
		{
			return new GetModernGroupDomainCommand(CallContext.Current).Execute();
		}

		// Token: 0x0600415B RID: 16731 RVA: 0x000DCAA5 File Offset: 0x000DACA5
		public GetPeopleIKnowGraphResponse GetPeopleIKnowGraphCommand(GetPeopleIKnowGraphRequest request)
		{
			return new GetPeopleIKnowGraphCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600415C RID: 16732 RVA: 0x000DCAB7 File Offset: 0x000DACB7
		public Microsoft.Exchange.Services.Core.Types.ItemId[] GetPersonaSuggestions(Microsoft.Exchange.Services.Core.Types.ItemId personaId)
		{
			return new GetPersonaSuggestionsCommand(CallContext.Current, personaId).Execute();
		}

		// Token: 0x0600415D RID: 16733 RVA: 0x000DCAC9 File Offset: 0x000DACC9
		public Persona UnlinkPersona(Microsoft.Exchange.Services.Core.Types.ItemId personaId, Microsoft.Exchange.Services.Core.Types.ItemId contactId)
		{
			return new UnlinkPersonaCommand(CallContext.Current, personaId, contactId).Execute();
		}

		// Token: 0x0600415E RID: 16734 RVA: 0x000DCADC File Offset: 0x000DACDC
		public Persona AcceptPersonaLinkSuggestion(Microsoft.Exchange.Services.Core.Types.ItemId personaId, Microsoft.Exchange.Services.Core.Types.ItemId suggestedPersonaId)
		{
			return new AcceptPersonaLinkSuggestionCommand(CallContext.Current, personaId, suggestedPersonaId).Execute();
		}

		// Token: 0x0600415F RID: 16735 RVA: 0x000DCAEF File Offset: 0x000DACEF
		public Persona LinkPersona(Microsoft.Exchange.Services.Core.Types.ItemId linkToPersonaId, Microsoft.Exchange.Services.Core.Types.ItemId personaIdToBeLinked)
		{
			return new LinkPersonaCommand(CallContext.Current, linkToPersonaId, personaIdToBeLinked).Execute();
		}

		// Token: 0x06004160 RID: 16736 RVA: 0x000DCB02 File Offset: 0x000DAD02
		public Persona RejectPersonaLinkSuggestion(Microsoft.Exchange.Services.Core.Types.ItemId personaId, Microsoft.Exchange.Services.Core.Types.ItemId suggestedPersonaId)
		{
			return new RejectPersonaLinkSuggestionCommand(CallContext.Current, personaId, suggestedPersonaId).Execute();
		}

		// Token: 0x06004161 RID: 16737 RVA: 0x000DCB15 File Offset: 0x000DAD15
		public CalendarActionGroupIdResponse CreateCalendarGroup(string newGroupName)
		{
			return new CreateCalendarGroupCommand(CallContext.Current, newGroupName).Execute();
		}

		// Token: 0x06004162 RID: 16738 RVA: 0x000DCB27 File Offset: 0x000DAD27
		public CalendarActionGroupIdResponse RenameCalendarGroup(Microsoft.Exchange.Services.Core.Types.ItemId groupId, string newGroupName)
		{
			return new RenameCalendarGroupCommand(CallContext.Current, groupId, newGroupName).Execute();
		}

		// Token: 0x06004163 RID: 16739 RVA: 0x000DCB3A File Offset: 0x000DAD3A
		public CalendarActionResponse DeleteCalendarGroup(string groupId)
		{
			return new DeleteCalendarGroupCommand(CallContext.Current, groupId).Execute();
		}

		// Token: 0x06004164 RID: 16740 RVA: 0x000DCB4C File Offset: 0x000DAD4C
		public CalendarActionFolderIdResponse CreateCalendar(string newCalendarName, string parentGroupGuid, string emailAddress)
		{
			return new CreateCalendarCommand(CallContext.Current, newCalendarName, parentGroupGuid, emailAddress).Execute();
		}

		// Token: 0x06004165 RID: 16741 RVA: 0x000DCB60 File Offset: 0x000DAD60
		public CalendarActionFolderIdResponse RenameCalendar(Microsoft.Exchange.Services.Core.Types.ItemId itemId, string newCalendarName)
		{
			return new RenameCalendarCommand(CallContext.Current, itemId, newCalendarName).Execute();
		}

		// Token: 0x06004166 RID: 16742 RVA: 0x000DCB73 File Offset: 0x000DAD73
		public CalendarActionResponse DeleteCalendar(Microsoft.Exchange.Services.Core.Types.ItemId itemId)
		{
			return new DeleteCalendarCommand(CallContext.Current, itemId).Execute();
		}

		// Token: 0x06004167 RID: 16743 RVA: 0x000DCB85 File Offset: 0x000DAD85
		public CalendarActionItemIdResponse SetCalendarColor(Microsoft.Exchange.Services.Core.Types.ItemId itemId, CalendarColor calendarColor)
		{
			return new SetCalendarColorCommand(CallContext.Current, itemId, calendarColor).Execute();
		}

		// Token: 0x06004168 RID: 16744 RVA: 0x000DCB98 File Offset: 0x000DAD98
		public CalendarActionResponse MoveCalendar(FolderId calendarToMove, string parentGroupId, FolderId calendarBefore)
		{
			return new MoveCalendarCommand(CallContext.Current, calendarToMove, parentGroupId, calendarBefore).Execute();
		}

		// Token: 0x06004169 RID: 16745 RVA: 0x000DCBAC File Offset: 0x000DADAC
		public CalendarActionResponse SetCalendarGroupOrder(string groupToPosition, string beforeGroup)
		{
			return new SetCalendarGroupOrderCommand(CallContext.Current, groupToPosition, beforeGroup).Execute();
		}

		// Token: 0x0600416A RID: 16746 RVA: 0x000DCBBF File Offset: 0x000DADBF
		public GetCalendarFoldersResponse GetCalendarFolders()
		{
			return new GetCalendarFoldersCommand(CallContext.Current).Execute();
		}

		// Token: 0x0600416B RID: 16747 RVA: 0x000DCBD0 File Offset: 0x000DADD0
		public GetCalendarFolderConfigurationResponse GetCalendarFolderConfiguration(GetCalendarFolderConfigurationRequest request)
		{
			return new GetCalendarFolderConfigurationCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600416C RID: 16748 RVA: 0x000DCBE2 File Offset: 0x000DADE2
		public GetUserAvailabilityInternalJsonResponse GetUserAvailabilityInternal(GetUserAvailabilityInternalJsonRequest request)
		{
			return new GetUserAvailabilityInternalCommand(CallContext.Current, request.Body).Execute();
		}

		// Token: 0x0600416D RID: 16749 RVA: 0x000DCBFC File Offset: 0x000DADFC
		public SyncCalendarResponse SyncCalendar(SyncCalendarParameters request)
		{
			SyncCalendar syncCalendar = new SyncCalendar(CallContext.Current, request);
			return syncCalendar.Execute();
		}

		// Token: 0x0600416E RID: 16750 RVA: 0x000DCC1C File Offset: 0x000DAE1C
		public UpdateUserConfigurationResponse UpdateUserConfiguration(UpdateUserConfigurationRequest request)
		{
			UpdateUserConfiguration updateUserConfiguration = new UpdateUserConfiguration(CallContext.Current, request);
			updateUserConfiguration.Execute();
			return (UpdateUserConfigurationResponse)updateUserConfiguration.GetResponse();
		}

		// Token: 0x0600416F RID: 16751 RVA: 0x000DCC47 File Offset: 0x000DAE47
		public bool SendReadReceipt(Microsoft.Exchange.Services.Core.Types.ItemId itemId)
		{
			return new SendReadReceipt(CallContext.Current, itemId).Execute();
		}

		// Token: 0x06004170 RID: 16752 RVA: 0x000DCC5C File Offset: 0x000DAE5C
		public SuiteStorageJsonResponse ProcessSuiteStorage(SuiteStorageJsonRequest request)
		{
			return new SuiteStorageJsonResponse
			{
				Body = new ProcessSuiteStorage(CallContext.Current, request.Body).Execute()
			};
		}

		// Token: 0x06004171 RID: 16753 RVA: 0x000DCC8C File Offset: 0x000DAE8C
		public SuiteStorageJsonResponse ProcessO365SuiteStorage(SuiteStorageJsonRequest request)
		{
			return new SuiteStorageJsonResponse
			{
				Body = new ProcessSuiteStorage(CallContext.Current, request.Body).Execute()
			};
		}

		// Token: 0x06004172 RID: 16754 RVA: 0x000DCCBB File Offset: 0x000DAEBB
		public IAsyncResult BeginGetWeatherForecast(GetWeatherForecastJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return new GetWeatherForecast(CallContext.Current, request.Body, asyncCallback, asyncState, new WeatherService(WeatherConfigurationCache.Instance)).Execute();
		}

		// Token: 0x06004173 RID: 16755 RVA: 0x000DCCE0 File Offset: 0x000DAEE0
		public GetWeatherForecastJsonResponse EndGetWeatherForecast(IAsyncResult result)
		{
			ServiceAsyncResult<Task<GetWeatherForecastResponse>> serviceAsyncResult = result as ServiceAsyncResult<Task<GetWeatherForecastResponse>>;
			if (serviceAsyncResult == null || serviceAsyncResult.Data == null)
			{
				throw new FaultException("IAsyncResult in EndGetWeatherForecast was null or not of the expected type.");
			}
			if (serviceAsyncResult.Data.IsFaulted)
			{
				throw new FaultException((serviceAsyncResult.Data.Exception != null) ? serviceAsyncResult.Data.Exception.InnerExceptions[0].Message : CoreResources.GetLocalizedString((CoreResources.IDs)2933471333U));
			}
			return new GetWeatherForecastJsonResponse
			{
				Body = serviceAsyncResult.Data.Result
			};
		}

		// Token: 0x06004174 RID: 16756 RVA: 0x000DCD6E File Offset: 0x000DAF6E
		public IAsyncResult BeginFindWeatherLocations(FindWeatherLocationsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return new FindWeatherLocations(CallContext.Current, request.Body, asyncCallback, asyncState, new WeatherService(WeatherConfigurationCache.Instance)).Execute();
		}

		// Token: 0x06004175 RID: 16757 RVA: 0x000DCD94 File Offset: 0x000DAF94
		public FindWeatherLocationsJsonResponse EndFindWeatherLocations(IAsyncResult result)
		{
			ServiceAsyncResult<Task<FindWeatherLocationsResponse>> serviceAsyncResult = result as ServiceAsyncResult<Task<FindWeatherLocationsResponse>>;
			if (serviceAsyncResult == null || serviceAsyncResult.Data == null)
			{
				throw new FaultException("IAsyncResult in EndFindWeatherLocations was null or not of the expected type.");
			}
			if (serviceAsyncResult.Data.IsFaulted)
			{
				throw new FaultException((serviceAsyncResult.Data.Exception != null) ? serviceAsyncResult.Data.Exception.InnerExceptions[0].Message : CoreResources.GetLocalizedString(CoreResources.IDs.MessageCouldNotFindWeatherLocations));
			}
			return new FindWeatherLocationsJsonResponse
			{
				Body = serviceAsyncResult.Data.Result
			};
		}

		// Token: 0x06004176 RID: 16758 RVA: 0x000DCE22 File Offset: 0x000DB022
		public DisableAppDataResponse DisableApp(DisableAppDataRequest request)
		{
			return new DisableAppCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004177 RID: 16759 RVA: 0x000DCE34 File Offset: 0x000DB034
		public EnableAppDataResponse EnableApp(EnableAppDataRequest request)
		{
			return new EnableAppCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004178 RID: 16760 RVA: 0x000DCE46 File Offset: 0x000DB046
		public RemoveAppDataResponse RemoveApp(RemoveAppDataRequest request)
		{
			return new RemoveAppCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004179 RID: 16761 RVA: 0x000DCE58 File Offset: 0x000DB058
		public GetCalendarNotificationResponse GetCalendarNotification()
		{
			return new GetCalendarNotificationCommand(CallContext.Current).Execute();
		}

		// Token: 0x0600417A RID: 16762 RVA: 0x000DCE69 File Offset: 0x000DB069
		public OptionsResponseBase SetCalendarNotification(SetCalendarNotificationRequest request)
		{
			return new SetCalendarNotificationCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600417B RID: 16763 RVA: 0x000DCE7B File Offset: 0x000DB07B
		public GetCalendarProcessingResponse GetCalendarProcessing()
		{
			return new GetCalendarProcessingCommand(CallContext.Current).Execute();
		}

		// Token: 0x0600417C RID: 16764 RVA: 0x000DCE8C File Offset: 0x000DB08C
		public OptionsResponseBase SetCalendarProcessing(SetCalendarProcessingRequest request)
		{
			return new SetCalendarProcessingCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600417D RID: 16765 RVA: 0x000DCE9E File Offset: 0x000DB09E
		public GetCASMailboxResponse GetCASMailbox()
		{
			return new GetCASMailboxCommand(CallContext.Current, null).Execute();
		}

		// Token: 0x0600417E RID: 16766 RVA: 0x000DCEB0 File Offset: 0x000DB0B0
		public GetCASMailboxResponse GetCASMailbox2(GetCASMailboxRequest request)
		{
			return new GetCASMailboxCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600417F RID: 16767 RVA: 0x000DCEC2 File Offset: 0x000DB0C2
		public SetCASMailboxResponse SetCASMailbox(SetCASMailboxRequest request)
		{
			return new SetCASMailboxCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004180 RID: 16768 RVA: 0x000DCED4 File Offset: 0x000DB0D4
		public GetConnectedAccountsResponse GetConnectedAccounts(GetConnectedAccountsRequest request)
		{
			return new GetConnectedAccountsCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004181 RID: 16769 RVA: 0x000DCEE6 File Offset: 0x000DB0E6
		public GetConnectSubscriptionResponse GetConnectSubscription(GetConnectSubscriptionRequest request)
		{
			return new GetConnectSubscriptionCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004182 RID: 16770 RVA: 0x000DCEF8 File Offset: 0x000DB0F8
		public NewConnectSubscriptionResponse NewConnectSubscription(NewConnectSubscriptionRequest request)
		{
			return new NewConnectSubscriptionCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004183 RID: 16771 RVA: 0x000DCF0A File Offset: 0x000DB10A
		public RemoveConnectSubscriptionResponse RemoveConnectSubscription(RemoveConnectSubscriptionRequest request)
		{
			return new RemoveConnectSubscriptionCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004184 RID: 16772 RVA: 0x000DCF1C File Offset: 0x000DB11C
		public SetConnectSubscriptionResponse SetConnectSubscription(SetConnectSubscriptionRequest request)
		{
			return new SetConnectSubscriptionCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004185 RID: 16773 RVA: 0x000DCF2E File Offset: 0x000DB12E
		public GetHotmailSubscriptionResponse GetHotmailSubscription(IdentityRequest request)
		{
			return new GetHotmailSubscriptionCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004186 RID: 16774 RVA: 0x000DCF40 File Offset: 0x000DB140
		public OptionsResponseBase SetHotmailSubscription(SetHotmailSubscriptionRequest request)
		{
			return new SetHotmailSubscriptionCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004187 RID: 16775 RVA: 0x000DCF52 File Offset: 0x000DB152
		public GetImapSubscriptionResponse GetImapSubscription(IdentityRequest request)
		{
			return new GetImapSubscriptionCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004188 RID: 16776 RVA: 0x000DCF64 File Offset: 0x000DB164
		public NewImapSubscriptionResponse NewImapSubscription(NewImapSubscriptionRequest request)
		{
			return new NewImapSubscriptionCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004189 RID: 16777 RVA: 0x000DCF76 File Offset: 0x000DB176
		public OptionsResponseBase SetImapSubscription(SetImapSubscriptionRequest request)
		{
			return new SetImapSubscriptionCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600418A RID: 16778 RVA: 0x000DCF88 File Offset: 0x000DB188
		public ImportContactListResponse ImportContactList(ImportContactListRequest request)
		{
			return new ImportContactListCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600418B RID: 16779 RVA: 0x000DCF9A File Offset: 0x000DB19A
		public DisableInboxRuleResponse DisableInboxRule(DisableInboxRuleRequest request)
		{
			return new DisableInboxRuleCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600418C RID: 16780 RVA: 0x000DCFAC File Offset: 0x000DB1AC
		public EnableInboxRuleResponse EnableInboxRule(EnableInboxRuleRequest request)
		{
			return new EnableInboxRuleCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600418D RID: 16781 RVA: 0x000DCFBE File Offset: 0x000DB1BE
		public GetInboxRuleResponse GetInboxRule(GetInboxRuleRequest request)
		{
			return new GetInboxRuleCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600418E RID: 16782 RVA: 0x000DCFD0 File Offset: 0x000DB1D0
		public NewInboxRuleResponse NewInboxRule(NewInboxRuleRequest request)
		{
			return new NewInboxRuleCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600418F RID: 16783 RVA: 0x000DCFE2 File Offset: 0x000DB1E2
		public RemoveInboxRuleResponse RemoveInboxRule(RemoveInboxRuleRequest request)
		{
			return new RemoveInboxRuleCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004190 RID: 16784 RVA: 0x000DCFF4 File Offset: 0x000DB1F4
		public SetInboxRuleResponse SetInboxRule(SetInboxRuleRequest request)
		{
			return new SetInboxRuleCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004191 RID: 16785 RVA: 0x000DD006 File Offset: 0x000DB206
		public GetMailboxResponse GetMailboxByIdentity(IdentityRequest request)
		{
			return new GetMailboxByIdentityCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004192 RID: 16786 RVA: 0x000DD018 File Offset: 0x000DB218
		public OptionsResponseBase SetMailbox(SetMailboxRequest request)
		{
			return new SetMailboxCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004193 RID: 16787 RVA: 0x000DD02A File Offset: 0x000DB22A
		public GetMailboxAutoReplyConfigurationResponse GetMailboxAutoReplyConfiguration()
		{
			return new GetMailboxAutoReplyConfigurationCommand(CallContext.Current).Execute();
		}

		// Token: 0x06004194 RID: 16788 RVA: 0x000DD03B File Offset: 0x000DB23B
		public OptionsResponseBase SetMailboxAutoReplyConfiguration(SetMailboxAutoReplyConfigurationRequest request)
		{
			return new SetMailboxAutoReplyConfigurationCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004195 RID: 16789 RVA: 0x000DD04D File Offset: 0x000DB24D
		public GetMailboxCalendarConfigurationResponse GetMailboxCalendarConfiguration()
		{
			return new GetMailboxCalendarConfigurationCommand(CallContext.Current).Execute();
		}

		// Token: 0x06004196 RID: 16790 RVA: 0x000DD05E File Offset: 0x000DB25E
		public OptionsResponseBase SetMailboxCalendarConfiguration(SetMailboxCalendarConfigurationRequest request)
		{
			return new SetMailboxCalendarConfigurationCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004197 RID: 16791 RVA: 0x000DD070 File Offset: 0x000DB270
		public GetMailboxJunkEmailConfigurationResponse GetMailboxJunkEmailConfiguration()
		{
			return new GetMailboxJunkEmailConfigurationCommand(CallContext.Current).Execute();
		}

		// Token: 0x06004198 RID: 16792 RVA: 0x000DD081 File Offset: 0x000DB281
		public OptionsResponseBase SetMailboxJunkEmailConfiguration(SetMailboxJunkEmailConfigurationRequest request)
		{
			return new SetMailboxJunkEmailConfigurationCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06004199 RID: 16793 RVA: 0x000DD093 File Offset: 0x000DB293
		public GetMailboxMessageConfigurationResponse GetMailboxMessageConfiguration()
		{
			return new GetMailboxMessageConfigurationCommand(CallContext.Current).Execute();
		}

		// Token: 0x0600419A RID: 16794 RVA: 0x000DD0A4 File Offset: 0x000DB2A4
		public OptionsResponseBase SetMailboxMessageConfiguration(SetMailboxMessageConfigurationRequest request)
		{
			return new SetMailboxMessageConfigurationCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600419B RID: 16795 RVA: 0x000DD0B6 File Offset: 0x000DB2B6
		public GetMailboxRegionalConfigurationResponse GetMailboxRegionalConfiguration(GetMailboxRegionalConfigurationRequest request)
		{
			return new GetMailboxRegionalConfigurationCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600419C RID: 16796 RVA: 0x000DD0C8 File Offset: 0x000DB2C8
		public SetMailboxRegionalConfigurationResponse SetMailboxRegionalConfiguration(SetMailboxRegionalConfigurationRequest request)
		{
			return new SetMailboxRegionalConfigurationCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600419D RID: 16797 RVA: 0x000DD0DA File Offset: 0x000DB2DA
		public GetMessageCategoryResponse GetMessageCategory()
		{
			return new GetMessageCategoryCommand(CallContext.Current).Execute();
		}

		// Token: 0x0600419E RID: 16798 RVA: 0x000DD0EB File Offset: 0x000DB2EB
		public GetMessageClassificationResponse GetMessageClassification()
		{
			return new GetMessageClassificationCommand(CallContext.Current).Execute();
		}

		// Token: 0x0600419F RID: 16799 RVA: 0x000DD0FC File Offset: 0x000DB2FC
		public GetAccountInformationResponse GetAccountInformation(GetAccountInformationRequest request)
		{
			return new GetAccountInformationCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x060041A0 RID: 16800 RVA: 0x000DD10E File Offset: 0x000DB30E
		public SetUserResponse SetUser(SetUserRequest request)
		{
			return new SetUserCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x060041A1 RID: 16801 RVA: 0x000DD120 File Offset: 0x000DB320
		public GetSocialNetworksOAuthInfoResponse GetConnectToSocialNetworksOAuthInfo(GetSocialNetworksOAuthInfoRequest request)
		{
			return new GetConnectToSocialNetworksOAuthInfoCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x060041A2 RID: 16802 RVA: 0x000DD132 File Offset: 0x000DB332
		public GetPopSubscriptionResponse GetPopSubscription(IdentityRequest request)
		{
			return new GetPopSubscriptionCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x060041A3 RID: 16803 RVA: 0x000DD144 File Offset: 0x000DB344
		public NewPopSubscriptionResponse NewPopSubscription(NewPopSubscriptionRequest request)
		{
			return new NewPopSubscriptionCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x060041A4 RID: 16804 RVA: 0x000DD156 File Offset: 0x000DB356
		public OptionsResponseBase SetPopSubscription(SetPopSubscriptionRequest request)
		{
			return new SetPopSubscriptionCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x060041A5 RID: 16805 RVA: 0x000DD168 File Offset: 0x000DB368
		public OptionsResponseBase AddActiveRetentionPolicyTags(IdentityCollectionRequest request)
		{
			return new AddActiveRetentionPolicyTagsCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x060041A6 RID: 16806 RVA: 0x000DD17A File Offset: 0x000DB37A
		public GetRetentionPolicyTagsResponse GetActiveRetentionPolicyTags(GetRetentionPolicyTagsRequest request)
		{
			return new GetActiveRetentionPolicyTagsCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x060041A7 RID: 16807 RVA: 0x000DD18C File Offset: 0x000DB38C
		public GetRetentionPolicyTagsResponse GetAvailableRetentionPolicyTags(GetRetentionPolicyTagsRequest request)
		{
			return new GetAvailableRetentionPolicyTagsCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x000DD19E File Offset: 0x000DB39E
		public OptionsResponseBase RemoveActiveRetentionPolicyTags(IdentityCollectionRequest request)
		{
			return new RemoveActiveRetentionPolicyTagsCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x060041A9 RID: 16809 RVA: 0x000DD1B0 File Offset: 0x000DB3B0
		public GetSendAddressResponse GetSendAddress()
		{
			return new GetSendAddressCommand(CallContext.Current).Execute();
		}

		// Token: 0x060041AA RID: 16810 RVA: 0x000DD1C1 File Offset: 0x000DB3C1
		public GetSubscriptionResponse GetSubscription()
		{
			return new GetSubscriptionCommand(CallContext.Current).Execute();
		}

		// Token: 0x060041AB RID: 16811 RVA: 0x000DD1D2 File Offset: 0x000DB3D2
		public NewSubscriptionResponse NewSubscription(NewSubscriptionRequest request)
		{
			return new NewSubscriptionCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x060041AC RID: 16812 RVA: 0x000DD1E4 File Offset: 0x000DB3E4
		public OptionsResponseBase RemoveSubscription(IdentityRequest request)
		{
			return new RemoveSubscriptionCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x060041AD RID: 16813 RVA: 0x000DD1F6 File Offset: 0x000DB3F6
		public LikeItemResponse LikeItem(LikeItemRequest request)
		{
			return new LikeItemCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x060041AE RID: 16814 RVA: 0x000DD208 File Offset: 0x000DB408
		public GetLikersResponseMessage GetLikers(GetLikersRequest request)
		{
			return new GetLikers(CallContext.Current, request).Execute();
		}

		// Token: 0x060041AF RID: 16815 RVA: 0x000DD21C File Offset: 0x000DB41C
		public GetAggregatedAccountResponse GetAggregatedAccount(GetAggregatedAccountRequest request)
		{
			GetAggregatedAccount getAggregatedAccount = new GetAggregatedAccount(CallContext.Current, request);
			if (getAggregatedAccount.PreExecute())
			{
				for (int i = 0; i < getAggregatedAccount.StepCount; i++)
				{
					CallContext.Current.Budget.CheckOverBudget();
					TaskExecuteResult taskExecuteResult = getAggregatedAccount.ExecuteStep();
					if (taskExecuteResult == TaskExecuteResult.ProcessingComplete)
					{
						break;
					}
				}
				return (GetAggregatedAccountResponse)getAggregatedAccount.PostExecute();
			}
			return null;
		}

		// Token: 0x060041B0 RID: 16816 RVA: 0x000DD278 File Offset: 0x000DB478
		public AddAggregatedAccountResponse AddAggregatedAccount(AddAggregatedAccountRequest request)
		{
			AddAggregatedAccount addAggregatedAccount = new AddAggregatedAccount(CallContext.Current, request);
			if (addAggregatedAccount.PreExecute())
			{
				for (int i = 0; i < addAggregatedAccount.StepCount; i++)
				{
					CallContext.Current.Budget.CheckOverBudget();
					TaskExecuteResult taskExecuteResult = addAggregatedAccount.ExecuteStep();
					if (taskExecuteResult == TaskExecuteResult.ProcessingComplete)
					{
						break;
					}
				}
				return (AddAggregatedAccountResponse)addAggregatedAccount.PostExecute();
			}
			return null;
		}

		// Token: 0x060041B1 RID: 16817 RVA: 0x000DD2D1 File Offset: 0x000DB4D1
		public IAsyncResult BeginCancelCalendarEvent(CancelCalendarEventJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<CancelCalendarEventResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041B2 RID: 16818 RVA: 0x000DD2FB File Offset: 0x000DB4FB
		public CancelCalendarEventJsonResponse EndCancelCalendarEvent(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<CancelCalendarEventJsonResponse, CancelCalendarEventResponse>(result, (CancelCalendarEventResponse body) => new CancelCalendarEventJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041B3 RID: 16819 RVA: 0x000DD320 File Offset: 0x000DB520
		public GetMobileDeviceStatisticsResponse GetMobileDeviceStatistics(GetMobileDeviceStatisticsRequest request)
		{
			return new GetMobileDeviceStatisticsCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x060041B4 RID: 16820 RVA: 0x000DD332 File Offset: 0x000DB532
		public RemoveMobileDeviceResponse RemoveMobileDevice(RemoveMobileDeviceRequest request)
		{
			return new RemoveMobileDeviceCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x060041B5 RID: 16821 RVA: 0x000DD344 File Offset: 0x000DB544
		public CalendarActionFolderIdResponse EnableBirthdayCalendar()
		{
			return new EnableBirthdayCalendarCommand(CallContext.Current).Execute();
		}

		// Token: 0x060041B6 RID: 16822 RVA: 0x000DD355 File Offset: 0x000DB555
		public CalendarActionResponse DisableBirthdayCalendar()
		{
			return new DisableBirthdayCalendarCommand(CallContext.Current).Execute();
		}

		// Token: 0x060041B7 RID: 16823 RVA: 0x000DD366 File Offset: 0x000DB566
		public CalendarActionResponse RemoveBirthdayEvent(Microsoft.Exchange.Services.Core.Types.ItemId contactId)
		{
			return new RemoveBirthdayEventCommand(CallContext.Current, contactId).Execute();
		}

		// Token: 0x060041B8 RID: 16824 RVA: 0x000DD378 File Offset: 0x000DB578
		public ClearMobileDeviceResponse ClearMobileDevice(ClearMobileDeviceRequest request)
		{
			return new ClearMobileDeviceCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x060041B9 RID: 16825 RVA: 0x000DD38A File Offset: 0x000DB58A
		public ClearTextMessagingAccountResponse ClearTextMessagingAccount(ClearTextMessagingAccountRequest request)
		{
			return new ClearTextMessagingAccountCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x060041BA RID: 16826 RVA: 0x000DD39C File Offset: 0x000DB59C
		public GetTextMessagingAccountResponse GetTextMessagingAccount(GetTextMessagingAccountRequest request)
		{
			return new GetTextMessagingAccountCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x060041BB RID: 16827 RVA: 0x000DD3AE File Offset: 0x000DB5AE
		public SetTextMessagingAccountResponse SetTextMessagingAccount(SetTextMessagingAccountRequest request)
		{
			return new SetTextMessagingAccountCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x060041BC RID: 16828 RVA: 0x000DD3C0 File Offset: 0x000DB5C0
		public CompareTextMessagingVerificationCodeResponse CompareTextMessagingVerificationCode(CompareTextMessagingVerificationCodeRequest request)
		{
			return new CompareTextMessagingVerificationCodeCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x060041BD RID: 16829 RVA: 0x000DD3D2 File Offset: 0x000DB5D2
		public SendTextMessagingVerificationCodeResponse SendTextMessagingVerificationCode(SendTextMessagingVerificationCodeRequest request)
		{
			return new SendTextMessagingVerificationCodeCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x060041BE RID: 16830 RVA: 0x000DD3E4 File Offset: 0x000DB5E4
		public GetAllowedOptionsResponse GetAllowedOptions(GetAllowedOptionsRequest request)
		{
			return new GetAllowedOptionsCommand(CallContext.Current).Execute();
		}

		// Token: 0x060041C0 RID: 16832 RVA: 0x000DD3FD File Offset: 0x000DB5FD
		public IAsyncResult BeginConvertId(ConvertIdJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<ConvertIdResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041C1 RID: 16833 RVA: 0x000DD427 File Offset: 0x000DB627
		public ConvertIdJsonResponse EndConvertId(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<ConvertIdJsonResponse, ConvertIdResponse>(result, (ConvertIdResponse body) => new ConvertIdJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041C2 RID: 16834 RVA: 0x000DD44C File Offset: 0x000DB64C
		public IAsyncResult BeginUploadItems(UploadItemsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041C3 RID: 16835 RVA: 0x000DD453 File Offset: 0x000DB653
		public UploadItemsJsonResponse EndUploadItems(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041C4 RID: 16836 RVA: 0x000DD45A File Offset: 0x000DB65A
		public IAsyncResult BeginExportItems(ExportItemsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041C5 RID: 16837 RVA: 0x000DD461 File Offset: 0x000DB661
		public ExportItemsJsonResponse EndExportItems(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041C6 RID: 16838 RVA: 0x000DD468 File Offset: 0x000DB668
		public IAsyncResult BeginGetFolder(GetFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetFolderResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041C7 RID: 16839 RVA: 0x000DD493 File Offset: 0x000DB693
		public GetFolderJsonResponse EndGetFolder(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetFolderJsonResponse, GetFolderResponse>(result, (GetFolderResponse body) => new GetFolderJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041C8 RID: 16840 RVA: 0x000DD4B8 File Offset: 0x000DB6B8
		public IAsyncResult BeginCreateFolder(CreateFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<CreateFolderResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041C9 RID: 16841 RVA: 0x000DD4E3 File Offset: 0x000DB6E3
		public CreateFolderJsonResponse EndCreateFolder(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<CreateFolderJsonResponse, CreateFolderResponse>(result, (CreateFolderResponse body) => new CreateFolderJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041CA RID: 16842 RVA: 0x000DD508 File Offset: 0x000DB708
		public IAsyncResult BeginDeleteFolder(DeleteFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<DeleteFolderResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041CB RID: 16843 RVA: 0x000DD533 File Offset: 0x000DB733
		public DeleteFolderJsonResponse EndDeleteFolder(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<DeleteFolderJsonResponse, DeleteFolderResponse>(result, (DeleteFolderResponse body) => new DeleteFolderJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041CC RID: 16844 RVA: 0x000DD558 File Offset: 0x000DB758
		public IAsyncResult BeginEmptyFolder(EmptyFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<EmptyFolderResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041CD RID: 16845 RVA: 0x000DD583 File Offset: 0x000DB783
		public EmptyFolderJsonResponse EndEmptyFolder(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<EmptyFolderJsonResponse, EmptyFolderResponse>(result, (EmptyFolderResponse body) => new EmptyFolderJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041CE RID: 16846 RVA: 0x000DD5A8 File Offset: 0x000DB7A8
		public IAsyncResult BeginUpdateFolder(UpdateFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<UpdateFolderResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041CF RID: 16847 RVA: 0x000DD5D3 File Offset: 0x000DB7D3
		public UpdateFolderJsonResponse EndUpdateFolder(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<UpdateFolderJsonResponse, UpdateFolderResponse>(result, (UpdateFolderResponse body) => new UpdateFolderJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041D0 RID: 16848 RVA: 0x000DD5F8 File Offset: 0x000DB7F8
		public IAsyncResult BeginMoveFolder(MoveFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<MoveFolderResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041D1 RID: 16849 RVA: 0x000DD623 File Offset: 0x000DB823
		public MoveFolderJsonResponse EndMoveFolder(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<MoveFolderJsonResponse, MoveFolderResponse>(result, (MoveFolderResponse body) => new MoveFolderJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041D2 RID: 16850 RVA: 0x000DD648 File Offset: 0x000DB848
		public IAsyncResult BeginCopyFolder(CopyFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<CopyFolderResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041D3 RID: 16851 RVA: 0x000DD673 File Offset: 0x000DB873
		public CopyFolderJsonResponse EndCopyFolder(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<CopyFolderJsonResponse, CopyFolderResponse>(result, (CopyFolderResponse body) => new CopyFolderJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041D4 RID: 16852 RVA: 0x000DD698 File Offset: 0x000DB898
		public IAsyncResult BeginFindItem(FindItemJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			JsonService.AdjustMaxRowsIfNeeded(request.Body.Paging);
			return request.Body.ValidateAndSubmit<FindItemResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041D5 RID: 16853 RVA: 0x000DD6D3 File Offset: 0x000DB8D3
		public FindItemJsonResponse EndFindItem(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<FindItemJsonResponse, FindItemResponse>(result, (FindItemResponse body) => new FindItemJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041D6 RID: 16854 RVA: 0x000DD6F8 File Offset: 0x000DB8F8
		public IAsyncResult BeginFindFolder(FindFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<FindFolderResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041D7 RID: 16855 RVA: 0x000DD723 File Offset: 0x000DB923
		public FindFolderJsonResponse EndFindFolder(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<FindFolderJsonResponse, FindFolderResponse>(result, (FindFolderResponse body) => new FindFolderJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041D8 RID: 16856 RVA: 0x000DD748 File Offset: 0x000DB948
		public IAsyncResult BeginGetItem(GetItemJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetItemResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041D9 RID: 16857 RVA: 0x000DD773 File Offset: 0x000DB973
		public GetItemJsonResponse EndGetItem(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetItemJsonResponse, GetItemResponse>(result, (GetItemResponse body) => new GetItemJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041DA RID: 16858 RVA: 0x000DD798 File Offset: 0x000DB998
		public IAsyncResult BeginCreateItem(CreateItemJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<CreateItemResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041DB RID: 16859 RVA: 0x000DD7C3 File Offset: 0x000DB9C3
		public CreateItemJsonResponse EndCreateItem(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<CreateItemJsonResponse, CreateItemResponse>(result, (CreateItemResponse body) => new CreateItemJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041DC RID: 16860 RVA: 0x000DD7E8 File Offset: 0x000DB9E8
		public IAsyncResult BeginDeleteItem(DeleteItemJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<DeleteItemResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041DD RID: 16861 RVA: 0x000DD813 File Offset: 0x000DBA13
		public DeleteItemJsonResponse EndDeleteItem(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<DeleteItemJsonResponse, DeleteItemResponse>(result, (DeleteItemResponse body) => new DeleteItemJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041DE RID: 16862 RVA: 0x000DD838 File Offset: 0x000DBA38
		public IAsyncResult BeginUpdateItem(UpdateItemJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<UpdateItemResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041DF RID: 16863 RVA: 0x000DD863 File Offset: 0x000DBA63
		public UpdateItemJsonResponse EndUpdateItem(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<UpdateItemJsonResponse, UpdateItemResponse>(result, (UpdateItemResponse body) => new UpdateItemJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041E0 RID: 16864 RVA: 0x000DD888 File Offset: 0x000DBA88
		public IAsyncResult BeginSendItem(SendItemJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<SendItemResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041E1 RID: 16865 RVA: 0x000DD8B3 File Offset: 0x000DBAB3
		public SendItemJsonResponse EndSendItem(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<SendItemJsonResponse, SendItemResponse>(result, (SendItemResponse body) => new SendItemJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041E2 RID: 16866 RVA: 0x000DD8D8 File Offset: 0x000DBAD8
		public IAsyncResult BeginMoveItem(MoveItemJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<MoveItemResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041E3 RID: 16867 RVA: 0x000DD903 File Offset: 0x000DBB03
		public MoveItemJsonResponse EndMoveItem(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<MoveItemJsonResponse, MoveItemResponse>(result, (MoveItemResponse body) => new MoveItemJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041E4 RID: 16868 RVA: 0x000DD928 File Offset: 0x000DBB28
		public IAsyncResult BeginCopyItem(CopyItemJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<CopyItemResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041E5 RID: 16869 RVA: 0x000DD953 File Offset: 0x000DBB53
		public CopyItemJsonResponse EndCopyItem(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<CopyItemJsonResponse, CopyItemResponse>(result, (CopyItemResponse body) => new CopyItemJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041E6 RID: 16870 RVA: 0x000DD978 File Offset: 0x000DBB78
		public IAsyncResult BeginCreateAttachment(CreateAttachmentJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<CreateAttachmentResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041E7 RID: 16871 RVA: 0x000DD9A3 File Offset: 0x000DBBA3
		public CreateAttachmentJsonResponse EndCreateAttachment(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<CreateAttachmentJsonResponse, CreateAttachmentResponse>(result, (CreateAttachmentResponse body) => new CreateAttachmentJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041E8 RID: 16872 RVA: 0x000DD9C8 File Offset: 0x000DBBC8
		public IAsyncResult BeginDeleteAttachment(DeleteAttachmentJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<DeleteAttachmentResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041E9 RID: 16873 RVA: 0x000DD9F3 File Offset: 0x000DBBF3
		public DeleteAttachmentJsonResponse EndDeleteAttachment(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<DeleteAttachmentJsonResponse, DeleteAttachmentResponse>(result, (DeleteAttachmentResponse body) => new DeleteAttachmentJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041EA RID: 16874 RVA: 0x000DDA18 File Offset: 0x000DBC18
		public IAsyncResult BeginGetAttachment(GetAttachmentJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetAttachmentResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041EB RID: 16875 RVA: 0x000DDA43 File Offset: 0x000DBC43
		public GetAttachmentJsonResponse EndGetAttachment(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetAttachmentJsonResponse, GetAttachmentResponse>(result, (GetAttachmentResponse body) => new GetAttachmentJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041EC RID: 16876 RVA: 0x000DDA68 File Offset: 0x000DBC68
		public IAsyncResult BeginGetClientAccessToken(GetClientAccessTokenJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetClientAccessTokenResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041ED RID: 16877 RVA: 0x000DDA93 File Offset: 0x000DBC93
		public GetClientAccessTokenJsonResponse EndGetClientAccessToken(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetClientAccessTokenJsonResponse, GetClientAccessTokenResponse>(result, (GetClientAccessTokenResponse body) => new GetClientAccessTokenJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041EE RID: 16878 RVA: 0x000DDAB8 File Offset: 0x000DBCB8
		public IAsyncResult BeginResolveNames(ResolveNamesJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<ResolveNamesResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041EF RID: 16879 RVA: 0x000DDAE3 File Offset: 0x000DBCE3
		public ResolveNamesJsonResponse EndResolveNames(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<ResolveNamesJsonResponse, ResolveNamesResponse>(result, (ResolveNamesResponse body) => new ResolveNamesJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041F0 RID: 16880 RVA: 0x000DDB08 File Offset: 0x000DBD08
		public IAsyncResult BeginExpandDL(ExpandDLJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041F1 RID: 16881 RVA: 0x000DDB0F File Offset: 0x000DBD0F
		public ExpandDLJsonResponse EndExpandDL(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041F2 RID: 16882 RVA: 0x000DDB16 File Offset: 0x000DBD16
		public IAsyncResult BeginGetServerTimeZones(GetServerTimeZonesJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetServerTimeZonesResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041F3 RID: 16883 RVA: 0x000DDB43 File Offset: 0x000DBD43
		public GetServerTimeZonesJsonResponse EndGetServerTimeZones(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetServerTimeZonesJsonResponse, GetServerTimeZonesResponse>(result, (GetServerTimeZonesResponse body) => new GetServerTimeZonesJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041F4 RID: 16884 RVA: 0x000DDB68 File Offset: 0x000DBD68
		public IAsyncResult BeginCreateManagedFolder(CreateManagedFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041F5 RID: 16885 RVA: 0x000DDB6F File Offset: 0x000DBD6F
		public CreateManagedFolderJsonResponse EndCreateManagedFolder(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041F6 RID: 16886 RVA: 0x000DDB76 File Offset: 0x000DBD76
		public IAsyncResult BeginSubscribe(SubscribeJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<SubscribeResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041F7 RID: 16887 RVA: 0x000DDBA3 File Offset: 0x000DBDA3
		public SubscribeJsonResponse EndSubscribe(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<SubscribeJsonResponse, SubscribeResponse>(result, (SubscribeResponse body) => new SubscribeJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041F8 RID: 16888 RVA: 0x000DDBC8 File Offset: 0x000DBDC8
		public IAsyncResult BeginUnsubscribe(UnsubscribeJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<UnsubscribeResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041F9 RID: 16889 RVA: 0x000DDBF3 File Offset: 0x000DBDF3
		public UnsubscribeJsonResponse EndUnsubscribe(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<UnsubscribeJsonResponse, UnsubscribeResponse>(result, (UnsubscribeResponse body) => new UnsubscribeJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041FA RID: 16890 RVA: 0x000DDC18 File Offset: 0x000DBE18
		public IAsyncResult BeginGetEvents(GetEventsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetEventsResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041FB RID: 16891 RVA: 0x000DDC43 File Offset: 0x000DBE43
		public GetEventsJsonResponse EndGetEvents(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetEventsJsonResponse, GetEventsResponse>(result, (GetEventsResponse body) => new GetEventsJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041FC RID: 16892 RVA: 0x000DDC68 File Offset: 0x000DBE68
		public IAsyncResult BeginSyncFolderHierarchy(SyncFolderHierarchyJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<SyncFolderHierarchyResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041FD RID: 16893 RVA: 0x000DDC93 File Offset: 0x000DBE93
		public SyncFolderHierarchyJsonResponse EndSyncFolderHierarchy(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<SyncFolderHierarchyJsonResponse, SyncFolderHierarchyResponse>(result, (SyncFolderHierarchyResponse body) => new SyncFolderHierarchyJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x060041FE RID: 16894 RVA: 0x000DDCB8 File Offset: 0x000DBEB8
		public IAsyncResult BeginSyncFolderItems(SyncFolderItemsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<SyncFolderItemsResponse>(asyncCallback, asyncState);
		}

		// Token: 0x060041FF RID: 16895 RVA: 0x000DDCE3 File Offset: 0x000DBEE3
		public SyncFolderItemsJsonResponse EndSyncFolderItems(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<SyncFolderItemsJsonResponse, SyncFolderItemsResponse>(result, (SyncFolderItemsResponse body) => new SyncFolderItemsJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004200 RID: 16896 RVA: 0x000DDD08 File Offset: 0x000DBF08
		public IAsyncResult BeginGetDelegate(GetDelegateJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004201 RID: 16897 RVA: 0x000DDD0F File Offset: 0x000DBF0F
		public GetDelegateJsonResponse EndGetDelegate(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004202 RID: 16898 RVA: 0x000DDD16 File Offset: 0x000DBF16
		public IAsyncResult BeginAddDelegate(AddDelegateJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004203 RID: 16899 RVA: 0x000DDD1D File Offset: 0x000DBF1D
		public AddDelegateJsonResponse EndAddDelegate(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004204 RID: 16900 RVA: 0x000DDD24 File Offset: 0x000DBF24
		public IAsyncResult BeginRemoveDelegate(RemoveDelegateJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004205 RID: 16901 RVA: 0x000DDD2B File Offset: 0x000DBF2B
		public RemoveDelegateJsonResponse EndRemoveDelegate(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004206 RID: 16902 RVA: 0x000DDD32 File Offset: 0x000DBF32
		public IAsyncResult BeginUpdateDelegate(UpdateDelegateJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004207 RID: 16903 RVA: 0x000DDD39 File Offset: 0x000DBF39
		public UpdateDelegateJsonResponse EndUpdateDelegate(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004208 RID: 16904 RVA: 0x000DDD40 File Offset: 0x000DBF40
		public IAsyncResult BeginCreateUserConfiguration(CreateUserConfigurationJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<CreateUserConfigurationResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06004209 RID: 16905 RVA: 0x000DDD6B File Offset: 0x000DBF6B
		public CreateUserConfigurationJsonResponse EndCreateUserConfiguration(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<CreateUserConfigurationJsonResponse, CreateUserConfigurationResponse>(result, (CreateUserConfigurationResponse body) => new CreateUserConfigurationJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x0600420A RID: 16906 RVA: 0x000DDD90 File Offset: 0x000DBF90
		public IAsyncResult BeginDeleteUserConfiguration(DeleteUserConfigurationJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<DeleteUserConfigurationResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600420B RID: 16907 RVA: 0x000DDDBB File Offset: 0x000DBFBB
		public DeleteUserConfigurationJsonResponse EndDeleteUserConfiguration(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<DeleteUserConfigurationJsonResponse, DeleteUserConfigurationResponse>(result, (DeleteUserConfigurationResponse body) => new DeleteUserConfigurationJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x0600420C RID: 16908 RVA: 0x000DDDE0 File Offset: 0x000DBFE0
		public IAsyncResult BeginGetUserConfiguration(GetUserConfigurationJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetUserConfigurationResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600420D RID: 16909 RVA: 0x000DDE0B File Offset: 0x000DC00B
		public GetUserConfigurationJsonResponse EndGetUserConfiguration(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetUserConfigurationJsonResponse, GetUserConfigurationResponse>(result, (GetUserConfigurationResponse body) => new GetUserConfigurationJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x0600420E RID: 16910 RVA: 0x000DDE30 File Offset: 0x000DC030
		public IAsyncResult BeginUpdateUserConfiguration(UpdateUserConfigurationJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<UpdateUserConfigurationResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600420F RID: 16911 RVA: 0x000DDE5B File Offset: 0x000DC05B
		public UpdateUserConfigurationJsonResponse EndUpdateUserConfiguration(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<UpdateUserConfigurationJsonResponse, UpdateUserConfigurationResponse>(result, (UpdateUserConfigurationResponse body) => new UpdateUserConfigurationJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004210 RID: 16912 RVA: 0x000DDE80 File Offset: 0x000DC080
		public IAsyncResult BeginGetServiceConfiguration(GetServiceConfigurationJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004211 RID: 16913 RVA: 0x000DDE87 File Offset: 0x000DC087
		public GetServiceConfigurationJsonResponse EndGetServiceConfiguration(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004212 RID: 16914 RVA: 0x000DDE8E File Offset: 0x000DC08E
		public IAsyncResult BeginGetMailTips(GetMailTipsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetMailTipsResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06004213 RID: 16915 RVA: 0x000DDEBB File Offset: 0x000DC0BB
		public GetMailTipsJsonResponse EndGetMailTips(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetMailTipsJsonResponse, GetMailTipsResponseMessage>(result, (GetMailTipsResponseMessage body) => new GetMailTipsJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004214 RID: 16916 RVA: 0x000DDEE0 File Offset: 0x000DC0E0
		public IAsyncResult BeginPlayOnPhone(PlayOnPhoneJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<PlayOnPhoneResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06004215 RID: 16917 RVA: 0x000DDF0B File Offset: 0x000DC10B
		public PlayOnPhoneJsonResponse EndPlayOnPhone(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<PlayOnPhoneJsonResponse, PlayOnPhoneResponseMessage>(result, (PlayOnPhoneResponseMessage body) => new PlayOnPhoneJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004216 RID: 16918 RVA: 0x000DDF30 File Offset: 0x000DC130
		public IAsyncResult BeginGetPhoneCallInformation(GetPhoneCallInformationJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetPhoneCallInformationResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06004217 RID: 16919 RVA: 0x000DDF5B File Offset: 0x000DC15B
		public GetPhoneCallInformationJsonResponse EndGetPhoneCallInformation(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetPhoneCallInformationJsonResponse, GetPhoneCallInformationResponseMessage>(result, (GetPhoneCallInformationResponseMessage body) => new GetPhoneCallInformationJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004218 RID: 16920 RVA: 0x000DDF80 File Offset: 0x000DC180
		public IAsyncResult BeginDisconnectPhoneCall(DisconnectPhoneCallJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<DisconnectPhoneCallResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06004219 RID: 16921 RVA: 0x000DDFAB File Offset: 0x000DC1AB
		public DisconnectPhoneCallJsonResponse EndDisconnectPhoneCall(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<DisconnectPhoneCallJsonResponse, DisconnectPhoneCallResponseMessage>(result, (DisconnectPhoneCallResponseMessage body) => new DisconnectPhoneCallJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x0600421A RID: 16922 RVA: 0x000DDFD0 File Offset: 0x000DC1D0
		public IAsyncResult BeginGetUserAvailability(GetUserAvailabilityJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetUserAvailabilityResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600421B RID: 16923 RVA: 0x000DDFFB File Offset: 0x000DC1FB
		public GetUserAvailabilityJsonResponse EndGetUserAvailability(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetUserAvailabilityJsonResponse, GetUserAvailabilityResponse>(result, (GetUserAvailabilityResponse body) => new GetUserAvailabilityJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x0600421C RID: 16924 RVA: 0x000DE020 File Offset: 0x000DC220
		public IAsyncResult BeginGetUserOofSettings(GetUserOofSettingsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600421D RID: 16925 RVA: 0x000DE027 File Offset: 0x000DC227
		public GetUserOofSettingsJsonResponse EndGetUserOofSettings(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600421E RID: 16926 RVA: 0x000DE02E File Offset: 0x000DC22E
		public IAsyncResult BeginSetUserOofSettings(SetUserOofSettingsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600421F RID: 16927 RVA: 0x000DE035 File Offset: 0x000DC235
		public SetUserOofSettingsJsonResponse EndSetUserOofSettings(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004220 RID: 16928 RVA: 0x000DE03C File Offset: 0x000DC23C
		public IAsyncResult BeginGetSharingMetadata(GetSharingMetadataJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004221 RID: 16929 RVA: 0x000DE043 File Offset: 0x000DC243
		public GetSharingMetadataJsonResponse EndGetSharingMetadata(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004222 RID: 16930 RVA: 0x000DE04A File Offset: 0x000DC24A
		public IAsyncResult BeginRefreshSharingFolder(RefreshSharingFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004223 RID: 16931 RVA: 0x000DE051 File Offset: 0x000DC251
		public RefreshSharingFolderJsonResponse EndRefreshSharingFolder(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004224 RID: 16932 RVA: 0x000DE058 File Offset: 0x000DC258
		public IAsyncResult BeginGetSharingFolder(GetSharingFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004225 RID: 16933 RVA: 0x000DE05F File Offset: 0x000DC25F
		public GetSharingFolderJsonResponse EndGetSharingFolder(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004226 RID: 16934 RVA: 0x000DE066 File Offset: 0x000DC266
		public IAsyncResult BeginGetReminders(GetRemindersJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetRemindersResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06004227 RID: 16935 RVA: 0x000DE093 File Offset: 0x000DC293
		public GetRemindersJsonResponse EndGetReminders(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetRemindersJsonResponse, GetRemindersResponse>(result, (GetRemindersResponse body) => new GetRemindersJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004228 RID: 16936 RVA: 0x000DE0B8 File Offset: 0x000DC2B8
		public IAsyncResult BeginPerformReminderAction(PerformReminderActionJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<PerformReminderActionResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06004229 RID: 16937 RVA: 0x000DE0E3 File Offset: 0x000DC2E3
		public PerformReminderActionJsonResponse EndPerformReminderAction(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<PerformReminderActionJsonResponse, PerformReminderActionResponse>(result, (PerformReminderActionResponse body) => new PerformReminderActionJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x0600422A RID: 16938 RVA: 0x000DE108 File Offset: 0x000DC308
		public IAsyncResult BeginGetRoomLists(GetRoomListsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetRoomListsResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600422B RID: 16939 RVA: 0x000DE133 File Offset: 0x000DC333
		public GetRoomListsJsonResponse EndGetRoomLists(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetRoomListsJsonResponse, GetRoomListsResponse>(result, (GetRoomListsResponse body) => new GetRoomListsJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x0600422C RID: 16940 RVA: 0x000DE158 File Offset: 0x000DC358
		public IAsyncResult BeginGetRooms(GetRoomsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600422D RID: 16941 RVA: 0x000DE15F File Offset: 0x000DC35F
		public GetRoomsJsonResponse EndGetRooms(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600422E RID: 16942 RVA: 0x000DE166 File Offset: 0x000DC366
		public IAsyncResult BeginFindMessageTrackingReport(FindMessageTrackingReportJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600422F RID: 16943 RVA: 0x000DE16D File Offset: 0x000DC36D
		public FindMessageTrackingReportJsonResponse EndFindMessageTrackingReport(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004230 RID: 16944 RVA: 0x000DE174 File Offset: 0x000DC374
		public IAsyncResult BeginGetMessageTrackingReport(GetMessageTrackingReportJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004231 RID: 16945 RVA: 0x000DE17B File Offset: 0x000DC37B
		public GetMessageTrackingReportJsonResponse EndGetMessageTrackingReport(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004232 RID: 16946 RVA: 0x000DE182 File Offset: 0x000DC382
		public IAsyncResult BeginFindConversation(FindConversationJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			JsonService.AdjustMaxRowsIfNeeded(request.Body.Paging);
			return request.Body.ValidateAndSubmit<FindConversationResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06004233 RID: 16947 RVA: 0x000DE1BF File Offset: 0x000DC3BF
		public FindConversationJsonResponse EndFindConversation(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<FindConversationJsonResponse, FindConversationResponseMessage>(result, (FindConversationResponseMessage body) => new FindConversationJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004234 RID: 16948 RVA: 0x000DE1E4 File Offset: 0x000DC3E4
		public IAsyncResult BeginSyncConversation(SyncConversationJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<SyncConversationResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06004235 RID: 16949 RVA: 0x000DE20F File Offset: 0x000DC40F
		public SyncConversationJsonResponse EndSyncConversation(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<SyncConversationJsonResponse, SyncConversationResponseMessage>(result, (SyncConversationResponseMessage body) => new SyncConversationJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004236 RID: 16950 RVA: 0x000DE234 File Offset: 0x000DC434
		public IAsyncResult BeginApplyConversationAction(ApplyConversationActionJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<ApplyConversationActionResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06004237 RID: 16951 RVA: 0x000DE25F File Offset: 0x000DC45F
		public ApplyConversationActionJsonResponse EndApplyConversationAction(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<ApplyConversationActionJsonResponse, ApplyConversationActionResponse>(result, (ApplyConversationActionResponse body) => new ApplyConversationActionJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004238 RID: 16952 RVA: 0x000DE284 File Offset: 0x000DC484
		public IAsyncResult BeginFindPeople(FindPeopleJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<FindPeopleResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06004239 RID: 16953 RVA: 0x000DE2AF File Offset: 0x000DC4AF
		public FindPeopleJsonResponse EndFindPeople(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<FindPeopleJsonResponse, FindPeopleResponseMessage>(result, (FindPeopleResponseMessage body) => new FindPeopleJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x0600423A RID: 16954 RVA: 0x000DE2D4 File Offset: 0x000DC4D4
		public IAsyncResult BeginSyncAutoCompleteRecipients(SyncAutoCompleteRecipientsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<SyncAutoCompleteRecipientsResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x0600423B RID: 16955 RVA: 0x000DE2FF File Offset: 0x000DC4FF
		public SyncAutoCompleteRecipientsJsonResponse EndSyncAutoCompleteRecipients(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<SyncAutoCompleteRecipientsJsonResponse, SyncAutoCompleteRecipientsResponseMessage>(result, (SyncAutoCompleteRecipientsResponseMessage body) => new SyncAutoCompleteRecipientsJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x0600423C RID: 16956 RVA: 0x000DE324 File Offset: 0x000DC524
		public IAsyncResult BeginSyncPeople(SyncPeopleJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<SyncPeopleResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x0600423D RID: 16957 RVA: 0x000DE34F File Offset: 0x000DC54F
		public SyncPeopleJsonResponse EndSyncPeople(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<SyncPeopleJsonResponse, SyncPeopleResponseMessage>(result, (SyncPeopleResponseMessage body) => new SyncPeopleJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x0600423E RID: 16958 RVA: 0x000DE374 File Offset: 0x000DC574
		public IAsyncResult BeginGetPersona(GetPersonaJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetPersonaResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x0600423F RID: 16959 RVA: 0x000DE39F File Offset: 0x000DC59F
		public GetPersonaJsonResponse EndGetPersona(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetPersonaJsonResponse, GetPersonaResponseMessage>(result, (GetPersonaResponseMessage body) => new GetPersonaJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004240 RID: 16960 RVA: 0x000DE3C4 File Offset: 0x000DC5C4
		public IAsyncResult BeginGetInboxRules(GetInboxRulesJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004241 RID: 16961 RVA: 0x000DE3CB File Offset: 0x000DC5CB
		public GetInboxRulesJsonResponse EndGetInboxRules(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004242 RID: 16962 RVA: 0x000DE3D2 File Offset: 0x000DC5D2
		public IAsyncResult BeginUpdateInboxRules(UpdateInboxRulesJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004243 RID: 16963 RVA: 0x000DE3D9 File Offset: 0x000DC5D9
		public UpdateInboxRulesJsonResponse EndUpdateInboxRules(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004244 RID: 16964 RVA: 0x000DE3E0 File Offset: 0x000DC5E0
		public IAsyncResult BeginExecuteDiagnosticMethod(ExecuteDiagnosticMethodJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004245 RID: 16965 RVA: 0x000DE3E7 File Offset: 0x000DC5E7
		public ExecuteDiagnosticMethodJsonResponse EndExecuteDiagnosticMethod(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004246 RID: 16966 RVA: 0x000DE3EE File Offset: 0x000DC5EE
		public IAsyncResult BeginFindMailboxStatisticsByKeywords(FindMailboxStatisticsByKeywordsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004247 RID: 16967 RVA: 0x000DE3F5 File Offset: 0x000DC5F5
		public FindMailboxStatisticsByKeywordsJsonResponse EndFindMailboxStatisticsByKeywords(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004248 RID: 16968 RVA: 0x000DE3FC File Offset: 0x000DC5FC
		public IAsyncResult BeginGetSearchableMailboxes(GetSearchableMailboxesJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetSearchableMailboxesResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06004249 RID: 16969 RVA: 0x000DE427 File Offset: 0x000DC627
		public GetSearchableMailboxesJsonResponse EndGetSearchableMailboxes(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetSearchableMailboxesJsonResponse, GetSearchableMailboxesResponse>(result, (GetSearchableMailboxesResponse body) => new GetSearchableMailboxesJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x0600424A RID: 16970 RVA: 0x000DE44C File Offset: 0x000DC64C
		public IAsyncResult BeginSearchMailboxes(SearchMailboxesJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<SearchMailboxesResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600424B RID: 16971 RVA: 0x000DE477 File Offset: 0x000DC677
		public SearchMailboxesJsonResponse EndSearchMailboxes(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<SearchMailboxesJsonResponse, SearchMailboxesResponse>(result, (SearchMailboxesResponse body) => new SearchMailboxesJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x0600424C RID: 16972 RVA: 0x000DE49C File Offset: 0x000DC69C
		public IAsyncResult BeginGetHoldOnMailboxes(GetHoldOnMailboxesJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetHoldOnMailboxesResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600424D RID: 16973 RVA: 0x000DE4C7 File Offset: 0x000DC6C7
		public GetHoldOnMailboxesJsonResponse EndGetHoldOnMailboxes(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetHoldOnMailboxesJsonResponse, GetHoldOnMailboxesResponse>(result, (GetHoldOnMailboxesResponse body) => new GetHoldOnMailboxesJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x0600424E RID: 16974 RVA: 0x000DE4EC File Offset: 0x000DC6EC
		public IAsyncResult BeginSetHoldOnMailboxes(SetHoldOnMailboxesJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<SetHoldOnMailboxesResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600424F RID: 16975 RVA: 0x000DE517 File Offset: 0x000DC717
		public SetHoldOnMailboxesJsonResponse EndSetHoldOnMailboxes(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<SetHoldOnMailboxesJsonResponse, SetHoldOnMailboxesResponse>(result, (SetHoldOnMailboxesResponse body) => new SetHoldOnMailboxesJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004250 RID: 16976 RVA: 0x000DE53C File Offset: 0x000DC73C
		public IAsyncResult BeginGetPasswordExpirationDate(GetPasswordExpirationDateJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetPasswordExpirationDateResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06004251 RID: 16977 RVA: 0x000DE567 File Offset: 0x000DC767
		public GetPasswordExpirationDateJsonResponse EndGetPasswordExpirationDate(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetPasswordExpirationDateJsonResponse, GetPasswordExpirationDateResponse>(result, (GetPasswordExpirationDateResponse body) => new GetPasswordExpirationDateJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004252 RID: 16978 RVA: 0x000DE58C File Offset: 0x000DC78C
		public IAsyncResult BeginMarkAllItemsAsRead(MarkAllItemsAsReadJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<MarkAllItemsAsReadResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06004253 RID: 16979 RVA: 0x000DE5B7 File Offset: 0x000DC7B7
		public MarkAllItemsAsReadJsonResponse EndMarkAllItemsAsRead(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<MarkAllItemsAsReadJsonResponse, MarkAllItemsAsReadResponse>(result, (MarkAllItemsAsReadResponse body) => new MarkAllItemsAsReadJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004254 RID: 16980 RVA: 0x000DE5DC File Offset: 0x000DC7DC
		public IAsyncResult BeginMarkAsJunk(MarkAsJunkJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<MarkAsJunkResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06004255 RID: 16981 RVA: 0x000DE607 File Offset: 0x000DC807
		public MarkAsJunkJsonResponse EndMarkAsJunk(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<MarkAsJunkJsonResponse, MarkAsJunkResponse>(result, (MarkAsJunkResponse body) => new MarkAsJunkJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004256 RID: 16982 RVA: 0x000DE62C File Offset: 0x000DC82C
		public IAsyncResult BeginAddDistributionGroupToImList(AddDistributionGroupToImListJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<AddDistributionGroupToImListResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06004257 RID: 16983 RVA: 0x000DE657 File Offset: 0x000DC857
		public AddDistributionGroupToImListJsonResponse EndAddDistributionGroupToImList(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<AddDistributionGroupToImListJsonResponse, AddDistributionGroupToImListResponseMessage>(result, (AddDistributionGroupToImListResponseMessage body) => new AddDistributionGroupToImListJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004258 RID: 16984 RVA: 0x000DE67C File Offset: 0x000DC87C
		public IAsyncResult BeginAddImContactToGroup(AddImContactToGroupJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<AddImContactToGroupResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06004259 RID: 16985 RVA: 0x000DE6A7 File Offset: 0x000DC8A7
		public AddImContactToGroupJsonResponse EndAddImContactToGroup(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<AddImContactToGroupJsonResponse, AddImContactToGroupResponseMessage>(result, (AddImContactToGroupResponseMessage body) => new AddImContactToGroupJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x0600425A RID: 16986 RVA: 0x000DE6CC File Offset: 0x000DC8CC
		public IAsyncResult BeginRemoveImContactFromGroup(RemoveImContactFromGroupJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<RemoveImContactFromGroupResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x0600425B RID: 16987 RVA: 0x000DE6F7 File Offset: 0x000DC8F7
		public RemoveImContactFromGroupJsonResponse EndRemoveImContactFromGroup(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<RemoveImContactFromGroupJsonResponse, RemoveImContactFromGroupResponseMessage>(result, (RemoveImContactFromGroupResponseMessage body) => new RemoveImContactFromGroupJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x0600425C RID: 16988 RVA: 0x000DE71C File Offset: 0x000DC91C
		public IAsyncResult BeginAddImGroup(AddImGroupJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<AddImGroupResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x0600425D RID: 16989 RVA: 0x000DE747 File Offset: 0x000DC947
		public AddImGroupJsonResponse EndAddImGroup(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<AddImGroupJsonResponse, AddImGroupResponseMessage>(result, (AddImGroupResponseMessage body) => new AddImGroupJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x0600425E RID: 16990 RVA: 0x000DE76C File Offset: 0x000DC96C
		public IAsyncResult BeginAddNewImContactToGroup(AddNewImContactToGroupJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<AddNewImContactToGroupResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x0600425F RID: 16991 RVA: 0x000DE797 File Offset: 0x000DC997
		public AddNewImContactToGroupJsonResponse EndAddNewImContactToGroup(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<AddNewImContactToGroupJsonResponse, AddNewImContactToGroupResponseMessage>(result, (AddNewImContactToGroupResponseMessage body) => new AddNewImContactToGroupJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004260 RID: 16992 RVA: 0x000DE7BC File Offset: 0x000DC9BC
		public IAsyncResult BeginAddNewTelUriContactToGroup(AddNewTelUriContactToGroupJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<AddNewTelUriContactToGroupResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06004261 RID: 16993 RVA: 0x000DE7E7 File Offset: 0x000DC9E7
		public AddNewTelUriContactToGroupJsonResponse EndAddNewTelUriContactToGroup(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<AddNewTelUriContactToGroupJsonResponse, AddNewTelUriContactToGroupResponseMessage>(result, (AddNewTelUriContactToGroupResponseMessage body) => new AddNewTelUriContactToGroupJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004262 RID: 16994 RVA: 0x000DE80C File Offset: 0x000DCA0C
		public IAsyncResult BeginGetImItemList(GetImItemListJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetImItemListResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06004263 RID: 16995 RVA: 0x000DE837 File Offset: 0x000DCA37
		public GetImItemListJsonResponse EndGetImItemList(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetImItemListJsonResponse, GetImItemListResponseMessage>(result, (GetImItemListResponseMessage body) => new GetImItemListJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004264 RID: 16996 RVA: 0x000DE85C File Offset: 0x000DCA5C
		public IAsyncResult BeginGetImItems(GetImItemsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetImItemsResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06004265 RID: 16997 RVA: 0x000DE887 File Offset: 0x000DCA87
		public GetImItemsJsonResponse EndGetImItems(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetImItemsJsonResponse, GetImItemsResponseMessage>(result, (GetImItemsResponseMessage body) => new GetImItemsJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004266 RID: 16998 RVA: 0x000DE8AC File Offset: 0x000DCAAC
		public IAsyncResult BeginRemoveContactFromImList(RemoveContactFromImListJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<RemoveContactFromImListResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06004267 RID: 16999 RVA: 0x000DE8D7 File Offset: 0x000DCAD7
		public RemoveContactFromImListJsonResponse EndRemoveContactFromImList(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<RemoveContactFromImListJsonResponse, RemoveContactFromImListResponseMessage>(result, (RemoveContactFromImListResponseMessage body) => new RemoveContactFromImListJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004268 RID: 17000 RVA: 0x000DE8FC File Offset: 0x000DCAFC
		public IAsyncResult BeginRemoveDistributionGroupFromImList(RemoveDistributionGroupFromImListJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<RemoveDistributionGroupFromImListResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06004269 RID: 17001 RVA: 0x000DE927 File Offset: 0x000DCB27
		public RemoveDistributionGroupFromImListJsonResponse EndRemoveDistributionGroupFromImList(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<RemoveDistributionGroupFromImListJsonResponse, RemoveDistributionGroupFromImListResponseMessage>(result, (RemoveDistributionGroupFromImListResponseMessage body) => new RemoveDistributionGroupFromImListJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x0600426A RID: 17002 RVA: 0x000DE94C File Offset: 0x000DCB4C
		public IAsyncResult BeginRemoveImGroup(RemoveImGroupJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<RemoveImGroupResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x0600426B RID: 17003 RVA: 0x000DE977 File Offset: 0x000DCB77
		public RemoveImGroupJsonResponse EndRemoveImGroup(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<RemoveImGroupJsonResponse, RemoveImGroupResponseMessage>(result, (RemoveImGroupResponseMessage body) => new RemoveImGroupJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x0600426C RID: 17004 RVA: 0x000DE99C File Offset: 0x000DCB9C
		public IAsyncResult BeginSetImGroup(SetImGroupJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<SetImGroupResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x0600426D RID: 17005 RVA: 0x000DE9C7 File Offset: 0x000DCBC7
		public SetImGroupJsonResponse EndSetImGroup(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<SetImGroupJsonResponse, SetImGroupResponseMessage>(result, (SetImGroupResponseMessage body) => new SetImGroupJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x0600426E RID: 17006 RVA: 0x000DE9EC File Offset: 0x000DCBEC
		public IAsyncResult BeginSetImListMigrationCompleted(SetImListMigrationCompletedJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<SetImListMigrationCompletedResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x0600426F RID: 17007 RVA: 0x000DEA17 File Offset: 0x000DCC17
		public SetImListMigrationCompletedJsonResponse EndSetImListMigrationCompleted(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<SetImListMigrationCompletedJsonResponse, SetImListMigrationCompletedResponseMessage>(result, (SetImListMigrationCompletedResponseMessage body) => new SetImListMigrationCompletedJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004270 RID: 17008 RVA: 0x000DEA3C File Offset: 0x000DCC3C
		public IAsyncResult BeginGetConversationItems(GetConversationItemsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetConversationItemsResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06004271 RID: 17009 RVA: 0x000DEA67 File Offset: 0x000DCC67
		public GetConversationItemsJsonResponse EndGetConversationItems(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetConversationItemsJsonResponse, GetConversationItemsResponse>(result, (GetConversationItemsResponse body) => new GetConversationItemsJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004272 RID: 17010 RVA: 0x000DEA8C File Offset: 0x000DCC8C
		public IAsyncResult BeginGetThreadedConversationItems(GetThreadedConversationItemsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetThreadedConversationItemsResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06004273 RID: 17011 RVA: 0x000DEAB7 File Offset: 0x000DCCB7
		public GetThreadedConversationItemsJsonResponse EndGetThreadedConversationItems(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetThreadedConversationItemsJsonResponse, GetThreadedConversationItemsResponse>(result, (GetThreadedConversationItemsResponse body) => new GetThreadedConversationItemsJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004274 RID: 17012 RVA: 0x000DEADC File Offset: 0x000DCCDC
		public IAsyncResult BeginGetConversationItemsDiagnostics(GetConversationItemsDiagnosticsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetConversationItemsDiagnosticsResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06004275 RID: 17013 RVA: 0x000DEB07 File Offset: 0x000DCD07
		public GetConversationItemsDiagnosticsJsonResponse EndGetConversationItemsDiagnostics(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetConversationItemsDiagnosticsJsonResponse, GetConversationItemsDiagnosticsResponse>(result, (GetConversationItemsDiagnosticsResponse body) => new GetConversationItemsDiagnosticsJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004276 RID: 17014 RVA: 0x000DEB2C File Offset: 0x000DCD2C
		public IAsyncResult BeginGetUserRetentionPolicyTags(GetUserRetentionPolicyTagsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetUserRetentionPolicyTagsResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06004277 RID: 17015 RVA: 0x000DEB57 File Offset: 0x000DCD57
		public GetUserRetentionPolicyTagsJsonResponse EndGetUserRetentionPolicyTags(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetUserRetentionPolicyTagsJsonResponse, GetUserRetentionPolicyTagsResponse>(result, (GetUserRetentionPolicyTagsResponse body) => new GetUserRetentionPolicyTagsJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004278 RID: 17016 RVA: 0x000DEB7C File Offset: 0x000DCD7C
		public IAsyncResult BeginProvision(ProvisionJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<ProvisionResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06004279 RID: 17017 RVA: 0x000DEBA7 File Offset: 0x000DCDA7
		public ProvisionJsonResponse EndProvision(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<ProvisionJsonResponse, ProvisionResponse>(result, (ProvisionResponse body) => new ProvisionJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x0600427A RID: 17018 RVA: 0x000DEBCC File Offset: 0x000DCDCC
		public IAsyncResult BeginGetTimeZoneOffsets(GetTimeZoneOffsetsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetTimeZoneOffsetsResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x0600427B RID: 17019 RVA: 0x000DEBF7 File Offset: 0x000DCDF7
		public GetTimeZoneOffsetsJsonResponse EndGetTimeZoneOffsets(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetTimeZoneOffsetsJsonResponse, GetTimeZoneOffsetsResponseMessage>(result, (GetTimeZoneOffsetsResponseMessage body) => new GetTimeZoneOffsetsJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x0600427C RID: 17020 RVA: 0x000DEC1C File Offset: 0x000DCE1C
		public IAsyncResult BeginDeprovision(DeprovisionJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<DeprovisionResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600427D RID: 17021 RVA: 0x000DEC47 File Offset: 0x000DCE47
		public DeprovisionJsonResponse EndDeprovision(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<DeprovisionJsonResponse, DeprovisionResponse>(result, (DeprovisionResponse body) => new DeprovisionJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x0600427E RID: 17022 RVA: 0x000DEC6C File Offset: 0x000DCE6C
		public IAsyncResult BeginLogPushNotificationData(LogPushNotificationDataJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<LogPushNotificationDataResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600427F RID: 17023 RVA: 0x000DEC97 File Offset: 0x000DCE97
		public LogPushNotificationDataJsonResponse EndLogPushNotificationData(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<LogPushNotificationDataJsonResponse, LogPushNotificationDataResponse>(result, (LogPushNotificationDataResponse body) => new LogPushNotificationDataJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004280 RID: 17024 RVA: 0x000DECBC File Offset: 0x000DCEBC
		public IAsyncResult BeginGetUserUnifiedGroups(GetUserUnifiedGroupsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetUserUnifiedGroupsResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06004281 RID: 17025 RVA: 0x000DECE7 File Offset: 0x000DCEE7
		public GetUserUnifiedGroupsJsonResponse EndGetUserUnifiedGroups(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetUserUnifiedGroupsJsonResponse, GetUserUnifiedGroupsResponseMessage>(result, (GetUserUnifiedGroupsResponseMessage body) => new GetUserUnifiedGroupsJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004282 RID: 17026 RVA: 0x000DED0C File Offset: 0x000DCF0C
		public IAsyncResult BeginGetBirthdayCalendarView(GetBirthdayCalendarViewJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetBirthdayCalendarViewResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x06004283 RID: 17027 RVA: 0x000DED37 File Offset: 0x000DCF37
		public GetBirthdayCalendarViewJsonResponse EndGetBirthdayCalendarView(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetBirthdayCalendarViewJsonResponse, GetBirthdayCalendarViewResponseMessage>(result, (GetBirthdayCalendarViewResponseMessage body) => new GetBirthdayCalendarViewJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004284 RID: 17028 RVA: 0x000DED5C File Offset: 0x000DCF5C
		public IAsyncResult BeginGetClutterState(GetClutterStateJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<GetClutterStateResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06004285 RID: 17029 RVA: 0x000DED87 File Offset: 0x000DCF87
		public GetClutterStateJsonResponse EndGetClutterState(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<GetClutterStateJsonResponse, GetClutterStateResponse>(result, (GetClutterStateResponse body) => new GetClutterStateJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004286 RID: 17030 RVA: 0x000DEDAC File Offset: 0x000DCFAC
		public IAsyncResult BeginSetClutterState(SetClutterStateJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<SetClutterStateResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06004287 RID: 17031 RVA: 0x000DEDD7 File Offset: 0x000DCFD7
		public SetClutterStateJsonResponse EndSetClutterState(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<SetClutterStateJsonResponse, SetClutterStateResponse>(result, (SetClutterStateResponse body) => new SetClutterStateJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004288 RID: 17032 RVA: 0x000DEDFC File Offset: 0x000DCFFC
		public IAsyncResult BeginGetUserPhoto(string email, UserPhotoSize size, bool isPreview, bool fallbackToClearImage, AsyncCallback callback, object state)
		{
			return new GetUserPhotoRequest(CallContext.Current.CreateWebResponseContext(), email, size, isPreview, fallbackToClearImage).ValidateAndSubmit<GetUserPhotoResponse>(callback, state);
		}

		// Token: 0x06004289 RID: 17033 RVA: 0x000DEE1C File Offset: 0x000DD01C
		public Stream EndGetUserPhoto(IAsyncResult result)
		{
			ServiceAsyncResult<GetUserPhotoResponse> serviceAsyncResult = (ServiceAsyncResult<GetUserPhotoResponse>)result;
			if (serviceAsyncResult.Data != null && serviceAsyncResult.Data.ResponseMessages.Items != null && serviceAsyncResult.Data.ResponseMessages.Items.Length > 0)
			{
				return ((GetUserPhotoResponseMessage)serviceAsyncResult.Data.ResponseMessages.Items[0]).UserPhotoStream;
			}
			IOutgoingWebResponseContext outgoingWebResponseContext = CallContext.Current.CreateWebResponseContext();
			outgoingWebResponseContext.StatusCode = HttpStatusCode.InternalServerError;
			return new MemoryStream();
		}

		// Token: 0x0600428A RID: 17034 RVA: 0x000DEE97 File Offset: 0x000DD097
		public IAsyncResult BeginGetPeopleICommunicateWith(AsyncCallback callback, object state)
		{
			return new GetPeopleICommunicateWithRequest(CallContext.Current.CreateWebResponseContext()).ValidateAndSubmit<GetPeopleICommunicateWithResponse>(callback, state);
		}

		// Token: 0x0600428B RID: 17035 RVA: 0x000DEEB0 File Offset: 0x000DD0B0
		public Stream EndGetPeopleICommunicateWith(IAsyncResult result)
		{
			ServiceAsyncResult<GetPeopleICommunicateWithResponse> serviceAsyncResult = (ServiceAsyncResult<GetPeopleICommunicateWithResponse>)result;
			if (serviceAsyncResult.Data != null && serviceAsyncResult.Data.ResponseMessages.Items != null && serviceAsyncResult.Data.ResponseMessages.Items.Length > 0)
			{
				return ((GetPeopleICommunicateWithResponseMessage)serviceAsyncResult.Data.ResponseMessages.Items[0]).Stream;
			}
			IOutgoingWebResponseContext outgoingWebResponseContext = CallContext.Current.CreateWebResponseContext();
			outgoingWebResponseContext.StatusCode = HttpStatusCode.InternalServerError;
			return new MemoryStream();
		}

		// Token: 0x0600428C RID: 17036 RVA: 0x000DEF2B File Offset: 0x000DD12B
		public IAsyncResult BeginSubscribeToPushNotification(SubscribeToPushNotificationJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<SubscribeToPushNotificationResponse>(asyncCallback, asyncState);
		}

		// Token: 0x0600428D RID: 17037 RVA: 0x000DEF57 File Offset: 0x000DD157
		public SubscribeToPushNotificationJsonResponse EndSubscribeToPushNotification(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<SubscribeToPushNotificationJsonResponse, SubscribeToPushNotificationResponse>(result, (SubscribeToPushNotificationResponse body) => new SubscribeToPushNotificationJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x0600428E RID: 17038 RVA: 0x000DEF7C File Offset: 0x000DD17C
		public IAsyncResult BeginRequestDeviceRegistrationChallenge(RequestDeviceRegistrationChallengeJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<RequestDeviceRegistrationChallengeResponseMessage>(asyncCallback, asyncState);
		}

		// Token: 0x0600428F RID: 17039 RVA: 0x000DEFA7 File Offset: 0x000DD1A7
		public RequestDeviceRegistrationChallengeJsonResponse EndRequestDeviceRegistrationChallenge(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<RequestDeviceRegistrationChallengeJsonResponse, RequestDeviceRegistrationChallengeResponseMessage>(result, (RequestDeviceRegistrationChallengeResponseMessage body) => new RequestDeviceRegistrationChallengeJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004290 RID: 17040 RVA: 0x000DEFCC File Offset: 0x000DD1CC
		public IAsyncResult BeginUnsubscribeToPushNotification(UnsubscribeToPushNotificationJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return request.Body.ValidateAndSubmit<UnsubscribeToPushNotificationResponse>(asyncCallback, asyncState);
		}

		// Token: 0x06004291 RID: 17041 RVA: 0x000DEFF7 File Offset: 0x000DD1F7
		public UnsubscribeToPushNotificationJsonResponse EndUnsubscribeToPushNotification(IAsyncResult result)
		{
			return JsonService.CreateJsonResponse<UnsubscribeToPushNotificationJsonResponse, UnsubscribeToPushNotificationResponse>(result, (UnsubscribeToPushNotificationResponse body) => new UnsubscribeToPushNotificationJsonResponse
			{
				Body = body
			});
		}

		// Token: 0x06004292 RID: 17042 RVA: 0x000DF01C File Offset: 0x000DD21C
		private static TJsonResponse CreateJsonResponse<TJsonResponse, TJsonResponseBody>(IAsyncResult result, Func<TJsonResponseBody, TJsonResponse> createJsonResponseCallback)
		{
			bool flag = false;
			if (CallContext.Current.AccessingPrincipal != null && ExUserTracingAdaptor.Instance.IsTracingEnabledUser(CallContext.Current.AccessingPrincipal.LegacyDn))
			{
				flag = true;
				BaseTrace.CurrentThreadSettings.EnableTracing();
			}
			TJsonResponse result2;
			try
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>(0L, "Entering End web method for {0}", CallContext.Current.Description);
				ServiceAsyncResult<TJsonResponseBody> serviceAsyncResult = JsonService.GetServiceAsyncResult<TJsonResponseBody>(result);
				TJsonResponse tjsonResponse = createJsonResponseCallback(serviceAsyncResult.Data);
				PerformanceMonitor.UpdateTotalCompletedRequestsCount();
				result2 = tjsonResponse;
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

		// Token: 0x06004293 RID: 17043 RVA: 0x000DF0B8 File Offset: 0x000DD2B8
		private static ServiceAsyncResult<TJsonResponseBody> GetServiceAsyncResult<TJsonResponseBody>(IAsyncResult result)
		{
			ServiceAsyncResult<TJsonResponseBody> serviceAsyncResult = (ServiceAsyncResult<TJsonResponseBody>)result;
			Exception ex = serviceAsyncResult.CompletionState as Exception;
			if (ex != null)
			{
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
				if (!JsonService.IsServiceHandledException(ex2))
				{
					throw new InternalServerErrorException(ex2);
				}
				ExceptionDispatchInfo exceptionDispatchInfo = ExceptionDispatchInfo.Capture(ex);
				exceptionDispatchInfo.Throw();
			}
			return serviceAsyncResult;
		}

		// Token: 0x06004294 RID: 17044 RVA: 0x000DF131 File Offset: 0x000DD331
		private static bool IsServiceHandledException(Exception exception)
		{
			return exception is BailOutException || exception is FaultException;
		}

		// Token: 0x06004295 RID: 17045 RVA: 0x000DF146 File Offset: 0x000DD346
		private static void AdjustMaxRowsIfNeeded(BasePagingType paging)
		{
			if (paging != null && !(paging is CalendarPageView))
			{
				paging.MaxRows = Math.Min(paging.MaxRows, 200);
			}
		}
	}
}
