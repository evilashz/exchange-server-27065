using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002A4 RID: 676
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class MailTipsServiceConfiguration : ServiceConfiguration
	{
		// Token: 0x040011BC RID: 4540
		public bool MailTipsEnabled;

		// Token: 0x040011BD RID: 4541
		public int MaxRecipientsPerGetMailTipsRequest;

		// Token: 0x040011BE RID: 4542
		public int MaxMessageSize;

		// Token: 0x040011BF RID: 4543
		public int LargeAudienceThreshold;

		// Token: 0x040011C0 RID: 4544
		public bool ShowExternalRecipientCount;

		// Token: 0x040011C1 RID: 4545
		[XmlArrayItem("Domain", IsNullable = false)]
		public SmtpDomain[] InternalDomains;

		// Token: 0x040011C2 RID: 4546
		public bool PolicyTipsEnabled;

		// Token: 0x040011C3 RID: 4547
		public int LargeAudienceCap;
	}
}
