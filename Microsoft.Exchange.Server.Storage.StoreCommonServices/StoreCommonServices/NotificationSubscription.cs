using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000E1 RID: 225
	public abstract class NotificationSubscription
	{
		// Token: 0x060008C2 RID: 2242 RVA: 0x00029DE8 File Offset: 0x00027FE8
		protected NotificationSubscription(SubscriptionKind kind, NotificationContext notificationContext, StoreDatabase database, int mailboxNumber, int eventTypeValueMask, NotificationCallback callback)
		{
			this.kind = kind;
			this.notificationContext = notificationContext;
			this.mailboxNumber = mailboxNumber;
			this.database = database;
			this.eventTypeValueMask = eventTypeValueMask;
			this.callback = callback;
			this.userIdentityContext = ((notificationContext != null && notificationContext.Session != null) ? new Guid?(notificationContext.Session.UserGuid) : null);
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00029E54 File Offset: 0x00028054
		public SubscriptionKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x00029E5C File Offset: 0x0002805C
		public NotificationContext NotificationContext
		{
			get
			{
				return this.notificationContext;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x00029E64 File Offset: 0x00028064
		public int MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00029E6C File Offset: 0x0002806C
		public StoreDatabase Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x00029E74 File Offset: 0x00028074
		public int EventTypeValueMask
		{
			get
			{
				return this.eventTypeValueMask;
			}
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00029E7C File Offset: 0x0002807C
		public static void PumpOneNotificationInCurrentContext(Context transactionContext, NotificationEvent nev)
		{
			NotificationSubscription.EnumerateSubscriptionsForEvent(NotificationPublishPhase.Pumping, transactionContext, nev, NotificationSubscription.PumpingPublishCallback);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00029E8C File Offset: 0x0002808C
		internal static void EnumerateSubscriptionsForEvent(NotificationPublishPhase phase, Context transactionContext, NotificationEvent nev, SubscriptionEnumerationCallback callback)
		{
			NotificationSubscription.GetGlobalSubscriptions().EnumerateSubscriptionsForEvent(phase, transactionContext, nev, callback);
			if (nev.MailboxNumber != 0)
			{
				INotificationSubscriptionList mailboxSubscriptions = Mailbox.GetMailboxSubscriptions(transactionContext, nev.MailboxNumber);
				if (mailboxSubscriptions != null)
				{
					mailboxSubscriptions.EnumerateSubscriptionsForEvent(phase, transactionContext, nev, callback);
				}
			}
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00029EC9 File Offset: 0x000280C9
		internal static void PublishNotificationWhilePumping(NotificationPublishPhase phase, Context transactionContext, NotificationSubscription subscription, NotificationEvent nev)
		{
			if (subscription.NotificationContext == NotificationContext.Current && subscription.IsUserInterested(transactionContext, nev) && subscription.IsInterested(nev))
			{
				subscription.PublishEvent(phase, transactionContext, nev);
			}
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00029EF4 File Offset: 0x000280F4
		internal static INotificationSubscriptionList GetGlobalSubscriptions()
		{
			return NotificationSubscription.globalSubscriptions;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00029EFB File Offset: 0x000280FB
		private static void PublishNotificationPreCommit(NotificationPublishPhase phase, Context transactionContext, NotificationSubscription subscription, NotificationEvent nev)
		{
			if ((subscription.NotificationContext == null || subscription.NotificationContext == NotificationContext.Current) && subscription.IsUserInterested(transactionContext, nev) && subscription.IsInterested(nev))
			{
				subscription.PublishEvent(phase, transactionContext, nev);
			}
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00029F2E File Offset: 0x0002812E
		private static void PublishNotificationPostCommit(NotificationPublishPhase phase, Context transactionContext, NotificationSubscription subscription, NotificationEvent nev)
		{
			if (subscription.IsUserInterested(transactionContext, nev) && subscription.IsInterested(nev))
			{
				if (subscription.NotificationContext == null || subscription.NotificationContext == NotificationContext.Current)
				{
					subscription.PublishEvent(phase, transactionContext, nev);
					return;
				}
				subscription.NotificationContext.EnqueueEvent(nev);
			}
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00029F70 File Offset: 0x00028170
		private bool IsUserInterested(Context transactionContext, NotificationEvent nev)
		{
			return nev.UserIdentityContext == null || this.userIdentityContext == null || this.userIdentityContext == nev.UserIdentityContext;
		}

		// Token: 0x060008CF RID: 2255
		public abstract bool IsInterested(NotificationEvent nev);

		// Token: 0x060008D0 RID: 2256 RVA: 0x00029FDC File Offset: 0x000281DC
		public void Register(Context context)
		{
			INotificationSubscriptionList mailboxSubscriptions;
			if (this.MailboxNumber == 0)
			{
				mailboxSubscriptions = NotificationSubscription.GetGlobalSubscriptions();
			}
			else
			{
				mailboxSubscriptions = Mailbox.GetMailboxSubscriptions(context, this.MailboxNumber);
			}
			mailboxSubscriptions.RegisterSubscription(this);
			this.registeredInList = mailboxSubscriptions;
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0002A014 File Offset: 0x00028214
		public void Unregister()
		{
			if (this.registeredInList != null)
			{
				this.registeredInList.UnregisterSubscription(this);
				this.registeredInList = null;
			}
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0002A031 File Offset: 0x00028231
		internal void PublishEvent(NotificationPublishPhase phase, Context transactionContext, NotificationEvent nev)
		{
			this.callback(phase, transactionContext, nev);
		}

		// Token: 0x060008D3 RID: 2259
		protected abstract void AppendClassName(StringBuilder sb);

		// Token: 0x060008D4 RID: 2260 RVA: 0x0002A044 File Offset: 0x00028244
		protected virtual void AppendFields(StringBuilder sb)
		{
			sb.Append("Kind:[");
			sb.Append(this.Kind);
			sb.Append("] MailboxGuid:[");
			sb.Append(this.MailboxNumber);
			sb.Append("] EventTypeValueMask:[");
			sb.Append(this.EventTypeValueMask);
			sb.Append("] NotificationContext:[");
			sb.Append(this.NotificationContext);
			sb.Append("] Callback:[");
			sb.Append(this.callback);
			sb.Append("]");
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0002A0E0 File Offset: 0x000282E0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(250);
			this.AppendClassName(stringBuilder);
			stringBuilder.Append(":[");
			this.AppendFields(stringBuilder);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000516 RID: 1302
		private const int AvgInterestedSubscriptionsForEvent = 10;

		// Token: 0x04000517 RID: 1303
		private static NotificationSubscription.GlobalNotificationSubscriptionList globalSubscriptions = new NotificationSubscription.GlobalNotificationSubscriptionList();

		// Token: 0x04000518 RID: 1304
		internal static readonly SubscriptionEnumerationCallback PreCommitPublishCallback = new SubscriptionEnumerationCallback(NotificationSubscription.PublishNotificationPreCommit);

		// Token: 0x04000519 RID: 1305
		internal static readonly SubscriptionEnumerationCallback PostCommitPublishCallback = new SubscriptionEnumerationCallback(NotificationSubscription.PublishNotificationPostCommit);

		// Token: 0x0400051A RID: 1306
		internal static readonly SubscriptionEnumerationCallback PumpingPublishCallback = new SubscriptionEnumerationCallback(NotificationSubscription.PublishNotificationWhilePumping);

		// Token: 0x0400051B RID: 1307
		private SubscriptionKind kind;

		// Token: 0x0400051C RID: 1308
		private NotificationContext notificationContext;

		// Token: 0x0400051D RID: 1309
		private int mailboxNumber;

		// Token: 0x0400051E RID: 1310
		private StoreDatabase database;

		// Token: 0x0400051F RID: 1311
		private int eventTypeValueMask;

		// Token: 0x04000520 RID: 1312
		private NotificationCallback callback;

		// Token: 0x04000521 RID: 1313
		private INotificationSubscriptionList registeredInList;

		// Token: 0x04000522 RID: 1314
		private Guid? userIdentityContext;

		// Token: 0x020000E2 RID: 226
		internal class GlobalNotificationSubscriptionList : INotificationSubscriptionList
		{
			// Token: 0x17000248 RID: 584
			// (get) Token: 0x060008D8 RID: 2264 RVA: 0x0002A16B File Offset: 0x0002836B
			// (set) Token: 0x060008D9 RID: 2265 RVA: 0x0002A173 File Offset: 0x00028373
			internal NotificationSubscription SingleGlobalSubscription
			{
				get
				{
					return this.singleGlobalSubscription;
				}
				set
				{
					this.singleGlobalSubscription = value;
				}
			}

			// Token: 0x060008DA RID: 2266 RVA: 0x0002A17C File Offset: 0x0002837C
			public void RegisterSubscription(NotificationSubscription subscription)
			{
				this.singleGlobalSubscription = subscription;
			}

			// Token: 0x060008DB RID: 2267 RVA: 0x0002A185 File Offset: 0x00028385
			public void UnregisterSubscription(NotificationSubscription subscription)
			{
				this.singleGlobalSubscription = null;
			}

			// Token: 0x060008DC RID: 2268 RVA: 0x0002A190 File Offset: 0x00028390
			public void EnumerateSubscriptionsForEvent(NotificationPublishPhase phase, Context transactionContext, NotificationEvent nev, SubscriptionEnumerationCallback callback)
			{
				NotificationSubscription notificationSubscription = this.singleGlobalSubscription;
				if (notificationSubscription != null && (notificationSubscription.EventTypeValueMask & nev.EventTypeValue) != 0 && (notificationSubscription.Kind & (SubscriptionKind)phase) != (SubscriptionKind)0U)
				{
					if (ExTraceGlobals.NotificationTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.NotificationTracer.TraceDebug<NotificationEvent>(36857L, "GlobalNotifEnumeration: {0}", nev);
					}
					callback(phase, transactionContext, notificationSubscription, nev);
					return;
				}
				if (ExTraceGlobals.NotificationTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.NotificationTracer.TraceDebug<NotificationEvent, NotificationSubscription>(30628L, "GlobalNotifEnumeration: Skipping callback for {0}, {1}", nev, notificationSubscription);
				}
			}

			// Token: 0x04000523 RID: 1315
			private NotificationSubscription singleGlobalSubscription;
		}
	}
}
