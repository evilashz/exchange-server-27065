using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002EB RID: 747
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(DistinguishedGroupByType))]
	[XmlInclude(typeof(GroupByType))]
	[Serializable]
	public abstract class BaseGroupByType
	{
		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x0600192B RID: 6443 RVA: 0x0002841D File Offset: 0x0002661D
		// (set) Token: 0x0600192C RID: 6444 RVA: 0x00028425 File Offset: 0x00026625
		[XmlAttribute]
		public SortDirectionType Order
		{
			get
			{
				return this.orderField;
			}
			set
			{
				this.orderField = value;
			}
		}

		// Token: 0x0400110F RID: 4367
		private SortDirectionType orderField;
	}
}
