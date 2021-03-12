using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200009D RID: 157
	[ClassAccessLevel(AccessLevel.Implementation)]
	public sealed class MapiNotificationsLogEvent : ILogEvent
	{
		// Token: 0x06000626 RID: 1574 RVA: 0x0001221C File Offset: 0x0001041C
		internal MapiNotificationsLogEvent(ExchangePrincipal exchangePrincipal, string userContext, MapiNotificationHandlerBase notificationHandler, string methodName)
		{
			if (!Globals.Owa2ServerUnitTestsHook && exchangePrincipal == null)
			{
				throw new ArgumentNullException("exchangePrincipal");
			}
			if (notificationHandler == null)
			{
				throw new ArgumentNullException("notificationHandler");
			}
			this.exchangePrincipal = exchangePrincipal;
			this.userContext = (userContext ?? string.Empty);
			this.notificationHandler = notificationHandler;
			this.MethodName = (methodName ?? string.Empty);
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x00012281 File Offset: 0x00010481
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x00012289 File Offset: 0x00010489
		public bool InvalidNotification { get; set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x00012292 File Offset: 0x00010492
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x0001229A File Offset: 0x0001049A
		public bool NullNotification { get; set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x000122A3 File Offset: 0x000104A3
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x000122AB File Offset: 0x000104AB
		public bool RefreshPayloadSent { get; set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x000122B4 File Offset: 0x000104B4
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x000122BC File Offset: 0x000104BC
		public Exception HandledException { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x000122C5 File Offset: 0x000104C5
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x000122CD File Offset: 0x000104CD
		public string MethodName { get; set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x000122D6 File Offset: 0x000104D6
		public string EventId
		{
			get
			{
				return "Notifications";
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x000122E0 File Offset: 0x000104E0
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			ICollection<KeyValuePair<string, object>> collection = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>(UserContextCookie.UserContextCookiePrefix, this.userContext),
				new KeyValuePair<string, object>("MG", this.exchangePrincipal.MailboxInfo.MailboxGuid.ToString()),
				new KeyValuePair<string, object>("PSA", this.exchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString()),
				new KeyValuePair<string, object>("MN", this.MethodName),
				new KeyValuePair<string, object>("Miss", this.notificationHandler.MissedNotifications.ToString()),
				new KeyValuePair<string, object>("NRS", this.notificationHandler.NeedToReinitSubscriptions.ToString()),
				new KeyValuePair<string, object>("NRP", this.notificationHandler.NeedRefreshPayload.ToString()),
				new KeyValuePair<string, object>("IN", this.InvalidNotification.ToString()),
				new KeyValuePair<string, object>("NN", this.NullNotification.ToString()),
				new KeyValuePair<string, object>("RPS", this.RefreshPayloadSent.ToString()),
				new KeyValuePair<string, object>("D", this.notificationHandler.IsDisposed.ToString())
			};
			if (this.HandledException != null)
			{
				collection.Add(new KeyValuePair<string, object>("Ex", this.HandledException.ToString()));
			}
			return collection;
		}

		// Token: 0x04000377 RID: 887
		private readonly ExchangePrincipal exchangePrincipal;

		// Token: 0x04000378 RID: 888
		private readonly string userContext;

		// Token: 0x04000379 RID: 889
		private readonly MapiNotificationHandlerBase notificationHandler;
	}
}
