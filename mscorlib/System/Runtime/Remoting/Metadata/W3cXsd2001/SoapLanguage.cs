using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007C6 RID: 1990
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapLanguage : ISoapXsd
	{
		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x060056BC RID: 22204 RVA: 0x00131CC6 File Offset: 0x0012FEC6
		public static string XsdType
		{
			get
			{
				return "language";
			}
		}

		// Token: 0x060056BD RID: 22205 RVA: 0x00131CCD File Offset: 0x0012FECD
		public string GetXsdType()
		{
			return SoapLanguage.XsdType;
		}

		// Token: 0x060056BE RID: 22206 RVA: 0x00131CD4 File Offset: 0x0012FED4
		public SoapLanguage()
		{
		}

		// Token: 0x060056BF RID: 22207 RVA: 0x00131CDC File Offset: 0x0012FEDC
		public SoapLanguage(string value)
		{
			this._value = value;
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x060056C0 RID: 22208 RVA: 0x00131CEB File Offset: 0x0012FEEB
		// (set) Token: 0x060056C1 RID: 22209 RVA: 0x00131CF3 File Offset: 0x0012FEF3
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

		// Token: 0x060056C2 RID: 22210 RVA: 0x00131CFC File Offset: 0x0012FEFC
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x060056C3 RID: 22211 RVA: 0x00131D09 File Offset: 0x0012FF09
		public static SoapLanguage Parse(string value)
		{
			return new SoapLanguage(value);
		}

		// Token: 0x0400276B RID: 10091
		private string _value;
	}
}
