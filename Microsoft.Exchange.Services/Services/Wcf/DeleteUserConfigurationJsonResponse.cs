using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BD6 RID: 3030
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeleteUserConfigurationJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F2D RID: 12077
		[DataMember(IsRequired = true, Order = 0)]
		public DeleteUserConfigurationResponse Body;
	}
}
