using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x0200001D RID: 29
	[TypeLibType(TypeLibTypeFlags.FNonExtensible)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("615A546C-8D4C-4053-A888-7B397AC3EF6E")]
	[ComImport]
	public interface ICAClassificationSession
	{
		// Token: 0x0600007A RID: 122
		[MethodImpl(MethodImplOptions.InternalCall)]
		void ClassifyUpdates([MarshalAs(UnmanagedType.BStr)] [In] string bstrText, [In] uint ulRelativeOffset, [In] int lTextShift, [In] uint ulModifiedTextStart, [In] uint ulModifiedTextEnd);

		// Token: 0x0600007B RID: 123
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		ICAClassificationResultCollection GetClassificationResults();
	}
}
