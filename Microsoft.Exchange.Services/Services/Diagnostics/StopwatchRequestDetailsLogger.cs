using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x02000054 RID: 84
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct StopwatchRequestDetailsLogger : IDisposable
	{
		// Token: 0x0600020B RID: 523 RVA: 0x0000B62F File Offset: 0x0000982F
		public StopwatchRequestDetailsLogger(RequestDetailsLogger logger, Enum marker)
		{
			ArgumentValidator.ThrowIfNull("logger", logger);
			ArgumentValidator.ThrowIfNull("marker", marker);
			this.logger = logger;
			this.marker = marker;
			this.stopwatch = Stopwatch.StartNew();
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000B660 File Offset: 0x00009860
		public void Dispose()
		{
			this.stopwatch.Stop();
			this.logger.Set(this.marker, this.stopwatch.ElapsedMilliseconds);
		}

		// Token: 0x040004C7 RID: 1223
		private readonly Enum marker;

		// Token: 0x040004C8 RID: 1224
		private readonly RequestDetailsLogger logger;

		// Token: 0x040004C9 RID: 1225
		private readonly Stopwatch stopwatch;
	}
}
