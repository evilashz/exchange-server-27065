using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B91 RID: 2961
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateFolderJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002EE8 RID: 12008
		[DataMember(IsRequired = true, Order = 0)]
		public CreateFolderRequest Body;
	}
}
