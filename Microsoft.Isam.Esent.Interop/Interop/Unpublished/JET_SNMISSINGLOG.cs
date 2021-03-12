using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x02000053 RID: 83
	public class JET_SNMISSINGLOG : JET_RECOVERYCONTROL
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0000B428 File Offset: 0x00009628
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x0000B430 File Offset: 0x00009630
		public int lGenMissing { get; internal set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000B439 File Offset: 0x00009639
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x0000B441 File Offset: 0x00009641
		public bool fCurrentLog { get; internal set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0000B44A File Offset: 0x0000964A
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x0000B452 File Offset: 0x00009652
		public JET_RECOVERYACTIONS eNextAction { get; internal set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0000B45B File Offset: 0x0000965B
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x0000B463 File Offset: 0x00009663
		public string wszLogFile { get; internal set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0000B46C File Offset: 0x0000966C
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x0000B474 File Offset: 0x00009674
		public int cdbinfomisc { get; internal set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000B47D File Offset: 0x0000967D
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x0000B485 File Offset: 0x00009685
		public JET_DBINFOMISC[] rgdbinfomisc { get; internal set; }

		// Token: 0x0600048D RID: 1165 RVA: 0x0000B490 File Offset: 0x00009690
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_SNMISSINGLOG({0})", new object[]
			{
				this.wszLogFile
			});
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000B4C0 File Offset: 0x000096C0
		internal void SetFromNativeSnmissinglog(ref NATIVE_SNMISSINGLOG native)
		{
			base.SetFromNativeSnrecoverycontrol(ref native.recoveryControl);
			checked
			{
				this.lGenMissing = (int)native.lGenMissing;
				this.fCurrentLog = (native.fCurrentLog != 0);
				this.eNextAction = (JET_RECOVERYACTIONS)native.eNextAction;
				this.wszLogFile = Marshal.PtrToStringUni(native.wszLogFile);
				this.cdbinfomisc = (int)native.cdbinfomisc;
				if (this.cdbinfomisc > 0)
				{
					NATIVE_DBINFOMISC7[] array = new NATIVE_DBINFOMISC7[this.cdbinfomisc];
					array[0] = (NATIVE_DBINFOMISC7)Marshal.PtrToStructure(native.rgdbinfomisc, typeof(NATIVE_DBINFOMISC7));
				}
			}
		}
	}
}
