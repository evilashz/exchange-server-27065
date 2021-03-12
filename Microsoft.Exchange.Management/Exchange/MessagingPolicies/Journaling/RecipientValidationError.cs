using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x02000A28 RID: 2600
	[Serializable]
	internal sealed class RecipientValidationError : ValidationError
	{
		// Token: 0x06005CFE RID: 23806 RVA: 0x001880AE File Offset: 0x001862AE
		internal RecipientValidationError(LocalizedString message) : base(message)
		{
		}
	}
}
