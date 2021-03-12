using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.MailboxTransport.StoreDriver
{
	// Token: 0x0200000D RID: 13
	internal class SmtpResponseException : LocalizedException
	{
		// Token: 0x06000067 RID: 103 RVA: 0x000039F6 File Offset: 0x00001BF6
		public SmtpResponseException(SmtpResponse response) : this(response, null)
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003A00 File Offset: 0x00001C00
		public SmtpResponseException(SmtpResponse response, string source) : base(new LocalizedString(response.ToString()))
		{
			MessageAction action;
			switch (response.SmtpResponseType)
			{
			case SmtpResponseType.Success:
				if (string.IsNullOrEmpty(source))
				{
					throw new ArgumentException("Source must be provided for success smtp response type", "response");
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

		// Token: 0x06000069 RID: 105 RVA: 0x00003A8B File Offset: 0x00001C8B
		public SmtpResponseException(SmtpResponse response, MessageAction action) : base(new LocalizedString(response.ToString()))
		{
			if (action == MessageAction.Throw)
			{
				throw new ArgumentException("MessageAction.Throw is not supported.", "action");
			}
			this.status = new MessageStatus(action, response, this);
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003AC7 File Offset: 0x00001CC7
		public MessageStatus Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x04000048 RID: 72
		private MessageStatus status;
	}
}
