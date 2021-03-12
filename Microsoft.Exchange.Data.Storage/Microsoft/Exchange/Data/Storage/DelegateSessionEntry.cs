using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000693 RID: 1683
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DelegateSessionEntry
	{
		// Token: 0x060044DD RID: 17629 RVA: 0x0012543B File Offset: 0x0012363B
		internal DelegateSessionEntry(MailboxSession mailboxSession, OpenBy openBy) : this(mailboxSession.MailboxOwnerLegacyDN, mailboxSession, openBy)
		{
		}

		// Token: 0x060044DE RID: 17630 RVA: 0x0012544B File Offset: 0x0012364B
		private DelegateSessionEntry(string mailboxLegacyDn, MailboxSession mailboxSession, OpenBy openBy)
		{
			this.mailboxLegacyDn = mailboxLegacyDn;
			this.mailboxSession = mailboxSession;
			this.stackTrace = new StackTrace();
			this.Access(openBy);
		}

		// Token: 0x17001403 RID: 5123
		// (get) Token: 0x060044DF RID: 17631 RVA: 0x00125473 File Offset: 0x00123673
		internal MailboxSession MailboxSession
		{
			get
			{
				return this.mailboxSession;
			}
		}

		// Token: 0x060044E0 RID: 17632 RVA: 0x0012547C File Offset: 0x0012367C
		internal void ForceDispose()
		{
			if (this.MailboxSession.IsDisposed)
			{
				return;
			}
			if (!this.MailboxSession.IsDead && this.MailboxSession.IsConnected)
			{
				this.MailboxSession.Disconnect();
			}
			this.MailboxSession.CanDispose = true;
			this.MailboxSession.Dispose();
		}

		// Token: 0x17001404 RID: 5124
		// (get) Token: 0x060044E1 RID: 17633 RVA: 0x001254D3 File Offset: 0x001236D3
		internal int ExternalRefCount
		{
			get
			{
				return this.externalRefCount;
			}
		}

		// Token: 0x060044E2 RID: 17634 RVA: 0x001254DC File Offset: 0x001236DC
		internal int DecrementExternalRefCount()
		{
			int result = this.externalRefCount;
			this.externalRefCount--;
			if (this.externalRefCount == 0 && this.IsConnected)
			{
				this.Disconnect();
			}
			return result;
		}

		// Token: 0x060044E3 RID: 17635 RVA: 0x00125518 File Offset: 0x00123718
		internal string GetCallStack()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (StackFrame stackFrame in this.stackTrace.GetFrames())
			{
				stringBuilder.AppendLine(stackFrame.GetMethod().ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x17001405 RID: 5125
		// (get) Token: 0x060044E4 RID: 17636 RVA: 0x00125561 File Offset: 0x00123761
		internal bool IsConnected
		{
			get
			{
				return this.MailboxSession.IsConnected;
			}
		}

		// Token: 0x060044E5 RID: 17637 RVA: 0x0012556E File Offset: 0x0012376E
		internal void Disconnect()
		{
			this.MailboxSession.Disconnect();
		}

		// Token: 0x060044E6 RID: 17638 RVA: 0x0012557B File Offset: 0x0012377B
		internal void Connect()
		{
			this.MailboxSession.Connect();
		}

		// Token: 0x060044E7 RID: 17639 RVA: 0x00125588 File Offset: 0x00123788
		internal void Access(OpenBy openBy)
		{
			if (openBy == OpenBy.Consumer)
			{
				this.externalRefCount++;
			}
			this.lastAccessed = DelegateSessionEntry.GetNextWaterMark();
		}

		// Token: 0x17001406 RID: 5126
		// (get) Token: 0x060044E8 RID: 17640 RVA: 0x001255A6 File Offset: 0x001237A6
		internal int LastAccessed
		{
			get
			{
				return this.lastAccessed;
			}
		}

		// Token: 0x17001407 RID: 5127
		// (get) Token: 0x060044E9 RID: 17641 RVA: 0x001255AE File Offset: 0x001237AE
		internal string LegacyDn
		{
			get
			{
				return this.mailboxLegacyDn;
			}
		}

		// Token: 0x060044EA RID: 17642 RVA: 0x001255B6 File Offset: 0x001237B6
		private static int GetNextWaterMark()
		{
			return Interlocked.Increment(ref DelegateSessionEntry.waterMark);
		}

		// Token: 0x0400257A RID: 9594
		private int externalRefCount;

		// Token: 0x0400257B RID: 9595
		private readonly string mailboxLegacyDn;

		// Token: 0x0400257C RID: 9596
		private readonly MailboxSession mailboxSession;

		// Token: 0x0400257D RID: 9597
		private int lastAccessed;

		// Token: 0x0400257E RID: 9598
		private static int waterMark;

		// Token: 0x0400257F RID: 9599
		private StackTrace stackTrace;
	}
}
