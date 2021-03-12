using System;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200000E RID: 14
	internal class MailboxSessionContext : DisposeTrackableBase
	{
		// Token: 0x06000081 RID: 129 RVA: 0x0000486D File Offset: 0x00002A6D
		public MailboxSessionContext(ExchangePrincipal mailboxPrincipal, CultureInfo sessionCulture)
		{
			ArgumentValidator.ThrowIfNull("mailboxPrincipal", mailboxPrincipal);
			ArgumentValidator.ThrowIfNull("sessionCulture", sessionCulture);
			this.MailboxPrincipal = mailboxPrincipal;
			this.SessionCulture = sessionCulture;
			this.sessionLock = new object();
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000048A4 File Offset: 0x00002AA4
		// (set) Token: 0x06000083 RID: 131 RVA: 0x000048AC File Offset: 0x00002AAC
		public ExchangePrincipal MailboxPrincipal { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000048B5 File Offset: 0x00002AB5
		// (set) Token: 0x06000085 RID: 133 RVA: 0x000048BD File Offset: 0x00002ABD
		public CultureInfo SessionCulture { get; private set; }

		// Token: 0x06000086 RID: 134 RVA: 0x000048C8 File Offset: 0x00002AC8
		public void DoOperationUnderSessionLock(Action<MailboxSession> operation)
		{
			bool flag = false;
			try
			{
				flag = Monitor.TryEnter(this.sessionLock, MailboxSessionContext.DefaultLockTimeout);
				if (flag)
				{
					try
					{
						if (this.mailboxSession == null)
						{
							this.CreateMailboxSession();
						}
						else
						{
							this.mailboxSession.Connect();
						}
						operation(this.mailboxSession);
						goto IL_81;
					}
					finally
					{
						if (this.mailboxSession != null && this.mailboxSession.IsConnected)
						{
							this.mailboxSession.Disconnect();
						}
					}
					goto IL_60;
					IL_81:
					return;
				}
				IL_60:
				throw new SessionLockTimeoutException(this.MailboxPrincipal.MailboxInfo.MailboxGuid, this.SessionCulture.Name);
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this.sessionLock);
				}
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004984 File Offset: 0x00002B84
		public bool MailboxSessionLockedByCurrentThread()
		{
			return Monitor.IsEntered(this.sessionLock);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004991 File Offset: 0x00002B91
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxSessionContext>(this);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000499C File Offset: 0x00002B9C
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				bool flag = false;
				try
				{
					flag = Monitor.TryEnter(this.sessionLock, MailboxSessionContext.DefaultLockTimeout);
					if (flag && this.mailboxSession != null)
					{
						this.mailboxSession.Dispose();
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(this.sessionLock);
					}
				}
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000049F8 File Offset: 0x00002BF8
		private void CreateMailboxSession()
		{
			this.mailboxSession = MailboxSession.OpenAsSystemService(this.MailboxPrincipal, this.SessionCulture, "Client=NotificationBroker");
		}

		// Token: 0x0400004D RID: 77
		private static readonly TimeSpan DefaultLockTimeout = TimeSpan.FromMinutes(3.0);

		// Token: 0x0400004E RID: 78
		private readonly object sessionLock;

		// Token: 0x0400004F RID: 79
		private MailboxSession mailboxSession;
	}
}
