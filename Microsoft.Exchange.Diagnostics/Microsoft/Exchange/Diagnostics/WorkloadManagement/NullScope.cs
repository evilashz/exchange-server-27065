using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement.Implementation;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement
{
	// Token: 0x020001FE RID: 510
	internal class NullScope : DisposeTrackableBase
	{
		// Token: 0x06000F06 RID: 3846 RVA: 0x0003D328 File Offset: 0x0003B528
		internal NullScope()
		{
			this.localId = SingleContext.Singleton.LocalId;
			this.threadId = Environment.CurrentManagedThreadId;
			SingleContext.Singleton.LocalId = null;
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0003D369 File Offset: 0x0003B569
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<NullScope>(this);
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0003D371 File Offset: 0x0003B571
		protected override void InternalDispose(bool disposing)
		{
			if (!base.IsDisposed && disposing)
			{
				ExAssert.RetailAssert(this.threadId == Environment.CurrentManagedThreadId, "ActivityContext.SuppressThreadScope() and NullScope.Dispose() must be called on same thread.");
				SingleContext.Singleton.LocalId = this.localId;
			}
		}

		// Token: 0x04000AAC RID: 2732
		private readonly int threadId;

		// Token: 0x04000AAD RID: 2733
		private Guid? localId;
	}
}
