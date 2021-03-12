using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007BC RID: 1980
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapInteger : ISoapXsd
	{
		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06005664 RID: 22116 RVA: 0x0013153C File Offset: 0x0012F73C
		public static string XsdType
		{
			get
			{
				return "integer";
			}
		}

		// Token: 0x06005665 RID: 22117 RVA: 0x00131543 File Offset: 0x0012F743
		public string GetXsdType()
		{
			return SoapInteger.XsdType;
		}

		// Token: 0x06005666 RID: 22118 RVA: 0x0013154A File Offset: 0x0012F74A
		public SoapInteger()
		{
		}

		// Token: 0x06005667 RID: 22119 RVA: 0x00131552 File Offset: 0x0012F752
		public SoapInteger(decimal value)
		{
			this._value = decimal.Truncate(value);
		}

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x06005668 RID: 22120 RVA: 0x00131566 File Offset: 0x0012F766
		// (set) Token: 0x06005669 RID: 22121 RVA: 0x0013156E File Offset: 0x0012F76E
		public decimal Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = decimal.Truncate(value);
			}
		}

		// Token: 0x0600566A RID: 22122 RVA: 0x0013157C File Offset: 0x0012F77C
		public override string ToString()
		{
			return this._value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600566B RID: 22123 RVA: 0x0013158E File Offset: 0x0012F78E
		public static SoapInteger Parse(string value)
		{
			return new SoapInteger(decimal.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture));
		}

		// Token: 0x0400275F RID: 10079
		private decimal _value;
	}
}
