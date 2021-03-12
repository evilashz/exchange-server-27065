using System;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002F5 RID: 757
	public struct JET_HANDLE : IEquatable<JET_HANDLE>, IFormattable
	{
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x0001BECC File Offset: 0x0001A0CC
		public static JET_HANDLE Nil
		{
			[DebuggerStepThrough]
			get
			{
				return default(JET_HANDLE);
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x0001BEE2 File Offset: 0x0001A0E2
		public bool IsInvalid
		{
			get
			{
				return this.Value == IntPtr.Zero || this.Value == new IntPtr(-1);
			}
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0001BF09 File Offset: 0x0001A109
		public static bool operator ==(JET_HANDLE lhs, JET_HANDLE rhs)
		{
			return lhs.Value == rhs.Value;
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0001BF1E File Offset: 0x0001A11E
		public static bool operator !=(JET_HANDLE lhs, JET_HANDLE rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0001BF2C File Offset: 0x0001A12C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_HANDLE(0x{0:x})", new object[]
			{
				this.Value.ToInt64()
			});
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0001BF64 File Offset: 0x0001A164
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (!string.IsNullOrEmpty(format) && !("G" == format))
			{
				return this.Value.ToInt64().ToString(format, formatProvider);
			}
			return this.ToString();
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0001BFA8 File Offset: 0x0001A1A8
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((JET_HANDLE)obj);
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0001BFD8 File Offset: 0x0001A1D8
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0001BFEB File Offset: 0x0001A1EB
		public bool Equals(JET_HANDLE other)
		{
			return this.Value.Equals(other.Value);
		}

		// Token: 0x0400093E RID: 2366
		internal IntPtr Value;
	}
}
