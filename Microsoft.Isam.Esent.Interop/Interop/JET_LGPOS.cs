using System;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002A0 RID: 672
	[Serializable]
	public struct JET_LGPOS : IEquatable<JET_LGPOS>, IComparable<JET_LGPOS>, INullableJetStruct
	{
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x00018345 File Offset: 0x00016545
		// (set) Token: 0x06000BF1 RID: 3057 RVA: 0x0001834D File Offset: 0x0001654D
		public int ib
		{
			[DebuggerStepThrough]
			get
			{
				return (int)this.offset;
			}
			set
			{
				this.offset = checked((ushort)value);
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x00018357 File Offset: 0x00016557
		// (set) Token: 0x06000BF3 RID: 3059 RVA: 0x0001835F File Offset: 0x0001655F
		public int isec
		{
			[DebuggerStepThrough]
			get
			{
				return (int)this.sector;
			}
			set
			{
				this.sector = checked((ushort)value);
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x00018369 File Offset: 0x00016569
		// (set) Token: 0x06000BF5 RID: 3061 RVA: 0x00018371 File Offset: 0x00016571
		public int lGeneration
		{
			[DebuggerStepThrough]
			get
			{
				return this.generation;
			}
			set
			{
				this.generation = value;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x0001837A File Offset: 0x0001657A
		public bool HasValue
		{
			get
			{
				return 0 != this.lGeneration;
			}
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00018388 File Offset: 0x00016588
		public static bool operator ==(JET_LGPOS lhs, JET_LGPOS rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00018392 File Offset: 0x00016592
		public static bool operator !=(JET_LGPOS lhs, JET_LGPOS rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0001839E File Offset: 0x0001659E
		public static bool operator <(JET_LGPOS lhs, JET_LGPOS rhs)
		{
			return lhs.CompareTo(rhs) < 0;
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x000183AB File Offset: 0x000165AB
		public static bool operator >(JET_LGPOS lhs, JET_LGPOS rhs)
		{
			return lhs.CompareTo(rhs) > 0;
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x000183B8 File Offset: 0x000165B8
		public static bool operator <=(JET_LGPOS lhs, JET_LGPOS rhs)
		{
			return lhs.CompareTo(rhs) <= 0;
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x000183C8 File Offset: 0x000165C8
		public static bool operator >=(JET_LGPOS lhs, JET_LGPOS rhs)
		{
			return lhs.CompareTo(rhs) >= 0;
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x000183D8 File Offset: 0x000165D8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_LGPOS(0x{0:X},{1:X},{2:X})", new object[]
			{
				this.lGeneration,
				this.isec,
				this.ib
			});
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00018426 File Offset: 0x00016626
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((JET_LGPOS)obj);
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00018456 File Offset: 0x00016656
		public override int GetHashCode()
		{
			return this.generation ^ (int)this.sector << 16 ^ (int)this.offset;
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0001846F File Offset: 0x0001666F
		public bool Equals(JET_LGPOS other)
		{
			return this.generation == other.generation && this.sector == other.sector && this.offset == other.offset;
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x000184A0 File Offset: 0x000166A0
		public int CompareTo(JET_LGPOS other)
		{
			int num = this.generation.CompareTo(other.generation);
			if (num == 0)
			{
				num = this.sector.CompareTo(other.sector);
			}
			if (num == 0)
			{
				num = this.offset.CompareTo(other.offset);
			}
			return num;
		}

		// Token: 0x0400075F RID: 1887
		private ushort offset;

		// Token: 0x04000760 RID: 1888
		private ushort sector;

		// Token: 0x04000761 RID: 1889
		private int generation;
	}
}
