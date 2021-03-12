using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200001A RID: 26
	[DebuggerNonUserCode]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct DisposeGuard : IDisposable
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00002FA0 File Offset: 0x000011A0
		public T Add<T>(T disposable) where T : class, IDisposable
		{
			this.CheckDisposed();
			if (disposable == null)
			{
				return disposable;
			}
			bool flag = false;
			try
			{
				this.AddGuardedObject(disposable);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					DisposeGuard.DisposeIfPresent(disposable);
				}
			}
			return disposable;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002FF4 File Offset: 0x000011F4
		public void Success()
		{
			this.CheckDisposed();
			this.SlotCount = 0;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003003 File Offset: 0x00001203
		internal static void DisposeIfPresent(IDisposable disposable)
		{
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003010 File Offset: 0x00001210
		private static IDisposable Swap(ref IDisposable fixedSlot, IDisposable newValue)
		{
			IDisposable result = fixedSlot;
			fixedSlot = newValue;
			return result;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003024 File Offset: 0x00001224
		private void CheckDisposed()
		{
			if (this.slotCount == 65535)
			{
				throw new ObjectDisposedException(base.GetType().ToString() + " has already been disposed.");
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003058 File Offset: 0x00001258
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00003060 File Offset: 0x00001260
		private ushort SlotCount
		{
			get
			{
				return this.slotCount;
			}
			set
			{
				if (value >= 4 && value > this.slotCount)
				{
					if (this.overflowSlots == null)
					{
						this.overflowSlots = new IDisposable[Math.Max((int)(value - 4), 4)];
					}
					if ((int)value > 4 + this.overflowSlots.Length)
					{
						Array.Resize<IDisposable>(ref this.overflowSlots, Math.Max((int)value, this.overflowSlots.Length * 2));
					}
				}
				this.slotCount = value;
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000030C5 File Offset: 0x000012C5
		private void AddGuardedObject(IDisposable disposable)
		{
			this.SlotCount += 1;
			this.SwapSlot((int)(this.SlotCount - 1), disposable);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000030E6 File Offset: 0x000012E6
		private void RemoveLastGuardedObject()
		{
			DisposeGuard.DisposeIfPresent(this.SwapSlot((int)(this.SlotCount - 1), null));
			this.SlotCount -= 1;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000310C File Offset: 0x0000130C
		private IDisposable SwapSlot(int index, IDisposable newValue)
		{
			switch (index)
			{
			case 0:
				return DisposeGuard.Swap(ref this.fixedSlot0, newValue);
			case 1:
				return DisposeGuard.Swap(ref this.fixedSlot1, newValue);
			case 2:
				return DisposeGuard.Swap(ref this.fixedSlot2, newValue);
			case 3:
				return DisposeGuard.Swap(ref this.fixedSlot3, newValue);
			default:
				return DisposeGuard.Swap(ref this.overflowSlots[index - 4], newValue);
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000317B File Offset: 0x0000137B
		public void Dispose()
		{
			while (this.SlotCount > 0)
			{
				this.RemoveLastGuardedObject();
			}
			this.slotCount = ushort.MaxValue;
		}

		// Token: 0x04000078 RID: 120
		private const int FixedSlotCapacity = 4;

		// Token: 0x04000079 RID: 121
		private const int InitialOverflowSlotCapacity = 4;

		// Token: 0x0400007A RID: 122
		private ushort slotCount;

		// Token: 0x0400007B RID: 123
		private IDisposable fixedSlot0;

		// Token: 0x0400007C RID: 124
		private IDisposable fixedSlot1;

		// Token: 0x0400007D RID: 125
		private IDisposable fixedSlot2;

		// Token: 0x0400007E RID: 126
		private IDisposable fixedSlot3;

		// Token: 0x0400007F RID: 127
		private IDisposable[] overflowSlots;
	}
}
