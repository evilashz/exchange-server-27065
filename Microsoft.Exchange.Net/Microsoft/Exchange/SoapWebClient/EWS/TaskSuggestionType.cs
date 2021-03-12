using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200025F RID: 607
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class TaskSuggestionType : EntityType
	{
		// Token: 0x04000FA6 RID: 4006
		public string TaskString;

		// Token: 0x04000FA7 RID: 4007
		[XmlArrayItem("EmailUser", IsNullable = false)]
		public EmailUserType[] Assignees;
	}
}
