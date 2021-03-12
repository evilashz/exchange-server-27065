using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Rpc;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Nspi
{
	// Token: 0x020001CD RID: 461
	internal static class NspiMarshal
	{
		// Token: 0x060012CF RID: 4815 RVA: 0x0005A450 File Offset: 0x00058650
		internal static void GuidToNative(Guid guid, IntPtr ptr)
		{
			if (ptr != IntPtr.Zero)
			{
				Marshal.StructureToPtr(guid, ptr, false);
			}
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0005A46C File Offset: 0x0005866C
		internal static SafeRpcMemoryHandle MarshalIntList(IList<int> list)
		{
			if (list == null)
			{
				return null;
			}
			int size = (list.Count + 1) * 4;
			SafeRpcMemoryHandle safeRpcMemoryHandle = new SafeRpcMemoryHandle();
			safeRpcMemoryHandle.Allocate(size);
			int num = 0;
			Marshal.WriteInt32(safeRpcMemoryHandle.DangerousGetHandle(), num, list.Count);
			foreach (int val in list)
			{
				num += 4;
				Marshal.WriteInt32(safeRpcMemoryHandle.DangerousGetHandle(), num, val);
			}
			return safeRpcMemoryHandle;
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x0005A4F8 File Offset: 0x000586F8
		internal static SafeRpcMemoryHandle MarshalRowSet(PropRowSet rowset)
		{
			if (rowset == null)
			{
				return null;
			}
			SafeRpcMemoryHandle safeRpcMemoryHandle = new SafeRpcMemoryHandle();
			foreach (PropRow propRow in rowset.Rows)
			{
				SafeRpcMemoryHandle safeRpcMemoryHandle2 = NspiMarshal.MarshalPropValueCollection(propRow.Properties);
				propRow.MarshalledPropertiesHandle = safeRpcMemoryHandle2;
				safeRpcMemoryHandle.AddAssociatedHandle(safeRpcMemoryHandle2);
			}
			safeRpcMemoryHandle.Allocate(rowset.GetBytesToMarshal());
			rowset.MarshalToNative(safeRpcMemoryHandle);
			return safeRpcMemoryHandle;
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0005A578 File Offset: 0x00058778
		internal static SafeRpcMemoryHandle MarshalRow(PropRow row)
		{
			if (row == null)
			{
				return null;
			}
			SafeRpcMemoryHandle safeRpcMemoryHandle = new SafeRpcMemoryHandle();
			SafeRpcMemoryHandle safeRpcMemoryHandle2 = NspiMarshal.MarshalPropValueCollection(row.Properties);
			row.MarshalledPropertiesHandle = safeRpcMemoryHandle2;
			safeRpcMemoryHandle.AddAssociatedHandle(safeRpcMemoryHandle2);
			safeRpcMemoryHandle.Allocate(row.GetBytesToMarshal());
			row.MarshalToNative(safeRpcMemoryHandle);
			return safeRpcMemoryHandle;
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x0005A5C0 File Offset: 0x000587C0
		internal static SafeRpcMemoryHandle MarshalPropValueCollection(ICollection<PropValue> properties)
		{
			SafeRpcMemoryHandle safeRpcMemoryHandle = new SafeRpcMemoryHandle();
			safeRpcMemoryHandle.Allocate(PropValue.GetBytesToMarshal(properties));
			PropValue.MarshalToNative(properties, safeRpcMemoryHandle);
			return safeRpcMemoryHandle;
		}
	}
}
