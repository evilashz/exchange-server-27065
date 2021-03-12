using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200093F RID: 2367
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum TypeLibExporterFlags
	{
		// Token: 0x04002AF1 RID: 10993
		None = 0,
		// Token: 0x04002AF2 RID: 10994
		OnlyReferenceRegistered = 1,
		// Token: 0x04002AF3 RID: 10995
		CallerResolvedReferences = 2,
		// Token: 0x04002AF4 RID: 10996
		OldNames = 4,
		// Token: 0x04002AF5 RID: 10997
		ExportAs32Bit = 16,
		// Token: 0x04002AF6 RID: 10998
		ExportAs64Bit = 32
	}
}
