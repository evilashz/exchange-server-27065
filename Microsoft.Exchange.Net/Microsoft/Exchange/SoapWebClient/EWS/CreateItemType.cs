using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200047A RID: 1146
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class CreateItemType : BaseRequestType
	{
		// Token: 0x0400178D RID: 6029
		public TargetFolderIdType SavedItemFolderId;

		// Token: 0x0400178E RID: 6030
		public NonEmptyArrayOfAllItemsType Items;

		// Token: 0x0400178F RID: 6031
		[XmlAttribute]
		public MessageDispositionType MessageDisposition;

		// Token: 0x04001790 RID: 6032
		[XmlIgnore]
		public bool MessageDispositionSpecified;

		// Token: 0x04001791 RID: 6033
		[XmlAttribute]
		public CalendarItemCreateOrDeleteOperationType SendMeetingInvitations;

		// Token: 0x04001792 RID: 6034
		[XmlIgnore]
		public bool SendMeetingInvitationsSpecified;
	}
}
