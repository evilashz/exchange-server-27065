using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002E9 RID: 745
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class AggregateOnType
	{
		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06001926 RID: 6438 RVA: 0x000283F3 File Offset: 0x000265F3
		// (set) Token: 0x06001927 RID: 6439 RVA: 0x000283FB File Offset: 0x000265FB
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

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06001928 RID: 6440 RVA: 0x00028404 File Offset: 0x00026604
		// (set) Token: 0x06001929 RID: 6441 RVA: 0x0002840C File Offset: 0x0002660C
		[XmlAttribute]
		public AggregateType Aggregate
		{
			get
			{
				return this.aggregateField;
			}
			set
			{
				this.aggregateField = value;
			}
		}

		// Token: 0x0400110A RID: 4362
		private BasePathToElementType itemField;

		// Token: 0x0400110B RID: 4363
		private AggregateType aggregateField;
	}
}
