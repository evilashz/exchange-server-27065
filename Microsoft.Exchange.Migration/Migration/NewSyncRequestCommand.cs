using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000179 RID: 377
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NewSyncRequestCommand : NewSyncRequestCommandBase
	{
		// Token: 0x060011C1 RID: 4545 RVA: 0x0004AF83 File Offset: 0x00049183
		public NewSyncRequestCommand(bool whatIf) : base("New-SyncRequest", NewSyncRequestCommand.ExceptionsToIgnore)
		{
			base.WhatIf = whatIf;
		}

		// Token: 0x1700054E RID: 1358
		// (set) Token: 0x060011C2 RID: 4546 RVA: 0x0004AF9C File Offset: 0x0004919C
		public string Name
		{
			set
			{
				base.AddParameter("Name", value);
			}
		}

		// Token: 0x1700054F RID: 1359
		// (set) Token: 0x060011C3 RID: 4547 RVA: 0x0004AFAA File Offset: 0x000491AA
		public object Mailbox
		{
			set
			{
				base.AddParameter("Mailbox", value);
			}
		}

		// Token: 0x17000550 RID: 1360
		// (set) Token: 0x060011C4 RID: 4548 RVA: 0x0004AFB8 File Offset: 0x000491B8
		public SmtpAddress RemoteEmailAddress
		{
			set
			{
				base.AddParameter("RemoteEmailAddress", value);
			}
		}

		// Token: 0x17000551 RID: 1361
		// (set) Token: 0x060011C5 RID: 4549 RVA: 0x0004AFCB File Offset: 0x000491CB
		public string UserName
		{
			set
			{
				base.AddParameter("UserName", value);
			}
		}

		// Token: 0x17000552 RID: 1362
		// (set) Token: 0x060011C6 RID: 4550 RVA: 0x0004AFD9 File Offset: 0x000491D9
		public bool Imap
		{
			set
			{
				base.AddParameter("Imap", value);
			}
		}

		// Token: 0x17000553 RID: 1363
		// (set) Token: 0x060011C7 RID: 4551 RVA: 0x0004AFEC File Offset: 0x000491EC
		public bool Olc
		{
			set
			{
				base.AddParameter("Olc", value);
			}
		}

		// Token: 0x17000554 RID: 1364
		// (set) Token: 0x060011C8 RID: 4552 RVA: 0x0004AFFF File Offset: 0x000491FF
		public RequestWorkloadType WorkloadType
		{
			set
			{
				base.AddParameter("WorkloadType", value);
			}
		}

		// Token: 0x17000555 RID: 1365
		// (set) Token: 0x060011C9 RID: 4553 RVA: 0x0004B012 File Offset: 0x00049212
		public string SkipMerging
		{
			set
			{
				base.AddParameter("SkipMerging", value);
			}
		}

		// Token: 0x04000628 RID: 1576
		public const string CmdletName = "New-SyncRequest";

		// Token: 0x04000629 RID: 1577
		internal const string MailboxParameter = "Mailbox";

		// Token: 0x0400062A RID: 1578
		internal const string RemoteServerNameParameter = "RemoteServerName";

		// Token: 0x0400062B RID: 1579
		internal const string RemoteServerPortParameter = "RemoteServerPort";

		// Token: 0x0400062C RID: 1580
		internal const string RemoteEmailAddressParameter = "RemoteEmailAddress";

		// Token: 0x0400062D RID: 1581
		internal const string UserNameParameter = "UserName";

		// Token: 0x0400062E RID: 1582
		internal const string PasswordParameter = "Password";

		// Token: 0x0400062F RID: 1583
		internal const string SecurityParameter = "Security";

		// Token: 0x04000630 RID: 1584
		internal const string AuthenticationParameter = "Authentication";

		// Token: 0x04000631 RID: 1585
		internal const string ImapParameter = "Imap";

		// Token: 0x04000632 RID: 1586
		internal const string OlcParameter = "Olc";

		// Token: 0x04000633 RID: 1587
		internal const string WorkloadTypeParameter = "WorkloadType";

		// Token: 0x04000634 RID: 1588
		private static readonly Type[] ExceptionsToIgnore = new Type[0];
	}
}
