using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CA5 RID: 3237
	[MessageContract(IsWrapped = false)]
	public class CreateFolderPathSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003035 RID: 12341
		[MessageBodyMember(Name = "CreateFolderPathResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public CreateFolderPathResponse Body;
	}
}
