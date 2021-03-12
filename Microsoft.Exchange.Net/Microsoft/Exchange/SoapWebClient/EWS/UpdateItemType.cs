using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000475 RID: 1141
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class UpdateItemType : BaseRequestType
	{
		// Token: 0x04001774 RID: 6004
		public TargetFolderIdType SavedItemFolderId;

		// Token: 0x04001775 RID: 6005
		[XmlArrayItem("ItemChange", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ItemChangeType[] ItemChanges;

		// Token: 0x04001776 RID: 6006
		[XmlAttribute]
		public ConflictResolutionType ConflictResolution;

		// Token: 0x04001777 RID: 6007
		[XmlAttribute]
		public MessageDispositionType MessageDisposition;

		// Token: 0x04001778 RID: 6008
		[XmlIgnore]
		public bool MessageDispositionSpecified;

		// Token: 0x04001779 RID: 6009
		[XmlAttribute]
		public CalendarItemUpdateOperationType SendMeetingInvitationsOrCancellations;

		// Token: 0x0400177A RID: 6010
		[XmlIgnore]
		public bool SendMeetingInvitationsOrCancellationsSpecified;

		// Token: 0x0400177B RID: 6011
		[XmlAttribute]
		public bool SuppressReadReceipts;

		// Token: 0x0400177C RID: 6012
		[XmlIgnore]
		public bool SuppressReadReceiptsSpecified;
	}
}
