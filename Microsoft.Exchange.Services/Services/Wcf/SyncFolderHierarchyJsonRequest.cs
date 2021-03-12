using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BC7 RID: 3015
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SyncFolderHierarchyJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F1E RID: 12062
		[DataMember(IsRequired = true, Order = 0)]
		public SyncFolderHierarchyRequest Body;
	}
}
