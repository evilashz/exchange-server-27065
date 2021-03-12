using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007C0 RID: 1984
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNegativeInteger : ISoapXsd
	{
		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06005684 RID: 22148 RVA: 0x00131865 File Offset: 0x0012FA65
		public static string XsdType
		{
			get
			{
				return "negativeInteger";
			}
		}

		// Token: 0x06005685 RID: 22149 RVA: 0x0013186C File Offset: 0x0012FA6C
		public string GetXsdType()
		{
			return SoapNegativeInteger.XsdType;
		}

		// Token: 0x06005686 RID: 22150 RVA: 0x00131873 File Offset: 0x0012FA73
		public SoapNegativeInteger()
		{
		}

		// Token: 0x06005687 RID: 22151 RVA: 0x0013187C File Offset: 0x0012FA7C
		public SoapNegativeInteger(decimal value)
		{
			this._value = decimal.Truncate(value);
			if (value > -1m)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "xsd:negativeInteger", value));
			}
		}

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06005688 RID: 22152 RVA: 0x001318CD File Offset: 0x0012FACD
		// (set) Token: 0x06005689 RID: 22153 RVA: 0x001318D8 File Offset: 0x0012FAD8
		public decimal Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = decimal.Truncate(value);
				if (this._value > -1m)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "xsd:negativeInteger", value));
				}
			}
		}

		// Token: 0x0600568A RID: 22154 RVA: 0x00131928 File Offset: 0x0012FB28
		public override string ToString()
		{
			return this._value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600568B RID: 22155 RVA: 0x0013193A File Offset: 0x0012FB3A
		public static SoapNegativeInteger Parse(string value)
		{
			return new SoapNegativeInteger(decimal.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture));
		}

		// Token: 0x04002763 RID: 10083
		private decimal _value;
	}
}
