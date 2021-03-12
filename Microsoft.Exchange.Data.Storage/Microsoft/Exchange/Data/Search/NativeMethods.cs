using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Search
{
	// Token: 0x02000CEE RID: 3310
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NativeMethods
	{
		// Token: 0x06007236 RID: 29238
		[DllImport("ole32.dll")]
		public static extern int CoCreateInstance([MarshalAs(UnmanagedType.LPStruct)] [In] Guid rclsid, [MarshalAs(UnmanagedType.IUnknown)] [In] object punkOuter, [In] uint dwClsCtx, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);
	}
}
