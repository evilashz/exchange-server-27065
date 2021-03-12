using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003FE RID: 1022
	public class ReadMessageToolbar : Toolbar
	{
		// Token: 0x06002548 RID: 9544 RVA: 0x000D7A24 File Offset: 0x000D5C24
		internal ReadMessageToolbar(bool isInDeleteItems, bool isEmbeddedItem, Item message, bool isInJunkEmailFolder, bool isSuspectedPhishingItem, bool isLinkEnabled) : this(isInDeleteItems, isEmbeddedItem, message, isInJunkEmailFolder, isSuspectedPhishingItem, isLinkEnabled, false, false, false, false, false)
		{
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x000D7A48 File Offset: 0x000D5C48
		internal ReadMessageToolbar(bool isInDeleteItems, bool isEmbeddedItem, Item message, bool isInJunkEmailFolder, bool isSuspectedPhishingItem, bool isLinkEnabled, bool isMessageReadForm, bool isReplyRestricted, bool isReplyAllRestricted, bool isForwardRestricted, bool isPrintRestricted) : base(ToolbarType.Form)
		{
			this.isInDeleteItems = isInDeleteItems;
			this.isEmbeddedItem = isEmbeddedItem;
			if (message == null)
			{
				throw new ArgumentException("message must not be null");
			}
			this.message = message;
			this.isInJunkEmailFolder = isInJunkEmailFolder;
			this.isSuspectedPhishingItem = isSuspectedPhishingItem;
			this.isLinkEnabled = isLinkEnabled;
			this.isMessageReadForm = isMessageReadForm;
			this.isReplyRestricted = isReplyRestricted;
			this.isReplyAllRestricted = isReplyAllRestricted;
			this.isForwardRestricted = isForwardRestricted;
			this.isPrintRestricted = isPrintRestricted;
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x0600254A RID: 9546 RVA: 0x000D7AC6 File Offset: 0x000D5CC6
		protected override bool IsNarrow
		{
			get
			{
				return this.isMessageReadForm;
			}
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x000D7AD0 File Offset: 0x000D5CD0
		protected override void RenderButtons()
		{
			ToolbarButtonFlags flags = this.isEmbeddedItem ? ToolbarButtonFlags.Disabled : ToolbarButtonFlags.None;
			bool flag = Utilities.IsPublic(this.message);
			bool flag2 = Utilities.IsOtherMailbox(this.message);
			base.RenderHelpButton(ObjectClass.IsSmsMessage(this.message.ClassName) ? HelpIdsLight.DefaultLight.ToString() : HelpIdsLight.MailLight.ToString(), string.Empty);
			if (ItemUtility.ShouldRenderSendAgain(this.message, this.isEmbeddedItem) && !flag2)
			{
				base.RenderButton(ToolbarButtons.SendAgain, flag ? ToolbarButtonFlags.Disabled : ToolbarButtonFlags.None);
			}
			if (this.isInJunkEmailFolder && base.UserContext.IsJunkEmailEnabled && !this.isSuspectedPhishingItem)
			{
				base.RenderButton(ToolbarButtons.NotJunk);
			}
			ToolbarButtonFlags flags2 = ToolbarButtonFlags.None;
			ToolbarButtonFlags flags3 = ToolbarButtonFlags.None;
			if (this.isInJunkEmailFolder || (this.isSuspectedPhishingItem && !this.isLinkEnabled))
			{
				flags2 = ToolbarButtonFlags.Disabled;
				flags3 = ToolbarButtonFlags.Disabled;
			}
			if (!base.UserContext.IsFeatureEnabled(Feature.Tasks) && ObjectClass.IsOfClass(this.message.ClassName, "IPM.Task"))
			{
				flags2 = ToolbarButtonFlags.Disabled;
				flags3 = ToolbarButtonFlags.Disabled;
			}
			bool flag3 = ReadMessageToolbar.IsReplySupported(this.message);
			bool flag4 = base.UserContext.IsSmsEnabled && ObjectClass.IsSmsMessage(this.message.ClassName);
			if (this.isReplyRestricted)
			{
				flags2 = ToolbarButtonFlags.Disabled;
			}
			if (this.isReplyAllRestricted)
			{
				flags3 = ToolbarButtonFlags.Disabled;
			}
			if (flag3)
			{
				base.RenderButton(flag4 ? ToolbarButtons.ReplySms : ToolbarButtons.Reply, flags2);
				base.RenderButton(flag4 ? ToolbarButtons.ReplyAllSms : ToolbarButtons.ReplyAll, flags3);
			}
			ToolbarButtonFlags flags4 = ToolbarButtonFlags.None;
			if (ObjectClass.IsOfClass(this.message.ClassName, "IPM.Note.Microsoft.Approval.Request") || this.isForwardRestricted || this.isInJunkEmailFolder || (this.isSuspectedPhishingItem && !this.isLinkEnabled))
			{
				flags4 = ToolbarButtonFlags.Disabled;
			}
			if (!ObjectClass.IsOfClass(this.message.ClassName, "IPM.Conflict.Message"))
			{
				base.RenderButton(flag4 ? ToolbarButtons.ForwardSms : ToolbarButtons.Forward, flags4);
			}
			bool flag5 = this.message is CalendarItemBase;
			bool flag6 = ItemUtility.UserCanEditItem(this.message);
			if (base.UserContext.IsInstantMessageEnabled() && (!flag5 || (flag5 && flag3)))
			{
				base.RenderButton(ToolbarButtons.Chat, ToolbarButtonFlags.Disabled);
			}
			MessageItem messageItem = this.message as MessageItem;
			bool flag7 = messageItem != null && messageItem.IsDraft;
			ToolbarButtonFlags flags5 = (flag6 && !flag7) ? ToolbarButtonFlags.None : ToolbarButtonFlags.Disabled;
			if (!this.isEmbeddedItem && base.UserContext.ExchangePrincipal.RecipientTypeDetails == RecipientTypeDetails.DiscoveryMailbox)
			{
				base.RenderButton(ToolbarButtons.MessageNoteInToolbar);
			}
			if (!flag5 && !this.isInDeleteItems && !this.isEmbeddedItem)
			{
				base.RenderButton(ToolbarButtons.Flag, flags5);
			}
			ToolbarButtonFlags flags6 = flag6 ? ToolbarButtonFlags.None : ToolbarButtonFlags.Disabled;
			if (!this.isEmbeddedItem)
			{
				bool flag8 = true;
				if (flag5)
				{
					CalendarItemBase calendarItemBase = (CalendarItemBase)this.message;
					flag8 = (CalendarItemType.Occurrence != calendarItemBase.CalendarItemType && CalendarItemType.Exception != calendarItemBase.CalendarItemType);
				}
				if (flag8)
				{
					base.RenderButton(ToolbarButtons.Categories, flags6);
				}
			}
			if (!flag5)
			{
				base.RenderButton(ToolbarButtons.MessageDetails);
			}
			base.RenderButton(ToolbarButtons.Print, this.isPrintRestricted ? ToolbarButtonFlags.Disabled : ToolbarButtonFlags.None);
			if (base.UserContext.IsFeatureEnabled(Feature.Rules) && this.isMessageReadForm && !flag2 && !this.isEmbeddedItem)
			{
				base.RenderButton(ToolbarButtons.CreateRule, (base.UserContext.IsWebPartRequest || flag) ? ToolbarButtonFlags.Disabled : ToolbarButtonFlags.None);
			}
			bool flag9;
			if (flag5)
			{
				flag9 = CalendarUtilities.UserCanDeleteCalendarItem((CalendarItemBase)this.message);
			}
			else
			{
				flag9 = ItemUtility.UserCanDeleteItem(this.message);
			}
			ToolbarButtonFlags flags7 = (!flag9 || this.isEmbeddedItem) ? ToolbarButtonFlags.Disabled : ToolbarButtonFlags.None;
			base.RenderButton(ToolbarButtons.Delete, flags7);
			if (!this.isEmbeddedItem && !flag5)
			{
				base.RenderButton(ToolbarButtons.Move);
			}
			if (!flag5)
			{
				base.RenderButton(ToolbarButtons.Previous, flags);
				base.RenderButton(ToolbarButtons.Next, flags);
			}
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x000D7EE4 File Offset: 0x000D60E4
		private static bool IsReplySupported(Item item)
		{
			MessageItem messageItem;
			if ((messageItem = (item as MessageItem)) != null)
			{
				return messageItem.IsReplyAllowed;
			}
			CalendarItemBase calendarItemBase;
			return (calendarItemBase = (item as CalendarItemBase)) != null && calendarItemBase.IsMeeting;
		}

		// Token: 0x040019B4 RID: 6580
		private Item message;

		// Token: 0x040019B5 RID: 6581
		private bool isEmbeddedItem;

		// Token: 0x040019B6 RID: 6582
		private bool isInDeleteItems;

		// Token: 0x040019B7 RID: 6583
		private bool isMessageReadForm;

		// Token: 0x040019B8 RID: 6584
		private bool isInJunkEmailFolder;

		// Token: 0x040019B9 RID: 6585
		private bool isSuspectedPhishingItem;

		// Token: 0x040019BA RID: 6586
		private bool isLinkEnabled = true;

		// Token: 0x040019BB RID: 6587
		private bool isReplyRestricted;

		// Token: 0x040019BC RID: 6588
		private bool isReplyAllRestricted;

		// Token: 0x040019BD RID: 6589
		private bool isForwardRestricted;

		// Token: 0x040019BE RID: 6590
		private bool isPrintRestricted;
	}
}
