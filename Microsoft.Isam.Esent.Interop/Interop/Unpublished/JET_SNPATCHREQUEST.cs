using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x0200005B RID: 91
	public class JET_SNPATCHREQUEST
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0000B7A3 File Offset: 0x000099A3
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x0000B7AB File Offset: 0x000099AB
		public int pageNumber { get; internal set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000B7B4 File Offset: 0x000099B4
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x0000B7BC File Offset: 0x000099BC
		public string szLogFile { get; internal set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0000B7C5 File Offset: 0x000099C5
		// (set) Token: 0x060004AE RID: 1198 RVA: 0x0000B7CD File Offset: 0x000099CD
		public JET_INSTANCE instance { get; private set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0000B7D6 File Offset: 0x000099D6
		// (set) Token: 0x060004B0 RID: 1200 RVA: 0x0000B7DE File Offset: 0x000099DE
		public JET_DBINFOMISC dbinfomisc { get; internal set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0000B7E7 File Offset: 0x000099E7
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x0000B7EF File Offset: 0x000099EF
		public byte[] pvToken { get; internal set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0000B7F8 File Offset: 0x000099F8
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x0000B800 File Offset: 0x00009A00
		public int cbToken { get; internal set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0000B809 File Offset: 0x00009A09
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x0000B811 File Offset: 0x00009A11
		public byte[] pvData { get; internal set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0000B81A File Offset: 0x00009A1A
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x0000B822 File Offset: 0x00009A22
		public int cbData { get; internal set; }

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000B82C File Offset: 0x00009A2C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_SNPATCHREQUEST({0})", new object[]
			{
				this.pageNumber
			});
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000B860 File Offset: 0x00009A60
		internal void SetFromNativeSnpatchrequest(ref NATIVE_SNPATCHREQUEST native)
		{
			checked
			{
				this.pageNumber = (int)native.pageNumber;
				this.szLogFile = Marshal.PtrToStringUni(native.szLogFile);
				this.instance = new JET_INSTANCE
				{
					Value = native.instance
				};
				this.dbinfomisc = new JET_DBINFOMISC();
				this.dbinfomisc.SetFromNativeDbinfoMisc(ref native.dbinfomisc);
				this.cbToken = (int)native.cbToken;
				this.pvToken = new byte[this.cbToken];
				if (this.cbToken > 0)
				{
					Marshal.Copy(native.pvToken, this.pvToken, 0, this.cbToken);
				}
				this.cbData = (int)native.cbData;
				this.pvData = new byte[this.cbData];
				if (this.cbData > 0)
				{
					Marshal.Copy(native.pvData, this.pvData, 0, this.cbData);
				}
			}
		}
	}
}
