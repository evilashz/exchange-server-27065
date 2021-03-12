using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Data.Storage.Optics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200088B RID: 2187
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ConversationAggregationLogger : MailboxLoggerBase, IConversationAggregationLogger, IExtensibleLogger, IWorkloadLogger
	{
		// Token: 0x0600520E RID: 21006 RVA: 0x001573B9 File Offset: 0x001555B9
		internal ConversationAggregationLogger(Guid mailboxGuid, OrganizationId organizationId) : base(mailboxGuid, organizationId, ConversationAggregationLogger.Instance.Value)
		{
		}

		// Token: 0x0600520F RID: 21007 RVA: 0x001573D0 File Offset: 0x001555D0
		public void LogParentMessageData(IStorePropertyBag parentMessage)
		{
			if (parentMessage == null)
			{
				return;
			}
			this.LogEvent(new SchemaBasedLogEvent<ConversationAggregationLogSchema.ParentMessageData>
			{
				{
					ConversationAggregationLogSchema.ParentMessageData.ConversationFamilyId,
					parentMessage.TryGetProperty(ItemSchema.ConversationFamilyId)
				},
				{
					ConversationAggregationLogSchema.ParentMessageData.ConversationId,
					parentMessage.TryGetProperty(ItemSchema.ConversationId)
				},
				{
					ConversationAggregationLogSchema.ParentMessageData.InternetMessageId,
					parentMessage.TryGetProperty(ItemSchema.InternetMessageId)
				},
				{
					ConversationAggregationLogSchema.ParentMessageData.ItemClass,
					parentMessage.TryGetProperty(StoreObjectSchema.ItemClass)
				},
				{
					ConversationAggregationLogSchema.ParentMessageData.SupportsSideConversation,
					parentMessage.TryGetProperty(ItemSchema.SupportsSideConversation)
				}
			});
		}

		// Token: 0x06005210 RID: 21008 RVA: 0x00157448 File Offset: 0x00155648
		public override void LogEvent(ILogEvent logEvent)
		{
			base.LogEvent(logEvent);
			ConversationAggregationLogger.Tracer.TraceDebug<string>((long)this.GetHashCode(), "{0}", logEvent.ToString());
		}

		// Token: 0x06005211 RID: 21009 RVA: 0x00157470 File Offset: 0x00155670
		public void LogDeliveredMessageData(ICorePropertyBag deliveredMessage)
		{
			this.LogEvent(new SchemaBasedLogEvent<ConversationAggregationLogSchema.DeliveredMessageData>
			{
				{
					ConversationAggregationLogSchema.DeliveredMessageData.InternetMessageId,
					deliveredMessage.TryGetProperty(ItemSchema.InternetMessageId)
				},
				{
					ConversationAggregationLogSchema.DeliveredMessageData.ItemClass,
					deliveredMessage.TryGetProperty(StoreObjectSchema.ItemClass)
				}
			});
		}

		// Token: 0x06005212 RID: 21010 RVA: 0x001574B0 File Offset: 0x001556B0
		public void LogMailboxOwnerData(IMailboxOwner owner, bool shouldSearchForDuplicatedMessage)
		{
			this.LogEvent(new SchemaBasedLogEvent<ConversationAggregationLogSchema.MailboxOwnerData>
			{
				{
					ConversationAggregationLogSchema.MailboxOwnerData.IsGroupMailbox,
					owner.IsGroupMailbox
				},
				{
					ConversationAggregationLogSchema.MailboxOwnerData.SideConversationProcessingEnabled,
					owner.SideConversationProcessingEnabled
				},
				{
					ConversationAggregationLogSchema.MailboxOwnerData.SearchDuplicatedMessages,
					shouldSearchForDuplicatedMessage
				}
			});
		}

		// Token: 0x06005213 RID: 21011 RVA: 0x001574FC File Offset: 0x001556FC
		public void LogAggregationResultData(ConversationAggregationResult aggregationResult)
		{
			this.LogEvent(new SchemaBasedLogEvent<ConversationAggregationLogSchema.AggregationResult>
			{
				{
					ConversationAggregationLogSchema.AggregationResult.ConversationFamilyId,
					aggregationResult.ConversationFamilyId
				},
				{
					ConversationAggregationLogSchema.AggregationResult.ConversationId,
					ConversationId.Create(aggregationResult.ConversationIndex)
				},
				{
					ConversationAggregationLogSchema.AggregationResult.IsOutOfOrderDelivery,
					ConversationIndex.IsFixupAddingOutOfOrderMessageToConversation(aggregationResult.Stage)
				},
				{
					ConversationAggregationLogSchema.AggregationResult.NewConversationCreated,
					ConversationIndex.IsFixUpCreatingNewConversation(aggregationResult.Stage)
				},
				{
					ConversationAggregationLogSchema.AggregationResult.SupportsSideConversation,
					aggregationResult.SupportsSideConversation
				},
				{
					ConversationAggregationLogSchema.AggregationResult.FixupStage,
					aggregationResult.Stage
				}
			});
		}

		// Token: 0x06005214 RID: 21012 RVA: 0x00157588 File Offset: 0x00155788
		public void LogSideConversationProcessingData(HashSet<string> parentReplyAllParticipants, HashSet<string> deliveredReplyAllParticipants)
		{
			SchemaBasedLogEvent<ConversationAggregationLogSchema.SideConversationProcessingData> schemaBasedLogEvent = new SchemaBasedLogEvent<ConversationAggregationLogSchema.SideConversationProcessingData>();
			if (deliveredReplyAllParticipants.Count > 10)
			{
				schemaBasedLogEvent.Add(ConversationAggregationLogSchema.SideConversationProcessingData.ParentMessageReplyAllParticipantsCount, parentReplyAllParticipants.Count);
				schemaBasedLogEvent.Add(ConversationAggregationLogSchema.SideConversationProcessingData.DeliveredMessageReplyAllParticipantsCount, deliveredReplyAllParticipants.Count);
			}
			else
			{
				schemaBasedLogEvent.Add(ConversationAggregationLogSchema.SideConversationProcessingData.ParentMessageReplyAllDisplayNames, ExtensibleLogger.FormatPIIValue(this.ConvertParticipantsToLogString(parentReplyAllParticipants)));
				schemaBasedLogEvent.Add(ConversationAggregationLogSchema.SideConversationProcessingData.ParentMessageReplyAllParticipantsCount, parentReplyAllParticipants.Count);
				schemaBasedLogEvent.Add(ConversationAggregationLogSchema.SideConversationProcessingData.DeliveredMessageReplyAllDisplayNames, ExtensibleLogger.FormatPIIValue(this.ConvertParticipantsToLogString(deliveredReplyAllParticipants)));
				schemaBasedLogEvent.Add(ConversationAggregationLogSchema.SideConversationProcessingData.DeliveredMessageReplyAllParticipantsCount, deliveredReplyAllParticipants.Count);
			}
			this.LogEvent(schemaBasedLogEvent);
		}

		// Token: 0x06005215 RID: 21013 RVA: 0x0015761C File Offset: 0x0015581C
		public void LogSideConversationProcessingData(ParticipantSet parentReplyAllParticipants, ParticipantSet deliveredReplyAllParticipants)
		{
			SchemaBasedLogEvent<ConversationAggregationLogSchema.SideConversationProcessingData> schemaBasedLogEvent = new SchemaBasedLogEvent<ConversationAggregationLogSchema.SideConversationProcessingData>();
			if (deliveredReplyAllParticipants.Count > 10)
			{
				schemaBasedLogEvent.Add(ConversationAggregationLogSchema.SideConversationProcessingData.ParentMessageReplyAllParticipantsCount, parentReplyAllParticipants.Count);
				schemaBasedLogEvent.Add(ConversationAggregationLogSchema.SideConversationProcessingData.DeliveredMessageReplyAllParticipantsCount, deliveredReplyAllParticipants.Count);
			}
			else
			{
				schemaBasedLogEvent.Add(ConversationAggregationLogSchema.SideConversationProcessingData.ParentMessageReplyAllDisplayNames, ExtensibleLogger.FormatPIIValue(this.ConvertParticipantsToLogString(parentReplyAllParticipants)));
				schemaBasedLogEvent.Add(ConversationAggregationLogSchema.SideConversationProcessingData.ParentMessageReplyAllParticipantsCount, parentReplyAllParticipants.Count);
				schemaBasedLogEvent.Add(ConversationAggregationLogSchema.SideConversationProcessingData.DeliveredMessageReplyAllDisplayNames, ExtensibleLogger.FormatPIIValue(this.ConvertParticipantsToLogString(deliveredReplyAllParticipants)));
				schemaBasedLogEvent.Add(ConversationAggregationLogSchema.SideConversationProcessingData.DeliveredMessageReplyAllParticipantsCount, deliveredReplyAllParticipants.Count);
			}
			this.LogEvent(schemaBasedLogEvent);
		}

		// Token: 0x06005216 RID: 21014 RVA: 0x001576B0 File Offset: 0x001558B0
		public void LogSideConversationProcessingData(string checkResult, bool requiredBindToParentMessage)
		{
			this.LogEvent(new SchemaBasedLogEvent<ConversationAggregationLogSchema.SideConversationProcessingData>
			{
				{
					ConversationAggregationLogSchema.SideConversationProcessingData.DisplayNameCheckResult,
					checkResult
				},
				{
					ConversationAggregationLogSchema.SideConversationProcessingData.RequiredBindToParentMessage,
					requiredBindToParentMessage
				}
			});
		}

		// Token: 0x06005217 RID: 21015 RVA: 0x001576E0 File Offset: 0x001558E0
		public void LogException(string operation, Exception exception)
		{
			this.LogEvent(new SchemaBasedLogEvent<ConversationAggregationLogSchema.Error>
			{
				{
					ConversationAggregationLogSchema.Error.Context,
					operation
				},
				{
					ConversationAggregationLogSchema.Error.Exception,
					exception.ToString()
				}
			});
		}

		// Token: 0x06005218 RID: 21016 RVA: 0x0015770F File Offset: 0x0015590F
		private string ConvertParticipantsToLogString(IEnumerable<string> data)
		{
			if (data != null)
			{
				return string.Join("|", data);
			}
			return null;
		}

		// Token: 0x06005219 RID: 21017 RVA: 0x00157729 File Offset: 0x00155929
		private string ConvertParticipantsToLogString(IEnumerable<IParticipant> data)
		{
			if (data != null)
			{
				return string.Join("|", from entry in data
				select entry.DisplayName);
			}
			return null;
		}

		// Token: 0x04002C99 RID: 11417
		private const int MAX_PARTICIPANTS_COUNT = 10;

		// Token: 0x04002C9A RID: 11418
		private static readonly Lazy<ExtensibleLogger> Instance = new Lazy<ExtensibleLogger>(() => new ExtensibleLogger(ConversationAggregationLogConfiguration.Default));

		// Token: 0x04002C9B RID: 11419
		private static readonly Trace Tracer = ExTraceGlobals.ConversationTracer;
	}
}
