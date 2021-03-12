using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B35 RID: 2869
	[XmlInclude(typeof(PeopleRefinerDataEntryType))]
	[KnownType(typeof(PeopleRefinerDataEntryType))]
	[KnownType(typeof(AttachmentRefinerDataEntryType))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "RefinerDataEntryType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(FolderRefinerDataEntryType))]
	[XmlInclude(typeof(AttachmentRefinerDataEntryType))]
	[KnownType(typeof(FolderRefinerDataEntryType))]
	[Serializable]
	public class RefinerDataEntryType
	{
		// Token: 0x06005159 RID: 20825 RVA: 0x0010A6DE File Offset: 0x001088DE
		public RefinerDataEntryType()
		{
		}

		// Token: 0x0600515A RID: 20826 RVA: 0x0010A6E6 File Offset: 0x001088E6
		public RefinerDataEntryType(long hitCount, string refinementQuery)
		{
			this.HitCount = hitCount;
			this.RefinementQuery = refinementQuery;
		}

		// Token: 0x1700138F RID: 5007
		// (get) Token: 0x0600515B RID: 20827 RVA: 0x0010A6FC File Offset: 0x001088FC
		// (set) Token: 0x0600515C RID: 20828 RVA: 0x0010A704 File Offset: 0x00108904
		[DataMember(IsRequired = true)]
		public string RefinementQuery { get; set; }

		// Token: 0x17001390 RID: 5008
		// (get) Token: 0x0600515D RID: 20829 RVA: 0x0010A70D File Offset: 0x0010890D
		// (set) Token: 0x0600515E RID: 20830 RVA: 0x0010A715 File Offset: 0x00108915
		[DataMember(IsRequired = true)]
		public long HitCount { get; set; }
	}
}
