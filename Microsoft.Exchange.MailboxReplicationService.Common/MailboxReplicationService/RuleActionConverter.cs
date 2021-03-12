using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000230 RID: 560
	internal class RuleActionConverter : IDataConverter<RuleAction, RuleActionData>
	{
		// Token: 0x06001DEC RID: 7660 RVA: 0x0003DAF5 File Offset: 0x0003BCF5
		RuleAction IDataConverter<RuleAction, RuleActionData>.GetNativeRepresentation(RuleActionData rad)
		{
			return rad.GetRuleAction();
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x0003DAFD File Offset: 0x0003BCFD
		RuleActionData IDataConverter<RuleAction, RuleActionData>.GetDataRepresentation(RuleAction ra)
		{
			return RuleActionData.GetRuleActionData(ra);
		}
	}
}
