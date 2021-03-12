using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200009F RID: 159
	public class ReliableEventPublishingSubscription : NotificationSubscription
	{
		// Token: 0x06000927 RID: 2343 RVA: 0x0004D386 File Offset: 0x0004B586
		public ReliableEventPublishingSubscription(NotificationCallback callback) : base(SubscriptionKind.PreCommit, null, null, 0, 50592894, callback)
		{
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0004D398 File Offset: 0x0004B598
		public EventType EventTypeMask
		{
			get
			{
				return (EventType)base.EventTypeValueMask;
			}
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0004D3A0 File Offset: 0x0004B5A0
		public override bool IsInterested(NotificationEvent nev)
		{
			return true;
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0004D3A3 File Offset: 0x0004B5A3
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("ReliableEventPublishingSubscription");
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0004D3B1 File Offset: 0x0004B5B1
		protected override void AppendFields(StringBuilder sb)
		{
			base.AppendFields(sb);
			sb.Append(" EventTypeMask:[");
			sb.Append(this.EventTypeMask);
			sb.Append("]");
		}
	}
}
