using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x02000033 RID: 51
	public class EndOfDataEventArgs : ReceiveEventArgs
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00005E67 File Offset: 0x00004067
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00005E6F File Offset: 0x0000406F
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

		// Token: 0x06000135 RID: 309 RVA: 0x00005E78 File Offset: 0x00004078
		internal EndOfDataEventArgs(SmtpSession smtpSession) : base(smtpSession)
		{
		}

		// Token: 0x04000146 RID: 326
		private MailItem mailItem;
	}
}
