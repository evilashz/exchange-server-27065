using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Data.Transport.StoreDriver;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.MailboxTransport.StoreDriver.Agents;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.Security.RightsManagement.Protectors;
using Microsoft.Exchange.Transport.RightsManagement;
using Microsoft.Win32;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000070 RID: 112
	internal class ConversationsProcessingAgent : StoreDriverDeliveryAgent
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x0001390C File Offset: 0x00011B0C
		static ConversationsProcessingAgent()
		{
			ConversationsProcessingAgent.ReadRegistrySettings();
			if ((ConversationsProcessingAgent.ControlFlags & 4) != 0)
			{
				ConversationsProcessingAgent.InitializePerformanceCounters();
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001398A File Offset: 0x00011B8A
		public ConversationsProcessingAgent()
		{
			base.OnPromotedMessage += this.OnPromotedMessageHandler;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000139B0 File Offset: 0x00011BB0
		public void OnPromotedMessageHandler(StoreDriverEventSource source, StoreDriverDeliveryEventArgs args)
		{
			ConversationsProcessingAgent.tracer.TraceDebug(0L, "Called ConversationsProcessingAgent.OnPromotedMessageHandler");
			if (args == null)
			{
				return;
			}
			StoreDriverDeliveryEventArgsImpl storeDriverDeliveryEventArgsImpl = (StoreDriverDeliveryEventArgsImpl)args;
			if (ConversationsProcessingAgent.ShouldSkipMessageProcessing(storeDriverDeliveryEventArgsImpl))
			{
				return;
			}
			if (storeDriverDeliveryEventArgsImpl.MailboxOwner == null)
			{
				return;
			}
			MailboxSession mailboxSession = storeDriverDeliveryEventArgsImpl.MailboxSession;
			if (mailboxSession.MailboxOwner.MailboxInfo.Location.ServerVersion <= 1937801369)
			{
				if (ConversationsProcessingAgent.tracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ConversationsProcessingAgent.tracer.TraceDebug<string, string>((long)this.GetHashCode(), "Backend server version (0x{0}) is less than minimum required (0x{1})", mailboxSession.MailboxOwner.MailboxInfo.Location.ServerVersion.ToString("X"), 1937801369.ToString("X"));
				}
				return;
			}
			ConversationsProcessingAgent.tracer.TraceDebug((long)this.GetHashCode(), "Processing incoming message");
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				if (storeDriverDeliveryEventArgsImpl.PropertiesForAllMessageCopies == null)
				{
					storeDriverDeliveryEventArgsImpl.PropertiesForAllMessageCopies = new Dictionary<PropertyDefinition, object>();
				}
				ConversationIndexTrackingEx conversationIndexTrackingEx = ConversationIndexTrackingEx.Create();
				this.performanceContext = ConversationsProcessingAgent.conversationLatencyDetectionContextFactory.CreateContext("15.00.1497.010", "FIXUP", new IPerformanceDataProvider[0]);
				if (!this.isBodyTagCalculated)
				{
					ConversationsProcessingAgent.tracer.TraceDebug((long)this.GetHashCode(), "Calculating body tag");
					Stopwatch stopwatch2 = Stopwatch.StartNew();
					if (storeDriverDeliveryEventArgsImpl.ReplayItem.IsRestricted)
					{
						if (!this.TryCalculateIrmBodyTag(storeDriverDeliveryEventArgsImpl))
						{
							this.bodyTag = null;
						}
					}
					else if (PropertyError.IsPropertyNotFound(storeDriverDeliveryEventArgsImpl.ReplayItem.TryGetProperty(ItemSchema.BodyTag)))
					{
						this.bodyTag = storeDriverDeliveryEventArgsImpl.ReplayItem.Body.CalculateBodyTag(out this.latestMessageWordCount);
					}
					else
					{
						this.bodyTag = storeDriverDeliveryEventArgsImpl.ReplayItem.GetValueOrDefault<byte[]>(ItemSchema.BodyTag);
						this.latestMessageWordCount = storeDriverDeliveryEventArgsImpl.ReplayItem.GetValueOrDefault<int>(MessageItemSchema.LatestMessageWordCount, int.MinValue);
					}
					stopwatch2.Stop();
					conversationIndexTrackingEx.Trace("BT", stopwatch2.ElapsedMilliseconds.ToString());
					this.isBodyTagCalculated = true;
				}
				if (this.bodyTag != null)
				{
					storeDriverDeliveryEventArgsImpl.PropertiesForAllMessageCopies[ItemSchema.BodyTag] = this.bodyTag;
					if (this.latestMessageWordCount >= 0)
					{
						storeDriverDeliveryEventArgsImpl.PropertiesForAllMessageCopies[MessageItemSchema.LatestMessageWordCount] = this.latestMessageWordCount;
						storeDriverDeliveryEventArgsImpl.ReplayItem.SafeSetProperty(MessageItemSchema.LatestMessageWordCount, this.latestMessageWordCount);
					}
				}
				if (this.forceAllAttachmentsHidden)
				{
					storeDriverDeliveryEventArgsImpl.PropertiesForAllMessageCopies[MessageItemSchema.AllAttachmentsHidden] = true;
				}
				if (!ConversationIndex.CompareTopics(storeDriverDeliveryEventArgsImpl.ReplayItem.TryGetProperty(ItemSchema.ConversationTopic) as string, storeDriverDeliveryEventArgsImpl.ReplayItem.TryGetProperty(ItemSchema.NormalizedSubject) as string))
				{
					storeDriverDeliveryEventArgsImpl.PropertiesForAllMessageCopies[ItemSchema.ConversationTopic] = ((storeDriverDeliveryEventArgsImpl.ReplayItem.TryGetProperty(ItemSchema.NormalizedSubject) as string) ?? string.Empty);
				}
				if (!storeDriverDeliveryEventArgsImpl.MessageClass.Equals("IPM.WorkingSet.Signal", StringComparison.InvariantCulture))
				{
					ConversationAggregationResult conversationAggregationResult = new ConversationAggregationResult();
					ConversationsProcessingAgent.tracer.TraceDebug((long)this.GetHashCode(), "Fixing up the conversation id");
					try
					{
						IConversationAggregator conversationAggregator;
						if (ConversationAggregatorFactory.TryInstantiateAggregatorForDelivery(mailboxSession, storeDriverDeliveryEventArgsImpl.MailboxOwner, conversationIndexTrackingEx, out conversationAggregator))
						{
							conversationAggregationResult = conversationAggregator.Aggregate(storeDriverDeliveryEventArgsImpl.ReplayItem.CoreItem);
						}
						else
						{
							ConversationsProcessingAgent.tracer.TraceError((long)this.GetHashCode(), "Not able to identify a valid conversationAggregator");
							conversationAggregationResult.ConversationIndex = ConversationIndex.CreateNew();
							conversationAggregationResult.Stage = ConversationIndex.FixupStage.Error;
						}
					}
					catch (ObjectNotFoundException arg)
					{
						ConversationsProcessingAgent.tracer.TraceError<ObjectNotFoundException>((long)this.GetHashCode(), "Exception - {0}", arg);
						conversationAggregationResult.ConversationIndex = ConversationIndex.CreateNew();
						conversationAggregationResult.Stage = ConversationIndex.FixupStage.Error;
					}
					this.performanceContext.StopAndFinalizeCollection();
					conversationIndexTrackingEx.Trace("FIXUP", this.performanceContext.Elapsed.TotalMilliseconds.ToString());
					ConversationsProcessingAgent.tracer.TraceDebug<int>((long)this.GetHashCode(), "FixupStage = {0}", (int)conversationAggregationResult.Stage);
					ConversationId conversationId = ConversationId.Create(conversationAggregationResult.ConversationIndex);
					ConversationsProcessingAgent.tracer.TraceDebug<ConversationId>((long)this.GetHashCode(), "ConversationId (CID) for item: {0}", conversationId);
					storeDriverDeliveryEventArgsImpl.PropertiesForAllMessageCopies[ItemSchema.ConversationIndex] = conversationAggregationResult.ConversationIndex.ToByteArray();
					storeDriverDeliveryEventArgsImpl.ReplayItem.SafeSetProperty(ItemSchema.ConversationIndex, storeDriverDeliveryEventArgsImpl.PropertiesForAllMessageCopies[ItemSchema.ConversationIndex]);
					storeDriverDeliveryEventArgsImpl.PropertiesForAllMessageCopies[ItemSchema.ConversationFamilyId] = conversationAggregationResult.ConversationFamilyId;
					storeDriverDeliveryEventArgsImpl.ReplayItem.SafeSetProperty(ItemSchema.ConversationFamilyId, storeDriverDeliveryEventArgsImpl.PropertiesForAllMessageCopies[ItemSchema.ConversationFamilyId]);
					storeDriverDeliveryEventArgsImpl.PropertiesForAllMessageCopies[ItemSchema.SupportsSideConversation] = conversationAggregationResult.SupportsSideConversation;
					storeDriverDeliveryEventArgsImpl.ReplayItem.SafeSetProperty(ItemSchema.SupportsSideConversation, storeDriverDeliveryEventArgsImpl.PropertiesForAllMessageCopies[ItemSchema.SupportsSideConversation]);
					byte[] value;
					if (this.TryCalculateConversationCreatorSid(mailboxSession, storeDriverDeliveryEventArgsImpl.MailboxOwner, conversationAggregationResult, storeDriverDeliveryEventArgsImpl.ReplayItem.PropertyBag, out value))
					{
						storeDriverDeliveryEventArgsImpl.PropertiesForAllMessageCopies[ItemSchema.ConversationCreatorSID] = value;
						storeDriverDeliveryEventArgsImpl.ReplayItem.SafeSetProperty(ItemSchema.ConversationCreatorSID, storeDriverDeliveryEventArgsImpl.PropertiesForAllMessageCopies[ItemSchema.ConversationCreatorSID]);
					}
					conversationIndexTrackingEx.TraceVersionAndHeuristics(conversationAggregationResult.Stage.ToString());
					storeDriverDeliveryEventArgsImpl.PropertiesForAllMessageCopies[ItemSchema.ConversationIndexTrackingEx] = conversationIndexTrackingEx.ToString();
					if (conversationAggregationResult.Stage != ConversationIndex.FixupStage.L1)
					{
						storeDriverDeliveryEventArgsImpl.PropertiesForAllMessageCopies[ItemSchema.ConversationIndexTracking] = true;
						storeDriverDeliveryEventArgsImpl.ReplayItem.SafeSetProperty(ItemSchema.ConversationIndexTracking, storeDriverDeliveryEventArgsImpl.PropertiesForAllMessageCopies[ItemSchema.ConversationIndexTracking]);
					}
					ConversationsProcessingAgent.tracer.TraceDebug<ConversationIndexTrackingEx>((long)this.GetHashCode(), "Time Spent in different stages - {0}", conversationIndexTrackingEx);
					ConversationsProcessingAgent.tracer.TraceDebug((long)this.GetHashCode(), "Processing conversation actions");
					if (!ConversationIndex.IsFixUpCreatingNewConversation(conversationAggregationResult.Stage))
					{
						this.ProcessConversationActions(conversationId, storeDriverDeliveryEventArgsImpl);
					}
					if (ConversationIndex.IsFixupAddingOutOfOrderMessageToConversation(conversationAggregationResult.Stage))
					{
						if (storeDriverDeliveryEventArgsImpl.SharedPropertiesBetweenAgents == null)
						{
							storeDriverDeliveryEventArgsImpl.SharedPropertiesBetweenAgents = new Dictionary<PropertyDefinition, object>();
						}
						storeDriverDeliveryEventArgsImpl.SharedPropertiesBetweenAgents[ItemSchema.ConversationLoadRequiredByInference] = true;
					}
				}
			}
			finally
			{
				stopwatch.Stop();
				MSExchangeConversationsProcessing.LastMessageProcessingTime.RawValue = stopwatch.ElapsedMilliseconds;
				ConversationsProcessingAgent.averageMessageProcessingTime.Update(stopwatch.ElapsedMilliseconds);
				ConversationsProcessingAgent.tracer.TraceDebug<long>((long)this.GetHashCode(), "Exiting ConversationsProcessing.ProcessMessage.  Total execution time = {0} ms.", stopwatch.ElapsedMilliseconds);
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001400C File Offset: 0x0001220C
		private static bool ShouldSkipMessageProcessing(StoreDriverDeliveryEventArgsImpl argsImpl)
		{
			return !ConversationsProcessingAgent.IsObjectClassInteresting(argsImpl.MessageClass) || argsImpl.IsPublicFolderRecipient || argsImpl.IsJournalReport;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0001402E File Offset: 0x0001222E
		private static bool IsObjectClassInteresting(string objClass)
		{
			return !string.IsNullOrEmpty(objClass);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001403C File Offset: 0x0001223C
		private static void ReadRegistrySettings()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Conversations"))
			{
				if (registryKey != null)
				{
					object value = registryKey.GetValue("TransportAgentFlags");
					if (value is int)
					{
						ConversationsProcessingAgent.ControlFlags = (int)value;
					}
					else
					{
						ConversationsProcessingAgent.ControlFlags = 0;
					}
					value = registryKey.GetValue("ConversationAgentThreshold");
					int? num = value as int?;
					if (num != null && num.Value >= 1)
					{
						ConversationsProcessingAgent.conversationAgentThreshold = TimeSpan.FromMinutes((double)num.Value);
					}
				}
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000140E0 File Offset: 0x000122E0
		private static void InitializePerformanceCounters()
		{
			MSExchangeConversationsProcessing.LastMessageProcessingTime.RawValue = 0L;
			MSExchangeConversationsProcessing.AverageMessageProcessingTime.RawValue = 0L;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000140FC File Offset: 0x000122FC
		private bool TryCalculateConversationCreatorSid(IMailboxSession session, MiniRecipient mailboxOwner, ConversationAggregationResult aggregationResult, ICorePropertyBag deliveredMessage, out byte[] conversationCreatorSid)
		{
			conversationCreatorSid = null;
			bool flag = false;
			ConversationCreatorSidCalculatorFactory conversationCreatorSidCalculatorFactory = new ConversationCreatorSidCalculatorFactory(XSOFactory.Default);
			IConversationCreatorSidCalculator conversationCreatorSidCalculator;
			if (conversationCreatorSidCalculatorFactory.TryCreate(session, mailboxOwner, out conversationCreatorSidCalculator) && conversationCreatorSidCalculator.TryCalculateOnDelivery(deliveredMessage, aggregationResult.Stage, aggregationResult.ConversationIndex, out conversationCreatorSid, out flag))
			{
				if (flag)
				{
					conversationCreatorSidCalculator.UpdateConversationMessages(aggregationResult.ConversationIndex, conversationCreatorSid);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0001415C File Offset: 0x0001235C
		private bool TryCalculateIrmBodyTag(StoreDriverDeliveryEventArgsImpl argsImpl)
		{
			MessageItem messageItem = null;
			bool result;
			try
			{
				OrganizationId organizationId = argsImpl.ADRecipientCache.OrganizationId;
				MessageItem replayItem = argsImpl.ReplayItem;
				using (Attachment attachment = this.TryOpenFirstAttachment(replayItem))
				{
					StreamAttachmentBase streamAttachmentBase = attachment as StreamAttachmentBase;
					if (streamAttachmentBase == null)
					{
						ConversationsProcessingAgent.tracer.TraceError((long)this.GetHashCode(), "message.rpmsg attachment is not of the correct type");
						return false;
					}
					using (Stream contentStream = streamAttachmentBase.GetContentStream(PropertyOpenMode.ReadOnly))
					{
						if (contentStream == null)
						{
							ConversationsProcessingAgent.tracer.TraceError((long)this.GetHashCode(), "The rights protected message is not properly formatted");
							return false;
						}
						using (DrmEmailMessageContainer drmEmailMessageContainer = new DrmEmailMessageContainer())
						{
							try
							{
								drmEmailMessageContainer.Load(contentStream, (object param0) => Streams.CreateTemporaryStorageStream());
							}
							catch (InvalidRpmsgFormatException)
							{
								ConversationsProcessingAgent.tracer.TraceError((long)this.GetHashCode(), "The rights protected message is not properly formatted");
								return false;
							}
							string text = null;
							if (!argsImpl.MailItemDeliver.MbxTransportMailItem.ExtendedProperties.TryGetValue<string>("Microsoft.Exchange.RightsManagement.TransportDecryptionUL", out text) || string.IsNullOrEmpty(text))
							{
								ConversationsProcessingAgent.tracer.TraceError((long)this.GetHashCode(), "Failed to load the useLicense");
								return false;
							}
							try
							{
								Uri licenseUri;
								XmlNode[] array;
								bool flag;
								RmsClientManager.GetLicensingUri(organizationId, drmEmailMessageContainer.PublishLicense, out licenseUri, out array, out flag);
								RpMsgToMsgConverter rpMsgToMsgConverter = new RpMsgToMsgConverter(drmEmailMessageContainer, organizationId, false);
								RmsClientManagerContext context = new RmsClientManagerContext(organizationId, RmsClientManagerContext.ContextId.MessageId, replayItem.InternetMessageId, argsImpl.ADRecipientCache, new RmsLatencyTracker(argsImpl.MailItemDeliver.MbxTransportMailItem.LatencyTracker), drmEmailMessageContainer.PublishLicense);
								using (DisposableTenantLicensePair disposableTenantLicensePair = RmsClientManager.AcquireTenantLicenses(context, licenseUri))
								{
									messageItem = rpMsgToMsgConverter.ConvertRpmsgToMsg(replayItem, text, disposableTenantLicensePair.EnablingPrincipalRac);
								}
							}
							catch (RightsManagementException arg)
							{
								ConversationsProcessingAgent.tracer.TraceError<RightsManagementException>((long)this.GetHashCode(), "Conversion failed: {0}", arg);
								return false;
							}
							catch (InvalidRpmsgFormatException arg2)
							{
								ConversationsProcessingAgent.tracer.TraceError<InvalidRpmsgFormatException>((long)this.GetHashCode(), "Conversion failed: {0}", arg2);
								return false;
							}
							catch (ExchangeConfigurationException arg3)
							{
								ConversationsProcessingAgent.tracer.TraceError<ExchangeConfigurationException>((long)this.GetHashCode(), "Conversion failed: {0}", arg3);
								return false;
							}
							catch (AttachmentProtectionException arg4)
							{
								ConversationsProcessingAgent.tracer.TraceError<AttachmentProtectionException>((long)this.GetHashCode(), "Conversion failed: {0}", arg4);
								return false;
							}
						}
					}
				}
				this.bodyTag = messageItem.Body.CalculateBodyTag(out this.latestMessageWordCount);
				if (messageItem.AttachmentCollection.Count == 0)
				{
					this.forceAllAttachmentsHidden = true;
				}
				else
				{
					foreach (AttachmentHandle attachmentHandle in messageItem.AttachmentCollection)
					{
						if (!attachmentHandle.IsInline)
						{
							break;
						}
						this.forceAllAttachmentsHidden = true;
					}
				}
				result = true;
			}
			finally
			{
				if (messageItem != null)
				{
					messageItem.Dispose();
				}
			}
			return result;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001452C File Offset: 0x0001272C
		private Attachment TryOpenFirstAttachment(MessageItem messageItem)
		{
			using (IEnumerator<AttachmentHandle> enumerator = messageItem.AttachmentCollection.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					AttachmentHandle handle = enumerator.Current;
					return messageItem.AttachmentCollection.Open(handle, AttachmentType.Stream);
				}
			}
			return null;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00014588 File Offset: 0x00012788
		private void ProcessConversationActions(ConversationId conversationId, StoreDriverDeliveryEventArgsImpl argsImpl)
		{
			MailboxSession mailboxSession = argsImpl.MailboxSession;
			try
			{
				StoreId storeId = ConversationActionItem.QueryConversationActionsFolder(mailboxSession, conversationId);
				if (storeId != null)
				{
					using (ConversationActionItem conversationActionItem = ConversationActionItem.Bind(mailboxSession, storeId))
					{
						ConversationsProcessingAgent.tracer.TraceDebug<ConversationId>((long)this.GetHashCode(), "Found CAT item for message with CID = {0}", conversationId);
						if (!conversationActionItem.IsCorrectVersion())
						{
							ConversationsProcessingAgent.tracer.TraceDebug<int, ConversationId>((long)this.GetHashCode(), "Found CAT item with major version {0} for message with CID = {1}", conversationActionItem.ConversationActionVersionMajor, conversationId);
						}
						else
						{
							StoreId targetFolderId = conversationActionItem.TargetFolderId;
							if (targetFolderId != null)
							{
								argsImpl.DeliverToFolder = targetFolderId;
								argsImpl.ShouldSkipMoveRule = true;
								ConversationsProcessingAgent.tracer.TraceDebug((long)this.GetHashCode(), "Applied move action to item");
							}
							List<string> categoriesForItem = conversationActionItem.GetCategoriesForItem(argsImpl.ReplayItem.TryGetProperty(ItemSchema.Categories) as string[]);
							if (categoriesForItem != null)
							{
								argsImpl.PropertiesForAllMessageCopies[ItemSchema.Categories] = categoriesForItem.ToArray();
								ConversationsProcessingAgent.tracer.TraceDebug((long)this.GetHashCode(), "Applied categorize action to item");
							}
							if (conversationActionItem.AlwaysClutterOrUnclutterValue != null)
							{
								if (argsImpl.SharedPropertiesBetweenAgents == null)
								{
									argsImpl.SharedPropertiesBetweenAgents = new Dictionary<PropertyDefinition, object>();
								}
								argsImpl.SharedPropertiesBetweenAgents[ItemSchema.InferenceConversationClutterActionApplied] = conversationActionItem.AlwaysClutterOrUnclutterValue.Value;
								ConversationsProcessingAgent.tracer.TraceDebug((long)this.GetHashCode(), "Applied clutter action to item");
							}
							conversationActionItem.Save(SaveMode.ResolveConflicts);
						}
					}
				}
			}
			catch (ObjectNotFoundException arg)
			{
				ConversationsProcessingAgent.tracer.TraceError<ObjectNotFoundException>((long)this.GetHashCode(), "Exception - {0}", arg);
			}
		}

		// Token: 0x0400023F RID: 575
		internal const string RegistryKeyPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Conversations";

		// Token: 0x04000240 RID: 576
		internal const string TransportAgentFlagsKeyName = "TransportAgentFlags";

		// Token: 0x04000241 RID: 577
		internal const string ConversationAgentThresholdKeyName = "ConversationAgentThreshold";

		// Token: 0x04000242 RID: 578
		private const int MinimumBackendServerVersion = 1937801369;

		// Token: 0x04000243 RID: 579
		private const string AssemblyVersion = "15.00.1497.010";

		// Token: 0x04000244 RID: 580
		internal static int ControlFlags = 0;

		// Token: 0x04000245 RID: 581
		private static readonly Microsoft.Exchange.Diagnostics.Trace tracer = ExTraceGlobals.ConversationsTracer;

		// Token: 0x04000246 RID: 582
		private static AveragePerformanceCounterWrapper averageMessageProcessingTime = new AveragePerformanceCounterWrapper(MSExchangeConversationsProcessing.AverageMessageProcessingTime);

		// Token: 0x04000247 RID: 583
		private static TimeSpan defaultMinConversationAgentThreshold = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000248 RID: 584
		private static TimeSpan conversationAgentThreshold = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000249 RID: 585
		private static LatencyDetectionContextFactory conversationLatencyDetectionContextFactory = LatencyDetectionContextFactory.CreateFactory("ConversationAgent", ConversationsProcessingAgent.conversationAgentThreshold, ConversationsProcessingAgent.defaultMinConversationAgentThreshold);

		// Token: 0x0400024A RID: 586
		private LatencyDetectionContext performanceContext;

		// Token: 0x0400024B RID: 587
		private byte[] bodyTag;

		// Token: 0x0400024C RID: 588
		private bool isBodyTagCalculated;

		// Token: 0x0400024D RID: 589
		private bool forceAllAttachmentsHidden;

		// Token: 0x0400024E RID: 590
		private int latestMessageWordCount = int.MinValue;
	}
}
