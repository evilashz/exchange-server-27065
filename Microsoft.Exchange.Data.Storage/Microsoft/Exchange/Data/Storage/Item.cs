using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000076 RID: 118
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Item : StoreObject, ICoreItemContext, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x060007FE RID: 2046 RVA: 0x0003E220 File Offset: 0x0003C420
		public Item(ICoreItem coreItem, bool shallowDispose = false) : base(coreItem, shallowDispose)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.itemCategoryList = new ItemCategoryList(this);
				this.reminder = this.CreateReminderObject();
				this.CoreItem.SetCoreItemContext(this);
				disposeGuard.Success();
			}
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0003E294 File Offset: 0x0003C494
		public static void CopyItemContent(Item source, Item target)
		{
			Microsoft.Exchange.Data.Storage.CoreItem.CopyItemContent(source.CoreItem, target.CoreItem);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0003E2A7 File Offset: 0x0003C4A7
		public static void CopyItemContent(Item source, Item target, ICollection<NativeStorePropertyDefinition> properties)
		{
			Microsoft.Exchange.Data.Storage.CoreItem.CopyItemContent(source.CoreItem, target.CoreItem, properties);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0003E2BC File Offset: 0x0003C4BC
		public static Item ConvertFrom(Item item, StoreSession session)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			StoreObjectType storeObjectType = ItemBuilder.ReadStoreObjectTypeFromPropertyBag(item.PropertyBag);
			ItemCreateInfo itemCreateInfo = ItemCreateInfo.GetItemCreateInfo(storeObjectType);
			item.CoreItem.PropertyBag.Load(itemCreateInfo.Schema.AutoloadProperties);
			Item item2 = itemCreateInfo.Creator(item.CoreItem);
			item2.CharsetDetector.DetectionOptions = item.CharsetDetector.DetectionOptions;
			return item2;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0003E33C File Offset: 0x0003C53C
		public static void SafeDisposeConvertedItem(Item originalItem, Item convertedItem)
		{
			if (convertedItem.CoreItem != null)
			{
				convertedItem.CoreItem.SetCoreItemContext(originalItem);
				((IDirectPropertyBag)convertedItem.PropertyBag).Context.StoreObject = originalItem;
				convertedItem.CoreObject = null;
			}
			convertedItem.Dispose();
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0003E370 File Offset: 0x0003C570
		public static Item Create(StoreSession session, string messageClass, StoreId parentFolderId)
		{
			Item item = null;
			bool flag = false;
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (messageClass == null)
			{
				throw new ArgumentNullException("messageClass");
			}
			if (parentFolderId == null)
			{
				throw new ArgumentNullException("parentFolderId");
			}
			Item result;
			try
			{
				item = ItemBuilder.CreateNewItem<Item>(session, parentFolderId, ItemCreateInfo.GenericItemInfo);
				item.ClassName = messageClass;
				flag = true;
				result = item;
			}
			finally
			{
				if (!flag && item != null)
				{
					item.Dispose();
					item = null;
				}
			}
			return result;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0003E3E8 File Offset: 0x0003C5E8
		public static Item CloneItem(StoreSession session, StoreId parentFolderId, Item itemToClone, bool bindAsMessage, bool rebindBeforeCloning, ICollection<PropertyDefinition> propertiesToLoad)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
			Util.ThrowOnNullArgument(itemToClone, "itemToClone");
			if (bindAsMessage)
			{
				propertiesToLoad = InternalSchema.Combine<PropertyDefinition>(MessageItemSchema.Instance.AutoloadProperties, propertiesToLoad);
			}
			else
			{
				propertiesToLoad = InternalSchema.Combine<PropertyDefinition>(ItemSchema.Instance.AutoloadProperties, propertiesToLoad);
			}
			MapiMessage mapiMessage = null;
			PersistablePropertyBag persistablePropertyBag = null;
			AcrPropertyBag acrPropertyBag = null;
			CoreItem coreItem = null;
			Item item = null;
			bool flag = false;
			Item result;
			try
			{
				mapiMessage = Microsoft.Exchange.Data.Storage.Item.CreateClonedMapiMessage(session, parentFolderId, itemToClone, rebindBeforeCloning);
				persistablePropertyBag = new StoreObjectPropertyBag(session, mapiMessage, propertiesToLoad);
				StoreObjectType storeObjectType = ItemBuilder.ReadStoreObjectTypeFromPropertyBag(persistablePropertyBag);
				ItemCreateInfo itemCreateInfo = ItemCreateInfo.GetItemCreateInfo(storeObjectType);
				acrPropertyBag = new AcrPropertyBag(persistablePropertyBag, itemCreateInfo.AcrProfile, null, new RetryBagFactory(session), null);
				propertiesToLoad = InternalSchema.Combine<PropertyDefinition>(itemCreateInfo.Schema.AutoloadProperties, propertiesToLoad);
				coreItem = new CoreItem(session, acrPropertyBag, null, null, Origin.New, ItemLevel.TopLevel, propertiesToLoad, ItemBindOption.None);
				ItemCreateInfo.ItemCreator itemCreator = bindAsMessage ? ItemCreateInfo.MessageItemInfo.Creator : itemCreateInfo.Creator;
				item = itemCreator(coreItem);
				flag = true;
				result = item;
			}
			finally
			{
				if (!flag)
				{
					Util.DisposeIfPresent(item);
					Util.DisposeIfPresent(coreItem);
					Util.DisposeIfPresent(acrPropertyBag);
					Util.DisposeIfPresent(persistablePropertyBag);
					Util.DisposeIfPresent(mapiMessage);
				}
			}
			return result;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0003E510 File Offset: 0x0003C710
		public static Item Bind(StoreSession session, StoreId storeId)
		{
			return Microsoft.Exchange.Data.Storage.Item.Bind(session, storeId, null);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0003E51A File Offset: 0x0003C71A
		public static Item Bind(StoreSession session, StoreId storeId, params PropertyDefinition[] propsToReturn)
		{
			return Microsoft.Exchange.Data.Storage.Item.Bind(session, storeId, (ICollection<PropertyDefinition>)propsToReturn);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0003E529 File Offset: 0x0003C729
		public static Item Bind(StoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			return Microsoft.Exchange.Data.Storage.Item.Bind(session, storeId, ItemBindOption.None, propsToReturn);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0003E534 File Offset: 0x0003C734
		public static Item Bind(StoreSession session, StoreId storeId, ItemBindOption itemBindOption)
		{
			return Microsoft.Exchange.Data.Storage.Item.Bind(session, storeId, itemBindOption, null);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0003E540 File Offset: 0x0003C740
		public static Item Bind(StoreSession session, StoreId storeId, ItemBindOption itemBindOption, ICollection<PropertyDefinition> propsToReturn)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(storeId, "storeId");
			EnumValidator.ThrowIfInvalid<ItemBindOption>(itemBindOption);
			if (propsToReturn == null)
			{
				propsToReturn = Array<PropertyDefinition>.Empty;
			}
			ItemCreateInfo itemCreateInfo = ItemCreateInfo.GetItemCreateInfo(StoreId.GetStoreObjectId(storeId).ObjectType);
			return ItemBuilder.ItemBind<Item>(session, storeId, itemCreateInfo.Schema, null, itemBindOption, propsToReturn);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0003E594 File Offset: 0x0003C794
		public static MessageItem BindAsMessage(StoreSession session, StoreId itemId)
		{
			return Microsoft.Exchange.Data.Storage.Item.BindAsMessage(session, itemId, null);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0003E59E File Offset: 0x0003C79E
		public static MessageItem BindAsMessage(StoreSession session, StoreId itemId, params PropertyDefinition[] propsToReturn)
		{
			return Microsoft.Exchange.Data.Storage.Item.BindAsMessage(session, itemId, (ICollection<PropertyDefinition>)propsToReturn);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0003E5AD File Offset: 0x0003C7AD
		public static MessageItem BindAsMessage(StoreSession session, StoreId itemId, ICollection<PropertyDefinition> propsToReturn)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(itemId, "itemId");
			return ItemBuilder.ItemBindAsMessage(session, itemId, null, ItemBindOption.None, propsToReturn);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0003E5CF File Offset: 0x0003C7CF
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<Item>(this);
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x0003E5D7 File Offset: 0x0003C7D7
		public Body Body
		{
			get
			{
				this.CheckDisposed("Body::get");
				return this.CoreItem.Body;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x0003E5EF File Offset: 0x0003C7EF
		public IBody IBody
		{
			get
			{
				return this.Body;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x0003E5F7 File Offset: 0x0003C7F7
		public string Preview
		{
			get
			{
				this.CheckDisposed("Preview::get");
				return base.GetValueOrDefault<string>(InternalSchema.Preview, string.Empty);
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x0003E614 File Offset: 0x0003C814
		// (set) Token: 0x06000812 RID: 2066 RVA: 0x0003E62C File Offset: 0x0003C82C
		public Sensitivity Sensitivity
		{
			get
			{
				this.CheckDisposed("Sensitivity::get");
				return base.GetValueOrDefault<Sensitivity>(InternalSchema.Sensitivity);
			}
			set
			{
				this.CheckDisposed("Sensitivity::set");
				EnumValidator.ThrowIfInvalid<Sensitivity>(value, "value");
				this[InternalSchema.Sensitivity] = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x0003E655 File Offset: 0x0003C855
		// (set) Token: 0x06000814 RID: 2068 RVA: 0x0003E672 File Offset: 0x0003C872
		public virtual string Subject
		{
			get
			{
				this.CheckDisposed("Subject::get");
				return base.GetValueOrDefault<string>(InternalSchema.Subject, string.Empty);
			}
			set
			{
				this.CheckDisposed("Subject::set");
				this[InternalSchema.Subject] = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x0003E68B File Offset: 0x0003C88B
		// (set) Token: 0x06000816 RID: 2070 RVA: 0x0003E6A4 File Offset: 0x0003C8A4
		public Importance Importance
		{
			get
			{
				this.CheckDisposed("Importance::get");
				return base.GetValueOrDefault<Importance>(InternalSchema.Importance, Importance.Normal);
			}
			set
			{
				this.CheckDisposed("Importance::set");
				EnumValidator.ThrowIfInvalid<Importance>(value, "value");
				this[InternalSchema.Importance] = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x0003E6CD File Offset: 0x0003C8CD
		public ItemCategoryList Categories
		{
			get
			{
				this.CheckDisposed("Categories::get");
				return this.itemCategoryList;
			}
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0003E6E0 File Offset: 0x0003C8E0
		public long Size()
		{
			this.CheckDisposed("Size");
			return (long)base.GetValueOrDefault<int>(InternalSchema.Size);
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x0003E6F9 File Offset: 0x0003C8F9
		public IAttachmentCollection IAttachmentCollection
		{
			get
			{
				return this.AttachmentCollection;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x0003E701 File Offset: 0x0003C901
		public AttachmentCollection AttachmentCollection
		{
			get
			{
				this.CheckDisposed("AttachmentCollection::get");
				return this.FetchAttachmentCollection();
			}
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0003E714 File Offset: 0x0003C914
		protected virtual AttachmentCollection FetchAttachmentCollection()
		{
			if (this.attachmentCollection == null)
			{
				this.CoreItem.OpenAttachmentCollection();
				this.attachmentCollection = new AttachmentCollection(this);
			}
			return this.attachmentCollection;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0003E73C File Offset: 0x0003C93C
		internal static NativeStorePropertyDefinition[] CheckNativeProperties(params PropertyDefinition[] properties)
		{
			NativeStorePropertyDefinition[] array = new NativeStorePropertyDefinition[properties.Length];
			for (int i = 0; i < properties.Length; i++)
			{
			}
			Array.Copy(properties, array, properties.Length);
			return array;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0003E76B File Offset: 0x0003C96B
		public void OpenAsReadWrite()
		{
			this.CoreItem.OpenAsReadWrite();
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0003E778 File Offset: 0x0003C978
		public ConflictResolutionResult Save(SaveMode saveMode)
		{
			this.CheckDisposed("Save");
			EnumValidator.ThrowIfInvalid<SaveMode>(saveMode, "saveMode");
			ExTraceGlobals.StorageTracer.Information<int>((long)this.GetHashCode(), "Item::Save. HashCode = {0}", this.GetHashCode());
			return this.SaveInternal(saveMode, true, null, CoreItemOperation.Save);
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0003E7B6 File Offset: 0x0003C9B6
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x0003E7D3 File Offset: 0x0003C9D3
		public override string ClassName
		{
			get
			{
				this.CheckDisposed("ClassName::get");
				return base.GetValueOrDefault<string>(InternalSchema.ItemClass, string.Empty);
			}
			set
			{
				this.CheckDisposed("ClassName::set");
				this[InternalSchema.ItemClass] = value;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x0003E7EC File Offset: 0x0003C9EC
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return ItemSchema.Instance;
			}
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0003E800 File Offset: 0x0003CA00
		public virtual void SetFlag(string flagRequest, ExDateTime? startDate, ExDateTime? dueDate)
		{
			this.CheckDisposed("SetFlag");
			this.CheckFlagAPIsSupported("SetFlag");
			if (flagRequest == null)
			{
				ExTraceGlobals.StorageTracer.TraceError<string>((long)this.GetHashCode(), "Item::SetFlag. The in parameter is null. {0}.", "flagRequest");
				throw new ArgumentNullException("flagRequest");
			}
			this[InternalSchema.FlagRequest] = flagRequest;
			this[InternalSchema.MessageTagged] = true;
			this.SetFlagInternal(startDate, dueDate);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0003E874 File Offset: 0x0003CA74
		public virtual void CompleteFlag(ExDateTime? completeTime)
		{
			this.CheckDisposed("CompleteFlag");
			this.CheckFlagAPIsSupported("CompleteFlag");
			this.CompleteFlagInternal((completeTime != null) ? new ExDateTime?(completeTime.Value.Date) : completeTime, completeTime);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0003E8C0 File Offset: 0x0003CAC0
		public virtual void ClearFlag()
		{
			this.CheckDisposed("ClearFlag");
			this.CheckFlagAPIsSupported("ClearFlag");
			base.DeleteProperties(new PropertyDefinition[]
			{
				InternalSchema.FlagCompleteTime,
				InternalSchema.CompleteDate,
				InternalSchema.ItemColor,
				InternalSchema.FlagRequest,
				InternalSchema.FlagStatus,
				InternalSchema.FlagSubject,
				InternalSchema.TaskStatus,
				InternalSchema.StartDate,
				InternalSchema.DueDate,
				InternalSchema.IsComplete,
				InternalSchema.PercentComplete
			});
			this[InternalSchema.IsToDoItem] = false;
			this[InternalSchema.IsFlagSetForRecipient] = false;
			this[InternalSchema.MessageTagged] = false;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0003E980 File Offset: 0x0003CB80
		public void SetFlagForUtcSession(string flagRequest, ExDateTime? localStartDate, ExDateTime? utcStartDate, ExDateTime? localDueDate, ExDateTime? utcDueDate)
		{
			this.CheckDisposed("SetFlagForUtcSession");
			this.CheckFlagAPIsSupported("SetFlagForUtcSession");
			if (base.PropertyBag.ExTimeZone != null && base.PropertyBag.ExTimeZone != ExTimeZone.UtcTimeZone)
			{
				throw new NotSupportedException(ServerStrings.CanUseApiOnlyWhenTimeZoneIsNull("SetFlagForUtcSession"));
			}
			this.SetFlag(flagRequest, utcStartDate, utcDueDate);
			base.PropertyBag.SetOrDeleteProperty(InternalSchema.LocalStartDate, TaskDate.PersistentLocalTime(localStartDate));
			base.PropertyBag.SetOrDeleteProperty(InternalSchema.LocalDueDate, TaskDate.PersistentLocalTime(localDueDate));
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0003EA18 File Offset: 0x0003CC18
		public void CompleteFlagForUtcSession(ExDateTime? completeDate, ExDateTime flagCompleteTime)
		{
			this.CheckDisposed("CompleteFlagForUtcSession");
			this.CheckFlagAPIsSupported("CompleteFlagForUtcSession");
			if (base.PropertyBag.ExTimeZone != null && base.PropertyBag.ExTimeZone != ExTimeZone.UtcTimeZone)
			{
				throw new NotSupportedException(ServerStrings.CanUseApiOnlyWhenTimeZoneIsNull("CompleteFlagForUtcSession"));
			}
			this.CompleteFlagInternal(completeDate, TaskDate.PersistentLocalTime(new ExDateTime?(flagCompleteTime)));
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x0003EA81 File Offset: 0x0003CC81
		public Reminder Reminder
		{
			get
			{
				this.CheckDisposed("Reminder::get");
				return this.reminder;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x0003EA94 File Offset: 0x0003CC94
		// (set) Token: 0x06000829 RID: 2089 RVA: 0x0003EAB1 File Offset: 0x0003CCB1
		public string VoiceReminderPhoneNumber
		{
			get
			{
				this.CheckDisposed("VoiceReminderPhoneNumber::get");
				return base.GetValueOrDefault<string>(ItemSchema.VoiceReminderPhoneNumber, string.Empty);
			}
			set
			{
				this.CheckDisposed("VoiceReminderPhoneNumber::set");
				this[ItemSchema.VoiceReminderPhoneNumber] = value;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x0003EACA File Offset: 0x0003CCCA
		// (set) Token: 0x0600082B RID: 2091 RVA: 0x0003EAE3 File Offset: 0x0003CCE3
		public bool IsVoiceReminderEnabled
		{
			get
			{
				this.CheckDisposed("IsVoiceReminderEnabled::get");
				return base.GetValueOrDefault<bool>(ItemSchema.IsVoiceReminderEnabled, false);
			}
			set
			{
				this.CheckDisposed("IsVoiceReminderEnabled::set");
				this[ItemSchema.IsVoiceReminderEnabled] = value;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x0003EB01 File Offset: 0x0003CD01
		// (set) Token: 0x0600082D RID: 2093 RVA: 0x0003EB1E File Offset: 0x0003CD1E
		public string WorkingSetId
		{
			get
			{
				this.CheckDisposed("WorkingSetId::get");
				return base.GetValueOrDefault<string>(ItemSchema.WorkingSetId, string.Empty);
			}
			set
			{
				this.CheckDisposed("WorkingSetId::set");
				this[ItemSchema.WorkingSetId] = value;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x0003EB37 File Offset: 0x0003CD37
		// (set) Token: 0x0600082F RID: 2095 RVA: 0x0003EB4F File Offset: 0x0003CD4F
		public WorkingSetSource WorkingSetSource
		{
			get
			{
				this.CheckDisposed("WorkingSetSource::get");
				return base.GetValueOrDefault<WorkingSetSource>(ItemSchema.WorkingSetSource);
			}
			set
			{
				this.CheckDisposed("WorkingSetSource::set");
				EnumValidator.ThrowIfInvalid<WorkingSetSource>(value, "value");
				this[ItemSchema.WorkingSetSource] = value;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x0003EB78 File Offset: 0x0003CD78
		// (set) Token: 0x06000831 RID: 2097 RVA: 0x0003EB90 File Offset: 0x0003CD90
		public string WorkingSetSourcePartition
		{
			get
			{
				this.CheckDisposed("WorkingSetSourcePartition::get");
				return base.GetValueOrDefault<string>(ItemSchema.WorkingSetSourcePartition);
			}
			set
			{
				this.CheckDisposed("WorkingSetSourcePartition::set");
				this[ItemSchema.WorkingSetSourcePartition] = value;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x0003EBA9 File Offset: 0x0003CDA9
		// (set) Token: 0x06000833 RID: 2099 RVA: 0x0003EBC2 File Offset: 0x0003CDC2
		public WorkingSetFlags WorkingSetFlags
		{
			get
			{
				this.CheckDisposed("WorkingSetFlags::get");
				return base.GetValueOrDefault<WorkingSetFlags>(ItemSchema.WorkingSetFlags, WorkingSetFlags.Exchange);
			}
			set
			{
				this.CheckDisposed("WorkingSetFlags::set");
				EnumValidator.ThrowIfInvalid<WorkingSetFlags>(value, "value");
				this[ItemSchema.WorkingSetFlags] = value;
			}
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0003EBEC File Offset: 0x0003CDEC
		public AttachmentId CreateAttachmentId(byte[] idBytes)
		{
			this.CheckDisposed("CreateAttachmentId");
			ExTraceGlobals.StorageTracer.Information<byte[]>((long)this.GetHashCode(), "Item::CreateAttachmentId. idBytes = {0}.", idBytes);
			AttachmentId result;
			if (AttachmentId.TryParse(idBytes, out result))
			{
				return result;
			}
			throw new CorruptDataException(ServerStrings.ExInvalidAttachmentId((idBytes == null) ? "<Null>" : Convert.ToBase64String(idBytes)));
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0003EC44 File Offset: 0x0003CE44
		public AttachmentId CreateAttachmentId(string idBase64)
		{
			this.CheckDisposed("CreateAttachmentId");
			if (idBase64 == null)
			{
				throw new ArgumentNullException("idBase64");
			}
			byte[] idBytes = null;
			try
			{
				idBytes = Convert.FromBase64String(idBase64);
			}
			catch (FormatException arg)
			{
				ExTraceGlobals.StorageTracer.TraceError<FormatException>((long)this.GetHashCode(), "Item::CreateAttachmentId. IdBase64 is not encrypted in base64 format. Throw out FormatException:{0}.", arg);
				throw new CorruptDataException(ServerStrings.ExInvalidBase64StringFormat(idBase64));
			}
			return this.CreateAttachmentId(idBytes);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0003ECB0 File Offset: 0x0003CEB0
		public Conversation GetConversation(params PropertyDefinition[] propsToLoad)
		{
			MailboxSession mailboxSession = base.Session as MailboxSession;
			if (mailboxSession == null)
			{
				throw new InvalidOperationException("Can't get conversation on public folders");
			}
			ConversationId valueOrDefault = base.PropertyBag.GetValueOrDefault<ConversationId>(ItemSchema.ConversationId);
			if (valueOrDefault == null)
			{
				throw new InvalidOperationException("Can't get conversation on item that doesn't have conversationId assigned to it");
			}
			return Conversation.Load(mailboxSession, valueOrDefault, propsToLoad);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0003ED00 File Offset: 0x0003CF00
		internal static void CopyCustomPublicStrings(Item from, Item to)
		{
			from.Load(StoreObjectSchema.ContentConversionProperties);
			List<StorePropertyDefinition> allPropertiesForPropSet = Microsoft.Exchange.Data.Storage.Item.GetAllPropertiesForPropSet(from, WellKnownPropertySet.PublicStrings);
			CalendarItemBase.CopyPropertiesTo(from, to, allPropertiesForPropSet.ToArray());
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0003ED34 File Offset: 0x0003CF34
		internal static List<StorePropertyDefinition> GetAllPropertiesForPropSet(Item item, Guid propSetGuid)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			List<StorePropertyDefinition> list = new List<StorePropertyDefinition>();
			foreach (PropertyDefinition propertyDefinition in item.AllNativeProperties)
			{
				GuidIdPropertyDefinition guidIdPropertyDefinition = propertyDefinition as GuidIdPropertyDefinition;
				if (guidIdPropertyDefinition != null && guidIdPropertyDefinition.Guid == propSetGuid)
				{
					list.Add(guidIdPropertyDefinition);
				}
				GuidNamePropertyDefinition guidNamePropertyDefinition = propertyDefinition as GuidNamePropertyDefinition;
				if (guidNamePropertyDefinition != null && guidNamePropertyDefinition.Guid == propSetGuid)
				{
					list.Add(guidNamePropertyDefinition);
				}
			}
			return list;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0003EDD4 File Offset: 0x0003CFD4
		protected MessageItem InternalCreateReply(MailboxSession session, StoreId parentFolderId, ReplyForwardConfiguration configuration)
		{
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "Item::InternalCreateReply");
			MessageItem messageItem = null;
			bool flag = false;
			MessageItem result;
			try
			{
				messageItem = MessageItem.Create(session, parentFolderId);
				ReplyCreation replyCreation = new ReplyCreation(this, messageItem, configuration, false, false, true);
				replyCreation.PopulateProperties();
				flag = true;
				result = messageItem;
			}
			finally
			{
				if (!flag && messageItem != null)
				{
					messageItem.Dispose();
					messageItem = null;
				}
			}
			return result;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0003EE3C File Offset: 0x0003D03C
		protected MessageItem InternalCreateReplyAll(StoreSession session, StoreId parentFolderId, ReplyForwardConfiguration configuration)
		{
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "Item::InternalCreateReplyAll");
			MessageItem messageItem = null;
			bool flag = false;
			MessageItem result;
			try
			{
				messageItem = MessageItem.Create(session, parentFolderId);
				ReplyCreation replyCreation = new ReplyCreation(this, messageItem, configuration, true, false, true);
				replyCreation.PopulateProperties();
				flag = true;
				result = messageItem;
			}
			finally
			{
				if (!flag && messageItem != null)
				{
					messageItem.Dispose();
					messageItem = null;
				}
			}
			return result;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0003EEA4 File Offset: 0x0003D0A4
		protected MessageItem InternalCreateForward(StoreSession session, StoreId parentFolderId, ReplyForwardConfiguration configuration)
		{
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "Item::InternalCreateForward.");
			MessageItem messageItem = null;
			bool flag = false;
			MessageItem result;
			try
			{
				messageItem = MessageItem.Create(session, parentFolderId);
				ForwardCreation forwardCreation = new ForwardCreation(this, messageItem, configuration);
				forwardCreation.PopulateProperties();
				flag = true;
				result = messageItem;
			}
			finally
			{
				if (!flag && messageItem != null)
				{
					messageItem.Dispose();
					messageItem = null;
				}
			}
			return result;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0003EF08 File Offset: 0x0003D108
		internal void DisableConstraintValidation()
		{
			Microsoft.Exchange.Data.Storage.CoreObject.GetPersistablePropertyBag(base.CoreObject).Context.IsValidationDisabled = true;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0003EF20 File Offset: 0x0003D120
		internal void SetFlagInternal(ExDateTime? startDate, ExDateTime? dueDate)
		{
			this[InternalSchema.FlagStatus] = 2;
			this[InternalSchema.FlagSubject] = this.Subject;
			this[InternalSchema.TaskStatus] = TaskStatus.NotStarted;
			base.SetOrDeleteProperty(InternalSchema.StartDate, startDate);
			base.SetOrDeleteProperty(InternalSchema.DueDate, dueDate);
			this[InternalSchema.IsComplete] = false;
			this[InternalSchema.PercentComplete] = 0.0;
			base.Delete(InternalSchema.FlagCompleteTime);
			base.Delete(InternalSchema.CompleteDate);
			if (this is MessageItem && base.GetValueOrDefault<bool>(InternalSchema.IsDraft))
			{
				this[InternalSchema.IsFlagSetForRecipient] = true;
			}
			else
			{
				this[InternalSchema.ItemColor] = 6;
				this[InternalSchema.IsToDoItem] = true;
			}
			ExDateTime? valueAsNullable = base.GetValueAsNullable<ExDateTime>(InternalSchema.ReceivedTime);
			if (valueAsNullable != null)
			{
				this[InternalSchema.ValidFlagStringProof] = valueAsNullable.Value;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x0003F038 File Offset: 0x0003D238
		// (set) Token: 0x0600083F RID: 2111 RVA: 0x0003F03F File Offset: 0x0003D23F
		internal virtual VersionedId AssociatedItemId
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0003F048 File Offset: 0x0003D248
		internal ConflictResolutionResult SaveInternal(SaveMode saveMode, bool commit, CallbackContext callbackContext = null, CoreItemOperation operation = CoreItemOperation.Save)
		{
			base.Load(null);
			this.OnBeforeSave();
			bool flag = false;
			if (callbackContext == null)
			{
				callbackContext = new CallbackContext(base.Session);
				flag = true;
			}
			ConflictResolutionResult result;
			try
			{
				ConflictResolutionResult conflictResolutionResult = this.CoreItem.InternalFlush(saveMode, operation, callbackContext);
				if ((conflictResolutionResult.SaveStatus == SaveResult.Success || conflictResolutionResult.SaveStatus == SaveResult.SuccessWithConflictResolution) && commit)
				{
					ConflictResolutionResult conflictResolutionResult2 = this.CoreItem.InternalSave(saveMode, callbackContext);
					if (conflictResolutionResult2.SaveStatus != SaveResult.Success)
					{
						conflictResolutionResult = conflictResolutionResult2;
					}
				}
				this.OnAfterSave(conflictResolutionResult);
				result = conflictResolutionResult;
			}
			finally
			{
				if (flag)
				{
					callbackContext.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0003F0D8 File Offset: 0x0003D2D8
		protected virtual void OnBeforeSave()
		{
			if (!base.IsNew)
			{
				this.flagStatus.ExistingItemObjectId = base.StoreObjectId;
				this.flagStatus.ParentId = base.GetValueOrDefault<StoreObjectId>(InternalSchema.ParentItemId);
			}
			if (!base.IsInMemoryObject)
			{
				MailboxSession mailboxSession = base.Session as MailboxSession;
				if (mailboxSession != null)
				{
					MasterCategoryList masterCategoryList = mailboxSession.InternalGetMasterCategoryList();
					foreach (string categoryName in this.itemCategoryList.GetNewCategories())
					{
						masterCategoryList.CategoryWasUsed(base.Id, this.ClassName, categoryName);
					}
				}
			}
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0003F184 File Offset: 0x0003D384
		protected virtual void OnAfterSave(ConflictResolutionResult acrResults)
		{
			if (acrResults.SaveStatus != SaveResult.IrresolvableConflict)
			{
				this.attachmentCollection = null;
			}
			try
			{
				if (acrResults.SaveStatus != SaveResult.IrresolvableConflict)
				{
					this.SetItemFlagsAndMessageStatus();
				}
			}
			catch (ObjectNotFoundException)
			{
			}
			catch (AccessDeniedException)
			{
				MailboxSession mailboxSession = base.Session as MailboxSession;
				if (mailboxSession != null && mailboxSession.LogonType != LogonType.Delegated && mailboxSession.LogonType != LogonType.BestAccess)
				{
					throw;
				}
			}
			if (this.Reminder != null && this.Reminder.HasAcrAffectedReminders(acrResults))
			{
				base.Load(null);
				this.Reminder.SaveStateAsInitial(true);
			}
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0003F220 File Offset: 0x0003D420
		internal void ReplaceAttachments(Item item)
		{
			this.ReplaceAttachments(item, null);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0003F240 File Offset: 0x0003D440
		public void ReplaceAttachments(Item item, BodyFormat? format)
		{
			this.CheckDisposed("ReplaceAttachments");
			Util.ThrowOnNullArgument(item, "item");
			item.AttachmentCollection.RemoveAll();
			AttachmentCollection attachmentCollection = this.AttachmentCollection;
			foreach (AttachmentHandle handle in attachmentCollection)
			{
				using (Attachment attachment = attachmentCollection.Open(handle, null))
				{
					if (!attachment.IsCalendarException && !attachment.GetValueOrDefault<bool>(InternalSchema.AttachInConflict))
					{
						using (Attachment attachment2 = attachment.CreateCopy(item.AttachmentCollection, format))
						{
							attachment2.Save();
						}
					}
				}
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0003F314 File Offset: 0x0003D514
		protected void CheckSetNull(string method, string argument, object setValue)
		{
			if (setValue == null)
			{
				ExTraceGlobals.StorageTracer.TraceError<string, string>((long)this.GetHashCode(), "{0}::set. {1} cannot be set to null.", method, argument);
				throw new ArgumentNullException(argument);
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0003F338 File Offset: 0x0003D538
		internal static OccurrencePropertyBag CreateOccurrencePropertyBag(StoreSession session, OccurrenceStoreObjectId occurrenceId, ICollection<PropertyDefinition> additionalProperties)
		{
			ICollection<PropertyDefinition> autoloadProperties = InternalSchema.Combine<PropertyDefinition>(CalendarItemBaseSchema.Instance.AutoloadProperties, additionalProperties);
			bool flag = false;
			StoreObjectPropertyBag storeObjectPropertyBag = new StoreObjectPropertyBag(session, session.GetMapiProp(occurrenceId), autoloadProperties);
			OccurrencePropertyBag result;
			try
			{
				byte[] largeBinaryProperty = storeObjectPropertyBag.GetLargeBinaryProperty(InternalSchema.AppointmentRecurrenceBlob);
				if (largeBinaryProperty == null)
				{
					throw new OccurrenceNotFoundException(ServerStrings.ExOccurrenceNotPresent(occurrenceId));
				}
				int valueOrDefault = storeObjectPropertyBag.GetValueOrDefault<int>(InternalSchema.Codepage, CalendarItem.DefaultCodePage);
				ExTimeZone recurringTimeZoneFromPropertyBag = TimeZoneHelper.GetRecurringTimeZoneFromPropertyBag(storeObjectPropertyBag);
				InternalRecurrence internalRecurrence = InternalRecurrence.InternalParse(largeBinaryProperty, new VersionedId(StoreObjectId.FromProviderSpecificId(occurrenceId.ProviderLevelItemId, StoreObjectType.CalendarItem), Array<byte>.Empty), recurringTimeZoneFromPropertyBag, session.ExTimeZone, valueOrDefault);
				ExDateTime exDateTime = occurrenceId.OccurrenceId;
				if (exDateTime.TimeZone == ExTimeZone.UnspecifiedTimeZone)
				{
					exDateTime = internalRecurrence.CreatedExTimeZone.ConvertDateTime(exDateTime);
				}
				if (!internalRecurrence.IsValidOccurrenceId(exDateTime) || internalRecurrence.IsOccurrenceDeleted(exDateTime))
				{
					ExTraceGlobals.StorageTracer.Information<OccurrenceStoreObjectId>(0L, "Item::CreateOccurrencePropertyBag. Open requested occurence on deleted occurrnece={0}", occurrenceId);
					throw new OccurrenceNotFoundException(ServerStrings.ExItemNotFound);
				}
				OccurrenceInfo occurrenceInfoByDateId = internalRecurrence.GetOccurrenceInfoByDateId(exDateTime);
				OccurrencePropertyBag occurrencePropertyBag = new OccurrencePropertyBag(session, storeObjectPropertyBag, occurrenceInfoByDateId, additionalProperties);
				flag = true;
				result = occurrencePropertyBag;
			}
			finally
			{
				if (!flag && storeObjectPropertyBag != null)
				{
					storeObjectPropertyBag.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0003F45C File Offset: 0x0003D65C
		internal static Item InternalBindCoreItem(ICoreItem coreItem)
		{
			StoreObjectType storeObjectType = ItemBuilder.ReadStoreObjectTypeFromPropertyBag(coreItem.PropertyBag);
			ItemCreateInfo itemCreateInfo = ItemCreateInfo.GetItemCreateInfo(storeObjectType);
			return itemCreateInfo.Creator(coreItem);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0003F488 File Offset: 0x0003D688
		internal static Item TransferOwnershipOfCoreItem(Item item)
		{
			StoreObjectType storeObjectType = ItemBuilder.ReadStoreObjectTypeFromPropertyBag(item.PropertyBag);
			ItemCreateInfo itemCreateInfo = ItemCreateInfo.GetItemCreateInfo(storeObjectType);
			Item result = itemCreateInfo.Creator(item.CoreItem);
			item.AssignNullToCoreItem();
			return result;
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x0003F4C1 File Offset: 0x0003D6C1
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x0003F4D9 File Offset: 0x0003D6D9
		public PropertyBagSaveFlags SaveFlags
		{
			get
			{
				this.CheckDisposed("Item.SaveFlags.get");
				return base.PropertyBag.SaveFlags;
			}
			set
			{
				this.CheckDisposed("Item.SaveFlags.set");
				EnumValidator.ThrowIfInvalid<PropertyBagSaveFlags>(value, "value");
				base.PropertyBag.SaveFlags = value;
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0003F4FD File Offset: 0x0003D6FD
		protected virtual Reminder CreateReminderObject()
		{
			return new Reminder(this);
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x0003F505 File Offset: 0x0003D705
		public MapiMessage MapiMessage
		{
			get
			{
				return (MapiMessage)base.MapiProp;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x0003F512 File Offset: 0x0003D712
		internal bool HasAllPropertiesLoaded
		{
			get
			{
				return base.PropertyBag.HasAllPropertiesLoaded;
			}
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0003F51F File Offset: 0x0003D71F
		protected void ClearFlagsPropertyForSet(PropertyDefinition propertyDefinition)
		{
			this.flagStatus.ClearFlagsForSet(propertyDefinition);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0003F52D File Offset: 0x0003D72D
		internal void SetFlagsApiProperties(PropertyDefinition propertyDefinition, int flag, bool value)
		{
			this.flagStatus.SetFlagsPropertyOnItem(propertyDefinition, flag, value);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0003F53D File Offset: 0x0003D73D
		internal bool? GetFlagsApiProperties(PropertyDefinition propertyDefinition, int flag)
		{
			return this.flagStatus.TryGetValue(propertyDefinition, flag);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0003F54C File Offset: 0x0003D74C
		private static MapiMessage CreateClonedMapiMessage(StoreSession destinationSession, StoreId parentFolderId, Item itemToClone, bool rebindBeforeCloning)
		{
			MapiMessage result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				MapiMessage mapiMessage = Folder.InternalCreateMapiMessage(destinationSession, parentFolderId, CreateMessageType.Normal);
				disposeGuard.Add<MapiMessage>(mapiMessage);
				StoreSession session = itemToClone.Session;
				if (rebindBeforeCloning)
				{
					using (MapiMessage mapiMessage2 = (MapiMessage)session.GetMapiProp(itemToClone.Id.ObjectId))
					{
						Microsoft.Exchange.Data.Storage.CoreObject.MapiCopyTo(mapiMessage2, mapiMessage, session, destinationSession, CopyPropertiesFlags.None, CopySubObjects.Copy, new NativeStorePropertyDefinition[0]);
						goto IL_70;
					}
				}
				Microsoft.Exchange.Data.Storage.CoreObject.MapiCopyTo(itemToClone.MapiMessage, mapiMessage, session, destinationSession, CopyPropertiesFlags.None, CopySubObjects.Copy, new NativeStorePropertyDefinition[0]);
				IL_70:
				disposeGuard.Success();
				result = mapiMessage;
			}
			return result;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0003F604 File Offset: 0x0003D804
		private void CheckFlagAPIsSupported(string method)
		{
			if (this is CalendarItemBase || this is Task)
			{
				throw new StoragePermanentException(ServerStrings.InvokingMethodNotSupported(base.GetType().Name, method));
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0003F630 File Offset: 0x0003D830
		private void CompleteFlagInternal(ExDateTime? completeDate, ExDateTime? flagCompleteTime)
		{
			base.DeleteProperties(new PropertyDefinition[]
			{
				InternalSchema.ItemColor
			});
			this[InternalSchema.IsComplete] = true;
			this[InternalSchema.PercentComplete] = 1.0;
			base.SetOrDeleteProperty(InternalSchema.CompleteDate, completeDate);
			base.SetOrDeleteProperty(InternalSchema.FlagCompleteTime, flagCompleteTime);
			this[InternalSchema.FlagStatus] = 1;
			string value = base.TryGetProperty(InternalSchema.FlagRequest) as string;
			if (string.IsNullOrEmpty(value))
			{
				this[InternalSchema.FlagRequest] = ClientStrings.Followup.ToString(base.Session.InternalPreferedCulture);
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x0003F6EC File Offset: 0x0003D8EC
		internal bool IsAttachmentCollectionLoaded
		{
			get
			{
				this.CheckDisposed("IsAttachmentCollectiongLoaded::get");
				return this.CoreItem.IsAttachmentCollectionLoaded;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x0003F704 File Offset: 0x0003D904
		public ItemCharsetDetector CharsetDetector
		{
			get
			{
				this.CheckDisposed("Item.CharsetDetector::get");
				return this.CoreItem.CharsetDetector;
			}
		}

		// Token: 0x1700018C RID: 396
		// (set) Token: 0x06000856 RID: 2134 RVA: 0x0003F71C File Offset: 0x0003D91C
		public int PreferredInternetCodePageForShiftJis
		{
			set
			{
				this.CharsetDetector.DetectionOptions.PreferredInternetCodePageForShiftJis = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (set) Token: 0x06000857 RID: 2135 RVA: 0x0003F72F File Offset: 0x0003D92F
		public int RequiredCoverage
		{
			set
			{
				this.CharsetDetector.DetectionOptions.RequiredCoverage = value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x0003F742 File Offset: 0x0003D942
		public ICoreItem CoreItem
		{
			get
			{
				return (ICoreItem)base.CoreObject;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x0003F74F File Offset: 0x0003D94F
		internal bool IsReadOnly
		{
			get
			{
				this.CheckDisposed("IsReadyOnly::get");
				return this.CoreItem.IsReadOnly;
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0003F768 File Offset: 0x0003D968
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.GetType().FullName);
			stringBuilder.AppendLine();
			if (base.IsDisposed)
			{
				stringBuilder.AppendLine("disposed");
			}
			else
			{
				if (base.Session != null)
				{
					stringBuilder.AppendFormat("Session: {0}", base.Session.ToString());
					stringBuilder.AppendLine();
				}
				if (base.StoreObjectId != null)
				{
					stringBuilder.AppendFormat("Item StoreObjectId: {0}", base.StoreObjectId.ToBase64String());
					stringBuilder.AppendLine();
				}
				if (base.Id != null)
				{
					stringBuilder.AppendFormat("Item Id: {0}", base.Id.ToBase64String());
					stringBuilder.AppendLine();
				}
				string valueOrDefault = base.GetValueOrDefault<string>(InternalSchema.InternetMessageId);
				if (valueOrDefault != null)
				{
					stringBuilder.AppendFormat("Message id: {0}", valueOrDefault);
					stringBuilder.AppendLine();
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0003F844 File Offset: 0x0003DA44
		private void SetItemFlagsAndMessageStatus()
		{
			if (base.Session == null)
			{
				return;
			}
			if (!this.flagStatus.IsDirty())
			{
				return;
			}
			if (this.flagStatus.ExistingItemObjectId == null)
			{
				base.Load(new PropertyDefinition[]
				{
					InternalSchema.ItemId
				});
				if (base.Id == null)
				{
					return;
				}
				this.flagStatus.ExistingItemObjectId = base.Id.ObjectId;
				this.flagStatus.ParentId = base.ParentId;
			}
			int setReadFlag = this.flagStatus.GetSetReadFlag();
			if (setReadFlag >= 0)
			{
				this.CoreItem.SetReadFlag(setReadFlag, false);
			}
			int num = 0;
			int num2 = 0;
			if (this.flagStatus.GetNonReadFlagsBits(out num, out num2))
			{
				using (MapiFolder mapiFolder = Folder.DeferBind(base.Session, this.flagStatus.ParentId))
				{
					StoreSession session = base.Session;
					bool flag = false;
					try
					{
						if (session != null)
						{
							session.BeginMapiCall();
							session.BeginServerHealthCall();
							flag = true;
						}
						if (StorageGlobals.MapiTestHookBeforeCall != null)
						{
							StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
						}
						mapiFolder.SetMessageFlags(this.flagStatus.ExistingItemObjectId.ProviderLevelItemId, (MessageFlags)num, (MessageFlags)num2);
					}
					catch (MapiPermanentException ex)
					{
						throw StorageGlobals.TranslateMapiException(ServerStrings.CannotSetMessageFlagStatus, ex, session, this, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("Item::SetItemFlagsAndMessageStatus.", new object[0]),
							ex
						});
					}
					catch (MapiRetryableException ex2)
					{
						throw StorageGlobals.TranslateMapiException(ServerStrings.CannotSetMessageFlagStatus, ex2, session, this, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("Item::SetItemFlagsAndMessageStatus.", new object[0]),
							ex2
						});
					}
					finally
					{
						try
						{
							if (session != null)
							{
								session.EndMapiCall();
								if (flag)
								{
									session.EndServerHealthCall();
								}
							}
						}
						finally
						{
							if (StorageGlobals.MapiTestHookAfterCall != null)
							{
								StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
							}
						}
					}
				}
			}
			if (this.flagStatus.GetDirtyStatusBits(out num, out num2))
			{
				using (MapiFolder mapiFolder2 = Folder.DeferBind(base.Session, this.flagStatus.ParentId))
				{
					CoreFolder.InternalSetItemStatus(mapiFolder2, base.Session, this, this.flagStatus.ExistingItemObjectId, (MessageStatus)num, (MessageStatus)num2);
				}
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0003FAB4 File Offset: 0x0003DCB4
		private void AssignNullToCoreItem()
		{
			base.CoreObject = null;
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x0003FABD File Offset: 0x0003DCBD
		internal virtual bool AreAttachmentsDirty
		{
			get
			{
				return this.IsAttachmentCollectionLoaded && this.CoreItem.AttachmentCollection.IsDirty;
			}
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0003FAD9 File Offset: 0x0003DCD9
		void ICoreItemContext.GetContextCharsetDetectionData(StringBuilder stringBuilder, CharsetDetectionDataFlags flags)
		{
			this.InternalGetContextCharsetDetectionData(stringBuilder, flags);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0003FAE3 File Offset: 0x0003DCE3
		protected virtual void InternalGetContextCharsetDetectionData(StringBuilder stringBuilder, CharsetDetectionDataFlags flags)
		{
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0003FAE8 File Offset: 0x0003DCE8
		internal static void CoreObjectUpdateInternetMessageId(CoreItem coreItem)
		{
			string valueOrDefault = coreItem.PropertyBag.GetValueOrDefault<string>(InternalSchema.InternetMessageId, null);
			if (valueOrDefault != null && valueOrDefault == string.Empty)
			{
				coreItem.PropertyBag.Delete(InternalSchema.InternetMessageId);
			}
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0003FB28 File Offset: 0x0003DD28
		internal static void CoreObjectUpdatePreview(CoreItem coreItem)
		{
			string valueOrDefault = coreItem.PropertyBag.GetValueOrDefault<string>(InternalSchema.Preview, null);
			if (coreItem.Body != null && ((ICoreItem)coreItem).AreOptionalAutoloadPropertiesLoaded && (coreItem.Body.IsBodyChanged || coreItem.Body.IsPreviewInvalid || string.IsNullOrEmpty(valueOrDefault)))
			{
				coreItem.PropertyBag.SetProperty(InternalSchema.Preview, coreItem.Body.PreviewText.Trim(Environment.NewLine.ToCharArray()));
			}
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0003FBA4 File Offset: 0x0003DDA4
		internal static void CoreObjectUpdateSentRepresentingType(CoreItem coreItem)
		{
			string valueOrDefault = coreItem.PropertyBag.GetValueOrDefault<string>(InternalSchema.SentRepresentingType, null);
			string valueOrDefault2 = coreItem.PropertyBag.GetValueOrDefault<string>(InternalSchema.SentRepresentingEmailAddress, null);
			byte[] valueOrDefault3 = coreItem.PropertyBag.GetValueOrDefault<byte[]>(InternalSchema.SentRepresentingEntryId, null);
			if (!string.IsNullOrWhiteSpace(valueOrDefault) && string.Compare(valueOrDefault, "EX", StringComparison.CurrentCultureIgnoreCase) != 0 && valueOrDefault2 == null && valueOrDefault3 != null && valueOrDefault3.Length > 0)
			{
				coreItem.PropertyBag.Delete(InternalSchema.SentRepresentingType);
			}
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0003FC1C File Offset: 0x0003DE1C
		internal static void CoreObjectUpdateAnnotationToken(CoreItem coreItem)
		{
			if (coreItem.IsMoveUser)
			{
				return;
			}
			bool flag = false;
			if (((ICoreItem)coreItem).IsAttachmentCollectionLoaded)
			{
				flag = coreItem.AttachmentCollection.IsDirty;
				if (flag && coreItem.Id == null)
				{
					flag = !coreItem.AttachmentCollection.IsClonedFromAnExistingAttachmentCollection;
				}
			}
			if ((coreItem.Body != null && coreItem.Body.IsBodyChanged) || flag)
			{
				coreItem.PropertyBag.Delete(InternalSchema.AnnotationToken);
			}
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0003FC8C File Offset: 0x0003DE8C
		internal static void CoreObjectUpdateAllAttachmentsHidden(CoreItem coreItem)
		{
			if (((ICoreItem)coreItem).IsAttachmentCollectionLoaded && (coreItem.Origin == Origin.New || coreItem.AttachmentCollection.IsDirty))
			{
				string valueOrDefault = coreItem.PropertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass, string.Empty);
				if (ObjectClass.IsDsn(valueOrDefault) || ObjectClass.IsMdn(valueOrDefault))
				{
					Microsoft.Exchange.Data.Storage.Item.EnsureAllAttachmentsHiddenValue(coreItem, true);
					return;
				}
				if (ObjectClass.IsSmime(valueOrDefault))
				{
					return;
				}
				string valueOrDefault2 = coreItem.PropertyBag.GetValueOrDefault<string>(InternalSchema.ContentClass, string.Empty);
				if (ObjectClass.IsRightsManagedContentClass(valueOrDefault2) && coreItem.AttachmentCollection.Count == 1)
				{
					return;
				}
				bool flag = false;
				foreach (AttachmentHandle handle in coreItem.AttachmentCollection)
				{
					if (!CoreAttachmentCollection.IsInlineAttachment(handle))
					{
						flag = true;
						break;
					}
				}
				Microsoft.Exchange.Data.Storage.Item.EnsureAllAttachmentsHiddenValue(coreItem, !flag);
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0003FD78 File Offset: 0x0003DF78
		internal static void EnsureAllAttachmentsHiddenValue(CoreItem coreItem, bool value)
		{
			object objA = coreItem.PropertyBag.TryGetProperty(InternalSchema.AllAttachmentsHidden);
			if (!object.Equals(objA, value))
			{
				coreItem.PropertyBag.SetProperty(InternalSchema.AllAttachmentsHidden, value);
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0003FDBC File Offset: 0x0003DFBC
		public void ForceUpdateImapiId()
		{
			base.SetProperties(Microsoft.Exchange.Data.Storage.Item.ImapIdProperty, new object[]
			{
				0
			});
			base.PropertyBag.SetUpdateImapIdFlag();
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0003FDF0 File Offset: 0x0003DFF0
		public RuleHistory GetRuleHistory()
		{
			this.CheckDisposed("GetRuleHistory");
			return this.GetRuleHistory(base.Session);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0003FE0C File Offset: 0x0003E00C
		public RuleHistory GetRuleHistory(StoreSession session)
		{
			this.CheckDisposed("GetRuleHistory");
			byte[] valueOrDefault = base.GetValueOrDefault<byte[]>(ItemSchema.RuleTriggerHistory, Array<byte>.Empty);
			return new RuleHistory(this, valueOrDefault, session);
		}

		// Token: 0x0400022C RID: 556
		private FlagStatusInternal flagStatus = new FlagStatusInternal();

		// Token: 0x0400022D RID: 557
		private readonly ItemCategoryList itemCategoryList;

		// Token: 0x0400022E RID: 558
		private readonly Reminder reminder;

		// Token: 0x0400022F RID: 559
		protected AttachmentCollection attachmentCollection;

		// Token: 0x04000230 RID: 560
		private static PropertyDefinition[] ImapIdProperty = new PropertyDefinition[]
		{
			ItemSchema.ImapId
		};
	}
}
