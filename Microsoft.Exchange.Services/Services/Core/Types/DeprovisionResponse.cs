using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004CD RID: 1229
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeprovisionResponse : BaseResponseMessage
	{
		// Token: 0x0600241F RID: 9247 RVA: 0x000A47E2 File Offset: 0x000A29E2
		public DeprovisionResponse() : base(ResponseType.DeprovisionResponseMessage)
		{
		}
	}
}
