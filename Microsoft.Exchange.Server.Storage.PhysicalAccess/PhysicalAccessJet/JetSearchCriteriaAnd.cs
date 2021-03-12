using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000BB RID: 187
	public class JetSearchCriteriaAnd : SearchCriteriaAnd, IJetSearchCriteria
	{
		// Token: 0x0600080E RID: 2062 RVA: 0x00027495 File Offset: 0x00025695
		public JetSearchCriteriaAnd(params SearchCriteria[] nestedCriteria) : base(nestedCriteria)
		{
		}
	}
}
