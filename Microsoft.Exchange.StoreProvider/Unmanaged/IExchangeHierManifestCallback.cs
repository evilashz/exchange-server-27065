using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000062 RID: 98
	[ClassAccessLevel(AccessLevel.Implementation)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("36F25379-50FD-4662-9021-AC684B0D6AAA")]
	[ComImport]
	internal interface IExchangeHierManifestCallback
	{
		// Token: 0x060002B7 RID: 695
		[PreserveSig]
		unsafe int Change(int cpval, SPropValue* ppval);

		// Token: 0x060002B8 RID: 696
		[PreserveSig]
		int Delete(int cbIdsetDeleted, IntPtr pbIdsetDeleted);
	}
}
