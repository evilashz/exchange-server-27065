using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007CC RID: 1996
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNcName : ISoapXsd
	{
		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x060056EC RID: 22252 RVA: 0x00131E88 File Offset: 0x00130088
		public static string XsdType
		{
			get
			{
				return "NCName";
			}
		}

		// Token: 0x060056ED RID: 22253 RVA: 0x00131E8F File Offset: 0x0013008F
		public string GetXsdType()
		{
			return SoapNcName.XsdType;
		}

		// Token: 0x060056EE RID: 22254 RVA: 0x00131E96 File Offset: 0x00130096
		public SoapNcName()
		{
		}

		// Token: 0x060056EF RID: 22255 RVA: 0x00131E9E File Offset: 0x0013009E
		public SoapNcName(string value)
		{
			this._value = value;
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x060056F0 RID: 22256 RVA: 0x00131EAD File Offset: 0x001300AD
		// (set) Token: 0x060056F1 RID: 22257 RVA: 0x00131EB5 File Offset: 0x001300B5
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

		// Token: 0x060056F2 RID: 22258 RVA: 0x00131EBE File Offset: 0x001300BE
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x060056F3 RID: 22259 RVA: 0x00131ECB File Offset: 0x001300CB
		public static SoapNcName Parse(string value)
		{
			return new SoapNcName(value);
		}

		// Token: 0x04002771 RID: 10097
		private string _value;
	}
}
