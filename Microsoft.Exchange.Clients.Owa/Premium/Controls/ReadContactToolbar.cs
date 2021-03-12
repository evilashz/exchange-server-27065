using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003FC RID: 1020
	public class ReadContactToolbar : Toolbar
	{
		// Token: 0x06002542 RID: 9538 RVA: 0x000D7877 File Offset: 0x000D5A77
		internal ReadContactToolbar(Contact contact) : base(ToolbarType.Form)
		{
			if (contact == null)
			{
				throw new ArgumentNullException("contact");
			}
			this.contact = contact;
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x000D7898 File Offset: 0x000D5A98
		protected override void RenderButtons()
		{
			ToolbarButtonFlags flags = ToolbarButtonFlags.None;
			if (!ReadContactToolbar.CanMailToContact(this.contact))
			{
				flags = ToolbarButtonFlags.Disabled;
			}
			base.RenderButton(ToolbarButtons.NewMessageToContact, flags);
			if (base.UserContext.IsFeatureEnabled(Feature.Calendar))
			{
				base.RenderButton(ToolbarButtons.NewMeetingRequestToContact, flags);
			}
			if (base.UserContext.IsSmsEnabled)
			{
				base.RenderButton(ToolbarButtons.SendATextMessage);
			}
			ToolbarButtonFlags flags2 = ToolbarButtonFlags.None;
			if (!ItemUtility.UserCanDeleteItem(this.contact))
			{
				flags2 = ToolbarButtonFlags.Disabled;
			}
			base.RenderButton(ToolbarButtons.Delete, flags2);
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x000D791A File Offset: 0x000D5B1A
		private static bool CanMailToContact(ContactBase contactBase)
		{
			return contactBase is DistributionList || ReadContactToolbar.ContactHasEmailAddress(contactBase, ContactSchema.Email1) || ReadContactToolbar.ContactHasEmailAddress(contactBase, ContactSchema.Email2) || ReadContactToolbar.ContactHasEmailAddress(contactBase, ContactSchema.Email3);
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x000D794C File Offset: 0x000D5B4C
		private static bool ContactHasEmailAddress(ContactBase contactBase, PropertyDefinition emailProperty)
		{
			Participant participant = contactBase.TryGetProperty(emailProperty) as Participant;
			return participant != null && !string.IsNullOrEmpty(participant.EmailAddress);
		}

		// Token: 0x040019B1 RID: 6577
		private Contact contact;
	}
}
