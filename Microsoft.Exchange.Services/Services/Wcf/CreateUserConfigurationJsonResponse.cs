using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BD4 RID: 3028
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateUserConfigurationJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F2B RID: 12075
		[DataMember(IsRequired = true, Order = 0)]
		public CreateUserConfigurationResponse Body;
	}
}
