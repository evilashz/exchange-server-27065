using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200046E RID: 1134
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[XmlInclude(typeof(MoveItemType))]
	[XmlInclude(typeof(CopyItemType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class BaseMoveCopyItemType : BaseRequestType
	{
		// Token: 0x0400175C RID: 5980
		public TargetFolderIdType ToFolderId;

		// Token: 0x0400175D RID: 5981
		[XmlArrayItem("OccurrenceItemId", typeof(OccurrenceItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemIdRanges", typeof(RecurringMasterItemIdRangesType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("ItemId", typeof(ItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemId", typeof(RecurringMasterItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseItemIdType[] ItemIds;

		// Token: 0x0400175E RID: 5982
		public bool ReturnNewItemIds;

		// Token: 0x0400175F RID: 5983
		[XmlIgnore]
		public bool ReturnNewItemIdsSpecified;
	}
}
