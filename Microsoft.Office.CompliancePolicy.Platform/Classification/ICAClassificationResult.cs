using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x02000017 RID: 23
	[TypeLibType(TypeLibTypeFlags.FNonExtensible)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("7061630B-0054-4A58-813F-25E5EB2C3BC6")]
	[ComImport]
	public interface ICAClassificationResult
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000063 RID: 99
		string ID { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x06000064 RID: 100
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object GetAttributeValue([MarshalAs(UnmanagedType.BStr)] [In] string attributeId);

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000065 RID: 101
		string RulePackageSetID { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000066 RID: 102
		string RulePackageID { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }
	}
}
