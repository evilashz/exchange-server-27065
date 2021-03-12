using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B09 RID: 2825
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetAllowedOptionsResponse : BaseJsonResponse
	{
		// Token: 0x17001320 RID: 4896
		// (get) Token: 0x06005023 RID: 20515 RVA: 0x00109390 File Offset: 0x00107590
		// (set) Token: 0x06005024 RID: 20516 RVA: 0x00109398 File Offset: 0x00107598
		[DataMember(IsRequired = true)]
		public string[] AllowedOptions { get; set; }
	}
}
