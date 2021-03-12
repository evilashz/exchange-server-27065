using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000355 RID: 853
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class InMemoryAttachmentProvider : DisposableObject, IAttachmentProvider, IDisposable
	{
		// Token: 0x060025F2 RID: 9714 RVA: 0x00097DF0 File Offset: 0x00095FF0
		internal InMemoryAttachmentProvider()
		{
			this.attachmentCounter = 0;
			this.savedAttachmentList = new Dictionary<int, PersistablePropertyBag>();
			this.newAttachmentList = new Dictionary<int, PersistablePropertyBag>();
			this.attachedItems = new Dictionary<int, CoreItem>();
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x00097E20 File Offset: 0x00096020
		public void SetCollection(CoreAttachmentCollection attachmentCollection)
		{
			this.attachmentCollection = attachmentCollection;
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x060025F4 RID: 9716 RVA: 0x00097E29 File Offset: 0x00096029
		public NativeStorePropertyDefinition[] AttachmentTablePropertyList
		{
			get
			{
				this.CheckDisposed(null);
				return this.AttachmentCollection.AttachmentTablePropertyList;
			}
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x00097E3D File Offset: 0x0009603D
		public bool SupportsCreateClone(AttachmentPropertyBag propertyBag)
		{
			return false;
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x00097E40 File Offset: 0x00096040
		public void OnAttachmentLoad(AttachmentPropertyBag attachmentBag)
		{
			this.CheckDisposed(null);
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x00097E49 File Offset: 0x00096049
		public void OnBeforeAttachmentSave(AttachmentPropertyBag attachmentBag)
		{
			this.CheckDisposed(null);
			this.AttachmentCollection.OnBeforeAttachmentSave(attachmentBag);
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x00097E5E File Offset: 0x0009605E
		public bool ExistsInCollection(AttachmentPropertyBag attachmentBag)
		{
			this.CheckDisposed(null);
			return this.AttachmentCollection.Exists(attachmentBag.AttachmentNumber);
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x00097E78 File Offset: 0x00096078
		public void OnAfterAttachmentSave(AttachmentPropertyBag attachmentBag)
		{
			this.CheckDisposed(null);
			int attachmentNumber = attachmentBag.AttachmentNumber;
			this.AttachmentCollection.OnAfterAttachmentSave(attachmentNumber);
			PersistablePropertyBag persistablePropertyBag = null;
			if (this.newAttachmentList.TryGetValue(attachmentNumber, out persistablePropertyBag))
			{
				this.newAttachmentList.Remove(attachmentNumber);
				this.savedAttachmentList.Add(attachmentNumber, attachmentBag.PersistablePropertyBag);
				byte[] bytes = BitConverter.GetBytes(attachmentNumber);
				persistablePropertyBag[InternalSchema.RecordKey] = bytes;
				this.AttachmentCollection.UpdateAttachmentId(attachmentBag.AttachmentId, attachmentNumber);
			}
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x00097EF8 File Offset: 0x000960F8
		public void OnAttachmentDisconnected(AttachmentPropertyBag attachmentBag, PersistablePropertyBag dataPropertyBag)
		{
			this.CheckDisposed(null);
			PersistablePropertyBag persistablePropertyBag = null;
			int attachmentNumber = attachmentBag.AttachmentNumber;
			if (this.newAttachmentList.TryGetValue(attachmentNumber, out persistablePropertyBag))
			{
				this.newAttachmentList.Remove(attachmentNumber);
				attachmentBag.Dispose();
			}
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x00097F38 File Offset: 0x00096138
		public void OnCollectionDisposed(AttachmentPropertyBag attachmentBag, PersistablePropertyBag dataPropertyBag)
		{
			this.CheckDisposed(null);
			int attachmentNumber = attachmentBag.AttachmentNumber;
			this.newAttachmentList.Remove(attachmentNumber);
			if (dataPropertyBag != null)
			{
				dataPropertyBag.Dispose();
			}
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x00097F6C File Offset: 0x0009616C
		public PersistablePropertyBag OpenAttachment(ICollection<PropertyDefinition> prefetchProperties, AttachmentPropertyBag attachmentBag)
		{
			this.CheckDisposed(null);
			int attachmentNumber = attachmentBag.AttachmentNumber;
			PersistablePropertyBag persistablePropertyBag = null;
			if (!this.savedAttachmentList.TryGetValue(attachmentNumber, out persistablePropertyBag))
			{
				throw new InvalidOperationException("InMemoryAttachmentProvider::OpenAttachment - Invalid attachment number");
			}
			persistablePropertyBag.Load(prefetchProperties);
			return persistablePropertyBag;
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x00097FAC File Offset: 0x000961AC
		public PersistablePropertyBag CreateAttachment(ICollection<PropertyDefinition> propertiesToLoad, CoreAttachment attachmentToClone, IItem itemToAttach, out int attachmentNumber)
		{
			this.CheckDisposed(null);
			InMemoryPersistablePropertyBag inMemoryPersistablePropertyBag = new InMemoryPersistablePropertyBag(propertiesToLoad);
			inMemoryPersistablePropertyBag.ExTimeZone = this.ExTimeZone;
			if (attachmentToClone != null)
			{
				throw new NotSupportedException("CreateAttachment for copied attachments is not supported");
			}
			attachmentNumber = this.attachmentCounter++;
			inMemoryPersistablePropertyBag[InternalSchema.AttachNum] = attachmentNumber;
			this.newAttachmentList.Add(attachmentNumber, inMemoryPersistablePropertyBag);
			if (itemToAttach != null)
			{
				string text = itemToAttach.TryGetProperty(InternalSchema.ItemClass) as string;
				Schema schema = (text != null) ? ObjectClass.GetSchema(text) : ItemSchema.Instance;
				propertiesToLoad = InternalSchema.Combine<PropertyDefinition>(schema.AutoloadProperties, propertiesToLoad);
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					PersistablePropertyBag persistablePropertyBag = new InMemoryPersistablePropertyBag(propertiesToLoad);
					disposeGuard.Add<PersistablePropertyBag>(persistablePropertyBag);
					persistablePropertyBag.ExTimeZone = this.ExTimeZone;
					CoreItem coreItem = new CoreItem(null, persistablePropertyBag, null, null, Origin.New, ItemLevel.Attached, propertiesToLoad, ItemBindOption.LoadRequiredPropertiesOnly);
					disposeGuard.Add<CoreItem>(coreItem);
					CoreItem.CopyItemContent(itemToAttach.CoreItem, coreItem);
					this.attachedItems.Add(attachmentNumber, coreItem);
					disposeGuard.Success();
				}
			}
			return inMemoryPersistablePropertyBag;
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x000980D8 File Offset: 0x000962D8
		public ICoreItem OpenAttachedItem(ICollection<PropertyDefinition> propertiesToLoad, AttachmentPropertyBag attachmentBag, bool isNew)
		{
			this.CheckDisposed(null);
			CoreItem coreItem = null;
			int attachmentNumber = attachmentBag.AttachmentNumber;
			if (this.attachedItems.TryGetValue(attachmentNumber, out coreItem))
			{
				string text = coreItem.PropertyBag.TryGetProperty(InternalSchema.ItemClass) as string;
				Schema schema = (text != null) ? ObjectClass.GetSchema(text) : MessageItemSchema.Instance;
				propertiesToLoad = InternalSchema.Combine<PropertyDefinition>(schema.AutoloadProperties, propertiesToLoad);
				coreItem.PropertyBag.Load(propertiesToLoad);
			}
			else
			{
				if (!isNew)
				{
					throw new ObjectNotFoundException(ServerStrings.MapiCannotOpenEmbeddedMessage);
				}
				string text2 = attachmentBag.TryGetProperty(InternalSchema.ItemClass) as string;
				Schema schema2 = (text2 != null) ? ObjectClass.GetSchema(text2) : MessageItemSchema.Instance;
				propertiesToLoad = InternalSchema.Combine<PropertyDefinition>(schema2.AutoloadProperties, propertiesToLoad);
				coreItem = new CoreItem(null, new InMemoryPersistablePropertyBag(propertiesToLoad)
				{
					ExTimeZone = this.ExTimeZone
				}, StoreObjectId.DummyId, null, Origin.New, ItemLevel.Attached, propertiesToLoad, ItemBindOption.LoadRequiredPropertiesOnly);
				if (text2 != null)
				{
					coreItem.PropertyBag[InternalSchema.ItemClass] = text2;
				}
				this.attachedItems.Add(attachmentNumber, coreItem);
			}
			return new CoreItemWrapper(coreItem);
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x000981E4 File Offset: 0x000963E4
		public void DeleteAttachment(int attachmentNumber)
		{
			this.CheckDisposed(null);
			this.savedAttachmentList.Remove(attachmentNumber);
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x000981FC File Offset: 0x000963FC
		public PropertyBag[] QueryAttachmentTable(NativeStorePropertyDefinition[] properties)
		{
			this.CheckDisposed(null);
			if (this.savedAttachmentList.Count == 0)
			{
				return null;
			}
			Dictionary<StorePropertyDefinition, int> propertyPositionsDictionary = QueryResultPropertyBag.CreatePropertyPositionsDictionary(properties);
			PropertyBag[] array = new PropertyBag[this.savedAttachmentList.Count];
			int num = 0;
			foreach (KeyValuePair<int, PersistablePropertyBag> keyValuePair in this.savedAttachmentList)
			{
				keyValuePair.Value[AttachmentSchema.AttachNum] = keyValuePair.Key;
				object[] array2 = new object[properties.Length];
				for (int num2 = 0; num2 != properties.Length; num2++)
				{
					NativeStorePropertyDefinition propertyDefinition = properties[num2];
					array2[num2] = ((IDirectPropertyBag)keyValuePair.Value).GetValue(propertyDefinition);
				}
				QueryResultPropertyBag queryResultPropertyBag = new QueryResultPropertyBag(null, propertyPositionsDictionary);
				queryResultPropertyBag.ExTimeZone = this.ExTimeZone;
				queryResultPropertyBag.ReturnErrorsOnTruncatedProperties = true;
				queryResultPropertyBag.SetQueryResultRow(array2);
				array[num++] = queryResultPropertyBag;
			}
			return array;
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x06002601 RID: 9729 RVA: 0x00098308 File Offset: 0x00096508
		private CoreAttachmentCollection AttachmentCollection
		{
			get
			{
				return this.attachmentCollection;
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06002602 RID: 9730 RVA: 0x00098310 File Offset: 0x00096510
		private ExTimeZone ExTimeZone
		{
			get
			{
				return this.AttachmentCollection.ExTimeZone;
			}
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x00098320 File Offset: 0x00096520
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				foreach (KeyValuePair<int, PersistablePropertyBag> keyValuePair in this.newAttachmentList)
				{
					PersistablePropertyBag value = keyValuePair.Value;
					value.Dispose();
				}
				foreach (KeyValuePair<int, PersistablePropertyBag> keyValuePair2 in this.savedAttachmentList)
				{
					PersistablePropertyBag value2 = keyValuePair2.Value;
					value2.Dispose();
				}
				foreach (KeyValuePair<int, CoreItem> keyValuePair3 in this.attachedItems)
				{
					CoreItem value3 = keyValuePair3.Value;
					value3.Dispose();
				}
				this.newAttachmentList.Clear();
				this.savedAttachmentList.Clear();
				this.attachedItems.Clear();
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x00098440 File Offset: 0x00096640
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<InMemoryAttachmentProvider>(this);
		}

		// Token: 0x040016D7 RID: 5847
		private CoreAttachmentCollection attachmentCollection;

		// Token: 0x040016D8 RID: 5848
		private int attachmentCounter;

		// Token: 0x040016D9 RID: 5849
		private Dictionary<int, PersistablePropertyBag> savedAttachmentList;

		// Token: 0x040016DA RID: 5850
		private Dictionary<int, PersistablePropertyBag> newAttachmentList;

		// Token: 0x040016DB RID: 5851
		private Dictionary<int, CoreItem> attachedItems;
	}
}
