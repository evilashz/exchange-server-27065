using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200027C RID: 636
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class SearchableMailboxType
	{
		// Token: 0x04001055 RID: 4181
		public string Guid;

		// Token: 0x04001056 RID: 4182
		public string PrimarySmtpAddress;

		// Token: 0x04001057 RID: 4183
		public bool IsExternalMailbox;

		// Token: 0x04001058 RID: 4184
		public string ExternalEmailAddress;

		// Token: 0x04001059 RID: 4185
		public string DisplayName;

		// Token: 0x0400105A RID: 4186
		public bool IsMembershipGroup;

		// Token: 0x0400105B RID: 4187
		public string ReferenceId;
	}
}
