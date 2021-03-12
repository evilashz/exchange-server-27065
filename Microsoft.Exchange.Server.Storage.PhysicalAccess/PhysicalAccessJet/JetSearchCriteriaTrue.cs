using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000B9 RID: 185
	public class JetSearchCriteriaTrue : SearchCriteriaTrue, IJetSearchCriteria
	{
		// Token: 0x0600080A RID: 2058 RVA: 0x0002746D File Offset: 0x0002566D
		internal JetSearchCriteriaTrue()
		{
		}

		// Token: 0x040002EB RID: 747
		public static readonly JetSearchCriteriaTrue Instance = new JetSearchCriteriaTrue();
	}
}
