using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000231 RID: 561
	internal class RuleConverter : IDataConverter<Rule, RuleData>
	{
		// Token: 0x06001DEF RID: 7663 RVA: 0x0003DB0D File Offset: 0x0003BD0D
		Rule IDataConverter<Rule, RuleData>.GetNativeRepresentation(RuleData rd)
		{
			return rd.GetRule();
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x0003DB15 File Offset: 0x0003BD15
		RuleData IDataConverter<Rule, RuleData>.GetDataRepresentation(Rule r)
		{
			return RuleData.Create(r);
		}
	}
}
