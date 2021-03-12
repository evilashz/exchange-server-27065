using System;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002F6 RID: 758
	public struct JET_LS : IEquatable<JET_LS>, IFormattable
	{
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x0001C00A File Offset: 0x0001A20A
		public bool IsInvalid
		{
			get
			{
				return this.Value == IntPtr.Zero || this.Value == new IntPtr(-1);
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x0001C031 File Offset: 0x0001A231
		// (set) Token: 0x06000DF5 RID: 3573 RVA: 0x0001C039 File Offset: 0x0001A239
		public IntPtr Value { get; set; }

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0001C042 File Offset: 0x0001A242
		public static bool operator ==(JET_LS lhs, JET_LS rhs)
		{
			return lhs.Value == rhs.Value;
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0001C057 File Offset: 0x0001A257
		public static bool operator !=(JET_LS lhs, JET_LS rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0001C064 File Offset: 0x0001A264
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_LS(0x{0:x})", new object[]
			{
				this.Value.ToInt64()
			});
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0001C0A0 File Offset: 0x0001A2A0
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (!string.IsNullOrEmpty(format) && !("G" == format))
			{
				return this.Value.ToInt64().ToString(format, formatProvider);
			}
			return this.ToString();
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0001C0E7 File Offset: 0x0001A2E7
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((JET_LS)obj);
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0001C118 File Offset: 0x0001A318
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0001C13C File Offset: 0x0001A33C
		public bool Equals(JET_LS other)
		{
			return this.Value.Equals(other.Value);
		}

		// Token: 0x0400093F RID: 2367
		public static readonly JET_LS Nil = new JET_LS
		{
			Value = new IntPtr(-1)
		};
	}
}
