using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001C0 RID: 448
	internal sealed class EmsmdbDummyRequest : MapiHttpRequest
	{
		// Token: 0x06000985 RID: 2437 RVA: 0x0001E4D1 File Offset: 0x0001C6D1
		public EmsmdbDummyRequest() : base(Array<byte>.EmptySegment)
		{
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0001E4DE File Offset: 0x0001C6DE
		public EmsmdbDummyRequest(Reader reader) : base(reader)
		{
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0001E4E7 File Offset: 0x0001C6E7
		public override void Serialize(Writer writer)
		{
		}
	}
}
