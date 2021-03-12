using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004CF RID: 1231
	[OwaEventNamespace("ReadMessage")]
	internal sealed class ReadMessageEventHandler : MessageEventHandler
	{
		// Token: 0x06002EF3 RID: 12019 RVA: 0x0010E35A File Offset: 0x0010C55A
		public static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(ReadMessageEventHandler));
		}

		// Token: 0x06002EF4 RID: 12020 RVA: 0x0010E36C File Offset: 0x0010C56C
		[OwaEventParameter("Subj", typeof(string), false, true)]
		[OwaEventParameter("CK", typeof(string), false, true)]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("AudioNotes", typeof(string), false, true)]
		[OwaEventParameter("AlWbBcn", typeof(bool), false, true)]
		[OwaEventParameter("StLnkEnbl", typeof(bool), false, true)]
		[OwaEvent("Save")]
		public void Save()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ReadMessageEventHandler.SaveMessage");
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("Id");
			string changeKey = (string)base.GetParameter("CK");
			using (MessageItem item = Utilities.GetItem<MessageItem>(base.UserContext, owaStoreObjectId, changeKey, true, new PropertyDefinition[0]))
			{
				ExTraceGlobals.MailTracer.TraceDebug((long)this.GetHashCode(), "Saving message");
				base.UpdateReadMessage(item);
				Utilities.SaveItem(item, true);
				item.Load();
				base.WriteChangeKey(item);
			}
		}

		// Token: 0x06002EF5 RID: 12021 RVA: 0x0010E414 File Offset: 0x0010C614
		[OwaEventVerb(OwaEventVerb.Get)]
		[OwaEvent("LMD")]
		[OwaEventParameter("Id", typeof(string))]
		public void LoadMessageDetails()
		{
			string str = (string)base.GetParameter("Id");
			string path = "forms/premium/messagedetailsdialog.aspx?" + str;
			this.HttpContext.Server.Execute(path, this.Writer);
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x0010E458 File Offset: 0x0010C658
		[OwaEvent("SRR")]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		public void SendReadReceipt()
		{
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("Id");
			using (MessageItem item = Utilities.GetItem<MessageItem>(base.UserContext, owaStoreObjectId, true, new PropertyDefinition[0]))
			{
				item.SendReadReceipt();
			}
			this.Writer.Write(LocalizedStrings.GetHtmlEncoded(641302712));
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x0010E4C4 File Offset: 0x0010C6C4
		[OwaEvent("RMR")]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		public void RemoveRestriction()
		{
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("Id");
			using (MessageItem item = Utilities.GetItem<MessageItem>(base.UserContext, owaStoreObjectId, false, new PropertyDefinition[0]))
			{
				Utilities.IrmRemoveRestriction(item, base.UserContext);
			}
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x0010E520 File Offset: 0x0010C720
		[OwaEventParameter("Vt", typeof(string))]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		[OwaEventParameter("CK", typeof(string))]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId))]
		[OwaEvent("ApvEd")]
		public void ApprovalEditResponse()
		{
			this.ProcessApprovalResponse(true);
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x0010E529 File Offset: 0x0010C729
		[OwaEvent("ApvSnd")]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		[OwaEventParameter("CK", typeof(string))]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("Vt", typeof(string))]
		public void ApprovalSendResponseNow()
		{
			this.ProcessApprovalResponse(false);
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x0010E534 File Offset: 0x0010C734
		private void ProcessApprovalResponse(bool isEdit)
		{
			MessageItem requestItem;
			MessageItem messageItem = requestItem = base.GetRequestItem<MessageItem>(ReadMessageEventHandler.ApprovalPrefetchProperties);
			try
			{
				if (!Utilities.IsValidUndecidedApprovalRequest(messageItem))
				{
					throw new OwaInvalidRequestException("The approval request was invalid or was already decided");
				}
				string[] array = (string[])messageItem.VotingInfo.GetOptionsList();
				string text = (string)base.GetParameter("Vt");
				if (string.IsNullOrEmpty(text))
				{
					throw new OwaInvalidRequestException("The approval vote was not supplied.");
				}
				if (array == null || Array.IndexOf<string>(array, text) == -1)
				{
					throw new OwaInvalidRequestException("The attempted approval vote was invalid for the approval request.");
				}
				BodyFormat replyForwardBodyFormat = ReplyForwardUtilities.GetReplyForwardBodyFormat(messageItem, base.UserContext);
				OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("fId");
				MessageItem messageItem3;
				MessageItem messageItem2 = messageItem3 = messageItem.CreateVotingResponse(string.Empty, replyForwardBodyFormat, base.UserContext.TryGetMyDefaultFolderId(DefaultFolderType.Drafts), text);
				try
				{
					messageItem2.Save(SaveMode.ResolveConflicts);
					messageItem2.Load();
					if (!isEdit)
					{
						messageItem2.Send();
					}
					else
					{
						base.WriteNewItemId(messageItem2);
					}
				}
				finally
				{
					if (messageItem3 != null)
					{
						((IDisposable)messageItem3).Dispose();
					}
				}
			}
			finally
			{
				if (requestItem != null)
				{
					((IDisposable)requestItem).Dispose();
				}
			}
		}

		// Token: 0x040020DB RID: 8411
		public const string EventNamespace = "ReadMessage";

		// Token: 0x040020DC RID: 8412
		public const string MethodLoadMessageDetails = "LMD";

		// Token: 0x040020DD RID: 8413
		public const string MethodSendReadReceipt = "SRR";

		// Token: 0x040020DE RID: 8414
		public const string MethodApprovalEditResponse = "ApvEd";

		// Token: 0x040020DF RID: 8415
		public const string MethodApprovalSendResponseNow = "ApvSnd";

		// Token: 0x040020E0 RID: 8416
		public const string Vote = "Vt";

		// Token: 0x040020E1 RID: 8417
		public const string MethodRemoveRestriction = "RMR";

		// Token: 0x040020E2 RID: 8418
		private static readonly StorePropertyDefinition[] ApprovalPrefetchProperties = new StorePropertyDefinition[]
		{
			MessageItemSchema.ApprovalDecisionTime,
			MessageItemSchema.ApprovalDecision,
			MessageItemSchema.ApprovalDecisionMaker
		};
	}
}
