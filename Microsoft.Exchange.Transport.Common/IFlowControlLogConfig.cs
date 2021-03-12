using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200000A RID: 10
	public interface IFlowControlLogConfig
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000042 RID: 66
		TimeSpan AsyncInterval { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000043 RID: 67
		int BufferSize { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000044 RID: 68
		TimeSpan FlushInterval { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000045 RID: 69
		TimeSpan SummaryLoggingInterval { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000046 RID: 70
		TimeSpan SummaryBucketLength { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000047 RID: 71
		int MaxSummaryLinesLogged { get; }
	}
}
