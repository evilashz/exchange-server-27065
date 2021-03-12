using System;
using System.Threading;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200002B RID: 43
	public abstract class ComponentDataStorageBase
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0001447D File Offset: 0x0001267D
		internal bool IsEmpty
		{
			get
			{
				return this.slots == null;
			}
		}

		// Token: 0x1700009C RID: 156
		public object this[int slotNumber]
		{
			get
			{
				if (this.slots == null)
				{
					return null;
				}
				return this.slots[slotNumber];
			}
			set
			{
				if (this.slots == null && value == null)
				{
					return;
				}
				if (this.slots == null)
				{
					Interlocked.CompareExchange<object[]>(ref this.slots, new object[this.SlotCount], null);
				}
				this.slots[slotNumber] = value;
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000144D3 File Offset: 0x000126D3
		public object CompareExchange(int slotNumber, object comparand, object value)
		{
			if (this.slots == null)
			{
				Interlocked.CompareExchange<object[]>(ref this.slots, new object[this.SlotCount], null);
			}
			return Interlocked.CompareExchange(ref this.slots[slotNumber], value, comparand);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00014508 File Offset: 0x00012708
		internal void CleanupDataSlots(Context context)
		{
			if (this.slots == null)
			{
				return;
			}
			bool flag = true;
			for (int i = 0; i < this.slots.Length; i++)
			{
				IComponentData componentData = this.slots[i] as IComponentData;
				if (componentData != null && !componentData.DoCleanup(context))
				{
					flag = false;
				}
				else
				{
					this.slots[i] = null;
				}
			}
			if (flag)
			{
				this.slots = null;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001D3 RID: 467
		internal abstract int SlotCount { get; }

		// Token: 0x04000209 RID: 521
		private object[] slots;
	}
}
