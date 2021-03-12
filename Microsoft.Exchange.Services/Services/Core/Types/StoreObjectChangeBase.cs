using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200078B RID: 1931
	[KnownType(typeof(DeleteFolderPropertyUpdate))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(SetFolderPropertyUpdate))]
	[KnownType(typeof(AppendItemPropertyUpdate))]
	[KnownType(typeof(DeleteItemPropertyUpdate))]
	[KnownType(typeof(SetItemPropertyUpdate))]
	[KnownType(typeof(AppendFolderPropertyUpdate))]
	public class StoreObjectChangeBase : IStoreObjectChange
	{
		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x060039A3 RID: 14755 RVA: 0x000CB7CC File Offset: 0x000C99CC
		// (set) Token: 0x060039A4 RID: 14756 RVA: 0x000CB7D4 File Offset: 0x000C99D4
		[XmlArrayItem("SetItemField", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(SetItemPropertyUpdate))]
		[XmlArrayItem("AppendToFolderField", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(AppendFolderPropertyUpdate))]
		[XmlArrayItem("DeleteItemField", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(DeleteItemPropertyUpdate))]
		[DataMember(Name = "Updates", IsRequired = true)]
		[XmlArray("Updates", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArrayItem("DeleteFolderField", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(DeleteFolderPropertyUpdate))]
		[XmlArrayItem("SetFolderField", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(SetFolderPropertyUpdate))]
		[XmlArrayItem("AppendToItemField", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(AppendItemPropertyUpdate))]
		public PropertyUpdate[] PropertyUpdates { get; set; }
	}
}
