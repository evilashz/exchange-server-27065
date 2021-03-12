using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000702 RID: 1794
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class EventPumpThreadLimiter
	{
		// Token: 0x06004711 RID: 18193 RVA: 0x0012E3D8 File Offset: 0x0012C5D8
		internal EventPumpThreadLimiter(EventPump eventPump)
		{
			this.eventPump = eventPump;
		}

		// Token: 0x06004712 RID: 18194 RVA: 0x0012E400 File Offset: 0x0012C600
		internal void RequestRecovery(EventSink eventSink)
		{
			bool flag = false;
			lock (this.thisLock)
			{
				this.eventSinks.Enqueue(eventSink);
				if (!this.isThreadActive)
				{
					this.isThreadActive = true;
					flag = true;
				}
			}
			if (flag)
			{
				this.ExecuteRecovery();
			}
		}

		// Token: 0x06004713 RID: 18195 RVA: 0x0012E464 File Offset: 0x0012C664
		private void ExecuteRecovery()
		{
			bool flag = false;
			while (!flag)
			{
				EventSink eventSink = null;
				lock (this.thisLock)
				{
					eventSink = this.eventSinks.Dequeue();
				}
				try
				{
					this.eventPump.ExecuteRecovery(eventSink);
				}
				catch (StoragePermanentException arg)
				{
					ExTraceGlobals.EventTracer.TraceError<EventPumpThreadLimiter, EventSink, StoragePermanentException>((long)this.GetHashCode(), "EventPump::ReadAndDistributeEvents. {0}. Exception caught during recovery. EventSink = {1}. Exception = {2}.", this, eventSink, arg);
				}
				catch (StorageTransientException arg2)
				{
					ExTraceGlobals.EventTracer.TraceError<EventPumpThreadLimiter, EventSink, StorageTransientException>((long)this.GetHashCode(), "EventPump::ReadAndDistributeEvents. {0}. Exception caught during recovery. EventSink = {1}. Exception = {2}.", this, eventSink, arg2);
				}
				lock (this.thisLock)
				{
					flag = (this.eventSinks.Count == 0);
					if (flag)
					{
						this.isThreadActive = false;
					}
				}
			}
		}

		// Token: 0x040026D9 RID: 9945
		private bool isThreadActive;

		// Token: 0x040026DA RID: 9946
		private Queue<EventSink> eventSinks = new Queue<EventSink>();

		// Token: 0x040026DB RID: 9947
		private readonly EventPump eventPump;

		// Token: 0x040026DC RID: 9948
		private readonly object thisLock = new object();
	}
}
