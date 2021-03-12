using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Transport.StoreDriver
{
	// Token: 0x020000AD RID: 173
	internal abstract class StoreDriverDeliveryEventArgs : StoreDriverEventArgs
	{
		// Token: 0x060003CC RID: 972 RVA: 0x00008D59 File Offset: 0x00006F59
		internal StoreDriverDeliveryEventArgs()
		{
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003CD RID: 973
		public abstract DeliverableMailItem MailItem { get; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003CE RID: 974
		public abstract RoutingAddress RecipientAddress { get; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003CF RID: 975
		public abstract string MessageClass { get; }

		// Token: 0x060003D0 RID: 976
		public abstract void AddAgentInfo(string agentName, string eventName, List<KeyValuePair<string, string>> data);
	}
}
