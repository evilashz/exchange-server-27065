using System;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200010D RID: 269
	internal struct JobItemOperationResult
	{
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x00039F7F File Offset: 0x0003817F
		// (set) Token: 0x06000E07 RID: 3591 RVA: 0x00039F87 File Offset: 0x00038187
		public int NumItemsProcessed { get; set; }

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x00039F90 File Offset: 0x00038190
		// (set) Token: 0x06000E09 RID: 3593 RVA: 0x00039F98 File Offset: 0x00038198
		public int NumItemsSuccessful { get; set; }

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x00039FA1 File Offset: 0x000381A1
		// (set) Token: 0x06000E0B RID: 3595 RVA: 0x00039FA9 File Offset: 0x000381A9
		public int NumItemsTransitioned { get; set; }

		// Token: 0x06000E0C RID: 3596 RVA: 0x00039FB4 File Offset: 0x000381B4
		public static JobItemOperationResult operator +(JobItemOperationResult value1, JobItemOperationResult value2)
		{
			return new JobItemOperationResult
			{
				NumItemsProcessed = value1.NumItemsProcessed + value2.NumItemsProcessed,
				NumItemsSuccessful = value1.NumItemsSuccessful + value2.NumItemsSuccessful,
				NumItemsTransitioned = value1.NumItemsTransitioned + value2.NumItemsTransitioned
			};
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0003A00C File Offset: 0x0003820C
		public static JobItemOperationResult operator -(JobItemOperationResult value1, JobItemOperationResult value2)
		{
			return new JobItemOperationResult
			{
				NumItemsProcessed = value1.NumItemsProcessed - value2.NumItemsProcessed,
				NumItemsSuccessful = value1.NumItemsSuccessful - value2.NumItemsSuccessful,
				NumItemsTransitioned = value1.NumItemsTransitioned - value2.NumItemsTransitioned
			};
		}
	}
}
