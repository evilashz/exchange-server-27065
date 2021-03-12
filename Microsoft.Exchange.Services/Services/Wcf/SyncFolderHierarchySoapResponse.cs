using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CE1 RID: 3297
	[MessageContract(IsWrapped = false)]
	public class SyncFolderHierarchySoapResponse : BaseSoapResponse
	{
		// Token: 0x04003073 RID: 12403
		[MessageBodyMember(Name = "SyncFolderHierarchyResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SyncFolderHierarchyResponse Body;
	}
}
