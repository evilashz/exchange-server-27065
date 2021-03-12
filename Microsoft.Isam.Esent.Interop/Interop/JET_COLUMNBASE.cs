using System;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000270 RID: 624
	public sealed class JET_COLUMNBASE : IEquatable<JET_COLUMNBASE>
	{
		// Token: 0x06000AD7 RID: 2775 RVA: 0x000161A3 File Offset: 0x000143A3
		internal JET_COLUMNBASE()
		{
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x000161AC File Offset: 0x000143AC
		internal JET_COLUMNBASE(NATIVE_COLUMNBASE value)
		{
			this.coltyp = (JET_coltyp)value.coltyp;
			this.cp = (JET_CP)value.cp;
			this.cbMax = checked((int)value.cbMax);
			this.grbit = (ColumndefGrbit)value.grbit;
			this.columnid = new JET_COLUMNID
			{
				Value = value.columnid
			};
			this.szBaseTableName = StringCache.TryToIntern(value.szBaseTableName);
			this.szBaseColumnName = StringCache.TryToIntern(value.szBaseColumnName);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00016238 File Offset: 0x00014438
		internal JET_COLUMNBASE(NATIVE_COLUMNBASE_WIDE value)
		{
			this.coltyp = (JET_coltyp)value.coltyp;
			this.cp = (JET_CP)value.cp;
			this.cbMax = checked((int)value.cbMax);
			this.grbit = (ColumndefGrbit)value.grbit;
			this.columnid = new JET_COLUMNID
			{
				Value = value.columnid
			};
			this.szBaseTableName = StringCache.TryToIntern(value.szBaseTableName);
			this.szBaseColumnName = StringCache.TryToIntern(value.szBaseColumnName);
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x000162C1 File Offset: 0x000144C1
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x000162C9 File Offset: 0x000144C9
		public JET_coltyp coltyp { get; internal set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x000162D2 File Offset: 0x000144D2
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x000162DA File Offset: 0x000144DA
		public JET_CP cp { get; internal set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x000162E3 File Offset: 0x000144E3
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x000162EB File Offset: 0x000144EB
		public int cbMax { get; internal set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x000162F4 File Offset: 0x000144F4
		// (set) Token: 0x06000AE1 RID: 2785 RVA: 0x000162FC File Offset: 0x000144FC
		public ColumndefGrbit grbit { get; internal set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x00016305 File Offset: 0x00014505
		// (set) Token: 0x06000AE3 RID: 2787 RVA: 0x0001630D File Offset: 0x0001450D
		public JET_COLUMNID columnid { get; internal set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x00016316 File Offset: 0x00014516
		// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x0001631E File Offset: 0x0001451E
		public string szBaseTableName { get; internal set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x00016327 File Offset: 0x00014527
		// (set) Token: 0x06000AE7 RID: 2791 RVA: 0x0001632F File Offset: 0x0001452F
		public string szBaseColumnName { get; internal set; }

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00016338 File Offset: 0x00014538
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_COLUMNBASE({0},{1})", new object[]
			{
				this.coltyp,
				this.grbit
			});
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00016378 File Offset: 0x00014578
		public override int GetHashCode()
		{
			int[] hashes = new int[]
			{
				this.coltyp.GetHashCode(),
				this.cp.GetHashCode(),
				this.cbMax,
				this.grbit.GetHashCode(),
				this.columnid.GetHashCode(),
				this.szBaseTableName.GetHashCode(),
				this.szBaseColumnName.GetHashCode()
			};
			return Util.CalculateHashCode(hashes);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00016409 File Offset: 0x00014609
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((JET_COLUMNBASE)obj);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00016430 File Offset: 0x00014630
		public bool Equals(JET_COLUMNBASE other)
		{
			return other != null && (this.coltyp == other.coltyp && this.cp == other.cp && this.cbMax == other.cbMax && this.columnid == other.columnid && this.grbit == other.grbit && string.Equals(this.szBaseTableName, other.szBaseTableName, StringComparison.Ordinal)) && string.Equals(this.szBaseColumnName, other.szBaseColumnName, StringComparison.Ordinal);
		}
	}
}
