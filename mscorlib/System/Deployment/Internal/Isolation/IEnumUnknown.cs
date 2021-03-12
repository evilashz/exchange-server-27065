using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000645 RID: 1605
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("00000100-0000-0000-C000-000000000046")]
	[ComImport]
	internal interface IEnumUnknown
	{
		// Token: 0x06004E06 RID: 19974
		[PreserveSig]
		int Next(uint celt, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.IUnknown)] [Out] object[] rgelt, ref uint celtFetched);

		// Token: 0x06004E07 RID: 19975
		[PreserveSig]
		int Skip(uint celt);

		// Token: 0x06004E08 RID: 19976
		[PreserveSig]
		int Reset();

		// Token: 0x06004E09 RID: 19977
		[PreserveSig]
		int Clone(out IEnumUnknown enumUnknown);
	}
}
