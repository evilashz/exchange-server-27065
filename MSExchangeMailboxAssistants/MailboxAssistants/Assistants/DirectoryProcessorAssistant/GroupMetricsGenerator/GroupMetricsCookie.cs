using System;
using System.Globalization;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.GroupMetricsGenerator
{
	// Token: 0x020001A1 RID: 417
	[Serializable]
	internal class GroupMetricsCookie
	{
		// Token: 0x0600106D RID: 4205 RVA: 0x0005FD78 File Offset: 0x0005DF78
		public GroupMetricsCookie(string domain)
		{
			this.Domain = domain;
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x0005FD87 File Offset: 0x0005DF87
		// (set) Token: 0x0600106F RID: 4207 RVA: 0x0005FD8F File Offset: 0x0005DF8F
		public string Domain { get; private set; }

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x0005FD98 File Offset: 0x0005DF98
		// (set) Token: 0x06001071 RID: 4209 RVA: 0x0005FDA0 File Offset: 0x0005DFA0
		public DateTime LastDeltaSync { get; set; }

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x0005FDA9 File Offset: 0x0005DFA9
		// (set) Token: 0x06001073 RID: 4211 RVA: 0x0005FDB1 File Offset: 0x0005DFB1
		public DateTime LastFullSync { get; set; }

		// Token: 0x06001074 RID: 4212 RVA: 0x0005FDBC File Offset: 0x0005DFBC
		public static bool TryDeserialize(string record, out GroupMetricsCookie cookie)
		{
			cookie = null;
			string[] array = record.Split(GroupMetricsCookie.RecordSeparator, 3);
			if (3 != array.Length)
			{
				return false;
			}
			DateTime lastDeltaSync;
			if (!GroupMetricsCookie.TryParseDateTime(array[1], out lastDeltaSync))
			{
				return false;
			}
			DateTime lastFullSync;
			if (!GroupMetricsCookie.TryParseDateTime(array[2], out lastFullSync))
			{
				return false;
			}
			cookie = new GroupMetricsCookie(array[0]);
			cookie.LastDeltaSync = lastDeltaSync;
			cookie.LastFullSync = lastFullSync;
			return true;
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0005FE18 File Offset: 0x0005E018
		public string Serialize()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{1}{3}", new object[]
			{
				this.Domain,
				GroupMetricsCookie.RecordSeparator[0],
				this.LastDeltaSync.ToUniversalTime(),
				this.LastFullSync.ToUniversalTime()
			});
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0005FE80 File Offset: 0x0005E080
		public static bool TryParseDateTime(string input, out DateTime output)
		{
			return DateTime.TryParse(input, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out output);
		}

		// Token: 0x04000A54 RID: 2644
		private static readonly char[] RecordSeparator = new char[]
		{
			';'
		};
	}
}
