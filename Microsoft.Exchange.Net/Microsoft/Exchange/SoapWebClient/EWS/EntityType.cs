using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200025C RID: 604
	[XmlInclude(typeof(ContactType))]
	[XmlInclude(typeof(TaskSuggestionType))]
	[XmlInclude(typeof(PhoneEntityType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(MeetingSuggestionType))]
	[XmlInclude(typeof(UrlEntityType))]
	[XmlInclude(typeof(EmailAddressEntityType))]
	[XmlInclude(typeof(AddressEntityType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class EntityType
	{
		// Token: 0x04000F9F RID: 3999
		[XmlElement("Position")]
		public EmailPositionType[] Position;
	}
}
