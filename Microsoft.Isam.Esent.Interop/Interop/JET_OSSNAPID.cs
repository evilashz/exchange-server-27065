using System;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002F4 RID: 756
	public struct JET_OSSNAPID : IEquatable<JET_OSSNAPID>, IFormattable
	{
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x0001BD8C File Offset: 0x00019F8C
		public static JET_OSSNAPID Nil
		{
			[DebuggerStepThrough]
			get
			{
				return default(JET_OSSNAPID);
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x0001BDA2 File Offset: 0x00019FA2
		public bool IsInvalid
		{
			get
			{
				return this.Value == IntPtr.Zero || this.Value == new IntPtr(-1);
			}
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0001BDC9 File Offset: 0x00019FC9
		public static bool operator ==(JET_OSSNAPID lhs, JET_OSSNAPID rhs)
		{
			return lhs.Value == rhs.Value;
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0001BDDE File Offset: 0x00019FDE
		public static bool operator !=(JET_OSSNAPID lhs, JET_OSSNAPID rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0001BDEC File Offset: 0x00019FEC
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_OSSNAPID(0x{0:x})", new object[]
			{
				this.Value.ToInt64()
			});
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0001BE24 File Offset: 0x0001A024
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (!string.IsNullOrEmpty(format) && !("G" == format))
			{
				return this.Value.ToInt64().ToString(format, formatProvider);
			}
			return this.ToString();
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x0001BE68 File Offset: 0x0001A068
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((JET_OSSNAPID)obj);
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0001BE98 File Offset: 0x0001A098
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0001BEAB File Offset: 0x0001A0AB
		public bool Equals(JET_OSSNAPID other)
		{
			return this.Value.Equals(other.Value);
		}

		// Token: 0x0400093D RID: 2365
		internal IntPtr Value;
	}
}
