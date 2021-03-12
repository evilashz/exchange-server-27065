using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x0200005F RID: 95
	internal class ApprovalRequestUpdater
	{
		// Token: 0x060003AF RID: 943 RVA: 0x0001031C File Offset: 0x0000E51C
		public static ApprovalRequestUpdater.Result TryUpdateExistingApprovalRequest(MessageItem updateMessage)
		{
			updateMessage.Load(ApprovalRequestUpdater.ApprovalRequestUpdateProperties);
			int? valueAsNullable = updateMessage.GetValueAsNullable<int>(MessageItemSchema.ApprovalDecision);
			string valueOrDefault = updateMessage.GetValueOrDefault<string>(MessageItemSchema.ApprovalDecisionMaker);
			string valueOrDefault2 = updateMessage.GetValueOrDefault<string>(MessageItemSchema.ApprovalRequestMessageId);
			ExDateTime? valueAsNullable2 = updateMessage.GetValueAsNullable<ExDateTime>(MessageItemSchema.ApprovalDecisionTime);
			if (valueAsNullable == null)
			{
				ApprovalRequestUpdater.diag.TraceDebug(0L, "Invalid update becasue there's no decision.");
				return ApprovalRequestUpdater.Result.InvalidUpdateMessage;
			}
			if (!ApprovalRequestUpdater.IsDecisionExpiry(valueAsNullable.Value) && string.IsNullOrEmpty(valueOrDefault))
			{
				ApprovalRequestUpdater.diag.TraceDebug(0L, "Invalid update becasue there's no decisionMaker");
				return ApprovalRequestUpdater.Result.InvalidUpdateMessage;
			}
			if (string.IsNullOrEmpty(valueOrDefault2))
			{
				ApprovalRequestUpdater.diag.TraceDebug(0L, "Invalid update becasue there's no messageId");
				return ApprovalRequestUpdater.Result.InvalidUpdateMessage;
			}
			if (valueAsNullable2 == null)
			{
				ApprovalRequestUpdater.diag.TraceDebug(0L, "Invalid update becasue there's no decisionTime");
				return ApprovalRequestUpdater.Result.InvalidUpdateMessage;
			}
			return ApprovalRequestUpdater.FindAndUpdateExistingApprovalRequest(updateMessage, valueAsNullable.Value, valueOrDefault, valueAsNullable2.Value, valueOrDefault2);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00010564 File Offset: 0x0000E764
		private static ApprovalRequestUpdater.Result FindAndUpdateExistingApprovalRequest(MessageItem updateMessage, int decision, string decisionMaker, ExDateTime decisionTime, string messageId)
		{
			bool updated = false;
			string local;
			string domain;
			if (!FindMessageUtils.TryParseMessageId(messageId, out local, out domain))
			{
				return ApprovalRequestUpdater.Result.InvalidUpdateMessage;
			}
			ApprovalRequestUpdater.diag.TraceDebug<string>(0L, "Update approval request: messageid={0}", messageId);
			MessageStatus messageStatus = StorageExceptionHandler.RunUnderExceptionHandler(ApprovalRequestUpdater.MessageConverterInstance, delegate
			{
				MailboxSession mailboxSession = (MailboxSession)updateMessage.Session;
				StoreObjectId storeObjectId = null;
				for (int i = 0; i < 25; i++)
				{
					string internetMessageId = ApprovalRequestWriter.FormatApprovalRequestMessageId(local, i, domain, false);
					IStorePropertyBag[] array = AllItemsFolderHelper.FindItemsFromInternetId(mailboxSession, internetMessageId, new PropertyDefinition[]
					{
						ItemSchema.Id
					});
					if (array != null && array.Length > 0)
					{
						ApprovalRequestUpdater.diag.TraceDebug<int>(0L, "Found {0} to update, picking the first.", array.Length);
						storeObjectId = ((VersionedId)array[0][ItemSchema.Id]).ObjectId;
						break;
					}
				}
				if (storeObjectId != null)
				{
					using (MessageItem messageItem = MessageItem.Bind(mailboxSession, storeObjectId))
					{
						if (ApprovalRequestUpdater.VerifyAndUpdateApprovalRequest(mailboxSession, updateMessage.Sender, decision, decisionMaker, decisionTime, messageItem))
						{
							ConflictResolutionResult conflictResolutionResult = messageItem.Save(SaveMode.ResolveConflicts);
							if (conflictResolutionResult.SaveStatus != SaveResult.Success)
							{
								ApprovalRequestUpdater.diag.TraceDebug<string, SaveResult>(0L, "Saving message: {0}, resulted in an update conflict ({1}). Ignored", messageId, conflictResolutionResult.SaveStatus);
							}
							AggregateOperationResult aggregateOperationResult = mailboxSession.Delete(DeleteItemFlags.MoveToDeletedItems, new StoreId[]
							{
								storeObjectId
							});
							if (aggregateOperationResult.OperationResult != OperationResult.Succeeded)
							{
								ApprovalRequestUpdater.diag.TraceDebug<string, OperationResult>(0L, "Delete message: {0}, resulted in failure {1} Ignored", messageId, aggregateOperationResult.OperationResult);
							}
							updated = true;
						}
					}
				}
			});
			if (!updated)
			{
				ApprovalRequestUpdater.diag.TraceDebug<string>(0L, "Couldn't find message: {0}, ignored", messageId);
				return ApprovalRequestUpdater.Result.NotFound;
			}
			if (MessageStatus.Success != messageStatus)
			{
				ApprovalRequestUpdater.diag.TraceDebug<string, string>(0L, "Message ({0}) processing was not successful ({1}), ignoring..", messageId, (messageStatus.Exception == null) ? "NULL exception" : messageStatus.Exception.Message);
				return ApprovalRequestUpdater.Result.SaveConflict;
			}
			return ApprovalRequestUpdater.Result.UpdatedSucessfully;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00010648 File Offset: 0x0000E848
		private static bool VerifyAndUpdateApprovalRequest(MailboxSession session, Participant updateMessageSender, int decision, string decisionMaker, ExDateTime decisionTime, MessageItem approvalRequest)
		{
			if (!object.Equals("IPM.Note.Microsoft.Approval.Request", approvalRequest.ClassName))
			{
				ApprovalRequestUpdater.diag.TraceDebug(0L, "not a approval request, ignore.");
				return false;
			}
			if (!Participant.HasSameEmail(updateMessageSender, approvalRequest.Sender))
			{
				ApprovalRequestUpdater.diag.TraceDebug(0L, "not the same sender, ignore.");
				return false;
			}
			approvalRequest.OpenAsReadWrite();
			if (ApprovalRequestUpdater.IsDecisionExpiry(decision))
			{
				approvalRequest[MessageItemSchema.ExpiryTime] = decisionTime;
			}
			else
			{
				approvalRequest[MessageItemSchema.ApprovalDecision] = decision;
				approvalRequest[MessageItemSchema.ApprovalDecisionMaker] = decisionMaker;
				approvalRequest[MessageItemSchema.ApprovalDecisionTime] = decisionTime;
			}
			return true;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x000106F4 File Offset: 0x0000E8F4
		private static bool IsDecisionExpiry(int decision)
		{
			return decision < 1;
		}

		// Token: 0x040001F5 RID: 501
		private static readonly PropertyDefinition[] ApprovalRequestUpdateProperties = new PropertyDefinition[]
		{
			MessageItemSchema.ApprovalRequestMessageId,
			MessageItemSchema.ApprovalDecisionTime,
			MessageItemSchema.ApprovalDecisionMaker,
			MessageItemSchema.ApprovalDecision
		};

		// Token: 0x040001F6 RID: 502
		private static readonly IMessageConverter MessageConverterInstance = new ApprovalRequestUpdater.MessageConverter();

		// Token: 0x040001F7 RID: 503
		private static readonly Trace diag = ExTraceGlobals.ApprovalAgentTracer;

		// Token: 0x02000060 RID: 96
		public enum Result
		{
			// Token: 0x040001F9 RID: 505
			UpdatedSucessfully,
			// Token: 0x040001FA RID: 506
			NotFound,
			// Token: 0x040001FB RID: 507
			InvalidUpdateMessage,
			// Token: 0x040001FC RID: 508
			SaveConflict
		}

		// Token: 0x02000061 RID: 97
		private class MessageConverter : IMessageConverter
		{
			// Token: 0x17000147 RID: 327
			// (get) Token: 0x060003B5 RID: 949 RVA: 0x00010752 File Offset: 0x0000E952
			public string Description
			{
				get
				{
					return "ApprovalAgent.ApprovalRequestUpdater";
				}
			}

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x060003B6 RID: 950 RVA: 0x00010759 File Offset: 0x0000E959
			public bool IsOutbound
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000149 RID: 329
			// (get) Token: 0x060003B7 RID: 951 RVA: 0x0001075C File Offset: 0x0000E95C
			public Trace Tracer
			{
				get
				{
					return ApprovalRequestUpdater.diag;
				}
			}

			// Token: 0x060003B8 RID: 952 RVA: 0x00010763 File Offset: 0x0000E963
			public void LogMessage(Exception exception)
			{
			}
		}
	}
}
