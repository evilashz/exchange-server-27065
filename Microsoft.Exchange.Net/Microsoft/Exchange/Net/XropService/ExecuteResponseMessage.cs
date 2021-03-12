using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Net.XropService
{
	// Token: 0x02000B9C RID: 2972
	[MessageContract]
	[CLSCompliant(false)]
	public sealed class ExecuteResponseMessage
	{
		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x06003FC6 RID: 16326 RVA: 0x000A84EF File Offset: 0x000A66EF
		// (set) Token: 0x06003FC7 RID: 16327 RVA: 0x000A84F7 File Offset: 0x000A66F7
		[MessageBodyMember]
		public ExecuteResponse Response { get; set; }
	}
}
