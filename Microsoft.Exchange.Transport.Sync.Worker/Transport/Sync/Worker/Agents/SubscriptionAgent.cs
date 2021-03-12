using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Worker.Agents
{
	// Token: 0x02000016 RID: 22
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class SubscriptionAgent : DisposeTrackableBase
	{
		// Token: 0x060001BF RID: 447 RVA: 0x000087A0 File Offset: 0x000069A0
		public SubscriptionAgent(string name)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("name", name);
			this.name = name;
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x000087BA File Offset: 0x000069BA
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000087C2 File Offset: 0x000069C2
		public virtual void OnWorkItemCompleted(SubscriptionEventSource source, SubscriptionWorkItemCompletedEventArgs e)
		{
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000087C4 File Offset: 0x000069C4
		public virtual void OnWorkItemFailedLoadSubscription(SubscriptionEventSource source, SubscriptionWorkItemFailedLoadSubscriptionEventArgs e)
		{
		}

		// Token: 0x060001C3 RID: 451
		public abstract bool IsEventInteresting(AggregationType aggregationType, SubscriptionEvents events);

		// Token: 0x060001C4 RID: 452 RVA: 0x000087C8 File Offset: 0x000069C8
		public void Invoke(SubscriptionEvents subscriptionEvent, object source, object e)
		{
			SubscriptionEventSource source2 = (SubscriptionEventSource)source;
			switch (subscriptionEvent)
			{
			case SubscriptionEvents.WorkItemCompleted:
			{
				SubscriptionWorkItemCompletedEventArgs e2 = (SubscriptionWorkItemCompletedEventArgs)e;
				this.OnWorkItemCompleted(source2, e2);
				return;
			}
			case SubscriptionEvents.WorkItemFailedLoadSubscription:
			{
				SubscriptionWorkItemFailedLoadSubscriptionEventArgs e3 = (SubscriptionWorkItemFailedLoadSubscriptionEventArgs)e;
				this.OnWorkItemFailedLoadSubscription(source2, e3);
				return;
			}
			default:
				throw new NotSupportedException("Unsupported Subscription Event: " + subscriptionEvent);
			}
		}

		// Token: 0x040000F8 RID: 248
		private string name;
	}
}
