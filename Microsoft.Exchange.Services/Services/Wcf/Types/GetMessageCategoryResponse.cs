using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AAE RID: 2734
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetMessageCategoryResponse : OptionsResponseBase
	{
		// Token: 0x06004D0A RID: 19722 RVA: 0x00106B26 File Offset: 0x00104D26
		public GetMessageCategoryResponse()
		{
			this.MessageCategoryCollection = new MessageCategoryCollection();
		}

		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x06004D0B RID: 19723 RVA: 0x00106B39 File Offset: 0x00104D39
		// (set) Token: 0x06004D0C RID: 19724 RVA: 0x00106B41 File Offset: 0x00104D41
		[DataMember(IsRequired = true)]
		public MessageCategoryCollection MessageCategoryCollection { get; set; }

		// Token: 0x06004D0D RID: 19725 RVA: 0x00106B4A File Offset: 0x00104D4A
		public override string ToString()
		{
			return string.Format("GetMessageCategoryResponse: {0}", this.MessageCategoryCollection);
		}
	}
}
