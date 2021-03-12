using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x02000016 RID: 22
	[TypeLibType(TypeLibTypeFlags.FNonExtensible)]
	[Guid("C8012AE8-E7A7-4158-ABFC-14F5582CE22B")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ICAAttributeDefinitionCollection
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000060 RID: 96
		int Count { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000016 RID: 22
		ICAAttributeDefinition this[int nIndex]
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000062 RID: 98
		object _NewEnum { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IUnknown)] get; }
	}
}
