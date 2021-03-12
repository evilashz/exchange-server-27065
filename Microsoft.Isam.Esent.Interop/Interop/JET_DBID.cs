using System;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002F2 RID: 754
	public struct JET_DBID : IEquatable<JET_DBID>, IFormattable
	{
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x0001BB30 File Offset: 0x00019D30
		public static JET_DBID Nil
		{
			get
			{
				return new JET_DBID
				{
					Value = uint.MaxValue
				};
			}
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0001BB4E File Offset: 0x00019D4E
		public static bool operator ==(JET_DBID lhs, JET_DBID rhs)
		{
			return lhs.Value == rhs.Value;
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0001BB60 File Offset: 0x00019D60
		public static bool operator !=(JET_DBID lhs, JET_DBID rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0001BB6C File Offset: 0x00019D6C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_DBID({0})", new object[]
			{
				this.Value
			});
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0001BB9E File Offset: 0x00019D9E
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (!string.IsNullOrEmpty(format) && !("G" == format))
			{
				return this.Value.ToString(format, formatProvider);
			}
			return this.ToString();
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0001BBCF File Offset: 0x00019DCF
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((JET_DBID)obj);
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0001BBFF File Offset: 0x00019DFF
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0001BC0C File Offset: 0x00019E0C
		public bool Equals(JET_DBID other)
		{
			return this.Value.Equals(other.Value);
		}

		// Token: 0x0400093B RID: 2363
		internal uint Value;
	}
}
