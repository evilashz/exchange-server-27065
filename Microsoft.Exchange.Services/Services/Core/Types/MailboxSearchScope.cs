using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007F5 RID: 2037
	[DataContract(Name = "MailboxSearchScope", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "MailboxSearchScopeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class MailboxSearchScope
	{
		// Token: 0x06003B8E RID: 15246 RVA: 0x000D027D File Offset: 0x000CE47D
		public MailboxSearchScope()
		{
		}

		// Token: 0x06003B8F RID: 15247 RVA: 0x000D028C File Offset: 0x000CE48C
		public MailboxSearchScope(string mailbox, MailboxSearchLocation searchScope)
		{
			this.mailbox = mailbox;
			this.searchScope = searchScope;
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06003B90 RID: 15248 RVA: 0x000D02A9 File Offset: 0x000CE4A9
		// (set) Token: 0x06003B91 RID: 15249 RVA: 0x000D02B1 File Offset: 0x000CE4B1
		[DataMember(Name = "Mailbox", IsRequired = true, Order = 0)]
		[XmlElement("Mailbox")]
		public string Mailbox
		{
			get
			{
				return this.mailbox;
			}
			set
			{
				this.mailbox = value;
			}
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06003B92 RID: 15250 RVA: 0x000D02BA File Offset: 0x000CE4BA
		// (set) Token: 0x06003B93 RID: 15251 RVA: 0x000D02C2 File Offset: 0x000CE4C2
		[XmlElement("SearchScope")]
		[IgnoreDataMember]
		public MailboxSearchLocation SearchScope
		{
			get
			{
				return this.searchScope;
			}
			set
			{
				this.searchScope = value;
			}
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06003B94 RID: 15252 RVA: 0x000D02CB File Offset: 0x000CE4CB
		// (set) Token: 0x06003B95 RID: 15253 RVA: 0x000D02D8 File Offset: 0x000CE4D8
		[XmlIgnore]
		[DataMember(Name = "SearchScope", IsRequired = true, Order = 1)]
		public string SearchScopeString
		{
			get
			{
				return EnumUtilities.ToString<MailboxSearchLocation>(this.searchScope);
			}
			set
			{
				this.searchScope = EnumUtilities.Parse<MailboxSearchLocation>(value);
			}
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06003B96 RID: 15254 RVA: 0x000D02E6 File Offset: 0x000CE4E6
		// (set) Token: 0x06003B97 RID: 15255 RVA: 0x000D02EE File Offset: 0x000CE4EE
		[XmlArray(ElementName = "ExtendedAttributes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArrayItem(ElementName = "ExtendedAttribute", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(ExtendedAttribute))]
		[DataMember(Name = "ExtendedAttributes", IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public ExtendedAttribute[] ExtendedAttributes
		{
			get
			{
				return this.extendedAttributes;
			}
			set
			{
				this.extendedAttributes = value;
			}
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06003B98 RID: 15256 RVA: 0x000D02F7 File Offset: 0x000CE4F7
		[XmlIgnore]
		public bool ExtendedAttributesSpecified
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040020D9 RID: 8409
		private string mailbox;

		// Token: 0x040020DA RID: 8410
		private MailboxSearchLocation searchScope = MailboxSearchLocation.All;

		// Token: 0x040020DB RID: 8411
		private ExtendedAttribute[] extendedAttributes;
	}
}
