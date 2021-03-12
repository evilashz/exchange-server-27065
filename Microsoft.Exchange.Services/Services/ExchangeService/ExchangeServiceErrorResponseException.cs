using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.ExchangeService
{
	// Token: 0x02000DEA RID: 3562
	[Serializable]
	internal class ExchangeServiceErrorResponseException : ExchangeServiceResponseException
	{
		// Token: 0x06005C16 RID: 23574 RVA: 0x0011DF37 File Offset: 0x0011C137
		public ExchangeServiceErrorResponseException(ResponseCodeType responseErrorCode, string responseErrorText) : base(CoreResources.ExchangeServiceResponseErrorWithCode(responseErrorCode.ToString(), responseErrorText))
		{
			this.ResponseErrorCode = responseErrorCode;
			this.ResponseErrorText = responseErrorText;
		}

		// Token: 0x170014E7 RID: 5351
		// (get) Token: 0x06005C17 RID: 23575 RVA: 0x0011DF5E File Offset: 0x0011C15E
		// (set) Token: 0x06005C18 RID: 23576 RVA: 0x0011DF66 File Offset: 0x0011C166
		public ResponseCodeType ResponseErrorCode { get; private set; }

		// Token: 0x170014E8 RID: 5352
		// (get) Token: 0x06005C19 RID: 23577 RVA: 0x0011DF6F File Offset: 0x0011C16F
		// (set) Token: 0x06005C1A RID: 23578 RVA: 0x0011DF77 File Offset: 0x0011C177
		public string ResponseErrorText { get; private set; }
	}
}
