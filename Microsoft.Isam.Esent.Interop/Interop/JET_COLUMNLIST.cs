using System;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000276 RID: 630
	public class JET_COLUMNLIST
	{
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x000169DB File Offset: 0x00014BDB
		// (set) Token: 0x06000B16 RID: 2838 RVA: 0x000169E3 File Offset: 0x00014BE3
		public JET_TABLEID tableid { get; internal set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x000169EC File Offset: 0x00014BEC
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x000169F4 File Offset: 0x00014BF4
		public int cRecord { get; internal set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x000169FD File Offset: 0x00014BFD
		// (set) Token: 0x06000B1A RID: 2842 RVA: 0x00016A05 File Offset: 0x00014C05
		public JET_COLUMNID columnidcolumnname { get; internal set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x00016A0E File Offset: 0x00014C0E
		// (set) Token: 0x06000B1C RID: 2844 RVA: 0x00016A16 File Offset: 0x00014C16
		public JET_COLUMNID columnidcolumnid { get; internal set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00016A1F File Offset: 0x00014C1F
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x00016A27 File Offset: 0x00014C27
		public JET_COLUMNID columnidcoltyp { get; internal set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00016A30 File Offset: 0x00014C30
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x00016A38 File Offset: 0x00014C38
		public JET_COLUMNID columnidCp { get; internal set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00016A41 File Offset: 0x00014C41
		// (set) Token: 0x06000B22 RID: 2850 RVA: 0x00016A49 File Offset: 0x00014C49
		public JET_COLUMNID columnidcbMax { get; internal set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x00016A52 File Offset: 0x00014C52
		// (set) Token: 0x06000B24 RID: 2852 RVA: 0x00016A5A File Offset: 0x00014C5A
		public JET_COLUMNID columnidgrbit { get; internal set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x00016A63 File Offset: 0x00014C63
		// (set) Token: 0x06000B26 RID: 2854 RVA: 0x00016A6B File Offset: 0x00014C6B
		public JET_COLUMNID columnidDefault { get; internal set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x00016A74 File Offset: 0x00014C74
		// (set) Token: 0x06000B28 RID: 2856 RVA: 0x00016A7C File Offset: 0x00014C7C
		public JET_COLUMNID columnidBaseTableName { get; internal set; }

		// Token: 0x06000B29 RID: 2857 RVA: 0x00016A88 File Offset: 0x00014C88
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_COLUMNLIST(0x{0:x},{1} records)", new object[]
			{
				this.tableid,
				this.cRecord
			});
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00016AC8 File Offset: 0x00014CC8
		internal void SetFromNativeColumnlist(NATIVE_COLUMNLIST value)
		{
			this.tableid = new JET_TABLEID
			{
				Value = value.tableid
			};
			this.cRecord = checked((int)value.cRecord);
			this.columnidcolumnname = new JET_COLUMNID
			{
				Value = value.columnidcolumnname
			};
			this.columnidcolumnid = new JET_COLUMNID
			{
				Value = value.columnidcolumnid
			};
			this.columnidcoltyp = new JET_COLUMNID
			{
				Value = value.columnidcoltyp
			};
			this.columnidCp = new JET_COLUMNID
			{
				Value = value.columnidCp
			};
			this.columnidcbMax = new JET_COLUMNID
			{
				Value = value.columnidcbMax
			};
			this.columnidgrbit = new JET_COLUMNID
			{
				Value = value.columnidgrbit
			};
			this.columnidDefault = new JET_COLUMNID
			{
				Value = value.columnidDefault
			};
			this.columnidBaseTableName = new JET_COLUMNID
			{
				Value = value.columnidBaseTableName
			};
		}
	}
}
