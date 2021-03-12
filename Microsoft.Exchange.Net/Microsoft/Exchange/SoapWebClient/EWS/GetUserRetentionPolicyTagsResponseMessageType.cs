using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001B0 RID: 432
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetUserRetentionPolicyTagsResponseMessageType : ResponseMessageType
	{
		// Token: 0x04000A07 RID: 2567
		[XmlArrayItem("RetentionPolicyTag", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public RetentionPolicyTagType[] RetentionPolicyTags;
	}
}
