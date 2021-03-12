using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002E7 RID: 743
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class FieldOrderType
	{
		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x000283C9 File Offset: 0x000265C9
		// (set) Token: 0x06001922 RID: 6434 RVA: 0x000283D1 File Offset: 0x000265D1
		[XmlElement("FieldURI", typeof(PathToUnindexedFieldType))]
		[XmlElement("IndexedFieldURI", typeof(PathToIndexedFieldType))]
		[XmlElement("ExtendedFieldURI", typeof(PathToExtendedFieldType))]
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

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06001923 RID: 6435 RVA: 0x000283DA File Offset: 0x000265DA
		// (set) Token: 0x06001924 RID: 6436 RVA: 0x000283E2 File Offset: 0x000265E2
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

		// Token: 0x04001105 RID: 4357
		private BasePathToElementType itemField;

		// Token: 0x04001106 RID: 4358
		private SortDirectionType orderField;
	}
}
