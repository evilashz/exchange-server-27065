using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003BC RID: 956
	[XmlInclude(typeof(GroupLocatorType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlInclude(typeof(UserLocatorType))]
	[Serializable]
	public class MailboxLocatorType
	{
		// Token: 0x04001517 RID: 5399
		public string ExternalDirectoryObjectId;

		// Token: 0x04001518 RID: 5400
		public string LegacyDn;
	}
}
