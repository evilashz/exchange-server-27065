using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000471 RID: 1137
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class DeleteItemType : BaseRequestType
	{
		// Token: 0x04001760 RID: 5984
		[XmlArrayItem("RecurringMasterItemId", typeof(RecurringMasterItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("ItemId", typeof(ItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemIdRanges", typeof(RecurringMasterItemIdRangesType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("OccurrenceItemId", typeof(OccurrenceItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseItemIdType[] ItemIds;

		// Token: 0x04001761 RID: 5985
		[XmlAttribute]
		public DisposalType DeleteType;

		// Token: 0x04001762 RID: 5986
		[XmlAttribute]
		public CalendarItemCreateOrDeleteOperationType SendMeetingCancellations;

		// Token: 0x04001763 RID: 5987
		[XmlIgnore]
		public bool SendMeetingCancellationsSpecified;

		// Token: 0x04001764 RID: 5988
		[XmlAttribute]
		public AffectedTaskOccurrencesType AffectedTaskOccurrences;

		// Token: 0x04001765 RID: 5989
		[XmlIgnore]
		public bool AffectedTaskOccurrencesSpecified;

		// Token: 0x04001766 RID: 5990
		[XmlAttribute]
		public bool SuppressReadReceipts;

		// Token: 0x04001767 RID: 5991
		[XmlIgnore]
		public bool SuppressReadReceiptsSpecified;
	}
}
