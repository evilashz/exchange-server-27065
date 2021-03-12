using System;
using System.Security.Principal;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x0200007A RID: 122
	public abstract class AddressBookEntry
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x000071F7 File Offset: 0x000053F7
		internal AddressBookEntry()
		{
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002A9 RID: 681
		public abstract RoutingAddress PrimaryAddress { get; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002AA RID: 682
		public abstract bool RequiresAuthentication { get; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002AB RID: 683
		public abstract bool AntispamBypass { get; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002AC RID: 684
		public abstract RecipientType RecipientType { get; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002AD RID: 685
		public abstract SecurityIdentifier UserAccountSid { get; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002AE RID: 686
		public abstract SecurityIdentifier MasterAccountSid { get; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002AF RID: 687
		public abstract string WindowsLiveId { get; }

		// Token: 0x060002B0 RID: 688
		public abstract int GetSpamConfidenceLevelThreshold(SpamAction action, int defaultValue);

		// Token: 0x060002B1 RID: 689
		public abstract bool IsSafeSender(RoutingAddress senderAddress);

		// Token: 0x060002B2 RID: 690
		public abstract bool IsSafeRecipient(RoutingAddress recipientAddress);

		// Token: 0x060002B3 RID: 691
		public abstract bool IsBlockedSender(RoutingAddress senderAddress);
	}
}
