using System;
using Microsoft.Exchange.Data.Mime;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x02000034 RID: 52
	public class EndOfHeadersEventArgs : ReceiveEventArgs
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00005E81 File Offset: 0x00004081
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00005E89 File Offset: 0x00004089
		public HeaderList Headers
		{
			get
			{
				return this.headers;
			}
			internal set
			{
				this.headers = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00005E92 File Offset: 0x00004092
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00005E9A File Offset: 0x0000409A
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

		// Token: 0x0600013A RID: 314 RVA: 0x00005EA3 File Offset: 0x000040A3
		internal EndOfHeadersEventArgs(SmtpSession smtpSession) : base(smtpSession)
		{
		}

		// Token: 0x04000147 RID: 327
		private HeaderList headers;

		// Token: 0x04000148 RID: 328
		private MailItem mailItem;
	}
}
