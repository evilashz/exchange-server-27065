using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000033 RID: 51
	public static class LawEnforcementClientActivitySchema
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00007B32 File Offset: 0x00005D32
		public static CsvTable ClientActivityEvent
		{
			get
			{
				return LawEnforcementClientActivitySchema.clientActivityEvent;
			}
		}

		// Token: 0x040000D0 RID: 208
		private static readonly CsvTable clientActivityEvent = new CsvTable(new CsvField[]
		{
			new CsvField("time", typeof(DateTime)),
			new CsvField("protocol", typeof(string)),
			new CsvField("account", typeof(string)),
			new CsvField("client-ip", typeof(string)),
			new CsvField("server-ip", typeof(string)),
			new CsvField("access-timestamp", typeof(string)),
			new CsvField("access-duration", typeof(string)),
			new CsvField("message-download", typeof(string))
		});
	}
}
