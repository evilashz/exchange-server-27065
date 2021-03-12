using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000080 RID: 128
	internal static class ReadMessageToolbarUtility
	{
		// Token: 0x0600038D RID: 909 RVA: 0x00020840 File Offset: 0x0001EA40
		public static void BuildHeaderToolbar(UserContext userContext, TextWriter output, bool isEmbeddedItem, Item message, bool isInJunkEmailFolder, bool isSuspectedPhishingItem, bool isLinkEnabled)
		{
			Toolbar toolbar = new Toolbar(output, true);
			toolbar.RenderStart();
			ReadMessageToolbarUtility.RenderButtons(userContext, toolbar, isEmbeddedItem, message, isInJunkEmailFolder, isSuspectedPhishingItem, isLinkEnabled);
			toolbar.RenderEnd();
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00020870 File Offset: 0x0001EA70
		private static void RenderButtons(UserContext userContext, Toolbar toolbar, bool isEmbeddedItem, Item message, bool isInJunkEmailFolder, bool isSuspectedPhishingItem, bool isLinkEnabled)
		{
			bool flag = false;
			if (isInJunkEmailFolder && userContext.IsJunkEmailEnabled && !isSuspectedPhishingItem)
			{
				toolbar.RenderButton(ToolbarButtons.NotJunk);
				toolbar.RenderDivider();
			}
			if (ItemUtility.ShouldRenderSendAgain(message, isEmbeddedItem))
			{
				toolbar.RenderButton(ToolbarButtons.SendAgain);
				flag = true;
			}
			MessageItem messageItem = message as MessageItem;
			if (!isInJunkEmailFolder && (!isSuspectedPhishingItem || isLinkEnabled) && (messageItem == null || messageItem.IsReplyAllowed) && ObjectClass.IsMessage(message.ClassName, false))
			{
				toolbar.RenderButton(ToolbarButtons.Reply);
				toolbar.RenderButton(ToolbarButtons.ReplyAll);
				flag = true;
			}
			if (!ObjectClass.IsOfClass(message.ClassName, "IPM.Conflict.Message"))
			{
				toolbar.RenderButton(ToolbarButtons.Forward);
				flag = true;
			}
			if (flag)
			{
				toolbar.RenderDivider();
			}
			if (!isEmbeddedItem)
			{
				toolbar.RenderButton(ToolbarButtons.MoveImage);
				toolbar.RenderButton(ToolbarButtons.DeleteImage);
				toolbar.RenderDivider();
				if (!isInJunkEmailFolder && userContext.IsJunkEmailEnabled)
				{
					toolbar.RenderButton(ToolbarButtons.Junk);
					toolbar.RenderDivider();
				}
			}
			toolbar.RenderButton(ToolbarButtons.CloseText);
			toolbar.RenderFill();
			if (!isEmbeddedItem)
			{
				toolbar.RenderButton(ToolbarButtons.Previous);
				toolbar.RenderButton(ToolbarButtons.Next);
			}
			toolbar.RenderButton(ToolbarButtons.CloseImage);
		}
	}
}
