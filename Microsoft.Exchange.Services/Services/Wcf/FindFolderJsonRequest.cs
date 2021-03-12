using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BA1 RID: 2977
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FindFolderJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002EF8 RID: 12024
		[DataMember(IsRequired = true, Order = 0)]
		public FindFolderRequest Body;
	}
}
