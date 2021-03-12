using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.MailboxTransport.StoreDriver
{
	// Token: 0x0200000B RID: 11
	internal class MessageStatus
	{
		// Token: 0x06000059 RID: 89 RVA: 0x0000393B File Offset: 0x00001B3B
		internal MessageStatus(MessageAction action, SmtpResponse smtpResponse, Exception exception) : this(action, smtpResponse, exception, false)
		{
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003947 File Offset: 0x00001B47
		internal MessageStatus(MessageAction action, SmtpResponse smtpResponse, Exception exception, bool forAllRecipients) : this(action, smtpResponse, exception, forAllRecipients, null)
		{
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003955 File Offset: 0x00001B55
		internal MessageStatus(MessageAction action, SmtpResponse smtpResponse, Exception exception, bool forAllRecipients, string source)
		{
			this.action = action;
			this.smtpResponse = smtpResponse;
			this.exception = exception;
			this.forAllRecipients = forAllRecipients;
			this.source = source;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003982 File Offset: 0x00001B82
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000398A File Offset: 0x00001B8A
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

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003993 File Offset: 0x00001B93
		// (set) Token: 0x0600005F RID: 95 RVA: 0x0000399B File Offset: 0x00001B9B
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

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000039A4 File Offset: 0x00001BA4
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000039AC File Offset: 0x00001BAC
		public TimeSpan? RetryInterval
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

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000039B5 File Offset: 0x00001BB5
		internal Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000039BD File Offset: 0x00001BBD
		internal bool NDRForAllRecipients
		{
			get
			{
				return this.forAllRecipients && this.action == MessageAction.NDR;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000039D2 File Offset: 0x00001BD2
		internal string Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x04000041 RID: 65
		internal static readonly MessageStatus Success = new MessageStatus(MessageAction.Success, SmtpResponse.Empty, null);

		// Token: 0x04000042 RID: 66
		private MessageAction action;

		// Token: 0x04000043 RID: 67
		private SmtpResponse smtpResponse;

		// Token: 0x04000044 RID: 68
		private Exception exception;

		// Token: 0x04000045 RID: 69
		private bool forAllRecipients;

		// Token: 0x04000046 RID: 70
		private TimeSpan? retryInterval;

		// Token: 0x04000047 RID: 71
		private string source;

		// Token: 0x0200000C RID: 12
		private class MailboxDatabaseOfflineException : StorageTransientException
		{
			// Token: 0x06000066 RID: 102 RVA: 0x000039ED File Offset: 0x00001BED
			public MailboxDatabaseOfflineException(LocalizedString message) : base(message)
			{
			}
		}
	}
}
