using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003BA RID: 954
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class MasterMailboxType
	{
		// Token: 0x04001506 RID: 5382
		public string MailboxType;

		// Token: 0x04001507 RID: 5383
		public string Alias;

		// Token: 0x04001508 RID: 5384
		public string DisplayName;

		// Token: 0x04001509 RID: 5385
		public string SmtpAddress;

		// Token: 0x0400150A RID: 5386
		public ModernGroupTypeType GroupType;

		// Token: 0x0400150B RID: 5387
		[XmlIgnore]
		public bool GroupTypeSpecified;

		// Token: 0x0400150C RID: 5388
		public string Description;

		// Token: 0x0400150D RID: 5389
		public string Photo;

		// Token: 0x0400150E RID: 5390
		public string SharePointUrl;

		// Token: 0x0400150F RID: 5391
		public string InboxUrl;

		// Token: 0x04001510 RID: 5392
		public string CalendarUrl;

		// Token: 0x04001511 RID: 5393
		public string DomainController;
	}
}
