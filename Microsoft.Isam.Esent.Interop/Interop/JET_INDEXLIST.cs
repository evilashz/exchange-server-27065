using System;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200029B RID: 667
	public sealed class JET_INDEXLIST
	{
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x00017BE6 File Offset: 0x00015DE6
		// (set) Token: 0x06000BB5 RID: 2997 RVA: 0x00017BEE File Offset: 0x00015DEE
		public JET_TABLEID tableid { get; internal set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x00017BF7 File Offset: 0x00015DF7
		// (set) Token: 0x06000BB7 RID: 2999 RVA: 0x00017BFF File Offset: 0x00015DFF
		public int cRecord { get; internal set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x00017C08 File Offset: 0x00015E08
		// (set) Token: 0x06000BB9 RID: 3001 RVA: 0x00017C10 File Offset: 0x00015E10
		public JET_COLUMNID columnidindexname { get; internal set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x00017C19 File Offset: 0x00015E19
		// (set) Token: 0x06000BBB RID: 3003 RVA: 0x00017C21 File Offset: 0x00015E21
		public JET_COLUMNID columnidgrbitIndex { get; internal set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x00017C2A File Offset: 0x00015E2A
		// (set) Token: 0x06000BBD RID: 3005 RVA: 0x00017C32 File Offset: 0x00015E32
		public JET_COLUMNID columnidcKey { get; internal set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x00017C3B File Offset: 0x00015E3B
		// (set) Token: 0x06000BBF RID: 3007 RVA: 0x00017C43 File Offset: 0x00015E43
		public JET_COLUMNID columnidcEntry { get; internal set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x00017C4C File Offset: 0x00015E4C
		// (set) Token: 0x06000BC1 RID: 3009 RVA: 0x00017C54 File Offset: 0x00015E54
		public JET_COLUMNID columnidcPage { get; internal set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x00017C5D File Offset: 0x00015E5D
		// (set) Token: 0x06000BC3 RID: 3011 RVA: 0x00017C65 File Offset: 0x00015E65
		public JET_COLUMNID columnidcColumn { get; internal set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x00017C6E File Offset: 0x00015E6E
		// (set) Token: 0x06000BC5 RID: 3013 RVA: 0x00017C76 File Offset: 0x00015E76
		public JET_COLUMNID columnidiColumn { get; internal set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x00017C7F File Offset: 0x00015E7F
		// (set) Token: 0x06000BC7 RID: 3015 RVA: 0x00017C87 File Offset: 0x00015E87
		public JET_COLUMNID columnidcolumnid { get; internal set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x00017C90 File Offset: 0x00015E90
		// (set) Token: 0x06000BC9 RID: 3017 RVA: 0x00017C98 File Offset: 0x00015E98
		public JET_COLUMNID columnidcoltyp { get; internal set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x00017CA1 File Offset: 0x00015EA1
		// (set) Token: 0x06000BCB RID: 3019 RVA: 0x00017CA9 File Offset: 0x00015EA9
		public JET_COLUMNID columnidLangid { get; internal set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x00017CB2 File Offset: 0x00015EB2
		// (set) Token: 0x06000BCD RID: 3021 RVA: 0x00017CBA File Offset: 0x00015EBA
		public JET_COLUMNID columnidCp { get; internal set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x00017CC3 File Offset: 0x00015EC3
		// (set) Token: 0x06000BCF RID: 3023 RVA: 0x00017CCB File Offset: 0x00015ECB
		public JET_COLUMNID columnidgrbitColumn { get; internal set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x00017CD4 File Offset: 0x00015ED4
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x00017CDC File Offset: 0x00015EDC
		public JET_COLUMNID columnidcolumnname { get; internal set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x00017CE5 File Offset: 0x00015EE5
		// (set) Token: 0x06000BD3 RID: 3027 RVA: 0x00017CED File Offset: 0x00015EED
		public JET_COLUMNID columnidLCMapFlags { get; internal set; }

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00017CF8 File Offset: 0x00015EF8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_INDEXLIST(0x{0:x},{1} records)", new object[]
			{
				this.tableid,
				this.cRecord
			});
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00017D38 File Offset: 0x00015F38
		internal void SetFromNativeIndexlist(NATIVE_INDEXLIST value)
		{
			this.tableid = new JET_TABLEID
			{
				Value = value.tableid
			};
			this.cRecord = checked((int)value.cRecord);
			this.columnidindexname = new JET_COLUMNID
			{
				Value = value.columnidindexname
			};
			this.columnidgrbitIndex = new JET_COLUMNID
			{
				Value = value.columnidgrbitIndex
			};
			this.columnidcKey = new JET_COLUMNID
			{
				Value = value.columnidcKey
			};
			this.columnidcEntry = new JET_COLUMNID
			{
				Value = value.columnidcEntry
			};
			this.columnidcPage = new JET_COLUMNID
			{
				Value = value.columnidcPage
			};
			this.columnidcColumn = new JET_COLUMNID
			{
				Value = value.columnidcColumn
			};
			this.columnidiColumn = new JET_COLUMNID
			{
				Value = value.columnidiColumn
			};
			this.columnidcolumnid = new JET_COLUMNID
			{
				Value = value.columnidcolumnid
			};
			this.columnidcoltyp = new JET_COLUMNID
			{
				Value = value.columnidcoltyp
			};
			this.columnidLangid = new JET_COLUMNID
			{
				Value = value.columnidLangid
			};
			this.columnidCp = new JET_COLUMNID
			{
				Value = value.columnidCp
			};
			this.columnidgrbitColumn = new JET_COLUMNID
			{
				Value = value.columnidgrbitColumn
			};
			this.columnidcolumnname = new JET_COLUMNID
			{
				Value = value.columnidcolumnname
			};
			this.columnidLCMapFlags = new JET_COLUMNID
			{
				Value = value.columnidLCMapFlags
			};
		}
	}
}
