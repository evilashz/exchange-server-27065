using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Cluster.Common;

namespace Microsoft.Exchange.Cluster.Replay.DatabaseCopyLayout
{
	// Token: 0x02000171 RID: 369
	internal class DatabaseGroupEqualityComparer : IEqualityComparer<DatabaseGroupLayoutEntry>
	{
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x0003FE59 File Offset: 0x0003E059
		public static DatabaseGroupEqualityComparer Instance
		{
			get
			{
				return DatabaseGroupEqualityComparer.d_instance;
			}
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0003FE60 File Offset: 0x0003E060
		private DatabaseGroupEqualityComparer()
		{
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0003FE68 File Offset: 0x0003E068
		public bool Equals(DatabaseGroupLayoutEntry source, DatabaseGroupLayoutEntry target)
		{
			return StringUtil.IsEqualIgnoreCase(source.DatabaseGroupId, target.DatabaseGroupId) && source.DatabaseNameList.SequenceEqual(target.DatabaseNameList, StringComparer.InvariantCultureIgnoreCase);
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0003FE9C File Offset: 0x0003E09C
		public int GetHashCode(DatabaseGroupLayoutEntry key)
		{
			int num = 1;
			foreach (string text in key.DatabaseNameList)
			{
				num ^= text.GetHashCode();
			}
			return key.DatabaseGroupId.GetHashCode() ^ num;
		}

		// Token: 0x04000620 RID: 1568
		private static readonly DatabaseGroupEqualityComparer d_instance = new DatabaseGroupEqualityComparer();
	}
}
