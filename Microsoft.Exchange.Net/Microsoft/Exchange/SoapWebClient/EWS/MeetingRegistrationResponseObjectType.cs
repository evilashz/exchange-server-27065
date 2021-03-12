using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001FC RID: 508
	[DesignerCategory("code")]
	[XmlInclude(typeof(TentativelyAcceptItemType))]
	[XmlInclude(typeof(AcceptItemType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlInclude(typeof(DeclineItemType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class MeetingRegistrationResponseObjectType : WellKnownResponseObjectType
	{
		// Token: 0x04000D50 RID: 3408
		public DateTime ProposedStart;

		// Token: 0x04000D51 RID: 3409
		[XmlIgnore]
		public bool ProposedStartSpecified;

		// Token: 0x04000D52 RID: 3410
		public DateTime ProposedEnd;

		// Token: 0x04000D53 RID: 3411
		[XmlIgnore]
		public bool ProposedEndSpecified;
	}
}
