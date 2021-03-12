using System;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x02000046 RID: 70
	internal struct NATIVE_RECOVERY_CONTROL
	{
		// Token: 0x0400015B RID: 347
		public uint cbStruct;

		// Token: 0x0400015C RID: 348
		public JET_err errDefault;

		// Token: 0x0400015D RID: 349
		public IntPtr instance;

		// Token: 0x0400015E RID: 350
		public JET_SNT sntUnion;
	}
}
