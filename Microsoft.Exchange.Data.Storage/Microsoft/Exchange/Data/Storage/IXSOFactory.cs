using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000289 RID: 649
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IXSOFactory
	{
		// Token: 0x06001AED RID: 6893
		IContact BindToContact(IStoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn);

		// Token: 0x06001AEE RID: 6894
		IContact CreateContact(IStoreSession session, StoreId contactFolderId);

		// Token: 0x06001AEF RID: 6895
		IFolder BindToFolder(IStoreSession session, StoreObjectId folderId);

		// Token: 0x06001AF0 RID: 6896
		IFolder BindToFolder(IMailboxSession session, DefaultFolderType defaultFolderType);

		// Token: 0x06001AF1 RID: 6897
		IFolder BindToFolder(IMailboxSession session, DefaultFolderType defaultFolderType, params PropertyDefinition[] propsToReturn);

		// Token: 0x06001AF2 RID: 6898
		ISearchFolder BindToSearchFolder(IMailboxSession session, DefaultFolderType defaultFolderType);

		// Token: 0x06001AF3 RID: 6899
		ISearchFolder BindToSearchFolder(IStoreSession session, StoreId folderId);

		// Token: 0x06001AF4 RID: 6900
		IDistributionList BindToDistributionList(IStoreSession session, StoreId storeId);

		// Token: 0x06001AF5 RID: 6901
		IDistributionList BindToDistributionList(IStoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn);

		// Token: 0x06001AF6 RID: 6902
		IDistributionList CreateDistributionList(IStoreSession session, StoreId contactFolderId);

		// Token: 0x06001AF7 RID: 6903
		IMailboxAssociationGroup BindToMailboxAssociationGroup(IStoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn);

		// Token: 0x06001AF8 RID: 6904
		IMailboxAssociationGroup CreateMailboxAssociationGroup(IStoreSession session, StoreId folderId);

		// Token: 0x06001AF9 RID: 6905
		IMailboxAssociationUser BindToMailboxAssociationUser(IStoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn);

		// Token: 0x06001AFA RID: 6906
		IMailboxAssociationUser CreateMailboxAssociationUser(IStoreSession session, StoreId folderId);

		// Token: 0x06001AFB RID: 6907
		IMessageItem BindToMessage(IStoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn = null);

		// Token: 0x06001AFC RID: 6908
		IMessageItem Create(IStoreSession session, StoreId destFolderId);

		// Token: 0x06001AFD RID: 6909
		IMessageItem CreateMessageAssociated(IStoreSession session, StoreId destFolderId);

		// Token: 0x06001AFE RID: 6910
		ICalendarFolder BindToCalendarFolder(IStoreSession session, StoreId id);

		// Token: 0x06001AFF RID: 6911
		IItem BindToItem(IStoreSession session, StoreId id, params PropertyDefinition[] propsToReturn);

		// Token: 0x06001B00 RID: 6912
		ICalendarFolder CreateCalendarFolder(IStoreSession session, StoreId parentFolderId);

		// Token: 0x06001B01 RID: 6913
		ICalendarGroupEntry BindToCalendarGroupEntry(IMailboxSession session, StoreId id);

		// Token: 0x06001B02 RID: 6914
		ICalendarGroupEntry CreateCalendarGroupEntry(IMailboxSession session, string legacyDistinguishedName, ICalendarGroup parentGroup);

		// Token: 0x06001B03 RID: 6915
		ICalendarGroupEntry CreateCalendarGroupEntry(IMailboxSession session, StoreObjectId calendarFolderId, ICalendarGroup parentGroup);

		// Token: 0x06001B04 RID: 6916
		CalendarGroupInfoList GetCalendarGroupsView(IMailboxSession session);

		// Token: 0x06001B05 RID: 6917
		ICalendarItemBase BindToCalendarItemBase(IStoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn = null);

		// Token: 0x06001B06 RID: 6918
		ICalendarItem CreateCalendarItem(IStoreSession session, StoreId parentFolderId);

		// Token: 0x06001B07 RID: 6919
		IMeetingMessage BindToMeetingMessage(IStoreSession session, StoreId storeId);

		// Token: 0x06001B08 RID: 6920
		IMeetingRequest BindToMeetingRequestMessage(IStoreSession session, StoreId storeId);

		// Token: 0x06001B09 RID: 6921
		ICalendarItemBase CreateCalendarItemSeries(IStoreSession session, StoreId parentFolderId);

		// Token: 0x06001B0A RID: 6922
		ICalendarGroup CreateCalendarGroup(IMailboxSession session);

		// Token: 0x06001B0B RID: 6923
		ICalendarGroup BindToCalendarGroup(IMailboxSession session, CalendarGroupType defaultGroupType);

		// Token: 0x06001B0C RID: 6924
		ICalendarGroup BindToCalendarGroup(IMailboxSession session, Guid groupClassId);

		// Token: 0x06001B0D RID: 6925
		ICalendarGroup BindToCalendarGroup(IMailboxSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn = null);

		// Token: 0x06001B0E RID: 6926
		IHierarchySyncMetadataItem GetHierarchySyncMetadataItem(IStoreSession session, IFolder folder, ICollection<PropertyDefinition> propsToReturn);

		// Token: 0x06001B0F RID: 6927
		IAttachment CloneAttachment(IAttachment attachment, IItem draftItem);

		// Token: 0x06001B10 RID: 6928
		IAttachment CreateAttachment(IItem item, AttachmentType type);

		// Token: 0x06001B11 RID: 6929
		IMailboxSession ConfigurableOpenMailboxSession(ExchangePrincipal mailbox, MailboxAccessInfo accessInfo, CultureInfo cultureInfo, string clientInfoString, LogonType logonType, PropertyDefinition[] mailboxProperties, MailboxSession.InitializationFlags initFlags, IList<DefaultFolderType> foldersToInit);

		// Token: 0x06001B12 RID: 6930
		T RunQueryOnAllItemsFolder<T>(IMailboxSession session, AllItemsFolderHelper.SupportedSortBy supportedSortBy, object seekToValue, T defaultValue, AllItemsFolderHelper.DoQueryProcessing<T> queryProcessor, ICollection<PropertyDefinition> properties);

		// Token: 0x06001B13 RID: 6931
		T RunQueryOnAllItemsFolder<T>(IMailboxSession session, AllItemsFolderHelper.SupportedSortBy supportedSortBy, AllItemsFolderHelper.DoQueryProcessing<T> queryProcessor, ICollection<PropertyDefinition> properties);

		// Token: 0x06001B14 RID: 6932
		IPushNotificationStorage CreatePushNotificationStorage(IMailboxSession session);

		// Token: 0x06001B15 RID: 6933
		IPushNotificationStorage FindPushNotificationStorage(IMailboxSession session);

		// Token: 0x06001B16 RID: 6934
		IPushNotificationSubscriptionItem BindToPushNotificationSubscriptionItem(IStoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn);

		// Token: 0x06001B17 RID: 6935
		IPushNotificationSubscriptionItem CreatePushNotificationSubscriptionItem(IStoreSession session, StoreId destFolderId);

		// Token: 0x06001B18 RID: 6936
		IOutlookServiceSubscriptionItem BindToOutlookServiceSubscriptionItem(IStoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn);

		// Token: 0x06001B19 RID: 6937
		IOutlookServiceSubscriptionItem CreateOutlookServiceSubscriptionItem(IStoreSession session, StoreId destFolderId);
	}
}
