using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006A9 RID: 1705
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("9D46FB70-7B54-4f4f-9331-BA9E87833FF5")]
	[ComImport]
	internal interface IHashElementEntry
	{
		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06004F52 RID: 20306
		HashElementEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06004F53 RID: 20307
		uint index { [SecurityCritical] get; }

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06004F54 RID: 20308
		byte Transform { [SecurityCritical] get; }

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06004F55 RID: 20309
		object TransformMetadata { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06004F56 RID: 20310
		byte DigestMethod { [SecurityCritical] get; }

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06004F57 RID: 20311
		object DigestValue { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06004F58 RID: 20312
		string Xml { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }
	}
}
