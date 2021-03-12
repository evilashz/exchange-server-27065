using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000BC RID: 188
	public class JetSearchCriteriaOr : SearchCriteriaOr, IJetSearchCriteria
	{
		// Token: 0x0600080F RID: 2063 RVA: 0x0002749E File Offset: 0x0002569E
		public JetSearchCriteriaOr(params SearchCriteria[] nestedCriteria) : base(nestedCriteria)
		{
		}
	}
}
