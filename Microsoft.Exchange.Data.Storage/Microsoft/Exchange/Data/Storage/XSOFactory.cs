using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002BA RID: 698
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class XSOFactory : IXSOFactory
	{
		// Token: 0x06001D09 RID: 7433 RVA: 0x00085854 File Offset: 0x00083A54
		public IContact BindToContact(IStoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			return Contact.Bind((StoreSession)session, storeId, propsToReturn);
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x00085863 File Offset: 0x00083A63
		public IContact CreateContact(IStoreSession session, StoreId contactFolderId)
		{
			return Contact.Create((StoreSession)session, contactFolderId);
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x00085871 File Offset: 0x00083A71
		public IFolder BindToFolder(IStoreSession session, StoreObjectId folderId)
		{
			return Folder.Bind((StoreSession)session, folderId);
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x0008587F File Offset: 0x00083A7F
		public IFolder BindToFolder(IMailboxSession session, DefaultFolderType defaultFolderType)
		{
			return Folder.Bind((MailboxSession)session, defaultFolderType);
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x0008588D File Offset: 0x00083A8D
		public IFolder BindToFolder(IMailboxSession session, DefaultFolderType defaultFolderType, params PropertyDefinition[] propsToReturn)
		{
			return Folder.Bind((MailboxSession)session, defaultFolderType, propsToReturn);
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x0008589C File Offset: 0x00083A9C
		public ISearchFolder BindToSearchFolder(IMailboxSession session, DefaultFolderType defaultFolderType)
		{
			return SearchFolder.Bind((MailboxSession)session, defaultFolderType);
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x000858AA File Offset: 0x00083AAA
		public ISearchFolder BindToSearchFolder(IStoreSession session, StoreId folderId)
		{
			return SearchFolder.Bind((StoreSession)session, folderId);
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x000858B8 File Offset: 0x00083AB8
		public IDistributionList BindToDistributionList(IStoreSession session, StoreId storeId)
		{
			return DistributionList.Bind((StoreSession)session, storeId);
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x000858C6 File Offset: 0x00083AC6
		public IDistributionList BindToDistributionList(IStoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			return DistributionList.Bind((StoreSession)session, storeId, propsToReturn);
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x000858D5 File Offset: 0x00083AD5
		public IDistributionList CreateDistributionList(IStoreSession session, StoreId contactFolderId)
		{
			return DistributionList.Create((StoreSession)session, contactFolderId);
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x000858E3 File Offset: 0x00083AE3
		public IMailboxAssociationGroup BindToMailboxAssociationGroup(IStoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			return MailboxAssociationGroup.Bind((StoreSession)session, storeId, propsToReturn);
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x000858F2 File Offset: 0x00083AF2
		public IMailboxAssociationGroup CreateMailboxAssociationGroup(IStoreSession session, StoreId folderId)
		{
			return MailboxAssociationGroup.Create((StoreSession)session, folderId);
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x00085900 File Offset: 0x00083B00
		public IMailboxAssociationUser BindToMailboxAssociationUser(IStoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			return MailboxAssociationUser.Bind((StoreSession)session, storeId, propsToReturn);
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x0008590F File Offset: 0x00083B0F
		public IMailboxAssociationUser CreateMailboxAssociationUser(IStoreSession session, StoreId folderId)
		{
			return MailboxAssociationUser.Create((StoreSession)session, folderId);
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x0008591D File Offset: 0x00083B1D
		public IMessageItem BindToMessage(IStoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn = null)
		{
			return MessageItem.Bind((StoreSession)session, storeId, propsToReturn);
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x0008592C File Offset: 0x00083B2C
		public IMessageItem Create(IStoreSession session, StoreId destFolderId)
		{
			return MessageItem.Create((StoreSession)session, destFolderId);
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x0008593A File Offset: 0x00083B3A
		public IMessageItem CreateMessageAssociated(IStoreSession session, StoreId destFolderId)
		{
			return MessageItem.CreateAssociated((StoreSession)session, destFolderId);
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x00085948 File Offset: 0x00083B48
		public ICalendarFolder BindToCalendarFolder(IStoreSession session, StoreId id)
		{
			return CalendarFolder.Bind((StoreSession)session, id);
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x00085956 File Offset: 0x00083B56
		public IItem BindToItem(IStoreSession session, StoreId id, params PropertyDefinition[] propsToReturn)
		{
			return Item.Bind((StoreSession)session, id, propsToReturn);
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x00085965 File Offset: 0x00083B65
		public ICalendarFolder CreateCalendarFolder(IStoreSession session, StoreId parentFolderId)
		{
			return CalendarFolder.Create((StoreSession)session, parentFolderId);
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x00085974 File Offset: 0x00083B74
		public ICalendarGroupEntry BindToCalendarGroupEntry(IMailboxSession session, StoreId id)
		{
			StoreObjectId storeObjectId = StoreId.GetStoreObjectId(id);
			if (!storeObjectId.IsFolderId)
			{
				return CalendarGroupEntry.Bind((MailboxSession)session, id, null);
			}
			return CalendarGroupEntry.BindFromCalendarFolder((MailboxSession)session, storeObjectId);
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x000859AA File Offset: 0x00083BAA
		public ICalendarGroupEntry CreateCalendarGroupEntry(IMailboxSession session, string legacyDistinguishedName, ICalendarGroup parentGroup)
		{
			return CalendarGroupEntry.Create((MailboxSession)session, legacyDistinguishedName, (CalendarGroup)parentGroup);
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x000859BE File Offset: 0x00083BBE
		public ICalendarGroupEntry CreateCalendarGroupEntry(IMailboxSession session, StoreObjectId calendarFolderId, ICalendarGroup parentGroup)
		{
			return CalendarGroupEntry.Create((MailboxSession)session, calendarFolderId, (CalendarGroup)parentGroup);
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x000859D2 File Offset: 0x00083BD2
		public CalendarGroupInfoList GetCalendarGroupsView(IMailboxSession session)
		{
			return CalendarGroup.GetCalendarGroupsView((MailboxSession)session);
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x000859DF File Offset: 0x00083BDF
		public ICalendarItemBase BindToCalendarItemBase(IStoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn = null)
		{
			return CalendarItemBase.Bind((StoreSession)session, storeId, propsToReturn);
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x000859EE File Offset: 0x00083BEE
		public ICalendarItem CreateCalendarItem(IStoreSession session, StoreId parentFolderId)
		{
			return CalendarItem.Create((StoreSession)session, parentFolderId);
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x000859FC File Offset: 0x00083BFC
		public IMeetingMessage BindToMeetingMessage(IStoreSession session, StoreId storeId)
		{
			return MeetingMessage.Bind((StoreSession)session, storeId);
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x00085A0A File Offset: 0x00083C0A
		public IMeetingRequest BindToMeetingRequestMessage(IStoreSession session, StoreId storeId)
		{
			return MeetingRequest.Bind((StoreSession)session, storeId);
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x00085A18 File Offset: 0x00083C18
		public ICalendarItemBase CreateCalendarItemSeries(IStoreSession session, StoreId parentFolderId)
		{
			return CalendarItemSeries.CreateSeries((StoreSession)session, parentFolderId, true);
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x00085A27 File Offset: 0x00083C27
		public ICalendarGroup CreateCalendarGroup(IMailboxSession session)
		{
			return CalendarGroup.Create((MailboxSession)session);
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x00085A34 File Offset: 0x00083C34
		public ICalendarGroup BindToCalendarGroup(IMailboxSession session, CalendarGroupType defaultGroupType)
		{
			return CalendarGroup.Bind((MailboxSession)session, defaultGroupType);
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x00085A42 File Offset: 0x00083C42
		public ICalendarGroup BindToCalendarGroup(IMailboxSession session, Guid groupClassId)
		{
			return CalendarGroup.Bind((MailboxSession)session, groupClassId);
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x00085A50 File Offset: 0x00083C50
		public ICalendarGroup BindToCalendarGroup(IMailboxSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn = null)
		{
			return CalendarGroup.Bind((MailboxSession)session, storeId, propsToReturn);
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x00085A5F File Offset: 0x00083C5F
		public IHierarchySyncMetadataItem GetHierarchySyncMetadataItem(IStoreSession session, IFolder folder, ICollection<PropertyDefinition> propsToReturn)
		{
			return new HierarchySyncMetadataItemHandler().GetItem(session, folder, propsToReturn);
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x00085A70 File Offset: 0x00083C70
		public IAttachment CloneAttachment(IAttachment attachment, IItem draftItem)
		{
			AttachmentType type = AttachmentType.Stream;
			if (attachment is IReferenceAttachment)
			{
				type = AttachmentType.Reference;
			}
			return draftItem.IAttachmentCollection.CreateIAttachment(type, attachment);
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x00085A96 File Offset: 0x00083C96
		public IAttachment CreateAttachment(IItem item, AttachmentType type)
		{
			return item.IAttachmentCollection.CreateIAttachment(type);
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x00085AA4 File Offset: 0x00083CA4
		public IMailboxSession ConfigurableOpenMailboxSession(ExchangePrincipal mailbox, MailboxAccessInfo accessInfo, CultureInfo cultureInfo, string clientInfoString, LogonType logonType, PropertyDefinition[] mailboxProperties, MailboxSession.InitializationFlags initFlags, IList<DefaultFolderType> foldersToInit)
		{
			return MailboxSession.ConfigurableOpen(mailbox, accessInfo, cultureInfo, clientInfoString, logonType, mailboxProperties, initFlags, foldersToInit);
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x00085AB8 File Offset: 0x00083CB8
		public T RunQueryOnAllItemsFolder<T>(IMailboxSession session, AllItemsFolderHelper.SupportedSortBy supportedSortBy, object seekToValue, T defaultValue, AllItemsFolderHelper.DoQueryProcessing<T> queryProcessor, ICollection<PropertyDefinition> properties)
		{
			return AllItemsFolderHelper.RunQueryOnAllItemsFolder<T>((MailboxSession)session, supportedSortBy, seekToValue, defaultValue, queryProcessor, properties);
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x00085ACD File Offset: 0x00083CCD
		public G RunQueryOnAllItemsFolder<G>(IMailboxSession session, AllItemsFolderHelper.SupportedSortBy supportedSortBy, AllItemsFolderHelper.DoQueryProcessing<G> queryProcessor, ICollection<PropertyDefinition> properties)
		{
			return AllItemsFolderHelper.RunQueryOnAllItemsFolder<G>((MailboxSession)session, supportedSortBy, queryProcessor, properties);
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x00085ADE File Offset: 0x00083CDE
		public IPushNotificationStorage CreatePushNotificationStorage(IMailboxSession session)
		{
			return PushNotificationStorage.Create(session, this);
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x00085AE7 File Offset: 0x00083CE7
		public IPushNotificationStorage FindPushNotificationStorage(IMailboxSession session)
		{
			return PushNotificationStorage.Find(session, this);
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x00085AF0 File Offset: 0x00083CF0
		public IPushNotificationSubscriptionItem BindToPushNotificationSubscriptionItem(IStoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			return ItemBuilder.ItemBind<PushNotificationSubscriptionItem>((StoreSession)session, storeId, PushNotificationSubscriptionItemSchema.Instance, propsToReturn);
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x00085B04 File Offset: 0x00083D04
		public IPushNotificationSubscriptionItem CreatePushNotificationSubscriptionItem(IStoreSession session, StoreId destFolderId)
		{
			return ItemBuilder.CreateNewItem<PushNotificationSubscriptionItem>((StoreSession)session, destFolderId, ItemCreateInfo.PushNotificationSubscriptionItemInfo);
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x00085B17 File Offset: 0x00083D17
		public IOutlookServiceSubscriptionItem BindToOutlookServiceSubscriptionItem(IStoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			return ItemBuilder.ItemBind<OutlookServiceSubscriptionItem>((StoreSession)session, storeId, OutlookServiceSubscriptionItemSchema.Instance, propsToReturn);
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x00085B2B File Offset: 0x00083D2B
		public IOutlookServiceSubscriptionItem CreateOutlookServiceSubscriptionItem(IStoreSession session, StoreId destFolderId)
		{
			return ItemBuilder.CreateNewItem<OutlookServiceSubscriptionItem>((StoreSession)session, destFolderId, ItemCreateInfo.OutlookServiceSubscriptionItemInfo);
		}

		// Token: 0x040013A9 RID: 5033
		public static readonly XSOFactory Default = new XSOFactory();
	}
}
