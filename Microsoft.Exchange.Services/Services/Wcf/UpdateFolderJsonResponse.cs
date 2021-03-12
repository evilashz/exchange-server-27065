using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B98 RID: 2968
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateFolderJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002EEF RID: 12015
		[DataMember(IsRequired = true, Order = 0)]
		public UpdateFolderResponse Body;
	}
}
