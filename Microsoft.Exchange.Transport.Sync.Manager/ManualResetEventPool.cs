using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x0200000B RID: 11
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ManualResetEventPool : Pool<ManualResetEvent>
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002D8A File Offset: 0x00000F8A
		internal ManualResetEventPool(int capacity, int maxCapacity) : base(capacity, maxCapacity, TimeSpan.FromMilliseconds(-1.0))
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002DA2 File Offset: 0x00000FA2
		protected override ManualResetEvent CreateItem(out bool needsBackOff)
		{
			needsBackOff = false;
			return new ManualResetEvent(false);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002DAD File Offset: 0x00000FAD
		protected override void DestroyItem(ManualResetEvent item)
		{
			item.Close();
		}
	}
}
