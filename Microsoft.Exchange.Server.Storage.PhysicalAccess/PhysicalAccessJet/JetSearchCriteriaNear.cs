using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000BE RID: 190
	public class JetSearchCriteriaNear : SearchCriteriaNear, IJetSearchCriteria
	{
		// Token: 0x06000811 RID: 2065 RVA: 0x000274B0 File Offset: 0x000256B0
		public JetSearchCriteriaNear(int distance, bool ordered, SearchCriteriaAnd criteria) : base(distance, ordered, criteria)
		{
		}
	}
}
