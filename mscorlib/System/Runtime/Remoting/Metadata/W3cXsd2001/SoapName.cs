using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007C7 RID: 1991
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapName : ISoapXsd
	{
		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x060056C4 RID: 22212 RVA: 0x00131D11 File Offset: 0x0012FF11
		public static string XsdType
		{
			get
			{
				return "Name";
			}
		}

		// Token: 0x060056C5 RID: 22213 RVA: 0x00131D18 File Offset: 0x0012FF18
		public string GetXsdType()
		{
			return SoapName.XsdType;
		}

		// Token: 0x060056C6 RID: 22214 RVA: 0x00131D1F File Offset: 0x0012FF1F
		public SoapName()
		{
		}

		// Token: 0x060056C7 RID: 22215 RVA: 0x00131D27 File Offset: 0x0012FF27
		public SoapName(string value)
		{
			this._value = value;
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x060056C8 RID: 22216 RVA: 0x00131D36 File Offset: 0x0012FF36
		// (set) Token: 0x060056C9 RID: 22217 RVA: 0x00131D3E File Offset: 0x0012FF3E
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

		// Token: 0x060056CA RID: 22218 RVA: 0x00131D47 File Offset: 0x0012FF47
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x060056CB RID: 22219 RVA: 0x00131D54 File Offset: 0x0012FF54
		public static SoapName Parse(string value)
		{
			return new SoapName(value);
		}

		// Token: 0x0400276C RID: 10092
		private string _value;
	}
}
