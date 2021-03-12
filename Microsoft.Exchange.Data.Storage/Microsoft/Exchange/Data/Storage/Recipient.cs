using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000859 RID: 2137
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Recipient : RecipientBase
	{
		// Token: 0x06005068 RID: 20584 RVA: 0x0014E327 File Offset: 0x0014C527
		internal Recipient(CoreRecipient coreRecipient) : base(coreRecipient)
		{
		}

		// Token: 0x06005069 RID: 20585 RVA: 0x0014E330 File Offset: 0x0014C530
		internal static void SetDefaultRecipientProperties(CoreRecipient coreRecipient)
		{
			RecipientBase.SetDefaultRecipientBaseProperties(coreRecipient);
			coreRecipient.RecipientItemType = RecipientItemType.To;
		}
	}
}
