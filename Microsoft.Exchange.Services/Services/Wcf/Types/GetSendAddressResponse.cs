using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AB3 RID: 2739
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetSendAddressResponse : OptionsResponseBase
	{
		// Token: 0x06004D1B RID: 19739 RVA: 0x00106BFB File Offset: 0x00104DFB
		public GetSendAddressResponse()
		{
			this.SendAddressCollection = new SendAddressCollection();
		}

		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x06004D1C RID: 19740 RVA: 0x00106C0E File Offset: 0x00104E0E
		// (set) Token: 0x06004D1D RID: 19741 RVA: 0x00106C16 File Offset: 0x00104E16
		[DataMember(IsRequired = true)]
		public SendAddressCollection SendAddressCollection { get; set; }

		// Token: 0x06004D1E RID: 19742 RVA: 0x00106C1F File Offset: 0x00104E1F
		public override string ToString()
		{
			return string.Format("GetSendAddressResponse: {0}", this.SendAddressCollection);
		}
	}
}
