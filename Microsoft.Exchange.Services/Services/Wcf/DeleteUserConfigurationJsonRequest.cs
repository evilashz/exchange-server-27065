using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BD5 RID: 3029
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeleteUserConfigurationJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F2C RID: 12076
		[DataMember(IsRequired = true, Order = 0)]
		public DeleteUserConfigurationRequest Body;
	}
}
