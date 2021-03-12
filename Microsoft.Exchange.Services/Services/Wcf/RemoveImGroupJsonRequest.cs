using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C3B RID: 3131
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RemoveImGroupJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F92 RID: 12178
		[DataMember(IsRequired = true, Order = 0)]
		public RemoveImGroupRequest Body;
	}
}
