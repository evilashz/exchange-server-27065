using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Approval;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000064 RID: 100
	internal class DecisionHandler
	{
		// Token: 0x060003C4 RID: 964 RVA: 0x0001096C File Offset: 0x0000EB6C
		private DecisionHandler(MessageItem messageItem, string sender, bool needDisposing)
		{
			this.messageItem = messageItem;
			this.sender = sender;
			this.needDisposing = needDisposing;
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x00010989 File Offset: 0x0000EB89
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x00010990 File Offset: 0x0000EB90
		public static HashSet<string> ApproveTextList
		{
			get
			{
				return DecisionHandler.approveTextList;
			}
			set
			{
				DecisionHandler.approveTextList = value;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00010998 File Offset: 0x0000EB98
		public bool NeedDisposing
		{
			get
			{
				return this.needDisposing;
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000109A0 File Offset: 0x0000EBA0
		public static bool TryCreate(MessageItem messageItem, string sender, OrganizationId organizationId, out DecisionHandler decisionHandler)
		{
			decisionHandler = null;
			MessageItem messageItem2;
			bool flag;
			if (!DecisionHandler.IsDecision(messageItem, out messageItem2, out flag))
			{
				return false;
			}
			decisionHandler = new DecisionHandler(messageItem2, sender, flag);
			return true;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x000109CC File Offset: 0x0000EBCC
		public ApprovalEngine.ApprovalProcessResults Process()
		{
			MailboxSession mailboxSession = (MailboxSession)this.messageItem.Session;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox);
			VersionedId correlatedItem = this.messageItem.VotingInfo.GetCorrelatedItem(defaultFolderId);
			stopwatch.Stop();
			long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
			if (correlatedItem == null)
			{
				DecisionHandler.diag.TraceDebug((long)this.GetHashCode(), "Initiation message not found");
				return new ApprovalEngine.ApprovalProcessResults(ApprovalEngine.ProcessResult.InitiationNotFoundForDecision, elapsedMilliseconds);
			}
			string existingDecisionMakerAddress;
			ApprovalStatus? existingApprovalStatus;
			ExDateTime? existingDecisionTime;
			DecisionConflict conflict;
			if (DecisionHandler.ApproveTextList.Contains(this.messageItem.VotingInfo.Response))
			{
				conflict = ApprovalProcessor.ApproveRequest(mailboxSession, correlatedItem.ObjectId, (SmtpAddress)this.sender, this.messageItem.Body, out existingDecisionMakerAddress, out existingApprovalStatus, out existingDecisionTime);
			}
			else
			{
				conflict = ApprovalProcessor.RejectRequest(mailboxSession, correlatedItem.ObjectId, (SmtpAddress)this.sender, this.messageItem.Body, out existingDecisionMakerAddress, out existingApprovalStatus, out existingDecisionTime);
			}
			return DecisionHandler.GetApprovalProcessResults(conflict, existingDecisionMakerAddress, existingApprovalStatus, existingDecisionTime, elapsedMilliseconds);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00010AC1 File Offset: 0x0000ECC1
		public void Dispose()
		{
			if (this.messageItem != null && this.needDisposing)
			{
				this.messageItem.Dispose();
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00010AE0 File Offset: 0x0000ECE0
		private static HashSet<string> CreateApproveTextList()
		{
			DecisionHandler.ApproveTextList = new HashSet<string>();
			foreach (CultureInfo formatProvider in LanguagePackInfo.GetInstalledLanguagePackCultures(LanguagePackType.Client))
			{
				DecisionHandler.ApproveTextList.TryAdd(SystemMessages.ApproveButtonText.ToString(formatProvider));
			}
			return DecisionHandler.ApproveTextList;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00010B30 File Offset: 0x0000ED30
		private static ApprovalEngine.ApprovalProcessResults GetApprovalProcessResults(DecisionConflict conflict, string existingDecisionMakerAddress, ApprovalStatus? existingApprovalStatus, ExDateTime? existingDecisionTime, long messageSearchElapsedMilliseconds)
		{
			ApprovalEngine.ApprovalProcessResults approvalProcessResults = new ApprovalEngine.ApprovalProcessResults();
			approvalProcessResults.InitiationMessageSearchTimeMilliseconds = messageSearchElapsedMilliseconds;
			if (conflict == DecisionConflict.NoConflict || conflict == DecisionConflict.SameApproverAndDecision || conflict == DecisionConflict.DifferentApproverSameDecision)
			{
				approvalProcessResults.ProcessResults = ApprovalEngine.ProcessResult.DecisionMarked;
			}
			else if (conflict == DecisionConflict.MissingItem)
			{
				approvalProcessResults.ProcessResults = ApprovalEngine.ProcessResult.InitiationNotFoundForDecision;
			}
			else if (conflict == DecisionConflict.Unauthorized)
			{
				approvalProcessResults.ProcessResults = ApprovalEngine.ProcessResult.UnauthorizedMessage;
			}
			else
			{
				approvalProcessResults.ProcessResults = ApprovalEngine.ProcessResult.DecisionAlreadyMade;
				approvalProcessResults.ExistingDecisionMakerAddress = existingDecisionMakerAddress;
				approvalProcessResults.ExistingDecisionTime = existingDecisionTime;
				approvalProcessResults.ExistingApprovalStatus = existingApprovalStatus;
			}
			return approvalProcessResults;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00010B98 File Offset: 0x0000ED98
		private static bool IsDecision(MessageItem messageItem, out MessageItem decisionMessageItem, out bool needsDisposing)
		{
			decisionMessageItem = messageItem;
			needsDisposing = false;
			if (messageItem == null || messageItem.Session == null)
			{
				return false;
			}
			if (!string.IsNullOrEmpty(messageItem.VotingInfo.Response))
			{
				return true;
			}
			if (messageItem.AttachmentCollection != null && messageItem.AttachmentCollection.Count == 1)
			{
				IList<AttachmentHandle> handles = messageItem.AttachmentCollection.GetHandles();
				if (handles != null && handles.Count > 0)
				{
					using (ItemAttachment itemAttachment = messageItem.AttachmentCollection.Open(handles[0], AttachmentType.EmbeddedMessage) as ItemAttachment)
					{
						if (itemAttachment != null)
						{
							decisionMessageItem = itemAttachment.GetItemAsMessage();
							needsDisposing = true;
							bool flag = decisionMessageItem != null && decisionMessageItem.Session != null && !string.IsNullOrEmpty(decisionMessageItem.VotingInfo.Response);
							if (decisionMessageItem != null && !flag)
							{
								decisionMessageItem.Dispose();
								needsDisposing = false;
							}
							return flag;
						}
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x040001FE RID: 510
		private static readonly Microsoft.Exchange.Diagnostics.Trace diag = ExTraceGlobals.ApprovalAgentTracer;

		// Token: 0x040001FF RID: 511
		private static HashSet<string> approveTextList = DecisionHandler.CreateApproveTextList();

		// Token: 0x04000200 RID: 512
		private readonly bool needDisposing;

		// Token: 0x04000201 RID: 513
		private readonly string sender;

		// Token: 0x04000202 RID: 514
		private MessageItem messageItem;
	}
}
