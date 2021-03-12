using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007B5 RID: 1973
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapYearMonth : ISoapXsd
	{
		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x0600561F RID: 22047 RVA: 0x00130EB0 File Offset: 0x0012F0B0
		public static string XsdType
		{
			get
			{
				return "gYearMonth";
			}
		}

		// Token: 0x06005620 RID: 22048 RVA: 0x00130EB7 File Offset: 0x0012F0B7
		public string GetXsdType()
		{
			return SoapYearMonth.XsdType;
		}

		// Token: 0x06005621 RID: 22049 RVA: 0x00130EBE File Offset: 0x0012F0BE
		public SoapYearMonth()
		{
		}

		// Token: 0x06005622 RID: 22050 RVA: 0x00130ED1 File Offset: 0x0012F0D1
		public SoapYearMonth(DateTime value)
		{
			this._value = value;
		}

		// Token: 0x06005623 RID: 22051 RVA: 0x00130EEB File Offset: 0x0012F0EB
		public SoapYearMonth(DateTime value, int sign)
		{
			this._value = value;
			this._sign = sign;
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x06005624 RID: 22052 RVA: 0x00130F0C File Offset: 0x0012F10C
		// (set) Token: 0x06005625 RID: 22053 RVA: 0x00130F14 File Offset: 0x0012F114
		public DateTime Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x06005626 RID: 22054 RVA: 0x00130F1D File Offset: 0x0012F11D
		// (set) Token: 0x06005627 RID: 22055 RVA: 0x00130F25 File Offset: 0x0012F125
		public int Sign
		{
			get
			{
				return this._sign;
			}
			set
			{
				this._sign = value;
			}
		}

		// Token: 0x06005628 RID: 22056 RVA: 0x00130F2E File Offset: 0x0012F12E
		public override string ToString()
		{
			if (this._sign < 0)
			{
				return this._value.ToString("'-'yyyy-MM", CultureInfo.InvariantCulture);
			}
			return this._value.ToString("yyyy-MM", CultureInfo.InvariantCulture);
		}

		// Token: 0x06005629 RID: 22057 RVA: 0x00130F64 File Offset: 0x0012F164
		public static SoapYearMonth Parse(string value)
		{
			int sign = 0;
			if (value[0] == '-')
			{
				sign = -1;
			}
			return new SoapYearMonth(DateTime.ParseExact(value, SoapYearMonth.formats, CultureInfo.InvariantCulture, DateTimeStyles.None), sign);
		}

		// Token: 0x04002750 RID: 10064
		private DateTime _value = DateTime.MinValue;

		// Token: 0x04002751 RID: 10065
		private int _sign;

		// Token: 0x04002752 RID: 10066
		private static string[] formats = new string[]
		{
			"yyyy-MM",
			"'+'yyyy-MM",
			"'-'yyyy-MM",
			"yyyy-MMzzz",
			"'+'yyyy-MMzzz",
			"'-'yyyy-MMzzz"
		};
	}
}
