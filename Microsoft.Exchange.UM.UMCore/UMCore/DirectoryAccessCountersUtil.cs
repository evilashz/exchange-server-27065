using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000036 RID: 54
	internal abstract class DirectoryAccessCountersUtil
	{
		// Token: 0x06000250 RID: 592 RVA: 0x0000AF92 File Offset: 0x00009192
		protected DirectoryAccessCountersUtil(BaseUMCallSession vo)
		{
			this.session = vo;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000AFA1 File Offset: 0x000091A1
		protected BaseUMCallSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000AFAC File Offset: 0x000091AC
		internal void Increment(DirectoryAccessCountersUtil.DirectoryAccessCounter counterName)
		{
			foreach (ExPerformanceCounter counter in this.GetCounters(counterName))
			{
				this.Session.IncrementCounter(counter);
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000B008 File Offset: 0x00009208
		internal void IncrementSingleCounter(DirectoryAccessCountersUtil.DirectoryAccessCounter counterName)
		{
			this.Session.IncrementCounter(this.GetSingleCounter(counterName));
		}

		// Token: 0x06000254 RID: 596
		protected abstract List<ExPerformanceCounter> GetCounters(DirectoryAccessCountersUtil.DirectoryAccessCounter counter);

		// Token: 0x06000255 RID: 597
		protected abstract ExPerformanceCounter GetSingleCounter(DirectoryAccessCountersUtil.DirectoryAccessCounter counter);

		// Token: 0x040000C1 RID: 193
		private BaseUMCallSession session;

		// Token: 0x02000037 RID: 55
		internal enum DirectoryAccessCounter
		{
			// Token: 0x040000C3 RID: 195
			DirectoryAccess,
			// Token: 0x040000C4 RID: 196
			DirectoryAccessedByExtension,
			// Token: 0x040000C5 RID: 197
			DirectoryAccessedByDialByName,
			// Token: 0x040000C6 RID: 198
			DirectoryAccessedSuccessfullyByDialByName,
			// Token: 0x040000C7 RID: 199
			DirectoryAccessedBySpokenName,
			// Token: 0x040000C8 RID: 200
			DirectoryAccessedSuccessfullyBySpokenName
		}
	}
}
