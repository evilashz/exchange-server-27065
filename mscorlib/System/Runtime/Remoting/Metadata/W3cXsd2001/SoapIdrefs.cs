using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007C8 RID: 1992
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapIdrefs : ISoapXsd
	{
		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x060056CC RID: 22220 RVA: 0x00131D5C File Offset: 0x0012FF5C
		public static string XsdType
		{
			get
			{
				return "IDREFS";
			}
		}

		// Token: 0x060056CD RID: 22221 RVA: 0x00131D63 File Offset: 0x0012FF63
		public string GetXsdType()
		{
			return SoapIdrefs.XsdType;
		}

		// Token: 0x060056CE RID: 22222 RVA: 0x00131D6A File Offset: 0x0012FF6A
		public SoapIdrefs()
		{
		}

		// Token: 0x060056CF RID: 22223 RVA: 0x00131D72 File Offset: 0x0012FF72
		public SoapIdrefs(string value)
		{
			this._value = value;
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x060056D0 RID: 22224 RVA: 0x00131D81 File Offset: 0x0012FF81
		// (set) Token: 0x060056D1 RID: 22225 RVA: 0x00131D89 File Offset: 0x0012FF89
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

		// Token: 0x060056D2 RID: 22226 RVA: 0x00131D92 File Offset: 0x0012FF92
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x060056D3 RID: 22227 RVA: 0x00131D9F File Offset: 0x0012FF9F
		public static SoapIdrefs Parse(string value)
		{
			return new SoapIdrefs(value);
		}

		// Token: 0x0400276D RID: 10093
		private string _value;
	}
}
