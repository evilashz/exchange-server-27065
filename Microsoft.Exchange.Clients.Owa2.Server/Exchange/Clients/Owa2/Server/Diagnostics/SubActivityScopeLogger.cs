using System;
using System.Diagnostics;
using System.Text;
using Microsoft.Exchange.Services;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000461 RID: 1121
	internal sealed class SubActivityScopeLogger
	{
		// Token: 0x0600259A RID: 9626 RVA: 0x000884F8 File Offset: 0x000866F8
		private SubActivityScopeLogger(RequestDetailsLogger logger, Enum subActivityId)
		{
			this.logger = logger;
			this.subActivityId = subActivityId;
			this.log.Append("[");
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x00088545 File Offset: 0x00086745
		internal static SubActivityScopeLogger Create(RequestDetailsLogger logger, Enum subActivityId)
		{
			return new SubActivityScopeLogger(logger, subActivityId);
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x00088550 File Offset: 0x00086750
		internal void LogNext(string tag)
		{
			if (string.IsNullOrEmpty(tag))
			{
				throw new ArgumentNullException("tag");
			}
			this.log.Append(tag);
			this.log.Append(":");
			this.log.Append(this.stopwatch.ElapsedMilliseconds);
			this.log.Append(",");
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x000885B8 File Offset: 0x000867B8
		internal void LogEnd()
		{
			this.LogNext("End");
			this.log.Append("]");
			if (this.logger != null && this.stopwatch.ElapsedMilliseconds > 1000L)
			{
				this.logger.Set(this.subActivityId, this.log.ToString());
			}
		}

		// Token: 0x040015CE RID: 5582
		private const uint Threshold = 1000U;

		// Token: 0x040015CF RID: 5583
		private StringBuilder log = new StringBuilder(128);

		// Token: 0x040015D0 RID: 5584
		private Stopwatch stopwatch = Stopwatch.StartNew();

		// Token: 0x040015D1 RID: 5585
		private RequestDetailsLogger logger;

		// Token: 0x040015D2 RID: 5586
		private Enum subActivityId;
	}
}
