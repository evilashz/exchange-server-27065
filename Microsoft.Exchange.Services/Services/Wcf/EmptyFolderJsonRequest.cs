using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B93 RID: 2963
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class EmptyFolderJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002EEA RID: 12010
		[DataMember(IsRequired = true, Order = 0)]
		public EmptyFolderRequest Body;
	}
}
