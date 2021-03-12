using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000537 RID: 1335
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ProvisionResponse : BaseResponseMessage
	{
		// Token: 0x06002600 RID: 9728 RVA: 0x000A61F7 File Offset: 0x000A43F7
		public ProvisionResponse() : base(ResponseType.ProvisionResponseMessage)
		{
		}
	}
}
