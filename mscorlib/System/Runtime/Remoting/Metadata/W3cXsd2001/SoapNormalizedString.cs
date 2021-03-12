using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007C4 RID: 1988
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNormalizedString : ISoapXsd
	{
		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x060056AA RID: 22186 RVA: 0x00131ADF File Offset: 0x0012FCDF
		public static string XsdType
		{
			get
			{
				return "normalizedString";
			}
		}

		// Token: 0x060056AB RID: 22187 RVA: 0x00131AE6 File Offset: 0x0012FCE6
		public string GetXsdType()
		{
			return SoapNormalizedString.XsdType;
		}

		// Token: 0x060056AC RID: 22188 RVA: 0x00131AED File Offset: 0x0012FCED
		public SoapNormalizedString()
		{
		}

		// Token: 0x060056AD RID: 22189 RVA: 0x00131AF5 File Offset: 0x0012FCF5
		public SoapNormalizedString(string value)
		{
			this._value = this.Validate(value);
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x060056AE RID: 22190 RVA: 0x00131B0A File Offset: 0x0012FD0A
		// (set) Token: 0x060056AF RID: 22191 RVA: 0x00131B12 File Offset: 0x0012FD12
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = this.Validate(value);
			}
		}

		// Token: 0x060056B0 RID: 22192 RVA: 0x00131B21 File Offset: 0x0012FD21
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x060056B1 RID: 22193 RVA: 0x00131B2E File Offset: 0x0012FD2E
		public static SoapNormalizedString Parse(string value)
		{
			return new SoapNormalizedString(value);
		}

		// Token: 0x060056B2 RID: 22194 RVA: 0x00131B38 File Offset: 0x0012FD38
		private string Validate(string value)
		{
			if (value == null || value.Length == 0)
			{
				return value;
			}
			char[] anyOf = new char[]
			{
				'\r',
				'\n',
				'\t'
			};
			int num = value.LastIndexOfAny(anyOf);
			if (num > -1)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid", new object[]
				{
					"xsd:normalizedString",
					value
				}));
			}
			return value;
		}

		// Token: 0x04002769 RID: 10089
		private string _value;
	}
}
