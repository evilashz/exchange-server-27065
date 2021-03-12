using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.Exchange.Data.HA.DirectoryServices;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200002E RID: 46
	internal class AmDbOperationDetailedStatus
	{
		// Token: 0x0600020D RID: 525 RVA: 0x0000C999 File Offset: 0x0000AB99
		public AmDbOperationDetailedStatus(IADDatabase db)
		{
			this.Database = db;
			this.AttemptedServerSubStatuses = new OrderedDictionary(5);
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000C9B4 File Offset: 0x0000ABB4
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000C9BC File Offset: 0x0000ABBC
		internal IADDatabase Database { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000C9C5 File Offset: 0x0000ABC5
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000C9CD File Offset: 0x0000ABCD
		internal AmDbStateInfo InitialDbState { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000C9D6 File Offset: 0x0000ABD6
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000C9DE File Offset: 0x0000ABDE
		internal AmDbStateInfo FinalDbState { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000C9E7 File Offset: 0x0000ABE7
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0000C9EF File Offset: 0x0000ABEF
		private OrderedDictionary AttemptedServerSubStatuses { get; set; }

		// Token: 0x06000216 RID: 534 RVA: 0x0000C9F8 File Offset: 0x0000ABF8
		public void AddSubstatus(AmDbOperationSubStatus subStatus)
		{
			this.AttemptedServerSubStatuses.Add(subStatus.ServerAttempted, subStatus);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000CBB8 File Offset: 0x0000ADB8
		public IEnumerable<AmDbOperationSubStatus> GetAllSubStatuses()
		{
			foreach (object obj in this.AttemptedServerSubStatuses)
			{
				DictionaryEntry de = (DictionaryEntry)obj;
				DictionaryEntry dictionaryEntry = de;
				yield return (AmDbOperationSubStatus)dictionaryEntry.Value;
			}
			yield break;
		}
	}
}
