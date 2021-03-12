using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C2B RID: 3115
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RemoveImContactFromGroupJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F82 RID: 12162
		[DataMember(IsRequired = true, Order = 0)]
		public RemoveImContactFromGroupRequest Body;
	}
}
