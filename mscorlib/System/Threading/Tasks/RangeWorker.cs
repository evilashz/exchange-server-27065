using System;
using System.Runtime.InteropServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000526 RID: 1318
	[StructLayout(LayoutKind.Auto)]
	internal struct RangeWorker
	{
		// Token: 0x06003F20 RID: 16160 RVA: 0x000EB39C File Offset: 0x000E959C
		internal RangeWorker(IndexRange[] ranges, int nInitialRange, long nStep, bool use32BitCurrentIndex)
		{
			this.m_indexRanges = ranges;
			this.m_nCurrentIndexRange = nInitialRange;
			this._use32BitCurrentIndex = use32BitCurrentIndex;
			this.m_nStep = nStep;
			this.m_nIncrementValue = nStep;
			this.m_nMaxIncrementValue = 16L * nStep;
		}

		// Token: 0x06003F21 RID: 16161 RVA: 0x000EB3D0 File Offset: 0x000E95D0
		internal unsafe bool FindNewWork(out long nFromInclusiveLocal, out long nToExclusiveLocal)
		{
			int num = this.m_indexRanges.Length;
			IndexRange indexRange;
			long num2;
			for (;;)
			{
				indexRange = this.m_indexRanges[this.m_nCurrentIndexRange];
				if (indexRange.m_bRangeFinished == 0)
				{
					if (this.m_indexRanges[this.m_nCurrentIndexRange].m_nSharedCurrentIndexOffset == null)
					{
						Interlocked.CompareExchange<Shared<long>>(ref this.m_indexRanges[this.m_nCurrentIndexRange].m_nSharedCurrentIndexOffset, new Shared<long>(0L), null);
					}
					if (IntPtr.Size == 4 && this._use32BitCurrentIndex)
					{
						fixed (long* ptr = &this.m_indexRanges[this.m_nCurrentIndexRange].m_nSharedCurrentIndexOffset.Value)
						{
							num2 = (long)Interlocked.Add(ref *(int*)ptr, (int)this.m_nIncrementValue) - this.m_nIncrementValue;
						}
					}
					else
					{
						num2 = Interlocked.Add(ref this.m_indexRanges[this.m_nCurrentIndexRange].m_nSharedCurrentIndexOffset.Value, this.m_nIncrementValue) - this.m_nIncrementValue;
					}
					if (indexRange.m_nToExclusive - indexRange.m_nFromInclusive > num2)
					{
						break;
					}
					Interlocked.Exchange(ref this.m_indexRanges[this.m_nCurrentIndexRange].m_bRangeFinished, 1);
				}
				this.m_nCurrentIndexRange = (this.m_nCurrentIndexRange + 1) % this.m_indexRanges.Length;
				num--;
				if (num <= 0)
				{
					goto Block_9;
				}
			}
			nFromInclusiveLocal = indexRange.m_nFromInclusive + num2;
			nToExclusiveLocal = nFromInclusiveLocal + this.m_nIncrementValue;
			if (nToExclusiveLocal > indexRange.m_nToExclusive || nToExclusiveLocal < indexRange.m_nFromInclusive)
			{
				nToExclusiveLocal = indexRange.m_nToExclusive;
			}
			if (this.m_nIncrementValue < this.m_nMaxIncrementValue)
			{
				this.m_nIncrementValue *= 2L;
				if (this.m_nIncrementValue > this.m_nMaxIncrementValue)
				{
					this.m_nIncrementValue = this.m_nMaxIncrementValue;
				}
			}
			return true;
			Block_9:
			nFromInclusiveLocal = 0L;
			nToExclusiveLocal = 0L;
			return false;
		}

		// Token: 0x06003F22 RID: 16162 RVA: 0x000EB580 File Offset: 0x000E9780
		internal bool FindNewWork32(out int nFromInclusiveLocal32, out int nToExclusiveLocal32)
		{
			long num;
			long num2;
			bool result = this.FindNewWork(out num, out num2);
			nFromInclusiveLocal32 = (int)num;
			nToExclusiveLocal32 = (int)num2;
			return result;
		}

		// Token: 0x04001A19 RID: 6681
		internal readonly IndexRange[] m_indexRanges;

		// Token: 0x04001A1A RID: 6682
		internal int m_nCurrentIndexRange;

		// Token: 0x04001A1B RID: 6683
		internal long m_nStep;

		// Token: 0x04001A1C RID: 6684
		internal long m_nIncrementValue;

		// Token: 0x04001A1D RID: 6685
		internal readonly long m_nMaxIncrementValue;

		// Token: 0x04001A1E RID: 6686
		internal readonly bool _use32BitCurrentIndex;
	}
}
