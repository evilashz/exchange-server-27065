using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x02000A27 RID: 2599
	[Serializable]
	public sealed class RecipientInvalidException : LocalizedException
	{
		// Token: 0x06005CFD RID: 23805 RVA: 0x001880A0 File Offset: 0x001862A0
		internal RecipientInvalidException(string errorMessage) : base(new LocalizedString(errorMessage))
		{
		}
	}
}
