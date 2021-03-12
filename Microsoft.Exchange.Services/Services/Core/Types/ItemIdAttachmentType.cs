using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000461 RID: 1121
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "ItemIdAttachment")]
	[Serializable]
	public class ItemIdAttachmentType : AttachmentType
	{
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06002100 RID: 8448 RVA: 0x000A2133 File Offset: 0x000A0333
		// (set) Token: 0x06002101 RID: 8449 RVA: 0x000A213B File Offset: 0x000A033B
		[DataMember(Name = "ItemId")]
		public ItemId ItemId { get; set; }

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06002102 RID: 8450 RVA: 0x000A2144 File Offset: 0x000A0344
		// (set) Token: 0x06002103 RID: 8451 RVA: 0x000A214C File Offset: 0x000A034C
		[DataMember(Name = "AttachmentIdToAttach")]
		public string AttachmentIdToAttach { get; set; }
	}
}
