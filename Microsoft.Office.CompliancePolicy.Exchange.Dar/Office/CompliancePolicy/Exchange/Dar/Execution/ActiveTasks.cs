using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.CompliancePolicy.Dar;

namespace Microsoft.Office.CompliancePolicy.Exchange.Dar.Execution
{
	// Token: 0x0200000C RID: 12
	public class ActiveTasks
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00003528 File Offset: 0x00001728
		public bool Contains(string tenantId)
		{
			return this.backingStore.ContainsKey(tenantId);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003536 File Offset: 0x00001736
		public IEnumerable<string> GetKnownTenants()
		{
			return this.backingStore.Keys;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000354C File Offset: 0x0000174C
		public IEnumerable<DarTask> GetByTenantOrAll(string tenantId = null)
		{
			if (tenantId != null)
			{
				return this.backingStore[tenantId].Values;
			}
			return this.backingStore.Values.SelectMany((Dictionary<string, DarTask> dic) => dic.Values);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000359C File Offset: 0x0000179C
		public Dictionary<string, DarTask> GetByTenant(string tenantId)
		{
			Dictionary<string, DarTask> dictionary;
			this.backingStore.TryGetValue(tenantId, out dictionary);
			return dictionary ?? new Dictionary<string, DarTask>();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000035C4 File Offset: 0x000017C4
		public void Update(string tenantId, Dictionary<string, DarTask> newGroup)
		{
			if (newGroup.Count == 0)
			{
				Dictionary<string, DarTask> dictionary;
				this.backingStore.TryRemove(tenantId, out dictionary);
				return;
			}
			this.backingStore[tenantId] = newGroup;
		}

		// Token: 0x04000025 RID: 37
		private ConcurrentDictionary<string, Dictionary<string, DarTask>> backingStore = new ConcurrentDictionary<string, Dictionary<string, DarTask>>();
	}
}
