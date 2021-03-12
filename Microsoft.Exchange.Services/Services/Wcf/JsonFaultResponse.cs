using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C8B RID: 3211
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class JsonFaultResponse : BaseJsonResponse
	{
		// Token: 0x0600570E RID: 22286 RVA: 0x00111977 File Offset: 0x0010FB77
		public JsonFaultResponse()
		{
			this.Body = new JsonFaultBody();
		}

		// Token: 0x04002FE8 RID: 12264
		[DataMember]
		public JsonFaultBody Body;
	}
}
