using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200034C RID: 844
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AttachmentCollection : IAttachmentCollection, IEnumerable<AttachmentHandle>, IEnumerable
	{
		// Token: 0x06002579 RID: 9593 RVA: 0x000969F3 File Offset: 0x00094BF3
		public AttachmentCollection(Item parent) : this(parent, false)
		{
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x000969FD File Offset: 0x00094BFD
		public AttachmentCollection(Item parent, bool hideCalendarExceptions)
		{
			this.parent = parent;
			this.hideCalendarExceptions = hideCalendarExceptions;
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x00096A13 File Offset: 0x00094C13
		public void Load(params PropertyDefinition[] propertyList)
		{
			this.CoreAttachmentCollection.Load(propertyList);
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x00096A24 File Offset: 0x00094C24
		public ItemAttachment AddExistingItem(IItem item)
		{
			Util.ThrowOnNullArgument(item, "item");
			ItemAttachment result = null;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				CoreAttachment coreAttachment = this.CoreAttachmentCollection.CreateFromExistingItem(item);
				disposeGuard.Add<CoreAttachment>(coreAttachment);
				result = (ItemAttachment)AttachmentCollection.CreateTypedAttachment(coreAttachment, new AttachmentType?(AttachmentType.EmbeddedMessage));
				disposeGuard.Success();
			}
			return result;
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x00096A98 File Offset: 0x00094C98
		public IAttachment CreateIAttachment(AttachmentType type)
		{
			return this.Create(type);
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x00096AA1 File Offset: 0x00094CA1
		public IAttachment CreateIAttachment(AttachmentType type, IAttachment attachment)
		{
			return this.Create(new AttachmentType?(type), (Attachment)attachment);
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x00096AB8 File Offset: 0x00094CB8
		public Attachment Create(AttachmentType type)
		{
			EnumValidator.ThrowIfInvalid<AttachmentType>(type, new AttachmentType[]
			{
				AttachmentType.Stream,
				AttachmentType.EmbeddedMessage,
				AttachmentType.Ole,
				AttachmentType.Reference
			});
			Attachment result = null;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				CoreAttachment coreAttachment = this.CoreAttachmentCollection.Create(type);
				disposeGuard.Add<CoreAttachment>(coreAttachment);
				result = AttachmentCollection.CreateTypedAttachment(coreAttachment, new AttachmentType?(type));
				disposeGuard.Success();
			}
			return result;
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x00096B38 File Offset: 0x00094D38
		public Attachment Create(AttachmentType? type, Attachment clone)
		{
			if (type != null)
			{
				EnumValidator.ThrowIfInvalid<AttachmentType>(type.Value, "type");
			}
			Util.ThrowOnNullArgument(clone, "clone");
			Attachment result = null;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				CoreAttachment coreAttachment = this.CoreAttachmentCollection.InternalCreateCopy(type, clone.CoreAttachment);
				disposeGuard.Add<CoreAttachment>(coreAttachment);
				result = AttachmentCollection.CreateTypedAttachment(coreAttachment, type);
				disposeGuard.Success();
			}
			return result;
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x00096BC0 File Offset: 0x00094DC0
		public ItemAttachment Create(StoreObjectType type)
		{
			EnumValidator.ThrowIfInvalid<StoreObjectType>(type, "type");
			ItemAttachment result = null;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				CoreAttachment coreAttachment = this.CoreAttachmentCollection.CreateItemAttachment(type);
				disposeGuard.Add<CoreAttachment>(coreAttachment);
				result = (ItemAttachment)AttachmentCollection.CreateTypedAttachment(coreAttachment, new AttachmentType?(AttachmentType.EmbeddedMessage));
				disposeGuard.Success();
			}
			return result;
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x00096C34 File Offset: 0x00094E34
		public IAttachment OpenIAttachment(AttachmentHandle handle)
		{
			return this.Open(handle);
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x00096C3D File Offset: 0x00094E3D
		public Attachment Open(AttachmentId id)
		{
			return this.Open(id, null);
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x00096C48 File Offset: 0x00094E48
		public Attachment Open(AttachmentId id, ICollection<PropertyDefinition> propertyDefinitions)
		{
			Util.ThrowOnNullArgument(id, "id");
			return AttachmentCollection.CreateTypedAttachment(this.CoreAttachmentCollection.Open(id, propertyDefinitions), null);
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x00096C7B File Offset: 0x00094E7B
		public Attachment Open(AttachmentHandle handle)
		{
			return this.Open(handle, null);
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x00096C88 File Offset: 0x00094E88
		public Attachment Open(AttachmentHandle handle, ICollection<PropertyDefinition> propertyDefinitions)
		{
			Util.ThrowOnNullArgument(handle, "handle");
			Attachment result = null;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				CoreAttachment coreAttachment = this.CoreAttachmentCollection.Open(handle, propertyDefinitions);
				disposeGuard.Add<CoreAttachment>(coreAttachment);
				result = AttachmentCollection.CreateTypedAttachment(coreAttachment, null);
				disposeGuard.Success();
			}
			return result;
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x00096CFC File Offset: 0x00094EFC
		public Attachment Open(AttachmentHandle handle, AttachmentType type)
		{
			Util.ThrowOnNullArgument(handle, "handle");
			EnumValidator.ThrowIfInvalid<AttachmentType>(type, "type");
			return this.Open(handle, new AttachmentType?(type), null);
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x00096D24 File Offset: 0x00094F24
		internal static Attachment CreateTypedAttachment(CoreAttachment attachment, AttachmentType? type)
		{
			int? num = ((IDirectPropertyBag)attachment.PropertyBag).GetValue(InternalSchema.AttachMethod) as int?;
			if (num != null)
			{
				switch (num.Value)
				{
				case 2:
				case 3:
				case 4:
				case 7:
					if (type != null && !(type == AttachmentType.Reference))
					{
						return null;
					}
					return new ReferenceAttachment(attachment);
				case 5:
					if (type != null && !(type == AttachmentType.EmbeddedMessage))
					{
						return null;
					}
					return new ItemAttachment(attachment);
				case 6:
					if (type != null && !(type == AttachmentType.Ole))
					{
						return null;
					}
					return new OleAttachment(attachment);
				}
			}
			if (type != null && !(type == AttachmentType.Stream))
			{
				return null;
			}
			return new StreamAttachment(attachment);
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x00096E2C File Offset: 0x0009502C
		internal Attachment Open(AttachmentHandle handle, AttachmentType? type, ICollection<PropertyDefinition> propertyDefinitions)
		{
			Util.ThrowOnNullArgument(handle, "handle");
			if (type != null)
			{
				EnumValidator.ThrowIfInvalid<AttachmentType>(type.Value, "type");
			}
			Attachment attachment = null;
			CoreAttachment coreAttachment = null;
			try
			{
				coreAttachment = this.CoreAttachmentCollection.Open(handle, propertyDefinitions);
				attachment = AttachmentCollection.CreateTypedAttachment(coreAttachment, type);
			}
			finally
			{
				if (attachment == null && coreAttachment != null)
				{
					coreAttachment.Dispose();
				}
			}
			return attachment;
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x00096E98 File Offset: 0x00095098
		internal Attachment TryOpenFirstAttachment(AttachmentType attachmentType)
		{
			using (IEnumerator<AttachmentHandle> enumerator = this.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					AttachmentHandle handle = enumerator.Current;
					return this.Open(handle, new AttachmentType?(attachmentType), null);
				}
			}
			return null;
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x00096EF0 File Offset: 0x000950F0
		public bool Contains(AttachmentId attachmentId)
		{
			Util.ThrowOnNullArgument(attachmentId, "attachmentId");
			return this.CoreAttachmentCollection.Contains(attachmentId);
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x00096F0C File Offset: 0x0009510C
		public bool Remove(AttachmentId attachmentId)
		{
			Util.ThrowOnNullArgument(attachmentId, "attachmentId");
			bool result = this.CoreAttachmentCollection.Remove(attachmentId);
			CalendarItemBase calendarItemBase = this.ContainerItem as CalendarItemBase;
			if (calendarItemBase != null)
			{
				calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(63349U, LastChangeAction.AttachmentRemoved);
			}
			return result;
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x00096F54 File Offset: 0x00095154
		public bool Remove(AttachmentHandle handle)
		{
			Util.ThrowOnNullArgument(handle, "handle");
			bool result = this.CoreAttachmentCollection.Remove(handle);
			CalendarItemBase calendarItemBase = this.ContainerItem as CalendarItemBase;
			if (calendarItemBase != null)
			{
				calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(38773U, LastChangeAction.AttachmentRemoved);
			}
			return result;
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x00096F9C File Offset: 0x0009519C
		public void RemoveAll()
		{
			if (this.hideCalendarExceptions)
			{
				IList<AttachmentHandle> handles = this.GetHandles();
				using (IEnumerator<AttachmentHandle> enumerator = handles.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AttachmentHandle handle = enumerator.Current;
						this.Remove(handle);
					}
					goto IL_46;
				}
			}
			this.CoreAttachmentCollection.RemoveAll();
			IL_46:
			CalendarItemBase calendarItemBase = this.ContainerItem as CalendarItemBase;
			if (calendarItemBase != null)
			{
				calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(55157U, LastChangeAction.AttachmentRemoved);
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x0600258F RID: 9615 RVA: 0x00097020 File Offset: 0x00095220
		public bool IsReadOnly
		{
			get
			{
				return this.CoreAttachmentCollection.IsReadOnly;
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06002590 RID: 9616 RVA: 0x00097030 File Offset: 0x00095230
		public int Count
		{
			get
			{
				if (this.hideCalendarExceptions)
				{
					int num = 0;
					foreach (AttachmentHandle handle in this.CoreAttachmentCollection)
					{
						if (!CoreAttachmentCollection.IsCalendarException(handle))
						{
							num++;
						}
					}
					return num;
				}
				return this.CoreAttachmentCollection.Count;
			}
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x0009709C File Offset: 0x0009529C
		public IEnumerator<AttachmentHandle> GetEnumerator()
		{
			if (this.hideCalendarExceptions)
			{
				return new AttachmentCollection.CalendarPublicAttachmentEnumerator(this.CoreAttachmentCollection.GetEnumerator());
			}
			return this.CoreAttachmentCollection.GetEnumerator();
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x000970C2 File Offset: 0x000952C2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06002593 RID: 9619 RVA: 0x000970CA File Offset: 0x000952CA
		internal Item ContainerItem
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x000970D4 File Offset: 0x000952D4
		public IList<AttachmentHandle> GetHandles()
		{
			if (this.hideCalendarExceptions)
			{
				List<AttachmentHandle> list = new List<AttachmentHandle>(this.CoreAttachmentCollection.Count);
				foreach (AttachmentHandle attachmentHandle in this.CoreAttachmentCollection)
				{
					if (!CoreAttachmentCollection.IsCalendarException(attachmentHandle))
					{
						list.Add(attachmentHandle);
					}
				}
				return list;
			}
			return this.GetAllHandles();
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x0009714C File Offset: 0x0009534C
		public IList<AttachmentHandle> GetAllHandles()
		{
			return this.CoreAttachmentCollection.GetAllHandles();
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06002596 RID: 9622 RVA: 0x00097159 File Offset: 0x00095359
		internal bool IsDirty
		{
			get
			{
				return this.CoreAttachmentCollection.IsDirty;
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06002597 RID: 9623 RVA: 0x00097166 File Offset: 0x00095366
		public CoreAttachmentCollection CoreAttachmentCollection
		{
			get
			{
				return this.parent.CoreItem.AttachmentCollection;
			}
		}

		// Token: 0x040016A8 RID: 5800
		private readonly Item parent;

		// Token: 0x040016A9 RID: 5801
		private readonly bool hideCalendarExceptions;

		// Token: 0x0200034D RID: 845
		private class CalendarPublicAttachmentEnumerator : IEnumerator<AttachmentHandle>, IDisposable, IEnumerator
		{
			// Token: 0x06002598 RID: 9624 RVA: 0x00097178 File Offset: 0x00095378
			public CalendarPublicAttachmentEnumerator(IEnumerator<AttachmentHandle> list)
			{
				this.list = list;
				this.isDisposed = false;
			}

			// Token: 0x17000C8E RID: 3214
			// (get) Token: 0x06002599 RID: 9625 RVA: 0x0009718E File Offset: 0x0009538E
			AttachmentHandle IEnumerator<AttachmentHandle>.Current
			{
				get
				{
					this.CheckDisposed();
					return this.list.Current;
				}
			}

			// Token: 0x17000C8F RID: 3215
			// (get) Token: 0x0600259A RID: 9626 RVA: 0x000971A1 File Offset: 0x000953A1
			object IEnumerator.Current
			{
				get
				{
					this.CheckDisposed();
					return this.list.Current;
				}
			}

			// Token: 0x0600259B RID: 9627 RVA: 0x000971B4 File Offset: 0x000953B4
			public void Reset()
			{
				this.CheckDisposed();
				this.list.Reset();
			}

			// Token: 0x0600259C RID: 9628 RVA: 0x000971C8 File Offset: 0x000953C8
			public bool MoveNext()
			{
				while (this.list.MoveNext())
				{
					AttachmentHandle handle = this.list.Current;
					if (!CoreAttachmentCollection.IsCalendarException(handle))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600259D RID: 9629 RVA: 0x000971FB File Offset: 0x000953FB
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x0600259E RID: 9630 RVA: 0x0009720A File Offset: 0x0009540A
			internal void Dispose(bool isDisposing)
			{
				if (isDisposing)
				{
					this.list.Dispose();
				}
				this.isDisposed = true;
			}

			// Token: 0x0600259F RID: 9631 RVA: 0x00097221 File Offset: 0x00095421
			private void CheckDisposed()
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("CoreAttachmentCollection::AttachmentEnumerator");
				}
			}

			// Token: 0x040016AA RID: 5802
			private IEnumerator<AttachmentHandle> list;

			// Token: 0x040016AB RID: 5803
			private bool isDisposed;
		}
	}
}
