using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B97 RID: 2967
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateFolderJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002EEE RID: 12014
		[DataMember(IsRequired = true, Order = 0)]
		public UpdateFolderRequest Body;
	}
}
