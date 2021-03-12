using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000076 RID: 118
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("149C53E5-8519-45dc-9F8B-9D248DB1A78C")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[ComImport]
	internal interface IExchangeManifestExCallback
	{
		// Token: 0x060002FB RID: 763
		[PreserveSig]
		unsafe int Change(ExchangeManifestCallbackChangeFlags flags, int cpvalHeader, SPropValue* ppvalHeader, int cpvalProps, SPropValue* ppvalProps);

		// Token: 0x060002FC RID: 764
		[PreserveSig]
		int Delete(ExchangeManifestCallbackDeleteFlags flags, int cbIdsetDeleted, IntPtr pbIdsetDeleted);

		// Token: 0x060002FD RID: 765
		[PreserveSig]
		int Read(ExchangeManifestCallbackReadFlags flags, int cbIdsetReadUnread, IntPtr pbIdsetReadUnread);
	}
}
