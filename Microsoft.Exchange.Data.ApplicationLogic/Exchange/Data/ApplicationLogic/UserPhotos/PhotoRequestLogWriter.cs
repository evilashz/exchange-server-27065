using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x02000203 RID: 515
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PhotoRequestLogWriter : ITraceEntryWriter
	{
		// Token: 0x060012B6 RID: 4790 RVA: 0x0004E384 File Offset: 0x0004C584
		public PhotoRequestLogWriter(PhotoRequestLog log, string requestId)
		{
			ArgumentValidator.ThrowIfNull("log", log);
			ArgumentValidator.ThrowIfNullOrEmpty("requestId", requestId);
			this.log = log;
			this.requestId = requestId;
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x0004E3B0 File Offset: 0x0004C5B0
		public void Write(TraceEntry entry)
		{
			switch (entry.TraceType)
			{
			case TraceType.WarningTrace:
				this.Log(entry, "WARNING");
				return;
			case TraceType.ErrorTrace:
				this.Log(entry, "ERROR");
				return;
			case TraceType.PerformanceTrace:
				this.Log(entry, "PERFORMANCE");
				return;
			}
			this.Log(entry, "DEBUG");
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x0004E41A File Offset: 0x0004C61A
		private void Log(TraceEntry entry, string eventType)
		{
			this.log.Log(entry.Timestamp, this.requestId, eventType, entry.FormatString);
		}

		// Token: 0x04000A55 RID: 2645
		private readonly PhotoRequestLog log;

		// Token: 0x04000A56 RID: 2646
		private readonly string requestId;

		// Token: 0x02000204 RID: 516
		private static class EventTypes
		{
			// Token: 0x04000A57 RID: 2647
			public const string Debug = "DEBUG";

			// Token: 0x04000A58 RID: 2648
			public const string Error = "ERROR";

			// Token: 0x04000A59 RID: 2649
			public const string Warning = "WARNING";

			// Token: 0x04000A5A RID: 2650
			public const string Performance = "PERFORMANCE";
		}
	}
}
