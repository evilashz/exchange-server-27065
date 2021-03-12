using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007DF RID: 2015
	internal interface IParticipantResolver
	{
		// Token: 0x06003B3D RID: 15165
		EmailAddressWrapper[] ResolveToEmailAddressWrapper(IEnumerable<IParticipant> participants);

		// Token: 0x06003B3E RID: 15166
		SingleRecipientType ResolveToSingleRecipientType(IParticipant participant);

		// Token: 0x06003B3F RID: 15167
		SmtpAddress ResolveToSmtpAddress(IParticipant participant);

		// Token: 0x06003B40 RID: 15168
		SingleRecipientType[] ResolveToSingleRecipientType(IEnumerable<IParticipant> participants);

		// Token: 0x06003B41 RID: 15169
		void LoadAdDataIfNeeded(IEnumerable<IParticipant> pregatherParticipants);
	}
}
