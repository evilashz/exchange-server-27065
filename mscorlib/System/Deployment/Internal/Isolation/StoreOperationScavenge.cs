using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200067D RID: 1661
	internal struct StoreOperationScavenge
	{
		// Token: 0x06004EBF RID: 20159 RVA: 0x00118250 File Offset: 0x00116450
		public StoreOperationScavenge(bool Light, ulong SizeLimit, ulong RunLimit, uint ComponentLimit)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationScavenge));
			this.Flags = StoreOperationScavenge.OpFlags.Nothing;
			if (Light)
			{
				this.Flags |= StoreOperationScavenge.OpFlags.Light;
			}
			this.SizeReclaimationLimit = SizeLimit;
			if (SizeLimit != 0UL)
			{
				this.Flags |= StoreOperationScavenge.OpFlags.LimitSize;
			}
			this.RuntimeLimit = RunLimit;
			if (RunLimit != 0UL)
			{
				this.Flags |= StoreOperationScavenge.OpFlags.LimitTime;
			}
			this.ComponentCountLimit = ComponentLimit;
			if (ComponentLimit != 0U)
			{
				this.Flags |= StoreOperationScavenge.OpFlags.LimitCount;
			}
		}

		// Token: 0x06004EC0 RID: 20160 RVA: 0x001182D4 File Offset: 0x001164D4
		public StoreOperationScavenge(bool Light)
		{
			this = new StoreOperationScavenge(Light, 0UL, 0UL, 0U);
		}

		// Token: 0x06004EC1 RID: 20161 RVA: 0x001182E2 File Offset: 0x001164E2
		public void Destroy()
		{
		}

		// Token: 0x04002199 RID: 8601
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x0400219A RID: 8602
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationScavenge.OpFlags Flags;

		// Token: 0x0400219B RID: 8603
		[MarshalAs(UnmanagedType.U8)]
		public ulong SizeReclaimationLimit;

		// Token: 0x0400219C RID: 8604
		[MarshalAs(UnmanagedType.U8)]
		public ulong RuntimeLimit;

		// Token: 0x0400219D RID: 8605
		[MarshalAs(UnmanagedType.U4)]
		public uint ComponentCountLimit;

		// Token: 0x02000C1F RID: 3103
		[Flags]
		public enum OpFlags
		{
			// Token: 0x040036A8 RID: 13992
			Nothing = 0,
			// Token: 0x040036A9 RID: 13993
			Light = 1,
			// Token: 0x040036AA RID: 13994
			LimitSize = 2,
			// Token: 0x040036AB RID: 13995
			LimitTime = 4,
			// Token: 0x040036AC RID: 13996
			LimitCount = 8
		}
	}
}
