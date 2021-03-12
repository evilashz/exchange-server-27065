using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000648 RID: 1608
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CoreAttachmentCollection : DisposableObject, IEnumerable<AttachmentHandle>, IEnumerable
	{
		// Token: 0x0600425D RID: 16989 RVA: 0x0011B2F4 File Offset: 0x001194F4
		internal CoreAttachmentCollection(IAttachmentProvider attachmentProvider, ICoreItem message, bool forceReadOnly, bool hasAttachments)
		{
			Util.ThrowOnNullArgument(message, "message");
			if (!hasAttachments)
			{
				this.isInitialized = true;
			}
			this.coreItem = message;
			this.forceReadOnly = forceReadOnly;
			this.attachmentProvider = attachmentProvider;
			this.fetchProperties = CoreAttachmentCollection.PrefetchPropertySet;
		}

		// Token: 0x17001387 RID: 4999
		// (get) Token: 0x0600425E RID: 16990 RVA: 0x0011B35E File Offset: 0x0011955E
		internal bool IsReadOnly
		{
			get
			{
				this.CheckDisposed(null);
				return this.forceReadOnly || this.ContainerItem.IsReadOnly;
			}
		}

		// Token: 0x17001388 RID: 5000
		// (get) Token: 0x0600425F RID: 16991 RVA: 0x0011B37C File Offset: 0x0011957C
		internal bool IsDirty
		{
			get
			{
				this.CheckDisposed(null);
				return this.isDirty;
			}
		}

		// Token: 0x17001389 RID: 5001
		// (get) Token: 0x06004260 RID: 16992 RVA: 0x0011B38B File Offset: 0x0011958B
		// (set) Token: 0x06004261 RID: 16993 RVA: 0x0011B393 File Offset: 0x00119593
		internal bool IsClonedFromAnExistingAttachmentCollection { get; private set; }

		// Token: 0x1700138A RID: 5002
		// (get) Token: 0x06004262 RID: 16994 RVA: 0x0011B39C File Offset: 0x0011959C
		internal bool IsInitialized
		{
			get
			{
				this.CheckDisposed(null);
				return this.isInitialized;
			}
		}

		// Token: 0x1700138B RID: 5003
		// (get) Token: 0x06004263 RID: 16995 RVA: 0x0011B3AB File Offset: 0x001195AB
		internal int Count
		{
			get
			{
				this.CheckDisposed(null);
				this.InitCollection("GetEnumerator", false);
				return this.savedAttachmentNumberMap.Count;
			}
		}

		// Token: 0x1700138C RID: 5004
		// (get) Token: 0x06004264 RID: 16996 RVA: 0x0011B3CB File Offset: 0x001195CB
		internal NativeStorePropertyDefinition[] AttachmentTablePropertyList
		{
			get
			{
				this.CheckDisposed(null);
				return this.fetchProperties.ToArray<NativeStorePropertyDefinition>();
			}
		}

		// Token: 0x1700138D RID: 5005
		// (get) Token: 0x06004265 RID: 16997 RVA: 0x0011B3DF File Offset: 0x001195DF
		internal ICoreItem ContainerItem
		{
			get
			{
				this.CheckDisposed(null);
				return this.coreItem;
			}
		}

		// Token: 0x1700138E RID: 5006
		// (get) Token: 0x06004266 RID: 16998 RVA: 0x0011B3EE File Offset: 0x001195EE
		internal ExTimeZone ExTimeZone
		{
			get
			{
				this.CheckDisposed(null);
				return CoreObject.GetPersistablePropertyBag(this.ContainerItem).ExTimeZone;
			}
		}

		// Token: 0x06004267 RID: 16999 RVA: 0x0011B408 File Offset: 0x00119608
		public CoreAttachment Create(AttachmentType type)
		{
			this.CheckDisposed(null);
			EnumValidator.ThrowIfInvalid<AttachmentType>(type, "type");
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "Storage.CoreAttachmentCollection.Create1");
			this.InitCollection("CreateFromExistingItem", true);
			return this.InternalCreate(new AttachmentType?(type), null, null, null);
		}

		// Token: 0x06004268 RID: 17000 RVA: 0x0011B458 File Offset: 0x00119658
		public CoreAttachment Open(AttachmentHandle handle)
		{
			return this.Open(handle, null);
		}

		// Token: 0x06004269 RID: 17001 RVA: 0x0011B464 File Offset: 0x00119664
		public CoreAttachment Open(AttachmentHandle handle, ICollection<PropertyDefinition> preloadProperties)
		{
			this.CheckDisposed(null);
			AttachmentHandle objB = null;
			if (!this.savedAttachmentNumberMap.TryGetValue(handle.AttachNumber, out objB) || !object.ReferenceEquals(handle, objB))
			{
				throw new ArgumentException("handle");
			}
			return this.InternalOpen(handle, preloadProperties);
		}

		// Token: 0x0600426A RID: 17002 RVA: 0x0011B4AB File Offset: 0x001196AB
		public IEnumerator<AttachmentHandle> GetEnumerator()
		{
			this.CheckDisposed(null);
			this.InitCollection("GetEnumerator", false);
			return this.savedAttachmentNumberMap.Values.GetEnumerator();
		}

		// Token: 0x0600426B RID: 17003 RVA: 0x0011B4D5 File Offset: 0x001196D5
		IEnumerator IEnumerable.GetEnumerator()
		{
			this.CheckDisposed(null);
			return this.GetEnumerator();
		}

		// Token: 0x0600426C RID: 17004 RVA: 0x0011B4E4 File Offset: 0x001196E4
		public bool Remove(AttachmentHandle attachmentHandle)
		{
			this.CheckDisposed(null);
			Util.ThrowOnNullArgument(attachmentHandle, "attachmentHandle");
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "Storage.CoreAttachmentCollection.Remove(AttachmentHandle)");
			this.InitCollection("Remove", true);
			int attachNumber = attachmentHandle.AttachNumber;
			this.attachmentProvider.DeleteAttachment(attachNumber);
			if (attachmentHandle.AttachmentId != null)
			{
				this.attachmentIdMap.Remove(attachmentHandle.AttachmentId);
			}
			this.savedAttachmentNumberMap.Remove(attachNumber);
			this.isDirty = true;
			this.IsClonedFromAnExistingAttachmentCollection = false;
			return true;
		}

		// Token: 0x0600426D RID: 17005 RVA: 0x0011B570 File Offset: 0x00119770
		internal static void CloneAttachmentCollection(ICoreItem sourceItem, ICoreItem destinationItem)
		{
			foreach (AttachmentHandle handle in sourceItem.AttachmentCollection)
			{
				using (CoreAttachment coreAttachment = sourceItem.AttachmentCollection.Open(handle, CoreObjectSchema.AllPropertiesOnStore))
				{
					using (CoreAttachment coreAttachment2 = destinationItem.AttachmentCollection.CreateCopy(coreAttachment))
					{
						using (Attachment attachment = AttachmentCollection.CreateTypedAttachment(coreAttachment2, new AttachmentType?(coreAttachment2.AttachmentType)))
						{
							attachment.SaveFlags |= (coreAttachment.PropertyBag.SaveFlags | PropertyBagSaveFlags.IgnoreMapiComputedErrors);
							attachment.Save();
						}
					}
				}
			}
			destinationItem.AttachmentCollection.IsClonedFromAnExistingAttachmentCollection = (sourceItem.AttachmentCollection.Count > 0 && !sourceItem.AttachmentCollection.IsDirty && sourceItem.AttachmentCollection.Count == destinationItem.AttachmentCollection.Count);
		}

		// Token: 0x0600426E RID: 17006 RVA: 0x0011B698 File Offset: 0x00119898
		internal static bool IsCalendarException(int attachCalendarFlags)
		{
			return (attachCalendarFlags & 6) != 0;
		}

		// Token: 0x0600426F RID: 17007 RVA: 0x0011B6A3 File Offset: 0x001198A3
		internal static bool IsCalendarException(AttachmentHandle handle)
		{
			return handle.IsCalendarException;
		}

		// Token: 0x06004270 RID: 17008 RVA: 0x0011B6AB File Offset: 0x001198AB
		internal static bool IsInlineAttachment(AttachmentHandle handle)
		{
			return handle.IsInline;
		}

		// Token: 0x06004271 RID: 17009 RVA: 0x0011B6B4 File Offset: 0x001198B4
		internal static Schema GetAttachmentSchema(int attachMethod)
		{
			switch (attachMethod)
			{
			case 1:
			case 6:
				return StreamAttachmentBaseSchema.Instance;
			case 2:
			case 3:
			case 4:
				return ReferenceAttachmentSchema.Instance;
			default:
				return ItemAttachmentSchema.Instance;
			}
		}

		// Token: 0x06004272 RID: 17010 RVA: 0x0011B6F4 File Offset: 0x001198F4
		internal static Schema GetAttachmentSchema(AttachmentType? attachmentType)
		{
			if (attachmentType == null || attachmentType.Value == AttachmentType.EmbeddedMessage)
			{
				return ItemAttachmentSchema.Instance;
			}
			if (attachmentType.Value == AttachmentType.Reference)
			{
				return ReferenceAttachmentSchema.Instance;
			}
			return StreamAttachmentBaseSchema.Instance;
		}

		// Token: 0x06004273 RID: 17011 RVA: 0x0011B724 File Offset: 0x00119924
		internal static AttachmentType GetAttachmentType(int? attachMethod)
		{
			if (attachMethod != null)
			{
				switch (attachMethod.Value)
				{
				case 2:
				case 3:
				case 4:
				case 7:
					return AttachmentType.Reference;
				case 5:
					return AttachmentType.EmbeddedMessage;
				case 6:
					return AttachmentType.Ole;
				}
				return AttachmentType.Stream;
			}
			return AttachmentType.Stream;
		}

		// Token: 0x06004274 RID: 17012 RVA: 0x0011B773 File Offset: 0x00119973
		internal void Load(ICollection<PropertyDefinition> propertyList)
		{
			this.CheckDisposed(null);
			if (propertyList == InternalSchema.ContentConversionProperties)
			{
				throw new InvalidOperationException("Cannot load ContentConversionProperties on the attachment collection. Call Load() on individual attachments instead");
			}
			if (propertyList != null && propertyList.Count != 0)
			{
				this.fetchProperties = CoreAttachmentCollection.CreatePrefetchPropertySet<PropertyDefinition>(this.fetchProperties, propertyList);
			}
		}

		// Token: 0x06004275 RID: 17013 RVA: 0x0011B7AC File Offset: 0x001199AC
		internal CoreAttachment CreateFromExistingItem(IItem item)
		{
			this.CheckDisposed(null);
			Util.ThrowOnNullArgument(item, "item");
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "Storage.CoreAttachmentCollection.AddExistingItem.");
			this.InitCollection("CreateFromExistingItem", true);
			return this.InternalCreate(new AttachmentType?(AttachmentType.EmbeddedMessage), null, null, item);
		}

		// Token: 0x06004276 RID: 17014 RVA: 0x0011B7FC File Offset: 0x001199FC
		internal CoreAttachment CreateCopy(CoreAttachment attachmentToCopy)
		{
			this.CheckDisposed(null);
			Util.ThrowOnNullArgument(attachmentToCopy, "attachmentToCopy");
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "Storage.CoreAttachmentCollection.CreateCopy");
			this.InitCollection("CreateCopy", true);
			return this.InternalCreateCopy(new AttachmentType?(attachmentToCopy.AttachmentType), attachmentToCopy);
		}

		// Token: 0x06004277 RID: 17015 RVA: 0x0011B850 File Offset: 0x00119A50
		internal CoreAttachment CreateItemAttachment(StoreObjectType type)
		{
			this.CheckDisposed(null);
			EnumValidator.ThrowIfInvalid<StoreObjectType>(type, "type");
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "Storage.CoreAttachmentCollection.Create2");
			this.InitCollection("CreateItemAttachment", true);
			string containerMessageClass = ObjectClass.GetContainerMessageClass(type);
			return this.InternalCreate(new AttachmentType?(AttachmentType.EmbeddedMessage), containerMessageClass, null, null);
		}

		// Token: 0x06004278 RID: 17016 RVA: 0x0011B8A8 File Offset: 0x00119AA8
		internal bool Contains(AttachmentId id)
		{
			this.CheckDisposed(null);
			Util.ThrowOnNullArgument(id, "id");
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "Storage.CoreAttachmentCollection.Contains");
			this.InitCollection("Contains", false);
			AttachmentHandle attachmentHandle = null;
			return this.attachmentIdMap.TryGetValue(id, out attachmentHandle) && attachmentHandle != null;
		}

		// Token: 0x06004279 RID: 17017 RVA: 0x0011B904 File Offset: 0x00119B04
		internal bool Remove(AttachmentId id)
		{
			this.CheckDisposed(null);
			Util.ThrowOnNullArgument(id, "id");
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "Storage.CoreAttachmentCollection.Remove(AttachmentId)");
			this.InitCollection("Remove", true);
			AttachmentHandle attachmentHandle = null;
			return this.attachmentIdMap.TryGetValue(id, out attachmentHandle) && this.Remove(attachmentHandle);
		}

		// Token: 0x0600427A RID: 17018 RVA: 0x0011B960 File Offset: 0x00119B60
		internal void RemoveAll()
		{
			this.CheckDisposed(null);
			this.InitCollection("RemoveAll", true);
			IList<AttachmentHandle> allHandles = this.GetAllHandles();
			if (allHandles != null)
			{
				foreach (AttachmentHandle attachmentHandle in allHandles)
				{
					this.Remove(attachmentHandle);
				}
			}
			this.Reset(true);
		}

		// Token: 0x0600427B RID: 17019 RVA: 0x0011B9D0 File Offset: 0x00119BD0
		internal CoreAttachment Open(AttachmentId attachmentId)
		{
			return this.Open(attachmentId, null);
		}

		// Token: 0x0600427C RID: 17020 RVA: 0x0011B9DC File Offset: 0x00119BDC
		internal CoreAttachment Open(AttachmentId attachmentId, ICollection<PropertyDefinition> preloadProperties)
		{
			this.CheckDisposed(null);
			Util.ThrowOnNullArgument(attachmentId, "attachmentId");
			this.InitCollection("Open", false);
			AttachmentHandle handle;
			if (!this.attachmentIdMap.TryGetValue(attachmentId, out handle))
			{
				throw new ObjectNotFoundException(ServerStrings.MapiCannotOpenAttachment);
			}
			return this.InternalOpen(handle, preloadProperties);
		}

		// Token: 0x0600427D RID: 17021 RVA: 0x0011BA2A File Offset: 0x00119C2A
		internal ICollection<PropertyDefinition> GetAttachmentLoadList(ICollection<PropertyDefinition> prefetchProperties, Schema attachmentSchema)
		{
			this.CheckDisposed(null);
			prefetchProperties = InternalSchema.Combine<PropertyDefinition>((ICollection<PropertyDefinition>)this.fetchProperties, prefetchProperties);
			return InternalSchema.Combine<PropertyDefinition>(attachmentSchema.AutoloadProperties, prefetchProperties);
		}

		// Token: 0x0600427E RID: 17022 RVA: 0x0011BA52 File Offset: 0x00119C52
		internal CoreAttachment InternalCreate(AttachmentType? type)
		{
			this.CheckDisposed(null);
			this.InitCollection("InternalCreate", true);
			return this.InternalCreate(type, null, null, null);
		}

		// Token: 0x0600427F RID: 17023 RVA: 0x0011BA74 File Offset: 0x00119C74
		internal CoreAttachment InternalCreateCopy(AttachmentType? type, CoreAttachment attachmentToClone)
		{
			this.CheckDisposed(null);
			this.InitCollection("InternalCreateCopy", true);
			CoreAttachment result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				CoreAttachment coreAttachment = this.InternalCreate(type, null, attachmentToClone, null);
				disposeGuard.Add<CoreAttachment>(coreAttachment);
				int valueOrDefault = attachmentToClone.PropertyBag.GetValueOrDefault<int>(InternalSchema.RenderingPosition, -1);
				coreAttachment.PropertyBag.SetOrDeleteProperty(InternalSchema.RenderingPosition, valueOrDefault);
				disposeGuard.Success();
				result = coreAttachment;
			}
			return result;
		}

		// Token: 0x06004280 RID: 17024 RVA: 0x0011BB04 File Offset: 0x00119D04
		internal IList<AttachmentHandle> GetAllHandles()
		{
			this.CheckDisposed(null);
			this.InitCollection("AllAttachments", false);
			AttachmentHandle[] array = new AttachmentHandle[this.savedAttachmentNumberMap.Count];
			this.savedAttachmentNumberMap.Values.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06004281 RID: 17025 RVA: 0x0011BB48 File Offset: 0x00119D48
		internal void UpdateAttachmentId(AttachmentId attachmentId, int attachmentNumber)
		{
			this.CheckDisposed(null);
			if (this.isInitialized)
			{
				AttachmentHandle attachmentHandle = this.GetAttachmentHandle(attachmentNumber);
				attachmentHandle.AttachmentId = attachmentId;
				this.attachmentIdMap[attachmentId] = attachmentHandle;
			}
		}

		// Token: 0x06004282 RID: 17026 RVA: 0x0011BB80 File Offset: 0x00119D80
		internal void OnBeforeAttachmentSave(AttachmentPropertyBag attachmentBag)
		{
			this.CheckDisposed(null);
			AttachmentHandle attachmentHandle = null;
			if (this.newAttachmentNumberMap.TryGetValue(attachmentBag.AttachmentNumber, out attachmentHandle))
			{
				attachmentHandle.UpdateProperties(attachmentBag);
				return;
			}
			if (this.savedAttachmentNumberMap.TryGetValue(attachmentBag.AttachmentNumber, out attachmentHandle))
			{
				attachmentHandle.UpdateProperties(attachmentBag);
			}
		}

		// Token: 0x06004283 RID: 17027 RVA: 0x0011BBD0 File Offset: 0x00119DD0
		internal void OnAfterAttachmentSave(int attachmentNumber)
		{
			this.CheckDisposed(null);
			AttachmentHandle value = null;
			if (this.newAttachmentNumberMap.TryGetValue(attachmentNumber, out value))
			{
				this.newAttachmentNumberMap.Remove(attachmentNumber);
				this.savedAttachmentNumberMap.Add(attachmentNumber, value);
			}
			this.isDirty = true;
			this.IsClonedFromAnExistingAttachmentCollection = false;
		}

		// Token: 0x06004284 RID: 17028 RVA: 0x0011BC20 File Offset: 0x00119E20
		internal void GetCharsetDetectionData(StringBuilder stringBuilder)
		{
			this.CheckDisposed(null);
			this.InitCollection("GetCharsetDetectionData", false);
			foreach (AttachmentHandle attachmentHandle in this.savedAttachmentNumberMap.Values)
			{
				stringBuilder.Append(attachmentHandle.CharsetDetectionData);
			}
		}

		// Token: 0x06004285 RID: 17029 RVA: 0x0011BC94 File Offset: 0x00119E94
		internal bool Exists(int attachmentNumber)
		{
			this.CheckDisposed(null);
			this.InitCollection("Exists", false);
			return this.savedAttachmentNumberMap.ContainsKey(attachmentNumber) || this.newAttachmentNumberMap.ContainsKey(attachmentNumber);
		}

		// Token: 0x06004286 RID: 17030 RVA: 0x0011BCC5 File Offset: 0x00119EC5
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<CoreAttachmentCollection>(this);
		}

		// Token: 0x06004287 RID: 17031 RVA: 0x0011BCCD File Offset: 0x00119ECD
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.Reset(false);
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x06004288 RID: 17032 RVA: 0x0011BCE0 File Offset: 0x00119EE0
		private static NativeStorePropertyDefinition[] CreatePrefetchPropertySet<T>(ICollection<T> propertySet) where T : PropertyDefinition
		{
			return StorePropertyDefinition.GetNativePropertyDefinitions<T>(PropertyDependencyType.AllRead, propertySet).ToArray<NativeStorePropertyDefinition>();
		}

		// Token: 0x06004289 RID: 17033 RVA: 0x0011BCF0 File Offset: 0x00119EF0
		private static NativeStorePropertyDefinition[] CreatePrefetchPropertySet<T>(ICollection<NativeStorePropertyDefinition> oldSet, ICollection<T> additionalProperties) where T : PropertyDefinition
		{
			HashSet<NativeStorePropertyDefinition> hashSet = new HashSet<NativeStorePropertyDefinition>(oldSet);
			ICollection<NativeStorePropertyDefinition> nativePropertyDefinitions = StorePropertyDefinition.GetNativePropertyDefinitions<T>(PropertyDependencyType.AllRead, additionalProperties);
			foreach (NativeStorePropertyDefinition item in nativePropertyDefinitions)
			{
				hashSet.TryAdd(item);
			}
			return hashSet.ToArray();
		}

		// Token: 0x0600428A RID: 17034 RVA: 0x0011BD50 File Offset: 0x00119F50
		internal void Reset()
		{
			this.CheckDisposed(null);
			this.Reset(false);
		}

		// Token: 0x0600428B RID: 17035 RVA: 0x0011BD60 File Offset: 0x00119F60
		internal void OnAfterCoreItemSave(SaveResult saveResult)
		{
			if (saveResult == SaveResult.SuccessWithConflictResolution)
			{
				this.Reset();
			}
			else if (saveResult == SaveResult.IrresolvableConflict)
			{
				return;
			}
			if (this.coreItem.TopLevelItem == null && this.newAttachmentNumberMap.Count == 0)
			{
				this.isDirty = false;
			}
			this.IsClonedFromAnExistingAttachmentCollection = false;
		}

		// Token: 0x0600428C RID: 17036 RVA: 0x0011BD9B File Offset: 0x00119F9B
		internal void OpenAsReadWrite()
		{
			this.CheckDisposed(null);
			this.forceReadOnly = false;
		}

		// Token: 0x0600428D RID: 17037 RVA: 0x0011BDAC File Offset: 0x00119FAC
		private CoreAttachment InternalCreate(AttachmentType? type, string itemClass, CoreAttachment attachmentToClone, IItem itemToAttach)
		{
			this.InitCollection("InternalCreate", true);
			AttachmentPropertyBag attachmentPropertyBag = null;
			CoreAttachment coreAttachment = null;
			bool flag = false;
			try
			{
				bool flag2 = false;
				if (attachmentToClone != null)
				{
					flag2 = this.attachmentProvider.SupportsCreateClone(attachmentToClone.PropertyBag);
				}
				attachmentPropertyBag = this.InternalCreateAttachmentPropertyBag(type, flag2 ? attachmentToClone : null, itemToAttach);
				if (itemClass != null)
				{
					((IDirectPropertyBag)attachmentPropertyBag).SetValue(InternalSchema.ItemClass, itemClass);
				}
				AttachmentHandle attachmentHandle = new AttachmentHandle(attachmentPropertyBag.AttachmentNumber);
				coreAttachment = new CoreAttachment(this, attachmentPropertyBag, Origin.New);
				attachmentPropertyBag = null;
				if (attachmentToClone != null && !flag2)
				{
					coreAttachment.CopyAttachmentContentFrom(attachmentToClone);
				}
				this.newAttachmentNumberMap.Add(attachmentHandle.AttachNumber, attachmentHandle);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					if (coreAttachment != null)
					{
						coreAttachment.Dispose();
						coreAttachment = null;
					}
					if (attachmentPropertyBag != null)
					{
						attachmentPropertyBag.Dispose();
						attachmentPropertyBag = null;
					}
				}
			}
			return coreAttachment;
		}

		// Token: 0x0600428E RID: 17038 RVA: 0x0011BE70 File Offset: 0x0011A070
		private AttachmentPropertyBag InternalCreateAttachmentPropertyBag(AttachmentType? type, CoreAttachment attachmentToClone, IItem itemToAttach)
		{
			this.InitCollection("InternalCreateAttachmentPropertyBag", true);
			bool flag = false;
			int attachmentNumber = -1;
			PersistablePropertyBag persistablePropertyBag = null;
			AttachmentPropertyBag attachmentPropertyBag = null;
			try
			{
				Schema attachmentSchema = CoreAttachmentCollection.GetAttachmentSchema(type);
				ICollection<PropertyDefinition> prefetchProperties = InternalSchema.Combine<PropertyDefinition>(attachmentSchema.AutoloadProperties, (ICollection<PropertyDefinition>)this.fetchProperties);
				persistablePropertyBag = this.attachmentProvider.CreateAttachment(prefetchProperties, attachmentToClone, itemToAttach, out attachmentNumber);
				attachmentPropertyBag = new AttachmentPropertyBag(this.attachmentProvider, attachmentNumber, persistablePropertyBag, true);
				attachmentPropertyBag.ExTimeZone = this.ExTimeZone;
				if (type != null)
				{
					int num = CoreAttachment.AttachmentTypeToAttachMethod(type.Value);
					((IDirectPropertyBag)attachmentPropertyBag).SetValue(InternalSchema.AttachMethod, num);
				}
				this.isDirty = true;
				this.IsClonedFromAnExistingAttachmentCollection = false;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					if (attachmentPropertyBag != null)
					{
						attachmentPropertyBag.Dispose();
						attachmentPropertyBag = null;
					}
					if (persistablePropertyBag != null)
					{
						persistablePropertyBag.Dispose();
						persistablePropertyBag = null;
					}
				}
			}
			return attachmentPropertyBag;
		}

		// Token: 0x0600428F RID: 17039 RVA: 0x0011BF48 File Offset: 0x0011A148
		private CoreAttachment InternalOpen(AttachmentHandle handle, ICollection<PropertyDefinition> preloadProperties)
		{
			PropertyBag propertyBag = handle.GetAndRemoveCachedPropertyBag();
			if (propertyBag == null)
			{
				PropertyBag[] array = this.attachmentProvider.QueryAttachmentTable(this.fetchProperties.ToArray<NativeStorePropertyDefinition>());
				if (array == null || array.Length == 0)
				{
					throw new InvalidOperationException("Attachment table is empty.");
				}
				foreach (PropertyBag propertyBag2 in array)
				{
					int num = (int)propertyBag2[InternalSchema.AttachNum];
					if (handle.AttachNumber == num)
					{
						propertyBag = propertyBag2;
					}
					else
					{
						AttachmentHandle attachmentHandle = null;
						if (this.savedAttachmentNumberMap.TryGetValue(num, out attachmentHandle))
						{
							attachmentHandle.SetCachedPropertyBag(propertyBag2);
						}
					}
				}
			}
			if (propertyBag == null)
			{
				throw new InvalidOperationException("Attachment instance doesn't exist.");
			}
			CoreAttachment result = null;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				AttachmentPropertyBag attachmentPropertyBag = new AttachmentPropertyBag(this.attachmentProvider, handle.AttachNumber, propertyBag, this.fetchProperties, false);
				disposeGuard.Add<AttachmentPropertyBag>(attachmentPropertyBag);
				attachmentPropertyBag.ExTimeZone = this.ExTimeZone;
				if (preloadProperties == (ICollection<PropertyDefinition>)InternalSchema.ContentConversionProperties || (preloadProperties != null && preloadProperties.Count != 0))
				{
					attachmentPropertyBag.Load(preloadProperties);
				}
				this.UpdateAttachmentId(attachmentPropertyBag.AttachmentId, handle.AttachNumber);
				result = new CoreAttachment(this, attachmentPropertyBag, Origin.Existing);
				disposeGuard.Success();
			}
			return result;
		}

		// Token: 0x06004290 RID: 17040 RVA: 0x0011C090 File Offset: 0x0011A290
		private void Reset(bool newAttachmentsOnly)
		{
			if (this.isInitialized)
			{
				this.newAttachmentNumberMap.Clear();
				if (!newAttachmentsOnly)
				{
					this.savedAttachmentNumberMap.Clear();
					this.attachmentIdMap.Clear();
					this.isInitialized = false;
					this.IsClonedFromAnExistingAttachmentCollection = false;
				}
			}
		}

		// Token: 0x06004291 RID: 17041 RVA: 0x0011C0CC File Offset: 0x0011A2CC
		private void InitCollection(string methodName, bool forWrite)
		{
			if (forWrite && this.IsReadOnly)
			{
				ExTraceGlobals.StorageTracer.TraceError<string>((long)this.GetHashCode(), "CoreAttachmentCollection::{0}. Cannot modify the collection because it's read only.", methodName);
				throw new AccessDeniedException(ServerStrings.ExItemIsOpenedInReadOnlyMode);
			}
			if (this.isInitialized)
			{
				return;
			}
			this.UpdateCollection();
			this.isInitialized = true;
		}

		// Token: 0x06004292 RID: 17042 RVA: 0x0011C11C File Offset: 0x0011A31C
		private void UpdateCollection()
		{
			PropertyBag[] array = this.attachmentProvider.QueryAttachmentTable(this.fetchProperties.ToArray<NativeStorePropertyDefinition>());
			if (array == null || array.Length == 0)
			{
				return;
			}
			foreach (PropertyBag propertyBag in array)
			{
				int num = (int)propertyBag[InternalSchema.AttachNum];
				AttachmentHandle attachmentHandle = null;
				if (!this.savedAttachmentNumberMap.TryGetValue(num, out attachmentHandle))
				{
					attachmentHandle = new AttachmentHandle(num);
					this.savedAttachmentNumberMap.Add(num, attachmentHandle);
				}
				using (AttachmentPropertyBag attachmentPropertyBag = new AttachmentPropertyBag(this.attachmentProvider, num, propertyBag, this.fetchProperties, false))
				{
					attachmentHandle.UpdateProperties(attachmentPropertyBag);
					this.attachmentIdMap[attachmentHandle.AttachmentId] = attachmentHandle;
					attachmentHandle.SetCachedPropertyBag(propertyBag);
				}
			}
		}

		// Token: 0x06004293 RID: 17043 RVA: 0x0011C1F8 File Offset: 0x0011A3F8
		private AttachmentHandle GetAttachmentHandle(int attachmentNumber)
		{
			AttachmentHandle result = null;
			if (!this.savedAttachmentNumberMap.TryGetValue(attachmentNumber, out result))
			{
				throw new InvalidOperationException();
			}
			return result;
		}

		// Token: 0x04002486 RID: 9350
		private static readonly NativeStorePropertyDefinition[] PrefetchPropertySet = CoreAttachmentCollection.CreatePrefetchPropertySet<PropertyDefinition>(AttachmentTableSchema.Instance.AutoloadProperties);

		// Token: 0x04002487 RID: 9351
		private readonly ICoreItem coreItem;

		// Token: 0x04002488 RID: 9352
		private readonly SortedDictionary<int, AttachmentHandle> savedAttachmentNumberMap = new SortedDictionary<int, AttachmentHandle>();

		// Token: 0x04002489 RID: 9353
		private readonly Dictionary<int, AttachmentHandle> newAttachmentNumberMap = new Dictionary<int, AttachmentHandle>();

		// Token: 0x0400248A RID: 9354
		private readonly Dictionary<AttachmentId, AttachmentHandle> attachmentIdMap = new Dictionary<AttachmentId, AttachmentHandle>();

		// Token: 0x0400248B RID: 9355
		private readonly IAttachmentProvider attachmentProvider;

		// Token: 0x0400248C RID: 9356
		private bool forceReadOnly;

		// Token: 0x0400248D RID: 9357
		private bool isInitialized;

		// Token: 0x0400248E RID: 9358
		private NativeStorePropertyDefinition[] fetchProperties;

		// Token: 0x0400248F RID: 9359
		private bool isDirty;
	}
}
