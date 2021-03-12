using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002CC RID: 716
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class UserIdType
	{
		// Token: 0x04001228 RID: 4648
		public string SID;

		// Token: 0x04001229 RID: 4649
		public string PrimarySmtpAddress;

		// Token: 0x0400122A RID: 4650
		public string DisplayName;

		// Token: 0x0400122B RID: 4651
		public DistinguishedUserType DistinguishedUser;

		// Token: 0x0400122C RID: 4652
		[XmlIgnore]
		public bool DistinguishedUserSpecified;

		// Token: 0x0400122D RID: 4653
		public string ExternalUserIdentity;
	}
}
