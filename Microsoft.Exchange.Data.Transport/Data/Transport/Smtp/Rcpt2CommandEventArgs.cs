using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x0200003A RID: 58
	internal class Rcpt2CommandEventArgs : ReceiveCommandEventArgs
	{
		// Token: 0x0600016B RID: 363 RVA: 0x00006141 File Offset: 0x00004341
		public Rcpt2CommandEventArgs(SmtpSession smtpSession) : base(smtpSession)
		{
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000614A File Offset: 0x0000434A
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00006152 File Offset: 0x00004352
		public RoutingAddress RecipientAddress
		{
			get
			{
				return this.recipientAddress;
			}
			set
			{
				if (!value.IsValid)
				{
					throw new ArgumentException(string.Format("The specified address is an invalid SMTP address - {0}", value));
				}
				this.recipientAddress = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000617A File Offset: 0x0000437A
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00006182 File Offset: 0x00004382
		public MailItem MailItem { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000618B File Offset: 0x0000438B
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00006193 File Offset: 0x00004393
		public Dictionary<string, string> ConsumerMailOptionalArguments { get; set; }

		// Token: 0x0400015C RID: 348
		private RoutingAddress recipientAddress;
	}
}
