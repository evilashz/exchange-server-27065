using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.DiagnosticsAggregation
{
	// Token: 0x02000004 RID: 4
	internal sealed class DiagnosticsAggregationLogRow : LogRowFormatter
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002374 File Offset: 0x00000574
		public DiagnosticsAggregationLogRow(LogSchema schema, DiagnosticsAggregationEvent evt, long? sessionId, TimeSpan? duration, string clientHostName, string clientProcessName, int? clientProcessId, string serverHostName, string description) : base(schema)
		{
			base[1] = Environment.MachineName;
			base[2] = ((sessionId != null) ? sessionId.ToString() : string.Empty);
			base[3] = evt;
			base[4] = ((duration != null) ? duration.ToString() : string.Empty);
			base[5] = clientHostName;
			base[6] = clientProcessName;
			base[7] = ((clientProcessId != null) ? clientProcessId.ToString() : string.Empty);
			base[8] = serverHostName;
			base[9] = description;
		}

		// Token: 0x04000016 RID: 22
		public static readonly string[] Fields = Enum.GetNames(typeof(DiagnosticsAggregationLogRow.Field));

		// Token: 0x02000005 RID: 5
		internal enum Field
		{
			// Token: 0x04000018 RID: 24
			Time,
			// Token: 0x04000019 RID: 25
			HostName,
			// Token: 0x0400001A RID: 26
			SessionId,
			// Token: 0x0400001B RID: 27
			Event,
			// Token: 0x0400001C RID: 28
			Duration,
			// Token: 0x0400001D RID: 29
			ClientHostName,
			// Token: 0x0400001E RID: 30
			ClientProcessName,
			// Token: 0x0400001F RID: 31
			ClientProcessId,
			// Token: 0x04000020 RID: 32
			ServerHostName,
			// Token: 0x04000021 RID: 33
			Description
		}
	}
}
