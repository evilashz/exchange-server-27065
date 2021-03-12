using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000D1 RID: 209
	public class IOStatistics
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x0002E9F6 File Offset: 0x0002CBF6
		public Dictionary<string, TableLevelIOStats> IOStats
		{
			get
			{
				return this.tableLevelIOStats;
			}
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0002EA00 File Offset: 0x0002CC00
		public void AddStats(string message)
		{
			string[] array = message.Split(new char[]
			{
				'.'
			});
			string text = array[0].Substring(array[0].IndexOf('\''));
			array = array[1].Split(new char[]
			{
				','
			});
			int num = int.Parse(array[1].Substring(array[1].LastIndexOf(' ')));
			int num2 = int.Parse(array[2].Substring(array[2].LastIndexOf(' ')));
			int num3 = int.Parse(array[3].Substring(array[3].LastIndexOf(' ')));
			int num4 = int.Parse(array[4].Substring(array[4].LastIndexOf(' ')));
			int num5 = int.Parse(array[5].Substring(array[5].LastIndexOf(' ')));
			int num6 = int.Parse(array[6].Substring(array[6].LastIndexOf(' ')));
			TableLevelIOStats tableLevelIOStats = null;
			if (!this.tableLevelIOStats.TryGetValue(text, out tableLevelIOStats))
			{
				tableLevelIOStats = new TableLevelIOStats(text);
				this.tableLevelIOStats.Add(text, tableLevelIOStats);
			}
			tableLevelIOStats.LogicalReads += num;
			tableLevelIOStats.PhysicalReads += num2;
			tableLevelIOStats.ReadAheads += num3;
			tableLevelIOStats.LobLogicalReads += num4;
			tableLevelIOStats.LobPhysicalReads += num5;
			tableLevelIOStats.LobReadAheads += num6;
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0002EB6C File Offset: 0x0002CD6C
		public void ResetAll()
		{
			foreach (TableLevelIOStats tableLevelIOStats in this.tableLevelIOStats.Values)
			{
				tableLevelIOStats.Reset();
			}
		}

		// Token: 0x04000333 RID: 819
		private const int AverageTablesPerConnection = 10;

		// Token: 0x04000334 RID: 820
		private Dictionary<string, TableLevelIOStats> tableLevelIOStats = new Dictionary<string, TableLevelIOStats>(10);
	}
}
