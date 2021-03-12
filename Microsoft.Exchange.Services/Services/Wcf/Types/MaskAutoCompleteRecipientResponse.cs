using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009F4 RID: 2548
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MaskAutoCompleteRecipientResponse : IExchangeWebMethodResponse
	{
		// Token: 0x060047FE RID: 18430 RVA: 0x00100D62 File Offset: 0x000FEF62
		public MaskAutoCompleteRecipientResponse()
		{
			this.WasSuccessful = true;
		}

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x060047FF RID: 18431 RVA: 0x00100D71 File Offset: 0x000FEF71
		// (set) Token: 0x06004800 RID: 18432 RVA: 0x00100D79 File Offset: 0x000FEF79
		[DataMember]
		public bool WasSuccessful { get; set; }

		// Token: 0x06004801 RID: 18433 RVA: 0x00100D82 File Offset: 0x000FEF82
		ResponseType IExchangeWebMethodResponse.GetResponseType()
		{
			return ResponseType.MaskAutoCompleteRecipientResponseMessage;
		}

		// Token: 0x06004802 RID: 18434 RVA: 0x00100D89 File Offset: 0x000FEF89
		ResponseCodeType IExchangeWebMethodResponse.GetErrorCodeToLog()
		{
			return ResponseCodeType.NoError;
		}
	}
}
