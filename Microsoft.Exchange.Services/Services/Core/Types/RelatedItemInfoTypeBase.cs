using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005A5 RID: 1445
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "RelatedItemInfoTypeBase")]
	[Serializable]
	public abstract class RelatedItemInfoTypeBase
	{
		// Token: 0x06002930 RID: 10544 RVA: 0x000ACFBD File Offset: 0x000AB1BD
		protected RelatedItemInfoTypeBase(ItemId itemId, SingleRecipientType from, string preview)
		{
			this.From = from;
			this.Preview = preview;
			this.ItemId = itemId;
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06002931 RID: 10545 RVA: 0x000ACFDA File Offset: 0x000AB1DA
		// (set) Token: 0x06002932 RID: 10546 RVA: 0x000ACFE2 File Offset: 0x000AB1E2
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public SingleRecipientType From { get; set; }

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06002933 RID: 10547 RVA: 0x000ACFEB File Offset: 0x000AB1EB
		// (set) Token: 0x06002934 RID: 10548 RVA: 0x000ACFF3 File Offset: 0x000AB1F3
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public string Preview { get; set; }

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06002935 RID: 10549 RVA: 0x000ACFFC File Offset: 0x000AB1FC
		// (set) Token: 0x06002936 RID: 10550 RVA: 0x000AD004 File Offset: 0x000AB204
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public ItemId ItemId { get; set; }
	}
}
