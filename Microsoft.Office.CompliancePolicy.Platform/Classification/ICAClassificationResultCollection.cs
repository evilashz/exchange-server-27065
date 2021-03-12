using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x02000019 RID: 25
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("D620B789-A29B-4996-9B8E-2A23981E6F9A")]
	[TypeLibType(TypeLibTypeFlags.FNonExtensible)]
	[ComImport]
	public interface ICAClassificationResultCollection
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006B RID: 107
		int Count { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700001C RID: 28
		ICAClassificationResult this[int nIndex]
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006D RID: 109
		object _NewEnum { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IUnknown)] get; }
	}
}
