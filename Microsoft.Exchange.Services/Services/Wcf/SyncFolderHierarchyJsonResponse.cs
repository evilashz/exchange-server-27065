using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BC8 RID: 3016
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SyncFolderHierarchyJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F1F RID: 12063
		[DataMember(IsRequired = true, Order = 0)]
		public SyncFolderHierarchyResponse Body;
	}
}
