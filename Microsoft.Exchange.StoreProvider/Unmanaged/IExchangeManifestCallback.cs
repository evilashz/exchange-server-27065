using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000073 RID: 115
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("8F590A55-9A10-4cd9-916C-8748E24C311F")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[ComImport]
	internal interface IExchangeManifestCallback
	{
		// Token: 0x060002EB RID: 747
		[PreserveSig]
		unsafe int Change(ExchangeManifestCallbackChangeFlags flags, int cpvalHeader, SPropValue* ppvalHeader, int cpvalProps, SPropValue* ppvalProps);

		// Token: 0x060002EC RID: 748
		[PreserveSig]
		unsafe int Delete(ExchangeManifestCallbackDeleteFlags flags, int cElements, _CallbackInfo* lpCallbackInfo);

		// Token: 0x060002ED RID: 749
		[PreserveSig]
		unsafe int Read(ExchangeManifestCallbackReadFlags flags, int cElements, _CallbackInfo* lpCallbackInfo);
	}
}
