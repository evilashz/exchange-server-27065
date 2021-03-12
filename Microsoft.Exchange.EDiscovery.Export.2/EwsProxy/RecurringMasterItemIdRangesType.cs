using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000B9 RID: 185
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class RecurringMasterItemIdRangesType : ItemIdType
	{
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x0001FE20 File Offset: 0x0001E020
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x0001FE28 File Offset: 0x0001E028
		[XmlArrayItem("Range", IsNullable = false)]
		public OccurrencesRangeType[] Ranges
		{
			get
			{
				return this.rangesField;
			}
			set
			{
				this.rangesField = value;
			}
		}

		// Token: 0x0400055C RID: 1372
		private OccurrencesRangeType[] rangesField;
	}
}
