using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007C5 RID: 1989
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapToken : ISoapXsd
	{
		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x060056B3 RID: 22195 RVA: 0x00131B93 File Offset: 0x0012FD93
		public static string XsdType
		{
			get
			{
				return "token";
			}
		}

		// Token: 0x060056B4 RID: 22196 RVA: 0x00131B9A File Offset: 0x0012FD9A
		public string GetXsdType()
		{
			return SoapToken.XsdType;
		}

		// Token: 0x060056B5 RID: 22197 RVA: 0x00131BA1 File Offset: 0x0012FDA1
		public SoapToken()
		{
		}

		// Token: 0x060056B6 RID: 22198 RVA: 0x00131BA9 File Offset: 0x0012FDA9
		public SoapToken(string value)
		{
			this._value = this.Validate(value);
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x060056B7 RID: 22199 RVA: 0x00131BBE File Offset: 0x0012FDBE
		// (set) Token: 0x060056B8 RID: 22200 RVA: 0x00131BC6 File Offset: 0x0012FDC6
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

		// Token: 0x060056B9 RID: 22201 RVA: 0x00131BD5 File Offset: 0x0012FDD5
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x060056BA RID: 22202 RVA: 0x00131BE2 File Offset: 0x0012FDE2
		public static SoapToken Parse(string value)
		{
			return new SoapToken(value);
		}

		// Token: 0x060056BB RID: 22203 RVA: 0x00131BEC File Offset: 0x0012FDEC
		private string Validate(string value)
		{
			if (value == null || value.Length == 0)
			{
				return value;
			}
			char[] anyOf = new char[]
			{
				'\r',
				'\t'
			};
			int num = value.LastIndexOfAny(anyOf);
			if (num > -1)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid", new object[]
				{
					"xsd:token",
					value
				}));
			}
			if (value.Length > 0 && (char.IsWhiteSpace(value[0]) || char.IsWhiteSpace(value[value.Length - 1])))
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid", new object[]
				{
					"xsd:token",
					value
				}));
			}
			num = value.IndexOf("  ");
			if (num > -1)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid", new object[]
				{
					"xsd:token",
					value
				}));
			}
			return value;
		}

		// Token: 0x0400276A RID: 10090
		private string _value;
	}
}
