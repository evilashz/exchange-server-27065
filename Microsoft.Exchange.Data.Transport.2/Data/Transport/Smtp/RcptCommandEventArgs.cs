using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x02000039 RID: 57
	public class RcptCommandEventArgs : ReceiveCommandEventArgs
	{
		// Token: 0x0600015F RID: 351 RVA: 0x000060BC File Offset: 0x000042BC
		internal RcptCommandEventArgs()
		{
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000060C4 File Offset: 0x000042C4
		internal RcptCommandEventArgs(SmtpSession smtpSession) : base(smtpSession)
		{
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000060CD File Offset: 0x000042CD
		// (set) Token: 0x06000162 RID: 354 RVA: 0x000060D5 File Offset: 0x000042D5
		public DsnTypeRequested Notify
		{
			get
			{
				return this.notify;
			}
			set
			{
				this.notify = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000060DE File Offset: 0x000042DE
		// (set) Token: 0x06000164 RID: 356 RVA: 0x000060E6 File Offset: 0x000042E6
		public string OriginalRecipient
		{
			get
			{
				return this.orcpt;
			}
			set
			{
				this.orcpt = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000060EF File Offset: 0x000042EF
		// (set) Token: 0x06000166 RID: 358 RVA: 0x000060F7 File Offset: 0x000042F7
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000611F File Offset: 0x0000431F
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00006127 File Offset: 0x00004327
		public MailItem MailItem
		{
			get
			{
				return this.mailItem;
			}
			internal set
			{
				this.mailItem = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00006130 File Offset: 0x00004330
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00006138 File Offset: 0x00004338
		internal Dictionary<string, string> ConsumerMailOptionalArguments { get; set; }

		// Token: 0x04000157 RID: 343
		private DsnTypeRequested notify;

		// Token: 0x04000158 RID: 344
		private string orcpt;

		// Token: 0x04000159 RID: 345
		private MailItem mailItem;

		// Token: 0x0400015A RID: 346
		private RoutingAddress recipientAddress;
	}
}
