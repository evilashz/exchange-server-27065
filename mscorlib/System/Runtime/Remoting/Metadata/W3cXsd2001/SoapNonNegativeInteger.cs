using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007BF RID: 1983
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNonNegativeInteger : ISoapXsd
	{
		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x0600567C RID: 22140 RVA: 0x00131779 File Offset: 0x0012F979
		public static string XsdType
		{
			get
			{
				return "nonNegativeInteger";
			}
		}

		// Token: 0x0600567D RID: 22141 RVA: 0x00131780 File Offset: 0x0012F980
		public string GetXsdType()
		{
			return SoapNonNegativeInteger.XsdType;
		}

		// Token: 0x0600567E RID: 22142 RVA: 0x00131787 File Offset: 0x0012F987
		public SoapNonNegativeInteger()
		{
		}

		// Token: 0x0600567F RID: 22143 RVA: 0x00131790 File Offset: 0x0012F990
		public SoapNonNegativeInteger(decimal value)
		{
			this._value = decimal.Truncate(value);
			if (this._value < 0m)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "xsd:nonNegativeInteger", value));
			}
		}

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06005680 RID: 22144 RVA: 0x001317E6 File Offset: 0x0012F9E6
		// (set) Token: 0x06005681 RID: 22145 RVA: 0x001317F0 File Offset: 0x0012F9F0
		public decimal Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = decimal.Truncate(value);
				if (this._value < 0m)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "xsd:nonNegativeInteger", value));
				}
			}
		}

		// Token: 0x06005682 RID: 22146 RVA: 0x00131840 File Offset: 0x0012FA40
		public override string ToString()
		{
			return this._value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06005683 RID: 22147 RVA: 0x00131852 File Offset: 0x0012FA52
		public static SoapNonNegativeInteger Parse(string value)
		{
			return new SoapNonNegativeInteger(decimal.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture));
		}

		// Token: 0x04002762 RID: 10082
		private decimal _value;
	}
}
