using System;

namespace Microsoft.Exchange.Management.Analysis
{
	// Token: 0x02000042 RID: 66
	internal class ProgressUpdateEventArgs : EventArgs
	{
		// Token: 0x060001BB RID: 443 RVA: 0x00007580 File Offset: 0x00005780
		public ProgressUpdateEventArgs(int completedRules, int totalRules)
		{
			this.CompletedRules = completedRules;
			this.TotalRules = totalRules;
			this.CompletedPercentage = 100;
			if (totalRules != 0)
			{
				this.CompletedPercentage = (int)((float)completedRules / (float)totalRules * 100f);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001BC RID: 444 RVA: 0x000075B3 File Offset: 0x000057B3
		// (set) Token: 0x060001BD RID: 445 RVA: 0x000075BB File Offset: 0x000057BB
		public int CompletedRules { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001BE RID: 446 RVA: 0x000075C4 File Offset: 0x000057C4
		// (set) Token: 0x060001BF RID: 447 RVA: 0x000075CC File Offset: 0x000057CC
		public int TotalRules { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x000075D5 File Offset: 0x000057D5
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x000075DD File Offset: 0x000057DD
		public int CompletedPercentage { get; private set; }
	}
}
