using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Office.Server.Directory;

namespace Microsoft.Exchange.FederatedDirectory
{
	// Token: 0x02000068 RID: 104
	internal sealed class CorrelationContext : DisposeTrackableBase
	{
		// Token: 0x06000266 RID: 614 RVA: 0x0000C608 File Offset: 0x0000A808
		public static IActivityScope GetActivityScope(Guid correlationId)
		{
			IActivityScope result;
			lock (CorrelationContext.activityScopes)
			{
				CorrelationContext.ActivityScopeCount activityScopeCount;
				if (CorrelationContext.activityScopes.TryGetValue(correlationId, out activityScopeCount))
				{
					result = activityScopeCount.ActivityScope;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000C65C File Offset: 0x0000A85C
		public CorrelationContext()
		{
			IActivityScope activityScope = ActivityContext.GetCurrentActivityScope();
			if (activityScope == null)
			{
				activityScope = ActivityContext.Start(null);
				this.endActivityScope = activityScope;
				CorrelationContext.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "CorrelationContext started new activity scope with ID: {0}", activityScope.ActivityId);
			}
			else
			{
				CorrelationContext.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "CorrelationContext using existing activity scope with ID: {0}", activityScope.ActivityId);
			}
			this.correlationId = activityScope.ActivityId;
			CorrelationContext.AddActivityScope(this.correlationId, activityScope);
			LogWriter.Initialize();
			LogManager.CorrelationStart(this.correlationId);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000C6E8 File Offset: 0x0000A8E8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CorrelationContext>(this);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000C6F0 File Offset: 0x0000A8F0
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.correlationId != Guid.Empty)
				{
					LogManager.CorrelationEnd();
					CorrelationContext.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "CorrelationContext ending with ID: {0}", this.correlationId);
					CorrelationContext.RemoveActivityScope(this.correlationId);
					this.correlationId = Guid.Empty;
				}
				if (this.endActivityScope != null)
				{
					this.endActivityScope.End();
					this.endActivityScope = null;
				}
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000C764 File Offset: 0x0000A964
		private static void AddActivityScope(Guid correlationId, IActivityScope activityScope)
		{
			lock (CorrelationContext.activityScopes)
			{
				CorrelationContext.ActivityScopeCount activityScopeCount;
				if (!CorrelationContext.activityScopes.TryGetValue(correlationId, out activityScopeCount))
				{
					activityScopeCount = new CorrelationContext.ActivityScopeCount(activityScope);
					CorrelationContext.activityScopes.Add(correlationId, activityScopeCount);
				}
				activityScopeCount.Count++;
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000C7D0 File Offset: 0x0000A9D0
		private static void RemoveActivityScope(Guid correlationId)
		{
			lock (CorrelationContext.activityScopes)
			{
				CorrelationContext.ActivityScopeCount activityScopeCount;
				if (CorrelationContext.activityScopes.TryGetValue(correlationId, out activityScopeCount))
				{
					activityScopeCount.Count--;
					if (activityScopeCount.Count == 0)
					{
						CorrelationContext.activityScopes.Remove(correlationId);
					}
				}
			}
		}

		// Token: 0x04000564 RID: 1380
		private static readonly Trace Tracer = ExTraceGlobals.FederatedDirectoryTracer;

		// Token: 0x04000565 RID: 1381
		private Guid correlationId;

		// Token: 0x04000566 RID: 1382
		private IActivityScope endActivityScope;

		// Token: 0x04000567 RID: 1383
		private static readonly Dictionary<Guid, CorrelationContext.ActivityScopeCount> activityScopes = new Dictionary<Guid, CorrelationContext.ActivityScopeCount>();

		// Token: 0x02000069 RID: 105
		private sealed class ActivityScopeCount
		{
			// Token: 0x0600026D RID: 621 RVA: 0x0000C852 File Offset: 0x0000AA52
			public ActivityScopeCount(IActivityScope activityScope)
			{
				this.ActivityScope = activityScope;
			}

			// Token: 0x170000AD RID: 173
			// (get) Token: 0x0600026E RID: 622 RVA: 0x0000C861 File Offset: 0x0000AA61
			// (set) Token: 0x0600026F RID: 623 RVA: 0x0000C869 File Offset: 0x0000AA69
			public int Count { get; set; }

			// Token: 0x170000AE RID: 174
			// (get) Token: 0x06000270 RID: 624 RVA: 0x0000C872 File Offset: 0x0000AA72
			// (set) Token: 0x06000271 RID: 625 RVA: 0x0000C87A File Offset: 0x0000AA7A
			public IActivityScope ActivityScope { get; private set; }
		}
	}
}
