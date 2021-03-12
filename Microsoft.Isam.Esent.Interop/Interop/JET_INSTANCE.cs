using System;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002EF RID: 751
	public struct JET_INSTANCE : IEquatable<JET_INSTANCE>, IFormattable
	{
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x0001B770 File Offset: 0x00019970
		public static JET_INSTANCE Nil
		{
			[DebuggerStepThrough]
			get
			{
				return default(JET_INSTANCE);
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x0001B786 File Offset: 0x00019986
		public bool IsInvalid
		{
			get
			{
				return this.Value == IntPtr.Zero || this.Value == new IntPtr(-1);
			}
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0001B7AD File Offset: 0x000199AD
		public static bool operator ==(JET_INSTANCE lhs, JET_INSTANCE rhs)
		{
			return lhs.Value == rhs.Value;
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0001B7C2 File Offset: 0x000199C2
		public static bool operator !=(JET_INSTANCE lhs, JET_INSTANCE rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0001B7D0 File Offset: 0x000199D0
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_INSTANCE(0x{0:x})", new object[]
			{
				this.Value.ToInt64()
			});
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0001B808 File Offset: 0x00019A08
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (!string.IsNullOrEmpty(format) && !("G" == format))
			{
				return this.Value.ToInt64().ToString(format, formatProvider);
			}
			return this.ToString();
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0001B84C File Offset: 0x00019A4C
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((JET_INSTANCE)obj);
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0001B87C File Offset: 0x00019A7C
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0001B88F File Offset: 0x00019A8F
		public bool Equals(JET_INSTANCE other)
		{
			return this.Value.Equals(other.Value);
		}

		// Token: 0x04000938 RID: 2360
		internal IntPtr Value;
	}
}
