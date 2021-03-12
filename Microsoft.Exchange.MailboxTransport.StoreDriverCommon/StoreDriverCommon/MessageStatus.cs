using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverCommon
{
	// Token: 0x0200000D RID: 13
	internal class MessageStatus
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000023E8 File Offset: 0x000005E8
		internal MessageStatus(MessageAction action, SmtpResponse smtpResponse, Exception exception) : this(action, smtpResponse, exception, false)
		{
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023F4 File Offset: 0x000005F4
		internal MessageStatus(MessageAction action, SmtpResponse smtpResponse, Exception exception, bool forAllRecipients) : this(action, smtpResponse, exception, forAllRecipients, null)
		{
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002402 File Offset: 0x00000602
		internal MessageStatus(MessageAction action, SmtpResponse smtpResponse, Exception exception, bool forAllRecipients, string source)
		{
			this.action = action;
			this.smtpResponse = smtpResponse;
			this.exception = exception;
			this.forAllRecipients = forAllRecipients;
			this.source = source;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000242F File Offset: 0x0000062F
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002437 File Offset: 0x00000637
		internal MessageAction Action
		{
			get
			{
				return this.action;
			}
			set
			{
				this.action = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002440 File Offset: 0x00000640
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002448 File Offset: 0x00000648
		internal SmtpResponse Response
		{
			get
			{
				return this.smtpResponse;
			}
			set
			{
				this.smtpResponse = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002451 File Offset: 0x00000651
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002459 File Offset: 0x00000659
		internal TimeSpan? RetryInterval
		{
			get
			{
				return this.retryInterval;
			}
			set
			{
				this.retryInterval = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002462 File Offset: 0x00000662
		internal Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000246A File Offset: 0x0000066A
		internal bool NDRForAllRecipients
		{
			get
			{
				return this.forAllRecipients && this.action == MessageAction.NDR;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000247F File Offset: 0x0000067F
		internal string Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x04000021 RID: 33
		internal static readonly MessageStatus Success = new MessageStatus(MessageAction.Success, SmtpResponse.Empty, null);

		// Token: 0x04000022 RID: 34
		private readonly bool forAllRecipients;

		// Token: 0x04000023 RID: 35
		private readonly string source;

		// Token: 0x04000024 RID: 36
		private MessageAction action;

		// Token: 0x04000025 RID: 37
		private SmtpResponse smtpResponse;

		// Token: 0x04000026 RID: 38
		private Exception exception;

		// Token: 0x04000027 RID: 39
		private TimeSpan? retryInterval;

		// Token: 0x0200000E RID: 14
		private class MailboxDatabaseOfflineException : StorageTransientException
		{
			// Token: 0x0600002B RID: 43 RVA: 0x0000249A File Offset: 0x0000069A
			public MailboxDatabaseOfflineException(LocalizedString message) : base(message)
			{
			}
		}
	}
}
