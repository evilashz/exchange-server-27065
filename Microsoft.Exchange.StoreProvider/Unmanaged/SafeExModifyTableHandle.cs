using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002C9 RID: 713
	[ComVisible(false)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SafeExModifyTableHandle : SafeExInterfaceHandle, IExModifyTable, IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000E7B RID: 3707 RVA: 0x00037008 File Offset: 0x00035208
		protected SafeExModifyTableHandle()
		{
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00037010 File Offset: 0x00035210
		internal SafeExModifyTableHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00037019 File Offset: 0x00035219
		internal SafeExModifyTableHandle(SafeExInterfaceHandle innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00037022 File Offset: 0x00035222
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExModifyTableHandle>(this);
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0003702C File Offset: 0x0003522C
		public int GetTable(int ulFlags, out IExMapiTable iMAPITable)
		{
			SafeExMapiTableHandle safeExMapiTableHandle = null;
			int result = SafeExModifyTableHandle.IExchangeModifyTable_GetTable(this.handle, ulFlags, out safeExMapiTableHandle);
			iMAPITable = safeExMapiTableHandle;
			return result;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0003704E File Offset: 0x0003524E
		public int ModifyTable(int ulFlags, ICollection<RowEntry> lpRowList)
		{
			return this.InternalModifyTable(ulFlags, lpRowList);
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00037058 File Offset: 0x00035258
		private unsafe int InternalModifyTable(int ulFlags, ICollection<RowEntry> lpRowList)
		{
			int num = (_RowList.SizeOf + 7 & -8) + (_RowEntry.SizeOf * lpRowList.Count + 7 & -8);
			foreach (RowEntry rowEntry in lpRowList)
			{
				num += (SPropValue.SizeOf * rowEntry.Values.Count + 7 & -8);
				foreach (PropValue propValue in rowEntry.Values)
				{
					num += propValue.GetBytesToMarshal();
				}
			}
			fixed (byte* ptr = new byte[num])
			{
				_RowList* ptr2 = (_RowList*)ptr;
				_RowEntry* ptr3 = &ptr2->aEntries;
				byte* ptr4 = ptr + (_RowList.SizeOf + 7 & -8) + (_RowEntry.SizeOf * lpRowList.Count + 7 & -8);
				ptr2->cEntries = lpRowList.Count;
				foreach (RowEntry rowEntry2 in lpRowList)
				{
					SPropValue* ptr5 = (SPropValue*)ptr4;
					ptr4 += (SPropValue.SizeOf * rowEntry2.Values.Count + 7 & -8);
					ptr3->ulRowFlags = (int)rowEntry2.RowFlags;
					ptr3->cValues = rowEntry2.Values.Count;
					ptr3->rgPropVals = ptr5;
					foreach (PropValue propValue2 in rowEntry2.Values)
					{
						propValue2.MarshalToNative(ptr5, ref ptr4);
						ptr5++;
					}
					ptr3++;
				}
				return SafeExModifyTableHandle.IExchangeModifyTable_ModifyTable(this.handle, ulFlags, (_RowList*)ptr);
			}
		}

		// Token: 0x06000E82 RID: 3714
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExchangeModifyTable_GetTable(IntPtr iExchangeModifyTable, int ulFlags, out SafeExMapiTableHandle iMAPITable);

		// Token: 0x06000E83 RID: 3715
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private unsafe static extern int IExchangeModifyTable_ModifyTable(IntPtr iExchangeModifyTable, int ulFlags, [In] _RowList* lpRowList);
	}
}
