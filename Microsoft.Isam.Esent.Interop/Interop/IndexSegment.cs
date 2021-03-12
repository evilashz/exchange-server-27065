using System;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000262 RID: 610
	[Serializable]
	public class IndexSegment : IEquatable<IndexSegment>
	{
		// Token: 0x06000A91 RID: 2705 RVA: 0x000156EA File Offset: 0x000138EA
		internal IndexSegment(string name, JET_coltyp coltyp, bool isAscending, bool isASCII)
		{
			this.columnName = name;
			this.coltyp = coltyp;
			this.isAscending = isAscending;
			this.isASCII = isASCII;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x0001570F File Offset: 0x0001390F
		public string ColumnName
		{
			[DebuggerStepThrough]
			get
			{
				return this.columnName;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x00015717 File Offset: 0x00013917
		public JET_coltyp Coltyp
		{
			[DebuggerStepThrough]
			get
			{
				return this.coltyp;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x0001571F File Offset: 0x0001391F
		public bool IsAscending
		{
			[DebuggerStepThrough]
			get
			{
				return this.isAscending;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x00015727 File Offset: 0x00013927
		public bool IsASCII
		{
			[DebuggerStepThrough]
			get
			{
				return this.isASCII;
			}
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0001572F File Offset: 0x0001392F
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((IndexSegment)obj);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00015758 File Offset: 0x00013958
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}({2})", new object[]
			{
				this.isAscending ? "+" : "-",
				this.columnName,
				this.coltyp
			});
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x000157AC File Offset: 0x000139AC
		public override int GetHashCode()
		{
			return this.columnName.GetHashCode() ^ (int)(this.coltyp * (JET_coltyp)31) ^ (this.isAscending ? 65536 : 131072) ^ (this.isASCII ? 262144 : 524288);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x000157F8 File Offset: 0x000139F8
		public bool Equals(IndexSegment other)
		{
			return other != null && (this.columnName.Equals(other.columnName, StringComparison.OrdinalIgnoreCase) && this.coltyp == other.coltyp && this.isAscending == other.isAscending) && this.isASCII == other.isASCII;
		}

		// Token: 0x0400044C RID: 1100
		private readonly string columnName;

		// Token: 0x0400044D RID: 1101
		private readonly JET_coltyp coltyp;

		// Token: 0x0400044E RID: 1102
		private readonly bool isAscending;

		// Token: 0x0400044F RID: 1103
		private readonly bool isASCII;
	}
}
