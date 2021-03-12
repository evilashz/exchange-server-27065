using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002C8 RID: 712
	[ComVisible(false)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SafeExMapiTableHandle : SafeExInterfaceHandle, IExMapiTable, IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000E4C RID: 3660 RVA: 0x00036CD5 File Offset: 0x00034ED5
		protected SafeExMapiTableHandle()
		{
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00036CDD File Offset: 0x00034EDD
		internal SafeExMapiTableHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00036CE6 File Offset: 0x00034EE6
		internal SafeExMapiTableHandle(SafeExInterfaceHandle innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00036CEF File Offset: 0x00034EEF
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExMapiTableHandle>(this);
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00036CF7 File Offset: 0x00034EF7
		public int Advise(AdviseFlags ulEventMask, IMAPIAdviseSink lpAdviseSink, out IntPtr piConnection)
		{
			return SafeExMapiTableHandle.IMAPITable_Advise(this.handle, ulEventMask, lpAdviseSink, out piConnection);
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x00036D07 File Offset: 0x00034F07
		public int AdviseEx(AdviseFlags ulEventMask, IntPtr iOnNotifyDelegate, ulong callbackId, out IntPtr piConnection)
		{
			return SafeExMapiTableHandle.IExRpcTable_AdviseEx(this.handle, ulEventMask, iOnNotifyDelegate, callbackId, out piConnection);
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00036D19 File Offset: 0x00034F19
		public int Unadvise(IntPtr iConnection)
		{
			return SafeExMapiTableHandle.IMAPITable_Unadvise(this.handle, iConnection);
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x00036D27 File Offset: 0x00034F27
		public int GetStatus(out int ulStatus, out int ulTableType)
		{
			return SafeExMapiTableHandle.IMAPITable_GetStatus(this.handle, out ulStatus, out ulTableType);
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00036D36 File Offset: 0x00034F36
		public int SetColumns(PropTag[] lpPropTagArray, int ulFlags)
		{
			return SafeExMapiTableHandle.IMAPITable_SetColumns(this.handle, lpPropTagArray, ulFlags);
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00036D48 File Offset: 0x00034F48
		public int QueryColumns(int ulFlags, out PropTag[] propList)
		{
			propList = null;
			SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
			int result;
			try
			{
				int num = SafeExMapiTableHandle.IMAPITable_QueryColumns(this.handle, ulFlags, out safeExLinkedMemoryHandle);
				if (num == 0)
				{
					propList = safeExLinkedMemoryHandle.ReadPropTagArray();
				}
				result = num;
			}
			finally
			{
				if (safeExLinkedMemoryHandle != null)
				{
					safeExLinkedMemoryHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00036D94 File Offset: 0x00034F94
		public int GetRowCount(int flags, out int count)
		{
			return SafeExMapiTableHandle.IMAPITable_GetRowCount(this.handle, flags, out count);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00036DA3 File Offset: 0x00034FA3
		public int SeekRow(uint bookmark, int crowSeek, ref int lpcrowSought)
		{
			return SafeExMapiTableHandle.IMAPITable_SeekRow(this.handle, bookmark, crowSeek, ref lpcrowSought);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00036DB3 File Offset: 0x00034FB3
		public int QueryPosition(ref int ulRows, ref int ulNumerator, ref int ulDenominator)
		{
			return SafeExMapiTableHandle.IMAPITable_QueryPosition(this.handle, ref ulRows, ref ulNumerator, ref ulDenominator);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00036DC3 File Offset: 0x00034FC3
		public int FindRow(Restriction lpRes, uint bookmark, int ulFlags)
		{
			return this.InternalFindRow(lpRes, bookmark, ulFlags);
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00036DD0 File Offset: 0x00034FD0
		private unsafe int InternalFindRow(Restriction lpRes, uint bookmark, int ulFlags)
		{
			SRestriction* ptr = null;
			int bytesToMarshal = lpRes.GetBytesToMarshal();
			byte* ptr2 = stackalloc byte[(UIntPtr)bytesToMarshal];
			ptr = (SRestriction*)ptr2;
			ptr2 += (SRestriction.SizeOf + 7 & -8);
			lpRes.MarshalToNative(ptr, ref ptr2);
			return SafeExMapiTableHandle.IMAPITable_FindRow(this.handle, ptr, bookmark, ulFlags);
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00036E12 File Offset: 0x00035012
		public int Restrict(Restriction lpRes, int ulFlags)
		{
			return this.InternalRestrict(lpRes, ulFlags);
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00036E1C File Offset: 0x0003501C
		private unsafe int InternalRestrict(Restriction lpRes, int ulFlags)
		{
			int bytesToMarshal = lpRes.GetBytesToMarshal();
			byte* ptr = stackalloc byte[(UIntPtr)bytesToMarshal];
			SRestriction* ptr2 = (SRestriction*)ptr;
			ptr += (SRestriction.SizeOf + 7 & -8);
			lpRes.MarshalToNative(ptr2, ref ptr);
			return SafeExMapiTableHandle.IMAPITable_Restrict(this.handle, ptr2, ulFlags);
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00036E5A File Offset: 0x0003505A
		public int SortTable(SortOrder lpSort, int ulFlags)
		{
			return this.InternalSortTable(lpSort, ulFlags);
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x00036E64 File Offset: 0x00035064
		private unsafe int InternalSortTable(SortOrder lpSort, int ulFlags)
		{
			int bytesToMarshal = lpSort.GetBytesToMarshal();
			byte* ptr = stackalloc byte[(UIntPtr)bytesToMarshal];
			SSortOrderSet* ptr2 = (SSortOrderSet*)ptr;
			lpSort.MarshalToNative(ptr2);
			return SafeExMapiTableHandle.IMAPITable_SortTable(this.handle, ptr2, ulFlags);
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x00036E94 File Offset: 0x00035094
		public int QueryRows(int crows, int ulFlags, out PropValue[][] lpSRowset)
		{
			lpSRowset = null;
			SafeExProwsHandle safeExProwsHandle = null;
			int result;
			try
			{
				int num = SafeExMapiTableHandle.IMAPITable_QueryRows(this.handle, crows, ulFlags, out safeExProwsHandle);
				if (num == 0)
				{
					lpSRowset = SRowSet.Unmarshal(safeExProwsHandle);
				}
				result = num;
			}
			finally
			{
				if (safeExProwsHandle != null)
				{
					safeExProwsHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00036EE0 File Offset: 0x000350E0
		public int ExpandRow(long categoryId, int ulRowCount, int ulFlags, out PropValue[][] lpSRowset, out int ulMoreRows)
		{
			lpSRowset = null;
			SafeExProwsHandle safeExProwsHandle = null;
			int result;
			try
			{
				int num = SafeExMapiTableHandle.IMAPITable_ExpandRow(this.handle, categoryId, ulRowCount, ulFlags, out safeExProwsHandle, out ulMoreRows);
				if (num == 0)
				{
					lpSRowset = SRowSet.Unmarshal(safeExProwsHandle);
				}
				result = num;
			}
			finally
			{
				if (safeExProwsHandle != null)
				{
					safeExProwsHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00036F34 File Offset: 0x00035134
		public int CollapseRow(long categoryId, int ulFlags, out int ulRowCount)
		{
			return SafeExMapiTableHandle.IMAPITable_CollapseRow(this.handle, categoryId, ulFlags, out ulRowCount);
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00036F44 File Offset: 0x00035144
		public int CreateBookmark(out uint bookmark)
		{
			return SafeExMapiTableHandle.IMAPITable_CreateBookmark(this.handle, out bookmark);
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00036F52 File Offset: 0x00035152
		public int FreeBookmark(uint bookmark)
		{
			return SafeExMapiTableHandle.IMAPITable_FreeBookmark(this.handle, bookmark);
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x00036F60 File Offset: 0x00035160
		public int SeekRowBookmark(uint bookmark, int crowSeek, bool fWantRowsSought, out bool fSoughtLess, ref int crowSought, out bool fPositionChanged)
		{
			return SafeExMapiTableHandle.IExRpcTable_SeekRowBookmark(this.handle, bookmark, crowSeek, fWantRowsSought, out fSoughtLess, ref crowSought, out fPositionChanged);
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x00036F78 File Offset: 0x00035178
		public int GetCollapseState(byte[] pbInstanceKey, out byte[] pbCollapseState)
		{
			pbCollapseState = null;
			SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
			int result;
			try
			{
				int num2;
				int num = SafeExMapiTableHandle.IMAPITable_GetCollapseState(this.handle, (pbInstanceKey != null) ? pbInstanceKey.Length : 0, pbInstanceKey, out num2, out safeExLinkedMemoryHandle);
				if (num == 0)
				{
					byte[] array = Array<byte>.Empty;
					if (num2 > 0 && safeExLinkedMemoryHandle != null)
					{
						array = new byte[num2];
						Marshal.Copy(safeExLinkedMemoryHandle.DangerousGetHandle(), array, 0, num2);
					}
					pbCollapseState = array;
				}
				result = num;
			}
			finally
			{
				if (safeExLinkedMemoryHandle != null)
				{
					safeExLinkedMemoryHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00036FF0 File Offset: 0x000351F0
		public int SetCollapseState(byte[] pbCollapseState, out uint bookmark)
		{
			return SafeExMapiTableHandle.IMAPITable_SetCollapseState(this.handle, (pbCollapseState != null) ? pbCollapseState.Length : 0, pbCollapseState, out bookmark);
		}

		// Token: 0x06000E67 RID: 3687
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcTable_SeekRowBookmark(IntPtr iMAPITable, uint bookmark, int crowSeek, bool fWantRowsSought, out bool fSoughtLess, ref int crowSought, out bool fPositionChanged);

		// Token: 0x06000E68 RID: 3688
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcTable_AdviseEx(IntPtr iMAPITable, AdviseFlags ulEventMask, IntPtr iOnNotifyDelegate, ulong callbackId, out IntPtr piConnection);

		// Token: 0x06000E69 RID: 3689
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPITable_Advise(IntPtr iMAPITable, AdviseFlags ulEventMask, IMAPIAdviseSink lpAdviseSink, out IntPtr piConnection);

		// Token: 0x06000E6A RID: 3690
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPITable_Unadvise(IntPtr iMAPITable, IntPtr iConnection);

		// Token: 0x06000E6B RID: 3691
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPITable_GetStatus(IntPtr iMAPITable, out int ulStatus, out int ulTableType);

		// Token: 0x06000E6C RID: 3692
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPITable_SetColumns(IntPtr iMAPITable, [MarshalAs(UnmanagedType.LPArray)] [In] PropTag[] lpPropTagArray, int ulFlags);

		// Token: 0x06000E6D RID: 3693
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPITable_QueryColumns(IntPtr iMAPITable, int ulFlags, out SafeExLinkedMemoryHandle propList);

		// Token: 0x06000E6E RID: 3694
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPITable_GetRowCount(IntPtr iMAPITable, int flags, out int count);

		// Token: 0x06000E6F RID: 3695
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPITable_SeekRow(IntPtr iMAPITable, uint bookmark, int crowSeek, ref int lpcrowSought);

		// Token: 0x06000E70 RID: 3696
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPITable_QueryPosition(IntPtr iMAPITable, ref int ulRows, ref int ulNumerator, ref int ulDenominator);

		// Token: 0x06000E71 RID: 3697
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IMAPITable_FindRow(IntPtr iMAPITable, [In] SRestriction* lpRes, uint bookmark, int ulFlags);

		// Token: 0x06000E72 RID: 3698
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IMAPITable_Restrict(IntPtr iMAPITable, [In] SRestriction* lpRes, int ulFlags);

		// Token: 0x06000E73 RID: 3699
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IMAPITable_SortTable(IntPtr iMAPITable, [In] SSortOrderSet* lpSort, int ulFlags);

		// Token: 0x06000E74 RID: 3700
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPITable_QueryRows(IntPtr iMAPITable, int crows, int ulFlags, out SafeExProwsHandle lpSRowset);

		// Token: 0x06000E75 RID: 3701
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPITable_ExpandRow(IntPtr iMAPITable, long categoryId, int ulRowCount, int ulFlags, out SafeExProwsHandle lpSRowset, out int ulMoreRows);

		// Token: 0x06000E76 RID: 3702
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPITable_CollapseRow(IntPtr iMAPITable, long categoryId, int ulFlags, out int ulRowCount);

		// Token: 0x06000E77 RID: 3703
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPITable_CreateBookmark(IntPtr iMAPITable, out uint bookmark);

		// Token: 0x06000E78 RID: 3704
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPITable_FreeBookmark(IntPtr iMAPITable, uint bookmark);

		// Token: 0x06000E79 RID: 3705
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPITable_GetCollapseState(IntPtr iMAPITable, int cbInstanceKey, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbInstanceKey, out int cbCollapseState, out SafeExLinkedMemoryHandle pbCollapseState);

		// Token: 0x06000E7A RID: 3706
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPITable_SetCollapseState(IntPtr iMAPITable, int cbCollapseState, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbCollapseState, out uint bookmark);
	}
}
