using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007CB RID: 1995
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNmtokens : ISoapXsd
	{
		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x060056E4 RID: 22244 RVA: 0x00131E3D File Offset: 0x0013003D
		public static string XsdType
		{
			get
			{
				return "NMTOKENS";
			}
		}

		// Token: 0x060056E5 RID: 22245 RVA: 0x00131E44 File Offset: 0x00130044
		public string GetXsdType()
		{
			return SoapNmtokens.XsdType;
		}

		// Token: 0x060056E6 RID: 22246 RVA: 0x00131E4B File Offset: 0x0013004B
		public SoapNmtokens()
		{
		}

		// Token: 0x060056E7 RID: 22247 RVA: 0x00131E53 File Offset: 0x00130053
		public SoapNmtokens(string value)
		{
			this._value = value;
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x060056E8 RID: 22248 RVA: 0x00131E62 File Offset: 0x00130062
		// (set) Token: 0x060056E9 RID: 22249 RVA: 0x00131E6A File Offset: 0x0013006A
		public string Value
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

		// Token: 0x060056EA RID: 22250 RVA: 0x00131E73 File Offset: 0x00130073
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x060056EB RID: 22251 RVA: 0x00131E80 File Offset: 0x00130080
		public static SoapNmtokens Parse(string value)
		{
			return new SoapNmtokens(value);
		}

		// Token: 0x04002770 RID: 10096
		private string _value;
	}
}
