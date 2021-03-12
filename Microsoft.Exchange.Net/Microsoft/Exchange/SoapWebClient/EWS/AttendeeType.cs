using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200020F RID: 527
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class AttendeeType
	{
		// Token: 0x04000DE8 RID: 3560
		public EmailAddressType Mailbox;

		// Token: 0x04000DE9 RID: 3561
		public ResponseTypeType ResponseType;

		// Token: 0x04000DEA RID: 3562
		[XmlIgnore]
		public bool ResponseTypeSpecified;

		// Token: 0x04000DEB RID: 3563
		public DateTime LastResponseTime;

		// Token: 0x04000DEC RID: 3564
		[XmlIgnore]
		public bool LastResponseTimeSpecified;

		// Token: 0x04000DED RID: 3565
		public DateTime ProposedStart;

		// Token: 0x04000DEE RID: 3566
		[XmlIgnore]
		public bool ProposedStartSpecified;

		// Token: 0x04000DEF RID: 3567
		public DateTime ProposedEnd;

		// Token: 0x04000DF0 RID: 3568
		[XmlIgnore]
		public bool ProposedEndSpecified;
	}
}
