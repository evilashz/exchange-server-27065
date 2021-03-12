using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200085B RID: 2139
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "RecognitionIdType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class RecognitionId
	{
		// Token: 0x06003D82 RID: 15746 RVA: 0x000D7963 File Offset: 0x000D5B63
		public RecognitionId()
		{
		}

		// Token: 0x06003D83 RID: 15747 RVA: 0x000D796B File Offset: 0x000D5B6B
		internal RecognitionId(Guid requestId)
		{
			this.RequestId = requestId;
		}

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x06003D84 RID: 15748 RVA: 0x000D797A File Offset: 0x000D5B7A
		// (set) Token: 0x06003D85 RID: 15749 RVA: 0x000D7982 File Offset: 0x000D5B82
		[DataMember(IsRequired = true)]
		[XmlAttribute("RequestId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public Guid RequestId { get; set; }
	}
}
