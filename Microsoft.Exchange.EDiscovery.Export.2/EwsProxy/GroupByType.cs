using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002EE RID: 750
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GroupByType : BaseGroupByType
	{
		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06001931 RID: 6449 RVA: 0x0002844F File Offset: 0x0002664F
		// (set) Token: 0x06001932 RID: 6450 RVA: 0x00028457 File Offset: 0x00026657
		[XmlElement("ExtendedFieldURI", typeof(PathToExtendedFieldType))]
		[XmlElement("FieldURI", typeof(PathToUnindexedFieldType))]
		[XmlElement("IndexedFieldURI", typeof(PathToIndexedFieldType))]
		public BasePathToElementType Item
		{
			get
			{
				return this.itemField;
			}
			set
			{
				this.itemField = value;
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06001933 RID: 6451 RVA: 0x00028460 File Offset: 0x00026660
		// (set) Token: 0x06001934 RID: 6452 RVA: 0x00028468 File Offset: 0x00026668
		public AggregateOnType AggregateOn
		{
			get
			{
				return this.aggregateOnField;
			}
			set
			{
				this.aggregateOnField = value;
			}
		}

		// Token: 0x04001113 RID: 4371
		private BasePathToElementType itemField;

		// Token: 0x04001114 RID: 4372
		private AggregateOnType aggregateOnField;
	}
}
