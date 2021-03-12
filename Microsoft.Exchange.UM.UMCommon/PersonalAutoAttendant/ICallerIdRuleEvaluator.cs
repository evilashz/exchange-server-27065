using System;
using System.Collections.Generic;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x020000E7 RID: 231
	internal interface ICallerIdRuleEvaluator
	{
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060007A5 RID: 1957
		PersonalContactInfo[] MatchedPersonalContacts { get; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060007A6 RID: 1958
		ADContactInfo MatchedADContact { get; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060007A7 RID: 1959
		List<string> MatchedPersonaEmails { get; }

		// Token: 0x060007A8 RID: 1960
		PhoneNumber GetCallerId();
	}
}
