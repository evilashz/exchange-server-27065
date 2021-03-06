using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000002 RID: 2
	public static class AgentLogSchema
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static CsvTable AgentLogEvent
		{
			get
			{
				return AgentLogSchema.agentLogEvent;
			}
		}

		// Token: 0x04000001 RID: 1
		private static readonly Version E12Version = new Version("12.00.0000.00");

		// Token: 0x04000002 RID: 2
		private static readonly Version NetworkMsgIDVersion = new Version("15.00.0390.00");

		// Token: 0x04000003 RID: 3
		private static readonly Version TenantIDVersion = new Version("15.00.0398.00");

		// Token: 0x04000004 RID: 4
		private static readonly Version DirectionalityVersion = new Version("15.00.0476.00");

		// Token: 0x04000005 RID: 5
		private static readonly CsvTable agentLogEvent = new CsvTable(new CsvField[]
		{
			new CsvField("Timestamp", typeof(DateTime), AgentLogSchema.E12Version),
			new CsvField("SessionId", typeof(string), AgentLogSchema.E12Version),
			new CsvField("LocalEndpoint", typeof(string), AgentLogSchema.E12Version),
			new CsvField("RemoteEndpoint", typeof(string), AgentLogSchema.E12Version),
			new CsvField("EnteredOrgFromIP", typeof(string), AgentLogSchema.E12Version),
			new CsvField("MessageId", typeof(string), true, AgentLogSchema.E12Version, new NormalizeColumnDataMethod(CsvFieldCache.NormalizeMessageID)),
			new CsvField("P1FromAddress", typeof(string), AgentLogSchema.E12Version),
			new CsvField("P2FromAddress", typeof(string), true, AgentLogSchema.E12Version, new NormalizeColumnDataMethod(CsvFieldCache.NormalizeEmailAddress)),
			new CsvField("Recipient", typeof(string[]), AgentLogSchema.E12Version),
			new CsvField("NumRecipients", typeof(string), AgentLogSchema.E12Version),
			new CsvField("Agent", typeof(string), AgentLogSchema.E12Version),
			new CsvField("Event", typeof(string), AgentLogSchema.E12Version),
			new CsvField("Action", typeof(string), AgentLogSchema.E12Version),
			new CsvField("SmtpResponse", typeof(string), AgentLogSchema.E12Version),
			new CsvField("Reason", typeof(string), AgentLogSchema.E12Version),
			new CsvField("ReasonData", typeof(string), AgentLogSchema.E12Version),
			new CsvField("Diagnostics", typeof(string), AgentLogSchema.E12Version),
			new CsvField("NetworkMsgID", typeof(Guid), AgentLogSchema.NetworkMsgIDVersion),
			new CsvField("TenantID", typeof(Guid), AgentLogSchema.TenantIDVersion),
			new CsvField("Directionality", typeof(string), AgentLogSchema.DirectionalityVersion)
		});
	}
}
