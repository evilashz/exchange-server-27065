using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C6F RID: 3183
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RemoveModernGroupJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FC2 RID: 12226
		[DataMember(IsRequired = true, Order = 0)]
		public RemoveModernGroupRequest Body;
	}
}
