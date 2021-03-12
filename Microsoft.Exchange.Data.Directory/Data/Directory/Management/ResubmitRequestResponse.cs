using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200074B RID: 1867
	[Serializable]
	internal sealed class ResubmitRequestResponse
	{
		// Token: 0x06005AD9 RID: 23257 RVA: 0x0013E348 File Offset: 0x0013C548
		public ResubmitRequestResponse(ResubmitRequestResponseCode responseCode, string message)
		{
			this.properties["ResponseCode"] = (int)responseCode;
			this.properties["ErrorMessage"] = message;
		}

		// Token: 0x17001F80 RID: 8064
		// (get) Token: 0x06005ADA RID: 23258 RVA: 0x0013E382 File Offset: 0x0013C582
		public ResubmitRequestResponseCode ResponseCode
		{
			get
			{
				return (ResubmitRequestResponseCode)this.properties["ResponseCode"];
			}
		}

		// Token: 0x17001F81 RID: 8065
		// (get) Token: 0x06005ADB RID: 23259 RVA: 0x0013E399 File Offset: 0x0013C599
		public string ErrorMessage
		{
			get
			{
				return (string)this.properties["ErrorMessage"];
			}
		}

		// Token: 0x04003CE9 RID: 15593
		private const string ResponseCodeParameterName = "ResponseCode";

		// Token: 0x04003CEA RID: 15594
		private const string ErrorMessageParameterName = "ErrorMessage";

		// Token: 0x04003CEB RID: 15595
		public static readonly ResubmitRequestResponse SuccessResponse = new ResubmitRequestResponse(ResubmitRequestResponseCode.Success, string.Empty);

		// Token: 0x04003CEC RID: 15596
		private readonly Dictionary<string, object> properties = new Dictionary<string, object>();
	}
}
