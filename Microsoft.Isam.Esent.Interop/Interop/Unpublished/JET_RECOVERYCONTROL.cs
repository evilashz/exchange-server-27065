using System;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x02000047 RID: 71
	public abstract class JET_RECOVERYCONTROL
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000B139 File Offset: 0x00009339
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x0000B141 File Offset: 0x00009341
		public JET_err errDefault { get; internal set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000B14A File Offset: 0x0000934A
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x0000B152 File Offset: 0x00009352
		public JET_INSTANCE instance { get; private set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0000B15B File Offset: 0x0000935B
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x0000B163 File Offset: 0x00009363
		public JET_SNT sntUnion { get; private set; }

		// Token: 0x06000463 RID: 1123 RVA: 0x0000B16C File Offset: 0x0000936C
		internal void SetFromNativeSnrecoverycontrol(ref NATIVE_RECOVERY_CONTROL native)
		{
			this.errDefault = native.errDefault;
			this.instance = new JET_INSTANCE
			{
				Value = native.instance
			};
			this.sntUnion = native.sntUnion;
		}
	}
}
