using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000BF RID: 191
	public class JetSearchCriteriaCompare : SearchCriteriaCompare, IJetSearchCriteria
	{
		// Token: 0x06000812 RID: 2066 RVA: 0x000274BB File Offset: 0x000256BB
		public JetSearchCriteriaCompare(Column lhs, SearchCriteriaCompare.SearchRelOp op, Column rhs) : base(lhs, op, rhs)
		{
		}
	}
}
