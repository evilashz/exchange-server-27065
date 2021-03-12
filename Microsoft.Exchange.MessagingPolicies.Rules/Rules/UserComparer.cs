using System;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000047 RID: 71
	internal class UserComparer : IStringComparer
	{
		// Token: 0x060002CA RID: 714 RVA: 0x000105E6 File Offset: 0x0000E7E6
		public UserComparer(AddressBook addressBook)
		{
			this.addressBook = addressBook;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000105F8 File Offset: 0x0000E7F8
		public bool Equals(string userX, string userY)
		{
			if (this.addressBook == null || userX == null || userY == null)
			{
				return false;
			}
			bool result;
			try
			{
				result = this.addressBook.IsSameRecipient((RoutingAddress)userX, (RoutingAddress)userY);
			}
			catch (Exception ex)
			{
				throw new TransportRulePermanentException(string.Format("Error matching recipients. Recipient1: '{0}', Recipient2: '{1}'. Inner error message: '{2}'", userX, userY, ex.Message), ex);
			}
			return result;
		}

		// Token: 0x040001D0 RID: 464
		private AddressBook addressBook;
	}
}
