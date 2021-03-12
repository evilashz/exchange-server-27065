using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000527 RID: 1319
	internal class RangeManager
	{
		// Token: 0x06003F23 RID: 16163 RVA: 0x000EB5A4 File Offset: 0x000E97A4
		internal RangeManager(long nFromInclusive, long nToExclusive, long nStep, int nNumExpectedWorkers)
		{
			this.m_nCurrentIndexRangeToAssign = 0;
			this.m_nStep = nStep;
			if (nNumExpectedWorkers == 1)
			{
				nNumExpectedWorkers = 2;
			}
			ulong num = (ulong)(nToExclusive - nFromInclusive);
			ulong num2 = num / (ulong)((long)nNumExpectedWorkers);
			num2 -= num2 % (ulong)nStep;
			if (num2 == 0UL)
			{
				num2 = (ulong)nStep;
			}
			int num3 = (int)(num / num2);
			if (num % num2 != 0UL)
			{
				num3++;
			}
			long num4 = (long)num2;
			this._use32BitCurrentIndex = (IntPtr.Size == 4 && num4 <= 2147483647L);
			this.m_indexRanges = new IndexRange[num3];
			long num5 = nFromInclusive;
			for (int i = 0; i < num3; i++)
			{
				this.m_indexRanges[i].m_nFromInclusive = num5;
				this.m_indexRanges[i].m_nSharedCurrentIndexOffset = null;
				this.m_indexRanges[i].m_bRangeFinished = 0;
				num5 += num4;
				if (num5 < num5 - num4 || num5 > nToExclusive)
				{
					num5 = nToExclusive;
				}
				this.m_indexRanges[i].m_nToExclusive = num5;
			}
		}

		// Token: 0x06003F24 RID: 16164 RVA: 0x000EB694 File Offset: 0x000E9894
		internal RangeWorker RegisterNewWorker()
		{
			int nInitialRange = (Interlocked.Increment(ref this.m_nCurrentIndexRangeToAssign) - 1) % this.m_indexRanges.Length;
			return new RangeWorker(this.m_indexRanges, nInitialRange, this.m_nStep, this._use32BitCurrentIndex);
		}

		// Token: 0x04001A1F RID: 6687
		internal readonly IndexRange[] m_indexRanges;

		// Token: 0x04001A20 RID: 6688
		internal readonly bool _use32BitCurrentIndex;

		// Token: 0x04001A21 RID: 6689
		internal int m_nCurrentIndexRangeToAssign;

		// Token: 0x04001A22 RID: 6690
		internal long m_nStep;
	}
}
