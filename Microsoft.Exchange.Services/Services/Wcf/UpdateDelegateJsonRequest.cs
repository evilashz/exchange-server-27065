using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BCF RID: 3023
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateDelegateJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F26 RID: 12070
		[DataMember(IsRequired = true, Order = 0)]
		public UpdateDelegateRequest Body;
	}
}
