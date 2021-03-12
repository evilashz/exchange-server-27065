using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000750 RID: 1872
	[DataContract(Name = "DiscoverySearchConfiguration", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "DiscoverySearchConfigurationType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class DiscoverySearchConfiguration
	{
		// Token: 0x06003825 RID: 14373 RVA: 0x000C6F3A File Offset: 0x000C513A
		public DiscoverySearchConfiguration()
		{
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x000C6F44 File Offset: 0x000C5144
		internal DiscoverySearchConfiguration(string id, SearchableMailbox[] mailboxes, string query, string inPlaceHoldIdentity, string managedByOrganization, string language)
		{
			this.SearchId = id;
			this.SearchableMailboxes = mailboxes;
			this.SearchQuery = query;
			this.InPlaceHoldIdentity = inPlaceHoldIdentity;
			if (!string.IsNullOrEmpty(managedByOrganization))
			{
				this.ManagedByOrganization = managedByOrganization;
			}
			if (!string.IsNullOrEmpty(language))
			{
				this.Language = language;
			}
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06003827 RID: 14375 RVA: 0x000C6F96 File Offset: 0x000C5196
		// (set) Token: 0x06003828 RID: 14376 RVA: 0x000C6F9E File Offset: 0x000C519E
		[XmlElement("SearchId")]
		[DataMember(Name = "SearchId", IsRequired = true)]
		public string SearchId { get; set; }

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06003829 RID: 14377 RVA: 0x000C6FA7 File Offset: 0x000C51A7
		// (set) Token: 0x0600382A RID: 14378 RVA: 0x000C6FAF File Offset: 0x000C51AF
		[DataMember(Name = "SearchQuery", IsRequired = true)]
		[XmlElement("SearchQuery")]
		public string SearchQuery { get; set; }

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x0600382B RID: 14379 RVA: 0x000C6FB8 File Offset: 0x000C51B8
		// (set) Token: 0x0600382C RID: 14380 RVA: 0x000C6FC0 File Offset: 0x000C51C0
		[DataMember(Name = "SearchableMailbox", IsRequired = false)]
		[XmlArray]
		[XmlArrayItem("SearchableMailbox", Type = typeof(SearchableMailbox), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public SearchableMailbox[] SearchableMailboxes { get; set; }

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x0600382D RID: 14381 RVA: 0x000C6FC9 File Offset: 0x000C51C9
		// (set) Token: 0x0600382E RID: 14382 RVA: 0x000C6FD1 File Offset: 0x000C51D1
		[DataMember(Name = "InPlaceHoldIdentity", IsRequired = false)]
		[XmlElement("InPlaceHoldIdentity")]
		public string InPlaceHoldIdentity { get; set; }

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x0600382F RID: 14383 RVA: 0x000C6FDA File Offset: 0x000C51DA
		// (set) Token: 0x06003830 RID: 14384 RVA: 0x000C6FE2 File Offset: 0x000C51E2
		[DataMember(Name = "ManagedByOrganization", IsRequired = false)]
		[XmlElement("ManagedByOrganization")]
		public string ManagedByOrganization { get; set; }

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x06003831 RID: 14385 RVA: 0x000C6FEB File Offset: 0x000C51EB
		// (set) Token: 0x06003832 RID: 14386 RVA: 0x000C6FF3 File Offset: 0x000C51F3
		[XmlElement("Language")]
		[DataMember(Name = "Language", IsRequired = false)]
		public string Language { get; set; }
	}
}
