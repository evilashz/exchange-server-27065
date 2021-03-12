using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000409 RID: 1033
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class RemoveImContactFromGroupType : BaseRequestType
	{
		// Token: 0x040015EA RID: 5610
		public ItemIdType ContactId;

		// Token: 0x040015EB RID: 5611
		public ItemIdType GroupId;
	}
}
