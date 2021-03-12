using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Clients.Owa2.Server.Web;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.PushNotifications;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Online.BOX.UI.Shell;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001FF RID: 511
	[MessageInspectorBehavior]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class OWAService : IOWAService, IJsonServiceContract, IOWAStreamingService, IJsonStreamingServiceContract
	{
		// Token: 0x060011F0 RID: 4592 RVA: 0x000457E6 File Offset: 0x000439E6
		public OWAService()
		{
			this.jsonService = new JsonService();
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x0004581C File Offset: 0x00043A1C
		public ExtensibilityContext GetExtensibilityContext(GetExtensibilityContextParameters request)
		{
			return this.jsonService.GetExtensibilityContext(request);
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x0004582A File Offset: 0x00043A2A
		public bool AddBuddy(Buddy buddy)
		{
			return this.jsonService.AddBuddy(buddy);
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00045838 File Offset: 0x00043A38
		public GetBuddyListResponse GetBuddyList()
		{
			return this.jsonService.GetBuddyList();
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00045845 File Offset: 0x00043A45
		public IAsyncResult BeginFindPlaces(FindPlacesRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginFindPlaces(request, asyncCallback, asyncState);
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00045855 File Offset: 0x00043A55
		public Persona[] EndFindPlaces(IAsyncResult result)
		{
			return this.jsonService.EndFindPlaces(result);
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x00045863 File Offset: 0x00043A63
		public DeletePlaceJsonResponse DeletePlace(DeletePlaceRequest request)
		{
			return this.jsonService.DeletePlace(request);
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x00045871 File Offset: 0x00043A71
		public CalendarActionResponse AddEventToMyCalendar(AddEventToMyCalendarRequest request)
		{
			return this.jsonService.AddEventToMyCalendar(request);
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0004587F File Offset: 0x00043A7F
		public bool AddTrustedSender(Microsoft.Exchange.Services.Core.Types.ItemId itemId)
		{
			return this.jsonService.AddTrustedSender(itemId);
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0004588D File Offset: 0x00043A8D
		public GetFavoritesResponse GetFavorites()
		{
			return this.jsonService.GetFavorites();
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0004589A File Offset: 0x00043A9A
		public UpdateFavoriteFolderResponse UpdateFavoriteFolder(UpdateFavoriteFolderRequest request)
		{
			return this.jsonService.UpdateFavoriteFolder(request);
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x000458A8 File Offset: 0x00043AA8
		public GetPersonaModernGroupMembershipJsonResponse GetPersonaModernGroupMembership(GetPersonaModernGroupMembershipJsonRequest request)
		{
			return this.jsonService.GetPersonaModernGroupMembership(request);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x000458B6 File Offset: 0x00043AB6
		public GetModernGroupJsonResponse GetModernGroup(GetModernGroupJsonRequest request)
		{
			return this.jsonService.GetModernGroup(request);
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x000458C4 File Offset: 0x00043AC4
		public GetModernGroupJsonResponse GetRecommendedModernGroup(GetModernGroupJsonRequest request)
		{
			return this.jsonService.GetRecommendedModernGroup(request);
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x000458D2 File Offset: 0x00043AD2
		public GetModernGroupsJsonResponse GetModernGroups()
		{
			return this.jsonService.GetModernGroups();
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x000458DF File Offset: 0x00043ADF
		public SetModernGroupPinStateJsonResponse SetModernGroupPinState(string smtpAddress, bool isPinned)
		{
			return this.jsonService.SetModernGroupPinState(smtpAddress, isPinned);
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x000458EE File Offset: 0x00043AEE
		public SetModernGroupMembershipJsonResponse SetModernGroupMembership(SetModernGroupMembershipJsonRequest request)
		{
			return this.jsonService.SetModernGroupMembership(request);
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x000458FC File Offset: 0x00043AFC
		public bool SetModernGroupSubscription()
		{
			return this.jsonService.SetModernGroupSubscription();
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00045909 File Offset: 0x00043B09
		public GetModernGroupUnseenItemsJsonResponse GetModernGroupUnseenItems(GetModernGroupUnseenItemsJsonRequest request)
		{
			return this.jsonService.GetModernGroupUnseenItems(request);
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00045917 File Offset: 0x00043B17
		public GetPeopleIKnowGraphResponse GetPeopleIKnowGraphCommand(GetPeopleIKnowGraphRequest request)
		{
			return this.jsonService.GetPeopleIKnowGraphCommand(request);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00045925 File Offset: 0x00043B25
		public UpdateMasterCategoryListResponse UpdateMasterCategoryList(UpdateMasterCategoryListRequest request)
		{
			return this.jsonService.UpdateMasterCategoryList(request);
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00045933 File Offset: 0x00043B33
		public MasterCategoryListActionResponse GetMasterCategoryList(GetMasterCategoryListRequest request)
		{
			return this.jsonService.GetMasterCategoryList(request);
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00045941 File Offset: 0x00043B41
		public GetTaskFoldersResponse GetTaskFolders()
		{
			return this.jsonService.GetTaskFolders();
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0004594E File Offset: 0x00043B4E
		public TaskFolderActionFolderIdResponse CreateTaskFolder(string newTaskFolderName, string parentGroupGuid)
		{
			return new CreateTaskFolderCommand(CallContext.Current, newTaskFolderName, parentGroupGuid).Execute();
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00045961 File Offset: 0x00043B61
		public TaskFolderActionFolderIdResponse RenameTaskFolder(Microsoft.Exchange.Services.Core.Types.ItemId itemId, string newTaskFolderName)
		{
			return new RenameTaskFolderCommand(CallContext.Current, itemId, newTaskFolderName).Execute();
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00045974 File Offset: 0x00043B74
		public TaskFolderActionResponse DeleteTaskFolder(Microsoft.Exchange.Services.Core.Types.ItemId itemId)
		{
			return new DeleteTaskFolderCommand(CallContext.Current, itemId).Execute();
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x00045986 File Offset: 0x00043B86
		public PeopleFilter[] GetPeopleFilters()
		{
			return new GetPeopleFilters(CallContext.Current).Execute();
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00045997 File Offset: 0x00043B97
		public string CreateAttachmentFromUri(Microsoft.Exchange.Services.Core.Types.ItemId itemId, string uri, string name, string subscriptionId)
		{
			return new CreateAttachmentFromUri(CallContext.Current, itemId, uri, name, subscriptionId).Execute();
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x000459AD File Offset: 0x00043BAD
		public GetUserAvailabilityInternalJsonResponse GetUserAvailabilityInternal(GetUserAvailabilityInternalJsonRequest request)
		{
			return this.jsonService.GetUserAvailabilityInternal(request);
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x000459BB File Offset: 0x00043BBB
		public OptionSummary GetOptionSummary()
		{
			return new GetOptionSummary(CallContext.Current).Execute();
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x000459CC File Offset: 0x00043BCC
		public UserOofSettingsType GetOwaUserOofSettings()
		{
			return new GetUserOofSettings(CallContext.Current, EWSSettings.RequestTimeZone).Execute();
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x000459E2 File Offset: 0x00043BE2
		public bool SetOwaUserOofSettings(UserOofSettingsType userOofSettings)
		{
			return new SetUserOofSettings(CallContext.Current, userOofSettings).Execute();
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x000459F4 File Offset: 0x00043BF4
		public EmailSignatureConfiguration GetOwaUserEmailSignature()
		{
			return new GetEmailSignature(CallContext.Current).Execute();
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00045A08 File Offset: 0x00043C08
		public ScopeFlightsSetting[] GetFlightsSettings()
		{
			UserContext userContext = UserContextManager.GetUserContext(CallContext.Current.HttpContext);
			if (!userContext.FeaturesManager.ClientServerSettings.FlightsView.Enabled)
			{
				throw new OwaNotSupportedException("This method is not supported.");
			}
			return new GetFlightsSettings(CallContext.Current, new ScopeFlightsSettingsProvider()).Execute();
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00045A5E File Offset: 0x00043C5E
		public bool SetOwaUserEmailSignature(EmailSignatureConfiguration userEmailSignature)
		{
			return new SetEmailSignature(CallContext.Current, userEmailSignature).Execute();
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00045A70 File Offset: 0x00043C70
		public int GetDaysUntilPasswordExpiration()
		{
			return new GetDaysUntilPasswordExpiration(CallContext.Current).Execute();
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00045A81 File Offset: 0x00043C81
		public EwsRoomType[] GetRoomsInternal(string roomList)
		{
			return new GetRoomsInternal(CallContext.Current, roomList).Execute().Rooms;
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00045A98 File Offset: 0x00043C98
		public ThemeSelectionInfoType GetThemes()
		{
			return new GetThemes(CallContext.Current).Execute();
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x00045AAC File Offset: 0x00043CAC
		public bool SetTheme(string theme)
		{
			SetUserThemeRequest request = new SetUserThemeRequest
			{
				ThemeId = theme,
				SkipO365Call = false
			};
			SetUserThemeResponse setUserThemeResponse = new SetUserTheme(CallContext.Current, request).Execute();
			return setUserThemeResponse.OwaSuccess;
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00045AE6 File Offset: 0x00043CE6
		public SetUserThemeResponse SetUserTheme(SetUserThemeRequest request)
		{
			return new SetUserTheme(CallContext.Current, request).Execute();
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00045AF8 File Offset: 0x00043CF8
		public TimeZoneConfiguration GetTimeZone(bool needTimeZoneList)
		{
			return new GetTimeZone(CallContext.Current, needTimeZoneList).Execute();
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00045B0A File Offset: 0x00043D0A
		public bool SetTimeZone(string timezone)
		{
			return new SetTimeZone(CallContext.Current, timezone).Execute();
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00045B1C File Offset: 0x00043D1C
		public bool SetUserLocale(string userLocale, bool localizeFolders)
		{
			return new SetUserLocale(CallContext.Current, userLocale, localizeFolders).Execute();
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00045B2F File Offset: 0x00043D2F
		public AddSharedCalendarResponse AddSharedCalendar(AddSharedCalendarRequest request)
		{
			return new AddSharedCalendarCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00045B41 File Offset: 0x00043D41
		public CalendarActionFolderIdResponse SubscribeInternalCalendar(SubscribeInternalCalendarRequest request)
		{
			return new SubscribeInternalCalendarCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00045B53 File Offset: 0x00043D53
		public CalendarActionFolderIdResponse SubscribeInternetCalendar(SubscribeInternetCalendarRequest request)
		{
			return new SubscribeInternetCalendarCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00045B65 File Offset: 0x00043D65
		public GetCalendarSharingRecipientInfoResponse GetCalendarSharingRecipientInfo(GetCalendarSharingRecipientInfoRequest request)
		{
			return new GetCalendarSharingRecipientInfoCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00045B77 File Offset: 0x00043D77
		public GetCalendarSharingPermissionsResponse GetCalendarSharingPermissions(GetCalendarSharingPermissionsRequest request)
		{
			return new GetCalendarSharingPermissionsCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00045B89 File Offset: 0x00043D89
		public CalendarActionResponse SetCalendarSharingPermissions(SetCalendarSharingPermissionsRequest request)
		{
			return new SetCalendarSharingPermissionsCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00045B9B File Offset: 0x00043D9B
		public SetCalendarPublishingResponse SetCalendarPublishing(SetCalendarPublishingRequest request)
		{
			return new SetCalendarPublishingCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00045BAD File Offset: 0x00043DAD
		public CalendarShareInviteResponse SendCalendarSharingInvite(CalendarShareInviteRequest request)
		{
			return new SendCalendarSharingInviteCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00045BBF File Offset: 0x00043DBF
		public CalendarActionGroupIdResponse CreateCalendarGroup(string newGroupName)
		{
			return new CreateCalendarGroupCommand(CallContext.Current, newGroupName).Execute();
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00045BD1 File Offset: 0x00043DD1
		public CalendarActionGroupIdResponse RenameCalendarGroup(Microsoft.Exchange.Services.Core.Types.ItemId groupId, string newGroupName)
		{
			return new RenameCalendarGroupCommand(CallContext.Current, groupId, newGroupName).Execute();
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00045BE4 File Offset: 0x00043DE4
		public CalendarActionResponse DeleteCalendarGroup(string groupId)
		{
			return new DeleteCalendarGroupCommand(CallContext.Current, groupId).Execute();
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00045BF6 File Offset: 0x00043DF6
		public CalendarActionFolderIdResponse CreateCalendar(string newCalendarName, string parentGroupGuid, string emailAddress)
		{
			return new CreateCalendarCommand(CallContext.Current, newCalendarName, parentGroupGuid, emailAddress).Execute();
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00045C0A File Offset: 0x00043E0A
		public CalendarActionFolderIdResponse RenameCalendar(Microsoft.Exchange.Services.Core.Types.ItemId itemId, string newCalendarName)
		{
			return new RenameCalendarCommand(CallContext.Current, itemId, newCalendarName).Execute();
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00045C1D File Offset: 0x00043E1D
		public CalendarActionResponse DeleteCalendar(Microsoft.Exchange.Services.Core.Types.ItemId itemId)
		{
			return new DeleteCalendarCommand(CallContext.Current, itemId).Execute();
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00045C2F File Offset: 0x00043E2F
		public CalendarActionItemIdResponse SetCalendarColor(Microsoft.Exchange.Services.Core.Types.ItemId itemId, CalendarColor calendarColor)
		{
			return new SetCalendarColorCommand(CallContext.Current, itemId, calendarColor).Execute();
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00045C42 File Offset: 0x00043E42
		public CalendarActionResponse MoveCalendar(FolderId calendarToMove, string parentGroupId, FolderId calendarBefore)
		{
			return new MoveCalendarCommand(CallContext.Current, calendarToMove, parentGroupId, calendarBefore).Execute();
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00045C56 File Offset: 0x00043E56
		public CalendarActionResponse SetCalendarGroupOrder(string groupToPosition, string beforeGroup)
		{
			return new SetCalendarGroupOrderCommand(CallContext.Current, groupToPosition, beforeGroup).Execute();
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00045C69 File Offset: 0x00043E69
		public GetCalendarFoldersResponse GetCalendarFolders()
		{
			return new GetCalendarFoldersCommand(CallContext.Current).Execute();
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00045C7A File Offset: 0x00043E7A
		public GetCalendarFolderConfigurationResponse GetCalendarFolderConfiguration(GetCalendarFolderConfigurationRequest request)
		{
			return this.jsonService.GetCalendarFolderConfiguration(request);
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00045C88 File Offset: 0x00043E88
		public GetModernAttachmentsResponse GetModernAttachments(GetModernAttachmentsRequest request)
		{
			return new GetModernAttachmentsCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00045C9A File Offset: 0x00043E9A
		public GetPersonaNotesResponse GetNotesForPersona(GetNotesForPersonaRequest getNotesForPersonaRequest)
		{
			return new GetPersonaNotesCommand(CallContext.Current, getNotesForPersonaRequest.PersonaId, getNotesForPersonaRequest.EmailAddress, getNotesForPersonaRequest.MaxBytesToFetch, getNotesForPersonaRequest.ParentFolderId).Execute();
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00045CC3 File Offset: 0x00043EC3
		public GetPersonaOrganizationHierarchyResponse GetOrganizationHierarchyForPersona(GetOrganizationHierarchyForPersonaRequest getOrganizationHierarchyForPersonaRequest)
		{
			return new GetPersonaOrganizationHierarchyCommand(CallContext.Current, getOrganizationHierarchyForPersonaRequest.GalObjectGuid, getOrganizationHierarchyForPersonaRequest.EmailAddress).Execute();
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00045CE0 File Offset: 0x00043EE0
		public GetPersonaOrganizationHierarchyResponse GetPersonaOrganizationHierarchy(string galObjectGuid)
		{
			return new GetPersonaOrganizationHierarchyCommand(CallContext.Current, galObjectGuid, null).Execute();
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x00045CF3 File Offset: 0x00043EF3
		public GetPersonaNotesResponse GetPersonaNotes(string personaId, int maxBytesToFetch)
		{
			return new GetPersonaNotesCommand(CallContext.Current, personaId, null, maxBytesToFetch, null).Execute();
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00045D08 File Offset: 0x00043F08
		public GetGroupResponse GetGroup(Microsoft.Exchange.Services.Core.Types.ItemId itemId, string adObjectId, EmailAddressWrapper emailAddress, IndexedPageView paging, GetGroupResultSet resultSet)
		{
			return new GetGroupCommand(CallContext.Current, itemId, adObjectId, emailAddress, paging, resultSet, null).Execute();
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00045D21 File Offset: 0x00043F21
		public GetGroupResponse GetGroupInfo(GetGroupInfoRequest getGroupInfoRequest)
		{
			return new GetGroupCommand(CallContext.Current, getGroupInfoRequest.ItemId, getGroupInfoRequest.AdObjectId, getGroupInfoRequest.EmailAddress, getGroupInfoRequest.Paging, getGroupInfoRequest.ResultSet, getGroupInfoRequest.ParentFolderId).Execute();
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00045D58 File Offset: 0x00043F58
		public IAsyncResult BeginGetDlpPolicyTips(GetDlpPolicyTipsRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			GetDlpPolicyTipsAsyncResult getDlpPolicyTipsAsyncResult = new GetDlpPolicyTipsAsyncResult(asyncCallback, asyncState);
			getDlpPolicyTipsAsyncResult.GetDlpPolicyTipsCommand(request);
			return getDlpPolicyTipsAsyncResult;
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00045D78 File Offset: 0x00043F78
		public GetDlpPolicyTipsResponse EndGetDlpPolicyTips(IAsyncResult result)
		{
			GetDlpPolicyTipsAsyncResult getDlpPolicyTipsAsyncResult = result as GetDlpPolicyTipsAsyncResult;
			if (getDlpPolicyTipsAsyncResult != null)
			{
				return getDlpPolicyTipsAsyncResult.Response;
			}
			throw new InvalidOperationException("IAsyncResult is null or not of type GetDlpPolicyTipsAsyncResult");
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00045DA0 File Offset: 0x00043FA0
		public IAsyncResult BeginExecuteEwsProxy(EwsProxyRequestParameters request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginExecuteEwsProxy(request, asyncCallback, asyncState);
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x00045DB0 File Offset: 0x00043FB0
		public EwsProxyResponse EndExecuteEwsProxy(IAsyncResult result)
		{
			return this.jsonService.EndExecuteEwsProxy(result);
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00045DC0 File Offset: 0x00043FC0
		public bool LogDatapoint(Datapoint[] datapoints)
		{
			UserContext userContext = OWAService.GetUserContext();
			return new LogDatapoint(CallContext.Current, datapoints, new Action<IEnumerable<ILogEvent>>(OwaClientLogger.AppendToLog), new Action<IEnumerable<ILogEvent>>(OwaClientTraceLogger.AppendToLog), this.registerType, true, userContext.CurrentOwaVersion).Execute();
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00045E08 File Offset: 0x00044008
		public bool ConnectedAccountsNotification(bool isOWALogon)
		{
			return new ConnectedAccountsNotification(CallContext.Current, isOWALogon).Execute();
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00045E1A File Offset: 0x0004401A
		public UploadPhotoResponse UploadPhoto(UploadPhotoRequest request)
		{
			return new UploadPhoto(CallContext.Current, request).Execute();
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00045E2C File Offset: 0x0004402C
		public UploadPhotoResponse UploadPhotoFromForm()
		{
			return new UploadPhotoFromForm(CallContext.Current, HttpContext.Current.Request).Execute();
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00045E47 File Offset: 0x00044047
		public GetFlowConversationResponse GetFlowConversation(BaseFolderId folderId, int conversationCount)
		{
			return new GetFlowConversation(CallContext.Current, folderId, conversationCount).Execute();
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00045E5A File Offset: 0x0004405A
		public FindFlowConversationItemResponse FindFlowConversationItem(BaseFolderId folderId, string flowConversationId, int itemCount)
		{
			return new FindFlowConversationItem(CallContext.Current, folderId, flowConversationId, itemCount).Execute();
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x00045E6E File Offset: 0x0004406E
		public int VerifyCert(string certRawData)
		{
			return new VerifyCert(CallContext.Current, certRawData).Execute();
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x00045E80 File Offset: 0x00044080
		[Obsolete]
		public GetCertsResponse GetCerts(GetCertsRequest request)
		{
			return new GetCerts(CallContext.Current, request).Execute();
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00045E92 File Offset: 0x00044092
		public GetCertsResponse GetEncryptionCerts(GetCertsRequest request)
		{
			return new GetEncryptionCerts(CallContext.Current, request).Execute();
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x00045EA4 File Offset: 0x000440A4
		public GetCertsInfoResponse GetCertsInfo(string certRawData, bool isSend)
		{
			return new GetCertsInfo(CallContext.Current, certRawData, isSend).Execute();
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00045EB7 File Offset: 0x000440B7
		public string GetMime(Microsoft.Exchange.Services.Core.Types.ItemId itemId)
		{
			return new GetMime(CallContext.Current, itemId).Execute();
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00045EC9 File Offset: 0x000440C9
		public AttachmentDataProvider AddAttachmentDataProvider(AttachmentDataProvider attachmentDataProvider)
		{
			return new AddAttachmentDataProvider(CallContext.Current, attachmentDataProvider).Execute();
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00045EDB File Offset: 0x000440DB
		public AttachmentDataProvider[] GetAttachmentDataProviders()
		{
			return this.GetAllAttachmentDataProviders(null);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x00045EE4 File Offset: 0x000440E4
		public AttachmentDataProvider[] GetAllAttachmentDataProviders(GetAttachmentDataProvidersRequest request)
		{
			return new GetAttachmentDataProviders(CallContext.Current, request).Execute();
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00045EF6 File Offset: 0x000440F6
		public AttachmentDataProviderType GetAttachmentDataProviderTypes()
		{
			return new GetAttachmentDataProviderTypes(CallContext.Current).Execute();
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x00045F07 File Offset: 0x00044107
		public GetAttachmentDataProviderItemsResponse GetAttachmentDataProviderItems(GetAttachmentDataProviderItemsRequest request)
		{
			return new GetAttachmentDataProviderItems(CallContext.Current, request).Execute();
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x00045F19 File Offset: 0x00044119
		public GetAttachmentDataProviderItemsResponse GetAttachmentDataProviderRecentItems()
		{
			return new GetAttachmentDataProvidersRecentItems(CallContext.Current).Execute();
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00045F2C File Offset: 0x0004412C
		public GetAttachmentDataProviderItemsResponse GetAttachmentDataProviderGroups()
		{
			UserContext userContext = UserContextManager.GetUserContext(CallContext.Current.HttpContext);
			if (!userContext.FeaturesManager.ClientServerSettings.ModernGroups.Enabled)
			{
				throw new OwaNotSupportedException("This method is not supported.");
			}
			return new GetAttachmentDataProviderGroups(CallContext.Current).Execute();
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00045F7D File Offset: 0x0004417D
		public bool CancelAttachment(string cancellationId)
		{
			return new CancelAttachment(CallContext.Current, cancellationId).Execute();
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00045F8F File Offset: 0x0004418F
		public string CreateReferenceAttachmentFromLocalFile(CreateReferenceAttachmentFromLocalFileRequest requestObject)
		{
			return new CreateReferenceAttachmentFromLocalFile(CallContext.Current, requestObject, true).Execute();
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00045FA4 File Offset: 0x000441A4
		public string CreateAttachmentFromAttachmentDataProvider(Microsoft.Exchange.Services.Core.Types.ItemId itemId, string attachmentDataProviderId, string location, string attachmentId, string subscriptionId, string channelId, string dataProviderParentItemId, string providerEndpointUrl, string cancellationId = null)
		{
			return new CreateAttachmentFromAttachmentDataProvider(CallContext.Current, itemId, attachmentDataProviderId, location, attachmentId, subscriptionId, dataProviderParentItemId, providerEndpointUrl, channelId, cancellationId).Execute();
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00045FCF File Offset: 0x000441CF
		public CreateAttachmentResponse CreateReferenceAttachmentFromAttachmentDataProvider(Microsoft.Exchange.Services.Core.Types.ItemId itemId, string attachmentDataProviderId, string location, string attachmentId, string dataProviderParentItemId, string providerEndpointUrl, string cancellationId = null)
		{
			return new CreateReferenceAttachmentFromAttachmentDataProvider(CallContext.Current, itemId, attachmentDataProviderId, location, attachmentId, dataProviderParentItemId, providerEndpointUrl, null).Execute();
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00045FEA File Offset: 0x000441EA
		public string GetAttachmentDataProviderUploadFolderName()
		{
			return new GetAttachmentDataProviderUploadFolderName(CallContext.Current).Execute();
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00045FFB File Offset: 0x000441FB
		public bool SetFolderMruConfiguration(TargetFolderMruConfiguration folderMruConfiguration)
		{
			return new SetFolderMruConfiguration(CallContext.Current, folderMruConfiguration).Execute();
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x0004600D File Offset: 0x0004420D
		public int InstantMessageSignIn(bool signedInManually)
		{
			return new InstantMessageSignIn(CallContext.Current, signedInManually).Execute();
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x0004601F File Offset: 0x0004421F
		public int InstantMessageSignOut(bool reserved)
		{
			return new InstantMessageSignOut(CallContext.Current).Execute();
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00046030 File Offset: 0x00044230
		public int SendChatMessage(ChatMessage message)
		{
			return new SendChatMessage(CallContext.Current, message).Execute();
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00046042 File Offset: 0x00044242
		public bool TerminateChatSession(int chatSessionId)
		{
			return new TerminateChatSession(CallContext.Current, chatSessionId).Execute();
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x00046054 File Offset: 0x00044254
		public int AcceptChatSession(int chatSessionId)
		{
			return new AcceptChatSession(CallContext.Current, chatSessionId).Execute();
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x00046066 File Offset: 0x00044266
		public bool AcceptBuddy(InstantMessageBuddy instantMessageBuddy, InstantMessageGroup instantMessageGroup)
		{
			return new AcceptBuddy(CallContext.Current, instantMessageBuddy, instantMessageGroup).Execute();
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x00046079 File Offset: 0x00044279
		public bool AddImBuddy(InstantMessageBuddy instantMessageBuddy, InstantMessageGroup instantMessageGroup)
		{
			return new AddImBuddy(CallContext.Current, instantMessageBuddy, instantMessageGroup).Execute();
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x0004608C File Offset: 0x0004428C
		public bool DeclineBuddy(InstantMessageBuddy instantMessageBuddy)
		{
			return new DeclineBuddy(CallContext.Current, instantMessageBuddy).Execute();
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x0004609E File Offset: 0x0004429E
		public bool RemoveBuddy(InstantMessageBuddy instantMessageBuddy, Microsoft.Exchange.Services.Core.Types.ItemId contactId)
		{
			return new RemoveBuddy(CallContext.Current, instantMessageBuddy, contactId).Execute();
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x000460B1 File Offset: 0x000442B1
		public bool AddFavorite(InstantMessageBuddy instantMessageBuddy)
		{
			return new AddFavoriteCommand(CallContext.Current, instantMessageBuddy).Execute();
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x000460C3 File Offset: 0x000442C3
		public bool RemoveFavorite(Microsoft.Exchange.Services.Core.Types.ItemId personaId)
		{
			return new RemoveFavoriteCommand(CallContext.Current, personaId).Execute();
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x000460D5 File Offset: 0x000442D5
		public bool NotifyAppWipe(DataWipeReason wipeReason)
		{
			return new NotifyAppWipe(CallContext.Current, wipeReason).Execute();
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x000460E7 File Offset: 0x000442E7
		public bool NotifyTyping(int chatSessionId, bool typingCancelled)
		{
			return new NotifyTyping(CallContext.Current, chatSessionId, typingCancelled).Execute();
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x000460FA File Offset: 0x000442FA
		public int SetPresence(InstantMessagePresence presenceSetting)
		{
			return new ChangePresence(CallContext.Current, new InstantMessagePresenceType?(presenceSetting.Presence)).Execute();
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00046116 File Offset: 0x00044316
		public int ResetPresence()
		{
			return new ResetPresence(CallContext.Current).Execute();
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00046127 File Offset: 0x00044327
		public int GetPresence(string[] sipUris)
		{
			return new GetPresence(CallContext.Current, sipUris).Execute();
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00046139 File Offset: 0x00044339
		public int SubscribeForPresenceUpdates(string[] sipUris)
		{
			return new SubscribeForPresenceUpdates(CallContext.Current, sipUris).Execute();
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0004614B File Offset: 0x0004434B
		public int UnsubscribeFromPresenceUpdates(string sipUri)
		{
			return new UnsubscribeFromPresenceUpdates(CallContext.Current, sipUri).Execute();
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x0004615D File Offset: 0x0004435D
		public ProxySettings[] GetInstantMessageProxySettings(string[] userPrincipalNames)
		{
			return new GetInstantMessageProxySettings(CallContext.Current, userPrincipalNames).Execute();
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x0004616F File Offset: 0x0004436F
		public SubscriptionResponseData[] SubscribeToNotification(NotificationSubscribeJsonRequest request, SubscriptionData[] subscriptionData)
		{
			return new SubscribeToNotification(request, CallContext.Current, subscriptionData).Execute();
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x00046182 File Offset: 0x00044382
		public bool UnsubscribeToNotification(SubscriptionData[] subscriptionData)
		{
			return new UnsubscribeToNotification(CallContext.Current, subscriptionData).Execute();
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00046194 File Offset: 0x00044394
		public SubscriptionResponseData[] SubscribeToGroupNotification(NotificationSubscribeJsonRequest request, SubscriptionData[] subscriptionData)
		{
			return new SubscribeToGroupNotification(request, CallContext.Current, subscriptionData).Execute();
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x000461A7 File Offset: 0x000443A7
		public bool UnsubscribeToGroupNotification(SubscriptionData[] subscriptionData)
		{
			return new UnsubscribeToGroupNotification(CallContext.Current, subscriptionData).Execute();
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x000461B9 File Offset: 0x000443B9
		public SubscriptionResponseData[] SubscribeToGroupUnseenNotification(NotificationSubscribeJsonRequest request, SubscriptionData[] subscriptionData)
		{
			return new SubscribeToGroupUnseenNotification(request, CallContext.Current, subscriptionData).Execute();
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x000461CC File Offset: 0x000443CC
		public bool UnsubscribeToGroupUnseenNotification(SubscriptionData[] subscriptionData)
		{
			return new UnsubscribeToGroupUnseenNotification(CallContext.Current, subscriptionData).Execute();
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x000461DE File Offset: 0x000443DE
		public bool AddSharedFolders(string displayName, string primarySMTPAddress)
		{
			return new AddSharedFolders(CallContext.Current, displayName, primarySMTPAddress).Execute();
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x000461F1 File Offset: 0x000443F1
		public bool RemoveSharedFolders(string primarySMTPAddress)
		{
			return new RemoveSharedFolders(CallContext.Current, primarySMTPAddress).Execute();
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00046203 File Offset: 0x00044403
		public OwaOtherMailboxConfiguration GetOtherMailboxConfiguration()
		{
			return new GetOtherMailboxConfiguration(CallContext.Current).Execute();
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00046214 File Offset: 0x00044414
		public NavBarData GetBposNavBarData()
		{
			return new GetBposNavBarData(CallContext.Current).Execute();
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00046225 File Offset: 0x00044425
		public NavBarData GetBposShellInfoNavBarData()
		{
			return new GetBposShellInfoNavBarData(CallContext.Current).Execute();
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00046263 File Offset: 0x00044463
		public OwaUserConfiguration GetOwaUserConfiguration()
		{
			return new GetOwaUserConfiguration(CallContext.Current, PlacesConfigurationCache.Instance, WeatherConfigurationCache.Instance, this.registerType, () => VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.WindowsLiveID.Enabled).Execute();
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x000462A1 File Offset: 0x000444A1
		public Alert[] GetSystemAlerts()
		{
			return new GetSystemAlerts(CallContext.Current).Execute();
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x000462B2 File Offset: 0x000444B2
		public bool SetNotificationSettings(NotificationSettingsJsonRequest settings)
		{
			return new SetNotificationSettings(CallContext.Current, settings.Body).Execute();
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x000462C9 File Offset: 0x000444C9
		public ComplianceConfiguration GetComplianceConfiguration()
		{
			return new GetComplianceConfiguration(CallContext.Current).Execute();
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x000462DA File Offset: 0x000444DA
		public TargetFolderMruConfiguration GetFolderMruConfiguration()
		{
			return new GetFolderMruConfiguration(CallContext.Current).Execute();
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x000462EB File Offset: 0x000444EB
		public UcwaUserConfiguration GetUcwaUserConfiguration(string sipUri)
		{
			return new GetUcwaUserConfiguration(CallContext.Current, sipUri).Execute();
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x000462FD File Offset: 0x000444FD
		public OnlineMeetingType CreateOnlineMeeting(string sipUri, Microsoft.Exchange.Services.Core.Types.ItemId itemId)
		{
			return new CreateOnlineMeeting(CallContext.Current, sipUri, itemId, false).Execute();
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00046311 File Offset: 0x00044511
		public OnlineMeetingType CreateMeetNow(string sipUri, string subject)
		{
			return new CreateMeetNow(CallContext.Current, sipUri, subject, true).Execute();
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00046325 File Offset: 0x00044525
		public string GetWacIframeUrl(string attachmentId)
		{
			return new GetWacIframeUrl(CallContext.Current, attachmentId).Execute();
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00046337 File Offset: 0x00044537
		public string GetWacIframeUrlForOneDrive(GetWacIframeUrlForOneDriveRequest request)
		{
			return new GetWacIframeUrlForOneDrive(CallContext.Current, request).Execute();
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x00046349 File Offset: 0x00044549
		public WacAttachmentType GetWacAttachmentInfo(string attachmentId, bool isEdit, string draftId)
		{
			return new GetWacAttachmentInfo(CallContext.Current, attachmentId, isEdit, draftId).Execute();
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0004635D File Offset: 0x0004455D
		public GetWellKnownShapesResponse GetWellKnownShapes()
		{
			return new GetWellKnownShapes(CallContext.Current).Execute();
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x0004636E File Offset: 0x0004456E
		public string CreateResendDraft(string ndrMessageId, string draftsFolderId)
		{
			return new CreateResendDraft(CallContext.Current, ndrMessageId, draftsFolderId).Execute();
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00046381 File Offset: 0x00044581
		public SaveExtensionSettingsResponse SaveExtensionSettings(SaveExtensionSettingsParameters request)
		{
			return this.jsonService.SaveExtensionSettings(request);
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x00046390 File Offset: 0x00044590
		public CreateAttachmentJsonResponse CreateAttachmentFromLocalFile(CreateAttachmentJsonRequest request)
		{
			return new CreateAttachmentJsonResponse
			{
				Body = new CreateAttachmentFromLocalFile(CallContext.Current, request.Body).Execute()
			};
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x000463C0 File Offset: 0x000445C0
		public CreateAttachmentJsonResponse CreateAttachmentFromForm()
		{
			return new CreateAttachmentJsonResponse
			{
				Body = new CreateAttachmentFromForm(CallContext.Current, HttpContext.Current.Request).Execute()
			};
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x000463F3 File Offset: 0x000445F3
		public string UploadAndShareAttachmentFromForm()
		{
			return new CreateReferenceAttachmentFromLocalFile(CallContext.Current, CreateAttachmentHelper.CreateReferenceAttachmentRequest(HttpContext.Current.Request), false).Execute();
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x00046414 File Offset: 0x00044614
		public string UpdateAttachmentPermissions(UpdateAttachmentPermissionsRequest permissionsRequest)
		{
			return new UpdateAttachmentPermissions(CallContext.Current, permissionsRequest).Execute();
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00046426 File Offset: 0x00044626
		public LoadExtensionCustomPropertiesResponse LoadExtensionCustomProperties(LoadExtensionCustomPropertiesParameters request)
		{
			return this.jsonService.LoadExtensionCustomProperties(request);
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x00046434 File Offset: 0x00044634
		public SaveExtensionCustomPropertiesResponse SaveExtensionCustomProperties(SaveExtensionCustomPropertiesParameters request)
		{
			return this.jsonService.SaveExtensionCustomProperties(request);
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x00046442 File Offset: 0x00044642
		public Persona UpdatePersona(UpdatePersonaJsonRequest request)
		{
			return this.jsonService.UpdatePersona(request);
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00046450 File Offset: 0x00044650
		public DeletePersonaJsonResponse DeletePersona(Microsoft.Exchange.Services.Core.Types.ItemId personaId, BaseFolderId folderId)
		{
			return this.jsonService.DeletePersona(personaId, folderId);
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x0004645F File Offset: 0x0004465F
		public MaskAutoCompleteRecipientResponse MaskAutoCompleteRecipient(MaskAutoCompleteRecipientRequest request)
		{
			return this.jsonService.MaskAutoCompleteRecipient(request);
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0004646D File Offset: 0x0004466D
		public Persona CreatePersona(CreatePersonaJsonRequest request)
		{
			return this.jsonService.CreatePersona(request);
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0004647C File Offset: 0x0004467C
		public CreateModernGroupResponse CreateModernGroup(CreateModernGroupRequest request)
		{
			UserContext userContext = OWAService.GetUserContext();
			bool groupCreationEnabledFromOwaMailboxPolicy = OWAService.GetGroupCreationEnabledFromOwaMailboxPolicy();
			if (userContext.FeaturesManager.ClientServerSettings.ModernGroups.Enabled && groupCreationEnabledFromOwaMailboxPolicy)
			{
				return new CreateModernGroupCommand(CallContext.Current, request).Execute();
			}
			throw new OwaNotSupportedException("This method is not supported.");
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x000464D0 File Offset: 0x000446D0
		public CreateUnifiedGroupResponse CreateUnifiedGroup(CreateUnifiedGroupRequest request)
		{
			UserContext userContext = OWAService.GetUserContext();
			bool groupCreationEnabledFromOwaMailboxPolicy = OWAService.GetGroupCreationEnabledFromOwaMailboxPolicy();
			if (userContext.FeaturesManager.ClientServerSettings.ModernGroups.Enabled && groupCreationEnabledFromOwaMailboxPolicy)
			{
				return new CreateUnifiedGroupCommand(CallContext.Current, request).Execute();
			}
			throw new OwaNotSupportedException("This method is not supported.");
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x00046521 File Offset: 0x00044721
		public FindMembersInUnifiedGroupResponse FindMembersInUnifiedGroup(FindMembersInUnifiedGroupRequest request)
		{
			return new FindMembersInUnifiedGroupCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x00046533 File Offset: 0x00044733
		public GetRegionalConfigurationResponse GetRegionalConfiguration(GetRegionalConfigurationRequest request)
		{
			return new GetRegionalConfiguration(CallContext.Current, request).Execute();
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x00046545 File Offset: 0x00044745
		public AddMembersToUnifiedGroupResponse AddMembersToUnifiedGroup(AddMembersToUnifiedGroupRequest request)
		{
			return new AddMembersToUnifiedGroupCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x00046557 File Offset: 0x00044757
		public UpdateModernGroupResponse UpdateModernGroup(UpdateModernGroupRequest request)
		{
			return new UpdateModernGroupCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00046569 File Offset: 0x00044769
		public RemoveModernGroupResponse RemoveModernGroup(RemoveModernGroupRequest request)
		{
			return new RemoveModernGroupCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0004657B File Offset: 0x0004477B
		public ModernGroupMembershipRequestMessageDetailsResponse ModernGroupMembershipRequestMessageDetails(ModernGroupMembershipRequestMessageDetailsRequest request)
		{
			return new ModernGroupMembershipRequestMessageDetailsCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0004658D File Offset: 0x0004478D
		public ValidateModernGroupAliasResponse ValidateModernGroupAlias(ValidateModernGroupAliasRequest request)
		{
			return new ValidateModernGroupAliasCommand(CallContext.Current, request).Execute();
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0004659F File Offset: 0x0004479F
		public GetModernGroupDomainResponse GetModernGroupDomain()
		{
			return new GetModernGroupDomainCommand(CallContext.Current).Execute();
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x000465B0 File Offset: 0x000447B0
		public Microsoft.Exchange.Services.Core.Types.ItemId[] GetPersonaSuggestions(Microsoft.Exchange.Services.Core.Types.ItemId personaId)
		{
			return this.jsonService.GetPersonaSuggestions(personaId);
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x000465BE File Offset: 0x000447BE
		public Persona UnlinkPersona(Microsoft.Exchange.Services.Core.Types.ItemId personaId, Microsoft.Exchange.Services.Core.Types.ItemId contactId)
		{
			return this.jsonService.UnlinkPersona(personaId, contactId);
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x000465CD File Offset: 0x000447CD
		public Persona AcceptPersonaLinkSuggestion(Microsoft.Exchange.Services.Core.Types.ItemId linkToPersonaId, Microsoft.Exchange.Services.Core.Types.ItemId suggestedPersonaId)
		{
			return this.jsonService.AcceptPersonaLinkSuggestion(linkToPersonaId, suggestedPersonaId);
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x000465DC File Offset: 0x000447DC
		public Persona LinkPersona(Microsoft.Exchange.Services.Core.Types.ItemId linkToPersonaId, Microsoft.Exchange.Services.Core.Types.ItemId personaIdToBeLinked)
		{
			return this.jsonService.LinkPersona(linkToPersonaId, personaIdToBeLinked);
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x000465EB File Offset: 0x000447EB
		public Persona RejectPersonaLinkSuggestion(Microsoft.Exchange.Services.Core.Types.ItemId personaId, Microsoft.Exchange.Services.Core.Types.ItemId suggestedPersonaId)
		{
			return this.jsonService.RejectPersonaLinkSuggestion(personaId, suggestedPersonaId);
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x000465FA File Offset: 0x000447FA
		public SyncCalendarResponse SyncCalendar(SyncCalendarParameters request)
		{
			return this.jsonService.SyncCalendar(request);
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00046608 File Offset: 0x00044808
		public bool SendReadReceipt(Microsoft.Exchange.Services.Core.Types.ItemId itemId)
		{
			return this.jsonService.SendReadReceipt(itemId);
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00046616 File Offset: 0x00044816
		public IAsyncResult BeginRequestDeviceRegistrationChallenge(RequestDeviceRegistrationChallengeJsonRequest deviceRegistrationChallengeRequest, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginRequestDeviceRegistrationChallenge(deviceRegistrationChallengeRequest, asyncCallback, asyncState);
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00046626 File Offset: 0x00044826
		public RequestDeviceRegistrationChallengeJsonResponse EndRequestDeviceRegistrationChallenge(IAsyncResult result)
		{
			return this.jsonService.EndRequestDeviceRegistrationChallenge(result);
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00046634 File Offset: 0x00044834
		public IAsyncResult BeginSubscribeToPushNotification(SubscribeToPushNotificationJsonRequest pushNotificationSubscription, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginSubscribeToPushNotification(pushNotificationSubscription, asyncCallback, asyncState);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00046644 File Offset: 0x00044844
		public SubscribeToPushNotificationJsonResponse EndSubscribeToPushNotification(IAsyncResult result)
		{
			return this.jsonService.EndSubscribeToPushNotification(result);
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00046652 File Offset: 0x00044852
		public IAsyncResult BeginUnsubscribeToPushNotification(UnsubscribeToPushNotificationJsonRequest pushNotificationSubscription, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginUnsubscribeToPushNotification(pushNotificationSubscription, asyncCallback, asyncState);
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00046662 File Offset: 0x00044862
		public UnsubscribeToPushNotificationJsonResponse EndUnsubscribeToPushNotification(IAsyncResult result)
		{
			return this.jsonService.EndUnsubscribeToPushNotification(result);
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00046670 File Offset: 0x00044870
		public int PingOwa()
		{
			return new PingOwa(CallContext.Current).Execute();
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00046681 File Offset: 0x00044881
		public string SanitizeHtml(string input)
		{
			return new SanitizeHtmlCommand(CallContext.Current, input).Execute();
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00046693 File Offset: 0x00044893
		public IAsyncResult BeginSynchronizeWacAttachment(SynchronizeWacAttachmentRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return new SynchronizeWacAttachment(CallContext.Current, request.AttachmentId, asyncCallback, asyncState).Execute();
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x000466AC File Offset: 0x000448AC
		public SynchronizeWacAttachmentResponse EndSynchronizeWacAttachment(IAsyncResult result)
		{
			ServiceAsyncResult<SynchronizeWacAttachmentResponse> serviceAsyncResult = result as ServiceAsyncResult<SynchronizeWacAttachmentResponse>;
			if (serviceAsyncResult != null)
			{
				return serviceAsyncResult.Data;
			}
			throw new InvalidOperationException("IAsyncResult is null or not of type ServiceAsyncResult<SynchronizeWacAttachmentResponse>");
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x000466D4 File Offset: 0x000448D4
		public IAsyncResult BeginPublishO365Notification(O365Notification notification, AsyncCallback asyncCallback, object asyncState)
		{
			return new PublishO365Notification(CallContext.Current, notification, asyncCallback, asyncState).Execute();
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x000466E8 File Offset: 0x000448E8
		public bool EndPublishO365Notification(IAsyncResult result)
		{
			ServiceAsyncResult<bool> serviceAsyncResult = result as ServiceAsyncResult<bool>;
			return serviceAsyncResult.Data;
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x00046702 File Offset: 0x00044902
		[OperationBehavior(AutoDisposeParameters = false)]
		public Stream GetFileAttachment(string id, bool isImagePreview, bool asDataUri)
		{
			return new GetAttachment(CallContext.Current, id, isImagePreview, asDataUri).Execute();
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00046716 File Offset: 0x00044916
		[OperationBehavior(AutoDisposeParameters = false)]
		public Stream GetAllAttachmentsAsZip(string id)
		{
			return new GetAllAttachmentsAsZip(CallContext.Current, id).Execute();
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x00046728 File Offset: 0x00044928
		[OperationBehavior(AutoDisposeParameters = false)]
		public Stream GetPersonaPhoto(string personId, string adObjectId, string email, string singleSourceId, UserPhotoSize size)
		{
			return new GetPersonaPhoto(CallContext.Current, personId, adObjectId, email, singleSourceId, size).Execute();
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x00046740 File Offset: 0x00044940
		public IAsyncResult BeginAddDelegate(AddDelegateJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginAddDelegate(request, asyncCallback, asyncState);
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x00046750 File Offset: 0x00044950
		public IAsyncResult BeginAddDistributionGroupToImList(AddDistributionGroupToImListJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginAddDistributionGroupToImList(request, asyncCallback, asyncState);
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x00046760 File Offset: 0x00044960
		public IAsyncResult BeginAddImContactToGroup(AddImContactToGroupJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginAddImContactToGroup(request, asyncCallback, asyncState);
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00046770 File Offset: 0x00044970
		public IAsyncResult BeginAddImGroup(AddImGroupJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginAddImGroup(request, asyncCallback, asyncState);
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x00046780 File Offset: 0x00044980
		public IAsyncResult BeginAddNewImContactToGroup(AddNewImContactToGroupJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginAddNewImContactToGroup(request, asyncCallback, asyncState);
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00046790 File Offset: 0x00044990
		public IAsyncResult BeginAddNewTelUriContactToGroup(AddNewTelUriContactToGroupJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginAddNewTelUriContactToGroup(request, asyncCallback, asyncState);
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x000467A0 File Offset: 0x000449A0
		public IAsyncResult BeginApplyConversationAction(ApplyConversationActionJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginApplyConversationAction(request, asyncCallback, asyncState);
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x000467B0 File Offset: 0x000449B0
		public IAsyncResult BeginConvertId(ConvertIdJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginConvertId(request, asyncCallback, asyncState);
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x000467C0 File Offset: 0x000449C0
		public IAsyncResult BeginCopyFolder(CopyFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginCopyFolder(request, asyncCallback, asyncState);
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x000467D0 File Offset: 0x000449D0
		public IAsyncResult BeginCopyItem(CopyItemJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginCopyItem(request, asyncCallback, asyncState);
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x000467E0 File Offset: 0x000449E0
		public IAsyncResult BeginCreateAttachment(CreateAttachmentJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginCreateAttachment(request, asyncCallback, asyncState);
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x000467F0 File Offset: 0x000449F0
		public IAsyncResult BeginCreateFolder(CreateFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginCreateFolder(request, asyncCallback, asyncState);
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x00046800 File Offset: 0x00044A00
		public IAsyncResult BeginCreateItem(CreateItemJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginCreateItem(request, asyncCallback, asyncState);
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x00046810 File Offset: 0x00044A10
		public IAsyncResult BeginPostModernGroupItem(PostModernGroupItemJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginPostModernGroupItem(request, asyncCallback, asyncState);
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00046820 File Offset: 0x00044A20
		public IAsyncResult BeginUpdateAndPostModernGroupItem(UpdateAndPostModernGroupItemJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginUpdateAndPostModernGroupItem(request, asyncCallback, asyncState);
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00046830 File Offset: 0x00044A30
		public IAsyncResult BeginCreateResponseFromModernGroup(CreateResponseFromModernGroupJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginCreateResponseFromModernGroup(request, asyncCallback, asyncState);
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00046840 File Offset: 0x00044A40
		public IAsyncResult BeginCreateManagedFolder(CreateManagedFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginCreateManagedFolder(request, asyncCallback, asyncState);
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00046850 File Offset: 0x00044A50
		public IAsyncResult BeginCreateUserConfiguration(CreateUserConfigurationJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginCreateUserConfiguration(request, asyncCallback, asyncState);
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00046860 File Offset: 0x00044A60
		public IAsyncResult BeginDeleteAttachment(DeleteAttachmentJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginDeleteAttachment(request, asyncCallback, asyncState);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00046870 File Offset: 0x00044A70
		public IAsyncResult BeginDeleteFolder(DeleteFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginDeleteFolder(request, asyncCallback, asyncState);
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00046880 File Offset: 0x00044A80
		public IAsyncResult BeginDeleteItem(DeleteItemJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginDeleteItem(request, asyncCallback, asyncState);
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00046890 File Offset: 0x00044A90
		public IAsyncResult BeginDeleteUserConfiguration(DeleteUserConfigurationJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginDeleteUserConfiguration(request, asyncCallback, asyncState);
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x000468A0 File Offset: 0x00044AA0
		public IAsyncResult BeginDisconnectPhoneCall(DisconnectPhoneCallJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginDisconnectPhoneCall(request, asyncCallback, asyncState);
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x000468B0 File Offset: 0x00044AB0
		public IAsyncResult BeginEmptyFolder(EmptyFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginEmptyFolder(request, asyncCallback, asyncState);
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x000468C0 File Offset: 0x00044AC0
		public IAsyncResult BeginExecuteDiagnosticMethod(ExecuteDiagnosticMethodJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginExecuteDiagnosticMethod(request, asyncCallback, asyncState);
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x000468D0 File Offset: 0x00044AD0
		public IAsyncResult BeginExpandDL(ExpandDLJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginExpandDL(request, asyncCallback, asyncState);
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x000468E0 File Offset: 0x00044AE0
		public IAsyncResult BeginExportItems(ExportItemsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginExportItems(request, asyncCallback, asyncState);
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x000468F0 File Offset: 0x00044AF0
		public IAsyncResult BeginFindConversation(FindConversationJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			bool isSearchCall = SearchUtil.IsSearch(request.Body.QueryString);
			this.RegisterOwaCallback(isSearchCall);
			return this.jsonService.BeginFindConversation(request, asyncCallback, asyncState);
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00046923 File Offset: 0x00044B23
		public IAsyncResult BeginFindTrendingConversation(FindTrendingConversationJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginFindTrendingConversation(request, asyncCallback, asyncState);
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00046933 File Offset: 0x00044B33
		public IAsyncResult BeginFindFolder(FindFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginFindFolder(request, asyncCallback, asyncState);
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00046944 File Offset: 0x00044B44
		public IAsyncResult BeginFindItem(FindItemJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			bool isSearchCall = SearchUtil.IsSearch(request.Body.QueryString);
			this.RegisterOwaCallback(isSearchCall);
			return this.jsonService.BeginFindItem(request, asyncCallback, asyncState);
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00046977 File Offset: 0x00044B77
		public IAsyncResult BeginFindMailboxStatisticsByKeywords(FindMailboxStatisticsByKeywordsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginFindMailboxStatisticsByKeywords(request, asyncCallback, asyncState);
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00046987 File Offset: 0x00044B87
		public IAsyncResult BeginFindMessageTrackingReport(FindMessageTrackingReportJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginFindMessageTrackingReport(request, asyncCallback, asyncState);
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00046997 File Offset: 0x00044B97
		public IAsyncResult BeginFindPeople(FindPeopleJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginFindPeople(request, asyncCallback, asyncState);
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x000469A7 File Offset: 0x00044BA7
		public IAsyncResult BeginSyncPeople(SyncPeopleJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginSyncPeople(request, asyncCallback, asyncState);
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x000469B7 File Offset: 0x00044BB7
		public IAsyncResult BeginSyncAutoCompleteRecipients(SyncAutoCompleteRecipientsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginSyncAutoCompleteRecipients(request, asyncCallback, asyncState);
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x000469C7 File Offset: 0x00044BC7
		public IAsyncResult BeginGetAttachment(GetAttachmentJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetAttachment(request, asyncCallback, asyncState);
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x000469D7 File Offset: 0x00044BD7
		public IAsyncResult BeginGetConversationItems(GetConversationItemsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetConversationItems(request, asyncCallback, asyncState);
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x000469E7 File Offset: 0x00044BE7
		public IAsyncResult BeginGetThreadedConversationItems(GetThreadedConversationItemsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetThreadedConversationItems(request, asyncCallback, asyncState);
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x000469F7 File Offset: 0x00044BF7
		public IAsyncResult BeginGetConversationItemsDiagnostics(GetConversationItemsDiagnosticsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetConversationItemsDiagnostics(request, asyncCallback, asyncState);
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00046A07 File Offset: 0x00044C07
		public IAsyncResult BeginGetDelegate(GetDelegateJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetDelegate(request, asyncCallback, asyncState);
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x00046A17 File Offset: 0x00044C17
		public IAsyncResult BeginGetEvents(GetEventsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetEvents(request, asyncCallback, asyncState);
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x00046A27 File Offset: 0x00044C27
		public IAsyncResult BeginGetFolder(GetFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetFolder(request, asyncCallback, asyncState);
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x00046A37 File Offset: 0x00044C37
		public IAsyncResult BeginGetHoldOnMailboxes(GetHoldOnMailboxesJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetHoldOnMailboxes(request, asyncCallback, asyncState);
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x00046A47 File Offset: 0x00044C47
		public IAsyncResult BeginGetImItemList(GetImItemListJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetImItemList(request, asyncCallback, asyncState);
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x00046A57 File Offset: 0x00044C57
		public IAsyncResult BeginGetImItems(GetImItemsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetImItems(request, asyncCallback, asyncState);
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x00046A67 File Offset: 0x00044C67
		public IAsyncResult BeginGetInboxRules(GetInboxRulesJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetInboxRules(request, asyncCallback, asyncState);
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x00046A77 File Offset: 0x00044C77
		public IAsyncResult BeginGetClientAccessToken(GetClientAccessTokenJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetClientAccessToken(request, asyncCallback, asyncState);
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x00046A87 File Offset: 0x00044C87
		public IAsyncResult BeginGetItem(GetItemJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetItem(request, asyncCallback, asyncState);
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x00046A97 File Offset: 0x00044C97
		public IAsyncResult BeginGetMailTips(GetMailTipsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetMailTips(request, asyncCallback, asyncState);
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x00046AA7 File Offset: 0x00044CA7
		public IAsyncResult BeginGetMessageTrackingReport(GetMessageTrackingReportJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetMessageTrackingReport(request, asyncCallback, asyncState);
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x00046AB7 File Offset: 0x00044CB7
		public IAsyncResult BeginGetPasswordExpirationDate(GetPasswordExpirationDateJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetPasswordExpirationDate(request, asyncCallback, asyncState);
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x00046AC7 File Offset: 0x00044CC7
		public IAsyncResult BeginGetPersona(GetPersonaJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetPersona(request, asyncCallback, asyncState);
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x00046AD7 File Offset: 0x00044CD7
		public IAsyncResult BeginGetPhoneCallInformation(GetPhoneCallInformationJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetPhoneCallInformation(request, asyncCallback, asyncState);
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00046AE7 File Offset: 0x00044CE7
		public IAsyncResult BeginGetReminders(GetRemindersJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetReminders(request, asyncCallback, asyncState);
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00046AF7 File Offset: 0x00044CF7
		public IAsyncResult BeginPerformReminderAction(PerformReminderActionJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginPerformReminderAction(request, asyncCallback, asyncState);
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x00046B07 File Offset: 0x00044D07
		public IAsyncResult BeginGetRoomLists(GetRoomListsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetRoomLists(request, asyncCallback, asyncState);
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00046B17 File Offset: 0x00044D17
		public IAsyncResult BeginGetRooms(GetRoomsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetRooms(request, asyncCallback, asyncState);
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x00046B27 File Offset: 0x00044D27
		public IAsyncResult BeginGetSearchableMailboxes(GetSearchableMailboxesJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetSearchableMailboxes(request, asyncCallback, asyncState);
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x00046B37 File Offset: 0x00044D37
		public IAsyncResult BeginGetServerTimeZones(GetServerTimeZonesJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetServerTimeZones(request, asyncCallback, asyncState);
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x00046B47 File Offset: 0x00044D47
		public IAsyncResult BeginGetServiceConfiguration(GetServiceConfigurationJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetServiceConfiguration(request, asyncCallback, asyncState);
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x00046B57 File Offset: 0x00044D57
		public IAsyncResult BeginGetSharingFolder(GetSharingFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetSharingFolder(request, asyncCallback, asyncState);
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00046B67 File Offset: 0x00044D67
		public IAsyncResult BeginGetSharingMetadata(GetSharingMetadataJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetSharingMetadata(request, asyncCallback, asyncState);
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x00046B77 File Offset: 0x00044D77
		public IAsyncResult BeginGetUserAvailability(GetUserAvailabilityJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetUserAvailability(request, asyncCallback, asyncState);
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00046B87 File Offset: 0x00044D87
		public IAsyncResult BeginGetUserConfiguration(GetUserConfigurationJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetUserConfiguration(request, asyncCallback, asyncState);
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x00046B97 File Offset: 0x00044D97
		public IAsyncResult BeginGetUserOofSettings(GetUserOofSettingsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetUserOofSettings(request, asyncCallback, asyncState);
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00046BA7 File Offset: 0x00044DA7
		public IAsyncResult BeginGetModernConversationAttachments(GetModernConversationAttachmentsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetModernConversationAttachments(request, asyncCallback, asyncState);
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00046BB7 File Offset: 0x00044DB7
		public IAsyncResult BeginGetUserRetentionPolicyTags(GetUserRetentionPolicyTagsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetUserRetentionPolicyTags(request, asyncCallback, asyncState);
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00046BC7 File Offset: 0x00044DC7
		public IAsyncResult BeginMarkAllItemsAsRead(MarkAllItemsAsReadJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginMarkAllItemsAsRead(request, asyncCallback, asyncState);
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00046BD7 File Offset: 0x00044DD7
		public IAsyncResult BeginMarkAsJunk(MarkAsJunkJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginMarkAsJunk(request, asyncCallback, asyncState);
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00046BE7 File Offset: 0x00044DE7
		public IAsyncResult BeginMoveFolder(MoveFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginMoveFolder(request, asyncCallback, asyncState);
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x00046BF7 File Offset: 0x00044DF7
		public IAsyncResult BeginMoveItem(MoveItemJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginMoveItem(request, asyncCallback, asyncState);
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x00046C07 File Offset: 0x00044E07
		public IAsyncResult BeginPlayOnPhone(PlayOnPhoneJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginPlayOnPhone(request, asyncCallback, asyncState);
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x00046C17 File Offset: 0x00044E17
		public IAsyncResult BeginProvision(ProvisionJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginProvision(request, asyncCallback, asyncState);
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x00046C27 File Offset: 0x00044E27
		public IAsyncResult BeginDeprovision(DeprovisionJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginDeprovision(request, asyncCallback, asyncState);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x00046C37 File Offset: 0x00044E37
		public IAsyncResult BeginRefreshSharingFolder(RefreshSharingFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginRefreshSharingFolder(request, asyncCallback, asyncState);
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x00046C47 File Offset: 0x00044E47
		public IAsyncResult BeginRemoveContactFromImList(RemoveContactFromImListJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginRemoveContactFromImList(request, asyncCallback, asyncState);
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00046C57 File Offset: 0x00044E57
		public IAsyncResult BeginRemoveDelegate(RemoveDelegateJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginRemoveDelegate(request, asyncCallback, asyncState);
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x00046C67 File Offset: 0x00044E67
		public IAsyncResult BeginRemoveDistributionGroupFromImList(RemoveDistributionGroupFromImListJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginRemoveDistributionGroupFromImList(request, asyncCallback, asyncState);
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00046C77 File Offset: 0x00044E77
		public IAsyncResult BeginRemoveImContactFromGroup(RemoveImContactFromGroupJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginRemoveImContactFromGroup(request, asyncCallback, asyncState);
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x00046C87 File Offset: 0x00044E87
		public IAsyncResult BeginRemoveImGroup(RemoveImGroupJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginRemoveImGroup(request, asyncCallback, asyncState);
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x00046C97 File Offset: 0x00044E97
		public IAsyncResult BeginResolveNames(ResolveNamesJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginResolveNames(request, asyncCallback, asyncState);
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x00046CA7 File Offset: 0x00044EA7
		public IAsyncResult BeginSearchMailboxes(SearchMailboxesJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginSearchMailboxes(request, asyncCallback, asyncState);
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x00046CB7 File Offset: 0x00044EB7
		public IAsyncResult BeginSendItem(SendItemJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginSendItem(request, asyncCallback, asyncState);
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x00046CC7 File Offset: 0x00044EC7
		public IAsyncResult BeginSetImGroup(SetImGroupJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginSetImGroup(request, asyncCallback, asyncState);
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x00046CD7 File Offset: 0x00044ED7
		public IAsyncResult BeginSetImListMigrationCompleted(SetImListMigrationCompletedJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginSetImListMigrationCompleted(request, asyncCallback, asyncState);
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x00046CE7 File Offset: 0x00044EE7
		public IAsyncResult BeginSetHoldOnMailboxes(SetHoldOnMailboxesJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginSetHoldOnMailboxes(request, asyncCallback, asyncState);
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00046CF7 File Offset: 0x00044EF7
		public IAsyncResult BeginSetUserOofSettings(SetUserOofSettingsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginSetUserOofSettings(request, asyncCallback, asyncState);
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x00046D07 File Offset: 0x00044F07
		public IAsyncResult BeginSubscribe(SubscribeJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginSubscribe(request, asyncCallback, asyncState);
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00046D17 File Offset: 0x00044F17
		public IAsyncResult BeginSyncFolderHierarchy(SyncFolderHierarchyJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginSyncFolderHierarchy(request, asyncCallback, asyncState);
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00046D27 File Offset: 0x00044F27
		public IAsyncResult BeginSyncFolderItems(SyncFolderItemsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginSyncFolderItems(request, asyncCallback, asyncState);
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00046D37 File Offset: 0x00044F37
		public IAsyncResult BeginSyncConversation(SyncConversationJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginSyncConversation(request, asyncCallback, asyncState);
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00046D47 File Offset: 0x00044F47
		public IAsyncResult BeginUnsubscribe(UnsubscribeJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginUnsubscribe(request, asyncCallback, asyncState);
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x00046D57 File Offset: 0x00044F57
		public IAsyncResult BeginUpdateDelegate(UpdateDelegateJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginUpdateDelegate(request, asyncCallback, asyncState);
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00046D67 File Offset: 0x00044F67
		public IAsyncResult BeginUpdateFolder(UpdateFolderJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginUpdateFolder(request, asyncCallback, asyncState);
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00046D77 File Offset: 0x00044F77
		public IAsyncResult BeginUpdateInboxRules(UpdateInboxRulesJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginUpdateInboxRules(request, asyncCallback, asyncState);
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x00046D87 File Offset: 0x00044F87
		public IAsyncResult BeginUpdateItem(UpdateItemJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginUpdateItem(request, asyncCallback, asyncState);
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00046D97 File Offset: 0x00044F97
		public IAsyncResult BeginUpdateUserConfiguration(UpdateUserConfigurationJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginUpdateUserConfiguration(request, asyncCallback, asyncState);
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00046DA7 File Offset: 0x00044FA7
		public IAsyncResult BeginUploadItems(UploadItemsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginUploadItems(request, asyncCallback, asyncState);
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00046DB7 File Offset: 0x00044FB7
		public IAsyncResult BeginLogPushNotificationData(LogPushNotificationDataJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginLogPushNotificationData(request, asyncCallback, asyncState);
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00046DC7 File Offset: 0x00044FC7
		public IAsyncResult BeginGetUserUnifiedGroups(GetUserUnifiedGroupsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetUserUnifiedGroups(request, asyncCallback, asyncState);
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x00046DD7 File Offset: 0x00044FD7
		public IAsyncResult BeginGetClutterState(GetClutterStateJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetClutterState(request, asyncCallback, asyncState);
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00046DE7 File Offset: 0x00044FE7
		public IAsyncResult BeginSetClutterState(SetClutterStateJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginSetClutterState(request, asyncCallback, asyncState);
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x00046DF7 File Offset: 0x00044FF7
		public LikeItemResponse LikeItem(LikeItemRequest request)
		{
			return this.jsonService.LikeItem(request);
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x00046E05 File Offset: 0x00045005
		public GetLikersResponseMessage GetLikers(GetLikersRequest request)
		{
			return this.jsonService.GetLikers(request);
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00046E13 File Offset: 0x00045013
		public GetAggregatedAccountResponse GetAggregatedAccount(GetAggregatedAccountRequest request)
		{
			return this.jsonService.GetAggregatedAccount(request);
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00046E21 File Offset: 0x00045021
		public AddAggregatedAccountResponse AddAggregatedAccount(AddAggregatedAccountRequest request)
		{
			return this.jsonService.AddAggregatedAccount(request);
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x00046E2F File Offset: 0x0004502F
		public AddDelegateJsonResponse EndAddDelegate(IAsyncResult result)
		{
			return this.jsonService.EndAddDelegate(result);
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x00046E3D File Offset: 0x0004503D
		public AddDistributionGroupToImListJsonResponse EndAddDistributionGroupToImList(IAsyncResult result)
		{
			return this.jsonService.EndAddDistributionGroupToImList(result);
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00046E4B File Offset: 0x0004504B
		public AddImContactToGroupJsonResponse EndAddImContactToGroup(IAsyncResult result)
		{
			return this.jsonService.EndAddImContactToGroup(result);
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x00046E59 File Offset: 0x00045059
		public AddImGroupJsonResponse EndAddImGroup(IAsyncResult result)
		{
			return this.jsonService.EndAddImGroup(result);
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00046E67 File Offset: 0x00045067
		public AddNewImContactToGroupJsonResponse EndAddNewImContactToGroup(IAsyncResult result)
		{
			return this.jsonService.EndAddNewImContactToGroup(result);
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x00046E75 File Offset: 0x00045075
		public AddNewTelUriContactToGroupJsonResponse EndAddNewTelUriContactToGroup(IAsyncResult result)
		{
			return this.jsonService.EndAddNewTelUriContactToGroup(result);
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00046E83 File Offset: 0x00045083
		public ApplyConversationActionJsonResponse EndApplyConversationAction(IAsyncResult result)
		{
			return this.jsonService.EndApplyConversationAction(result);
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00046E91 File Offset: 0x00045091
		public ConvertIdJsonResponse EndConvertId(IAsyncResult result)
		{
			return this.jsonService.EndConvertId(result);
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00046E9F File Offset: 0x0004509F
		public CopyFolderJsonResponse EndCopyFolder(IAsyncResult result)
		{
			return this.jsonService.EndCopyFolder(result);
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x00046EAD File Offset: 0x000450AD
		public CopyItemJsonResponse EndCopyItem(IAsyncResult result)
		{
			return this.jsonService.EndCopyItem(result);
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00046EBB File Offset: 0x000450BB
		public CreateAttachmentJsonResponse EndCreateAttachment(IAsyncResult result)
		{
			return this.jsonService.EndCreateAttachment(result);
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00046EC9 File Offset: 0x000450C9
		public CreateFolderJsonResponse EndCreateFolder(IAsyncResult result)
		{
			return this.jsonService.EndCreateFolder(result);
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00046ED7 File Offset: 0x000450D7
		public CreateItemJsonResponse EndCreateItem(IAsyncResult result)
		{
			return this.jsonService.EndCreateItem(result);
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00046EE5 File Offset: 0x000450E5
		public PostModernGroupItemJsonResponse EndPostModernGroupItem(IAsyncResult result)
		{
			return this.jsonService.EndPostModernGroupItem(result);
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x00046EF3 File Offset: 0x000450F3
		public UpdateAndPostModernGroupItemJsonResponse EndUpdateAndPostModernGroupItem(IAsyncResult result)
		{
			return this.jsonService.EndUpdateAndPostModernGroupItem(result);
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00046F01 File Offset: 0x00045101
		public CreateResponseFromModernGroupJsonResponse EndCreateResponseFromModernGroup(IAsyncResult result)
		{
			return this.jsonService.EndCreateResponseFromModernGroup(result);
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x00046F0F File Offset: 0x0004510F
		public CreateManagedFolderJsonResponse EndCreateManagedFolder(IAsyncResult result)
		{
			return this.jsonService.EndCreateManagedFolder(result);
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00046F1D File Offset: 0x0004511D
		public CreateUserConfigurationJsonResponse EndCreateUserConfiguration(IAsyncResult result)
		{
			return this.jsonService.EndCreateUserConfiguration(result);
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00046F2B File Offset: 0x0004512B
		public DeleteAttachmentJsonResponse EndDeleteAttachment(IAsyncResult result)
		{
			return this.jsonService.EndDeleteAttachment(result);
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x00046F39 File Offset: 0x00045139
		public DeleteFolderJsonResponse EndDeleteFolder(IAsyncResult result)
		{
			return this.jsonService.EndDeleteFolder(result);
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00046F47 File Offset: 0x00045147
		public DeleteItemJsonResponse EndDeleteItem(IAsyncResult result)
		{
			return this.jsonService.EndDeleteItem(result);
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x00046F55 File Offset: 0x00045155
		public DeleteUserConfigurationJsonResponse EndDeleteUserConfiguration(IAsyncResult result)
		{
			return this.jsonService.EndDeleteUserConfiguration(result);
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x00046F63 File Offset: 0x00045163
		public DisconnectPhoneCallJsonResponse EndDisconnectPhoneCall(IAsyncResult result)
		{
			return this.jsonService.EndDisconnectPhoneCall(result);
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x00046F71 File Offset: 0x00045171
		public EmptyFolderJsonResponse EndEmptyFolder(IAsyncResult result)
		{
			return this.jsonService.EndEmptyFolder(result);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00046F7F File Offset: 0x0004517F
		public ExecuteDiagnosticMethodJsonResponse EndExecuteDiagnosticMethod(IAsyncResult result)
		{
			return this.jsonService.EndExecuteDiagnosticMethod(result);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00046F8D File Offset: 0x0004518D
		public ExpandDLJsonResponse EndExpandDL(IAsyncResult result)
		{
			return this.jsonService.EndExpandDL(result);
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x00046F9B File Offset: 0x0004519B
		public ExportItemsJsonResponse EndExportItems(IAsyncResult result)
		{
			return this.jsonService.EndExportItems(result);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x00046FA9 File Offset: 0x000451A9
		public FindConversationJsonResponse EndFindConversation(IAsyncResult result)
		{
			return this.jsonService.EndFindConversation(result);
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x00046FB7 File Offset: 0x000451B7
		public FindConversationJsonResponse EndFindTrendingConversation(IAsyncResult result)
		{
			return this.jsonService.EndFindTrendingConversation(result);
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x00046FC5 File Offset: 0x000451C5
		public FindFolderJsonResponse EndFindFolder(IAsyncResult result)
		{
			return this.jsonService.EndFindFolder(result);
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00046FD3 File Offset: 0x000451D3
		public FindItemJsonResponse EndFindItem(IAsyncResult result)
		{
			return this.jsonService.EndFindItem(result);
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x00046FE1 File Offset: 0x000451E1
		public FindMailboxStatisticsByKeywordsJsonResponse EndFindMailboxStatisticsByKeywords(IAsyncResult result)
		{
			return this.jsonService.EndFindMailboxStatisticsByKeywords(result);
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x00046FEF File Offset: 0x000451EF
		public FindMessageTrackingReportJsonResponse EndFindMessageTrackingReport(IAsyncResult result)
		{
			return this.jsonService.EndFindMessageTrackingReport(result);
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00046FFD File Offset: 0x000451FD
		public FindPeopleJsonResponse EndFindPeople(IAsyncResult result)
		{
			return this.jsonService.EndFindPeople(result);
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0004700B File Offset: 0x0004520B
		public SyncPeopleJsonResponse EndSyncPeople(IAsyncResult result)
		{
			return this.jsonService.EndSyncPeople(result);
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x00047019 File Offset: 0x00045219
		public SyncAutoCompleteRecipientsJsonResponse EndSyncAutoCompleteRecipients(IAsyncResult result)
		{
			return this.jsonService.EndSyncAutoCompleteRecipients(result);
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x00047027 File Offset: 0x00045227
		public GetAttachmentJsonResponse EndGetAttachment(IAsyncResult result)
		{
			return this.jsonService.EndGetAttachment(result);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x00047035 File Offset: 0x00045235
		public GetConversationItemsJsonResponse EndGetConversationItems(IAsyncResult result)
		{
			return this.jsonService.EndGetConversationItems(result);
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x00047043 File Offset: 0x00045243
		public GetThreadedConversationItemsJsonResponse EndGetThreadedConversationItems(IAsyncResult result)
		{
			return this.jsonService.EndGetThreadedConversationItems(result);
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00047051 File Offset: 0x00045251
		public GetConversationItemsDiagnosticsJsonResponse EndGetConversationItemsDiagnostics(IAsyncResult result)
		{
			return this.jsonService.EndGetConversationItemsDiagnostics(result);
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0004705F File Offset: 0x0004525F
		public GetModernConversationAttachmentsJsonResponse EndGetModernConversationAttachments(IAsyncResult result)
		{
			return this.jsonService.EndGetModernConversationAttachments(result);
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0004706D File Offset: 0x0004526D
		public GetDelegateJsonResponse EndGetDelegate(IAsyncResult result)
		{
			return this.jsonService.EndGetDelegate(result);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0004707B File Offset: 0x0004527B
		public GetEventsJsonResponse EndGetEvents(IAsyncResult result)
		{
			return this.jsonService.EndGetEvents(result);
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00047089 File Offset: 0x00045289
		public GetFolderJsonResponse EndGetFolder(IAsyncResult result)
		{
			return this.jsonService.EndGetFolder(result);
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x00047097 File Offset: 0x00045297
		public GetHoldOnMailboxesJsonResponse EndGetHoldOnMailboxes(IAsyncResult result)
		{
			return this.jsonService.EndGetHoldOnMailboxes(result);
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x000470A5 File Offset: 0x000452A5
		public GetImItemListJsonResponse EndGetImItemList(IAsyncResult result)
		{
			return this.jsonService.EndGetImItemList(result);
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x000470B3 File Offset: 0x000452B3
		public GetImItemsJsonResponse EndGetImItems(IAsyncResult result)
		{
			return this.jsonService.EndGetImItems(result);
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x000470C1 File Offset: 0x000452C1
		public GetInboxRulesJsonResponse EndGetInboxRules(IAsyncResult result)
		{
			return this.jsonService.EndGetInboxRules(result);
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x000470CF File Offset: 0x000452CF
		public GetClientAccessTokenJsonResponse EndGetClientAccessToken(IAsyncResult result)
		{
			return this.jsonService.EndGetClientAccessToken(result);
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x000470DD File Offset: 0x000452DD
		public GetItemJsonResponse EndGetItem(IAsyncResult result)
		{
			return this.jsonService.EndGetItem(result);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x000470EB File Offset: 0x000452EB
		public GetMailTipsJsonResponse EndGetMailTips(IAsyncResult result)
		{
			return this.jsonService.EndGetMailTips(result);
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x000470F9 File Offset: 0x000452F9
		public GetMessageTrackingReportJsonResponse EndGetMessageTrackingReport(IAsyncResult result)
		{
			return this.jsonService.EndGetMessageTrackingReport(result);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x00047107 File Offset: 0x00045307
		public GetPasswordExpirationDateJsonResponse EndGetPasswordExpirationDate(IAsyncResult result)
		{
			return this.jsonService.EndGetPasswordExpirationDate(result);
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x00047115 File Offset: 0x00045315
		public GetPersonaJsonResponse EndGetPersona(IAsyncResult result)
		{
			return this.jsonService.EndGetPersona(result);
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x00047123 File Offset: 0x00045323
		public GetPhoneCallInformationJsonResponse EndGetPhoneCallInformation(IAsyncResult result)
		{
			return this.jsonService.EndGetPhoneCallInformation(result);
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00047131 File Offset: 0x00045331
		public GetRemindersJsonResponse EndGetReminders(IAsyncResult result)
		{
			return this.jsonService.EndGetReminders(result);
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x0004713F File Offset: 0x0004533F
		public PerformReminderActionJsonResponse EndPerformReminderAction(IAsyncResult result)
		{
			return this.jsonService.EndPerformReminderAction(result);
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0004714D File Offset: 0x0004534D
		public GetRoomListsJsonResponse EndGetRoomLists(IAsyncResult result)
		{
			return this.jsonService.EndGetRoomLists(result);
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0004715B File Offset: 0x0004535B
		public GetRoomsJsonResponse EndGetRooms(IAsyncResult result)
		{
			return this.jsonService.EndGetRooms(result);
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00047169 File Offset: 0x00045369
		public GetSearchableMailboxesJsonResponse EndGetSearchableMailboxes(IAsyncResult result)
		{
			return this.jsonService.EndGetSearchableMailboxes(result);
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00047177 File Offset: 0x00045377
		public GetServerTimeZonesJsonResponse EndGetServerTimeZones(IAsyncResult result)
		{
			return this.jsonService.EndGetServerTimeZones(result);
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x00047185 File Offset: 0x00045385
		public GetServiceConfigurationJsonResponse EndGetServiceConfiguration(IAsyncResult result)
		{
			return this.jsonService.EndGetServiceConfiguration(result);
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00047193 File Offset: 0x00045393
		public GetSharingFolderJsonResponse EndGetSharingFolder(IAsyncResult result)
		{
			return this.jsonService.EndGetSharingFolder(result);
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x000471A1 File Offset: 0x000453A1
		public GetSharingMetadataJsonResponse EndGetSharingMetadata(IAsyncResult result)
		{
			return this.jsonService.EndGetSharingMetadata(result);
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x000471AF File Offset: 0x000453AF
		public GetUserAvailabilityJsonResponse EndGetUserAvailability(IAsyncResult result)
		{
			return this.jsonService.EndGetUserAvailability(result);
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x000471BD File Offset: 0x000453BD
		public GetUserConfigurationJsonResponse EndGetUserConfiguration(IAsyncResult result)
		{
			return this.jsonService.EndGetUserConfiguration(result);
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x000471CB File Offset: 0x000453CB
		public GetUserOofSettingsJsonResponse EndGetUserOofSettings(IAsyncResult result)
		{
			return this.jsonService.EndGetUserOofSettings(result);
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x000471D9 File Offset: 0x000453D9
		public GetUserRetentionPolicyTagsJsonResponse EndGetUserRetentionPolicyTags(IAsyncResult result)
		{
			return this.jsonService.EndGetUserRetentionPolicyTags(result);
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x000471E7 File Offset: 0x000453E7
		public MarkAllItemsAsReadJsonResponse EndMarkAllItemsAsRead(IAsyncResult result)
		{
			return this.jsonService.EndMarkAllItemsAsRead(result);
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x000471F5 File Offset: 0x000453F5
		public MarkAsJunkJsonResponse EndMarkAsJunk(IAsyncResult result)
		{
			return this.jsonService.EndMarkAsJunk(result);
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00047203 File Offset: 0x00045403
		public MoveFolderJsonResponse EndMoveFolder(IAsyncResult result)
		{
			return this.jsonService.EndMoveFolder(result);
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00047211 File Offset: 0x00045411
		public MoveItemJsonResponse EndMoveItem(IAsyncResult result)
		{
			return this.jsonService.EndMoveItem(result);
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0004721F File Offset: 0x0004541F
		public PlayOnPhoneJsonResponse EndPlayOnPhone(IAsyncResult result)
		{
			return this.jsonService.EndPlayOnPhone(result);
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0004722D File Offset: 0x0004542D
		public ProvisionJsonResponse EndProvision(IAsyncResult result)
		{
			return this.jsonService.EndProvision(result);
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0004723B File Offset: 0x0004543B
		public DeprovisionJsonResponse EndDeprovision(IAsyncResult result)
		{
			return this.jsonService.EndDeprovision(result);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00047249 File Offset: 0x00045449
		public RefreshSharingFolderJsonResponse EndRefreshSharingFolder(IAsyncResult result)
		{
			return this.jsonService.EndRefreshSharingFolder(result);
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00047257 File Offset: 0x00045457
		public RemoveContactFromImListJsonResponse EndRemoveContactFromImList(IAsyncResult result)
		{
			return this.jsonService.EndRemoveContactFromImList(result);
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00047265 File Offset: 0x00045465
		public RemoveDelegateJsonResponse EndRemoveDelegate(IAsyncResult result)
		{
			return this.jsonService.EndRemoveDelegate(result);
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00047273 File Offset: 0x00045473
		public RemoveDistributionGroupFromImListJsonResponse EndRemoveDistributionGroupFromImList(IAsyncResult result)
		{
			return this.jsonService.EndRemoveDistributionGroupFromImList(result);
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00047281 File Offset: 0x00045481
		public RemoveImContactFromGroupJsonResponse EndRemoveImContactFromGroup(IAsyncResult result)
		{
			return this.jsonService.EndRemoveImContactFromGroup(result);
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0004728F File Offset: 0x0004548F
		public RemoveImGroupJsonResponse EndRemoveImGroup(IAsyncResult result)
		{
			return this.jsonService.EndRemoveImGroup(result);
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x0004729D File Offset: 0x0004549D
		public ResolveNamesJsonResponse EndResolveNames(IAsyncResult result)
		{
			return this.jsonService.EndResolveNames(result);
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x000472AB File Offset: 0x000454AB
		public SearchMailboxesJsonResponse EndSearchMailboxes(IAsyncResult result)
		{
			return this.jsonService.EndSearchMailboxes(result);
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x000472B9 File Offset: 0x000454B9
		public SendItemJsonResponse EndSendItem(IAsyncResult result)
		{
			return this.jsonService.EndSendItem(result);
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x000472C7 File Offset: 0x000454C7
		public SetHoldOnMailboxesJsonResponse EndSetHoldOnMailboxes(IAsyncResult result)
		{
			return this.jsonService.EndSetHoldOnMailboxes(result);
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x000472D5 File Offset: 0x000454D5
		public SetImGroupJsonResponse EndSetImGroup(IAsyncResult result)
		{
			return this.jsonService.EndSetImGroup(result);
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x000472E3 File Offset: 0x000454E3
		public SetImListMigrationCompletedJsonResponse EndSetImListMigrationCompleted(IAsyncResult result)
		{
			return this.jsonService.EndSetImListMigrationCompleted(result);
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x000472F1 File Offset: 0x000454F1
		public SetUserOofSettingsJsonResponse EndSetUserOofSettings(IAsyncResult result)
		{
			return this.jsonService.EndSetUserOofSettings(result);
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x000472FF File Offset: 0x000454FF
		public SubscribeJsonResponse EndSubscribe(IAsyncResult result)
		{
			return this.jsonService.EndSubscribe(result);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0004730D File Offset: 0x0004550D
		public SyncFolderHierarchyJsonResponse EndSyncFolderHierarchy(IAsyncResult result)
		{
			return this.jsonService.EndSyncFolderHierarchy(result);
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0004731B File Offset: 0x0004551B
		public SyncFolderItemsJsonResponse EndSyncFolderItems(IAsyncResult result)
		{
			return this.jsonService.EndSyncFolderItems(result);
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00047329 File Offset: 0x00045529
		public SyncConversationJsonResponse EndSyncConversation(IAsyncResult result)
		{
			return this.jsonService.EndSyncConversation(result);
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x00047337 File Offset: 0x00045537
		public UnsubscribeJsonResponse EndUnsubscribe(IAsyncResult result)
		{
			return this.jsonService.EndUnsubscribe(result);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x00047345 File Offset: 0x00045545
		public UpdateDelegateJsonResponse EndUpdateDelegate(IAsyncResult result)
		{
			return this.jsonService.EndUpdateDelegate(result);
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x00047353 File Offset: 0x00045553
		public UpdateFolderJsonResponse EndUpdateFolder(IAsyncResult result)
		{
			return this.jsonService.EndUpdateFolder(result);
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x00047361 File Offset: 0x00045561
		public UpdateInboxRulesJsonResponse EndUpdateInboxRules(IAsyncResult result)
		{
			return this.jsonService.EndUpdateInboxRules(result);
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0004736F File Offset: 0x0004556F
		public UpdateItemJsonResponse EndUpdateItem(IAsyncResult result)
		{
			return this.jsonService.EndUpdateItem(result);
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0004737D File Offset: 0x0004557D
		public UpdateUserConfigurationJsonResponse EndUpdateUserConfiguration(IAsyncResult result)
		{
			return this.jsonService.EndUpdateUserConfiguration(result);
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0004738B File Offset: 0x0004558B
		public UploadItemsJsonResponse EndUploadItems(IAsyncResult result)
		{
			return this.jsonService.EndUploadItems(result);
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00047399 File Offset: 0x00045599
		public LogPushNotificationDataJsonResponse EndLogPushNotificationData(IAsyncResult result)
		{
			return this.jsonService.EndLogPushNotificationData(result);
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x000473A7 File Offset: 0x000455A7
		public GetUserUnifiedGroupsJsonResponse EndGetUserUnifiedGroups(IAsyncResult result)
		{
			return this.jsonService.EndGetUserUnifiedGroups(result);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x000473B5 File Offset: 0x000455B5
		public GetClutterStateJsonResponse EndGetClutterState(IAsyncResult result)
		{
			return this.jsonService.EndGetClutterState(result);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x000473C3 File Offset: 0x000455C3
		public SetClutterStateJsonResponse EndSetClutterState(IAsyncResult result)
		{
			return this.jsonService.EndSetClutterState(result);
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x000473D1 File Offset: 0x000455D1
		public IAsyncResult BeginGetUserPhoto(string email, UserPhotoSize size, bool isPreview, bool fallbackToClearImage, AsyncCallback callback, object state)
		{
			return this.jsonService.BeginGetUserPhoto(email, size, isPreview, fallbackToClearImage, callback, state);
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x000473E7 File Offset: 0x000455E7
		public Stream EndGetUserPhoto(IAsyncResult result)
		{
			return this.jsonService.EndGetUserPhoto(result);
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x000473F5 File Offset: 0x000455F5
		public IAsyncResult BeginGetPeopleICommunicateWith(AsyncCallback callback, object state)
		{
			return this.jsonService.BeginGetPeopleICommunicateWith(callback, state);
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x00047404 File Offset: 0x00045604
		public Stream EndGetPeopleICommunicateWith(IAsyncResult result)
		{
			return this.jsonService.EndGetPeopleICommunicateWith(result);
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x00047412 File Offset: 0x00045612
		public IAsyncResult BeginGetTimeZoneOffsets(GetTimeZoneOffsetsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetTimeZoneOffsets(request, asyncCallback, asyncState);
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x00047422 File Offset: 0x00045622
		public GetTimeZoneOffsetsJsonResponse EndGetTimeZoneOffsets(IAsyncResult result)
		{
			return this.jsonService.EndGetTimeZoneOffsets(result);
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x00047430 File Offset: 0x00045630
		public ContactFolderResponse CreateContactFolder(BaseFolderId parentFolderId, string displayName, int priority)
		{
			return new CreateContactFolder(CallContext.Current, parentFolderId, displayName, priority).Execute();
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x00047444 File Offset: 0x00045644
		public bool DeleteContactFolder(FolderId folderId)
		{
			return new DeleteContactFolder(CallContext.Current, folderId).Execute();
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x00047456 File Offset: 0x00045656
		public ContactFolderResponse MoveContactFolder(FolderId folderId, int priority)
		{
			return new MoveContactFolder(CallContext.Current, folderId, priority).Execute();
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x00047469 File Offset: 0x00045669
		public bool SetLayoutSettings(LayoutSettingsType layoutSettings)
		{
			return new SetLayoutSettings(CallContext.Current, layoutSettings).Execute();
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0004747C File Offset: 0x0004567C
		public InlineExploreSpResultListType GetInlineExploreSpContent(string query, string targetUrl)
		{
			UserContext userContext = OWAService.GetUserContext();
			if (!userContext.FeaturesManager.ServerSettings.InlineExploreUI.Enabled)
			{
				throw new OwaInvalidRequestException("Service not enabled");
			}
			return new GetInlineExploreSpContentCommand(CallContext.Current, query, targetUrl).Execute();
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x000474C5 File Offset: 0x000456C5
		public SuiteStorageJsonResponse ProcessSuiteStorage(SuiteStorageJsonRequest request)
		{
			return this.jsonService.ProcessSuiteStorage(request);
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x000474D3 File Offset: 0x000456D3
		public IAsyncResult BeginGetWeatherForecast(GetWeatherForecastJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetWeatherForecast(request, asyncCallback, asyncState);
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x000474E3 File Offset: 0x000456E3
		public GetWeatherForecastJsonResponse EndGetWeatherForecast(IAsyncResult result)
		{
			return this.jsonService.EndGetWeatherForecast(result);
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x000474F1 File Offset: 0x000456F1
		public IAsyncResult BeginFindWeatherLocations(FindWeatherLocationsJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginFindWeatherLocations(request, asyncCallback, asyncState);
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00047501 File Offset: 0x00045701
		public FindWeatherLocationsJsonResponse EndFindWeatherLocations(IAsyncResult result)
		{
			return this.jsonService.EndFindWeatherLocations(result);
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x000475EC File Offset: 0x000457EC
		public async Task<GetLinkPreviewResponse> GetLinkPreview(GetLinkPreviewRequest getLinkPreviewRequest)
		{
			return await new GetLinkPreview(CallContext.Current, getLinkPreviewRequest).Execute();
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0004763A File Offset: 0x0004583A
		public GetBingMapsPreviewResponse GetBingMapsPreview(GetBingMapsPreviewRequest getBingMapsPreviewRequest)
		{
			return new GetBingMapsPreview(CallContext.Current, getBingMapsPreviewRequest).Execute();
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0004764C File Offset: 0x0004584C
		public PerformInstantSearchResponse PerformInstantSearch(PerformInstantSearchRequest request)
		{
			UserContext userContext = OWAService.GetUserContext();
			CallContext.Current.HttpContext.Response.Buffer = false;
			PerformInstantSearch performInstantSearch = new PerformInstantSearch(CallContext.Current, userContext.InstantSearchManager, userContext.InstantSearchNotificationHandler, request);
			OWAService.PreExecute("PerformInstantSearch");
			PerformInstantSearchResponse result;
			try
			{
				PerformInstantSearchResponse value = performInstantSearch.Execute().Value;
				result = value;
			}
			finally
			{
				OWAService.PostExecute();
			}
			return result;
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x000476C0 File Offset: 0x000458C0
		public EnableAppDataResponse EnableApp(EnableAppDataRequest request)
		{
			return this.jsonService.EnableApp(request);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x000476CE File Offset: 0x000458CE
		public DisableAppDataResponse DisableApp(DisableAppDataRequest request)
		{
			return this.jsonService.DisableApp(request);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x000476DC File Offset: 0x000458DC
		public RemoveAppDataResponse RemoveApp(RemoveAppDataRequest request)
		{
			return this.jsonService.RemoveApp(request);
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x000476EA File Offset: 0x000458EA
		public GetCalendarNotificationResponse GetCalendarNotification()
		{
			return this.jsonService.GetCalendarNotification();
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x000476F7 File Offset: 0x000458F7
		public OptionsResponseBase SetCalendarNotification(SetCalendarNotificationRequest request)
		{
			return this.jsonService.SetCalendarNotification(request);
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00047705 File Offset: 0x00045905
		public GetCalendarProcessingResponse GetCalendarProcessing()
		{
			return this.jsonService.GetCalendarProcessing();
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x00047712 File Offset: 0x00045912
		public OptionsResponseBase SetCalendarProcessing(SetCalendarProcessingRequest request)
		{
			return this.jsonService.SetCalendarProcessing(request);
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x00047720 File Offset: 0x00045920
		public GetCASMailboxResponse GetCASMailbox()
		{
			return this.jsonService.GetCASMailbox();
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0004772D File Offset: 0x0004592D
		public GetCASMailboxResponse GetCASMailbox2(GetCASMailboxRequest request)
		{
			return this.jsonService.GetCASMailbox2(request);
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x0004773B File Offset: 0x0004593B
		public SetCASMailboxResponse SetCASMailbox(SetCASMailboxRequest request)
		{
			return this.jsonService.SetCASMailbox(request);
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x00047749 File Offset: 0x00045949
		public GetConnectedAccountsResponse GetConnectedAccounts(GetConnectedAccountsRequest request)
		{
			return this.jsonService.GetConnectedAccounts(request);
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00047757 File Offset: 0x00045957
		public GetConnectSubscriptionResponse GetConnectSubscription(GetConnectSubscriptionRequest request)
		{
			return this.jsonService.GetConnectSubscription(request);
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00047765 File Offset: 0x00045965
		public NewConnectSubscriptionResponse NewConnectSubscription(NewConnectSubscriptionRequest request)
		{
			return this.jsonService.NewConnectSubscription(request);
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00047773 File Offset: 0x00045973
		public SetConnectSubscriptionResponse SetConnectSubscription(SetConnectSubscriptionRequest request)
		{
			return this.jsonService.SetConnectSubscription(request);
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00047781 File Offset: 0x00045981
		public RemoveConnectSubscriptionResponse RemoveConnectSubscription(RemoveConnectSubscriptionRequest request)
		{
			return this.jsonService.RemoveConnectSubscription(request);
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0004778F File Offset: 0x0004598F
		public GetHotmailSubscriptionResponse GetHotmailSubscription(IdentityRequest request)
		{
			return this.jsonService.GetHotmailSubscription(request);
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0004779D File Offset: 0x0004599D
		public OptionsResponseBase SetHotmailSubscription(SetHotmailSubscriptionRequest request)
		{
			return this.jsonService.SetHotmailSubscription(request);
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x000477AB File Offset: 0x000459AB
		public GetImapSubscriptionResponse GetImapSubscription(IdentityRequest request)
		{
			return this.jsonService.GetImapSubscription(request);
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x000477B9 File Offset: 0x000459B9
		public NewImapSubscriptionResponse NewImapSubscription(NewImapSubscriptionRequest request)
		{
			return this.jsonService.NewImapSubscription(request);
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x000477C7 File Offset: 0x000459C7
		public OptionsResponseBase SetImapSubscription(SetImapSubscriptionRequest request)
		{
			return this.jsonService.SetImapSubscription(request);
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x000477D5 File Offset: 0x000459D5
		public ImportContactListResponse ImportContactList(ImportContactListRequest request)
		{
			return this.jsonService.ImportContactList(request);
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x000477E3 File Offset: 0x000459E3
		public DisableInboxRuleResponse DisableInboxRule(DisableInboxRuleRequest request)
		{
			return this.jsonService.DisableInboxRule(request);
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x000477F1 File Offset: 0x000459F1
		public EnableInboxRuleResponse EnableInboxRule(EnableInboxRuleRequest request)
		{
			return this.jsonService.EnableInboxRule(request);
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x000477FF File Offset: 0x000459FF
		public GetInboxRuleResponse GetInboxRule(GetInboxRuleRequest request)
		{
			return this.jsonService.GetInboxRule(request);
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0004780D File Offset: 0x00045A0D
		public NewInboxRuleResponse NewInboxRule(NewInboxRuleRequest request)
		{
			return this.jsonService.NewInboxRule(request);
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0004781B File Offset: 0x00045A1B
		public RemoveInboxRuleResponse RemoveInboxRule(RemoveInboxRuleRequest request)
		{
			return this.jsonService.RemoveInboxRule(request);
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x00047829 File Offset: 0x00045A29
		public SetInboxRuleResponse SetInboxRule(SetInboxRuleRequest request)
		{
			return this.jsonService.SetInboxRule(request);
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x00047837 File Offset: 0x00045A37
		public GetMailboxResponse GetMailboxByIdentity(IdentityRequest request)
		{
			return this.jsonService.GetMailboxByIdentity(request);
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x00047845 File Offset: 0x00045A45
		public OptionsResponseBase SetMailbox(SetMailboxRequest request)
		{
			return this.jsonService.SetMailbox(request);
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x00047853 File Offset: 0x00045A53
		public GetMailboxAutoReplyConfigurationResponse GetMailboxAutoReplyConfiguration()
		{
			return this.jsonService.GetMailboxAutoReplyConfiguration();
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x00047860 File Offset: 0x00045A60
		public OptionsResponseBase SetMailboxAutoReplyConfiguration(SetMailboxAutoReplyConfigurationRequest request)
		{
			return this.jsonService.SetMailboxAutoReplyConfiguration(request);
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0004786E File Offset: 0x00045A6E
		public GetMailboxCalendarConfigurationResponse GetMailboxCalendarConfiguration()
		{
			return this.jsonService.GetMailboxCalendarConfiguration();
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0004787B File Offset: 0x00045A7B
		public OptionsResponseBase SetMailboxCalendarConfiguration(SetMailboxCalendarConfigurationRequest request)
		{
			return this.jsonService.SetMailboxCalendarConfiguration(request);
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x00047889 File Offset: 0x00045A89
		public GetMailboxJunkEmailConfigurationResponse GetMailboxJunkEmailConfiguration()
		{
			return this.jsonService.GetMailboxJunkEmailConfiguration();
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x00047896 File Offset: 0x00045A96
		public OptionsResponseBase SetMailboxJunkEmailConfiguration(SetMailboxJunkEmailConfigurationRequest request)
		{
			return this.jsonService.SetMailboxJunkEmailConfiguration(request);
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x000478A4 File Offset: 0x00045AA4
		public GetMailboxMessageConfigurationResponse GetMailboxMessageConfiguration()
		{
			return this.jsonService.GetMailboxMessageConfiguration();
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x000478B1 File Offset: 0x00045AB1
		public OptionsResponseBase SetMailboxMessageConfiguration(SetMailboxMessageConfigurationRequest request)
		{
			return this.jsonService.SetMailboxMessageConfiguration(request);
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x000478BF File Offset: 0x00045ABF
		public GetMailboxRegionalConfigurationResponse GetMailboxRegionalConfiguration(GetMailboxRegionalConfigurationRequest request)
		{
			return this.jsonService.GetMailboxRegionalConfiguration(request);
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x000478CD File Offset: 0x00045ACD
		public SetMailboxRegionalConfigurationResponse SetMailboxRegionalConfiguration(SetMailboxRegionalConfigurationRequest request)
		{
			return this.jsonService.SetMailboxRegionalConfiguration(request);
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x000478DB File Offset: 0x00045ADB
		public GetMessageCategoryResponse GetMessageCategory()
		{
			return this.jsonService.GetMessageCategory();
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x000478E8 File Offset: 0x00045AE8
		public GetMessageClassificationResponse GetMessageClassification()
		{
			return this.jsonService.GetMessageClassification();
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x000478F5 File Offset: 0x00045AF5
		public GetAccountInformationResponse GetAccountInformation(GetAccountInformationRequest request)
		{
			return this.jsonService.GetAccountInformation(request);
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x00047903 File Offset: 0x00045B03
		public GetSocialNetworksOAuthInfoResponse GetConnectToSocialNetworksOAuthInfo(GetSocialNetworksOAuthInfoRequest request)
		{
			return this.jsonService.GetConnectToSocialNetworksOAuthInfo(request);
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00047911 File Offset: 0x00045B11
		public GetPopSubscriptionResponse GetPopSubscription(IdentityRequest request)
		{
			return this.jsonService.GetPopSubscription(request);
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0004791F File Offset: 0x00045B1F
		public NewPopSubscriptionResponse NewPopSubscription(NewPopSubscriptionRequest request)
		{
			return this.jsonService.NewPopSubscription(request);
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x0004792D File Offset: 0x00045B2D
		public OptionsResponseBase SetPopSubscription(SetPopSubscriptionRequest request)
		{
			return this.jsonService.SetPopSubscription(request);
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0004793B File Offset: 0x00045B3B
		public OptionsResponseBase AddActiveRetentionPolicyTags(IdentityCollectionRequest request)
		{
			return this.jsonService.AddActiveRetentionPolicyTags(request);
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00047949 File Offset: 0x00045B49
		public GetRetentionPolicyTagsResponse GetActiveRetentionPolicyTags(GetRetentionPolicyTagsRequest request)
		{
			return this.jsonService.GetActiveRetentionPolicyTags(request);
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00047957 File Offset: 0x00045B57
		public GetRetentionPolicyTagsResponse GetAvailableRetentionPolicyTags(GetRetentionPolicyTagsRequest request)
		{
			return this.jsonService.GetAvailableRetentionPolicyTags(request);
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x00047965 File Offset: 0x00045B65
		public OptionsResponseBase RemoveActiveRetentionPolicyTags(IdentityCollectionRequest request)
		{
			return this.jsonService.RemoveActiveRetentionPolicyTags(request);
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x00047973 File Offset: 0x00045B73
		public GetSendAddressResponse GetSendAddress()
		{
			return this.jsonService.GetSendAddress();
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00047980 File Offset: 0x00045B80
		public GetSubscriptionResponse GetSubscription()
		{
			return this.jsonService.GetSubscription();
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0004798D File Offset: 0x00045B8D
		public NewSubscriptionResponse NewSubscription(NewSubscriptionRequest request)
		{
			return this.jsonService.NewSubscription(request);
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0004799B File Offset: 0x00045B9B
		public OptionsResponseBase RemoveSubscription(IdentityRequest request)
		{
			return this.jsonService.RemoveSubscription(request);
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x000479A9 File Offset: 0x00045BA9
		public SetUserResponse SetUser(SetUserRequest request)
		{
			return this.jsonService.SetUser(request);
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x000479B8 File Offset: 0x00045BB8
		public EndInstantSearchSessionResponse EndInstantSearchSession(string sessionId)
		{
			UserContext userContext = OWAService.GetUserContext();
			EndInstantSearchSessionRequest endInstantSearchSessionRequest = new EndInstantSearchSessionRequest();
			endInstantSearchSessionRequest.SessionId = sessionId;
			EndInstantSearchSession endInstantSearchSession = new EndInstantSearchSession(CallContext.Current, endInstantSearchSessionRequest, userContext.InstantSearchManager);
			return endInstantSearchSession.Execute().Value;
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x000479F5 File Offset: 0x00045BF5
		public GetMobileDeviceStatisticsResponse GetMobileDeviceStatistics(GetMobileDeviceStatisticsRequest request)
		{
			return this.jsonService.GetMobileDeviceStatistics(request);
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x00047A03 File Offset: 0x00045C03
		public RemoveMobileDeviceResponse RemoveMobileDevice(RemoveMobileDeviceRequest request)
		{
			return this.jsonService.RemoveMobileDevice(request);
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x00047A11 File Offset: 0x00045C11
		public ClearMobileDeviceResponse ClearMobileDevice(ClearMobileDeviceRequest request)
		{
			return this.jsonService.ClearMobileDevice(request);
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00047A1F File Offset: 0x00045C1F
		public ClearTextMessagingAccountResponse ClearTextMessagingAccount(ClearTextMessagingAccountRequest request)
		{
			return this.jsonService.ClearTextMessagingAccount(request);
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x00047A2D File Offset: 0x00045C2D
		public GetTextMessagingAccountResponse GetTextMessagingAccount(GetTextMessagingAccountRequest request)
		{
			return this.jsonService.GetTextMessagingAccount(request);
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x00047A3B File Offset: 0x00045C3B
		public SetTextMessagingAccountResponse SetTextMessagingAccount(SetTextMessagingAccountRequest request)
		{
			return this.jsonService.SetTextMessagingAccount(request);
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x00047A49 File Offset: 0x00045C49
		public CompareTextMessagingVerificationCodeResponse CompareTextMessagingVerificationCode(CompareTextMessagingVerificationCodeRequest request)
		{
			return this.jsonService.CompareTextMessagingVerificationCode(request);
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x00047A57 File Offset: 0x00045C57
		public SendTextMessagingVerificationCodeResponse SendTextMessagingVerificationCode(SendTextMessagingVerificationCodeRequest request)
		{
			return this.jsonService.SendTextMessagingVerificationCode(request);
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x00047A65 File Offset: 0x00045C65
		private static UserContext GetUserContext()
		{
			return UserContextManager.GetUserContext(CallContext.Current.HttpContext, CallContext.Current.EffectiveCaller, true);
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x00047A84 File Offset: 0x00045C84
		private static bool GetGroupCreationEnabledFromOwaMailboxPolicy()
		{
			PolicyConfiguration policyConfiguration = OwaMailboxPolicyCache.GetPolicyConfiguration(CallContext.Current.AccessingADUser.OwaMailboxPolicy, CallContext.Current.AccessingADUser.OrganizationId);
			if (policyConfiguration != null)
			{
				return policyConfiguration.GroupCreationEnabled;
			}
			if (ExEnvironment.IsTest && !VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
			{
				return true;
			}
			throw new OwaInvalidOperationException("Invalid owa mailbox policy returned.");
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x00047AEC File Offset: 0x00045CEC
		private void RegisterOwaCallback(bool isSearchCall)
		{
			if (isSearchCall)
			{
				IUserContext userContext = OWAService.GetUserContext();
				if (userContext != null && userContext.NotificationManager != null)
				{
					userContext.NotificationManager.SubscribeToSearchNotification();
					CallContext.Current.OwaCallback = userContext.NotificationManager.SearchNotificationHandler;
					return;
				}
			}
			if (CallContext.Current != null)
			{
				CallContext.Current.OwaCallback = NoOpOwaCallback.Prototype;
			}
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00047B44 File Offset: 0x00045D44
		public bool SendLinkClickedSignalToSP(SendLinkClickedSignalToSPRequest sendLinkClickedRequest)
		{
			OWAService.GetUserContext();
			return new SendLinkClickedSignalToSP(CallContext.Current, sendLinkClickedRequest).Execute();
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x00047B5C File Offset: 0x00045D5C
		public ValidateAggregatedConfigurationResponse ValidateAggregatedConfiguration(ValidateAggregatedConfigurationRequest request)
		{
			return new ValidateAggregatedConfiguration(CallContext.Current).Execute();
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x00047B6D File Offset: 0x00045D6D
		public IAsyncResult BeginCancelCalendarEvent(CancelCalendarEventJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginCancelCalendarEvent(request, asyncCallback, asyncState);
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x00047B7D File Offset: 0x00045D7D
		public CancelCalendarEventJsonResponse EndCancelCalendarEvent(IAsyncResult result)
		{
			return this.jsonService.EndCancelCalendarEvent(result);
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x00047B8B File Offset: 0x00045D8B
		public CalendarActionFolderIdResponse EnableBirthdayCalendar()
		{
			return new EnableBirthdayCalendarCommand(CallContext.Current).Execute();
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x00047B9C File Offset: 0x00045D9C
		public CalendarActionResponse DisableBirthdayCalendar()
		{
			return new DisableBirthdayCalendarCommand(CallContext.Current).Execute();
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x00047BAD File Offset: 0x00045DAD
		public CalendarActionResponse RemoveBirthdayEvent(Microsoft.Exchange.Services.Core.Types.ItemId contactId)
		{
			return new RemoveBirthdayEventCommand(CallContext.Current, contactId).Execute();
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x00047BBF File Offset: 0x00045DBF
		public IAsyncResult BeginGetBirthdayCalendarView(GetBirthdayCalendarViewJsonRequest request, AsyncCallback asyncCallback, object asyncState)
		{
			return this.jsonService.BeginGetBirthdayCalendarView(request, asyncCallback, asyncState);
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x00047BCF File Offset: 0x00045DCF
		public GetBirthdayCalendarViewJsonResponse EndGetBirthdayCalendarView(IAsyncResult result)
		{
			return this.jsonService.EndGetBirthdayCalendarView(result);
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x00047BDD File Offset: 0x00045DDD
		public GetAllowedOptionsResponse GetAllowedOptions(GetAllowedOptionsRequest request)
		{
			return this.jsonService.GetAllowedOptions(request);
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x00047BEC File Offset: 0x00045DEC
		private static void PreExecute(string action)
		{
			RequestDetailsLogger protocolLog = CallContext.Current.ProtocolLog;
			if (protocolLog != null)
			{
				IActivityScope activityScope = protocolLog.ActivityScope;
				if (activityScope != null)
				{
					if (string.IsNullOrEmpty(activityScope.Component))
					{
						activityScope.Component = OWAService.component;
					}
					if (string.IsNullOrEmpty(activityScope.Action))
					{
						activityScope.Action = action;
					}
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(protocolLog, ServiceLatencyMetadata.PreExecutionLatency, activityScope.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
				}
			}
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x00047C5C File Offset: 0x00045E5C
		private static void PostExecute()
		{
			RequestDetailsLogger protocolLog = CallContext.Current.ProtocolLog;
			if (protocolLog != null)
			{
				IActivityScope activityScope = protocolLog.ActivityScope;
				if (activityScope != null)
				{
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(protocolLog, ServiceLatencyMetadata.CoreExecutionLatency, activityScope.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
				}
			}
		}

		// Token: 0x04000AA8 RID: 2728
		private static readonly string component = WorkloadType.Owa.ToString();

		// Token: 0x04000AA9 RID: 2729
		private readonly JsonService jsonService;

		// Token: 0x04000AAA RID: 2730
		private readonly Action<string, Type> registerType = delegate(string s, Type type)
		{
			OwsLogRegistry.Register(s, type, new Type[0]);
		};
	}
}
