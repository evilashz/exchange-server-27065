using System;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000048 RID: 72
	internal class MembershipChecker : IStringComparer
	{
		// Token: 0x060002CC RID: 716 RVA: 0x0001065C File Offset: 0x0000E85C
		public MembershipChecker(AddressBook addressBook)
		{
			this.addressBook = addressBook;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0001066B File Offset: 0x0000E86B
		public bool Equals(string recipientAddress, string groupAddress)
		{
			return this.addressBook != null && recipientAddress != null && groupAddress != null && this.addressBook.IsMemberOf((RoutingAddress)recipientAddress, (RoutingAddress)groupAddress);
		}

		// Token: 0x040001D1 RID: 465
		private AddressBook addressBook;
	}
}
