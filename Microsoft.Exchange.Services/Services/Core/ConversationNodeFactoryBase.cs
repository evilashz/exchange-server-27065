using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020003BA RID: 954
	internal abstract class ConversationNodeFactoryBase<TConversationNodeType> where TConversationNodeType : ConversationNode
	{
		// Token: 0x06001ABF RID: 6847 RVA: 0x00099404 File Offset: 0x00097604
		protected ConversationNodeFactoryBase(MailboxSession mailboxSession, ICoreConversation conversation, IParticipantResolver participantResolver, ItemResponseShape itemResponse, ICollection<PropertyDefinition> mandatoryPropertiesToLoad, ICollection<PropertyDefinition> conversationPropertiesToLoad, HashSet<PropertyDefinition> propertiesLoaded, Dictionary<StoreObjectId, HashSet<PropertyDefinition>> propertiesLoadedPerItem, bool isOwaCall)
		{
			this.itemResponseShape = itemResponse;
			this.mailboxSession = mailboxSession;
			this.conversation = conversation;
			this.propertiesLoaded = propertiesLoaded;
			this.propertiesLoadedPerItem = propertiesLoadedPerItem;
			this.mandatoryPropertiesToLoad = mandatoryPropertiesToLoad;
			this.conversationPropertiesToLoad = conversationPropertiesToLoad;
			this.isOwaCall = isOwaCall;
			this.participantResolver = participantResolver;
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x0009946C File Offset: 0x0009766C
		protected static string GetInternetMessageId(IConversationTreeNode treeNode)
		{
			return treeNode.GetValueOrDefault<string>(ItemSchema.InternetMessageId, null);
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x0009947C File Offset: 0x0009767C
		public TConversationNodeType CreateInstance(IConversationTreeNode treeNode, Func<StoreObjectId, bool> returnOnlyMandatoryProperties)
		{
			TConversationNodeType tconversationNodeType = this.CreateEmptyInstance();
			this.PopulateConversatioNodeBasicProperties(tconversationNodeType, treeNode);
			this.PopulateConversationNodeComplexProperties(tconversationNodeType, treeNode, returnOnlyMandatoryProperties);
			return tconversationNodeType;
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x000994A4 File Offset: 0x000976A4
		public ConversationNode BuildConversationNodeFromSingleNodeTree()
		{
			IConversationTreeNode rootMessageNode = this.conversation.RootMessageNode;
			ConversationNode conversationNode = new ConversationNode
			{
				InternetMessageId = rootMessageNode.GetValueOrDefault<string>(ItemSchema.InternetMessageId, null),
				IsRootNode = true
			};
			ItemResponseShape singleItemResponseShape = ConversationNodeFactoryBase<TConversationNodeType>.CreateItemResponseShapeWithNormalizedBody(this.itemResponseShape);
			foreach (IStorePropertyBag storePropertyBag in rootMessageNode.StorePropertyBags)
			{
				ItemType item = this.CreateItemForSingleNodeTree(singleItemResponseShape, storePropertyBag);
				conversationNode.AddItem(item);
			}
			return conversationNode;
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x00099544 File Offset: 0x00097744
		private void PopulateConversatioNodeBasicProperties(TConversationNodeType conversationNode, IConversationTreeNode treeNode)
		{
			conversationNode.InternetMessageId = ConversationNodeFactoryBase<TConversationNodeType>.GetInternetMessageId(treeNode);
			conversationNode.ParentInternetMessageId = ConversationNodeFactoryBase<TConversationNodeType>.GetInternetMessageId(treeNode.ParentNode);
		}

		// Token: 0x06001AC4 RID: 6852
		protected abstract TConversationNodeType CreateEmptyInstance();

		// Token: 0x06001AC5 RID: 6853 RVA: 0x00099574 File Offset: 0x00097774
		protected bool TryLoadServiceItemType(StoreObjectId storeObjectId, IStorePropertyBag storePropertyBag, bool returnOnlyMandatoryProperties, out ItemType item)
		{
			item = ItemType.CreateFromStoreObjectType(storeObjectId.ObjectType);
			string valueOrDefault = storePropertyBag.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty);
			bool flag = ObjectClass.IsOfClass(valueOrDefault, "IPM.Note.Microsoft.Approval.Request");
			bool flag2 = ObjectClass.IsEventReminderMessage(valueOrDefault);
			bool flag3 = storePropertyBag.GetValueOrDefault<byte[]>(MessageItemSchema.VotingBlob, null) != null || storePropertyBag.GetValueOrDefault<string>(MessageItemSchema.VotingResponse, null) != null;
			if (item is MeetingMessageType || !(item is MessageType) || flag || flag3 || flag2)
			{
				bool flag4 = this.itemResponseShape.AdditionalProperties != null && this.itemResponseShape.AdditionalProperties.Contains(ItemSchema.UniqueBody.PropertyPath);
				ItemResponseShape itemResponseShape = this.itemResponseShape;
				if (flag4 && this.itemResponseShape.AdditionalProperties != null)
				{
					List<PropertyPath> list = this.itemResponseShape.AdditionalProperties.ToList<PropertyPath>();
					list.Remove(ItemSchema.UniqueBody.PropertyPath);
					itemResponseShape = new ItemResponseShape(this.itemResponseShape)
					{
						AdditionalProperties = list.ToArray()
					};
				}
				bool flag5 = this.TryLoadServiceItemTypeUsingBind(storeObjectId, itemResponseShape, out item);
				if (flag4 && flag5)
				{
					item.UniqueBody = UniqueBodyProperty.GetBodyContentFromItemPart(BodyProperty.GetBodyType(this.itemResponseShape.UniqueBodyType), this.itemResponseShape, this.conversation.GetItemPart(storeObjectId));
				}
				return flag5;
			}
			return this.TryLoadServiceItemTypeUsingPropertyBag(storeObjectId, storePropertyBag, returnOnlyMandatoryProperties, out item);
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x000996D4 File Offset: 0x000978D4
		protected virtual void PopulateConversationNodeComplexProperties(TConversationNodeType conversationNode, IConversationTreeNode treeNode, Func<StoreObjectId, bool> returnOnlyMandatoryProperties)
		{
			foreach (IStorePropertyBag storePropertyBag in treeNode.StorePropertyBags)
			{
				StoreObjectId storeObjectId = ConversationDataConverter.GetStoreObjectId(storePropertyBag);
				ItemType itemType;
				if (this.TryLoadServiceItemType(storeObjectId, storePropertyBag, returnOnlyMandatoryProperties(storeObjectId), out itemType))
				{
					if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2012))
					{
						EWSSettings.SetInlineAttachmentFlags(itemType);
					}
					conversationNode.AddItem(itemType);
				}
			}
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x00099760 File Offset: 0x00097960
		private byte[] GetInstanceKeyProperty(ItemPart itemPart)
		{
			byte[] array = itemPart.StorePropertyBag.GetValueOrDefault<byte[]>(ItemSchema.InstanceKey, null);
			if (array != null)
			{
				return array;
			}
			object obj = itemPart.StorePropertyBag.TryGetProperty(ItemSchema.Fid);
			object obj2 = itemPart.StorePropertyBag.TryGetProperty(MessageItemSchema.MID);
			if (obj != null && !(obj is PropertyError) && obj2 != null && !(obj2 is PropertyError))
			{
				byte[] bytes = BitConverter.GetBytes((long)obj);
				byte[] bytes2 = BitConverter.GetBytes((long)obj2);
				int value = 0;
				array = new byte[bytes.Length + bytes2.Length + BitConverter.GetBytes(value).Length];
				Array.Copy(bytes, 0, array, 0, bytes.Length);
				Array.Copy(bytes2, 0, array, bytes.Length, bytes2.Length);
				return array;
			}
			return null;
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x00099810 File Offset: 0x00097A10
		private RightsManagementLicenseDataType LoadRightsManagementLicenseData(ItemPart itemPart)
		{
			RightsManagementLicenseDataType result = null;
			if (this.itemResponseShape.ClientSupportsIrm)
			{
				RightsManagementLicenseDataProperty.LoadRightsManagementLicenseData(itemPart, out result);
			}
			return result;
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x00099838 File Offset: 0x00097A38
		private RetentionTagType LoadRetentionTag(ItemPart itemPart, bool isArchive)
		{
			RetentionTagType result = null;
			PropertyDefinition propertyDefinition = isArchive ? StoreObjectSchema.ArchiveTag : StoreObjectSchema.PolicyTag;
			byte[] valueOrDefault = itemPart.StorePropertyBag.GetValueOrDefault<byte[]>(propertyDefinition, null);
			if (valueOrDefault != null)
			{
				Guid guid = new Guid(valueOrDefault);
				PropertyDefinition propertyDefinition2 = isArchive ? StoreObjectSchema.ArchivePeriod : StoreObjectSchema.RetentionPeriod;
				object valueOrDefault2 = itemPart.StorePropertyBag.GetValueOrDefault<object>(propertyDefinition2, null);
				bool isExplicit = valueOrDefault2 != null;
				result = new RetentionTagType
				{
					IsExplicit = isExplicit,
					Value = guid.ToString()
				};
			}
			return result;
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x000998C4 File Offset: 0x00097AC4
		private RecipientCountsType GetRecipientCounts(ItemPart itemPart)
		{
			return new RecipientCountsType
			{
				ToRecipientsCount = itemPart.Recipients[RecipientItemType.To].Count<IParticipant>(),
				CcRecipientsCount = itemPart.Recipients[RecipientItemType.Cc].Count<IParticipant>(),
				BccRecipientsCount = itemPart.Recipients[RecipientItemType.Bcc].Count<IParticipant>()
			};
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x0009991D File Offset: 0x00097B1D
		private void SetPropertyIfRequested(ItemType item, PropertyInformation property, HashSet<PropertyPath> requestedProperties, Func<object> propGetterFunc)
		{
			if (requestedProperties.Contains(property.PropertyPath))
			{
				item.PropertyBag[property] = propGetterFunc();
			}
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x00099940 File Offset: 0x00097B40
		private bool? GetItemBlockStatus(ItemPart itemPart)
		{
			return new bool?(Util.GetItemBlockStatus(this.participantResolver, itemPart, this.itemResponseShape.BlockExternalImages, this.itemResponseShape.BlockExternalImagesIfSenderUntrusted));
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x00099A14 File Offset: 0x00097C14
		private void SetRecipientProperties(ItemType item, ItemPart itemPart, HashSet<PropertyPath> requestedProperties)
		{
			int maxRecipients = (this.itemResponseShape.MaximumRecipientsToReturn > 0) ? this.itemResponseShape.MaximumRecipientsToReturn : int.MaxValue;
			using (Dictionary<PropertyInformation, RecipientItemType>.Enumerator enumerator = ConversationNodeFactoryBase<TConversationNodeType>.recipientPropertyMap.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<PropertyInformation, RecipientItemType> kvPair = enumerator.Current;
					KeyValuePair<PropertyInformation, RecipientItemType> kvPair3 = kvPair;
					this.SetPropertyIfRequested(item, kvPair3.Key, requestedProperties, delegate
					{
						IParticipantResolver participantResolver = this.participantResolver;
						ParticipantTable recipients = itemPart.Recipients;
						KeyValuePair<PropertyInformation, RecipientItemType> kvPair2 = kvPair;
						return participantResolver.ResolveToEmailAddressWrapper(recipients[kvPair2.Value].Take(maxRecipients));
					});
				}
			}
			this.SetPropertyIfRequested(item, MessageSchema.RecipientCounts, requestedProperties, () => this.GetRecipientCounts(itemPart));
			string valueOrDefault = itemPart.StorePropertyBag.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty);
			foreach (KeyValuePair<PropertyInformation, PropertyDefinition> keyValuePair in ConversationNodeFactoryBase<TConversationNodeType>.singleRecipientPropertyMap)
			{
				if ((keyValuePair.Value != MessageItemSchema.ReceivedBy && keyValuePair.Value != MessageItemSchema.ReceivedRepresenting) || ObjectClass.IsMeetingMessage(valueOrDefault))
				{
					SingleRecipientType singleRecipient = this.participantResolver.ResolveToSingleRecipientType(itemPart.StorePropertyBag.GetValueOrDefault<IParticipant>(keyValuePair.Value, null));
					this.SetPropertyIfRequested(item, keyValuePair.Key, requestedProperties, () => singleRecipient);
				}
			}
			if (itemPart.StorePropertyBag.GetValueOrDefault<bool>(MessageItemSchema.ReplyToNamesExists, false) || itemPart.StorePropertyBag.GetValueOrDefault<bool>(MessageItemSchema.ReplyToBlobExists, false))
			{
				this.SetPropertyIfRequested(item, MessageSchema.ReplyTo, requestedProperties, () => this.participantResolver.ResolveToEmailAddressWrapper(itemPart.ReplyToParticipants));
			}
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x00099C24 File Offset: 0x00097E24
		private ResponseObjectType[] GetResponseObjects(StoreObjectId storeObjectId, ItemType item, IStorePropertyBag storePropertyBag)
		{
			List<ResponseObjectType> list = new List<ResponseObjectType>();
			MessageType messageType = item as MessageType;
			MeetingMessageType meetingMessageType = item as MeetingMessageType;
			PostItemType postItemType = item as PostItemType;
			if (messageType != null)
			{
				this.GetMessageResponseObjects(storeObjectId, storePropertyBag, list);
			}
			else if (meetingMessageType != null)
			{
				this.GetMeetingMessageResponseObjects(storeObjectId, meetingMessageType, storePropertyBag, list);
			}
			else if (postItemType != null)
			{
				this.GetPostItemResponseObjects(list);
			}
			if (list.Count <= 0)
			{
				return null;
			}
			return list.ToArray();
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x00099C84 File Offset: 0x00097E84
		private void GetMessageResponseObjects(StoreObjectId storeObjectId, IStorePropertyBag storePropertyBag, List<ResponseObjectType> responseObjects)
		{
			if (!storePropertyBag.GetValueOrDefault<bool>(MessageItemSchema.IsDraft, false) && storeObjectId.ObjectType != StoreObjectType.Report)
			{
				responseObjects.Add(new ReplyToItemType());
				responseObjects.Add(new ReplyAllToItemType());
			}
			responseObjects.Add(new ForwardItemType());
			bool valueOrDefault = storePropertyBag.GetValueOrDefault<bool>(MessageItemSchema.IsReadReceiptPending, false);
			if (valueOrDefault)
			{
				StoreId valueOrDefault2 = storePropertyBag.GetValueOrDefault<StoreId>(StoreObjectSchema.ParentItemId, null);
				if (valueOrDefault2 != null && this.mailboxSession.IsDefaultFolderType(valueOrDefault2) != DefaultFolderType.JunkEmail)
				{
					responseObjects.Add(new SuppressReadReceiptType());
				}
			}
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x00099D04 File Offset: 0x00097F04
		private void GetMeetingMessageResponseObjects(StoreObjectId storeObjectId, MeetingMessageType meetingMessage, IStorePropertyBag storePropertyBag, List<ResponseObjectType> responseObjects)
		{
			this.GetMessageResponseObjects(storeObjectId, storePropertyBag, responseObjects);
			bool valueOrDefault = storePropertyBag.GetValueOrDefault<bool>(CalendarItemBaseSchema.IsOrganizer, false);
			if (meetingMessage is MeetingCancellationMessageType)
			{
				if (!valueOrDefault)
				{
					responseObjects.Add(new RemoveItemType());
					return;
				}
			}
			else if (meetingMessage is MeetingRequestMessageType)
			{
				bool valueOrDefault2 = storePropertyBag.GetValueOrDefault<bool>(MeetingMessageSchema.IsOutOfDate, false);
				bool flag = this.mailboxSession.IsGroupMailbox();
				if (flag && !valueOrDefault2)
				{
					responseObjects.Add(new AddItemToMyCalendarType());
					return;
				}
				if (!valueOrDefault && !valueOrDefault2)
				{
					responseObjects.Add(new AcceptItemType());
					responseObjects.Add(new TentativelyAcceptItemType());
					responseObjects.Add(new DeclineItemType());
				}
			}
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x00099D9E File Offset: 0x00097F9E
		private void GetPostItemResponseObjects(List<ResponseObjectType> responseObjects)
		{
			responseObjects.Add(new PostReplyItemType());
			responseObjects.Add(new ReplyToItemType());
			responseObjects.Add(new ReplyAllToItemType());
			responseObjects.Add(new ForwardItemType());
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x00099DCC File Offset: 0x00097FCC
		private IDictionary<PropertyDefinition, object> CreateEwsPropertyBag(IStorePropertyBag storePropertyBag, bool returnOnlyMandatoryProperties)
		{
			ICollection<PropertyDefinition> collection;
			if (returnOnlyMandatoryProperties)
			{
				collection = this.mandatoryPropertiesToLoad;
			}
			else
			{
				collection = this.conversationPropertiesToLoad;
			}
			Dictionary<PropertyDefinition, object> dictionary = new Dictionary<PropertyDefinition, object>();
			foreach (PropertyDefinition propertyDefinition in collection)
			{
				if (!dictionary.ContainsKey(propertyDefinition))
				{
					object obj = storePropertyBag.TryGetProperty(propertyDefinition);
					if (obj != null && !(obj is PropertyError))
					{
						dictionary.Add(propertyDefinition, obj);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x00099E50 File Offset: 0x00098050
		private ToServiceObjectForPropertyBagPropertyList GetPropertyListForItem(StoreObjectId storeObjectId)
		{
			ObjectInformation objectInformation = Schema.GetObjectInformation(storeObjectId.ObjectType);
			return XsoDataConverter.GetServiceObjectPropertyListForPropertyBag(this.itemResponseShape, objectInformation);
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x00099E78 File Offset: 0x00098078
		private bool TryLoadServiceItemTypeUsingPropertyBag(StoreObjectId storeObjectId, IStorePropertyBag storePropertyBag, bool returnOnlyMandatoryProperties, out ItemType item)
		{
			IdAndSession idAndSession = new IdAndSession(storeObjectId, this.mailboxSession);
			item = ItemType.CreateFromStoreObjectType(storeObjectId.ObjectType);
			bool result;
			try
			{
				ToServiceObjectForPropertyBagPropertyList propertyListForItem = this.GetPropertyListForItem(storeObjectId);
				ToServiceObjectPropertyList propertyListForItem2 = this.GetPropertyListForItem(storeObjectId, this.itemResponseShape);
				HashSet<PropertyPath> properties = propertyListForItem.GetProperties();
				HashSet<PropertyPath> properties2 = propertyListForItem2.GetProperties();
				List<PropertyPath> list = properties2.Except(properties).Except(ConversationNodeFactoryBase<TConversationNodeType>.specialProperties).Except(ConversationNodeFactoryBase<TConversationNodeType>.reminderMessageProperties).Except(ConversationNodeFactoryBase<TConversationNodeType>.approvalAndVotingProperties).ToList<PropertyPath>();
				IDictionary<PropertyDefinition, object> propertyBag = this.CreateEwsPropertyBag(storePropertyBag, returnOnlyMandatoryProperties);
				item.ContainsOnlyMandatoryProperties = returnOnlyMandatoryProperties;
				propertyListForItem.ConvertPropertiesToServiceObject(item, propertyBag, idAndSession);
				this.SetSpecialProperties(storeObjectId, item, properties2);
				if (list.Count == 0)
				{
					result = true;
				}
				else
				{
					ItemResponseShape itemResponseShape = new ItemResponseShape(this.itemResponseShape)
					{
						BaseShape = ShapeEnum.IdOnly,
						AdditionalProperties = list.ToArray()
					};
					ExTraceGlobals.ItemAlgorithmTracer.TraceWarning<string, int>((long)this.GetHashCode(), "Item of type {0} requires an additional bind for {1} properties. This should have been handled already.", item.GetType().Name, list.Count);
					ToServiceObjectPropertyList propertyListForItem3 = this.GetPropertyListForItem(storeObjectId, itemResponseShape);
					this.LoadItemProperties(item, idAndSession, itemResponseShape, propertyListForItem3);
					result = true;
				}
			}
			catch (ObjectNotFoundException)
			{
				item = null;
				result = false;
			}
			return result;
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x00099FB8 File Offset: 0x000981B8
		private bool TryLoadServiceItemTypeUsingBind(StoreObjectId storeObjectId, ItemResponseShape itemResponseShape, out ItemType item)
		{
			IdAndSession itemIdAndSession = new IdAndSession(storeObjectId, this.mailboxSession);
			item = ItemType.CreateFromStoreObjectType(storeObjectId.ObjectType);
			ToServiceObjectPropertyList propertyListForItem = this.GetPropertyListForItem(storeObjectId, itemResponseShape);
			bool result;
			try
			{
				this.LoadItemProperties(item, itemIdAndSession, this.itemResponseShape, propertyListForItem);
				result = true;
			}
			catch (ObjectNotFoundException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0009A014 File Offset: 0x00098214
		private void LoadItemProperties(ItemType item, IdAndSession itemIdAndSession, ItemResponseShape itemResponseShape, ToServiceObjectPropertyList itemPropertyList)
		{
			using (Item rootXsoItem = itemIdAndSession.GetRootXsoItem(itemPropertyList.GetPropertyDefinitions()))
			{
				if (itemResponseShape.ClientSupportsIrm)
				{
					IrmUtils.DecodeIrmMessage((MailboxSession)itemIdAndSession.Session, rootXsoItem, false);
				}
				ServiceCommandBase.LoadServiceObject(item, rootXsoItem, itemIdAndSession, itemResponseShape, itemPropertyList);
				bool flag = itemPropertyList.GetProperties().Contains(ItemSchema.Preview.PropertyPath);
				if (flag && IrmUtils.IsMessageRestrictedAndDecoded(rootXsoItem))
				{
					item.Preview = IrmUtils.GetItemPreview(rootXsoItem);
				}
				if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2012))
				{
					EWSSettings.SetInlineAttachmentFlags(item);
				}
			}
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x0009A0B8 File Offset: 0x000982B8
		private static byte[] GetConversationIndex(IStorePropertyBag storeObjectPropertyBag)
		{
			return storeObjectPropertyBag.GetValueOrDefault<byte[]>(ItemSchema.ConversationIndex, null);
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x0009A2F8 File Offset: 0x000984F8
		private void SetSpecialProperties(StoreObjectId storeObjectId, ItemType item, HashSet<PropertyPath> requestedProperties)
		{
			ItemPart itemPart = this.conversation.GetItemPart(storeObjectId);
			if (itemPart == null)
			{
				return;
			}
			this.SetPropertyIfRequested(item, ItemSchema.ConversationId, requestedProperties, () => ConversationDataConverter.GetConversationId(itemPart, this.mailboxSession, ItemSchema.ConversationId));
			if (item.ContainsOnlyMandatoryProperties)
			{
				return;
			}
			this.SetPropertyIfRequested(item, ItemSchema.IsDraft, requestedProperties, () => itemPart.StorePropertyBag.GetValueOrDefault<bool>(MessageItemSchema.IsDraft, false));
			this.SetPropertyIfRequested(item, ItemSchema.Preview, requestedProperties, () => this.GetItemPreview(itemPart, item));
			this.SetPropertyIfRequested(item, ItemSchema.DateTimeCreated, requestedProperties, () => ConversationDataConverter.GetDatetimeProperty(itemPart, StoreObjectSchema.CreationTime));
			this.SetPropertyIfRequested(item, ItemSchema.DateTimeSent, requestedProperties, () => ConversationDataConverter.GetDatetimeProperty(itemPart, ItemSchema.SentTime));
			this.SetPropertyIfRequested(item, ItemSchema.RetentionDate, requestedProperties, () => ConversationDataConverter.GetDatetimeProperty(itemPart, ItemSchema.RetentionDate));
			this.SetPropertyIfRequested(item, ItemSchema.InstanceKey, requestedProperties, () => this.GetInstanceKeyProperty(itemPart));
			this.SetPropertyIfRequested(item, MessageSchema.ConversationIndex, requestedProperties, () => ConversationNodeFactoryBase<TConversationNodeType>.GetConversationIndex(itemPart.StorePropertyBag));
			this.SetPropertyIfRequested(item, MessageSchema.IsReadReceiptRequested, requestedProperties, () => itemPart.StorePropertyBag.GetValueOrDefault<bool>(MessageItemSchema.IsReadReceiptRequested, false));
			this.SetPropertyIfRequested(item, MessageSchema.IsDeliveryReceiptRequested, requestedProperties, () => itemPart.StorePropertyBag.GetValueOrDefault<bool>(MessageItemSchema.IsDeliveryReceiptRequested, false));
			this.SetPropertyIfRequested(item, ItemSchema.IsClutter, requestedProperties, () => IsClutterProperty.GetFlagValueOrDefaultFromStorePropertyBag(itemPart.StorePropertyBag, this.itemResponseShape));
			this.SetPropertyIfRequested(item, ItemSchema.BlockStatus, requestedProperties, () => this.GetItemBlockStatus(itemPart));
			this.SetPropertyIfRequested(item, ItemSchema.UniqueBody, requestedProperties, () => this.GetItemUniqueBodyContext(itemPart, item));
			this.SetPropertyIfRequested(item, ItemSchema.HasBlockedImages, requestedProperties, () => EWSSettings.ItemHasBlockedImages != null && EWSSettings.ItemHasBlockedImages.Value);
			if (!itemPart.IsExtractedPart)
			{
				this.SetPropertyIfRequested(item, ItemSchema.EntityExtractionResult, requestedProperties, () => EntityExtractionResultProperty.Render(itemPart, this.mailboxSession.ExTimeZone));
			}
			this.SetPropertyIfRequested(item, ItemSchema.Attachments, requestedProperties, () => ConversationDataConverter.GetAttachments(itemPart, new IdAndSession(storeObjectId, this.mailboxSession)));
			this.SetPropertyIfRequested(item, ItemSchema.ResponseObjects, requestedProperties, () => this.GetResponseObjects(storeObjectId, item, itemPart.StorePropertyBag));
			this.SetPropertyIfRequested(item, MeetingRequestSchema.ChangeHighlights, requestedProperties, () => ChangeHighlightsProperty.Render(itemPart));
			this.SetPropertyIfRequested(item, ItemSchema.Flag, requestedProperties, () => FlagProperty.GetFlagForItemPart(itemPart));
			this.SetPropertyIfRequested(item, ItemSchema.RightsManagementLicenseData, requestedProperties, () => this.LoadRightsManagementLicenseData(itemPart));
			this.SetPropertyIfRequested(item, ItemSchema.ArchiveTag, requestedProperties, () => this.LoadRetentionTag(itemPart, true));
			this.SetPropertyIfRequested(item, ItemSchema.PolicyTag, requestedProperties, () => this.LoadRetentionTag(itemPart, false));
			if (requestedProperties.Contains(ItemSchema.EntityExtractionResult.PropertyPath))
			{
				item.PropertyExistence = PropertyExistenceProperty.GetValueFromStorePropertyBag(itemPart.StorePropertyBag);
			}
			this.SetRecipientProperties(item, itemPart, requestedProperties);
			this.SetExtendedProperties(item, itemPart);
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0009A659 File Offset: 0x00098859
		private BodyContentType GetItemUniqueBodyContext(ItemPart itemPart, ItemType item)
		{
			if (!itemPart.DidLoadSucceed)
			{
				item.AddPropertyError(new PropertyUri(PropertyUriEnum.UniqueBody), PropertyErrorCodeType.NotEnoughMemory);
				return null;
			}
			return UniqueBodyProperty.GetBodyContentFromItemPart(BodyProperty.GetBodyType(this.itemResponseShape.UniqueBodyType), this.itemResponseShape, itemPart);
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x0009A690 File Offset: 0x00098890
		private string GetItemPreview(ItemPart itemPart, ItemType item)
		{
			bool didLoadSucceed = itemPart.DidLoadSucceed;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<bool>(4177931581U, ref didLoadSucceed);
			if (!didLoadSucceed)
			{
				item.AddPropertyError(new PropertyUri(PropertyUriEnum.Preview), PropertyErrorCodeType.NotEnoughMemory);
				return null;
			}
			PropertyDefinition preview = ItemSchema.Preview;
			object obj = itemPart.StorePropertyBag.TryGetProperty(preview);
			if (obj == null)
			{
				return null;
			}
			string text = obj as string;
			if (text != null)
			{
				return text;
			}
			PropertyError propertyError = obj as PropertyError;
			if (propertyError != null && propertyError.PropertyErrorCode != PropertyErrorCode.NotFound)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<PropertyErrorCode, string>((long)this.GetHashCode(), "[GetConversationItems::GetItemPreview] encountered property error {0} for preview property: {1}", propertyError.PropertyErrorCode, propertyError.PropertyErrorDescription);
				item.AddPropertyError(new PropertyUri(PropertyUriEnum.Preview), (PropertyErrorCodeType)propertyError.PropertyErrorCode);
			}
			return null;
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x0009A73C File Offset: 0x0009893C
		private void SetExtendedProperties(ItemType item, ItemPart itemPart)
		{
			if (this.itemResponseShape.AdditionalProperties == null || this.itemResponseShape.AdditionalProperties.Length == 0)
			{
				return;
			}
			IStorePropertyBag storePropertyBag = itemPart.StorePropertyBag;
			foreach (ExtendedPropertyUri extendedPropertyUri in this.itemResponseShape.AdditionalProperties.OfType<ExtendedPropertyUri>())
			{
				PropertyDefinition propertyDefinition = extendedPropertyUri.ToPropertyDefinition();
				if (!item.ContainsExtendedProperty(extendedPropertyUri) && (this.propertiesLoaded.Contains(propertyDefinition) || (this.propertiesLoadedPerItem.ContainsKey(itemPart.ItemId) && this.propertiesLoadedPerItem[itemPart.ItemId].Contains(propertyDefinition))))
				{
					object obj = storePropertyBag.TryGetProperty(propertyDefinition);
					if (obj != null && !(obj is PropertyError))
					{
						ExtendedPropertyType extendedPropertyForValues = ExtendedPropertyProperty.GetExtendedPropertyForValues(extendedPropertyUri, propertyDefinition, obj);
						if (extendedPropertyForValues != null)
						{
							item.AddExtendedPropertyValue(extendedPropertyForValues);
						}
					}
				}
			}
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x0009A82C File Offset: 0x00098A2C
		private ToServiceObjectPropertyList GetPropertyListForItem(StoreObjectId id, ItemResponseShape itemResponseShape)
		{
			ToServiceObjectPropertyList toServiceObjectPropertyList = XsoDataConverter.GetToServiceObjectPropertyList(id, this.mailboxSession, itemResponseShape, this.participantResolver);
			toServiceObjectPropertyList.CharBuffer = this.charBuffer;
			return toServiceObjectPropertyList;
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x0009A85C File Offset: 0x00098A5C
		private ItemType CreateItemForSingleNodeTree(ItemResponseShape singleItemResponseShape, IStorePropertyBag storePropertyBag)
		{
			VersionedId versionedId = (VersionedId)storePropertyBag.TryGetProperty(ItemSchema.Id);
			StoreObjectId objectId = versionedId.ObjectId;
			ToServiceObjectPropertyList propertyListForItem = this.GetPropertyListForItem(objectId, singleItemResponseShape);
			IdAndSession itemIdAndSession = new IdAndSession(objectId, this.mailboxSession);
			ItemType itemType = ItemType.CreateFromStoreObjectType(objectId.ObjectType);
			this.LoadItemProperties(itemType, itemIdAndSession, singleItemResponseShape, propertyListForItem);
			itemType.UniqueBody = itemType.NormalizedBody;
			itemType.NormalizedBody = null;
			return itemType;
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x0009A8C8 File Offset: 0x00098AC8
		private static ItemResponseShape CreateItemResponseShapeWithNormalizedBody(ItemResponseShape responseShape)
		{
			responseShape = new ItemResponseShape(responseShape);
			for (int i = 0; i < responseShape.AdditionalProperties.Length; i++)
			{
				PropertyUri obj = responseShape.AdditionalProperties[i] as PropertyUri;
				if (ConversationNodeFactoryBase<TConversationNodeType>.uniqueBodyPropertyUri.Equals(obj))
				{
					responseShape.AdditionalProperties[i] = ConversationNodeFactoryBase<TConversationNodeType>.normalizedBodyPropertyUri;
					responseShape.BodyType = responseShape.UniqueBodyType;
				}
			}
			return responseShape;
		}

		// Token: 0x04001199 RID: 4505
		private static PropertyPath[] specialProperties = new PropertyPath[]
		{
			ItemSchema.BlockStatus.PropertyPath,
			ItemSchema.ConversationId.PropertyPath,
			ItemSchema.HasBlockedImages.PropertyPath,
			ItemSchema.UniqueBody.PropertyPath,
			ItemSchema.EntityExtractionResult.PropertyPath,
			MessageSchema.From.PropertyPath,
			MessageSchema.Sender.PropertyPath,
			MessageSchema.ReplyTo.PropertyPath,
			MessageSchema.ToRecipients.PropertyPath,
			MessageSchema.CcRecipients.PropertyPath,
			MessageSchema.BccRecipients.PropertyPath,
			ItemSchema.Attachments.PropertyPath,
			ItemSchema.ResponseObjects.PropertyPath,
			MeetingRequestSchema.ChangeHighlights.PropertyPath,
			ItemSchema.RightsManagementLicenseData.PropertyPath,
			ItemSchema.IsClutter.PropertyPath,
			MessageSchema.RecipientCounts.PropertyPath
		};

		// Token: 0x0400119A RID: 4506
		private static PropertyPath[] approvalAndVotingProperties = new PropertyPath[]
		{
			MessageSchema.ApprovalRequestData.PropertyPath,
			MessageSchema.VotingInformation.PropertyPath
		};

		// Token: 0x0400119B RID: 4507
		private static PropertyPath[] reminderMessageProperties = new PropertyPath[]
		{
			MessageSchema.ReminderMessageData.PropertyPath
		};

		// Token: 0x0400119C RID: 4508
		private static Dictionary<PropertyInformation, RecipientItemType> recipientPropertyMap = new Dictionary<PropertyInformation, RecipientItemType>
		{
			{
				MessageSchema.ToRecipients,
				RecipientItemType.To
			},
			{
				MessageSchema.CcRecipients,
				RecipientItemType.Cc
			},
			{
				MessageSchema.BccRecipients,
				RecipientItemType.Bcc
			}
		};

		// Token: 0x0400119D RID: 4509
		private static Dictionary<PropertyInformation, PropertyDefinition> singleRecipientPropertyMap = new Dictionary<PropertyInformation, PropertyDefinition>
		{
			{
				MessageSchema.From,
				ItemSchema.From
			},
			{
				MessageSchema.Sender,
				ItemSchema.Sender
			},
			{
				MessageSchema.ReceivedBy,
				MessageItemSchema.ReceivedBy
			},
			{
				MessageSchema.ReceivedRepresenting,
				MessageItemSchema.ReceivedRepresenting
			}
		};

		// Token: 0x0400119E RID: 4510
		private static PropertyUri uniqueBodyPropertyUri = new PropertyUri(PropertyUriEnum.UniqueBody);

		// Token: 0x0400119F RID: 4511
		private static PropertyUri normalizedBodyPropertyUri = new PropertyUri(PropertyUriEnum.NormalizedBody);

		// Token: 0x040011A0 RID: 4512
		private readonly ItemResponseShape itemResponseShape;

		// Token: 0x040011A1 RID: 4513
		private readonly bool isOwaCall;

		// Token: 0x040011A2 RID: 4514
		private readonly ICoreConversation conversation;

		// Token: 0x040011A3 RID: 4515
		private readonly MailboxSession mailboxSession;

		// Token: 0x040011A4 RID: 4516
		private readonly ICollection<PropertyDefinition> mandatoryPropertiesToLoad;

		// Token: 0x040011A5 RID: 4517
		private readonly ICollection<PropertyDefinition> conversationPropertiesToLoad;

		// Token: 0x040011A6 RID: 4518
		private readonly IParticipantResolver participantResolver;

		// Token: 0x040011A7 RID: 4519
		private char[] charBuffer = new char[32768];

		// Token: 0x040011A8 RID: 4520
		private HashSet<PropertyDefinition> propertiesLoaded;

		// Token: 0x040011A9 RID: 4521
		private Dictionary<StoreObjectId, HashSet<PropertyDefinition>> propertiesLoadedPerItem;
	}
}
