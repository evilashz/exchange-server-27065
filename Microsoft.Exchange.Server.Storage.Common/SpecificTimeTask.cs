using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000083 RID: 131
	public class SpecificTimeTask<T> : RecurringTask<T>
	{
		// Token: 0x06000728 RID: 1832 RVA: 0x00014234 File Offset: 0x00012434
		public SpecificTimeTask(Task<T>.TaskCallback callback, T context, DateTime triggerUtcTime) : base(callback, context, triggerUtcTime - DateTime.UtcNow, RecurringTask<T>.RunOnce, true)
		{
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001424F File Offset: 0x0001244F
		public new void Start()
		{
			throw new InvalidOperationException("Can not start a SpecificTimeTask");
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001425B File Offset: 0x0001245B
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SpecificTimeTask<T>>(this);
		}
	}
}
