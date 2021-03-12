using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x0200001C RID: 28
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("F3E2330F-E898-46B6-BE7A-F61457B90A6F")]
	[TypeLibType(TypeLibTypeFlags.FNonExtensible)]
	[ComImport]
	public interface ICAClassificationDefinitionCollection
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000077 RID: 119
		int Count { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000022 RID: 34
		ICAClassificationDefinition this[object index]
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000079 RID: 121
		object _NewEnum { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IUnknown)] get; }
	}
}
