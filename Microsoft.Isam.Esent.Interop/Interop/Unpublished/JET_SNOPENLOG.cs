using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x02000059 RID: 89
	public class JET_SNOPENLOG : JET_RECOVERYCONTROL
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0000B634 File Offset: 0x00009834
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x0000B63C File Offset: 0x0000983C
		public int lGenNext { get; internal set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0000B645 File Offset: 0x00009845
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x0000B64D File Offset: 0x0000984D
		public bool fCurrentLog { get; internal set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000B656 File Offset: 0x00009856
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x0000B65E File Offset: 0x0000985E
		public JET_OpenLog eReason { get; internal set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0000B667 File Offset: 0x00009867
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x0000B66F File Offset: 0x0000986F
		public string wszLogFile { get; internal set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0000B678 File Offset: 0x00009878
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x0000B680 File Offset: 0x00009880
		public int cdbinfomisc { get; internal set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0000B689 File Offset: 0x00009889
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x0000B691 File Offset: 0x00009891
		public JET_DBINFOMISC[] rgdbinfomisc { get; internal set; }

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000B69C File Offset: 0x0000989C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_SNOPENLOG({0})", new object[]
			{
				this.wszLogFile
			});
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000B6CC File Offset: 0x000098CC
		internal void SetFromNativeSnopenlog(ref NATIVE_SNOPENLOG native)
		{
			base.SetFromNativeSnrecoverycontrol(ref native.recoveryControl);
			checked
			{
				this.lGenNext = (int)native.lGenNext;
				this.fCurrentLog = (native.fCurrentLog != 0);
				this.eReason = (JET_OpenLog)native.eReason;
				this.wszLogFile = Marshal.PtrToStringUni(native.wszLogFile);
				this.cdbinfomisc = (int)native.cdbinfomisc;
				if (this.cdbinfomisc > 0)
				{
					NATIVE_DBINFOMISC7[] array = new NATIVE_DBINFOMISC7[this.cdbinfomisc];
					array[0] = (NATIVE_DBINFOMISC7)Marshal.PtrToStructure(native.rgdbinfomisc, typeof(NATIVE_DBINFOMISC7));
					this.rgdbinfomisc = new JET_DBINFOMISC[this.cdbinfomisc];
					this.rgdbinfomisc[0] = new JET_DBINFOMISC();
					this.rgdbinfomisc[0].SetFromNativeDbinfoMisc(ref array[0]);
				}
			}
		}
	}
}
