using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005E4 RID: 1508
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "InReplyToAdapterType")]
	[Serializable]
	public class InReplyToAdapterType : RelatedItemInfoTypeBase
	{
		// Token: 0x06002D6A RID: 11626 RVA: 0x000B22CB File Offset: 0x000B04CB
		public InReplyToAdapterType(ItemId itemId, SingleRecipientType from, string preview) : base(itemId, from, preview)
		{
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06002D6B RID: 11627 RVA: 0x000B22D6 File Offset: 0x000B04D6
		// (set) Token: 0x06002D6C RID: 11628 RVA: 0x000B22DE File Offset: 0x000B04DE
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public string InternetMessageId { get; set; }

		// Token: 0x06002D6D RID: 11629 RVA: 0x000B22E7 File Offset: 0x000B04E7
		public static InReplyToAdapterType FromRelatedItemInfo(IRelatedItemInfo itemInfo)
		{
			return new InReplyToAdapterType(itemInfo.ItemId, itemInfo.From, itemInfo.Preview);
		}
	}
}
