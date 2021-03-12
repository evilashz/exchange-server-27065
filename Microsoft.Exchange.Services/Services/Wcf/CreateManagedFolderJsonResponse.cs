using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BC0 RID: 3008
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateManagedFolderJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F17 RID: 12055
		[DataMember(IsRequired = true, Order = 0)]
		public CreateManagedFolderResponse Body;
	}
}
