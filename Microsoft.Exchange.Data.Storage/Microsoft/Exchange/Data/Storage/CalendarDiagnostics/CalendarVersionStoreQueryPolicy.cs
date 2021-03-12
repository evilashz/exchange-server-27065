using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.CalendarDiagnostics
{
	// Token: 0x02000374 RID: 884
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct CalendarVersionStoreQueryPolicy
	{
		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x06002704 RID: 9988 RVA: 0x0009C5F9 File Offset: 0x0009A7F9
		public TimeSpan WaitTimeForPopulation
		{
			get
			{
				return this.waitTime;
			}
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x06002705 RID: 9989 RVA: 0x0009C604 File Offset: 0x0009A804
		public bool WaitForPopulation
		{
			get
			{
				return !this.WaitTimeForPopulation.Equals(TimeSpan.Zero);
			}
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x0009C627 File Offset: 0x0009A827
		public CalendarVersionStoreQueryPolicy(TimeSpan waitTimeForPopulation)
		{
			this.waitTime = waitTimeForPopulation;
		}

		// Token: 0x0400172C RID: 5932
		private TimeSpan waitTime;

		// Token: 0x0400172D RID: 5933
		public static readonly TimeSpan DefaultWaitTimeForPopulation = TimeSpan.FromMinutes(2.0);
	}
}
