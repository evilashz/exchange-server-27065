using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C3C RID: 3132
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RemoveImGroupJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F93 RID: 12179
		[DataMember(IsRequired = true, Order = 0)]
		public RemoveImGroupResponseMessage Body;
	}
}
