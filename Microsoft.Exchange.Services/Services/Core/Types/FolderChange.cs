using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200078C RID: 1932
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "FolderChangeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class FolderChange : StoreObjectChangeBase
	{
		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x060039A6 RID: 14758 RVA: 0x000CB7E5 File Offset: 0x000C99E5
		// (set) Token: 0x060039A7 RID: 14759 RVA: 0x000CB7ED File Offset: 0x000C99ED
		[XmlElement("DistinguishedFolderId", typeof(DistinguishedFolderId))]
		[DataMember(Name = "FolderId", IsRequired = true)]
		[XmlElement("FolderId", typeof(FolderId))]
		public BaseFolderId FolderId { get; set; }
	}
}
