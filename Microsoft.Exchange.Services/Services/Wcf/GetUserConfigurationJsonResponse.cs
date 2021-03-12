using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BD8 RID: 3032
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetUserConfigurationJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F2F RID: 12079
		[DataMember(IsRequired = true, Order = 0)]
		public GetUserConfigurationResponse Body;
	}
}
