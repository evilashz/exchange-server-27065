using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C40 RID: 3136
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetImListMigrationCompletedJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F97 RID: 12183
		[DataMember(IsRequired = true, Order = 0)]
		public SetImListMigrationCompletedResponseMessage Body;
	}
}
