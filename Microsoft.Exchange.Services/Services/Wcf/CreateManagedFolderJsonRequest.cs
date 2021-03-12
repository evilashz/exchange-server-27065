using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BBF RID: 3007
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateManagedFolderJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F16 RID: 12054
		[DataMember(IsRequired = true, Order = 0)]
		public CreateManagedFolderRequest Body;
	}
}
