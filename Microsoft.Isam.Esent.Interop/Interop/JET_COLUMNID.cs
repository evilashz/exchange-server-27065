using System;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002F3 RID: 755
	public struct JET_COLUMNID : IEquatable<JET_COLUMNID>, IComparable<JET_COLUMNID>, IFormattable
	{
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x0001BC20 File Offset: 0x00019E20
		public static JET_COLUMNID Nil
		{
			[DebuggerStepThrough]
			get
			{
				return default(JET_COLUMNID);
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x0001BC36 File Offset: 0x00019E36
		public bool IsInvalid
		{
			get
			{
				return this.Value == 0U || this.Value == uint.MaxValue;
			}
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0001BC4B File Offset: 0x00019E4B
		public static bool operator ==(JET_COLUMNID lhs, JET_COLUMNID rhs)
		{
			return lhs.Value == rhs.Value;
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0001BC5D File Offset: 0x00019E5D
		public static bool operator !=(JET_COLUMNID lhs, JET_COLUMNID rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0001BC69 File Offset: 0x00019E69
		public static bool operator <(JET_COLUMNID lhs, JET_COLUMNID rhs)
		{
			return lhs.CompareTo(rhs) < 0;
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0001BC76 File Offset: 0x00019E76
		public static bool operator >(JET_COLUMNID lhs, JET_COLUMNID rhs)
		{
			return lhs.CompareTo(rhs) > 0;
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0001BC83 File Offset: 0x00019E83
		public static bool operator <=(JET_COLUMNID lhs, JET_COLUMNID rhs)
		{
			return lhs.CompareTo(rhs) <= 0;
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x0001BC93 File Offset: 0x00019E93
		public static bool operator >=(JET_COLUMNID lhs, JET_COLUMNID rhs)
		{
			return lhs.CompareTo(rhs) >= 0;
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x0001BCA4 File Offset: 0x00019EA4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_COLUMNID(0x{0:x})", new object[]
			{
				this.Value
			});
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0001BCD6 File Offset: 0x00019ED6
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (!string.IsNullOrEmpty(format) && !("G" == format))
			{
				return this.Value.ToString(format, formatProvider);
			}
			return this.ToString();
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0001BD07 File Offset: 0x00019F07
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((JET_COLUMNID)obj);
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0001BD37 File Offset: 0x00019F37
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0001BD44 File Offset: 0x00019F44
		public bool Equals(JET_COLUMNID other)
		{
			return this.Value.Equals(other.Value);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0001BD58 File Offset: 0x00019F58
		public int CompareTo(JET_COLUMNID other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0001BD6C File Offset: 0x00019F6C
		internal static JET_COLUMNID CreateColumnidFromNativeValue(int nativeValue)
		{
			return new JET_COLUMNID
			{
				Value = (uint)nativeValue
			};
		}

		// Token: 0x0400093C RID: 2364
		internal uint Value;
	}
}
