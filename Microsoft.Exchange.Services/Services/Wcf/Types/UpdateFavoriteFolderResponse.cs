using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A47 RID: 2631
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateFavoriteFolderResponse
	{
		// Token: 0x06004A63 RID: 19043 RVA: 0x001046BD File Offset: 0x001028BD
		public UpdateFavoriteFolderResponse(string errorMessage)
		{
			this.WasSuccessful = false;
			this.ErrorMessage = errorMessage;
		}

		// Token: 0x06004A64 RID: 19044 RVA: 0x001046D3 File Offset: 0x001028D3
		public UpdateFavoriteFolderResponse()
		{
			this.WasSuccessful = true;
		}

		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x06004A65 RID: 19045 RVA: 0x001046E2 File Offset: 0x001028E2
		// (set) Token: 0x06004A66 RID: 19046 RVA: 0x001046EA File Offset: 0x001028EA
		[DataMember]
		public bool WasSuccessful { get; set; }

		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x06004A67 RID: 19047 RVA: 0x001046F3 File Offset: 0x001028F3
		// (set) Token: 0x06004A68 RID: 19048 RVA: 0x001046FB File Offset: 0x001028FB
		[DataMember]
		public string ErrorMessage { get; set; }
	}
}
