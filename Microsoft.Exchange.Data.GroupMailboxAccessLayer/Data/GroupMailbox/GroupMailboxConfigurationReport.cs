using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000036 RID: 54
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class GroupMailboxConfigurationReport
	{
		// Token: 0x0600018F RID: 399 RVA: 0x0000BEEC File Offset: 0x0000A0EC
		public GroupMailboxConfigurationReport()
		{
			this.ConfigurationActionLatencyStatistics = new Dictionary<GroupMailboxConfigurationAction, LatencyStatistics>();
			this.Warnings = new List<LocalizedString>();
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000190 RID: 400 RVA: 0x0000BF0A File Offset: 0x0000A10A
		// (set) Token: 0x06000191 RID: 401 RVA: 0x0000BF12 File Offset: 0x0000A112
		public Dictionary<GroupMailboxConfigurationAction, LatencyStatistics> ConfigurationActionLatencyStatistics { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000BF1B File Offset: 0x0000A11B
		// (set) Token: 0x06000193 RID: 403 RVA: 0x0000BF23 File Offset: 0x0000A123
		public List<LocalizedString> Warnings { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000BF2C File Offset: 0x0000A12C
		// (set) Token: 0x06000195 RID: 405 RVA: 0x0000BF34 File Offset: 0x0000A134
		public bool IsConfigurationExecuted { get; internal set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0000BF3D File Offset: 0x0000A13D
		// (set) Token: 0x06000197 RID: 407 RVA: 0x0000BF45 File Offset: 0x0000A145
		public int FoldersCreatedCount { get; internal set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000BF4E File Offset: 0x0000A14E
		// (set) Token: 0x06000199 RID: 409 RVA: 0x0000BF56 File Offset: 0x0000A156
		public int FoldersPrivilegedCount { get; internal set; }

		// Token: 0x0600019A RID: 410 RVA: 0x0000BF60 File Offset: 0x0000A160
		public LatencyStatistics? GetElapsedTime(GroupMailboxConfigurationAction action)
		{
			if (!this.ConfigurationActionLatencyStatistics.ContainsKey(action))
			{
				return null;
			}
			return new LatencyStatistics?(this.ConfigurationActionLatencyStatistics[action]);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000BF98 File Offset: 0x0000A198
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(300);
			foreach (KeyValuePair<GroupMailboxConfigurationAction, LatencyStatistics> keyValuePair in this.ConfigurationActionLatencyStatistics)
			{
				stringBuilder.Append(keyValuePair.Key);
				stringBuilder.Append("ElapsedTimeMs=");
				stringBuilder.Append(keyValuePair.Value.ElapsedTime.TotalMilliseconds.ToString("n0"));
				stringBuilder.Append(", ");
			}
			stringBuilder.Append("FoldersCreatedCount=");
			stringBuilder.Append(this.FoldersCreatedCount);
			stringBuilder.Append(", FoldersPrivilegedCount=");
			stringBuilder.Append(this.FoldersPrivilegedCount);
			stringBuilder.Append(", Warnings={");
			stringBuilder.Append(string.Join<LocalizedString>(",", this.Warnings));
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}
	}
}
