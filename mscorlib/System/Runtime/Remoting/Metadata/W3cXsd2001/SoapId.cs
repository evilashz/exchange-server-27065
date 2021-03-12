using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007CD RID: 1997
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapId : ISoapXsd
	{
		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x060056F4 RID: 22260 RVA: 0x00131ED3 File Offset: 0x001300D3
		public static string XsdType
		{
			get
			{
				return "ID";
			}
		}

		// Token: 0x060056F5 RID: 22261 RVA: 0x00131EDA File Offset: 0x001300DA
		public string GetXsdType()
		{
			return SoapId.XsdType;
		}

		// Token: 0x060056F6 RID: 22262 RVA: 0x00131EE1 File Offset: 0x001300E1
		public SoapId()
		{
		}

		// Token: 0x060056F7 RID: 22263 RVA: 0x00131EE9 File Offset: 0x001300E9
		public SoapId(string value)
		{
			this._value = value;
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x060056F8 RID: 22264 RVA: 0x00131EF8 File Offset: 0x001300F8
		// (set) Token: 0x060056F9 RID: 22265 RVA: 0x00131F00 File Offset: 0x00130100
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

		// Token: 0x060056FA RID: 22266 RVA: 0x00131F09 File Offset: 0x00130109
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x060056FB RID: 22267 RVA: 0x00131F16 File Offset: 0x00130116
		public static SoapId Parse(string value)
		{
			return new SoapId(value);
		}

		// Token: 0x04002772 RID: 10098
		private string _value;
	}
}
