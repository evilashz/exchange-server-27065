using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004CE RID: 1230
	[OwaEventNamespace("ReadConversation")]
	internal sealed class ReadConversationEventHandler : OwaEventHandlerBase
	{
		// Token: 0x06002EEF RID: 12015 RVA: 0x0010E089 File Offset: 0x0010C289
		public static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(ReadConversationEventHandler));
		}

		// Token: 0x06002EF0 RID: 12016 RVA: 0x0010E09C File Offset: 0x0010C29C
		[OwaEventParameter("CnvId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("IPId", typeof(OwaStoreObjectId), true)]
		[OwaEvent("ExpIP")]
		public void ExpandItemParts()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ReadConversationEventHandler.ExpandItemParts");
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("CnvId");
			OwaStoreObjectId[] array = (OwaStoreObjectId[])base.GetParameter("IPId");
			Conversation conversation = ConversationUtilities.LoadConversation(base.UserContext, owaStoreObjectId, ItemPartWriter.GetRequestedProperties());
			List<StoreObjectId> localItemIds = ConversationUtilities.GetLocalItemIds((MailboxSession)owaStoreObjectId.GetSession(base.UserContext), conversation, owaStoreObjectId.ParentFolderId);
			ConversationUtilities.MarkLocalNodes(conversation, localItemIds);
			conversation.OnBeforeItemLoad += ItemPartWriter.OnBeforeItemLoad;
			foreach (OwaStoreObjectId owaStoreObjectId2 in array)
			{
				this.SanitizingWriter.Write("<div id=\"");
				this.SanitizingWriter.Write(owaStoreObjectId2.ToString());
				this.SanitizingWriter.Write("\">");
				IConversationTreeNode conversationTreeNode;
				if (!conversation.ConversationTree.TryGetConversationTreeNode(owaStoreObjectId2.StoreObjectId, out conversationTreeNode))
				{
					this.Writer.Write("<div id=divExp itemNotFound=1>&nbsp;</div>");
				}
				else
				{
					MailboxSession session = (MailboxSession)owaStoreObjectId.GetSession(base.UserContext);
					ConversationUtilities.SortPropertyBags(conversationTreeNode, localItemIds, session);
					using (ItemPartWriter writer = ItemPartWriter.GetWriter(base.UserContext, this.Writer, owaStoreObjectId, conversation, conversationTreeNode))
					{
						writer.RenderExpandedItemPart(false, false, null);
					}
				}
				this.SanitizingWriter.Write("</div>");
			}
		}

		// Token: 0x06002EF1 RID: 12017 RVA: 0x0010E214 File Offset: 0x0010C414
		[OwaEventParameter("CnvId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("ExpIds", typeof(OwaStoreObjectId), true)]
		[OwaEventParameter("ExpInternetMIds", typeof(int), true)]
		[OwaEvent("Rfrsh")]
		public void Refresh()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ReadConversationEventHandler.Refresh");
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("CnvId");
			OwaStoreObjectId[] expandedIds = (OwaStoreObjectId[])base.GetParameter("ExpIds");
			int[] expandedInternetMIds = (int[])base.GetParameter("ExpInternetMIds");
			Conversation conversation = ConversationUtilities.LoadConversation(base.UserContext, owaStoreObjectId, ItemPartWriter.GetRequestedProperties());
			conversation.TrimToNewest(Globals.MaxItemsInConversationReadingPane);
			MailboxSession session = (MailboxSession)owaStoreObjectId.GetSession(base.UserContext);
			List<StoreObjectId> localItemIds = ConversationUtilities.GetLocalItemIds(session, conversation, owaStoreObjectId.ParentFolderId);
			conversation.OnBeforeItemLoad += ItemPartWriter.OnBeforeItemLoad;
			this.Writer.Write("<div id=divRfrsh");
			RenderingUtilities.RenderExpando(this.Writer, "iGC", ConversationUtilities.GetGlobalCount(conversation));
			RenderingUtilities.RenderExpando(this.Writer, "iC", localItemIds.Count);
			RenderingUtilities.RenderExpando(this.Writer, "iSort", (int)base.UserContext.UserOptions.ConversationSortOrder);
			this.Writer.Write(">");
			ConversationUtilities.RenderItemParts(this.Writer, base.UserContext, owaStoreObjectId, conversation, expandedIds, expandedInternetMIds, localItemIds, null, false);
			this.Writer.Write("</div>");
		}

		// Token: 0x040020D4 RID: 8404
		public const string EventNamespace = "ReadConversation";

		// Token: 0x040020D5 RID: 8405
		public const string MethodExpandItemParts = "ExpIP";

		// Token: 0x040020D6 RID: 8406
		public const string MethodRefresh = "Rfrsh";

		// Token: 0x040020D7 RID: 8407
		public const string ConversationIdParameter = "CnvId";

		// Token: 0x040020D8 RID: 8408
		public const string ItemPartId = "IPId";

		// Token: 0x040020D9 RID: 8409
		public const string ExpandedItemPartInternetMIds = "ExpInternetMIds";

		// Token: 0x040020DA RID: 8410
		public const string ExpandedItemPartIds = "ExpIds";
	}
}
