using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200025B RID: 603
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class EntityExtractionResultType
	{
		// Token: 0x04000F98 RID: 3992
		[XmlArrayItem("AddressEntity", IsNullable = false)]
		public AddressEntityType[] Addresses;

		// Token: 0x04000F99 RID: 3993
		[XmlArrayItem("MeetingSuggestion", IsNullable = false)]
		public MeetingSuggestionType[] MeetingSuggestions;

		// Token: 0x04000F9A RID: 3994
		[XmlArrayItem("TaskSuggestion", IsNullable = false)]
		public TaskSuggestionType[] TaskSuggestions;

		// Token: 0x04000F9B RID: 3995
		[XmlArrayItem("EmailAddressEntity", IsNullable = false)]
		public EmailAddressEntityType[] EmailAddresses;

		// Token: 0x04000F9C RID: 3996
		[XmlArrayItem("Contact", IsNullable = false)]
		public ContactType[] Contacts;

		// Token: 0x04000F9D RID: 3997
		[XmlArrayItem("UrlEntity", IsNullable = false)]
		public UrlEntityType[] Urls;

		// Token: 0x04000F9E RID: 3998
		[XmlArrayItem("Phone", IsNullable = false)]
		public PhoneEntityType[] PhoneNumbers;
	}
}
