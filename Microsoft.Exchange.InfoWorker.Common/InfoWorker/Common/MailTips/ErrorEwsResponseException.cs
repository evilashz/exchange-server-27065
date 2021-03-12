using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;

namespace Microsoft.Exchange.InfoWorker.Common.MailTips
{
	// Token: 0x0200011E RID: 286
	public class ErrorEwsResponseException : LocalizedException
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x000233A1 File Offset: 0x000215A1
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x000233A9 File Offset: 0x000215A9
		public ResponseCodeType ResponseCodeType { get; private set; }

		// Token: 0x060007E7 RID: 2023 RVA: 0x000233B2 File Offset: 0x000215B2
		public ErrorEwsResponseException(ResponseCodeType responseCodeType) : base(Strings.descErrorEwsResponse((int)responseCodeType))
		{
			this.ResponseCodeType = responseCodeType;
		}
	}
}
