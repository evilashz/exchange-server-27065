using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000C1 RID: 193
	public class JetSearchCriteriaText : SearchCriteriaText, IJetSearchCriteria
	{
		// Token: 0x06000814 RID: 2068 RVA: 0x000274D1 File Offset: 0x000256D1
		public JetSearchCriteriaText(Column lhs, SearchCriteriaText.SearchTextFullness fullnessFlags, SearchCriteriaText.SearchTextFuzzyLevel fuzzynessFlags, Column rhs) : base(lhs, fullnessFlags, fuzzynessFlags, rhs)
		{
		}
	}
}
