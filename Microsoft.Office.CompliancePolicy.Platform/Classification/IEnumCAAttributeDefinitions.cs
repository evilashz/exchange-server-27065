using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x02000015 RID: 21
	[TypeLibType(TypeLibTypeFlags.FNonExtensible)]
	[Guid("B306FB28-C252-419E-9C6A-A271C8D9FEB7")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IEnumCAAttributeDefinitions
	{
		// Token: 0x0600005C RID: 92
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int Next([In] uint cElementsWanted, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Interface, SizeParamIndex = 2)] ICAAttributeDefinition[] attributeDefinitions, out uint elementsFetched);

		// Token: 0x0600005D RID: 93
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int Skip([In] uint cElementsToSkip);

		// Token: 0x0600005E RID: 94
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Reset();

		// Token: 0x0600005F RID: 95
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IEnumCAAttributeDefinitions Clone();
	}
}
