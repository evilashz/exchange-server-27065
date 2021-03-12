using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002EC RID: 748
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DistinguishedGroupByType : BaseGroupByType
	{
		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x0600192E RID: 6446 RVA: 0x00028436 File Offset: 0x00026636
		// (set) Token: 0x0600192F RID: 6447 RVA: 0x0002843E File Offset: 0x0002663E
		public StandardGroupByType StandardGroupBy
		{
			get
			{
				return this.standardGroupByField;
			}
			set
			{
				this.standardGroupByField = value;
			}
		}

		// Token: 0x04001110 RID: 4368
		private StandardGroupByType standardGroupByField;
	}
}
