using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000294 RID: 660
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IExMapiTable : IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000C1C RID: 3100
		int Advise(AdviseFlags ulEventMask, IMAPIAdviseSink lpAdviseSink, out IntPtr piConnection);

		// Token: 0x06000C1D RID: 3101
		int AdviseEx(AdviseFlags ulEventMask, IntPtr iOnNotifyDelegate, ulong callbackId, out IntPtr piConnection);

		// Token: 0x06000C1E RID: 3102
		int Unadvise(IntPtr iConnection);

		// Token: 0x06000C1F RID: 3103
		int GetStatus(out int ulStatus, out int ulTableType);

		// Token: 0x06000C20 RID: 3104
		int SetColumns(PropTag[] lpPropTagArray, int ulFlags);

		// Token: 0x06000C21 RID: 3105
		int QueryColumns(int ulFlags, out PropTag[] propList);

		// Token: 0x06000C22 RID: 3106
		int GetRowCount(int flags, out int count);

		// Token: 0x06000C23 RID: 3107
		int SeekRow(uint bookmark, int crowSeek, ref int lpcrowSought);

		// Token: 0x06000C24 RID: 3108
		int QueryPosition(ref int ulRows, ref int ulNumerator, ref int ulDenominator);

		// Token: 0x06000C25 RID: 3109
		int FindRow(Restriction lpRes, uint bookmark, int ulFlags);

		// Token: 0x06000C26 RID: 3110
		int Restrict(Restriction lpRes, int ulFlags);

		// Token: 0x06000C27 RID: 3111
		int SortTable(SortOrder lpSort, int ulFlags);

		// Token: 0x06000C28 RID: 3112
		int QueryRows(int crows, int ulFlags, out PropValue[][] lpSRowset);

		// Token: 0x06000C29 RID: 3113
		int ExpandRow(long categoryId, int ulRowCount, int ulFlags, out PropValue[][] lpSRowset, out int ulMoreRows);

		// Token: 0x06000C2A RID: 3114
		int CollapseRow(long categoryId, int ulFlags, out int ulRowCount);

		// Token: 0x06000C2B RID: 3115
		int CreateBookmark(out uint bookmark);

		// Token: 0x06000C2C RID: 3116
		int FreeBookmark(uint bookmark);

		// Token: 0x06000C2D RID: 3117
		int SeekRowBookmark(uint bookmark, int crowSeek, bool fWantRowsSought, out bool fSoughtLess, ref int crowSought, out bool fPositionChanged);

		// Token: 0x06000C2E RID: 3118
		int GetCollapseState(byte[] pbInstanceKey, out byte[] pbCollapseState);

		// Token: 0x06000C2F RID: 3119
		int SetCollapseState(byte[] pbCollapseState, out uint bookmark);
	}
}
