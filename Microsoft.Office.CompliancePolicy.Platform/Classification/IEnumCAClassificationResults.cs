using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x02000018 RID: 24
	[Guid("52048868-E68A-4036-B2FC-46719EB5B8D1")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[TypeLibType(TypeLibTypeFlags.FNonExtensible)]
	[ComImport]
	public interface IEnumCAClassificationResults
	{
		// Token: 0x06000067 RID: 103
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int Next([In] uint cElementsWanted, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Interface, SizeParamIndex = 2)] ICAClassificationResult[] classificationResults, out uint elementsFetched);

		// Token: 0x06000068 RID: 104
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int Skip([In] uint cElementsToSkip);

		// Token: 0x06000069 RID: 105
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Reset();

		// Token: 0x0600006A RID: 106
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IEnumCAClassificationResults Clone();
	}
}
