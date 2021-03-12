using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200084E RID: 2126
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class ItemBuilder
	{
		// Token: 0x06004F27 RID: 20263 RVA: 0x0014B85C File Offset: 0x00149A5C
		internal static T CreateNewItem<T>(StoreSession session, StoreId parentFolderId, ItemCreateInfo itemCreateInfo) where T : Item
		{
			return ItemBuilder.CreateNewItem<T>(session, parentFolderId, itemCreateInfo, CreateMessageType.Normal);
		}

		// Token: 0x06004F28 RID: 20264 RVA: 0x0014B888 File Offset: 0x00149A88
		internal static T CreateNewItem<T>(StoreSession session, StoreId parentFolderId, ItemCreateInfo itemCreateInfo, CreateMessageType createMessageType) where T : Item
		{
			return ItemBuilder.CreateNewItem<T>(session, itemCreateInfo, () => Folder.InternalCreateMapiMessage(session, parentFolderId, createMessageType));
		}

		// Token: 0x06004F29 RID: 20265 RVA: 0x0014B8C8 File Offset: 0x00149AC8
		internal static T CreateNewItem<T>(StoreSession session, ItemCreateInfo itemCreateInfo, ItemBuilder.MapiMessageCreator mapiMessageCreator) where T : Item
		{
			T t = default(T);
			CoreItem coreItem = null;
			bool flag = false;
			T result;
			try
			{
				coreItem = ItemBuilder.CreateNewCoreItem(session, itemCreateInfo, true, mapiMessageCreator);
				t = (T)((object)itemCreateInfo.Creator(coreItem));
				flag = true;
				result = t;
			}
			finally
			{
				if (!flag)
				{
					Util.DisposeIfPresent(t);
					Util.DisposeIfPresent(coreItem);
				}
			}
			return result;
		}

		// Token: 0x06004F2A RID: 20266 RVA: 0x0014B92C File Offset: 0x00149B2C
		internal static CoreItem CreateNewCoreItem(StoreSession session, ItemCreateInfo itemCreateInfo, bool useAcr, ItemBuilder.MapiMessageCreator mapiMessageCreator)
		{
			return ItemBuilder.CreateNewCoreItem(session, itemCreateInfo, null, useAcr, mapiMessageCreator);
		}

		// Token: 0x06004F2B RID: 20267 RVA: 0x0014B938 File Offset: 0x00149B38
		internal static CoreItem CreateNewCoreItem(StoreSession session, ItemCreateInfo itemCreateInfo, VersionedId itemId, bool useAcr, ItemBuilder.MapiMessageCreator mapiMessageCreator)
		{
			PersistablePropertyBag persistablePropertyBag = null;
			CoreItem coreItem = null;
			bool flag = false;
			StoreObjectId storeObjectId = null;
			byte[] changeKey = null;
			Origin origin = Origin.New;
			CoreItem result;
			try
			{
				persistablePropertyBag = ItemBuilder.ConstructItemPersistablePropertyBag(session, itemCreateInfo.Schema.AutoloadProperties, useAcr, itemCreateInfo.AcrProfile, mapiMessageCreator);
				if (itemId != null)
				{
					object obj = persistablePropertyBag.TryGetProperty(CoreItemSchema.ReadCnNew);
					if (obj is byte[] && ((byte[])obj).Length > 0)
					{
						changeKey = itemId.ChangeKeyAsByteArray();
						storeObjectId = itemId.ObjectId;
						origin = Origin.Existing;
					}
				}
				coreItem = new CoreItem(session, persistablePropertyBag, storeObjectId, changeKey, origin, ItemLevel.TopLevel, itemCreateInfo.Schema.AutoloadProperties, ItemBindOption.None);
				flag = true;
				result = coreItem;
			}
			finally
			{
				if (!flag)
				{
					Util.DisposeIfPresent(coreItem);
					Util.DisposeIfPresent(persistablePropertyBag);
				}
			}
			return result;
		}

		// Token: 0x06004F2C RID: 20268 RVA: 0x0014B9E8 File Offset: 0x00149BE8
		internal static T ConstructItem<T>(StoreSession session, StoreObjectId id, byte[] changeKey, ICollection<PropertyDefinition> propertiesToLoad, ItemBuilder.PropertyBagCreator propertyBagCreator, ItemCreateInfo.ItemCreator creator, Origin origin, ItemLevel itemLevel) where T : Item
		{
			PersistablePropertyBag persistablePropertyBag = null;
			CoreItem coreItem = null;
			T t = default(T);
			bool flag = false;
			T result;
			try
			{
				persistablePropertyBag = propertyBagCreator();
				coreItem = new CoreItem(session, persistablePropertyBag, id, changeKey, origin, itemLevel, propertiesToLoad, ItemBindOption.None);
				t = (T)((object)creator(coreItem));
				flag = true;
				result = t;
			}
			finally
			{
				if (!flag)
				{
					Util.DisposeIfPresent(t);
					Util.DisposeIfPresent(coreItem);
					Util.DisposeIfPresent(persistablePropertyBag);
				}
			}
			return result;
		}

		// Token: 0x06004F2D RID: 20269 RVA: 0x0014BA60 File Offset: 0x00149C60
		internal static T ItemBind<T>(StoreSession session, StoreId id, Schema expectedSchema, ICollection<PropertyDefinition> propertiesToLoad) where T : Item
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(id, "id");
			Util.ThrowOnNullArgument(expectedSchema, "expectedSchema");
			return ItemBuilder.ItemBind<T>(session, id, expectedSchema, null, ItemBindOption.None, propertiesToLoad);
		}

		// Token: 0x06004F2E RID: 20270 RVA: 0x0014BA90 File Offset: 0x00149C90
		internal static T ItemBind<T>(StoreSession session, StoreId storeId, Schema expectedSchema, ItemBuilder.MapiMessageCreator mapiMessageCreator, ItemBindOption itemBindOption, ICollection<PropertyDefinition> propertiesToLoad) where T : Item
		{
			propertiesToLoad = ItemBuilder.GetPropertiesToLoad(itemBindOption, expectedSchema, propertiesToLoad);
			bool flag = false;
			CoreItem coreItem = null;
			Item item = null;
			T t = default(T);
			T result;
			try
			{
				StoreObjectType storeObjectType = StoreObjectType.Unknown;
				coreItem = ItemBuilder.CoreItemBind(session, storeId, mapiMessageCreator, itemBindOption, propertiesToLoad, ref storeObjectType);
				ItemCreateInfo itemCreateInfo = ItemCreateInfo.GetItemCreateInfo(storeObjectType);
				item = itemCreateInfo.Creator(coreItem);
				t = item.DownCastStoreObject<T>();
				flag = true;
				result = t;
			}
			finally
			{
				if (!flag)
				{
					Util.DisposeIfPresent(t);
					Util.DisposeIfPresent(item);
					Util.DisposeIfPresent(coreItem);
				}
			}
			return result;
		}

		// Token: 0x06004F2F RID: 20271 RVA: 0x0014BB1C File Offset: 0x00149D1C
		internal static MessageItem ItemBindAsMessage(StoreSession session, StoreId storeId, ItemBuilder.MapiMessageCreator mapiMessageCreator, ItemBindOption itemBindOption, ICollection<PropertyDefinition> propertiesToLoad)
		{
			ItemCreateInfo messageItemInfo = ItemCreateInfo.MessageItemInfo;
			propertiesToLoad = ItemBuilder.GetPropertiesToLoad(itemBindOption, messageItemInfo.Schema, propertiesToLoad);
			CoreItem coreItem = null;
			MessageItem messageItem = null;
			bool flag = false;
			MessageItem result;
			try
			{
				StoreObjectType storeObjectType = StoreObjectType.Message;
				coreItem = ItemBuilder.CoreItemBind(session, storeId, mapiMessageCreator, itemBindOption, propertiesToLoad, ref storeObjectType);
				messageItem = (MessageItem)messageItemInfo.Creator(coreItem);
				flag = true;
				result = messageItem;
			}
			finally
			{
				if (!flag)
				{
					Util.DisposeIfPresent(messageItem);
					Util.DisposeIfPresent(coreItem);
				}
			}
			return result;
		}

		// Token: 0x06004F30 RID: 20272 RVA: 0x0014BB94 File Offset: 0x00149D94
		internal static CoreItem CoreItemBind(StoreSession session, StoreId storeId, ItemBuilder.MapiMessageCreator mapiMessageCreator, ItemBindOption itemBindOption, ICollection<PropertyDefinition> propertiesToLoad, ref StoreObjectType storeObjectType)
		{
			Util.ThrowOnNullArgument(session, "session");
			EnumValidator.ThrowIfInvalid<ItemBindOption>(itemBindOption);
			Util.ThrowOnNullArgument(propertiesToLoad, "propertiesToLoad");
			bool flag = false;
			MapiProp mapiProp = null;
			PersistablePropertyBag persistablePropertyBag = null;
			AcrPropertyBag acrPropertyBag = null;
			CoreItem coreItem = null;
			CoreItem result2;
			using (CallbackContext callbackContext = new CallbackContext(session))
			{
				try
				{
					session.OnBeforeItemChange(ItemChangeOperation.ItemBind, session, storeId, coreItem, callbackContext);
					StoreObjectId storeObjectId;
					byte[] array;
					StoreId.SplitStoreObjectIdAndChangeKey(storeId, out storeObjectId, out array);
					session.CheckSystemFolderAccess(storeObjectId);
					if (storeObjectId != null && !IdConverter.IsMessageId(storeObjectId))
					{
						throw new ArgumentException(ServerStrings.ExInvalidItemId);
					}
					bool flag2 = false;
					OccurrenceStoreObjectId occurrenceStoreObjectId = storeObjectId as OccurrenceStoreObjectId;
					IPropertyBagFactory propertyBagFactory;
					if (occurrenceStoreObjectId != null)
					{
						persistablePropertyBag = Item.CreateOccurrencePropertyBag(session, occurrenceStoreObjectId, propertiesToLoad);
						storeObjectType = StoreObjectType.CalendarItemOccurrence;
						flag2 = true;
						propertyBagFactory = new OccurrenceBagFactory(session, occurrenceStoreObjectId);
					}
					else
					{
						if (mapiMessageCreator != null)
						{
							mapiProp = mapiMessageCreator();
						}
						else if ((itemBindOption & ItemBindOption.SoftDeletedItem) == ItemBindOption.SoftDeletedItem)
						{
							mapiProp = session.GetMapiProp(storeObjectId, OpenEntryFlags.BestAccess | OpenEntryFlags.DeferredErrors | OpenEntryFlags.ShowSoftDeletes);
						}
						else
						{
							mapiProp = session.GetMapiProp(storeObjectId);
						}
						persistablePropertyBag = new StoreObjectPropertyBag(session, mapiProp, propertiesToLoad);
						StoreObjectType storeObjectType2 = ItemBuilder.ReadStoreObjectTypeFromPropertyBag(persistablePropertyBag);
						if (storeObjectType2 == storeObjectType)
						{
							flag2 = true;
						}
						else
						{
							storeObjectType = storeObjectType2;
						}
						propertyBagFactory = new RetryBagFactory(session);
						if (storeObjectId != null && storeObjectId.ObjectType != storeObjectType)
						{
							storeObjectId = StoreObjectId.FromProviderSpecificId(storeObjectId.ProviderLevelItemId, storeObjectType);
						}
					}
					ItemBuilder.CheckPrivateItem(session, persistablePropertyBag);
					ItemCreateInfo itemCreateInfo = ItemCreateInfo.GetItemCreateInfo(storeObjectType);
					if (flag2)
					{
						propertiesToLoad = null;
					}
					else
					{
						propertiesToLoad = ItemBuilder.GetPropertiesToLoad(itemBindOption, itemCreateInfo.Schema, propertiesToLoad);
					}
					acrPropertyBag = new AcrPropertyBag(persistablePropertyBag, itemCreateInfo.AcrProfile, storeObjectId, propertyBagFactory, array);
					coreItem = new CoreItem(session, acrPropertyBag, storeObjectId, array, Origin.Existing, ItemLevel.TopLevel, propertiesToLoad, itemBindOption);
					flag = true;
					ConflictResolutionResult result = flag ? ConflictResolutionResult.Success : ConflictResolutionResult.Failure;
					session.OnAfterItemChange(ItemChangeOperation.ItemBind, session, storeId, coreItem, result, callbackContext);
					result2 = coreItem;
				}
				finally
				{
					if (!flag)
					{
						Util.DisposeIfPresent(coreItem);
						Util.DisposeIfPresent(acrPropertyBag);
						Util.DisposeIfPresent(persistablePropertyBag);
						Util.DisposeIfPresent(mapiProp);
					}
				}
			}
			return result2;
		}

		// Token: 0x06004F31 RID: 20273 RVA: 0x0014BD8C File Offset: 0x00149F8C
		internal static StoreObjectType ReadStoreObjectTypeFromPropertyBag(ICorePropertyBag propertyBag)
		{
			object propertyValue = propertyBag.TryGetProperty(CoreItemSchema.ItemClass);
			string text;
			if (PropertyError.IsPropertyValueTooBig(propertyValue) || PropertyError.IsPropertyNotFound(propertyValue))
			{
				text = string.Empty;
			}
			else
			{
				text = PropertyBag.CheckPropertyValue<string>(CoreItemSchema.ItemClass, propertyValue);
			}
			StoreObjectType objectType = ObjectClass.GetObjectType(text);
			for (int i = 0; i < ItemBuilder.storeObjectTypeDetectionChain.Length; i++)
			{
				StoreObjectType? storeObjectType = ItemBuilder.storeObjectTypeDetectionChain[i](propertyBag, text, objectType);
				if (storeObjectType != null)
				{
					return storeObjectType.Value;
				}
			}
			return objectType;
		}

		// Token: 0x06004F32 RID: 20274 RVA: 0x0014BE08 File Offset: 0x0014A008
		private static StoreObjectType? TryDetectFolderTreeDataStoreObjectType(ICorePropertyBag propertyBag, string itemClass, StoreObjectType detectedType)
		{
			StoreObjectType? result = null;
			if (ObjectClass.IsFolderTreeData(itemClass))
			{
				propertyBag.Load(new StorePropertyDefinition[]
				{
					FolderTreeDataSchema.GroupSection,
					FolderTreeDataSchema.Type
				});
				FolderTreeDataSection? valueAsNullable = propertyBag.GetValueAsNullable<FolderTreeDataSection>(FolderTreeDataSchema.GroupSection);
				FolderTreeDataType? valueAsNullable2 = propertyBag.GetValueAsNullable<FolderTreeDataType>(FolderTreeDataSchema.Type);
				if (valueAsNullable == FolderTreeDataSection.Calendar)
				{
					if (valueAsNullable2 == FolderTreeDataType.Header)
					{
						result = new StoreObjectType?(StoreObjectType.CalendarGroup);
					}
					else if (valueAsNullable2 == FolderTreeDataType.NormalFolder || valueAsNullable2 == FolderTreeDataType.SharedFolder)
					{
						result = new StoreObjectType?(StoreObjectType.CalendarGroupEntry);
					}
				}
				else if (valueAsNullable == FolderTreeDataSection.First)
				{
					result = new StoreObjectType?(StoreObjectType.FavoriteFolderEntry);
				}
				else if (valueAsNullable == FolderTreeDataSection.Tasks)
				{
					if (valueAsNullable2 == FolderTreeDataType.Header)
					{
						result = new StoreObjectType?(StoreObjectType.TaskGroup);
					}
					else if (valueAsNullable2 == FolderTreeDataType.NormalFolder)
					{
						result = new StoreObjectType?(StoreObjectType.TaskGroupEntry);
					}
				}
			}
			return result;
		}

		// Token: 0x06004F33 RID: 20275 RVA: 0x0014BF64 File Offset: 0x0014A164
		private static StoreObjectType? TryDetectShortcutMessageEntryStoreObjectType(ICorePropertyBag propertyBag, string itemClass, StoreObjectType detectedType)
		{
			if (detectedType != StoreObjectType.Message || !ObjectClass.IsShortcutMessageEntry(propertyBag.GetValueOrDefault<int>(CoreItemSchema.FavLevelMask, -1)))
			{
				return null;
			}
			return new StoreObjectType?(StoreObjectType.ShortcutMessage);
		}

		// Token: 0x06004F34 RID: 20276 RVA: 0x0014BF9C File Offset: 0x0014A19C
		private static StoreObjectType? TryDetectRightsManagementStoreObjectType(ICorePropertyBag propertyBag, string itemClass, StoreObjectType detectedType)
		{
			if (detectedType != StoreObjectType.Message || !ObjectClass.IsRightsManagedContentClass(propertyBag.GetValueOrDefault<string>(CoreObjectSchema.ContentClass, null)))
			{
				return null;
			}
			return new StoreObjectType?(StoreObjectType.RightsManagedMessage);
		}

		// Token: 0x06004F35 RID: 20277 RVA: 0x0014BFD4 File Offset: 0x0014A1D4
		internal static ICollection<PropertyDefinition> GetPropertiesToLoad(ItemBindOption itemBindOption, Schema schema, ICollection<PropertyDefinition> requestedProperties)
		{
			ICollection<PropertyDefinition> first = ((itemBindOption & ItemBindOption.LoadRequiredPropertiesOnly) == ItemBindOption.LoadRequiredPropertiesOnly) ? schema.RequiredAutoloadProperties : schema.AutoloadProperties;
			return InternalSchema.Combine<PropertyDefinition>(first, requestedProperties);
		}

		// Token: 0x06004F36 RID: 20278 RVA: 0x0014C000 File Offset: 0x0014A200
		internal static PersistablePropertyBag ConstructItemPersistablePropertyBag(StoreSession session, ICollection<PropertyDefinition> propertiesToLoad, bool createAcrPropertyBag, AcrProfile acrProfile, ItemBuilder.MapiMessageCreator mapiMessageCreator)
		{
			MapiMessage mapiMessage = null;
			PersistablePropertyBag persistablePropertyBag = null;
			PersistablePropertyBag persistablePropertyBag2 = null;
			bool flag = false;
			PersistablePropertyBag result;
			try
			{
				mapiMessage = mapiMessageCreator();
				persistablePropertyBag = new StoreObjectPropertyBag(session, mapiMessage, propertiesToLoad);
				PersistablePropertyBag persistablePropertyBag3;
				if (createAcrPropertyBag)
				{
					persistablePropertyBag2 = new AcrPropertyBag(persistablePropertyBag, acrProfile, null, new RetryBagFactory(session), null);
					persistablePropertyBag3 = persistablePropertyBag2;
				}
				else
				{
					persistablePropertyBag3 = persistablePropertyBag;
				}
				flag = true;
				result = persistablePropertyBag3;
			}
			finally
			{
				if (!flag)
				{
					Util.DisposeIfPresent(persistablePropertyBag2);
					Util.DisposeIfPresent(persistablePropertyBag);
					Util.DisposeIfPresent(mapiMessage);
				}
			}
			return result;
		}

		// Token: 0x06004F37 RID: 20279 RVA: 0x0014C074 File Offset: 0x0014A274
		private static void CheckPrivateItem(StoreSession session, PropertyBag propertyBag)
		{
			MailboxSession mailboxSession = session as MailboxSession;
			if (mailboxSession != null && mailboxSession.FilterPrivateItems)
			{
				Sensitivity? valueAsNullable = propertyBag.GetValueAsNullable<Sensitivity>(CoreItemSchema.MapiSensitivity);
				if (valueAsNullable != null && valueAsNullable.Value == Sensitivity.Private)
				{
					throw new ObjectNotFoundException(ServerStrings.ExItemNotFound);
				}
			}
		}

		// Token: 0x04002B2D RID: 11053
		private static readonly Func<ICorePropertyBag, string, StoreObjectType, StoreObjectType?>[] storeObjectTypeDetectionChain = new Func<ICorePropertyBag, string, StoreObjectType, StoreObjectType?>[]
		{
			new Func<ICorePropertyBag, string, StoreObjectType, StoreObjectType?>(ItemBuilder.TryDetectFolderTreeDataStoreObjectType),
			new Func<ICorePropertyBag, string, StoreObjectType, StoreObjectType?>(ItemBuilder.TryDetectShortcutMessageEntryStoreObjectType),
			new Func<ICorePropertyBag, string, StoreObjectType, StoreObjectType?>(ItemBuilder.TryDetectRightsManagementStoreObjectType)
		};

		// Token: 0x0200084F RID: 2127
		// (Invoke) Token: 0x06004F3A RID: 20282
		internal delegate MapiMessage MapiMessageCreator();

		// Token: 0x02000850 RID: 2128
		// (Invoke) Token: 0x06004F3E RID: 20286
		internal delegate PersistablePropertyBag PropertyBagCreator();
	}
}
