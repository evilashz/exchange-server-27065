using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x0200002A RID: 42
	internal class ResourceEventCounterCrashInfo
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x0000B4CF File Offset: 0x000096CF
		public ResourceEventCounterCrashInfo(SortedSet<DateTime> crashTimes, bool isPoisonNdrSent)
		{
			this.crashTimes = crashTimes;
			this.IsPoisonNdrSent = isPoisonNdrSent;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000B4F0 File Offset: 0x000096F0
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x0000B4F8 File Offset: 0x000096F8
		public bool IsPoisonNdrSent { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000B501 File Offset: 0x00009701
		public SortedSet<DateTime> CrashTimes
		{
			get
			{
				return this.crashTimes;
			}
		}

		// Token: 0x040000C6 RID: 198
		private readonly SortedSet<DateTime> crashTimes = new SortedSet<DateTime>();
	}
}
