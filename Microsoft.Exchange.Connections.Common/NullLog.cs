using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000017 RID: 23
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NullLog : Log
	{
		// Token: 0x06000056 RID: 86 RVA: 0x0000263A File Offset: 0x0000083A
		public NullLog() : base(new NullLogEmitter(), LogLevel.LogNone)
		{
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002648 File Offset: 0x00000848
		public override void Assert(bool condition, string formatString, params object[] args)
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000264A File Offset: 0x0000084A
		public override void RetailAssert(bool condition, string formatString, params object[] args)
		{
		}
	}
}
