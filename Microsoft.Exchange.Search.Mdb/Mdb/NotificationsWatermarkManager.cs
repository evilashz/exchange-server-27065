using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x02000029 RID: 41
	internal sealed class NotificationsWatermarkManager : WatermarkManager<long>
	{
		// Token: 0x06000147 RID: 327 RVA: 0x00009CE2 File Offset: 0x00007EE2
		internal NotificationsWatermarkManager(int batchSize) : base(batchSize)
		{
			base.DiagnosticsSession.ComponentName = "NotificationsWatermarkManager";
			base.DiagnosticsSession.Tracer = ExTraceGlobals.NotificationsWatermarkManagerTracer;
			this.stateUpdateList = new SortedDictionary<long, bool>();
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00009D18 File Offset: 0x00007F18
		internal void Add(long eventId, bool interested)
		{
			lock (this.stateUpdateList)
			{
				base.DiagnosticsSession.TraceDebug<long>("Add a new document (EventId={0})", eventId);
				this.stateUpdateList.Add(eventId, !interested);
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00009D74 File Offset: 0x00007F74
		internal bool TryComplete(long eventId, out long newWatermark)
		{
			bool result;
			lock (this.stateUpdateList)
			{
				base.DiagnosticsSession.TraceDebug<long>("TryComplete: EventId = {0}", eventId);
				bool flag2;
				if (!this.stateUpdateList.TryGetValue(eventId, out flag2))
				{
					newWatermark = -1L;
					base.DiagnosticsSession.TraceDebug("State does not need to be updated.", new object[0]);
					result = false;
				}
				else
				{
					this.stateUpdateList[eventId] = true;
					if (base.TryFindNewWatermark(this.stateUpdateList, out newWatermark))
					{
						base.DiagnosticsSession.TraceDebug<long>("State needs to update to {0}.", newWatermark);
						result = true;
					}
					else
					{
						newWatermark = -1L;
						base.DiagnosticsSession.TraceDebug("State does not need to be updated.", new object[0]);
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x040000DA RID: 218
		private readonly SortedDictionary<long, bool> stateUpdateList;
	}
}
