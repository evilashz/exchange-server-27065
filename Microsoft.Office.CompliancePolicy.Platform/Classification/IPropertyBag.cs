using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x02000003 RID: 3
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("55272A00-42CB-11CE-8135-00AA004BB851")]
	[ComImport]
	public interface IPropertyBag
	{
		// Token: 0x06000017 RID: 23
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int Read([MarshalAs(UnmanagedType.LPWStr)] [In] string propertyName, [MarshalAs(UnmanagedType.Struct)] [In] [Out] ref object propertyValue, [MarshalAs(UnmanagedType.Interface)] [In] IErrorLog errorLog);

		// Token: 0x06000018 RID: 24
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int Write([MarshalAs(UnmanagedType.LPWStr)] [In] string propertyName, [MarshalAs(UnmanagedType.Struct)] [In] ref object propertyValue);
	}
}
