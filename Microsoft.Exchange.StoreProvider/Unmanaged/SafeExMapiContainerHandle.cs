using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002C3 RID: 707
	[ComVisible(false)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SafeExMapiContainerHandle : SafeExMapiPropHandle, IExMapiContainer, IExMapiProp, IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000D69 RID: 3433 RVA: 0x000352AE File Offset: 0x000334AE
		protected SafeExMapiContainerHandle()
		{
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x000352B6 File Offset: 0x000334B6
		internal SafeExMapiContainerHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x000352BF File Offset: 0x000334BF
		internal SafeExMapiContainerHandle(SafeExInterfaceHandle innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x000352C8 File Offset: 0x000334C8
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExMapiContainerHandle>(this);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x000352D0 File Offset: 0x000334D0
		public int GetContentsTable(int ulFlags, out IExMapiTable iMAPITable)
		{
			SafeExMapiTableHandle safeExMapiTableHandle = null;
			int result = SafeExMapiContainerHandle.IMAPIContainer_GetContentsTable(this.handle, ulFlags, out safeExMapiTableHandle);
			iMAPITable = safeExMapiTableHandle;
			return result;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x000352F4 File Offset: 0x000334F4
		public int GetHierarchyTable(int ulFlags, out IExMapiTable iMAPITable)
		{
			SafeExMapiTableHandle safeExMapiTableHandle = null;
			int result = SafeExMapiContainerHandle.IMAPIContainer_GetHierarchyTable(this.handle, ulFlags, out safeExMapiTableHandle);
			iMAPITable = safeExMapiTableHandle;
			return result;
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00035318 File Offset: 0x00033518
		public int OpenEntry(byte[] lpEntryID, Guid lpInterface, int ulFlags, out int lpulObjType, out IExInterface iObj)
		{
			SafeExInterfaceHandle safeExInterfaceHandle = null;
			int result = SafeExMapiContainerHandle.IMAPIContainer_OpenEntry(this.handle, (lpEntryID != null) ? lpEntryID.Length : 0, lpEntryID, lpInterface, ulFlags, out lpulObjType, out safeExInterfaceHandle);
			iObj = safeExInterfaceHandle;
			return result;
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00035348 File Offset: 0x00033548
		public int SetSearchCriteria(Restriction lpRestriction, byte[][] lpContainerList, int ulSearchFlags)
		{
			return this.InternalSetSearchCriteria(lpRestriction, lpContainerList, ulSearchFlags);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00035354 File Offset: 0x00033554
		private unsafe int InternalSetSearchCriteria(Restriction lpRestriction, byte[][] lpContainerList, int ulSearchFlags)
		{
			bool flag = lpRestriction == null;
			bool flag2 = lpContainerList == null;
			int num = 1;
			int num2 = 1;
			if (!flag)
			{
				num = lpRestriction.GetBytesToMarshal();
			}
			SBinary[] array = null;
			if (lpContainerList != null)
			{
				array = new SBinary[lpContainerList.GetLength(0)];
				for (int i = 0; i < lpContainerList.GetLength(0); i++)
				{
					array[i] = new SBinary(lpContainerList[i]);
				}
				num2 = SBinaryArray.GetBytesToMarshal(array);
			}
			fixed (byte* ptr = new byte[num])
			{
				fixed (byte* ptr2 = new byte[num2])
				{
					if (!flag)
					{
						byte* ptr3 = ptr + (SRestriction.SizeOf + 7 & -8);
						lpRestriction.MarshalToNative((SRestriction*)ptr, ref ptr3);
					}
					if (!flag2)
					{
						SBinaryArray.MarshalToNative(ptr2, array);
					}
					return SafeExMapiContainerHandle.IMAPIContainer_SetSearchCriteria(this.handle, flag ? null : ((SRestriction*)ptr), flag2 ? null : ((_SBinaryArray*)ptr2), ulSearchFlags);
				}
			}
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0003544C File Offset: 0x0003364C
		public int GetSearchCriteria(int ulFlags, out Restriction lpRestriction, out byte[][] lpContainerList, out int ulSearchState)
		{
			SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
			SafeExLinkedMemoryHandle safeExLinkedMemoryHandle2 = null;
			lpRestriction = null;
			lpContainerList = null;
			int result;
			try
			{
				int num = SafeExMapiContainerHandle.IMAPIContainer_GetSearchCriteria(this.handle, ulFlags, out safeExLinkedMemoryHandle, out safeExLinkedMemoryHandle2, out ulSearchState);
				if (num == 0)
				{
					lpContainerList = _SBinaryArray.Unmarshal(safeExLinkedMemoryHandle2);
					lpRestriction = Restriction.Unmarshal(safeExLinkedMemoryHandle);
				}
				result = num;
			}
			finally
			{
				if (safeExLinkedMemoryHandle != null)
				{
					safeExLinkedMemoryHandle.Dispose();
				}
				if (safeExLinkedMemoryHandle2 != null)
				{
					safeExLinkedMemoryHandle2.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000D73 RID: 3443
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPIContainer_GetContentsTable(IntPtr iMAPIContainer, int ulFlags, out SafeExMapiTableHandle iMAPITable);

		// Token: 0x06000D74 RID: 3444
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPIContainer_GetHierarchyTable(IntPtr iMAPIContainer, int ulFlags, out SafeExMapiTableHandle iMAPITable);

		// Token: 0x06000D75 RID: 3445
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPIContainer_OpenEntry(IntPtr iMAPIContainer, int cbEntryID, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryID, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid lpInterface, int ulFlags, out int lpulObjType, out SafeExInterfaceHandle iObj);

		// Token: 0x06000D76 RID: 3446
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IMAPIContainer_SetSearchCriteria(IntPtr iMAPIContainer, [In] SRestriction* lpRestriction, _SBinaryArray* lpContainerList, int ulSearchFlags);

		// Token: 0x06000D77 RID: 3447
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IMAPIContainer_GetSearchCriteria(IntPtr iMAPIContainer, int ulFlags, out SafeExLinkedMemoryHandle lpRestriction, out SafeExLinkedMemoryHandle lpContainerList, out int ulSearchState);
	}
}
