using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200013C RID: 316
	public interface IEacRbacPrincipal
	{
		// Token: 0x17001A4F RID: 6735
		// (get) Token: 0x06002103 RID: 8451
		ADObjectId ExecutingUserId { get; }

		// Token: 0x17001A50 RID: 6736
		// (get) Token: 0x06002104 RID: 8452
		string Name { get; }

		// Token: 0x17001A51 RID: 6737
		// (get) Token: 0x06002105 RID: 8453
		SmtpAddress ExecutingUserPrimarySmtpAddress { get; }

		// Token: 0x17001A52 RID: 6738
		// (get) Token: 0x06002106 RID: 8454
		ExTimeZone UserTimeZone { get; }

		// Token: 0x17001A53 RID: 6739
		// (get) Token: 0x06002107 RID: 8455
		string DateFormat { get; }

		// Token: 0x17001A54 RID: 6740
		// (get) Token: 0x06002108 RID: 8456
		string TimeFormat { get; }
	}
}
