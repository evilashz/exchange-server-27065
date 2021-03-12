using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x0200002F RID: 47
	public class DataCommandEventArgs : ReceiveCommandEventArgs
	{
		// Token: 0x06000122 RID: 290 RVA: 0x00005D82 File Offset: 0x00003F82
		internal DataCommandEventArgs()
		{
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005D8A File Offset: 0x00003F8A
		internal DataCommandEventArgs(SmtpSession smtpSession) : base(smtpSession)
		{
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00005D93 File Offset: 0x00003F93
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00005D9B File Offset: 0x00003F9B
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

		// Token: 0x04000142 RID: 322
		private MailItem mailItem;
	}
}
