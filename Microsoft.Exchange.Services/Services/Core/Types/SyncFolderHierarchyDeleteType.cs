using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000669 RID: 1641
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class SyncFolderHierarchyDeleteType : SyncFolderHierarchyChangeBase
	{
		// Token: 0x06003237 RID: 12855 RVA: 0x000B7929 File Offset: 0x000B5B29
		public SyncFolderHierarchyDeleteType()
		{
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x000B7931 File Offset: 0x000B5B31
		public SyncFolderHierarchyDeleteType(FolderId folderId)
		{
			this.FolderId = folderId;
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06003239 RID: 12857 RVA: 0x000B7940 File Offset: 0x000B5B40
		// (set) Token: 0x0600323A RID: 12858 RVA: 0x000B7948 File Offset: 0x000B5B48
		[DataMember(EmitDefaultValue = false)]
		public FolderId FolderId { get; set; }

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x0600323B RID: 12859 RVA: 0x000B7951 File Offset: 0x000B5B51
		public override SyncFolderHierarchyChangesEnum ChangeType
		{
			get
			{
				return SyncFolderHierarchyChangesEnum.Delete;
			}
		}
	}
}
