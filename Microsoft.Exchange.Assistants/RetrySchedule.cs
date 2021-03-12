using System;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000077 RID: 119
	internal sealed class RetrySchedule
	{
		// Token: 0x06000377 RID: 887 RVA: 0x0001142A File Offset: 0x0000F62A
		public RetrySchedule(FinalAction finalAction, TimeSpan timeToGiveUp, params TimeSpan[] retryIntervals)
		{
			this.finalAction = finalAction;
			this.timeToGiveUp = timeToGiveUp;
			this.retryIntervals = retryIntervals;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00011447 File Offset: 0x0000F647
		// (set) Token: 0x06000379 RID: 889 RVA: 0x0001144F File Offset: 0x0000F64F
		public TimeSpan[] RetryIntervals
		{
			get
			{
				return this.retryIntervals;
			}
			set
			{
				this.retryIntervals = value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00011458 File Offset: 0x0000F658
		// (set) Token: 0x0600037B RID: 891 RVA: 0x00011460 File Offset: 0x0000F660
		public TimeSpan TimeToGiveUp
		{
			get
			{
				return this.timeToGiveUp;
			}
			set
			{
				this.timeToGiveUp = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00011469 File Offset: 0x0000F669
		// (set) Token: 0x0600037D RID: 893 RVA: 0x00011471 File Offset: 0x0000F671
		public FinalAction FinalAction
		{
			get
			{
				return this.finalAction;
			}
			set
			{
				this.finalAction = value;
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001147A File Offset: 0x0000F67A
		public TimeSpan GetRetryInterval(uint n)
		{
			if ((ulong)n < (ulong)((long)this.RetryIntervals.Length))
			{
				return this.RetryIntervals[(int)((UIntPtr)n)];
			}
			return this.RetryIntervals[this.RetryIntervals.Length - 1];
		}

		// Token: 0x040001FC RID: 508
		private FinalAction finalAction;

		// Token: 0x040001FD RID: 509
		private TimeSpan timeToGiveUp;

		// Token: 0x040001FE RID: 510
		private TimeSpan[] retryIntervals;
	}
}
