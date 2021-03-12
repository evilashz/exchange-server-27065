using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007BE RID: 1982
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNonPositiveInteger : ISoapXsd
	{
		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06005674 RID: 22132 RVA: 0x0013168D File Offset: 0x0012F88D
		public static string XsdType
		{
			get
			{
				return "nonPositiveInteger";
			}
		}

		// Token: 0x06005675 RID: 22133 RVA: 0x00131694 File Offset: 0x0012F894
		public string GetXsdType()
		{
			return SoapNonPositiveInteger.XsdType;
		}

		// Token: 0x06005676 RID: 22134 RVA: 0x0013169B File Offset: 0x0012F89B
		public SoapNonPositiveInteger()
		{
		}

		// Token: 0x06005677 RID: 22135 RVA: 0x001316A4 File Offset: 0x0012F8A4
		public SoapNonPositiveInteger(decimal value)
		{
			this._value = decimal.Truncate(value);
			if (this._value > 0m)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "xsd:nonPositiveInteger", value));
			}
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06005678 RID: 22136 RVA: 0x001316FA File Offset: 0x0012F8FA
		// (set) Token: 0x06005679 RID: 22137 RVA: 0x00131704 File Offset: 0x0012F904
		public decimal Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = decimal.Truncate(value);
				if (this._value > 0m)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "xsd:nonPositiveInteger", value));
				}
			}
		}

		// Token: 0x0600567A RID: 22138 RVA: 0x00131754 File Offset: 0x0012F954
		public override string ToString()
		{
			return this._value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600567B RID: 22139 RVA: 0x00131766 File Offset: 0x0012F966
		public static SoapNonPositiveInteger Parse(string value)
		{
			return new SoapNonPositiveInteger(decimal.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture));
		}

		// Token: 0x04002761 RID: 10081
		private decimal _value;
	}
}
