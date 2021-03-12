using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop.Vista
{
	// Token: 0x020002A9 RID: 681
	public class JET_OPENTEMPORARYTABLE
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x000187A7 File Offset: 0x000169A7
		// (set) Token: 0x06000C25 RID: 3109 RVA: 0x000187AF File Offset: 0x000169AF
		public JET_COLUMNDEF[] prgcolumndef { get; set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x000187B8 File Offset: 0x000169B8
		// (set) Token: 0x06000C27 RID: 3111 RVA: 0x000187C0 File Offset: 0x000169C0
		public int ccolumn { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x000187C9 File Offset: 0x000169C9
		// (set) Token: 0x06000C29 RID: 3113 RVA: 0x000187D1 File Offset: 0x000169D1
		public JET_UNICODEINDEX pidxunicode { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x000187DA File Offset: 0x000169DA
		// (set) Token: 0x06000C2B RID: 3115 RVA: 0x000187E2 File Offset: 0x000169E2
		public TempTableGrbit grbit { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x000187EB File Offset: 0x000169EB
		// (set) Token: 0x06000C2D RID: 3117 RVA: 0x000187F3 File Offset: 0x000169F3
		public JET_COLUMNID[] prgcolumnid { get; set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x000187FC File Offset: 0x000169FC
		// (set) Token: 0x06000C2F RID: 3119 RVA: 0x00018804 File Offset: 0x00016A04
		public int cbKeyMost { get; set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x0001880D File Offset: 0x00016A0D
		// (set) Token: 0x06000C31 RID: 3121 RVA: 0x00018815 File Offset: 0x00016A15
		public int cbVarSegMac { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0001881E File Offset: 0x00016A1E
		// (set) Token: 0x06000C33 RID: 3123 RVA: 0x00018826 File Offset: 0x00016A26
		public JET_TABLEID tableid { get; internal set; }

		// Token: 0x06000C34 RID: 3124 RVA: 0x00018830 File Offset: 0x00016A30
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_OPENTEMPORARYTABLE({0}, {1} columns)", new object[]
			{
				this.grbit,
				this.ccolumn
			});
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00018870 File Offset: 0x00016A70
		internal NATIVE_OPENTEMPORARYTABLE GetNativeOpenTemporaryTable()
		{
			this.CheckDataSize();
			return checked(new NATIVE_OPENTEMPORARYTABLE
			{
				cbStruct = (uint)Marshal.SizeOf(typeof(NATIVE_OPENTEMPORARYTABLE)),
				ccolumn = (uint)this.ccolumn,
				grbit = (uint)this.grbit,
				cbKeyMost = (uint)this.cbKeyMost,
				cbVarSegMac = (uint)this.cbVarSegMac
			});
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x000188DC File Offset: 0x00016ADC
		private void CheckDataSize()
		{
			if (this.prgcolumndef == null)
			{
				throw new ArgumentNullException("prgcolumndef");
			}
			if (this.prgcolumnid == null)
			{
				throw new ArgumentNullException("prgcolumnid");
			}
			if (this.ccolumn < 0)
			{
				throw new ArgumentOutOfRangeException("ccolumn", this.ccolumn, "cannot be negative");
			}
			if (this.ccolumn > this.prgcolumndef.Length)
			{
				throw new ArgumentOutOfRangeException("ccolumn", this.ccolumn, "cannot be greater than prgcolumndef.Length");
			}
			if (this.ccolumn > this.prgcolumnid.Length)
			{
				throw new ArgumentOutOfRangeException("ccolumn", this.ccolumn, "cannot be greater than prgcolumnid.Length");
			}
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0001898C File Offset: 0x00016B8C
		internal NATIVE_OPENTEMPORARYTABLE2 GetNativeOpenTemporaryTable2()
		{
			this.CheckDataSize();
			return checked(new NATIVE_OPENTEMPORARYTABLE2
			{
				cbStruct = (uint)Marshal.SizeOf(typeof(NATIVE_OPENTEMPORARYTABLE2)),
				ccolumn = (uint)this.ccolumn,
				grbit = (uint)this.grbit,
				cbKeyMost = (uint)this.cbKeyMost,
				cbVarSegMac = (uint)this.cbVarSegMac
			});
		}
	}
}
