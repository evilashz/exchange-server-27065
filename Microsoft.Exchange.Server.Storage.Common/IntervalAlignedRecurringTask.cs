using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000042 RID: 66
	public class IntervalAlignedRecurringTask<T> : RecurringTask<T>
	{
		// Token: 0x0600048C RID: 1164 RVA: 0x0000CCE9 File Offset: 0x0000AEE9
		public IntervalAlignedRecurringTask(Task<T>.TaskCallback callback, T context, TimeSpan alignment) : base(callback, context, alignment)
		{
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000CCF4 File Offset: 0x0000AEF4
		public override void Start()
		{
			using (LockManager.Lock(this, LockManager.LockType.Task))
			{
				base.CheckDisposed();
				base.InitialDelay = TimeSpan.FromTicks(base.Interval.Ticks - DateTime.UtcNow.Ticks % base.Interval.Ticks);
				base.Start();
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000CD6C File Offset: 0x0000AF6C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<IntervalAlignedRecurringTask<T>>(this);
		}
	}
}
