using System;
using System.Collections.Generic;
using Microsoft.Exchange.Migration.DataAccessLayer;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000DE RID: 222
	internal sealed class NspiMigrationDataRowProvider : IMigrationDataRowProvider
	{
		// Token: 0x06000B94 RID: 2964 RVA: 0x00033146 File Offset: 0x00031346
		public NspiMigrationDataRowProvider(ExchangeOutlookAnywhereEndpoint endpoint, MigrationJob job, bool discoverProvisioning = true)
		{
			this.discoverProvisioning = discoverProvisioning;
			this.nspiDataReader = endpoint.GetNspiDataReader(job);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00033364 File Offset: 0x00031564
		public IEnumerable<IMigrationDataRow> GetNextBatchItem(string cursorPosition, int maxCountHint)
		{
			int delta = -1;
			if (!string.IsNullOrEmpty(cursorPosition) && !int.TryParse(cursorPosition, out delta))
			{
				throw new ArgumentException("cursorPosition");
			}
			IEnumerable<IMigrationDataRow> items = this.nspiDataReader.GetItems(delta, maxCountHint, this.discoverProvisioning);
			foreach (IMigrationDataRow row in items)
			{
				yield return row;
			}
			yield break;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0003338F File Offset: 0x0003158F
		internal string[] GetMembers(string groupSmtpAddress)
		{
			return this.nspiDataReader.GetMembers(groupSmtpAddress);
		}

		// Token: 0x04000470 RID: 1136
		private readonly NspiMigrationDataReader nspiDataReader;

		// Token: 0x04000471 RID: 1137
		private readonly bool discoverProvisioning;
	}
}
