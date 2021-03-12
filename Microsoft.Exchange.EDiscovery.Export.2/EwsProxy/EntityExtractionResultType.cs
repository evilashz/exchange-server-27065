using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200017A RID: 378
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class EntityExtractionResultType
	{
		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x00023B15 File Offset: 0x00021D15
		// (set) Token: 0x06001082 RID: 4226 RVA: 0x00023B1D File Offset: 0x00021D1D
		[XmlArrayItem("AddressEntity", IsNullable = false)]
		public AddressEntityType[] Addresses
		{
			get
			{
				return this.addressesField;
			}
			set
			{
				this.addressesField = value;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001083 RID: 4227 RVA: 0x00023B26 File Offset: 0x00021D26
		// (set) Token: 0x06001084 RID: 4228 RVA: 0x00023B2E File Offset: 0x00021D2E
		[XmlArrayItem("MeetingSuggestion", IsNullable = false)]
		public MeetingSuggestionType[] MeetingSuggestions
		{
			get
			{
				return this.meetingSuggestionsField;
			}
			set
			{
				this.meetingSuggestionsField = value;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x00023B37 File Offset: 0x00021D37
		// (set) Token: 0x06001086 RID: 4230 RVA: 0x00023B3F File Offset: 0x00021D3F
		[XmlArrayItem("TaskSuggestion", IsNullable = false)]
		public TaskSuggestionType[] TaskSuggestions
		{
			get
			{
				return this.taskSuggestionsField;
			}
			set
			{
				this.taskSuggestionsField = value;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x00023B48 File Offset: 0x00021D48
		// (set) Token: 0x06001088 RID: 4232 RVA: 0x00023B50 File Offset: 0x00021D50
		[XmlArrayItem("EmailAddressEntity", IsNullable = false)]
		public EmailAddressEntityType[] EmailAddresses
		{
			get
			{
				return this.emailAddressesField;
			}
			set
			{
				this.emailAddressesField = value;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x00023B59 File Offset: 0x00021D59
		// (set) Token: 0x0600108A RID: 4234 RVA: 0x00023B61 File Offset: 0x00021D61
		[XmlArrayItem("Contact", IsNullable = false)]
		public ContactType[] Contacts
		{
			get
			{
				return this.contactsField;
			}
			set
			{
				this.contactsField = value;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x00023B6A File Offset: 0x00021D6A
		// (set) Token: 0x0600108C RID: 4236 RVA: 0x00023B72 File Offset: 0x00021D72
		[XmlArrayItem("UrlEntity", IsNullable = false)]
		public UrlEntityType[] Urls
		{
			get
			{
				return this.urlsField;
			}
			set
			{
				this.urlsField = value;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x0600108D RID: 4237 RVA: 0x00023B7B File Offset: 0x00021D7B
		// (set) Token: 0x0600108E RID: 4238 RVA: 0x00023B83 File Offset: 0x00021D83
		[XmlArrayItem("Phone", IsNullable = false)]
		public PhoneEntityType[] PhoneNumbers
		{
			get
			{
				return this.phoneNumbersField;
			}
			set
			{
				this.phoneNumbersField = value;
			}
		}

		// Token: 0x04000B46 RID: 2886
		private AddressEntityType[] addressesField;

		// Token: 0x04000B47 RID: 2887
		private MeetingSuggestionType[] meetingSuggestionsField;

		// Token: 0x04000B48 RID: 2888
		private TaskSuggestionType[] taskSuggestionsField;

		// Token: 0x04000B49 RID: 2889
		private EmailAddressEntityType[] emailAddressesField;

		// Token: 0x04000B4A RID: 2890
		private ContactType[] contactsField;

		// Token: 0x04000B4B RID: 2891
		private UrlEntityType[] urlsField;

		// Token: 0x04000B4C RID: 2892
		private PhoneEntityType[] phoneNumbersField;
	}
}
