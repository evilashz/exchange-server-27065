using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C67 RID: 3175
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SuiteStorageJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FBB RID: 12219
		[DataMember(IsRequired = true, Order = 0)]
		public SuiteStorageResponse Body;
	}
}
