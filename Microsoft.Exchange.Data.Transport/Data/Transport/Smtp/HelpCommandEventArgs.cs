using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x02000036 RID: 54
	public class HelpCommandEventArgs : ReceiveCommandEventArgs
	{
		// Token: 0x06000140 RID: 320 RVA: 0x00005F5D File Offset: 0x0000415D
		internal HelpCommandEventArgs()
		{
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005F65 File Offset: 0x00004165
		internal HelpCommandEventArgs(SmtpSession smtpSession, string helpArg = null) : base(smtpSession)
		{
			this.HelpArgument = helpArg;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00005F75 File Offset: 0x00004175
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00005F7D File Offset: 0x0000417D
		public string HelpArgument
		{
			get
			{
				return this.helpArgument;
			}
			set
			{
				this.helpArgument = value;
			}
		}

		// Token: 0x0400014A RID: 330
		private string helpArgument;
	}
}
