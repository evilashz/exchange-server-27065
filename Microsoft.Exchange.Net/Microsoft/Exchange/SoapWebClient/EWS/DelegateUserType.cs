using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002CB RID: 715
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class DelegateUserType
	{
		// Token: 0x04001222 RID: 4642
		public UserIdType UserId;

		// Token: 0x04001223 RID: 4643
		public DelegatePermissionsType DelegatePermissions;

		// Token: 0x04001224 RID: 4644
		public bool ReceiveCopiesOfMeetingMessages;

		// Token: 0x04001225 RID: 4645
		[XmlIgnore]
		public bool ReceiveCopiesOfMeetingMessagesSpecified;

		// Token: 0x04001226 RID: 4646
		public bool ViewPrivateItems;

		// Token: 0x04001227 RID: 4647
		[XmlIgnore]
		public bool ViewPrivateItemsSpecified;
	}
}
