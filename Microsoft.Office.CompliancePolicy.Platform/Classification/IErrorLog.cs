using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x0200001F RID: 31
	[Guid("3127CA40-446E-11CE-8135-00AA004BB851")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IErrorLog
	{
		// Token: 0x0600007D RID: 125
		[MethodImpl(MethodImplOptions.InternalCall)]
		void AddError([MarshalAs(UnmanagedType.LPWStr)] [In] string propertyName, [In] ref System.Runtime.InteropServices.ComTypes.EXCEPINFO exceptionInfo);
	}
}
