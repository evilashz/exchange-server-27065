using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007BD RID: 1981
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapPositiveInteger : ISoapXsd
	{
		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x0600566C RID: 22124 RVA: 0x001315A1 File Offset: 0x0012F7A1
		public static string XsdType
		{
			get
			{
				return "positiveInteger";
			}
		}

		// Token: 0x0600566D RID: 22125 RVA: 0x001315A8 File Offset: 0x0012F7A8
		public string GetXsdType()
		{
			return SoapPositiveInteger.XsdType;
		}

		// Token: 0x0600566E RID: 22126 RVA: 0x001315AF File Offset: 0x0012F7AF
		public SoapPositiveInteger()
		{
		}

		// Token: 0x0600566F RID: 22127 RVA: 0x001315B8 File Offset: 0x0012F7B8
		public SoapPositiveInteger(decimal value)
		{
			this._value = decimal.Truncate(value);
			if (this._value < 1m)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "xsd:positiveInteger", value));
			}
		}

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x06005670 RID: 22128 RVA: 0x0013160E File Offset: 0x0012F80E
		// (set) Token: 0x06005671 RID: 22129 RVA: 0x00131618 File Offset: 0x0012F818
		public decimal Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = decimal.Truncate(value);
				if (this._value < 1m)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "xsd:positiveInteger", value));
				}
			}
		}

		// Token: 0x06005672 RID: 22130 RVA: 0x00131668 File Offset: 0x0012F868
		public override string ToString()
		{
			return this._value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06005673 RID: 22131 RVA: 0x0013167A File Offset: 0x0012F87A
		public static SoapPositiveInteger Parse(string value)
		{
			return new SoapPositiveInteger(decimal.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture));
		}

		// Token: 0x04002760 RID: 10080
		private decimal _value;
	}
}
