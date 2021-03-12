using System;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002F0 RID: 752
	public struct JET_SESID : IEquatable<JET_SESID>, IFormattable
	{
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x0001B8B0 File Offset: 0x00019AB0
		public static JET_SESID Nil
		{
			[DebuggerStepThrough]
			get
			{
				return default(JET_SESID);
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x0001B8C6 File Offset: 0x00019AC6
		public bool IsInvalid
		{
			get
			{
				return this.Value == IntPtr.Zero || this.Value == new IntPtr(-1);
			}
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0001B8ED File Offset: 0x00019AED
		public static bool operator ==(JET_SESID lhs, JET_SESID rhs)
		{
			return lhs.Value == rhs.Value;
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0001B902 File Offset: 0x00019B02
		public static bool operator !=(JET_SESID lhs, JET_SESID rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0001B910 File Offset: 0x00019B10
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_SESID(0x{0:x})", new object[]
			{
				this.Value.ToInt64()
			});
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0001B948 File Offset: 0x00019B48
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (!string.IsNullOrEmpty(format) && !("G" == format))
			{
				return this.Value.ToInt64().ToString(format, formatProvider);
			}
			return this.ToString();
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0001B98C File Offset: 0x00019B8C
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((JET_SESID)obj);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0001B9BC File Offset: 0x00019BBC
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0001B9CF File Offset: 0x00019BCF
		public bool Equals(JET_SESID other)
		{
			return this.Value.Equals(other.Value);
		}

		// Token: 0x04000939 RID: 2361
		internal IntPtr Value;
	}
}
