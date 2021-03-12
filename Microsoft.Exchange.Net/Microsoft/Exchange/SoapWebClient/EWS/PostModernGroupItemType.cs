using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000479 RID: 1145
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class PostModernGroupItemType : BaseRequestType
	{
		// Token: 0x0400178B RID: 6027
		public EmailAddressType ModernGroupEmailAddress;

		// Token: 0x0400178C RID: 6028
		public NonEmptyArrayOfAllItemsType Items;
	}
}
