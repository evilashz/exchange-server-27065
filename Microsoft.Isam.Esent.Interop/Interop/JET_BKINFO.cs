using System;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000268 RID: 616
	[Serializable]
	public struct JET_BKINFO : IEquatable<JET_BKINFO>, INullableJetStruct
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x00015CAC File Offset: 0x00013EAC
		// (set) Token: 0x06000ABA RID: 2746 RVA: 0x00015CB4 File Offset: 0x00013EB4
		public JET_LGPOS lgposMark
		{
			[DebuggerStepThrough]
			get
			{
				return this.logPosition;
			}
			internal set
			{
				this.logPosition = value;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x00015CBD File Offset: 0x00013EBD
		// (set) Token: 0x06000ABC RID: 2748 RVA: 0x00015CC5 File Offset: 0x00013EC5
		public JET_BKLOGTIME bklogtimeMark
		{
			[DebuggerStepThrough]
			get
			{
				return this.backupTime;
			}
			internal set
			{
				this.backupTime = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x00015CCE File Offset: 0x00013ECE
		// (set) Token: 0x06000ABE RID: 2750 RVA: 0x00015CD6 File Offset: 0x00013ED6
		public int genLow
		{
			[DebuggerStepThrough]
			get
			{
				return (int)this.lowGeneration;
			}
			internal set
			{
				this.lowGeneration = checked((uint)value);
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x00015CE0 File Offset: 0x00013EE0
		// (set) Token: 0x06000AC0 RID: 2752 RVA: 0x00015CE8 File Offset: 0x00013EE8
		public int genHigh
		{
			[DebuggerStepThrough]
			get
			{
				return (int)this.highGeneration;
			}
			set
			{
				this.highGeneration = checked((uint)value);
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00015CF4 File Offset: 0x00013EF4
		public bool HasValue
		{
			get
			{
				return this.lgposMark.HasValue && this.backupTime.HasValue && this.lowGeneration != 0U && 0U != this.highGeneration;
			}
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00015D34 File Offset: 0x00013F34
		public static bool operator ==(JET_BKINFO lhs, JET_BKINFO rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00015D3E File Offset: 0x00013F3E
		public static bool operator !=(JET_BKINFO lhs, JET_BKINFO rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00015D4C File Offset: 0x00013F4C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_BKINFO({0}-{1}:{2}:{3})", new object[]
			{
				this.genLow,
				this.genHigh,
				this.lgposMark,
				this.bklogtimeMark
			});
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00015DA8 File Offset: 0x00013FA8
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((JET_BKINFO)obj);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00015DD8 File Offset: 0x00013FD8
		public override int GetHashCode()
		{
			return this.logPosition.GetHashCode() ^ this.backupTime.GetHashCode() ^ (int)((int)this.lowGeneration << 16) ^ (int)this.lowGeneration >> 16 ^ (int)this.highGeneration;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00015E18 File Offset: 0x00014018
		public bool Equals(JET_BKINFO other)
		{
			return this.logPosition == other.logPosition && this.backupTime == other.backupTime && this.lowGeneration == other.lowGeneration && this.highGeneration == other.highGeneration;
		}

		// Token: 0x04000456 RID: 1110
		private JET_LGPOS logPosition;

		// Token: 0x04000457 RID: 1111
		private JET_BKLOGTIME backupTime;

		// Token: 0x04000458 RID: 1112
		private uint lowGeneration;

		// Token: 0x04000459 RID: 1113
		private uint highGeneration;
	}
}
