using System;

namespace Microsoft.Isam.Esent.Interop.Win32
{
	// Token: 0x02000307 RID: 775
	[Flags]
	internal enum AllocationType : uint
	{
		// Token: 0x0400097F RID: 2431
		MEM_COMMIT = 4096U,
		// Token: 0x04000980 RID: 2432
		MEM_RESERVE = 8192U
	}
}
