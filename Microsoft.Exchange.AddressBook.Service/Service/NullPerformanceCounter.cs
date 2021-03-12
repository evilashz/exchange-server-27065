using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x0200003F RID: 63
	internal class NullPerformanceCounter : IExPerformanceCounter
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002BB RID: 699 RVA: 0x000117EB File Offset: 0x0000F9EB
		// (set) Token: 0x060002BC RID: 700 RVA: 0x000117EF File Offset: 0x0000F9EF
		public long RawValue
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x000117F1 File Offset: 0x0000F9F1
		public long Decrement()
		{
			return 0L;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x000117F5 File Offset: 0x0000F9F5
		public long Increment()
		{
			return 0L;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x000117F9 File Offset: 0x0000F9F9
		public long IncrementBy(long incrementValue)
		{
			return 0L;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x000117FD File Offset: 0x0000F9FD
		public void Reset()
		{
		}

		// Token: 0x04000186 RID: 390
		public static readonly NullPerformanceCounter Instance = new NullPerformanceCounter();
	}
}
