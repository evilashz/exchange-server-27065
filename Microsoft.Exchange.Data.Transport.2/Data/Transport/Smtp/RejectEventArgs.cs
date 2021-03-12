using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x0200003B RID: 59
	public class RejectEventArgs : ReceiveEventArgs
	{
		// Token: 0x17000059 RID: 89
		// (set) Token: 0x06000172 RID: 370 RVA: 0x0000619C File Offset: 0x0000439C
		internal byte[] RawCommand
		{
			set
			{
				this.command = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000061A5 File Offset: 0x000043A5
		public string Command
		{
			get
			{
				if (this.commandString == null)
				{
					this.commandString = CTSGlobals.AsciiEncoding.GetString(this.command);
				}
				return this.commandString;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000061CB File Offset: 0x000043CB
		// (set) Token: 0x06000175 RID: 373 RVA: 0x000061D3 File Offset: 0x000043D3
		public EventArgs OriginalArguments
		{
			get
			{
				return this.originalArguments;
			}
			internal set
			{
				this.originalArguments = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000176 RID: 374 RVA: 0x000061DC File Offset: 0x000043DC
		// (set) Token: 0x06000177 RID: 375 RVA: 0x000061E4 File Offset: 0x000043E4
		public ParsingStatus ParsingStatus
		{
			get
			{
				return this.parsingStatus;
			}
			internal set
			{
				this.parsingStatus = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000178 RID: 376 RVA: 0x000061ED File Offset: 0x000043ED
		// (set) Token: 0x06000179 RID: 377 RVA: 0x000061F5 File Offset: 0x000043F5
		public SmtpResponse SmtpResponse
		{
			get
			{
				return this.smtpResponse;
			}
			internal set
			{
				this.smtpResponse = value;
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000061FE File Offset: 0x000043FE
		internal RejectEventArgs(SmtpSession smtpSession) : base(smtpSession)
		{
		}

		// Token: 0x0400015F RID: 351
		private byte[] command;

		// Token: 0x04000160 RID: 352
		private EventArgs originalArguments;

		// Token: 0x04000161 RID: 353
		private ParsingStatus parsingStatus;

		// Token: 0x04000162 RID: 354
		private SmtpResponse smtpResponse;

		// Token: 0x04000163 RID: 355
		private string commandString;
	}
}
