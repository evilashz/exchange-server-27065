using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.QueueProcessing
{
	// Token: 0x020000E6 RID: 230
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ScheduledRequest : IRequest
	{
		// Token: 0x0600070B RID: 1803 RVA: 0x00014218 File Offset: 0x00012418
		public ScheduledRequest(IRequest request, DateTime firstExecution, Func<TimeSpan> periodGetter)
		{
			this.periodGetter = periodGetter;
			this.request = request;
			this.nextExecution = firstExecution;
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x00014235 File Offset: 0x00012435
		public bool IsBlocked
		{
			get
			{
				return TimeProvider.UtcNow < this.nextExecution || this.request.IsBlocked;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x00014256 File Offset: 0x00012456
		public IRequestQueue Queue
		{
			get
			{
				return this.request.Queue;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x00014263 File Offset: 0x00012463
		public IEnumerable<ResourceKey> Resources
		{
			get
			{
				return this.request.Resources;
			}
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00014270 File Offset: 0x00012470
		public void Abort()
		{
			this.request.Abort();
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001427D File Offset: 0x0001247D
		public void AssignQueue(IRequestQueue queue)
		{
			this.request.AssignQueue(queue);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001428B File Offset: 0x0001248B
		public RequestDiagnosticData GetDiagnosticData(bool verbose)
		{
			return this.request.GetDiagnosticData(verbose);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001429C File Offset: 0x0001249C
		public void Process()
		{
			this.request.Process();
			TimeSpan timeSpan = this.periodGetter();
			if (timeSpan > TimeSpan.Zero)
			{
				this.nextExecution = TimeProvider.UtcNow.Add(timeSpan);
				this.Queue.EnqueueRequest(this);
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x000142ED File Offset: 0x000124ED
		public bool ShouldCancel(TimeSpan queueTimeout)
		{
			return false;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x000142F0 File Offset: 0x000124F0
		public bool WaitExecution()
		{
			return this.request.WaitExecution();
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x000142FD File Offset: 0x000124FD
		public bool WaitExecution(TimeSpan timeout)
		{
			return this.request.WaitExecution(timeout);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001430B File Offset: 0x0001250B
		public bool WaitExecutionAndThrowOnFailure(TimeSpan timeout)
		{
			return this.request.WaitExecutionAndThrowOnFailure(timeout);
		}

		// Token: 0x040002B6 RID: 694
		private readonly Func<TimeSpan> periodGetter;

		// Token: 0x040002B7 RID: 695
		private readonly IRequest request;

		// Token: 0x040002B8 RID: 696
		private DateTime nextExecution;
	}
}
