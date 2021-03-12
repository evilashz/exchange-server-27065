using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003D0 RID: 976
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ItemChangeType
	{
		// Token: 0x04001567 RID: 5479
		[XmlElement("OccurrenceItemId", typeof(OccurrenceItemIdType))]
		[XmlElement("ItemId", typeof(ItemIdType))]
		[XmlElement("RecurringMasterItemId", typeof(RecurringMasterItemIdType))]
		public BaseItemIdType Item;

		// Token: 0x04001568 RID: 5480
		[XmlArrayItem("AppendToItemField", typeof(AppendToItemFieldType), IsNullable = false)]
		[XmlArrayItem("SetItemField", typeof(SetItemFieldType), IsNullable = false)]
		[XmlArrayItem("DeleteItemField", typeof(DeleteItemFieldType), IsNullable = false)]
		public ItemChangeDescriptionType[] Updates;
	}
}
