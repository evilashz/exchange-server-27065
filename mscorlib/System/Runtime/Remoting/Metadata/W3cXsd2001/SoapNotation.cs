using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007C3 RID: 1987
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNotation : ISoapXsd
	{
		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x060056A2 RID: 22178 RVA: 0x00131A99 File Offset: 0x0012FC99
		public static string XsdType
		{
			get
			{
				return "NOTATION";
			}
		}

		// Token: 0x060056A3 RID: 22179 RVA: 0x00131AA0 File Offset: 0x0012FCA0
		public string GetXsdType()
		{
			return SoapNotation.XsdType;
		}

		// Token: 0x060056A4 RID: 22180 RVA: 0x00131AA7 File Offset: 0x0012FCA7
		public SoapNotation()
		{
		}

		// Token: 0x060056A5 RID: 22181 RVA: 0x00131AAF File Offset: 0x0012FCAF
		public SoapNotation(string value)
		{
			this._value = value;
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x060056A6 RID: 22182 RVA: 0x00131ABE File Offset: 0x0012FCBE
		// (set) Token: 0x060056A7 RID: 22183 RVA: 0x00131AC6 File Offset: 0x0012FCC6
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

		// Token: 0x060056A8 RID: 22184 RVA: 0x00131ACF File Offset: 0x0012FCCF
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x060056A9 RID: 22185 RVA: 0x00131AD7 File Offset: 0x0012FCD7
		public static SoapNotation Parse(string value)
		{
			return new SoapNotation(value);
		}

		// Token: 0x04002768 RID: 10088
		private string _value;
	}
}
