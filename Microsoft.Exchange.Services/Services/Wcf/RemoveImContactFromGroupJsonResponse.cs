using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C2C RID: 3116
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RemoveImContactFromGroupJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F83 RID: 12163
		[DataMember(IsRequired = true, Order = 0)]
		public RemoveImContactFromGroupResponseMessage Body;
	}
}
