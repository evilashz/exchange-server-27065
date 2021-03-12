using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003B0 RID: 944
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class MailboxData
	{
		// Token: 0x040014D9 RID: 5337
		public EmailAddress Email;

		// Token: 0x040014DA RID: 5338
		public MeetingAttendeeType AttendeeType;

		// Token: 0x040014DB RID: 5339
		public bool ExcludeConflicts;

		// Token: 0x040014DC RID: 5340
		[XmlIgnore]
		public bool ExcludeConflictsSpecified;
	}
}
