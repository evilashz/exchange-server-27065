using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.SendAsVerification
{
	// Token: 0x020000A6 RID: 166
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DelayedEmailSender : IEmailSender
	{
		// Token: 0x06000411 RID: 1041 RVA: 0x000165A8 File Offset: 0x000147A8
		public DelayedEmailSender(IEmailSender toWrap)
		{
			SyncUtilities.ThrowIfArgumentNull("toWrap", toWrap);
			this.wrappedEmailSender = toWrap;
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x000165C2 File Offset: 0x000147C2
		public bool SendAttempted
		{
			get
			{
				return this.sendAttempted;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x000165CA File Offset: 0x000147CA
		public bool SendSuccessful
		{
			get
			{
				return this.SendAttempted;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x000165D2 File Offset: 0x000147D2
		public string MessageId
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000165D9 File Offset: 0x000147D9
		public void SendWith(Guid sharedSecret)
		{
			if (this.wrappedEmailSender != EmailSender.NullEmailSender)
			{
				SyncUtilities.ThrowIfGuidEmpty("sharedSecret", sharedSecret);
			}
			this.sendAttempted = true;
			this.sharedSecret = sharedSecret;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00016601 File Offset: 0x00014801
		public IEmailSender TriggerDelayedSend()
		{
			if (!this.SendAttempted)
			{
				return null;
			}
			this.wrappedEmailSender.SendWith(this.sharedSecret);
			return this.wrappedEmailSender;
		}

		// Token: 0x0400027B RID: 635
		private IEmailSender wrappedEmailSender;

		// Token: 0x0400027C RID: 636
		private bool sendAttempted;

		// Token: 0x0400027D RID: 637
		private Guid sharedSecret;
	}
}
