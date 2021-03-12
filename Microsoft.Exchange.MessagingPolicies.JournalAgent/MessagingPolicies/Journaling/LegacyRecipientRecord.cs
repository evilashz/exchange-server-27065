using System;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport.Categorizer;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x02000027 RID: 39
	internal class LegacyRecipientRecord
	{
		// Token: 0x040000D2 RID: 210
		public string Address;

		// Token: 0x040000D3 RID: 211
		public RecipientP2Type P2Type;

		// Token: 0x040000D4 RID: 212
		public HistoryRecord History;
	}
}
