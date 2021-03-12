using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B38 RID: 2872
	[XmlType(TypeName = "AttachmentRefinerDataEntryType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class AttachmentRefinerDataEntryType : RefinerDataEntryType
	{
		// Token: 0x06005169 RID: 20841 RVA: 0x0010A78B File Offset: 0x0010898B
		public AttachmentRefinerDataEntryType()
		{
		}

		// Token: 0x0600516A RID: 20842 RVA: 0x0010A793 File Offset: 0x00108993
		public AttachmentRefinerDataEntryType(bool withAttachments, long hitCount, string refinementQuery) : base(hitCount, refinementQuery)
		{
			this.WithAttachments = withAttachments;
		}

		// Token: 0x17001394 RID: 5012
		// (get) Token: 0x0600516B RID: 20843 RVA: 0x0010A7A4 File Offset: 0x001089A4
		// (set) Token: 0x0600516C RID: 20844 RVA: 0x0010A7AC File Offset: 0x001089AC
		[DataMember(IsRequired = true)]
		public bool WithAttachments { get; set; }
	}
}
