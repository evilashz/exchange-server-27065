using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BD9 RID: 3033
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateUserConfigurationJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F30 RID: 12080
		[DataMember(IsRequired = true, Order = 0)]
		public UpdateUserConfigurationOwaRequest Body;
	}
}
