using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007CA RID: 1994
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNmtoken : ISoapXsd
	{
		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x060056DC RID: 22236 RVA: 0x00131DF2 File Offset: 0x0012FFF2
		public static string XsdType
		{
			get
			{
				return "NMTOKEN";
			}
		}

		// Token: 0x060056DD RID: 22237 RVA: 0x00131DF9 File Offset: 0x0012FFF9
		public string GetXsdType()
		{
			return SoapNmtoken.XsdType;
		}

		// Token: 0x060056DE RID: 22238 RVA: 0x00131E00 File Offset: 0x00130000
		public SoapNmtoken()
		{
		}

		// Token: 0x060056DF RID: 22239 RVA: 0x00131E08 File Offset: 0x00130008
		public SoapNmtoken(string value)
		{
			this._value = value;
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x060056E0 RID: 22240 RVA: 0x00131E17 File Offset: 0x00130017
		// (set) Token: 0x060056E1 RID: 22241 RVA: 0x00131E1F File Offset: 0x0013001F
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

		// Token: 0x060056E2 RID: 22242 RVA: 0x00131E28 File Offset: 0x00130028
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x060056E3 RID: 22243 RVA: 0x00131E35 File Offset: 0x00130035
		public static SoapNmtoken Parse(string value)
		{
			return new SoapNmtoken(value);
		}

		// Token: 0x0400276F RID: 10095
		private string _value;
	}
}
