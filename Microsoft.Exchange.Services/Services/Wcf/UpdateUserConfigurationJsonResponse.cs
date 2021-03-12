using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BDA RID: 3034
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateUserConfigurationJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F31 RID: 12081
		[DataMember(IsRequired = true, Order = 0)]
		public UpdateUserConfigurationResponse Body;
	}
}
