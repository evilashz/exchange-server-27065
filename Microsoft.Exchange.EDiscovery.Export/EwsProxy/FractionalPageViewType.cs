using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000308 RID: 776
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class FractionalPageViewType : BasePagingType
	{
		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x060019AF RID: 6575 RVA: 0x00028872 File Offset: 0x00026A72
		// (set) Token: 0x060019B0 RID: 6576 RVA: 0x0002887A File Offset: 0x00026A7A
		[XmlAttribute]
		public int Numerator
		{
			get
			{
				return this.numeratorField;
			}
			set
			{
				this.numeratorField = value;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x00028883 File Offset: 0x00026A83
		// (set) Token: 0x060019B2 RID: 6578 RVA: 0x0002888B File Offset: 0x00026A8B
		[XmlAttribute]
		public int Denominator
		{
			get
			{
				return this.denominatorField;
			}
			set
			{
				this.denominatorField = value;
			}
		}

		// Token: 0x0400114C RID: 4428
		private int numeratorField;

		// Token: 0x0400114D RID: 4429
		private int denominatorField;
	}
}
