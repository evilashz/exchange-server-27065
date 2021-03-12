using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x020000B0 RID: 176
	[DataContract]
	internal abstract class Operation : Resource
	{
		// Token: 0x06000402 RID: 1026 RVA: 0x0000A8E3 File Offset: 0x00008AE3
		protected Operation(string selfUri) : base(selfUri)
		{
		}
	}
}
