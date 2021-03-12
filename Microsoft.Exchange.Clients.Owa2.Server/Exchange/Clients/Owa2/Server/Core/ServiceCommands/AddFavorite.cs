using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands
{
	// Token: 0x02000240 RID: 576
	internal class AddFavorite
	{
		// Token: 0x060015B5 RID: 5557 RVA: 0x0004D584 File Offset: 0x0004B784
		internal AddFavorite(IXSOFactory xsoFactory, IMailboxSession mailboxSession, InstantMessageBuddy imBuddy)
		{
			if (xsoFactory == null)
			{
				throw new ArgumentNullException("xsoFactory");
			}
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			if (imBuddy == null)
			{
				throw new ArgumentNullException("imBuddy");
			}
			if (imBuddy.EmailAddress == null && string.IsNullOrEmpty(imBuddy.SipUri))
			{
				throw new ArgumentException("Either EmailAddress or SipUri is mandatory for imBuddy");
			}
			this.xso = xsoFactory;
			this.session = mailboxSession;
			this.utilities = new UnifiedContactStoreUtilities(this.session, this.xso);
			this.buddy = imBuddy;
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x0004D610 File Offset: 0x0004B810
		internal bool Execute()
		{
			EmailAddress emailAddress = null;
			if (this.buddy.EmailAddress != null)
			{
				emailAddress = new EmailAddress
				{
					Address = this.buddy.EmailAddress.EmailAddress,
					Name = this.buddy.EmailAddress.Name,
					OriginalDisplayName = this.buddy.EmailAddress.OriginalDisplayName,
					RoutingType = this.buddy.EmailAddress.RoutingType
				};
			}
			StoreObjectId storeObjectId;
			PersonId personId;
			this.utilities.RetrieveOrCreateContact(this.buddy.SipUri, emailAddress, this.buddy.DisplayName, this.buddy.FirstName, this.buddy.LastName, out storeObjectId, out personId);
			if (storeObjectId != null)
			{
				StoreObjectId systemPdlId = this.utilities.GetSystemPdlId(UnifiedContactStoreUtilities.FavoritesPdlDisplayName, "IPM.DistList.MOC.Favorites");
				if (systemPdlId != null)
				{
					this.utilities.AddContactToGroup(storeObjectId, this.buddy.DisplayName, systemPdlId);
					this.SetIsFavoriteFlag(storeObjectId);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x0004D70C File Offset: 0x0004B90C
		private void SetIsFavoriteFlag(StoreObjectId contactId)
		{
			using (IContact contact = this.xso.BindToContact(this.session, contactId, null))
			{
				contact.OpenAsReadWrite();
				contact.IsFavorite = true;
				contact.Save(SaveMode.ResolveConflicts);
			}
		}

		// Token: 0x04000BFF RID: 3071
		private readonly UnifiedContactStoreUtilities utilities;

		// Token: 0x04000C00 RID: 3072
		private readonly IXSOFactory xso;

		// Token: 0x04000C01 RID: 3073
		private readonly IMailboxSession session;

		// Token: 0x04000C02 RID: 3074
		private readonly InstantMessageBuddy buddy;
	}
}
