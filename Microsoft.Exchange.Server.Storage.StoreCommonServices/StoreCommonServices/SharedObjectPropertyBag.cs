using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000A0 RID: 160
	public abstract class SharedObjectPropertyBag : ObjectPropertyBag
	{
		// Token: 0x060005F5 RID: 1525 RVA: 0x00020258 File Offset: 0x0001E458
		protected SharedObjectPropertyBag(Context context, Table table, Mailbox mailbox, bool newBag, bool writeThrough, SharedObjectPropertyBagDataCache propertyBagDataCache, ExchangeId propertyBagId, params ColumnValue[] initialValues) : base(context, false)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				if (mailbox != null)
				{
					mailbox.IsValid();
					this.mailbox = mailbox;
				}
				else
				{
					this.mailbox = (Mailbox)this;
				}
				this.objectPropertyBagData = propertyBagDataCache.LoadPropertyBagData(context, this.mailbox, propertyBagId, newBag, table, writeThrough, initialValues);
				base.LoadData(context);
				this.Mailbox.AddPropertyBag(propertyBagId, this);
				if (this.DataRow != null)
				{
					this.OnAccess(context);
					if (!base.IsSaved)
					{
						this.OnDirty(context);
					}
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0002030C File Offset: 0x0001E50C
		// (set) Token: 0x060005F7 RID: 1527 RVA: 0x00020323 File Offset: 0x0001E523
		internal override DataRow DataRow
		{
			get
			{
				if (this.objectPropertyBagData != null)
				{
					return this.objectPropertyBagData.DataRow;
				}
				return null;
			}
			set
			{
				this.objectPropertyBagData.DataRow = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00020331 File Offset: 0x0001E531
		protected override Dictionary<ushort, KeyValuePair<StorePropTag, object>> Properties
		{
			get
			{
				if (this.objectPropertyBagData != null)
				{
					return this.objectPropertyBagData.Properties;
				}
				return null;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00020348 File Offset: 0x0001E548
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x0002035F File Offset: 0x0001E55F
		protected override bool PropertiesDirty
		{
			get
			{
				return this.objectPropertyBagData != null && this.objectPropertyBagData.PropertiesDirty;
			}
			set
			{
				this.objectPropertyBagData.PropertiesDirty = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0002036D File Offset: 0x0001E56D
		public Mailbox Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x00020375 File Offset: 0x0001E575
		public override bool IsDirty
		{
			get
			{
				return base.IsDirty && this.Mailbox == this.objectPropertyBagData.ModifierMailbox;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x00020394 File Offset: 0x0001E594
		public override Context CurrentOperationContext
		{
			get
			{
				return this.Mailbox.CurrentOperationContext;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x000203A1 File Offset: 0x0001E5A1
		public override ReplidGuidMap ReplidGuidMap
		{
			get
			{
				return this.Mailbox.ReplidGuidMap;
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x000203AE File Offset: 0x0001E5AE
		protected override StorePropTag MapPropTag(Context context, uint propertyTag)
		{
			return this.Mailbox.GetStorePropTag(context, propertyTag, this.GetObjectType());
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x000203C4 File Offset: 0x0001E5C4
		protected override void AssignPropertiesToUse(Dictionary<ushort, KeyValuePair<StorePropTag, object>> properties)
		{
			using (LockManager.Lock(this.objectPropertyBagData, LockManager.LockType.LeafMonitorLock))
			{
				if (this.objectPropertyBagData.Properties == null)
				{
					this.objectPropertyBagData.Properties = properties;
				}
			}
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00020418 File Offset: 0x0001E618
		internal static int AllocateComponentDataSlot()
		{
			return SharedObjectPropertyBagData.AllocateComponentDataSlot();
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0002041F File Offset: 0x0001E61F
		internal object GetComponentData(int slotNumber)
		{
			base.CheckNotDead();
			return this.objectPropertyBagData.GetComponentData(slotNumber);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00020433 File Offset: 0x0001E633
		internal void SetComponentData(int slotNumber, object value)
		{
			base.CheckNotDead();
			this.objectPropertyBagData.SetComponentData(slotNumber, value);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00020448 File Offset: 0x0001E648
		public object CompareExchangeComponentData(int slotNumber, object comparand, object value)
		{
			base.CheckNotDead();
			return this.objectPropertyBagData.CompareExchangeComponentData(slotNumber, comparand, value);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0002045E File Offset: 0x0001E65E
		internal void OnMailboxDisconnect()
		{
			this.markedAsActive = false;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00020468 File Offset: 0x0001E668
		protected override void OnAccess(Context context)
		{
			if (!this.markedAsActive)
			{
				using (LockManager.Lock(this.objectPropertyBagData, LockManager.LockType.LeafMonitorLock, context.Diagnostics))
				{
					this.DataRow.ReloadCacheIfDiscarded(context);
				}
				this.objectPropertyBagData.Cache.MarkAsActiveInTheCache(this.objectPropertyBagData, this.objectPropertyBagData.PropertyBagId);
				this.markedAsActive = true;
				this.Mailbox.OnPropertyBagActive(this);
			}
			base.OnAccess(context);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x000204FC File Offset: 0x0001E6FC
		protected override void OnDirty(Context context)
		{
			MailboxState sharedState = this.Mailbox.SharedState;
			this.objectPropertyBagData.OnDirty(context, this.Mailbox);
			base.OnDirty(context);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00020524 File Offset: 0x0001E724
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.objectPropertyBagData != null)
			{
				this.Mailbox.RemovePropertyBag(this.objectPropertyBagData.PropertyBagId);
				this.objectPropertyBagData.Cache.ReleasePropertyBagData(this.objectPropertyBagData, this.objectPropertyBagData.PropertyBagId);
				this.objectPropertyBagData = null;
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00020584 File Offset: 0x0001E784
		public override void Flush(Context context, bool flushLargeDirtyStreams)
		{
			try
			{
				base.Flush(context, flushLargeDirtyStreams);
			}
			finally
			{
				if (!base.IsDirty)
				{
					this.objectPropertyBagData.ResetModifierMailbox();
				}
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x000205C0 File Offset: 0x0001E7C0
		protected override void Delete(Context context, bool notifyParent)
		{
			SharedObjectPropertyBagData sharedObjectPropertyBagData = null;
			try
			{
				sharedObjectPropertyBagData = this.objectPropertyBagData;
				base.Delete(context, notifyParent);
			}
			finally
			{
				sharedObjectPropertyBagData.ResetModifierMailbox();
			}
		}

		// Token: 0x040003FB RID: 1019
		private readonly Mailbox mailbox;

		// Token: 0x040003FC RID: 1020
		private SharedObjectPropertyBagData objectPropertyBagData;

		// Token: 0x040003FD RID: 1021
		private bool markedAsActive;

		// Token: 0x020000A1 RID: 161
		public struct NonDiscardableComponentData<T> : IComponentData where T : struct
		{
			// Token: 0x1700017F RID: 383
			// (get) Token: 0x0600060B RID: 1547 RVA: 0x000205F8 File Offset: 0x0001E7F8
			// (set) Token: 0x0600060C RID: 1548 RVA: 0x00020600 File Offset: 0x0001E800
			public T Value { get; private set; }

			// Token: 0x0600060D RID: 1549 RVA: 0x00020609 File Offset: 0x0001E809
			public NonDiscardableComponentData(T value)
			{
				this = default(SharedObjectPropertyBag.NonDiscardableComponentData<T>);
				this.Value = value;
			}

			// Token: 0x0600060E RID: 1550 RVA: 0x00020619 File Offset: 0x0001E819
			bool IComponentData.DoCleanup(Context context)
			{
				return false;
			}
		}
	}
}
