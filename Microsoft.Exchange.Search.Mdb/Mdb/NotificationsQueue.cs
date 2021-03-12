﻿using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x02000028 RID: 40
	internal sealed class NotificationsQueue : QueueManager<NotificationData>
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00009A6A File Offset: 0x00007C6A
		internal NotificationsQueue(int capacity, int outstandingSize, ExPerformanceCounter stallPerfCounter) : base(capacity, outstandingSize, stallPerfCounter)
		{
			base.DiagnosticsSession.ComponentName = "NotificationsQueue";
			base.DiagnosticsSession.Tracer = ExTraceGlobals.MdbNotificationsFeederTracer;
			this.optimizeMap = new Dictionary<MdbItemIdentity, NotificationData>(capacity);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00009AA4 File Offset: 0x00007CA4
		protected override void PreEnqueue(NotificationData notification)
		{
			if (notification.Type == NotificationType.Uninteresting)
			{
				return;
			}
			MdbItemIdentity identity = notification.Identity;
			if (identity.ItemId == StoreObjectId.DummyId)
			{
				return;
			}
			NotificationData notificationData;
			if (this.optimizeMap.TryGetValue(identity, out notificationData))
			{
				if ((notificationData.Operation == DocumentOperation.Update && notification.Operation == DocumentOperation.Update) || (notificationData.Operation == DocumentOperation.Update && notification.Operation == DocumentOperation.Delete) || (notificationData.Operation == DocumentOperation.Move && notification.Operation == DocumentOperation.Move) || (notificationData.Operation == DocumentOperation.Move && notification.Operation == DocumentOperation.Update) || (notificationData.Operation == DocumentOperation.Move && notification.Operation == DocumentOperation.Delete))
				{
					base.DiagnosticsSession.TraceDebug("Notification: {0} already existed in the batch (op:{1}). Replacing with notification: {2} (op: {3})...", new object[]
					{
						notificationData,
						notificationData.Operation,
						notification,
						notification.Operation
					});
					notificationData.Type = NotificationType.Uninteresting;
					notification.TrackMergeWith(notificationData);
					this.optimizeMap[identity] = notification;
					return;
				}
				if ((notificationData.Operation == DocumentOperation.Insert && notification.Operation == DocumentOperation.Move) || (notificationData.Operation == DocumentOperation.Insert && notification.Operation == DocumentOperation.Update) || (notificationData.Operation == DocumentOperation.Update && notification.Operation == DocumentOperation.Move))
				{
					base.DiagnosticsSession.TraceDebug("Notification: {0} already existed in the batch (op:{1}). Ignoring: {2} (op: {3})...", new object[]
					{
						notificationData,
						notificationData.Operation,
						notification,
						notification.Operation
					});
					notificationData.TrackMergeWith(notification);
					notification.Type = NotificationType.Uninteresting;
					notificationData.Identity = notification.Identity;
					return;
				}
				if (notificationData.Operation == DocumentOperation.Insert && notification.Operation == DocumentOperation.Delete)
				{
					base.DiagnosticsSession.TraceDebug("Notification: {0} already existed in the batch (op:{1}). Removing with notification: {2} (op: {3})...", new object[]
					{
						notificationData,
						notificationData.Operation,
						notification,
						notification.Operation
					});
					notification.Type = NotificationType.Uninteresting;
					notificationData.Type = NotificationType.Uninteresting;
					this.optimizeMap.Remove(identity);
					return;
				}
			}
			else
			{
				this.optimizeMap.Add(identity, notification);
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00009C9C File Offset: 0x00007E9C
		protected override bool PostDequeue(NotificationData notification)
		{
			MdbItemIdentity identity = notification.Identity;
			NotificationData objA;
			if (this.optimizeMap.TryGetValue(identity, out objA) && object.ReferenceEquals(objA, notification))
			{
				this.optimizeMap.Remove(identity);
			}
			return notification.Type != NotificationType.Uninteresting;
		}

		// Token: 0x040000D9 RID: 217
		private readonly Dictionary<MdbItemIdentity, NotificationData> optimizeMap;
	}
}
