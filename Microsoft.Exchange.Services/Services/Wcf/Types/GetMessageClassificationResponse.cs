using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AAF RID: 2735
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetMessageClassificationResponse : OptionsResponseBase
	{
		// Token: 0x06004D0E RID: 19726 RVA: 0x00106B5C File Offset: 0x00104D5C
		public GetMessageClassificationResponse()
		{
			this.MessageClassificationCollection = new MessageClassificationCollection();
		}

		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x06004D0F RID: 19727 RVA: 0x00106B6F File Offset: 0x00104D6F
		// (set) Token: 0x06004D10 RID: 19728 RVA: 0x00106B77 File Offset: 0x00104D77
		[DataMember(IsRequired = true)]
		public MessageClassificationCollection MessageClassificationCollection { get; set; }

		// Token: 0x06004D11 RID: 19729 RVA: 0x00106B80 File Offset: 0x00104D80
		public override string ToString()
		{
			return string.Format("GetMessageClassificationResponse: {0}", this.MessageClassificationCollection);
		}
	}
}
