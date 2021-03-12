using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000284 RID: 644
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("00020301-0000-0000-C000-000000000046")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[ComImport]
	internal interface IMAPITable
	{
		// Token: 0x06000B87 RID: 2951
		[PreserveSig]
		unsafe int GetLastError(int hResult, int ulFlags, out MAPIERROR* lpMapiError);

		// Token: 0x06000B88 RID: 2952
		[PreserveSig]
		int Advise(AdviseFlags ulEventMask, IMAPIAdviseSink lpAdviseSink, out IntPtr piConnection);

		// Token: 0x06000B89 RID: 2953
		[PreserveSig]
		int Unadvise(IntPtr iConnection);

		// Token: 0x06000B8A RID: 2954
		[PreserveSig]
		int GetStatus(out int ulStatus, out int ulTableType);

		// Token: 0x06000B8B RID: 2955
		[PreserveSig]
		int SetColumns([MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, int ulFlags);

		// Token: 0x06000B8C RID: 2956
		[PreserveSig]
		int QueryColumns(int ulFlags, [PointerType("PropTag*")] out SafeExLinkedMemoryHandle propList);

		// Token: 0x06000B8D RID: 2957
		[PreserveSig]
		int GetRowCount(int flags, out int count);

		// Token: 0x06000B8E RID: 2958
		[PreserveSig]
		int SeekRow(uint bookmark, int crowSeek, ref int lpcrowSought);

		// Token: 0x06000B8F RID: 2959
		[PreserveSig]
		int Slot0b();

		// Token: 0x06000B90 RID: 2960
		[PreserveSig]
		int QueryPosition(ref int ulRows, ref int ulNumerator, ref int ulDenominator);

		// Token: 0x06000B91 RID: 2961
		[PreserveSig]
		unsafe int FindRow([In] SRestriction* lpRes, uint bookmark, int ulFlags);

		// Token: 0x06000B92 RID: 2962
		[PreserveSig]
		unsafe int Restrict([In] SRestriction* lpRes, int ulFlags);

		// Token: 0x06000B93 RID: 2963
		[PreserveSig]
		int CreateBookmark(out uint bookmark);

		// Token: 0x06000B94 RID: 2964
		[PreserveSig]
		int FreeBookmark(uint bookmark);

		// Token: 0x06000B95 RID: 2965
		[PreserveSig]
		unsafe int SortTable([In] SSortOrderSet* lpSort, int ulFlags);

		// Token: 0x06000B96 RID: 2966
		[PreserveSig]
		int Slot12();

		// Token: 0x06000B97 RID: 2967
		[PreserveSig]
		int QueryRows(int crows, int ulFlags, [PointerType("SRowSet*")] out SafeExProwsHandle lpSRowset);

		// Token: 0x06000B98 RID: 2968
		[PreserveSig]
		int Slot14();

		// Token: 0x06000B99 RID: 2969
		[PreserveSig]
		int ExpandRow(long categoryId, int ulRowCount, int ulFlags, [PointerType("SRowSet*")] out SafeExProwsHandle lpSRowset, out int ulMoreRows);

		// Token: 0x06000B9A RID: 2970
		[PreserveSig]
		int CollapseRow(long categoryId, int ulFlags, out int ulRowCount);

		// Token: 0x06000B9B RID: 2971
		[PreserveSig]
		int Slot17();

		// Token: 0x06000B9C RID: 2972
		[PreserveSig]
		int GetCollapseState(int cbInstanceKey, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbInstanceKey, out int cbCollapseState, out SafeExLinkedMemoryHandle pbCollapseState);

		// Token: 0x06000B9D RID: 2973
		[PreserveSig]
		int SetCollapseState(int cbCollapseState, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbCollapseState, out uint bookmark);
	}
}
