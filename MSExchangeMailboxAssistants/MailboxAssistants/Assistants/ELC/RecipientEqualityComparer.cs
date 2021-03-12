using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200009E RID: 158
	internal class RecipientEqualityComparer : IEqualityComparer<Recipient>
	{
		// Token: 0x06000616 RID: 1558 RVA: 0x0002ED35 File Offset: 0x0002CF35
		public bool Equals(Recipient x, Recipient y)
		{
			return x != null && y != null && Participant.HasSameEmail(x.Participant, y.Participant);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0002ED50 File Offset: 0x0002CF50
		public int GetHashCode(Recipient x)
		{
			if (x == null || !(x.Participant != null))
			{
				return 0;
			}
			return x.Participant.GetHashCode();
		}

		// Token: 0x04000486 RID: 1158
		public static RecipientEqualityComparer Default = new RecipientEqualityComparer();
	}
}
