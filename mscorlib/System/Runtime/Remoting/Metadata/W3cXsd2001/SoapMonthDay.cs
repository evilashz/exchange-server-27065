using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007B7 RID: 1975
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapMonthDay : ISoapXsd
	{
		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06005637 RID: 22071 RVA: 0x001310F8 File Offset: 0x0012F2F8
		public static string XsdType
		{
			get
			{
				return "gMonthDay";
			}
		}

		// Token: 0x06005638 RID: 22072 RVA: 0x001310FF File Offset: 0x0012F2FF
		public string GetXsdType()
		{
			return SoapMonthDay.XsdType;
		}

		// Token: 0x06005639 RID: 22073 RVA: 0x00131106 File Offset: 0x0012F306
		public SoapMonthDay()
		{
		}

		// Token: 0x0600563A RID: 22074 RVA: 0x00131119 File Offset: 0x0012F319
		public SoapMonthDay(DateTime value)
		{
			this._value = value;
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x0600563B RID: 22075 RVA: 0x00131133 File Offset: 0x0012F333
		// (set) Token: 0x0600563C RID: 22076 RVA: 0x0013113B File Offset: 0x0012F33B
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

		// Token: 0x0600563D RID: 22077 RVA: 0x00131144 File Offset: 0x0012F344
		public override string ToString()
		{
			return this._value.ToString("'--'MM'-'dd", CultureInfo.InvariantCulture);
		}

		// Token: 0x0600563E RID: 22078 RVA: 0x0013115B File Offset: 0x0012F35B
		public static SoapMonthDay Parse(string value)
		{
			return new SoapMonthDay(DateTime.ParseExact(value, SoapMonthDay.formats, CultureInfo.InvariantCulture, DateTimeStyles.None));
		}

		// Token: 0x04002756 RID: 10070
		private DateTime _value = DateTime.MinValue;

		// Token: 0x04002757 RID: 10071
		private static string[] formats = new string[]
		{
			"--MM-dd",
			"--MM-ddzzz"
		};
	}
}
