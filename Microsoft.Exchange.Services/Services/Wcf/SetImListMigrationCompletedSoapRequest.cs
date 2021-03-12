using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D74 RID: 3444
	[MessageContract(IsWrapped = false)]
	public class SetImListMigrationCompletedSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003106 RID: 12550
		[MessageBodyMember(Name = "SetImListMigrationCompleted", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SetImListMigrationCompletedRequest Body;
	}
}
