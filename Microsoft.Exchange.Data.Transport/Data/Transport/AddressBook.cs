using System;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x02000079 RID: 121
	public abstract class AddressBook
	{
		// Token: 0x0600029B RID: 667 RVA: 0x000071DE File Offset: 0x000053DE
		internal AddressBook()
		{
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600029C RID: 668 RVA: 0x000071E6 File Offset: 0x000053E6
		// (set) Token: 0x0600029D RID: 669 RVA: 0x000071EE File Offset: 0x000053EE
		internal object RecipientCache
		{
			get
			{
				return this.recipientCache;
			}
			set
			{
				this.recipientCache = value;
			}
		}

		// Token: 0x0600029E RID: 670
		public abstract bool Contains(RoutingAddress smtpAddress);

		// Token: 0x0600029F RID: 671
		public abstract AddressBookEntry Find(RoutingAddress smtpAddress);

		// Token: 0x060002A0 RID: 672
		public abstract ReadOnlyCollection<AddressBookEntry> Find(params RoutingAddress[] smtpAddresses);

		// Token: 0x060002A1 RID: 673
		public abstract ReadOnlyCollection<AddressBookEntry> Find(EnvelopeRecipientCollection recipients);

		// Token: 0x060002A2 RID: 674
		public abstract bool IsMemberOf(RoutingAddress recipientSmtpAddress, RoutingAddress groupSmtpAddress);

		// Token: 0x060002A3 RID: 675
		public abstract bool IsSameRecipient(RoutingAddress proxyAddressA, RoutingAddress proxyAddressB);

		// Token: 0x060002A4 RID: 676
		public abstract bool IsInternal(RoutingAddress address);

		// Token: 0x060002A5 RID: 677
		public abstract bool IsInternal(RoutingDomain domain);

		// Token: 0x060002A6 RID: 678
		public abstract bool IsInternalTo(RoutingAddress address, RoutingAddress organizationAddress);

		// Token: 0x060002A7 RID: 679
		public abstract bool IsInternalTo(RoutingAddress address, RoutingDomain organizationDomain);

		// Token: 0x040001E1 RID: 481
		private object recipientCache;
	}
}
