using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Net.XropService
{
	// Token: 0x02000B9A RID: 2970
	[CLSCompliant(false)]
	[MessageContract]
	public sealed class ExecuteRequestMessage
	{
		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x06003FB6 RID: 16310 RVA: 0x000A8468 File Offset: 0x000A6668
		// (set) Token: 0x06003FB7 RID: 16311 RVA: 0x000A8470 File Offset: 0x000A6670
		[MessageBodyMember]
		public ExecuteRequest Request { get; set; }
	}
}
