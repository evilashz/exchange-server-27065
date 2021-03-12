using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Clients.Owa.Server.LyncIMLogging;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000138 RID: 312
	internal class InstantMessageNotifier : PendingRequestNotifierBase
	{
		// Token: 0x06000A85 RID: 2693 RVA: 0x0002424B File Offset: 0x0002244B
		internal InstantMessageNotifier(IUserContext userContext) : base(userContext)
		{
			this.pendingNotifications = new List<NotificationPayloadBase>();
			base.UserContext.PendingRequestManager.KeepAlive += this.KeepAlive;
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000A86 RID: 2694 RVA: 0x0002427C File Offset: 0x0002247C
		// (remove) Token: 0x06000A87 RID: 2695 RVA: 0x000242B4 File Offset: 0x000224B4
		public event EventHandler<EventArgs> ChangeUserPresenceAfterInactivity;

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x000242E9 File Offset: 0x000224E9
		// (set) Token: 0x06000A89 RID: 2697 RVA: 0x000242F1 File Offset: 0x000224F1
		public new string SubscriptionId
		{
			get
			{
				return base.SubscriptionId;
			}
			set
			{
				base.SubscriptionId = value;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x000242FA File Offset: 0x000224FA
		public override bool ShouldThrottle
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x000242FD File Offset: 0x000224FD
		public bool IsRegistered
		{
			get
			{
				return !string.IsNullOrEmpty(this.SubscriptionId);
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x0002430D File Offset: 0x0002250D
		public int PendingCount
		{
			get
			{
				return this.pendingNotifications.Count;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0002431A File Offset: 0x0002251A
		internal List<NotificationPayloadBase> PendingNotifications
		{
			get
			{
				return this.pendingNotifications;
			}
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00024322 File Offset: 0x00022522
		public void Clear()
		{
			this.pendingNotifications.Clear();
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00024330 File Offset: 0x00022530
		public void Add(InstantMessagePayload payloadItem)
		{
			if (this.pendingNotifications.Count < 10000)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceDebug((long)this.GetHashCode(), "Queuing IM payload item {0} for user {1}", new object[]
				{
					payloadItem.PayloadType,
					this.GetUriForUser()
				});
				if (!this.isOverMaxSize)
				{
					payloadItem.SubscriptionId = this.SubscriptionId;
					this.pendingNotifications.Add(payloadItem);
					return;
				}
			}
			else
			{
				this.LogPayloadNotPickedEvent();
			}
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x000243B0 File Offset: 0x000225B0
		internal void KeepAlive(object sender, EventArgs e)
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageNotifier.KeepAlive. User: {0}", new object[]
			{
				this.GetUriForUser()
			});
			long num = Globals.ApplicationTime - ((IUserContext)base.UserContext).LastUserRequestTime;
			if (num > (long)Globals.ActivityBasedPresenceDuration)
			{
				EventArgs e2 = new EventArgs();
				EventHandler<EventArgs> changeUserPresenceAfterInactivity = this.ChangeUserPresenceAfterInactivity;
				if (changeUserPresenceAfterInactivity != null)
				{
					changeUserPresenceAfterInactivity(this, e2);
				}
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0002441C File Offset: 0x0002261C
		protected override IList<NotificationPayloadBase> ReadDataAndResetStateInternal()
		{
			IList<NotificationPayloadBase> result = null;
			ExTraceGlobals.InstantMessagingTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageNotifier.ReadDataAndResetStateInternal. SIP Uri: {0}", new object[]
			{
				this.GetUriForUser()
			});
			lock (this)
			{
				result = new List<NotificationPayloadBase>(this.pendingNotifications);
				this.Clear();
			}
			return result;
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00024490 File Offset: 0x00022690
		protected override bool IsDataAvailableForPickup()
		{
			return this.pendingNotifications.Count > 0;
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x000244A0 File Offset: 0x000226A0
		protected virtual void Cancel()
		{
			this.Clear();
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x000244A8 File Offset: 0x000226A8
		private void LogPayloadNotPickedEvent()
		{
			if (!this.isOverMaxSize)
			{
				this.isOverMaxSize = true;
				string uriForUser = this.GetUriForUser();
				ExTraceGlobals.InstantMessagingTracer.TraceError((long)this.GetHashCode(), "InstantMessageNotifier.LogPayloadNotPickedEvent. Payload has grown too large without being picked up. User: {0}", new object[]
				{
					uriForUser
				});
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_PayloadNotBeingPickedup, string.Empty, new object[]
				{
					this.GetUriForUser()
				});
				PendingRequestManager pendingRequestManager = base.UserContext.PendingRequestManager;
				if (pendingRequestManager != null && pendingRequestManager.HasAnyActivePendingGetChannel())
				{
					InstantMessageUtilities.SendWatsonReport("InstantMessageNotifier.LogPayloadNotPickedEvent", (IUserContext)base.UserContext, new OverflowException(string.Format("Payload has grown too large without being picked up. User: {0}", uriForUser)));
				}
				this.Cancel();
				this.Add(new InstantMessagePayload(InstantMessagePayloadType.ServiceUnavailable)
				{
					ServiceError = InstantMessageServiceError.OverMaxPayloadSize
				});
			}
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00024574 File Offset: 0x00022774
		private string GetUriForUser()
		{
			if (((IUserContext)base.UserContext).InstantMessageType != InstantMessagingTypeOptions.Ocs)
			{
				return string.Empty;
			}
			return ((IUserContext)base.UserContext).SipUri;
		}

		// Token: 0x040006DE RID: 1758
		private const int MaxPayloadSize = 10000;

		// Token: 0x040006DF RID: 1759
		private volatile bool isOverMaxSize;

		// Token: 0x040006E0 RID: 1760
		private List<NotificationPayloadBase> pendingNotifications;
	}
}
