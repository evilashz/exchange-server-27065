using System;

namespace Microsoft.Exchange.Rpc.MailSubmission
{
	// Token: 0x0200028C RID: 652
	internal class MailSubmissionResult
	{
		// Token: 0x06000C29 RID: 3113 RVA: 0x0002B428 File Offset: 0x0002A828
		public MailSubmissionResult(uint ec)
		{
			this.errorCode = ec;
			base..ctor();
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0002B414 File Offset: 0x0002A814
		public MailSubmissionResult()
		{
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x0002B444 File Offset: 0x0002A844
		// (set) Token: 0x06000C2C RID: 3116 RVA: 0x0002B458 File Offset: 0x0002A858
		public uint ErrorCode
		{
			get
			{
				return this.errorCode;
			}
			set
			{
				this.errorCode = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x0002B46C File Offset: 0x0002A86C
		// (set) Token: 0x06000C2E RID: 3118 RVA: 0x0002B480 File Offset: 0x0002A880
		public string Sender
		{
			get
			{
				return this.sender;
			}
			set
			{
				this.sender = value;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0002B494 File Offset: 0x0002A894
		// (set) Token: 0x06000C30 RID: 3120 RVA: 0x0002B4A8 File Offset: 0x0002A8A8
		public string From
		{
			get
			{
				return this.from;
			}
			set
			{
				this.from = value;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0002B4BC File Offset: 0x0002A8BC
		// (set) Token: 0x06000C32 RID: 3122 RVA: 0x0002B4D0 File Offset: 0x0002A8D0
		public string MessageId
		{
			get
			{
				return this.internetMessageId;
			}
			set
			{
				this.internetMessageId = value;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0002B4E4 File Offset: 0x0002A8E4
		// (set) Token: 0x06000C34 RID: 3124 RVA: 0x0002B4F8 File Offset: 0x0002A8F8
		public string Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				this.subject = value;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x0002B50C File Offset: 0x0002A90C
		// (set) Token: 0x06000C36 RID: 3126 RVA: 0x0002B520 File Offset: 0x0002A920
		public string DiagnosticInfo
		{
			get
			{
				return this.diagnosticInfo;
			}
			set
			{
				this.diagnosticInfo = value;
			}
		}

		// Token: 0x04000D2F RID: 3375
		private uint errorCode;

		// Token: 0x04000D30 RID: 3376
		private string sender;

		// Token: 0x04000D31 RID: 3377
		private string from;

		// Token: 0x04000D32 RID: 3378
		private string internetMessageId;

		// Token: 0x04000D33 RID: 3379
		private string subject;

		// Token: 0x04000D34 RID: 3380
		private string diagnosticInfo;
	}
}
