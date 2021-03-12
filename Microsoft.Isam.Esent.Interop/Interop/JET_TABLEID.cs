using System;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002F1 RID: 753
	public struct JET_TABLEID : IEquatable<JET_TABLEID>, IFormattable
	{
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x0001B9F0 File Offset: 0x00019BF0
		public static JET_TABLEID Nil
		{
			[DebuggerStepThrough]
			get
			{
				return default(JET_TABLEID);
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x0001BA06 File Offset: 0x00019C06
		public bool IsInvalid
		{
			get
			{
				return this.Value == IntPtr.Zero || this.Value == new IntPtr(-1);
			}
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0001BA2D File Offset: 0x00019C2D
		public static bool operator ==(JET_TABLEID lhs, JET_TABLEID rhs)
		{
			return lhs.Value == rhs.Value;
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0001BA42 File Offset: 0x00019C42
		public static bool operator !=(JET_TABLEID lhs, JET_TABLEID rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0001BA50 File Offset: 0x00019C50
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_TABLEID(0x{0:x})", new object[]
			{
				this.Value.ToInt64()
			});
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0001BA88 File Offset: 0x00019C88
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (!string.IsNullOrEmpty(format) && !("G" == format))
			{
				return this.Value.ToInt64().ToString(format, formatProvider);
			}
			return this.ToString();
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0001BACC File Offset: 0x00019CCC
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((JET_TABLEID)obj);
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0001BAFC File Offset: 0x00019CFC
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0001BB0F File Offset: 0x00019D0F
		public bool Equals(JET_TABLEID other)
		{
			return this.Value.Equals(other.Value);
		}

		// Token: 0x0400093A RID: 2362
		internal IntPtr Value;
	}
}
