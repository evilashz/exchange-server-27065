using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C28 RID: 3112
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddDistributionGroupToImListJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F7F RID: 12159
		[DataMember(IsRequired = true, Order = 0)]
		public AddDistributionGroupToImListResponseMessage Body;
	}
}
