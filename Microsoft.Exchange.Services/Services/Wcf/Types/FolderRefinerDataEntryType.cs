using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B36 RID: 2870
	[XmlType(TypeName = "FolderRefinerDataEntryType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class FolderRefinerDataEntryType : RefinerDataEntryType
	{
		// Token: 0x0600515F RID: 20831 RVA: 0x0010A71E File Offset: 0x0010891E
		public FolderRefinerDataEntryType()
		{
		}

		// Token: 0x06005160 RID: 20832 RVA: 0x0010A726 File Offset: 0x00108926
		public FolderRefinerDataEntryType(FolderId folderId, long hitCount, string refinementQuery) : base(hitCount, refinementQuery)
		{
			this.FolderId = folderId;
		}

		// Token: 0x17001391 RID: 5009
		// (get) Token: 0x06005161 RID: 20833 RVA: 0x0010A737 File Offset: 0x00108937
		// (set) Token: 0x06005162 RID: 20834 RVA: 0x0010A73F File Offset: 0x0010893F
		[DataMember(IsRequired = true)]
		public FolderId FolderId { get; set; }
	}
}
