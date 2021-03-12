using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000302 RID: 770
	public sealed class AddressBookContextMenu : ContextMenu
	{
		// Token: 0x06001D27 RID: 7463 RVA: 0x000A7BB2 File Offset: 0x000A5DB2
		public AddressBookContextMenu(UserContext userContext, bool isInAddressBook, bool isFromContact) : base("divVwm", userContext)
		{
			this.isFromContactList = isFromContact;
			this.isInAddressBook = isInAddressBook;
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x000A7BD0 File Offset: 0x000A5DD0
		protected override void RenderMenuItems(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			base.RenderMenuItem(output, 197744374, ThemeFileId.None, "divO", "open");
			ContextMenu.RenderMenuDivider(output, "divS1");
			base.RenderMenuItem(output, -747517193, ThemeFileId.EMailContact, "divNM", "nmsgct");
			if (base.UserContext.IsFeatureEnabled(Feature.Calendar))
			{
				base.RenderMenuItem(output, -1596894910, ThemeFileId.Appointment, "divNMR", "nmrgct");
			}
			if (base.UserContext.IsInstantMessageEnabled())
			{
				base.RenderMenuItem(output, -124986716, ThemeFileId.Chat, "divCht", "chat");
				ContextMenu.RenderMenuDivider(output, "divS4");
				base.RenderMenuItem(output, 1457127060, ThemeFileId.AddBuddy, "divAdBdyLst", "ablst");
				base.RenderMenuItem(output, -205408082, ThemeFileId.RemoveBuddy, "divRmBdyLst", "rmblst");
			}
			if (base.UserContext.IsSmsEnabled)
			{
				base.RenderMenuItem(output, -843330244, ThemeFileId.Sms, "divSndSms", "sendsms");
			}
			if (!this.isFromContactList && base.UserContext.IsFeatureEnabled(Feature.Contacts))
			{
				ContextMenu.RenderMenuDivider(output, "divS2");
				base.RenderMenuItem(output, 1775424225, ThemeFileId.AddToContacts, "divACT", "addconts");
			}
			if (this.isFromContactList)
			{
				ContextMenu.RenderMenuDivider(output, "divS3");
				base.RenderMenuItem(output, 438661106, ThemeFileId.ForwardAsAttachment, "divFIA", "fwia");
			}
			if (!this.isInAddressBook)
			{
				ContextMenu.RenderMenuDivider(output, "divS5");
				base.RenderMenuItem(output, 1381996313, ThemeFileId.Delete, "divD", "delete");
			}
		}

		// Token: 0x04001565 RID: 5477
		private const string ContextMenuId = "divVwm";

		// Token: 0x04001566 RID: 5478
		private readonly bool isFromContactList;

		// Token: 0x04001567 RID: 5479
		private readonly bool isInAddressBook;
	}
}
