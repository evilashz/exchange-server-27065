using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D75 RID: 3445
	[MessageContract(IsWrapped = false)]
	public class SetImListMigrationCompletedSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003107 RID: 12551
		[MessageBodyMember(Name = "SetImListMigrationCompletedResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SetImListMigrationCompletedResponseMessage Body;
	}
}
