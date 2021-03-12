using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000851 RID: 2129
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ItemCreateInfo
	{
		// Token: 0x06004F41 RID: 20289 RVA: 0x0014C107 File Offset: 0x0014A307
		private ItemCreateInfo(StoreObjectType type, Schema schema, AcrProfile acrProfile, ItemCreateInfo.ItemCreator creator)
		{
			this.Type = type;
			this.Schema = schema;
			this.AcrProfile = acrProfile;
			this.Creator = creator;
		}

		// Token: 0x06004F42 RID: 20290 RVA: 0x0014C12C File Offset: 0x0014A32C
		private static CalendarItemOccurrence CalendarItemOccurrenceCreator(ICoreItem coreItem)
		{
			return new CalendarItemOccurrence(coreItem);
		}

		// Token: 0x06004F43 RID: 20291 RVA: 0x0014C134 File Offset: 0x0014A334
		private static ReportMessage ReportMessageCreator(ICoreItem coreItem)
		{
			return new ReportMessage(coreItem);
		}

		// Token: 0x06004F44 RID: 20292 RVA: 0x0014C13C File Offset: 0x0014A33C
		private static MessageItem MessageItemCreator(ICoreItem coreItem)
		{
			return new MessageItem(coreItem, false);
		}

		// Token: 0x06004F45 RID: 20293 RVA: 0x0014C145 File Offset: 0x0014A345
		private static PostItem PostItemCreator(ICoreItem coreItem)
		{
			return new PostItem(coreItem);
		}

		// Token: 0x06004F46 RID: 20294 RVA: 0x0014C14D File Offset: 0x0014A34D
		private static CalendarItem CalendarItemCreator(ICoreItem coreItem)
		{
			return new CalendarItem(coreItem);
		}

		// Token: 0x06004F47 RID: 20295 RVA: 0x0014C155 File Offset: 0x0014A355
		private static CalendarItemSeries CalendarItemSeriesCreator(ICoreItem coreItem)
		{
			return new CalendarItemSeries(coreItem);
		}

		// Token: 0x06004F48 RID: 20296 RVA: 0x0014C15D File Offset: 0x0014A35D
		private static ParkedMeetingMessage ParkedMeetingMessageCreator(ICoreItem coreItem)
		{
			return new ParkedMeetingMessage(coreItem);
		}

		// Token: 0x06004F49 RID: 20297 RVA: 0x0014C165 File Offset: 0x0014A365
		private static MeetingRequest MeetingRequestCreator(ICoreItem coreItem)
		{
			return new MeetingRequest(coreItem);
		}

		// Token: 0x06004F4A RID: 20298 RVA: 0x0014C16D File Offset: 0x0014A36D
		private static MeetingResponse MeetingResponseCreator(ICoreItem coreItem)
		{
			return new MeetingResponse(coreItem);
		}

		// Token: 0x06004F4B RID: 20299 RVA: 0x0014C175 File Offset: 0x0014A375
		private static MeetingCancellation MeetingCancellationCreator(ICoreItem coreItem)
		{
			return new MeetingCancellation(coreItem);
		}

		// Token: 0x06004F4C RID: 20300 RVA: 0x0014C17D File Offset: 0x0014A37D
		private static MeetingForwardNotification MeetingForwardNotificationCreator(ICoreItem coreItem)
		{
			return new MeetingForwardNotification(coreItem);
		}

		// Token: 0x06004F4D RID: 20301 RVA: 0x0014C185 File Offset: 0x0014A385
		private static MeetingInquiryMessage MeetingInquiryMessageCreator(ICoreItem coreItem)
		{
			return new MeetingInquiryMessage(coreItem);
		}

		// Token: 0x06004F4E RID: 20302 RVA: 0x0014C18D File Offset: 0x0014A38D
		private static Contact ContactCreator(ICoreItem coreItem)
		{
			return new Contact(coreItem);
		}

		// Token: 0x06004F4F RID: 20303 RVA: 0x0014C195 File Offset: 0x0014A395
		private static Place PlaceCreator(ICoreItem coreItem)
		{
			return new Place(coreItem);
		}

		// Token: 0x06004F50 RID: 20304 RVA: 0x0014C19D File Offset: 0x0014A39D
		private static DistributionList DistributionListCreator(ICoreItem coreItem)
		{
			return new DistributionList(coreItem);
		}

		// Token: 0x06004F51 RID: 20305 RVA: 0x0014C1A5 File Offset: 0x0014A3A5
		private static MailboxAssociationGroup MailboxAssociationGroupCreator(ICoreItem coreItem)
		{
			return new MailboxAssociationGroup(coreItem);
		}

		// Token: 0x06004F52 RID: 20306 RVA: 0x0014C1AD File Offset: 0x0014A3AD
		private static MailboxAssociationUser MailboxAssociationUserCreator(ICoreItem coreItem)
		{
			return new MailboxAssociationUser(coreItem);
		}

		// Token: 0x06004F53 RID: 20307 RVA: 0x0014C1B5 File Offset: 0x0014A3B5
		private static HierarchySyncMetadataItem HierarchySyncMetadataCreator(ICoreItem coreItem)
		{
			return new HierarchySyncMetadataItem(coreItem);
		}

		// Token: 0x06004F54 RID: 20308 RVA: 0x0014C1BD File Offset: 0x0014A3BD
		private static Task TaskCreator(ICoreItem coreItem)
		{
			return new Task(coreItem);
		}

		// Token: 0x06004F55 RID: 20309 RVA: 0x0014C1C5 File Offset: 0x0014A3C5
		private static TaskRequest TaskRequestCreator(ICoreItem coreItem)
		{
			return new TaskRequest(coreItem);
		}

		// Token: 0x06004F56 RID: 20310 RVA: 0x0014C1CD File Offset: 0x0014A3CD
		private static ReminderMessage ReminderMessageCreator(ICoreItem coreItem)
		{
			return new ReminderMessage(coreItem);
		}

		// Token: 0x06004F57 RID: 20311 RVA: 0x0014C1D5 File Offset: 0x0014A3D5
		private static ConfigurationItem ConfigurationItemCreator(ICoreItem coreItem)
		{
			return new ConfigurationItem(coreItem);
		}

		// Token: 0x06004F58 RID: 20312 RVA: 0x0014C1DD File Offset: 0x0014A3DD
		private static Item GenericItemCreator(ICoreItem coreItem)
		{
			return new Item(coreItem, false);
		}

		// Token: 0x06004F59 RID: 20313 RVA: 0x0014C1E6 File Offset: 0x0014A3E6
		private static ConversationActionItem ConversationActionItemCreator(ICoreItem coreItem)
		{
			return new ConversationActionItem(coreItem);
		}

		// Token: 0x06004F5A RID: 20314 RVA: 0x0014C1EE File Offset: 0x0014A3EE
		private static CalendarGroup CalendarGroupCreator(ICoreItem coreItem)
		{
			return new CalendarGroup(coreItem);
		}

		// Token: 0x06004F5B RID: 20315 RVA: 0x0014C1F6 File Offset: 0x0014A3F6
		private static CalendarGroupEntry CalendarGroupEntryCreator(ICoreItem coreItem)
		{
			return new CalendarGroupEntry(coreItem);
		}

		// Token: 0x06004F5C RID: 20316 RVA: 0x0014C1FE File Offset: 0x0014A3FE
		private static FavoriteFolderEntry FavoriteFolderEntryCreator(ICoreItem coreItem)
		{
			return new FavoriteFolderEntry(coreItem);
		}

		// Token: 0x06004F5D RID: 20317 RVA: 0x0014C206 File Offset: 0x0014A406
		private static ShortcutMessage ShortcutMessageCreator(ICoreItem coreItem)
		{
			return new ShortcutMessage(coreItem);
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x0014C20E File Offset: 0x0014A40E
		private static TaskGroup TaskGroupCreator(ICoreItem coreItem)
		{
			return new TaskGroup(coreItem);
		}

		// Token: 0x06004F5F RID: 20319 RVA: 0x0014C216 File Offset: 0x0014A416
		private static TaskGroupEntry TaskGroupEntryCreator(ICoreItem coreItem)
		{
			return new TaskGroupEntry(coreItem);
		}

		// Token: 0x06004F60 RID: 20320 RVA: 0x0014C21E File Offset: 0x0014A41E
		private static ItemCreateInfo CreateCalendarItemOccurrenceInfo()
		{
			return new ItemCreateInfo(StoreObjectType.CalendarItemOccurrence, CalendarItemOccurrenceSchema.Instance, AcrProfile.AppointmentProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.CalendarItemOccurrenceCreator));
		}

		// Token: 0x06004F61 RID: 20321 RVA: 0x0014C23D File Offset: 0x0014A43D
		private static ItemCreateInfo CreateReportInfo()
		{
			return new ItemCreateInfo(StoreObjectType.Report, ReportMessageSchema.Instance, AcrProfile.MessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.ReportMessageCreator));
		}

		// Token: 0x06004F62 RID: 20322 RVA: 0x0014C25C File Offset: 0x0014A45C
		private static ItemCreateInfo CreateMessageItemInfo()
		{
			return new ItemCreateInfo(StoreObjectType.Message, MessageItemSchema.Instance, AcrProfile.MessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.MessageItemCreator));
		}

		// Token: 0x06004F63 RID: 20323 RVA: 0x0014C27B File Offset: 0x0014A47B
		private static ItemCreateInfo CreatePostInfo()
		{
			return new ItemCreateInfo(StoreObjectType.Post, PostItemSchema.Instance, AcrProfile.MessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.PostItemCreator));
		}

		// Token: 0x06004F64 RID: 20324 RVA: 0x0014C29A File Offset: 0x0014A49A
		private static ItemCreateInfo CreateCalendarItemInfo()
		{
			return new ItemCreateInfo(StoreObjectType.CalendarItem, CalendarItemSchema.Instance, AcrProfile.AppointmentProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.CalendarItemCreator));
		}

		// Token: 0x06004F65 RID: 20325 RVA: 0x0014C2B9 File Offset: 0x0014A4B9
		private static ItemCreateInfo CreateCalendarItemSeriesInfo()
		{
			return new ItemCreateInfo(StoreObjectType.CalendarItemSeries, CalendarItemSeriesSchema.Instance, AcrProfile.AppointmentProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.CalendarItemSeriesCreator));
		}

		// Token: 0x06004F66 RID: 20326 RVA: 0x0014C2D8 File Offset: 0x0014A4D8
		private static ItemCreateInfo CreateParkedMeetingMessageInfo()
		{
			return new ItemCreateInfo(StoreObjectType.ParkedMeetingMessage, ParkedMeetingMessageSchema.Instance, AcrProfile.BlankProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.ParkedMeetingMessageCreator));
		}

		// Token: 0x06004F67 RID: 20327 RVA: 0x0014C2F7 File Offset: 0x0014A4F7
		private static ItemCreateInfo CreateMeetingRequestInfo()
		{
			return new ItemCreateInfo(StoreObjectType.MeetingRequest, MeetingRequestSchema.Instance, AcrProfile.MeetingMessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.MeetingRequestCreator));
		}

		// Token: 0x06004F68 RID: 20328 RVA: 0x0014C316 File Offset: 0x0014A516
		private static ItemCreateInfo CreateMeetingRequestSeriesInfo()
		{
			return new ItemCreateInfo(StoreObjectType.MeetingRequestSeries, MeetingRequestSchema.Instance, AcrProfile.MeetingMessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.MeetingRequestCreator));
		}

		// Token: 0x06004F69 RID: 20329 RVA: 0x0014C335 File Offset: 0x0014A535
		private static ItemCreateInfo CreateMeetingResponseInfo()
		{
			return new ItemCreateInfo(StoreObjectType.MeetingResponse, MeetingResponseSchema.Instance, AcrProfile.MeetingMessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.MeetingResponseCreator));
		}

		// Token: 0x06004F6A RID: 20330 RVA: 0x0014C354 File Offset: 0x0014A554
		private static ItemCreateInfo CreateMeetingResponseSeriesInfo()
		{
			return new ItemCreateInfo(StoreObjectType.MeetingResponseSeries, MeetingResponseSchema.Instance, AcrProfile.MeetingMessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.MeetingResponseCreator));
		}

		// Token: 0x06004F6B RID: 20331 RVA: 0x0014C373 File Offset: 0x0014A573
		private static ItemCreateInfo CreateMeetingCancellationInfo()
		{
			return new ItemCreateInfo(StoreObjectType.MeetingCancellation, MeetingMessageInstanceSchema.Instance, AcrProfile.MeetingMessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.MeetingCancellationCreator));
		}

		// Token: 0x06004F6C RID: 20332 RVA: 0x0014C392 File Offset: 0x0014A592
		private static ItemCreateInfo CreateMeetingCancellationSeriesInfo()
		{
			return new ItemCreateInfo(StoreObjectType.MeetingCancellationSeries, MeetingMessageInstanceSchema.Instance, AcrProfile.MeetingMessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.MeetingCancellationCreator));
		}

		// Token: 0x06004F6D RID: 20333 RVA: 0x0014C3B1 File Offset: 0x0014A5B1
		private static ItemCreateInfo CreateMeetingForwardNotificationInfo()
		{
			return new ItemCreateInfo(StoreObjectType.MeetingForwardNotification, MeetingForwardNotificationSchema.Instance, AcrProfile.MeetingMessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.MeetingForwardNotificationCreator));
		}

		// Token: 0x06004F6E RID: 20334 RVA: 0x0014C3D0 File Offset: 0x0014A5D0
		private static ItemCreateInfo CreateMeetingForwardNotificationSeriesInfo()
		{
			return new ItemCreateInfo(StoreObjectType.MeetingForwardNotificationSeries, MeetingForwardNotificationSchema.Instance, AcrProfile.MeetingMessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.MeetingForwardNotificationCreator));
		}

		// Token: 0x06004F6F RID: 20335 RVA: 0x0014C3EF File Offset: 0x0014A5EF
		private static ItemCreateInfo CreateMeetingInquiryInfo()
		{
			return new ItemCreateInfo(StoreObjectType.MeetingInquiryMessage, MeetingInquiryMessageSchema.Instance, AcrProfile.MeetingMessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.MeetingInquiryMessageCreator));
		}

		// Token: 0x06004F70 RID: 20336 RVA: 0x0014C40E File Offset: 0x0014A60E
		private static ItemCreateInfo CreateContactInfo()
		{
			return new ItemCreateInfo(StoreObjectType.Contact, ContactSchema.Instance, AcrProfile.ContactProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.ContactCreator));
		}

		// Token: 0x06004F71 RID: 20337 RVA: 0x0014C42D File Offset: 0x0014A62D
		private static ItemCreateInfo CreatePlaceInfo()
		{
			return new ItemCreateInfo(StoreObjectType.Place, PlaceSchema.Instance, AcrProfile.ContactProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.PlaceCreator));
		}

		// Token: 0x06004F72 RID: 20338 RVA: 0x0014C44C File Offset: 0x0014A64C
		private static ItemCreateInfo CreateDistributionListInfo()
		{
			return new ItemCreateInfo(StoreObjectType.DistributionList, DistributionListSchema.Instance, AcrProfile.ContactProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.DistributionListCreator));
		}

		// Token: 0x06004F73 RID: 20339 RVA: 0x0014C46B File Offset: 0x0014A66B
		private static ItemCreateInfo CreateMailboxAssociationGroupInfo()
		{
			return new ItemCreateInfo(StoreObjectType.MailboxAssociationGroup, MailboxAssociationGroupSchema.Instance, AcrProfile.MailboxAssociationProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.MailboxAssociationGroupCreator));
		}

		// Token: 0x06004F74 RID: 20340 RVA: 0x0014C48A File Offset: 0x0014A68A
		private static ItemCreateInfo CreateMailboxAssociationUserInfo()
		{
			return new ItemCreateInfo(StoreObjectType.MailboxAssociationUser, MailboxAssociationUserSchema.Instance, AcrProfile.MailboxAssociationProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.MailboxAssociationUserCreator));
		}

		// Token: 0x06004F75 RID: 20341 RVA: 0x0014C4A9 File Offset: 0x0014A6A9
		private static ItemCreateInfo CreateHierarchySyncMetadataInfo()
		{
			return new ItemCreateInfo(StoreObjectType.HierarchySyncMetadata, HierarchySyncMetadataItemSchema.Instance, AcrProfile.MailboxAssociationProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.HierarchySyncMetadataCreator));
		}

		// Token: 0x06004F76 RID: 20342 RVA: 0x0014C4C8 File Offset: 0x0014A6C8
		private static ItemCreateInfo CreateTaskInfo()
		{
			return new ItemCreateInfo(StoreObjectType.Task, TaskSchema.Instance, AcrProfile.MessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.TaskCreator));
		}

		// Token: 0x06004F77 RID: 20343 RVA: 0x0014C4E7 File Offset: 0x0014A6E7
		private static ItemCreateInfo CreateTaskRequestInfo()
		{
			return new ItemCreateInfo(StoreObjectType.TaskRequest, TaskRequestSchema.Instance, AcrProfile.MessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.TaskRequestCreator));
		}

		// Token: 0x06004F78 RID: 20344 RVA: 0x0014C506 File Offset: 0x0014A706
		private static ItemCreateInfo CreateReminderMessageInfo()
		{
			return new ItemCreateInfo(StoreObjectType.ReminderMessage, ReminderMessageSchema.Instance, AcrProfile.MessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.ReminderMessageCreator));
		}

		// Token: 0x06004F79 RID: 20345 RVA: 0x0014C525 File Offset: 0x0014A725
		private static ItemCreateInfo CreateGenericItemInfo()
		{
			return new ItemCreateInfo(StoreObjectType.Unknown, ItemSchema.Instance, AcrProfile.GenericItemProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.GenericItemCreator));
		}

		// Token: 0x06004F7A RID: 20346 RVA: 0x0014C543 File Offset: 0x0014A743
		private static ItemCreateInfo CreateConversationActionInfo()
		{
			return new ItemCreateInfo(StoreObjectType.ConversationActionItem, ConversationActionItemSchema.Instance, AcrProfile.GenericItemProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.ConversationActionItemCreator));
		}

		// Token: 0x06004F7B RID: 20347 RVA: 0x0014C562 File Offset: 0x0014A762
		private static ItemCreateInfo CreateOofMessageItemInfo()
		{
			return new ItemCreateInfo(StoreObjectType.OofMessage, MessageItemSchema.Instance, AcrProfile.MessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.MessageItemCreator));
		}

		// Token: 0x06004F7C RID: 20348 RVA: 0x0014C581 File Offset: 0x0014A781
		private static ItemCreateInfo CreateExternalOofMessageItemInfo()
		{
			return new ItemCreateInfo(StoreObjectType.ExternalOofMessage, MessageItemSchema.Instance, AcrProfile.MessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.MessageItemCreator));
		}

		// Token: 0x06004F7D RID: 20349 RVA: 0x0014C5A0 File Offset: 0x0014A7A0
		private static ItemCreateInfo CreateCalendarGroupInfo()
		{
			return new ItemCreateInfo(StoreObjectType.CalendarGroup, CalendarGroupSchema.Instance, AcrProfile.MessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.CalendarGroupCreator));
		}

		// Token: 0x06004F7E RID: 20350 RVA: 0x0014C5BF File Offset: 0x0014A7BF
		private static ItemCreateInfo CreateCalendarGroupEntryInfo()
		{
			return new ItemCreateInfo(StoreObjectType.CalendarGroupEntry, CalendarGroupEntrySchema.Instance, AcrProfile.MessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.CalendarGroupEntryCreator));
		}

		// Token: 0x06004F7F RID: 20351 RVA: 0x0014C5DE File Offset: 0x0014A7DE
		private static ItemCreateInfo CreateFavoriteFolderEntryInfo()
		{
			return new ItemCreateInfo(StoreObjectType.FavoriteFolderEntry, FavoriteFolderEntrySchema.Instance, AcrProfile.MessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.FavoriteFolderEntryCreator));
		}

		// Token: 0x06004F80 RID: 20352 RVA: 0x0014C5FD File Offset: 0x0014A7FD
		private static ItemCreateInfo CreateShortcutMessageInfo()
		{
			return new ItemCreateInfo(StoreObjectType.ShortcutMessage, ShortcutMessageSchema.Instance, AcrProfile.MessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.ShortcutMessageCreator));
		}

		// Token: 0x06004F81 RID: 20353 RVA: 0x0014C61C File Offset: 0x0014A81C
		private static ItemCreateInfo CreateTaskGroupInfo()
		{
			return new ItemCreateInfo(StoreObjectType.TaskGroup, TaskGroupSchema.Instance, AcrProfile.MessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.TaskGroupCreator));
		}

		// Token: 0x06004F82 RID: 20354 RVA: 0x0014C63B File Offset: 0x0014A83B
		private static ItemCreateInfo CreateTaskGroupEntryInfo()
		{
			return new ItemCreateInfo(StoreObjectType.TaskGroupEntry, TaskGroupEntrySchema.Instance, AcrProfile.MessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.TaskGroupEntryCreator));
		}

		// Token: 0x06004F83 RID: 20355 RVA: 0x0014C65A File Offset: 0x0014A85A
		private static ItemCreateInfo CreateConfigurationItemInfo()
		{
			return new ItemCreateInfo(StoreObjectType.Configuration, ConfigurationItemSchema.Instance, AcrProfile.GenericItemProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.ConfigurationItemCreator));
		}

		// Token: 0x17001669 RID: 5737
		// (get) Token: 0x06004F84 RID: 20356 RVA: 0x0014C679 File Offset: 0x0014A879
		// (set) Token: 0x06004F85 RID: 20357 RVA: 0x0014C680 File Offset: 0x0014A880
		internal static ItemCreateInfo RightsManagedMessageItemInfo { get; private set; }

		// Token: 0x1700166A RID: 5738
		// (get) Token: 0x06004F86 RID: 20358 RVA: 0x0014C688 File Offset: 0x0014A888
		// (set) Token: 0x06004F87 RID: 20359 RVA: 0x0014C68F File Offset: 0x0014A88F
		internal static ItemCreateInfo SharingMessageItemInfo { get; private set; }

		// Token: 0x1700166B RID: 5739
		// (get) Token: 0x06004F88 RID: 20360 RVA: 0x0014C697 File Offset: 0x0014A897
		// (set) Token: 0x06004F89 RID: 20361 RVA: 0x0014C69E File Offset: 0x0014A89E
		internal static ItemCreateInfo PushNotificationSubscriptionItemInfo { get; private set; }

		// Token: 0x1700166C RID: 5740
		// (get) Token: 0x06004F8A RID: 20362 RVA: 0x0014C6A6 File Offset: 0x0014A8A6
		// (set) Token: 0x06004F8B RID: 20363 RVA: 0x0014C6AD File Offset: 0x0014A8AD
		internal static ItemCreateInfo CalendarItemOccurrenceInfo { get; private set; }

		// Token: 0x1700166D RID: 5741
		// (get) Token: 0x06004F8C RID: 20364 RVA: 0x0014C6B5 File Offset: 0x0014A8B5
		// (set) Token: 0x06004F8D RID: 20365 RVA: 0x0014C6BC File Offset: 0x0014A8BC
		internal static ItemCreateInfo ReportInfo { get; private set; }

		// Token: 0x1700166E RID: 5742
		// (get) Token: 0x06004F8E RID: 20366 RVA: 0x0014C6C4 File Offset: 0x0014A8C4
		// (set) Token: 0x06004F8F RID: 20367 RVA: 0x0014C6CB File Offset: 0x0014A8CB
		internal static ItemCreateInfo MessageItemInfo { get; private set; }

		// Token: 0x1700166F RID: 5743
		// (get) Token: 0x06004F90 RID: 20368 RVA: 0x0014C6D3 File Offset: 0x0014A8D3
		// (set) Token: 0x06004F91 RID: 20369 RVA: 0x0014C6DA File Offset: 0x0014A8DA
		internal static ItemCreateInfo PostInfo { get; private set; }

		// Token: 0x17001670 RID: 5744
		// (get) Token: 0x06004F92 RID: 20370 RVA: 0x0014C6E2 File Offset: 0x0014A8E2
		// (set) Token: 0x06004F93 RID: 20371 RVA: 0x0014C6E9 File Offset: 0x0014A8E9
		internal static ItemCreateInfo CalendarItemInfo { get; private set; }

		// Token: 0x17001671 RID: 5745
		// (get) Token: 0x06004F94 RID: 20372 RVA: 0x0014C6F1 File Offset: 0x0014A8F1
		// (set) Token: 0x06004F95 RID: 20373 RVA: 0x0014C6F8 File Offset: 0x0014A8F8
		internal static ItemCreateInfo CalendarItemSeriesInfo { get; private set; }

		// Token: 0x17001672 RID: 5746
		// (get) Token: 0x06004F96 RID: 20374 RVA: 0x0014C700 File Offset: 0x0014A900
		// (set) Token: 0x06004F97 RID: 20375 RVA: 0x0014C707 File Offset: 0x0014A907
		internal static ItemCreateInfo ParkedMeetingMessageInfo { get; private set; }

		// Token: 0x17001673 RID: 5747
		// (get) Token: 0x06004F98 RID: 20376 RVA: 0x0014C70F File Offset: 0x0014A90F
		// (set) Token: 0x06004F99 RID: 20377 RVA: 0x0014C716 File Offset: 0x0014A916
		internal static ItemCreateInfo MeetingRequestInfo { get; private set; }

		// Token: 0x17001674 RID: 5748
		// (get) Token: 0x06004F9A RID: 20378 RVA: 0x0014C71E File Offset: 0x0014A91E
		// (set) Token: 0x06004F9B RID: 20379 RVA: 0x0014C725 File Offset: 0x0014A925
		internal static ItemCreateInfo MeetingRequestSeriesInfo { get; private set; }

		// Token: 0x17001675 RID: 5749
		// (get) Token: 0x06004F9C RID: 20380 RVA: 0x0014C72D File Offset: 0x0014A92D
		// (set) Token: 0x06004F9D RID: 20381 RVA: 0x0014C734 File Offset: 0x0014A934
		internal static ItemCreateInfo MeetingResponseInfo { get; private set; }

		// Token: 0x17001676 RID: 5750
		// (get) Token: 0x06004F9E RID: 20382 RVA: 0x0014C73C File Offset: 0x0014A93C
		// (set) Token: 0x06004F9F RID: 20383 RVA: 0x0014C743 File Offset: 0x0014A943
		internal static ItemCreateInfo MeetingResponseSeriesInfo { get; private set; }

		// Token: 0x17001677 RID: 5751
		// (get) Token: 0x06004FA0 RID: 20384 RVA: 0x0014C74B File Offset: 0x0014A94B
		// (set) Token: 0x06004FA1 RID: 20385 RVA: 0x0014C752 File Offset: 0x0014A952
		internal static ItemCreateInfo MeetingCancellationInfo { get; private set; }

		// Token: 0x17001678 RID: 5752
		// (get) Token: 0x06004FA2 RID: 20386 RVA: 0x0014C75A File Offset: 0x0014A95A
		// (set) Token: 0x06004FA3 RID: 20387 RVA: 0x0014C761 File Offset: 0x0014A961
		internal static ItemCreateInfo MeetingCancellationSeriesInfo { get; private set; }

		// Token: 0x17001679 RID: 5753
		// (get) Token: 0x06004FA4 RID: 20388 RVA: 0x0014C769 File Offset: 0x0014A969
		// (set) Token: 0x06004FA5 RID: 20389 RVA: 0x0014C770 File Offset: 0x0014A970
		internal static ItemCreateInfo MeetingForwardNotificationInfo { get; private set; }

		// Token: 0x1700167A RID: 5754
		// (get) Token: 0x06004FA6 RID: 20390 RVA: 0x0014C778 File Offset: 0x0014A978
		// (set) Token: 0x06004FA7 RID: 20391 RVA: 0x0014C77F File Offset: 0x0014A97F
		internal static ItemCreateInfo MeetingForwardNotificationSeriesInfo { get; private set; }

		// Token: 0x1700167B RID: 5755
		// (get) Token: 0x06004FA8 RID: 20392 RVA: 0x0014C787 File Offset: 0x0014A987
		// (set) Token: 0x06004FA9 RID: 20393 RVA: 0x0014C78E File Offset: 0x0014A98E
		internal static ItemCreateInfo MeetingInquiryInfo { get; private set; }

		// Token: 0x1700167C RID: 5756
		// (get) Token: 0x06004FAA RID: 20394 RVA: 0x0014C796 File Offset: 0x0014A996
		// (set) Token: 0x06004FAB RID: 20395 RVA: 0x0014C79D File Offset: 0x0014A99D
		internal static ItemCreateInfo ContactInfo { get; private set; }

		// Token: 0x1700167D RID: 5757
		// (get) Token: 0x06004FAC RID: 20396 RVA: 0x0014C7A5 File Offset: 0x0014A9A5
		// (set) Token: 0x06004FAD RID: 20397 RVA: 0x0014C7AC File Offset: 0x0014A9AC
		internal static ItemCreateInfo PlaceInfo { get; private set; }

		// Token: 0x1700167E RID: 5758
		// (get) Token: 0x06004FAE RID: 20398 RVA: 0x0014C7B4 File Offset: 0x0014A9B4
		// (set) Token: 0x06004FAF RID: 20399 RVA: 0x0014C7BB File Offset: 0x0014A9BB
		internal static ItemCreateInfo DistributionListInfo { get; private set; }

		// Token: 0x1700167F RID: 5759
		// (get) Token: 0x06004FB0 RID: 20400 RVA: 0x0014C7C3 File Offset: 0x0014A9C3
		// (set) Token: 0x06004FB1 RID: 20401 RVA: 0x0014C7CA File Offset: 0x0014A9CA
		internal static ItemCreateInfo MailboxAssociationGroupInfo { get; private set; }

		// Token: 0x17001680 RID: 5760
		// (get) Token: 0x06004FB2 RID: 20402 RVA: 0x0014C7D2 File Offset: 0x0014A9D2
		// (set) Token: 0x06004FB3 RID: 20403 RVA: 0x0014C7D9 File Offset: 0x0014A9D9
		internal static ItemCreateInfo MailboxAssociationUserInfo { get; private set; }

		// Token: 0x17001681 RID: 5761
		// (get) Token: 0x06004FB4 RID: 20404 RVA: 0x0014C7E1 File Offset: 0x0014A9E1
		// (set) Token: 0x06004FB5 RID: 20405 RVA: 0x0014C7E8 File Offset: 0x0014A9E8
		internal static ItemCreateInfo HierarchySyncMetadataInfo { get; private set; }

		// Token: 0x17001682 RID: 5762
		// (get) Token: 0x06004FB6 RID: 20406 RVA: 0x0014C7F0 File Offset: 0x0014A9F0
		// (set) Token: 0x06004FB7 RID: 20407 RVA: 0x0014C7F7 File Offset: 0x0014A9F7
		internal static ItemCreateInfo TaskInfo { get; private set; }

		// Token: 0x17001683 RID: 5763
		// (get) Token: 0x06004FB8 RID: 20408 RVA: 0x0014C7FF File Offset: 0x0014A9FF
		// (set) Token: 0x06004FB9 RID: 20409 RVA: 0x0014C806 File Offset: 0x0014AA06
		internal static ItemCreateInfo TaskRequestInfo { get; private set; }

		// Token: 0x17001684 RID: 5764
		// (get) Token: 0x06004FBA RID: 20410 RVA: 0x0014C80E File Offset: 0x0014AA0E
		// (set) Token: 0x06004FBB RID: 20411 RVA: 0x0014C815 File Offset: 0x0014AA15
		internal static ItemCreateInfo ReminderMessageInfo { get; private set; }

		// Token: 0x17001685 RID: 5765
		// (get) Token: 0x06004FBC RID: 20412 RVA: 0x0014C81D File Offset: 0x0014AA1D
		// (set) Token: 0x06004FBD RID: 20413 RVA: 0x0014C824 File Offset: 0x0014AA24
		internal static ItemCreateInfo GenericItemInfo { get; private set; }

		// Token: 0x17001686 RID: 5766
		// (get) Token: 0x06004FBE RID: 20414 RVA: 0x0014C82C File Offset: 0x0014AA2C
		// (set) Token: 0x06004FBF RID: 20415 RVA: 0x0014C833 File Offset: 0x0014AA33
		internal static ItemCreateInfo ConversationActionInfo { get; private set; }

		// Token: 0x17001687 RID: 5767
		// (get) Token: 0x06004FC0 RID: 20416 RVA: 0x0014C83B File Offset: 0x0014AA3B
		// (set) Token: 0x06004FC1 RID: 20417 RVA: 0x0014C842 File Offset: 0x0014AA42
		internal static ItemCreateInfo OofMessageItemInfo { get; private set; }

		// Token: 0x17001688 RID: 5768
		// (get) Token: 0x06004FC2 RID: 20418 RVA: 0x0014C84A File Offset: 0x0014AA4A
		// (set) Token: 0x06004FC3 RID: 20419 RVA: 0x0014C851 File Offset: 0x0014AA51
		internal static ItemCreateInfo ExternalOofMessageItemInfo { get; private set; }

		// Token: 0x17001689 RID: 5769
		// (get) Token: 0x06004FC4 RID: 20420 RVA: 0x0014C859 File Offset: 0x0014AA59
		// (set) Token: 0x06004FC5 RID: 20421 RVA: 0x0014C860 File Offset: 0x0014AA60
		internal static ItemCreateInfo CalendarGroupInfo { get; private set; }

		// Token: 0x1700168A RID: 5770
		// (get) Token: 0x06004FC6 RID: 20422 RVA: 0x0014C868 File Offset: 0x0014AA68
		// (set) Token: 0x06004FC7 RID: 20423 RVA: 0x0014C86F File Offset: 0x0014AA6F
		internal static ItemCreateInfo CalendarGroupEntryInfo { get; private set; }

		// Token: 0x1700168B RID: 5771
		// (get) Token: 0x06004FC8 RID: 20424 RVA: 0x0014C877 File Offset: 0x0014AA77
		// (set) Token: 0x06004FC9 RID: 20425 RVA: 0x0014C87E File Offset: 0x0014AA7E
		internal static ItemCreateInfo FavoriteFolderEntryInfo { get; private set; }

		// Token: 0x1700168C RID: 5772
		// (get) Token: 0x06004FCA RID: 20426 RVA: 0x0014C886 File Offset: 0x0014AA86
		// (set) Token: 0x06004FCB RID: 20427 RVA: 0x0014C88D File Offset: 0x0014AA8D
		internal static ItemCreateInfo ShortcutMessageInfo { get; private set; }

		// Token: 0x1700168D RID: 5773
		// (get) Token: 0x06004FCC RID: 20428 RVA: 0x0014C895 File Offset: 0x0014AA95
		// (set) Token: 0x06004FCD RID: 20429 RVA: 0x0014C89C File Offset: 0x0014AA9C
		internal static ItemCreateInfo GroupMailboxJoinRequestMessageInfo { get; private set; }

		// Token: 0x1700168E RID: 5774
		// (get) Token: 0x06004FCE RID: 20430 RVA: 0x0014C8A4 File Offset: 0x0014AAA4
		// (set) Token: 0x06004FCF RID: 20431 RVA: 0x0014C8AB File Offset: 0x0014AAAB
		internal static ItemCreateInfo TaskGroupInfo { get; private set; }

		// Token: 0x1700168F RID: 5775
		// (get) Token: 0x06004FD0 RID: 20432 RVA: 0x0014C8B3 File Offset: 0x0014AAB3
		// (set) Token: 0x06004FD1 RID: 20433 RVA: 0x0014C8BA File Offset: 0x0014AABA
		internal static ItemCreateInfo TaskGroupEntryInfo { get; private set; }

		// Token: 0x17001690 RID: 5776
		// (get) Token: 0x06004FD2 RID: 20434 RVA: 0x0014C8C2 File Offset: 0x0014AAC2
		// (set) Token: 0x06004FD3 RID: 20435 RVA: 0x0014C8C9 File Offset: 0x0014AAC9
		internal static ItemCreateInfo OutlookServiceSubscriptionItemInfo { get; private set; }

		// Token: 0x17001691 RID: 5777
		// (get) Token: 0x06004FD4 RID: 20436 RVA: 0x0014C8D1 File Offset: 0x0014AAD1
		// (set) Token: 0x06004FD5 RID: 20437 RVA: 0x0014C8D8 File Offset: 0x0014AAD8
		internal static ItemCreateInfo ConfigurationItemInfo { get; private set; }

		// Token: 0x06004FD6 RID: 20438 RVA: 0x0014C8E0 File Offset: 0x0014AAE0
		private static RightsManagedMessageItem RightsManagedMessageItemCreator(ICoreItem coreItem)
		{
			return new RightsManagedMessageItem(coreItem);
		}

		// Token: 0x06004FD7 RID: 20439 RVA: 0x0014C8E8 File Offset: 0x0014AAE8
		private static SharingMessageItem SharingMessageItemCreator(ICoreItem coreItem)
		{
			return new SharingMessageItem(coreItem);
		}

		// Token: 0x06004FD8 RID: 20440 RVA: 0x0014C8F0 File Offset: 0x0014AAF0
		private static PushNotificationSubscriptionItem PushNotificationSubscriptionItemCreator(ICoreItem coreItem)
		{
			return new PushNotificationSubscriptionItem(coreItem);
		}

		// Token: 0x06004FD9 RID: 20441 RVA: 0x0014C8F8 File Offset: 0x0014AAF8
		private static GroupMailboxJoinRequestMessageItem GroupMailboxJoinRequestMessageItemCreator(ICoreItem coreItem)
		{
			return new GroupMailboxJoinRequestMessageItem(coreItem);
		}

		// Token: 0x06004FDA RID: 20442 RVA: 0x0014C900 File Offset: 0x0014AB00
		private static OutlookServiceSubscriptionItem OutlookServiceSubscriptionItemCreator(ICoreItem coreItem)
		{
			return new OutlookServiceSubscriptionItem(coreItem);
		}

		// Token: 0x06004FDB RID: 20443 RVA: 0x0014C908 File Offset: 0x0014AB08
		private static ItemCreateInfo CreateOutlookServiceSubscriptionItemInfo()
		{
			return new ItemCreateInfo(StoreObjectType.OutlookServiceSubscription, OutlookServiceSubscriptionItemSchema.Instance, AcrProfile.GenericItemProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.OutlookServiceSubscriptionItemCreator));
		}

		// Token: 0x06004FDC RID: 20444 RVA: 0x0014C928 File Offset: 0x0014AB28
		internal static ItemCreateInfo GetItemCreateInfo(StoreObjectType storeObjectType)
		{
			ItemCreateInfo result;
			if (!ItemCreateInfo.itemCreateInfoDictionary.TryGetValue(storeObjectType, out result))
			{
				result = ItemCreateInfo.itemCreateInfoDictionary[StoreObjectType.Unknown];
			}
			return result;
		}

		// Token: 0x06004FDD RID: 20445 RVA: 0x0014C954 File Offset: 0x0014AB54
		private static Dictionary<StoreObjectType, ItemCreateInfo> CreateDictionary()
		{
			ItemCreateInfo.RightsManagedMessageItemInfo = new ItemCreateInfo(StoreObjectType.RightsManagedMessage, RightsManagedMessageItemSchema.Instance, AcrProfile.MessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.RightsManagedMessageItemCreator));
			ItemCreateInfo.SharingMessageItemInfo = new ItemCreateInfo(StoreObjectType.SharingMessage, SharingMessageItemSchema.Instance, AcrProfile.MessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.SharingMessageItemCreator));
			ItemCreateInfo.PushNotificationSubscriptionItemInfo = new ItemCreateInfo(StoreObjectType.PushNotificationSubscription, PushNotificationSubscriptionItemSchema.Instance, AcrProfile.GenericItemProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.PushNotificationSubscriptionItemCreator));
			ItemCreateInfo.GroupMailboxJoinRequestMessageInfo = new ItemCreateInfo(StoreObjectType.GroupMailboxRequestMessage, GroupMailboxJoinRequestMessageSchema.Instance, AcrProfile.MessageProfile, new ItemCreateInfo.ItemCreator(ItemCreateInfo.GroupMailboxJoinRequestMessageItemCreator));
			ItemCreateInfo.CalendarItemOccurrenceInfo = ItemCreateInfo.CreateCalendarItemOccurrenceInfo();
			ItemCreateInfo.ReportInfo = ItemCreateInfo.CreateReportInfo();
			ItemCreateInfo.MessageItemInfo = ItemCreateInfo.CreateMessageItemInfo();
			ItemCreateInfo.PostInfo = ItemCreateInfo.CreatePostInfo();
			ItemCreateInfo.CalendarItemInfo = ItemCreateInfo.CreateCalendarItemInfo();
			ItemCreateInfo.CalendarItemSeriesInfo = ItemCreateInfo.CreateCalendarItemSeriesInfo();
			ItemCreateInfo.ParkedMeetingMessageInfo = ItemCreateInfo.CreateParkedMeetingMessageInfo();
			ItemCreateInfo.MeetingRequestInfo = ItemCreateInfo.CreateMeetingRequestInfo();
			ItemCreateInfo.MeetingRequestSeriesInfo = ItemCreateInfo.CreateMeetingRequestSeriesInfo();
			ItemCreateInfo.MeetingResponseInfo = ItemCreateInfo.CreateMeetingResponseInfo();
			ItemCreateInfo.MeetingResponseSeriesInfo = ItemCreateInfo.CreateMeetingResponseSeriesInfo();
			ItemCreateInfo.MeetingCancellationInfo = ItemCreateInfo.CreateMeetingCancellationInfo();
			ItemCreateInfo.MeetingCancellationSeriesInfo = ItemCreateInfo.CreateMeetingCancellationSeriesInfo();
			ItemCreateInfo.MeetingForwardNotificationInfo = ItemCreateInfo.CreateMeetingForwardNotificationInfo();
			ItemCreateInfo.MeetingForwardNotificationSeriesInfo = ItemCreateInfo.CreateMeetingForwardNotificationSeriesInfo();
			ItemCreateInfo.MeetingInquiryInfo = ItemCreateInfo.CreateMeetingInquiryInfo();
			ItemCreateInfo.ContactInfo = ItemCreateInfo.CreateContactInfo();
			ItemCreateInfo.PlaceInfo = ItemCreateInfo.CreatePlaceInfo();
			ItemCreateInfo.DistributionListInfo = ItemCreateInfo.CreateDistributionListInfo();
			ItemCreateInfo.MailboxAssociationGroupInfo = ItemCreateInfo.CreateMailboxAssociationGroupInfo();
			ItemCreateInfo.MailboxAssociationUserInfo = ItemCreateInfo.CreateMailboxAssociationUserInfo();
			ItemCreateInfo.HierarchySyncMetadataInfo = ItemCreateInfo.CreateHierarchySyncMetadataInfo();
			ItemCreateInfo.TaskInfo = ItemCreateInfo.CreateTaskInfo();
			ItemCreateInfo.TaskRequestInfo = ItemCreateInfo.CreateTaskRequestInfo();
			ItemCreateInfo.ReminderMessageInfo = ItemCreateInfo.CreateReminderMessageInfo();
			ItemCreateInfo.GenericItemInfo = ItemCreateInfo.CreateGenericItemInfo();
			ItemCreateInfo.ConversationActionInfo = ItemCreateInfo.CreateConversationActionInfo();
			ItemCreateInfo.OofMessageItemInfo = ItemCreateInfo.CreateOofMessageItemInfo();
			ItemCreateInfo.ExternalOofMessageItemInfo = ItemCreateInfo.CreateExternalOofMessageItemInfo();
			ItemCreateInfo.CalendarGroupInfo = ItemCreateInfo.CreateCalendarGroupInfo();
			ItemCreateInfo.CalendarGroupEntryInfo = ItemCreateInfo.CreateCalendarGroupEntryInfo();
			ItemCreateInfo.FavoriteFolderEntryInfo = ItemCreateInfo.CreateFavoriteFolderEntryInfo();
			ItemCreateInfo.ShortcutMessageInfo = ItemCreateInfo.CreateShortcutMessageInfo();
			ItemCreateInfo.TaskGroupInfo = ItemCreateInfo.CreateTaskGroupInfo();
			ItemCreateInfo.TaskGroupEntryInfo = ItemCreateInfo.CreateTaskGroupEntryInfo();
			ItemCreateInfo.OutlookServiceSubscriptionItemInfo = ItemCreateInfo.CreateOutlookServiceSubscriptionItemInfo();
			ItemCreateInfo.ConfigurationItemInfo = ItemCreateInfo.CreateConfigurationItemInfo();
			Dictionary<StoreObjectType, ItemCreateInfo> dictionary = new Dictionary<StoreObjectType, ItemCreateInfo>(new StoreObjectTypeComparer());
			ItemCreateInfo[] array = new ItemCreateInfo[]
			{
				ItemCreateInfo.RightsManagedMessageItemInfo,
				ItemCreateInfo.SharingMessageItemInfo,
				ItemCreateInfo.PushNotificationSubscriptionItemInfo,
				ItemCreateInfo.CalendarItemOccurrenceInfo,
				ItemCreateInfo.ReportInfo,
				ItemCreateInfo.MessageItemInfo,
				ItemCreateInfo.PostInfo,
				ItemCreateInfo.CalendarItemInfo,
				ItemCreateInfo.CalendarItemSeriesInfo,
				ItemCreateInfo.ParkedMeetingMessageInfo,
				ItemCreateInfo.MeetingRequestInfo,
				ItemCreateInfo.MeetingRequestSeriesInfo,
				ItemCreateInfo.MeetingResponseInfo,
				ItemCreateInfo.MeetingResponseSeriesInfo,
				ItemCreateInfo.MeetingCancellationInfo,
				ItemCreateInfo.MeetingCancellationSeriesInfo,
				ItemCreateInfo.MeetingForwardNotificationInfo,
				ItemCreateInfo.MeetingForwardNotificationSeriesInfo,
				ItemCreateInfo.MeetingInquiryInfo,
				ItemCreateInfo.ContactInfo,
				ItemCreateInfo.PlaceInfo,
				ItemCreateInfo.DistributionListInfo,
				ItemCreateInfo.MailboxAssociationGroupInfo,
				ItemCreateInfo.MailboxAssociationUserInfo,
				ItemCreateInfo.HierarchySyncMetadataInfo,
				ItemCreateInfo.TaskInfo,
				ItemCreateInfo.TaskRequestInfo,
				ItemCreateInfo.ReminderMessageInfo,
				ItemCreateInfo.GenericItemInfo,
				ItemCreateInfo.ConversationActionInfo,
				ItemCreateInfo.OofMessageItemInfo,
				ItemCreateInfo.ExternalOofMessageItemInfo,
				ItemCreateInfo.CalendarGroupInfo,
				ItemCreateInfo.CalendarGroupEntryInfo,
				ItemCreateInfo.FavoriteFolderEntryInfo,
				ItemCreateInfo.ShortcutMessageInfo,
				ItemCreateInfo.TaskGroupInfo,
				ItemCreateInfo.TaskGroupEntryInfo,
				ItemCreateInfo.GroupMailboxJoinRequestMessageInfo,
				ItemCreateInfo.OutlookServiceSubscriptionItemInfo,
				ItemCreateInfo.ConfigurationItemInfo
			};
			foreach (ItemCreateInfo itemCreateInfo in array)
			{
				dictionary.Add(itemCreateInfo.Type, itemCreateInfo);
			}
			return dictionary;
		}

		// Token: 0x04002B2E RID: 11054
		internal readonly StoreObjectType Type;

		// Token: 0x04002B2F RID: 11055
		internal readonly Schema Schema;

		// Token: 0x04002B30 RID: 11056
		internal readonly AcrProfile AcrProfile;

		// Token: 0x04002B31 RID: 11057
		internal readonly ItemCreateInfo.ItemCreator Creator;

		// Token: 0x04002B32 RID: 11058
		private static Dictionary<StoreObjectType, ItemCreateInfo> itemCreateInfoDictionary = ItemCreateInfo.CreateDictionary();

		// Token: 0x02000852 RID: 2130
		// (Invoke) Token: 0x06004FE0 RID: 20448
		internal delegate Item ItemCreator(ICoreItem coreItem);
	}
}
