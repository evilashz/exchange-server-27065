using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000E3 RID: 227
	internal class SharedObjectPropertyBagData : DisposableBase, IStateObject
	{
		// Token: 0x060008DD RID: 2269 RVA: 0x0002A214 File Offset: 0x00028414
		internal SharedObjectPropertyBagData(Context context, Mailbox mailbox, SharedObjectPropertyBagDataCache cache, ExchangeId propertyBagId, DataRow dataRow)
		{
			this.cache = cache;
			this.propertyBagId = propertyBagId;
			this.dataRow = dataRow;
			if (dataRow != null && dataRow.IsNew)
			{
				this.isOriginallyNew = true;
				this.OnDirty(context, mailbox);
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x0002A265 File Offset: 0x00028465
		internal SharedObjectPropertyBagDataCache Cache
		{
			get
			{
				return this.cache;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x0002A26D File Offset: 0x0002846D
		internal ExchangeId PropertyBagId
		{
			get
			{
				return this.propertyBagId;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x0002A275 File Offset: 0x00028475
		// (set) Token: 0x060008E1 RID: 2273 RVA: 0x0002A27D File Offset: 0x0002847D
		internal DataRow DataRow
		{
			get
			{
				return this.dataRow;
			}
			set
			{
				this.dataRow = value;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x0002A286 File Offset: 0x00028486
		// (set) Token: 0x060008E3 RID: 2275 RVA: 0x0002A28E File Offset: 0x0002848E
		internal Dictionary<ushort, KeyValuePair<StorePropTag, object>> Properties
		{
			get
			{
				return this.properties;
			}
			set
			{
				this.properties = value;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x0002A297 File Offset: 0x00028497
		// (set) Token: 0x060008E5 RID: 2277 RVA: 0x0002A29F File Offset: 0x0002849F
		internal bool PropertiesDirty
		{
			get
			{
				return this.propertiesDirty;
			}
			set
			{
				this.propertiesDirty = value;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x0002A2A8 File Offset: 0x000284A8
		internal bool IsInUse
		{
			get
			{
				return this.usageCount > 0;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0002A2B3 File Offset: 0x000284B3
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x0002A2BB File Offset: 0x000284BB
		internal bool IsActiveInTheCache
		{
			get
			{
				return this.isActiveInTheCache;
			}
			set
			{
				this.isActiveInTheCache = value;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0002A2C4 File Offset: 0x000284C4
		internal Mailbox ModifierMailbox
		{
			get
			{
				return this.modifierMailbox;
			}
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0002A2CC File Offset: 0x000284CC
		internal static int AllocateComponentDataSlot()
		{
			return SharedObjectPropertyBagData.ComponentDataStorage.AllocateSlot();
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0002A2D3 File Offset: 0x000284D3
		internal void ResetModifierMailbox()
		{
			Interlocked.Exchange<Mailbox>(ref this.modifierMailbox, null);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0002A2E2 File Offset: 0x000284E2
		public virtual void OnBeforeCommit(Context context)
		{
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0002A2E4 File Offset: 0x000284E4
		public virtual void OnCommit(Context context)
		{
			if (!base.IsDisposed)
			{
				this.isOriginallyNew = false;
				if (this.modifierMailbox != null)
				{
					this.ResetModifierMailbox();
				}
			}
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0002A304 File Offset: 0x00028504
		public virtual void OnAbort(Context context)
		{
			if (!base.IsDisposed)
			{
				if (ExTraceGlobals.FolderTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.FolderTracer.TraceDebug<ExchangeId>(0L, "Discarding shared object property bag {0} cache on Abort", this.PropertyBagId);
				}
				if (this.isOriginallyNew)
				{
					if (this.dataRow != null)
					{
						this.dataRow.Dispose();
						this.dataRow = null;
					}
					this.ResetModifierMailbox();
					return;
				}
				this.DiscardCache(context);
			}
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0002A370 File Offset: 0x00028570
		public void OnDirty(Context context, Mailbox modifierMailbox)
		{
			if (!context.IsStateObjectRegistered(this))
			{
				context.RegisterStateObject(this);
			}
			Mailbox mailbox = Interlocked.Exchange<Mailbox>(ref this.modifierMailbox, modifierMailbox);
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(mailbox == null || mailbox == modifierMailbox, "Concurrent modifier?");
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0002A3B0 File Offset: 0x000285B0
		internal void DiscardCache(Context context)
		{
			if (this.DataRow != null && !this.DataRow.IsDead)
			{
				this.DataRow.DiscardCache(context, true);
			}
			this.Properties = null;
			this.PropertiesDirty = false;
			this.componentData.CleanupDataSlots(context);
			this.ResetModifierMailbox();
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0002A3FF File Offset: 0x000285FF
		internal void IncrementUsage()
		{
			this.usageCount++;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0002A40F File Offset: 0x0002860F
		internal void DecrementUsage()
		{
			this.usageCount--;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0002A41F File Offset: 0x0002861F
		internal object GetComponentData(int slotNumber)
		{
			return this.componentData[slotNumber];
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0002A42D File Offset: 0x0002862D
		internal void SetComponentData(int slotNumber, object value)
		{
			this.componentData[slotNumber] = value;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0002A43C File Offset: 0x0002863C
		public object CompareExchangeComponentData(int slotNumber, object comparand, object value)
		{
			return this.componentData.CompareExchange(slotNumber, comparand, value);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0002A44C File Offset: 0x0002864C
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.dataRow != null)
			{
				this.dataRow.Dispose();
				this.dataRow = null;
			}
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0002A46B File Offset: 0x0002866B
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SharedObjectPropertyBagData>(this);
		}

		// Token: 0x04000524 RID: 1316
		private SharedObjectPropertyBagDataCache cache;

		// Token: 0x04000525 RID: 1317
		private ExchangeId propertyBagId;

		// Token: 0x04000526 RID: 1318
		private Dictionary<ushort, KeyValuePair<StorePropTag, object>> properties;

		// Token: 0x04000527 RID: 1319
		private bool propertiesDirty;

		// Token: 0x04000528 RID: 1320
		private DataRow dataRow;

		// Token: 0x04000529 RID: 1321
		private int usageCount;

		// Token: 0x0400052A RID: 1322
		private bool isActiveInTheCache;

		// Token: 0x0400052B RID: 1323
		private bool isOriginallyNew;

		// Token: 0x0400052C RID: 1324
		private SharedObjectPropertyBagData.ComponentDataStorage componentData = new SharedObjectPropertyBagData.ComponentDataStorage();

		// Token: 0x0400052D RID: 1325
		private Mailbox modifierMailbox;

		// Token: 0x020000E4 RID: 228
		private class ComponentDataStorage : ComponentDataStorageBase
		{
			// Token: 0x060008F8 RID: 2296 RVA: 0x0002A473 File Offset: 0x00028673
			internal static int AllocateSlot()
			{
				return Interlocked.Increment(ref SharedObjectPropertyBagData.ComponentDataStorage.nextAvailableSlot) - 1;
			}

			// Token: 0x17000251 RID: 593
			// (get) Token: 0x060008F9 RID: 2297 RVA: 0x0002A481 File Offset: 0x00028681
			internal override int SlotCount
			{
				get
				{
					return SharedObjectPropertyBagData.ComponentDataStorage.nextAvailableSlot;
				}
			}

			// Token: 0x0400052E RID: 1326
			private static int nextAvailableSlot;
		}
	}
}
