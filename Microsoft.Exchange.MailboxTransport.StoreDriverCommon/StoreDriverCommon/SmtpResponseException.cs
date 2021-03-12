using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverCommon
{
	// Token: 0x0200000F RID: 15
	internal class SmtpResponseException : LocalizedException
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000024A3 File Offset: 0x000006A3
		public SmtpResponseException(SmtpResponse response) : this(response, null)
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000024B0 File Offset: 0x000006B0
		public SmtpResponseException(SmtpResponse response, string source) : base(new LocalizedString(response.ToString()))
		{
			MessageAction action;
			switch (response.SmtpResponseType)
			{
			case SmtpResponseType.Success:
				if (string.IsNullOrEmpty(source))
				{
					throw new ArgumentException("Source must be provided for success smtp response type", "source");
				}
				action = MessageAction.LogProcess;
				goto IL_6E;
			case SmtpResponseType.TransientError:
				action = MessageAction.Retry;
				goto IL_6E;
			case SmtpResponseType.PermanentError:
				action = MessageAction.NDR;
				goto IL_6E;
			}
			throw new ArgumentException("The smtp response type is not supported.", "response");
			IL_6E:
			this.status = new MessageStatus(action, response, this, false, source);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000253B File Offset: 0x0000073B
		public SmtpResponseException(SmtpResponse response, MessageAction action) : base(new LocalizedString(response.ToString()))
		{
			if (action == MessageAction.Throw)
			{
				throw new ArgumentException("MessageAction.Throw is not supported.", "action");
			}
			this.status = new MessageStatus(action, response, this);
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002577 File Offset: 0x00000777
		public MessageStatus Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x04000028 RID: 40
		private MessageStatus status;
	}
}
