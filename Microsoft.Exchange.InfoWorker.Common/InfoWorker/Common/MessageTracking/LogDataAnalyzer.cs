using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002CA RID: 714
	internal class LogDataAnalyzer
	{
		// Token: 0x060013E1 RID: 5089 RVA: 0x0005C918 File Offset: 0x0005AB18
		public LogDataAnalyzer(TrackingContext context)
		{
			this.context = context;
			this.startingServer = Fqdn.Parse(this.context.StartingEventId.Server);
			this.cache = context.Cache;
			this.remainingTrackers = new Queue<MailItemTracker>();
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0005C96F File Offset: 0x0005AB6F
		private static bool TryEnqueueNextHopAfterSubmit(MessageTrackingLogEntry submitEvent, TrackingContext context, Queue<MailItemTracker> remainingTrackers)
		{
			return LogDataAnalyzer.TryEnqueueNextHopAfterSubmitInternal(submitEvent, context, remainingTrackers, false);
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0005C97C File Offset: 0x0005AB7C
		private static bool TryEnqueueNextHopAfterSubmitInternal(MessageTrackingLogEntry submitEvent, TrackingContext context, Queue<MailItemTracker> remainingTrackers, bool searchAfterModerationApproval)
		{
			ServerInfo nextHopServer = submitEvent.GetNextHopServer();
			if (LogDataAnalyzer.ShouldSkipTracker(nextHopServer, submitEvent, context))
			{
				return false;
			}
			ILogReader logReader = RpcLogReader.GetLogReader(submitEvent.NextHopFqdnOrName, context.DirectoryContext);
			if (logReader == null)
			{
				return false;
			}
			List<MessageTrackingLogEntry> messageLog = context.Cache.GetMessageLog(RpcReason.None, logReader, TrackingLogPrefix.MSGTRK, submitEvent.MessageId, new MessageTrackingSource?((logReader.MtrSchemaVersion >= MtrSchemaVersion.E15RTM) ? MessageTrackingSource.SMTP : MessageTrackingSource.STOREDRIVER), SearchMessageTrackingReportImpl.ReceiveEventFilterSet);
			if (messageLog.Count == 0)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string, string, MessageTrackingLogEntry>(0, "No {0} Receive event found on {1} to match {2}.", (logReader.MtrSchemaVersion >= MtrSchemaVersion.E15RTM) ? "SMTP" : "StoreDriver", submitEvent.NextHopFqdnOrName, submitEvent);
				return false;
			}
			long num;
			if (!searchAfterModerationApproval)
			{
				num = messageLog[0].ServerLogKeyMailItemId;
			}
			else
			{
				num = LogDataAnalyzer.GetCheckReceiveEventAfterModerationApproval(messageLog, context, logReader);
				if (num == 0L)
				{
					return false;
				}
			}
			foreach (MessageTrackingLogEntry messageTrackingLogEntry in messageLog)
			{
				LogDataAnalyzer.CreateAndEnqueueTracker(context, remainingTrackers, logReader, TrackingLogPrefix.MSGTRK, messageTrackingLogEntry.MessageId, num);
			}
			return true;
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0005CA98 File Offset: 0x0005AC98
		private static long GetCheckReceiveEventAfterModerationApproval(List<MessageTrackingLogEntry> possibleReceiveMatches, TrackingContext context, ILogReader reader)
		{
			MessageTrackingLogEntry messageTrackingLogEntry = possibleReceiveMatches[possibleReceiveMatches.Count - 1];
			foreach (MessageTrackingLogEntry messageTrackingLogEntry2 in possibleReceiveMatches)
			{
				if (messageTrackingLogEntry2.ProcessedBy == null)
				{
					messageTrackingLogEntry = messageTrackingLogEntry2;
					break;
				}
			}
			long serverLogKeyMailItemId = messageTrackingLogEntry.ServerLogKeyMailItemId;
			List<MessageTrackingLogEntry> messageLog = context.Cache.GetMessageLog(RpcReason.None, reader, TrackingLogPrefix.MSGTRK, messageTrackingLogEntry.MessageId, serverLogKeyMailItemId);
			if (messageLog.Count != 0)
			{
				if (!messageLog.TrueForAll((MessageTrackingLogEntry receiveEntry) => receiveEntry.Source != MessageTrackingSource.APPROVAL))
				{
					TraceWrapper.SearchLibraryTracer.TraceDebug<string, long>(0, "Last Receive event for message {0} InternalMessageId {1} doesn't indicate resubmission after approval because contains APPROVAL event in the path.", messageTrackingLogEntry.MessageId, serverLogKeyMailItemId);
					return 0L;
				}
			}
			return serverLogKeyMailItemId;
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0005CB60 File Offset: 0x0005AD60
		private static bool TryEnqueueNextHopAfterTransfer(MessageTrackingLogEntry transferEvent, TrackingContext context, Queue<MailItemTracker> remainingTrackers)
		{
			ServerInfo server = ServerCache.Instance.FindMailboxOrHubServer(transferEvent.Server, 32UL);
			if (LogDataAnalyzer.ShouldSkipTracker(server, transferEvent, context))
			{
				return false;
			}
			ILogReader logReader = RpcLogReader.GetLogReader(transferEvent.Server, context.DirectoryContext);
			if (logReader == null)
			{
				return false;
			}
			List<MessageTrackingLogEntry> messageLog = context.Cache.GetMessageLog(RpcReason.None, logReader, TrackingLogPrefix.MSGTRK, transferEvent.MessageId, transferEvent.InternalMessageId);
			if (messageLog.Count == 0)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string, long>(0, "Could not find any events matching MessageId={0}, and InternalId={1}.", transferEvent.MessageId, transferEvent.ServerLogKeyMailItemId);
				return false;
			}
			MailItemTracker item = new MailItemTracker(messageLog, context.Tree);
			remainingTrackers.Enqueue(item);
			return true;
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x0005CBFC File Offset: 0x0005ADFC
		private static bool TryEnqueueNextHopAfterSend(MessageTrackingLogEntry sendEvent, TrackingContext context, Queue<MailItemTracker> remainingTrackers)
		{
			ServerInfo nextHopServer = sendEvent.GetNextHopServer();
			if (LogDataAnalyzer.ShouldSkipTracker(nextHopServer, sendEvent, context))
			{
				return false;
			}
			ILogReader logReader = RpcLogReader.GetLogReader(sendEvent.NextHopFqdnOrName, context.DirectoryContext);
			if (logReader == null)
			{
				return false;
			}
			MessageTrackingSource? sourceFilter = null;
			HashSet<MessageTrackingEvent> eventIdFilterSet = SearchMessageTrackingReportImpl.ReceiveOrDeliverEventsFilterSet;
			if (logReader.MtrSchemaVersion < MtrSchemaVersion.E15RTM)
			{
				sourceFilter = new MessageTrackingSource?(MessageTrackingSource.SMTP);
				eventIdFilterSet = SearchMessageTrackingReportImpl.ReceiveEventFilterSet;
			}
			List<MessageTrackingLogEntry> messageLog = context.Cache.GetMessageLog(RpcReason.None, logReader, TrackingLogPrefix.MSGTRK, sendEvent.MessageId, sourceFilter, eventIdFilterSet);
			if (messageLog.Count == 0)
			{
				return false;
			}
			foreach (MessageTrackingLogEntry messageTrackingLogEntry in messageLog)
			{
				if (LogDataAnalyzer.CheckRecipientListEquality(sendEvent, messageTrackingLogEntry))
				{
					LogDataAnalyzer.CreateAndEnqueueTracker(context, remainingTrackers, logReader, TrackingLogPrefix.MSGTRK, messageTrackingLogEntry.MessageId, messageTrackingLogEntry.ServerLogKeyMailItemId);
					return true;
				}
			}
			TraceWrapper.SearchLibraryTracer.TraceError<MessageTrackingLogEntry, string>(0, "Could not find any receive events to match {0} on {1}.", sendEvent, sendEvent.NextHopFqdnOrName);
			return false;
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0005CCFC File Offset: 0x0005AEFC
		private static bool TryEnqueueNextHopAfterInitMessage(MessageTrackingLogEntry initMessageEvent, TrackingContext context, Queue<MailItemTracker> remainingTrackers)
		{
			ILogReader logReader;
			MessageTrackingLogEntry messageTrackingLogEntry = LogDataAnalyzer.LocateModeratorResponse(initMessageEvent, context, out logReader);
			if (messageTrackingLogEntry == null)
			{
				return false;
			}
			if (messageTrackingLogEntry.RecipientAddresses == null)
			{
				messageTrackingLogEntry.RecipientAddresses = initMessageEvent.RecipientAddresses;
			}
			MailItemTracker item = new MailItemTracker(new List<MessageTrackingLogEntry>(1)
			{
				messageTrackingLogEntry
			}, context.Tree);
			remainingTrackers.Enqueue(item);
			return true;
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0005CD50 File Offset: 0x0005AF50
		private static bool TryEnqueueNextHopAfterModeratorApprove(MessageTrackingLogEntry approvalEvent, TrackingContext context, Queue<MailItemTracker> remainingTrackers)
		{
			ILogReader logReader = RpcLogReader.GetLogReader(approvalEvent.Server, context.DirectoryContext);
			if (logReader == null)
			{
				return false;
			}
			List<MessageTrackingLogEntry> messageLog = context.Cache.GetMessageLog(RpcReason.None, logReader, TrackingLogPrefix.MSGTRK, approvalEvent.MessageId, new MessageTrackingSource?(MessageTrackingSource.STOREDRIVER), SearchMessageTrackingReportImpl.SubmitEventFilterSet);
			if (messageLog.Count == 0)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string>(0, "Moderator approval event found, but no matching submission of the message back into transport was found on mailbox server {0}.", approvalEvent.Server);
				return false;
			}
			MessageTrackingLogEntry messageTrackingLogEntry = null;
			foreach (MessageTrackingLogEntry messageTrackingLogEntry2 in messageLog)
			{
				if (!LogDataAnalyzer.IsEntryProcessed(messageTrackingLogEntry2, context.Tree))
				{
					messageTrackingLogEntry = messageTrackingLogEntry2;
					messageTrackingLogEntry.ProcessedBy = context.Tree;
					break;
				}
			}
			if (messageTrackingLogEntry == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError(0, "All submission events found after processing moderator approval had already been processed.", new object[0]);
				return false;
			}
			ILogReader logReader2 = RpcLogReader.GetLogReader(messageTrackingLogEntry.NextHopFqdnOrName, context.DirectoryContext);
			if (logReader2 == null)
			{
				return false;
			}
			List<MessageTrackingLogEntry> messageLog2 = context.Cache.GetMessageLog(RpcReason.None, logReader2, TrackingLogPrefix.MSGTRK, approvalEvent.OrigMessageId, new MessageTrackingSource?(MessageTrackingSource.APPROVAL), SearchMessageTrackingReportImpl.ReceiveEventFilterSet);
			if (messageLog2.Count == 0)
			{
				TraceWrapper.SearchLibraryTracer.TraceError(0, "No Approval Received events found", new object[0]);
				return false;
			}
			foreach (MessageTrackingLogEntry messageTrackingLogEntry3 in messageLog2)
			{
				if (!LogDataAnalyzer.IsEntryProcessed(messageTrackingLogEntry3, context.Tree) && approvalEvent.MessageId.Equals(messageTrackingLogEntry3.InitMessageId, StringComparison.Ordinal))
				{
					if (logReader.MtrSchemaVersion >= MtrSchemaVersion.E15RTM)
					{
						if (!LogDataAnalyzer.TryEnqueueNextHopAfterSubmitInternal(messageTrackingLogEntry3, context, remainingTrackers, true))
						{
							continue;
						}
					}
					else
					{
						List<MessageTrackingLogEntry> messageLog3 = context.Cache.GetMessageLog(RpcReason.None, logReader2, TrackingLogPrefix.MSGTRK, approvalEvent.OrigMessageId, messageTrackingLogEntry3.ServerLogKeyMailItemId);
						if (messageLog3.Count == 0)
						{
							continue;
						}
						MailItemTracker item = new MailItemTracker(messageLog3, context.Tree);
						remainingTrackers.Enqueue(item);
					}
					return true;
				}
			}
			TraceWrapper.SearchLibraryTracer.TraceError(0, "No unprocessed Approval Received events found", new object[0]);
			return false;
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0005CF64 File Offset: 0x0005B164
		private static bool TryEnqueueNextHopAfterHARedirect(MessageTrackingLogEntry haRedirectEvent, TrackingContext context, Queue<MailItemTracker> remainingTrackers)
		{
			ServerInfo nextHopServer = haRedirectEvent.GetNextHopServer();
			if (LogDataAnalyzer.ShouldSkipTracker(nextHopServer, haRedirectEvent, context))
			{
				return false;
			}
			ILogReader logReader = RpcLogReader.GetLogReader(haRedirectEvent.NextHopFqdnOrName, context.DirectoryContext);
			if (logReader == null)
			{
				return false;
			}
			List<MessageTrackingLogEntry> messageLog = context.Cache.GetMessageLog(RpcReason.None, logReader, TrackingLogPrefix.MSGTRK, haRedirectEvent.MessageId, null, SearchMessageTrackingReportImpl.HAReceiveOrResubmitEventsFilterSet);
			if (messageLog.Count == 0)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<MessageTrackingLogEntry, string>(0, "Could not find any HA receive events to match {0} on {1}.", haRedirectEvent, haRedirectEvent.NextHopFqdnOrName);
				return false;
			}
			MailItemTracker item = new MailItemTracker(messageLog, context.Tree);
			remainingTrackers.Enqueue(item);
			return true;
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0005D004 File Offset: 0x0005B204
		private static bool TryEnqueueNextHopAfterHAResubmit(MessageTrackingLogEntry haResubmitEvent, TrackingContext context, Queue<MailItemTracker> remainingTrackers)
		{
			if (haResubmitEvent.Source != MessageTrackingSource.REDUNDANCY)
			{
				return false;
			}
			ServerInfo server = ServerCache.Instance.FindMailboxOrHubServer(haResubmitEvent.Server, 32UL);
			if (LogDataAnalyzer.ShouldSkipTracker(server, haResubmitEvent, context))
			{
				return false;
			}
			ILogReader logReader = RpcLogReader.GetLogReader(haResubmitEvent.Server, context.DirectoryContext);
			if (logReader == null)
			{
				return false;
			}
			List<MessageTrackingLogEntry> messageLog = context.Cache.GetMessageLog(RpcReason.None, logReader, TrackingLogPrefix.MSGTRK, haResubmitEvent.MessageId, haResubmitEvent.InternalMessageId);
			List<MessageTrackingLogEntry> list = messageLog.FindAll((MessageTrackingLogEntry entry) => entry.Source != MessageTrackingSource.REDUNDANCY);
			if (list.Count == 0)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string, long>(0, "Could not find any events following resubmission matching MessageId={0}, and InternalId={1}.", haResubmitEvent.MessageId, haResubmitEvent.InternalMessageId);
				return false;
			}
			MailItemTracker item = new MailItemTracker(list, context.Tree);
			remainingTrackers.Enqueue(item);
			return true;
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0005D0D0 File Offset: 0x0005B2D0
		private static void CreateAndEnqueueTracker(TrackingContext context, Queue<MailItemTracker> remainingTrackers, ILogReader reader, TrackingLogPrefix prefix, string messageId, long mailItemId)
		{
			List<MessageTrackingLogEntry> messageLog = context.Cache.GetMessageLog(RpcReason.None, reader, prefix, messageId, mailItemId);
			MailItemTracker item = new MailItemTracker(messageLog, context.Tree);
			remainingTrackers.Enqueue(item);
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0005D104 File Offset: 0x0005B304
		private static bool CheckRecipientListEquality(MessageTrackingLogEntry firstEntry, MessageTrackingLogEntry secondEntry)
		{
			if (firstEntry.RecipientAddresses == null || secondEntry.RecipientAddresses == null)
			{
				return false;
			}
			if (firstEntry.RecipientAddresses.Length != secondEntry.RecipientAddresses.Length)
			{
				return false;
			}
			int num = firstEntry.RecipientAddresses.Length;
			for (int i = 0; i < num; i++)
			{
				if (!firstEntry.RecipientAddresses[i].Equals(secondEntry.RecipientAddresses[i], StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0005D168 File Offset: 0x0005B368
		private static MessageTrackingLogEntry LocateModeratorResponse(MessageTrackingLogEntry initMessageEvent, TrackingContext context, out ILogReader reader)
		{
			ServerInfo serverInfo = LogDataAnalyzer.IdentifyArbitrationMailboxServer(initMessageEvent.ArbitrationMailboxAddress, context.DirectoryContext);
			if (serverInfo.ServerSiteId.Equals(ServerCache.Instance.GetLocalServerSiteId(context.DirectoryContext)))
			{
				reader = null;
				return null;
			}
			if (serverInfo.AdminDisplayVersion.ToInt() > Constants.E14SP1ModerationReferralSupportVersion)
			{
				IEnumerable<ServerInfo> casServers = ServerCache.Instance.GetCasServers(serverInfo.ServerSiteId);
				foreach (ServerInfo serverInfo2 in casServers)
				{
					if (serverInfo2.AdminDisplayVersion.ToInt() > Constants.E14SP1ModerationReferralSupportVersion)
					{
						TraceWrapper.SearchLibraryTracer.TraceError<ADObjectId>(0, "Cas in site {0} can handle x-site moderation init message referral. Init message will be handled as a referral later.", serverInfo.ServerSiteId);
						reader = null;
						return null;
					}
				}
			}
			if (!serverInfo.IsSearchable)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string, ServerStatus>(0, "Could not search mailbox server for arbitration mailbox {0}: {1}.", serverInfo.Key, serverInfo.Status);
				reader = null;
				return null;
			}
			reader = RpcLogReader.GetLogReader(serverInfo.Key, context.DirectoryContext);
			if (reader == null)
			{
				return null;
			}
			List<MessageTrackingLogEntry> messageLog = context.Cache.GetMessageLog(RpcReason.None, reader, TrackingLogPrefix.MSGTRK, initMessageEvent.InitMessageId, null, LogDataAnalyzer.validModeratorResponseTypes);
			if (messageLog.Count == 0)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string, string>(0, "No moderator responses found for Initiation Message {0} on {1}", initMessageEvent.InitMessageId, serverInfo.Key);
				return null;
			}
			return messageLog[0];
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0005D2DC File Offset: 0x0005B4DC
		private static ServerInfo IdentifyArbitrationMailboxServer(SmtpAddress arbMailboxAddress, DirectoryContext directoryContext)
		{
			PropertyDefinition[] properties = new PropertyDefinition[]
			{
				IADMailStorageSchema.ServerName
			};
			ADRawEntry adrawEntry = null;
			try
			{
				adrawEntry = directoryContext.TenantGalSession.FindByProxyAddress(new SmtpProxyAddress((string)arbMailboxAddress, true), properties);
			}
			catch (NonUniqueRecipientException ex)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string>(0, "Caught NonUniqueRecipientException when attempting to look up server for arbitration mailbox {0}", (string)arbMailboxAddress);
				TrackingFatalException.RaiseEDX(ErrorCode.InvalidADData, ex.ToString(), "Multiple users had proxy address {0}", new object[]
				{
					arbMailboxAddress
				});
			}
			if (adrawEntry == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string>(0, "Attempt to get server for arbitration mailbox '{0}' failed.", (string)arbMailboxAddress);
				TrackingFatalException.RaiseED(ErrorCode.InvalidADData, "Arbitration Mailbox not found for email-address {0}", new object[]
				{
					arbMailboxAddress
				});
			}
			string text = (string)adrawEntry[IADMailStorageSchema.ServerName];
			if (!string.IsNullOrEmpty(text))
			{
				return ServerCache.Instance.FindMailboxOrHubServer(text, 2UL);
			}
			return ServerInfo.NotFound;
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0005D3D4 File Offset: 0x0005B5D4
		private static bool IsEntryProcessed(MessageTrackingLogEntry entry, EventTree tree)
		{
			return entry.ProcessedBy == tree;
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0005D3E0 File Offset: 0x0005B5E0
		private static bool ShouldSkipTracker(ServerInfo server, MessageTrackingLogEntry logEntry, TrackingContext context)
		{
			if (!server.IsSearchable || logEntry.IsNextHopCrossSite(context.DirectoryContext))
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string, string>(0, "Skipping search logs on {0}: {1}", logEntry.NextHopFqdnOrName, server.IsSearchable ? "Server is in remote site" : "Server is not searchable");
				return true;
			}
			return false;
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0005D434 File Offset: 0x0005B634
		public List<MessageTrackingLogEntry> AnalyzeLogData(string messageId, long internalId, out MessageTrackingLogEntry rootEvent)
		{
			rootEvent = null;
			List<MessageTrackingLogEntry> startingMailItemEntries = this.GetStartingMailItemEntries(messageId, internalId);
			this.CreateInitialTracker(startingMailItemEntries);
			this.ProcessAllTrackers();
			if (this.context.Tree.Root == null)
			{
				return new List<MessageTrackingLogEntry>(0);
			}
			rootEvent = (MessageTrackingLogEntry)this.context.Tree.Root.Value;
			return this.ApplyReportTemplateToTree();
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0005D498 File Offset: 0x0005B698
		public List<List<MessageTrackingLogEntry>> GetHandedOffRecipPaths()
		{
			EventTree tree = this.context.Tree;
			ICollection<Node> leafNodes = tree.GetLeafNodes();
			if (leafNodes == null || leafNodes.Count == 0)
			{
				return null;
			}
			List<List<MessageTrackingLogEntry>> list = null;
			foreach (Node node in leafNodes)
			{
				MessageTrackingLogEntry messageTrackingLogEntry = (MessageTrackingLogEntry)node.Value;
				if ((messageTrackingLogEntry.EventId == MessageTrackingEvent.SEND && messageTrackingLogEntry.Source == MessageTrackingSource.SMTP) || (messageTrackingLogEntry.EventId == MessageTrackingEvent.EXPAND && messageTrackingLogEntry.FederatedDeliveryAddress != null) || messageTrackingLogEntry.EventId == MessageTrackingEvent.INITMESSAGECREATED)
				{
					ICollection<Node> pathToLeaf = tree.GetPathToLeaf(node.Key);
					List<MessageTrackingLogEntry> list2 = new List<MessageTrackingLogEntry>(pathToLeaf.Count);
					foreach (Node node2 in pathToLeaf)
					{
						MessageTrackingLogEntry messageTrackingLogEntry2 = (MessageTrackingLogEntry)node2.Value;
						messageTrackingLogEntry2.RecipientAddress = node2.Key;
						list2.Add(messageTrackingLogEntry2);
					}
					if (list == null)
					{
						list = new List<List<MessageTrackingLogEntry>>();
					}
					list.Add(list2);
				}
			}
			return list;
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0005D5DC File Offset: 0x0005B7DC
		private List<MessageTrackingLogEntry> GetStartingMailItemEntries(string messageId, long internalId)
		{
			ILogReader logReader = RpcLogReader.GetLogReader(this.startingServer, this.context.DirectoryContext);
			if (logReader == null)
			{
				return null;
			}
			List<MessageTrackingLogEntry> messageLog = this.cache.GetMessageLog(RpcReason.None, logReader, TrackingLogPrefix.MSGTRK, messageId, internalId);
			if (internalId == 0L && messageLog.Count > 1)
			{
				foreach (MessageTrackingLogEntry messageTrackingLogEntry in messageLog)
				{
					if (messageTrackingLogEntry.EventId == MessageTrackingEvent.MODERATORAPPROVE || messageTrackingLogEntry.EventId == MessageTrackingEvent.MODERATORREJECT || messageTrackingLogEntry.EventId == MessageTrackingEvent.MODERATIONEXPIRE)
					{
						return new List<MessageTrackingLogEntry>(1)
						{
							messageTrackingLogEntry
						};
					}
				}
				List<MessageTrackingLogEntry> list = new List<MessageTrackingLogEntry>(1);
				list.Add(messageLog.Find((MessageTrackingLogEntry entry) => entry.EventId == MessageTrackingEvent.SUBMIT));
				return list;
			}
			return messageLog;
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0005D6D4 File Offset: 0x0005B8D4
		private void CreateInitialTracker(List<MessageTrackingLogEntry> startingMailItem)
		{
			MailItemTracker item = new MailItemTracker(startingMailItem, this.context.Tree);
			this.remainingTrackers.Enqueue(item);
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0005D700 File Offset: 0x0005B900
		private void ProcessAllTrackers()
		{
			while (this.remainingTrackers.Count > 0)
			{
				MailItemTracker mailItemTracker = this.remainingTrackers.Dequeue();
				List<MessageTrackingLogEntry> terminalEvents = mailItemTracker.ProcessEntries();
				this.AddNextHops(terminalEvents);
			}
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0005D738 File Offset: 0x0005B938
		private void AddNextHops(List<MessageTrackingLogEntry> terminalEvents)
		{
			foreach (MessageTrackingLogEntry messageTrackingLogEntry in terminalEvents)
			{
				TryEnqueueNextHopDelegate tryEnqueueNextHopDelegate;
				if (LogDataAnalyzer.nextHopDelegates.TryGetValue(messageTrackingLogEntry.EventId, out tryEnqueueNextHopDelegate))
				{
					tryEnqueueNextHopDelegate(messageTrackingLogEntry, this.context, this.remainingTrackers);
				}
			}
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0005D7A8 File Offset: 0x0005B9A8
		private List<MessageTrackingLogEntry> ApplyReportTemplateToTree()
		{
			ICollection<Node> collection;
			if (this.context.ReportTemplate == ReportTemplate.RecipientPath)
			{
				collection = this.context.Tree.GetPathToLeaf(this.context.SelectedRecipient);
			}
			else
			{
				collection = this.context.Tree.GetLeafNodes();
			}
			List<MessageTrackingLogEntry> list = new List<MessageTrackingLogEntry>(collection.Count);
			foreach (Node node in collection)
			{
				MessageTrackingLogEntry messageTrackingLogEntry = (MessageTrackingLogEntry)node.Value;
				messageTrackingLogEntry.RecipientAddress = node.Key;
				list.Add(messageTrackingLogEntry);
			}
			return list;
		}

		// Token: 0x04000D23 RID: 3363
		private static readonly HashSet<MessageTrackingEvent> validModeratorResponseTypes = new HashSet<MessageTrackingEvent>
		{
			MessageTrackingEvent.MODERATORREJECT,
			MessageTrackingEvent.MODERATORAPPROVE,
			MessageTrackingEvent.MODERATIONEXPIRE
		};

		// Token: 0x04000D24 RID: 3364
		private static Dictionary<MessageTrackingEvent, TryEnqueueNextHopDelegate> nextHopDelegates = new Dictionary<MessageTrackingEvent, TryEnqueueNextHopDelegate>
		{
			{
				MessageTrackingEvent.SUBMIT,
				new TryEnqueueNextHopDelegate(LogDataAnalyzer.TryEnqueueNextHopAfterSubmit)
			},
			{
				MessageTrackingEvent.TRANSFER,
				new TryEnqueueNextHopDelegate(LogDataAnalyzer.TryEnqueueNextHopAfterTransfer)
			},
			{
				MessageTrackingEvent.SEND,
				new TryEnqueueNextHopDelegate(LogDataAnalyzer.TryEnqueueNextHopAfterSend)
			},
			{
				MessageTrackingEvent.INITMESSAGECREATED,
				new TryEnqueueNextHopDelegate(LogDataAnalyzer.TryEnqueueNextHopAfterInitMessage)
			},
			{
				MessageTrackingEvent.MODERATORAPPROVE,
				new TryEnqueueNextHopDelegate(LogDataAnalyzer.TryEnqueueNextHopAfterModeratorApprove)
			},
			{
				MessageTrackingEvent.RESUBMIT,
				new TryEnqueueNextHopDelegate(LogDataAnalyzer.TryEnqueueNextHopAfterHAResubmit)
			},
			{
				MessageTrackingEvent.HAREDIRECT,
				new TryEnqueueNextHopDelegate(LogDataAnalyzer.TryEnqueueNextHopAfterHARedirect)
			}
		};

		// Token: 0x04000D25 RID: 3365
		private static Converter<Node, MessageTrackingLogEntry> nodeToValueConverter = (Node node) => (MessageTrackingLogEntry)node.Value;

		// Token: 0x04000D26 RID: 3366
		private LogCache cache;

		// Token: 0x04000D27 RID: 3367
		private TrackingContext context;

		// Token: 0x04000D28 RID: 3368
		private Queue<MailItemTracker> remainingTrackers = new Queue<MailItemTracker>();

		// Token: 0x04000D29 RID: 3369
		private Fqdn startingServer;
	}
}
