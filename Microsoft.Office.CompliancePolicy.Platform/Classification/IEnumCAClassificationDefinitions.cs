using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x0200001B RID: 27
	[Guid("6B83FB56-85B3-40C8-901E-22AD67C0BE81")]
	[TypeLibType(TypeLibTypeFlags.FNonExtensible)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IEnumCAClassificationDefinitions
	{
		// Token: 0x06000073 RID: 115
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int Next([In] uint cElementsWanted, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Interface, SizeParamIndex = 2)] ICAClassificationDefinition[] classificationDefinitions, out uint elementsFetched);

		// Token: 0x06000074 RID: 116
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int Skip([In] uint cElementsToSkip);

		// Token: 0x06000075 RID: 117
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Reset();

		// Token: 0x06000076 RID: 118
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IEnumCAClassificationDefinitions Clone();
	}
}
