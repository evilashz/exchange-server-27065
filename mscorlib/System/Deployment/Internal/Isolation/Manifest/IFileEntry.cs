using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006AC RID: 1708
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("A2A55FAD-349B-469b-BF12-ADC33D14A937")]
	[ComImport]
	internal interface IFileEntry
	{
		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06004F5D RID: 20317
		FileEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06004F5E RID: 20318
		string Name { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06004F5F RID: 20319
		uint HashAlgorithm { [SecurityCritical] get; }

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06004F60 RID: 20320
		string LoadFrom { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06004F61 RID: 20321
		string SourcePath { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06004F62 RID: 20322
		string ImportPath { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06004F63 RID: 20323
		string SourceName { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06004F64 RID: 20324
		string Location { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06004F65 RID: 20325
		object HashValue { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06004F66 RID: 20326
		ulong Size { [SecurityCritical] get; }

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06004F67 RID: 20327
		string Group { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06004F68 RID: 20328
		uint Flags { [SecurityCritical] get; }

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06004F69 RID: 20329
		IMuiResourceMapEntry MuiMapping { [SecurityCritical] get; }

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06004F6A RID: 20330
		uint WritableType { [SecurityCritical] get; }

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06004F6B RID: 20331
		ISection HashElements { [SecurityCritical] get; }
	}
}
