using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000446 RID: 1094
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetUserAvailabilityRequestType : BaseRequestType
	{
		// Token: 0x040016CF RID: 5839
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public SerializableTimeZone TimeZone;

		// Token: 0x040016D0 RID: 5840
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public MailboxData[] MailboxDataArray;

		// Token: 0x040016D1 RID: 5841
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public FreeBusyViewOptionsType FreeBusyViewOptions;

		// Token: 0x040016D2 RID: 5842
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public SuggestionsViewOptionsType SuggestionsViewOptions;
	}
}
