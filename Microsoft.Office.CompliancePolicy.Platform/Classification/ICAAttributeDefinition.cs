using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x02000014 RID: 20
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[TypeLibType(TypeLibTypeFlags.FNonExtensible)]
	[Guid("5F3FC246-F5B7-4997-B1AB-8125F923679C")]
	[ComImport]
	public interface ICAAttributeDefinition
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000059 RID: 89
		string ID { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005A RID: 90
		ushort Type { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005B RID: 91
		string Name { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }
	}
}
