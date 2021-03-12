using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C66 RID: 3174
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SuiteStorageJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FBA RID: 12218
		[DataMember(IsRequired = true, Order = 0)]
		public SuiteStorageRequest Body;
	}
}
