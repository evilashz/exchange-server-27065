using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C3F RID: 3135
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetImListMigrationCompletedJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F96 RID: 12182
		[DataMember(IsRequired = true, Order = 0)]
		public SetImListMigrationCompletedRequest Body;
	}
}
