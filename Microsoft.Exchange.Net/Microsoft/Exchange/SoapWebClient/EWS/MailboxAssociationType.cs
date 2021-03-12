using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003BF RID: 959
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class MailboxAssociationType
	{
		// Token: 0x04001519 RID: 5401
		public GroupLocatorType Group;

		// Token: 0x0400151A RID: 5402
		public UserLocatorType User;

		// Token: 0x0400151B RID: 5403
		public bool IsMember;

		// Token: 0x0400151C RID: 5404
		[XmlIgnore]
		public bool IsMemberSpecified;

		// Token: 0x0400151D RID: 5405
		public DateTime JoinDate;

		// Token: 0x0400151E RID: 5406
		[XmlIgnore]
		public bool JoinDateSpecified;

		// Token: 0x0400151F RID: 5407
		public bool IsPin;

		// Token: 0x04001520 RID: 5408
		[XmlIgnore]
		public bool IsPinSpecified;

		// Token: 0x04001521 RID: 5409
		public string JoinedBy;
	}
}
