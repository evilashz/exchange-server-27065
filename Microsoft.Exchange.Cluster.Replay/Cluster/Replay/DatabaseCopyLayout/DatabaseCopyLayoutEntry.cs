using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay.DatabaseCopyLayout
{
	// Token: 0x02000172 RID: 370
	internal class DatabaseCopyLayoutEntry
	{
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x0003FF10 File Offset: 0x0003E110
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.DatabaseCopyLayoutTracer;
			}
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0003FF17 File Offset: 0x0003E117
		public DatabaseCopyLayoutEntry(string databaseNamePrefix = "", string databaseNumberFormatSpecifier = "D3")
		{
			this.m_databaseNamePrefix = databaseNamePrefix;
			this.m_databaseNumberFormatSpecifier = databaseNumberFormatSpecifier;
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0003FF2D File Offset: 0x0003E12D
		public string GetDatabaseName(int databaseNumber)
		{
			return this.m_databaseNamePrefix + databaseNumber.ToString(this.m_databaseNumberFormatSpecifier);
		}

		// Token: 0x04000621 RID: 1569
		private readonly string m_databaseNamePrefix;

		// Token: 0x04000622 RID: 1570
		private readonly string m_databaseNumberFormatSpecifier;
	}
}
