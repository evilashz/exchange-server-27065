using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Data.Transport.Internal.MExRuntime;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Data.Transport.StoreDriver;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxTransport.StoreDriver;
using Microsoft.Exchange.MailboxTransport.StoreDriver.Shared;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Common;
using Microsoft.Exchange.Transport.Internal;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000008 RID: 8
	internal class MailItemDeliver : DisposeTrackableBase, IMessageConverter
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x000045BC File Offset: 0x000027BC
		public MailItemDeliver(MbxTransportMailItem mailItem, ulong sessionId)
		{
			bool flag = false;
			try
			{
				this.Thread = Thread.CurrentThread;
				this.mbxTransportMailItem = mailItem;
				this.sessionId = sessionId;
				this.sessionSourceContext = StoreDriverDelivery.GenerateSessionSourceContext(sessionId, mailItem.SessionStartTime);
				this.wasSessionOpenedForLastRecipient = false;
				this.currentItem = new DeliveryItem(this);
				if (this.MbxTransportMailItem != null && this.MbxTransportMailItem.RootPart != null)
				{
					Header header = this.MbxTransportMailItem.RootPart.Headers.FindFirst("X-MS-Exchange-Organization-Journal-Report");
					this.isJournalReport = (header != null);
				}
				else
				{
					this.isJournalReport = false;
				}
				if (this.currentItem.DisposeTracker != null)
				{
					this.currentItem.DisposeTracker.AddExtraDataWithStackTrace("MailItemDeliver owns currentItem at");
				}
				this.deliveredRecipients = new List<string>();
				this.deliveredRecipientsMailboxInfo = new List<string>();
				this.duplicateRecipients = new List<string>();
				this.breadcrumb = new MailItemDeliver.Breadcrumb();
				MailItemDeliver.deliveryHistory.Drop(this.breadcrumb);
				this.breadcrumb.Sender = this.MbxTransportMailItem.MimeSender.ToString();
				this.breadcrumb.MessageId = this.MbxTransportMailItem.InternetMessageId;
				this.breadcrumb.MailboxDatabase = this.MbxTransportMailItem.DatabaseName;
				this.breadcrumb.LatencyTracker = this.MbxTransportMailItem.LatencyTracker;
				MailItemDeliver.Diag.TraceDebug<long>(0L, "Delivering mailitem {0}", this.mbxTransportMailItem.RecordId);
				try
				{
					this.mexSession = MExEvents.GetExecutionContext(StoreDriverDeliveryServer.GetInstance(this.MbxTransportMailItem.OrganizationId));
					this.mexSession.Dispatcher.OnAgentInvokeEnd += this.AgentInvokeEndHandler;
				}
				catch (DataSourceTransientException exception)
				{
					throw new RetryException(new MessageStatus(MessageAction.RetryQueue, new SmtpResponse("432", "4.3.2", new string[]
					{
						"storedriver agent context transient failure"
					}), exception));
				}
				this.agentLatencyTracker = new AgentLatencyTracker(this.mexSession);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.Dispose();
				}
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000047F4 File Offset: 0x000029F4
		public int DeliveredRecipients
		{
			get
			{
				return this.deliveredRecipients.Count;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004801 File Offset: 0x00002A01
		public string Description
		{
			get
			{
				return "Deliver";
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004808 File Offset: 0x00002A08
		public bool IsOutbound
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000480B File Offset: 0x00002A0B
		public string MessageClass
		{
			get
			{
				return this.messageClass;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00004813 File Offset: 0x00002A13
		public MessageItem ReplayItem
		{
			get
			{
				return this.replayItem;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000481B File Offset: 0x00002A1B
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00004823 File Offset: 0x00002A23
		public IDeliveryItem DeliveryItem
		{
			get
			{
				return this.currentItem;
			}
			set
			{
				this.currentItem = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000482C File Offset: 0x00002A2C
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00004834 File Offset: 0x00002A34
		public MailRecipient Recipient
		{
			get
			{
				return this.recipient;
			}
			internal set
			{
				this.recipient = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000483D File Offset: 0x00002A3D
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00004845 File Offset: 0x00002A45
		public bool IsPublicFolderRecipient { get; internal set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000484E File Offset: 0x00002A4E
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00004856 File Offset: 0x00002A56
		public Guid RecipientMailboxGuid { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000BE RID: 190 RVA: 0x0000485F File Offset: 0x00002A5F
		public bool IsJournalReport
		{
			get
			{
				return this.isJournalReport;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004867 File Offset: 0x00002A67
		public TimeSpan? RetryInterval
		{
			get
			{
				return this.retryInterval;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x0000486F File Offset: 0x00002A6F
		public MbxMailItemWrapper MailItemWrapper
		{
			get
			{
				return this.mailItemWrapper;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004877 File Offset: 0x00002A77
		public Trace Tracer
		{
			get
			{
				return ExTraceGlobals.StoreDriverDeliveryTracer;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x0000487E File Offset: 0x00002A7E
		internal static DeliveryRecipientThreadMap RecipientThreadMap
		{
			get
			{
				return MailItemDeliver.recipientThreadMap;
			}
		}

		// Token: 0x17000064 RID: 100
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00004885 File Offset: 0x00002A85
		internal string DeliverToFolderName
		{
			set
			{
				this.deliverToFolderName = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000488E File Offset: 0x00002A8E
		internal ulong SessionId
		{
			get
			{
				return this.sessionId;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00004896 File Offset: 0x00002A96
		internal MbxTransportMailItem MbxTransportMailItem
		{
			get
			{
				return this.mbxTransportMailItem;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000489E File Offset: 0x00002A9E
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x000048A6 File Offset: 0x00002AA6
		internal bool WasSessionOpenedForLastRecipient
		{
			get
			{
				return this.wasSessionOpenedForLastRecipient;
			}
			set
			{
				this.wasSessionOpenedForLastRecipient = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000048AF File Offset: 0x00002AAF
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x000048B7 File Offset: 0x00002AB7
		internal List<KeyValuePair<string, string>> ExtraTrackingEventData { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000048C0 File Offset: 0x00002AC0
		// (set) Token: 0x060000CB RID: 203 RVA: 0x000048C8 File Offset: 0x00002AC8
		internal ExDateTime RecipientStartTime { get; private set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000048D1 File Offset: 0x00002AD1
		// (set) Token: 0x060000CD RID: 205 RVA: 0x000048D9 File Offset: 0x00002AD9
		internal MailItemDeliver.DeliveryStage Stage
		{
			get
			{
				return this.deliveryStage;
			}
			set
			{
				this.deliveryStage = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000048E2 File Offset: 0x00002AE2
		// (set) Token: 0x060000CF RID: 207 RVA: 0x000048EA File Offset: 0x00002AEA
		internal MailItemDeliver.Breadcrumb DeliveryBreadcrumb
		{
			get
			{
				return this.breadcrumb;
			}
			set
			{
				this.breadcrumb = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000048F3 File Offset: 0x00002AF3
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x000048FB File Offset: 0x00002AFB
		internal int? DatabaseHealthMeasureToLog
		{
			get
			{
				return this.databaseHealthMeasureToLog;
			}
			set
			{
				this.databaseHealthMeasureToLog = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004904 File Offset: 0x00002B04
		internal StoreDriverDeliveryEventArgsImpl EventArguments
		{
			get
			{
				return this.eventArguments;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x0000490C File Offset: 0x00002B0C
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00004914 File Offset: 0x00002B14
		internal Thread Thread { get; private set; }

		// Token: 0x060000D5 RID: 213 RVA: 0x00004920 File Offset: 0x00002B20
		public void DeliverToRecipients()
		{
			PerformanceContext performanceContext = new PerformanceContext(PerformanceContext.Current);
			this.recipient = this.MbxTransportMailItem.GetNextRecipient();
			try
			{
				while (this.recipient != null)
				{
					TraceHelper.StoreDriverDeliveryTracer.TracePass<MailRecipient>(TraceHelper.MessageProbeActivityId, 0L, "Delivery: processing started for recipient {0}.", this.recipient);
					this.breadcrumb.RecipientCount++;
					MessageStatus messageStatus = this.DeliverToRecipient();
					if (messageStatus.NDRForAllRecipients)
					{
						this.FailAllRecipients(messageStatus);
						TraceHelper.StoreDriverDeliveryTracer.TraceFail(TraceHelper.MessageProbeActivityId, 0L, "Delivery: processing failed NDR being generated for all recipients.");
						break;
					}
					TraceHelper.StoreDriverDeliveryTracer.TracePass<MailRecipient>(TraceHelper.MessageProbeActivityId, 0L, "Delivery: processing completed for recipient {0}.", this.recipient);
					this.recipient = this.MbxTransportMailItem.GetNextRecipient();
				}
			}
			finally
			{
				this.breadcrumb.DeliveredToAll = ExDateTime.UtcNow.Subtract(this.breadcrumb.RecordCreation);
				TimeSpan timeSpan = PerformanceContext.Current.Latency - performanceContext.Latency;
				LatencyTracker.TrackExternalComponentLatency(LatencyComponent.StoreDriverDeliveryAD, this.MbxTransportMailItem.LatencyTracker, timeSpan);
				this.breadcrumb.LdapLatency = timeSpan;
				string arg = this.MbxTransportMailItem.InternetMessageId ?? "NoMessageId";
				MailItemDeliver.Diag.TraceDebug<TimeSpan, string>(0L, "Ldap latency {0} recorded for message {1}", timeSpan, arg);
				MailItemDeliver.Diag.TraceDebug<TimeSpan, string>(0L, "RPC latency {0} recorded for message {1}", this.rpcLatency, arg);
				LatencyTracker.TrackExternalComponentLatency(LatencyComponent.StoreDriverDeliveryStore, this.MbxTransportMailItem.LatencyTracker, this.storeLatency);
				MailItemDeliver.Diag.TraceDebug<TimeSpan, string>(0L, "Store latency {0} recorded for message {1}", this.storeLatency, arg);
			}
			this.WriteDeliveredAndProcessedTrackingLog();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004B0C File Offset: 0x00002D0C
		public MessageStatus DeliverToRecipient()
		{
			MessageStatus messageStatus = null;
			this.RecipientStartTime = ExDateTime.UtcNow;
			string exceptionAgentName = string.Empty;
			using (ActivityScope activityScope = ActivityContext.Start(this))
			{
				activityScope.Component = "MailItemDeliver";
				try
				{
					MSExchangeStoreDriver.PendingDeliveries.Increment();
					this.deliveryStage = this.breadcrumb.SetStage(MailItemDeliver.DeliveryStage.Start);
					StoreDriverDeliveryPerfCounters.Instance.IncrementDeliveryAttempt(false);
					StoreDriverDatabasePerfCounters.IncrementDeliveryAttempt(this.mbxTransportMailItem.DatabaseGuid.ToString(), false);
					messageStatus = StorageExceptionHandler.RunUnderExceptionHandler(this, delegate
					{
						try
						{
							this.DeliverMessageForRecipient();
						}
						catch (StoreDriverAgentRaisedException ex)
						{
							exceptionAgentName = ex.AgentName;
							throw;
						}
					});
					if (!string.IsNullOrEmpty(exceptionAgentName))
					{
						this.recipient.ExtendedProperties.SetValue<string>("ExceptionAgentName", exceptionAgentName);
					}
					this.AcknowledgeDeliveryStatusForRecipient(messageStatus);
					if (this.IsPermanentFailure(messageStatus.Action) && !string.IsNullOrEmpty(exceptionAgentName))
					{
						MailItemDeliver.UpdateAgentDeliveryFailureStatistics(exceptionAgentName);
					}
					StoreDriverDeliveryPerfCounters.Instance.AddDeliveryLatencySample(ExDateTime.UtcNow - this.RecipientStartTime, false);
				}
				finally
				{
					MSExchangeStoreDriver.PendingDeliveries.Decrement();
					messageStatus = this.RaiseOnCompletedMessage();
					this.DecrementThrottlingForCurrentRecipient();
					this.DisposeCurrentMessageItem();
				}
			}
			MSExchangeStoreDriver.RecipientsDelivered.Increment();
			return messageStatus;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004C78 File Offset: 0x00002E78
		public void SetDeliveryFolder(Folder folder)
		{
			ArgumentValidator.ThrowIfNull("folder", folder);
			if (folder.Id == null || folder.Id.ObjectId == null)
			{
				throw new ArgumentException("Folder ID is null or incomplete", "folder");
			}
			this.currentItem.DeliverToFolder = folder.Id;
			this.deliverToFolderName = folder.DisplayName;
			string text = null;
			if (folder.Id.ObjectId.Equals(this.currentItem.MailboxSession.GetDefaultFolderId(DefaultFolderType.DeletedItems)))
			{
				text = "Deleted Items";
			}
			else if (folder.Id.ObjectId.Equals(this.currentItem.MailboxSession.GetDefaultFolderId(DefaultFolderType.JunkEmail)))
			{
				text = "Junk Email";
			}
			if (text != null && this.deliverToFolderName != null && !text.Equals(this.deliverToFolderName, StringComparison.Ordinal))
			{
				this.deliverToFolderName = string.Format(CultureInfo.InvariantCulture, "{0}<{1}>", new object[]
				{
					this.deliverToFolderName,
					text
				});
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004D6C File Offset: 0x00002F6C
		public void LogMessage(Exception exception)
		{
			if (exception is StoragePermanentException && exception.InnerException != null && exception.InnerException is MapiExceptionDuplicateDelivery)
			{
				return;
			}
			if (exception is SmtpResponseException)
			{
				return;
			}
			ItemConversion.SaveFailedMimeDocument(this.MbxTransportMailItem.Message, exception, Components.Configuration.LocalServer.ContentConversionTracingPath);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004DC0 File Offset: 0x00002FC0
		public void AddDeliveryErrors(List<string> errorRecords)
		{
			if (errorRecords == null || errorRecords.Count == 0)
			{
				return;
			}
			if (this.deliveryErrors == null)
			{
				this.deliveryErrors = new List<string>(errorRecords.Count);
			}
			foreach (string item in errorRecords)
			{
				this.deliveryErrors.Add(item);
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004E38 File Offset: 0x00003038
		internal static XElement GetDiagnosticInfo()
		{
			XElement xelement = new XElement("DeliveryHistory");
			foreach (MailItemDeliver.Breadcrumb breadcrumb in ((IEnumerable<MailItemDeliver.Breadcrumb>)MailItemDeliver.deliveryHistory))
			{
				xelement.Add(breadcrumb.GetDiagnosticInfo());
			}
			return xelement;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004E9C File Offset: 0x0000309C
		internal void AddRpcLatency(TimeSpan additionalLatency, string rpcType)
		{
			this.rpcLatency += additionalLatency;
			this.breadcrumb.RpcLatency = this.rpcLatency;
			MailItemDeliver.Diag.TraceDebug(0L, "{0} latency {1} added to for message {2}, making total RPC latency {3}", new object[]
			{
				rpcType,
				additionalLatency,
				this.MbxTransportMailItem.InternetMessageId ?? "NoMessageId",
				this.rpcLatency
			});
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004F17 File Offset: 0x00003117
		internal void BeginTrackLatency(LatencyComponent component)
		{
			if (this.MbxTransportMailItem != null)
			{
				LatencyTracker.BeginTrackLatency(component, this.MbxTransportMailItem.LatencyTracker);
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004F32 File Offset: 0x00003132
		internal TimeSpan EndTrackLatency(LatencyComponent component)
		{
			if (this.mbxTransportMailItem != null)
			{
				return LatencyTracker.EndTrackLatency(component, this.mbxTransportMailItem.LatencyTracker, true);
			}
			return TimeSpan.Zero;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004F54 File Offset: 0x00003154
		internal void CreateSession(DeliverableItem item)
		{
			this.currentItem.CreateSession(this.recipient, this.deliveryFlags, item, this.recipientLanguages);
			this.breadcrumb.MailboxServer = this.GetMailboxServerName();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004F88 File Offset: 0x00003188
		internal bool LoadMessageForAgentEventsRetry()
		{
			try
			{
				if (this.currentItem.DeliverToFolder == null)
				{
					this.currentItem.DeliverToFolder = this.currentItem.MailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox);
				}
				this.currentItem.LoadMailboxMessage(this.replayItem.InternetMessageId);
				ExTraceGlobals.FaultInjectionTracer.TraceTest(64128U);
				ExTraceGlobals.FaultInjectionTracer.TraceTest(2265328957U);
			}
			catch (UnexpectedMessageCountException arg)
			{
				MailItemDeliver.Diag.TraceWarning<UnexpectedMessageCountException>(0L, "Unable to load the message to allow delivery agent event retry. Error {0}.", arg);
				return false;
			}
			catch (System.Data.ObjectNotFoundException arg2)
			{
				MailItemDeliver.Diag.TraceDebug<System.Data.ObjectNotFoundException>(0L, "Unable to load the message to allow delivery agent event retry. This operation will be retried. Error {0}.", arg2);
				return false;
			}
			return true;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005044 File Offset: 0x00003244
		internal void CreateMessage(DeliverableItem item)
		{
			if (this.IsPublicFolderRecipient)
			{
				this.currentItem.CreatePublicFolderMessage(this.recipient, item);
				return;
			}
			this.currentItem.CreateMailboxMessage(this.originalReceivedTime != null);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005078 File Offset: 0x00003278
		internal TimeSpan RaiseEvent(string deliveryEventBindings, LatencyComponent eventComponent)
		{
			DateTime utcNow = DateTime.UtcNow;
			TimeSpan result;
			try
			{
				this.agentLatencyTracker.BeginTrackLatency(eventComponent, this.MbxTransportMailItem.LatencyTracker);
				MExEvents.RaiseEvent(this.mexSession, deliveryEventBindings, new object[]
				{
					MailItemDeliver.eventSource,
					this.eventArguments
				});
				try
				{
					ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(3540397373U, deliveryEventBindings);
				}
				catch (LocalizedException actualAgentException)
				{
					throw new StoreDriverAgentRaisedException("Fault injection.", actualAgentException);
				}
			}
			finally
			{
				result = DateTime.UtcNow - utcNow;
				this.agentLatencyTracker.EndTrackLatency();
				this.MbxTransportMailItem.TrackAgentInfo();
			}
			return result;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005128 File Offset: 0x00003328
		internal void PromotePropertiesToItem()
		{
			if (this.spamConfidenceLevel != null)
			{
				this.currentItem.SetProperty(ItemSchema.SpamConfidenceLevel, this.spamConfidenceLevel);
			}
			if (this.originalReceivedTime != null)
			{
				this.currentItem.SetProperty(ItemSchema.ReceivedTime, this.originalReceivedTime.Value.ToUniversalTime());
			}
			this.PromoteDrmPropertiesToItem();
			byte[] array = null;
			if (!string.IsNullOrEmpty(this.recipient.ORcpt))
			{
				array = RedirectionHistory.GenerateRedirectionHistoryFromOrcpt(this.recipient.ORcpt);
			}
			if (array != null)
			{
				MailItemDeliver.Diag.TraceDebug<string>(0L, "Promote Redirection History {0}", this.recipient.Email.ToString());
				this.currentItem.SetProperty(RecipientSchema.RedirectionHistory, array);
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000051F4 File Offset: 0x000033F4
		internal void ExtractCulture()
		{
			ADRecipientCache<TransportMiniRecipient> adrecipientCache = this.MbxTransportMailItem.ADRecipientCache;
			ProxyAddress proxyAddress = new SmtpProxyAddress((string)this.recipient.Email, true);
			TransportMiniRecipient data = adrecipientCache.FindAndCacheRecipient(proxyAddress).Data;
			if (data != null)
			{
				this.recipientLanguages = data.Languages;
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005244 File Offset: 0x00003444
		internal void DeliverItem()
		{
			ExTraceGlobals.FaultInjectionTracer.TraceTest(55936U);
			if (this.eventArguments.PropertiesForAllMessageCopies != null)
			{
				foreach (KeyValuePair<PropertyDefinition, object> keyValuePair in this.eventArguments.PropertiesForAllMessageCopies)
				{
					this.currentItem.Message.SetOrDeleteProperty(keyValuePair.Key, keyValuePair.Value);
				}
			}
			this.BeginTrackLatency(LatencyComponent.StoreDriverDeliveryRpc);
			try
			{
				ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(2227580221U, this.currentItem.Message.ConversationTopic);
				ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(61072U, this.currentItem.Session.MdbGuid.ToString());
				ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(36496U, this.currentItem.Session.ServerFullyQualifiedDomainName);
				ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(44688U, (this.currentItem.MailboxSession != null) ? this.currentItem.MailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString() : null);
				this.currentItem.Deliver(ProxyAddress.Parse(ProxyAddressPrefix.Smtp.PrimaryPrefix, (string)this.recipient.Email));
			}
			catch (StoragePermanentException exception)
			{
				this.LogMessage(exception);
				throw;
			}
			finally
			{
				TimeSpan additionalLatency = this.EndTrackLatency(LatencyComponent.StoreDriverDeliveryRpc);
				this.AddRpcLatency(additionalLatency, "Deliver");
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000053F4 File Offset: 0x000035F4
		internal void CreateReplay()
		{
			if (this.replayCreated)
			{
				return;
			}
			try
			{
				this.BeginTrackLatency(LatencyComponent.StoreDriverDeliveryContentConversion);
				this.GetInboundConversionOptions();
				this.GetSpamConfidenceLevel();
				this.GetOriginalReceivedTime();
				this.GetDeliveryFlags();
				this.GetVirusScanningStamps();
				this.CreateReplayItem();
				this.replayCreated = true;
			}
			finally
			{
				this.breadcrumb.ContentConversionLatency = this.EndTrackLatency(LatencyComponent.StoreDriverDeliveryContentConversion);
				MailItemDeliver.Diag.TraceDebug<TimeSpan, string>(0L, "Content conversion latency {0} for message {1}", this.breadcrumb.ContentConversionLatency, this.MbxTransportMailItem.InternetMessageId ?? "NoMessageId");
			}
			this.PromoteDrmPropertiesToReplayItem();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000549C File Offset: 0x0000369C
		internal void ClearRetryOnDuplicateDelivery()
		{
			if (this.recipient != null)
			{
				this.recipient.ExtendedProperties.Remove("Microsoft.Exchange.Transport.MailboxTransport.RetryOnDuplicateDelivery ");
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000054BC File Offset: 0x000036BC
		internal void RaiseOnDeliveredEvent()
		{
			this.deliveryStage = this.breadcrumb.SetStage(MailItemDeliver.DeliveryStage.OnDeliveredEvent);
			try
			{
				this.RaiseEvent("OnDeliveredMessage", LatencyComponent.StoreDriverOnDeliveredMessage);
				this.deliveryStage = this.breadcrumb.SetStage(MailItemDeliver.DeliveryStage.Done);
			}
			finally
			{
				TraceHelper.StoreDriverDeliveryTracer.TracePass<MailRecipient>(TraceHelper.MessageProbeActivityId, 0L, "Delivery: OnDeliveredMessage complete for recipient {0}.", this.recipient);
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000552C File Offset: 0x0000372C
		internal void AddRecipientInfoForDeliveredEvent()
		{
			this.AddDeliveryErrors(ref this.deliveredRecipientsErrors, this.deliveryErrors);
			MailItemDeliver.AddFolderNameIfNeeded(ref this.deliveredRecipientsFolderNames, this.deliverToFolderName, this.deliveredRecipients.Count);
			this.deliveredRecipients.Add(this.recipient.Email.ToString());
			this.deliveredRecipientsMailboxInfo.Add(this.RecipientMailboxGuid.ToString());
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000055AA File Offset: 0x000037AA
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MailItemDeliver>(this);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000055B4 File Offset: 0x000037B4
		protected override void InternalDispose(bool disposing)
		{
			if (this.currentItem != null && this.currentItem.DisposeTracker != null)
			{
				this.currentItem.DisposeTracker.AddExtraDataWithStackTrace(string.Format(CultureInfo.InvariantCulture, "MailItemDeliver.InternalDispose({0}) called with stack", new object[]
				{
					disposing
				}));
			}
			if (disposing)
			{
				try
				{
					if (this.currentItem != null)
					{
						this.currentItem.Dispose();
						this.currentItem = null;
					}
					if (this.replayItem != null)
					{
						this.replayItem.Dispose();
						this.replayItem = null;
					}
					if (this.agentLatencyTracker != null)
					{
						this.agentLatencyTracker.Dispose();
						this.agentLatencyTracker = null;
					}
				}
				finally
				{
					if (this.mexSession != null)
					{
						MExEvents.FreeExecutionContext(this.mexSession);
						this.mexSession = null;
					}
				}
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005684 File Offset: 0x00003884
		private static bool IsPublicFolder(MailRecipient recipient)
		{
			DeliverableItem deliverableItem = RecipientItem.Create(recipient) as DeliverableItem;
			return deliverableItem.RecipientType == Microsoft.Exchange.Data.Directory.Recipient.RecipientType.PublicFolder;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000056A7 File Offset: 0x000038A7
		private static void AddFolderNameIfNeeded(ref List<string> folderNameList, string folderName, int count)
		{
			if (folderNameList == null)
			{
				if (string.IsNullOrEmpty(folderName))
				{
					return;
				}
				folderNameList = new List<string>(count);
				while (count > 0)
				{
					folderNameList.Add(null);
					count--;
				}
			}
			folderNameList.Add(folderName);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000056DC File Offset: 0x000038DC
		private static string CombineDeliveryErrors(List<string> errors)
		{
			string result = null;
			if (errors != null && errors.Count > 0)
			{
				result = string.Join(" ", errors.ToArray());
			}
			return result;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005709 File Offset: 0x00003909
		private static void UpdateAgentDeliveryFailureStatistics(string exceptionAgentName)
		{
			StoreDriverDeliveryAgentPerfCounters.IncrementAgentDeliveryAttempt(exceptionAgentName);
			StoreDriverDeliveryAgentPerfCounters.IncrementAgentDeliveryFailure(exceptionAgentName);
			StoreDriverDeliveryAgentPerfCounters.RefreshAgentDeliveryPercentCounter(exceptionAgentName);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005720 File Offset: 0x00003920
		private void DeliverMessageForRecipient()
		{
			this.Initialize();
			ExTraceGlobals.FaultInjectionTracer.TraceTest(57488U);
			IDeliveryProcessor deliveryProcessor = DeliveryProcessorFactory.Create(this);
			this.deliveryStage = this.breadcrumb.SetStage(MailItemDeliver.DeliveryStage.OnInitializedEvent);
			deliveryProcessor.Initialize();
			TraceHelper.StoreDriverDeliveryTracer.TracePass<MailRecipient>(TraceHelper.MessageProbeActivityId, 0L, "Delivery: OnInitializedMessage complete for recipient {0}.", this.recipient);
			DeliverableItem item = deliveryProcessor.CreateSession();
			TraceHelper.StoreDriverDeliveryTracer.TracePass<MailRecipient>(TraceHelper.MessageProbeActivityId, 0L, "Delivery: OnPromotedMessage complete for recipient {0}.", this.recipient);
			deliveryProcessor.CreateMessage(item);
			TraceHelper.StoreDriverDeliveryTracer.TracePass<MailRecipient>(TraceHelper.MessageProbeActivityId, 0L, "Delivery: OnCreatedMessage complete for recipient {0}.", this.recipient);
			deliveryProcessor.DeliverMessage();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000057CC File Offset: 0x000039CC
		private void AcknowledgeDeliveryStatusForRecipient(MessageStatus messageStatus)
		{
			if (messageStatus.Action != MessageAction.Retry)
			{
				this.ClearRetryOnDuplicateDelivery();
			}
			if (messageStatus.Action == MessageAction.Success)
			{
				this.ProcessDeliveryForRecipient();
				TraceHelper.StoreDriverDeliveryTracer.TracePass<MailRecipient, MessageAction>(TraceHelper.MessageProbeActivityId, 0L, "Delivery: Processing complete for recipient {0} Action: {1}.", this.recipient, messageStatus.Action);
				return;
			}
			TraceHelper.StoreDriverDeliveryTracer.TraceFail<MailRecipient, MessageAction>(TraceHelper.MessageProbeActivityId, 0L, "Delivery: Processing complete for recipient {0} Action: {1}.", this.recipient, messageStatus.Action);
			StoreDriverDeliveryDiagnostics.RecordExceptionForDiagnostics(messageStatus, this);
			switch (messageStatus.Action)
			{
			case MessageAction.Retry:
				if (this.deliveryStage != MailItemDeliver.DeliveryStage.OnDeliveredEvent || !(messageStatus.Exception is StoreDriverAgentTransientException))
				{
					ExTraceGlobals.FaultInjectionTracer.TraceTest(65168U);
					this.ClearRetryOnDuplicateDelivery();
					this.AckRetryRecipient(messageStatus.Response);
					if (messageStatus.RetryInterval != null && (this.retryInterval == null || messageStatus.RetryInterval.Value < this.retryInterval.Value))
					{
						this.retryInterval = messageStatus.RetryInterval;
					}
					MSExchangeStoreDriver.DeliveryRetry.Increment();
					this.IncrementDeliveryFailure(messageStatus.Action);
					return;
				}
				ExTraceGlobals.FaultInjectionTracer.TraceTest(50816U);
				MailItemDeliver.Diag.TraceDebug(0L, "Store Driver Agent transient exception thrown during the OnDeliveredMessage event. The message was delivered but is being retried to complete message processing.");
				if (this.recipient.ExtendedProperties.GetValue<bool>("Microsoft.Exchange.Transport.MailboxTransport.RetryOnDuplicateDelivery ", false))
				{
					this.ProcessDuplicateDeliveryForRecipient();
					return;
				}
				this.recipient.ExtendedProperties.SetValue<bool>("Microsoft.Exchange.Transport.MailboxTransport.RetryOnDuplicateDelivery ", true);
				MailItemDeliver.Diag.TraceDebug(0L, "RetryOnDuplicateDelivery set by delivery processing for incoming message.");
				this.ProcessDeliveryForRecipient();
				return;
			case MessageAction.RetryQueue:
				ExTraceGlobals.FaultInjectionTracer.TraceTest(61584U);
				this.IncrementDeliveryFailure(messageStatus.Action);
				throw new RetryException(messageStatus, this.FormatStoreDriverContext());
			case MessageAction.NDR:
				ExTraceGlobals.FaultInjectionTracer.TraceTest(48784U);
				this.AckFailedRecipient(messageStatus.Response);
				this.IncrementDeliveryFailure(messageStatus.Action);
				return;
			case MessageAction.Reroute:
				ExTraceGlobals.FaultInjectionTracer.TraceTest(2445684029U);
				this.AckRerouteRecipient(messageStatus.Response);
				this.recipient.SmtpResponse = messageStatus.Response;
				MSExchangeStoreDriver.DeliveryReroute.Increment();
				this.IncrementDeliveryFailure(messageStatus.Action);
				return;
			case MessageAction.LogDuplicate:
				ExTraceGlobals.FaultInjectionTracer.TraceTest(47744U);
				this.ProcessDuplicateDeliveryForRecipient();
				return;
			case MessageAction.LogProcess:
				this.AckDeliveredRecipient();
				this.AddProcessedRecipient((string)this.recipient.Email, messageStatus);
				MSExchangeStoreDriver.SuccessfulDeliveries.Increment();
				MSExchangeStoreDriver.BytesDelivered.IncrementBy(this.MbxTransportMailItem.MimeSize);
				return;
			}
			throw new InvalidOperationException("Unexpected message action!");
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005AB8 File Offset: 0x00003CB8
		private MessageStatus RaiseOnCompletedMessage()
		{
			string exceptionAgentName = string.Empty;
			MessageStatus messageStatus = StorageExceptionHandler.RunUnderExceptionHandler(this, delegate
			{
				try
				{
					this.RaiseEvent("OnCompletedMessage", LatencyComponent.StoreDriverOnCompletedMessage);
				}
				catch (StoreDriverAgentRaisedException ex)
				{
					exceptionAgentName = ex.AgentName;
					throw;
				}
			});
			ExTraceGlobals.FaultInjectionTracer.TraceTest<MessageAction>(49504U, messageStatus.Action);
			if (messageStatus.Action != MessageAction.Success)
			{
				MailItemDeliver.Diag.TraceDebug<SmtpResponse>(0L, "OnCompletedMessage failed: {0}", messageStatus.Response);
				if (this.IsPermanentFailure(messageStatus.Action) && !string.IsNullOrEmpty(exceptionAgentName))
				{
					MailItemDeliver.UpdateAgentDeliveryFailureStatistics(exceptionAgentName);
				}
			}
			return messageStatus;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005B4C File Offset: 0x00003D4C
		private void DisposeCurrentMessageItem()
		{
			if (this.currentItem != null)
			{
				if (this.currentItem.DisposeTracker != null)
				{
					this.currentItem.DisposeTracker.AddExtraDataWithStackTrace("MailItemDeliver.DeliverToRecipient disposing currentItem with stack");
				}
				if (this.currentItem.Session != null)
				{
					this.AddStoreLatency(this.currentItem.Session.GetStoreCumulativeRPCStats().timeInServer);
				}
				this.currentItem.DisposeMessageAndSession();
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005BBC File Offset: 0x00003DBC
		private string FormatStoreDriverContext()
		{
			string text = null;
			if ((this.deliveryStage == MailItemDeliver.DeliveryStage.OnInitializedEvent || this.deliveryStage == MailItemDeliver.DeliveryStage.OnPromotedEvent || this.deliveryStage == MailItemDeliver.DeliveryStage.OnCreatedEvent || this.deliveryStage == MailItemDeliver.DeliveryStage.OnDeliveredEvent) && this.mexSession != null)
			{
				text = this.mexSession.LastAgentName;
			}
			string text2 = this.deliveryStage.ToString();
			string result;
			if (!string.IsNullOrEmpty(text))
			{
				result = string.Format(CultureInfo.InvariantCulture, "[Stage: {0}][Agent: {1}]", new object[]
				{
					text2,
					text
				});
			}
			else
			{
				result = string.Format(CultureInfo.InvariantCulture, "[Stage: {0}]", new object[]
				{
					text2
				});
			}
			return result;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005C5F File Offset: 0x00003E5F
		private void AgentInvokeEndHandler(object dispatcher, MExSession session)
		{
			StoreDriverDeliveryAgentPerfCounters.IncrementAgentDeliveryAttempt(session.CurrentAgent.Name);
			StoreDriverDeliveryAgentPerfCounters.RefreshAgentDeliveryPercentCounter(session.CurrentAgent.Name);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005C84 File Offset: 0x00003E84
		private void FailAllRecipients(MessageStatus deliveryStatus)
		{
			MailItemDeliver.Diag.TraceDebug<string>(0L, "Fail all recipients with the same response {0}", deliveryStatus.Response.EnhancedStatusCode);
			this.recipient = this.MbxTransportMailItem.GetNextRecipient();
			while (this.recipient != null)
			{
				this.AckFailedRecipient(deliveryStatus.Response);
				MSExchangeStoreDriver.RecipientsDelivered.Increment();
				this.IncrementDeliveryFailure(MessageAction.NDR);
				this.recipient = this.MbxTransportMailItem.GetNextRecipient();
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005CFC File Offset: 0x00003EFC
		private void AddProcessedRecipient(string recipient, MessageStatus status)
		{
			if (this.processedRecipients == null)
			{
				this.processedRecipients = new List<string>(1);
				this.processedRecipientsSources = new List<string>(1);
			}
			this.processedRecipients.Add(recipient);
			string item = string.Format("{0};{1}", this.FormatStoreDriverContext(), status.Response.StatusText[0]);
			this.processedRecipientsSources.Add(item);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005D64 File Offset: 0x00003F64
		private void PromoteDrmPropertiesToItem()
		{
			string text;
			if (this.recipient.ExtendedProperties.TryGetValue<string>("Microsoft.Exchange.RightsManagement.B2BDRMLicense", out text) && !string.IsNullOrEmpty(text))
			{
				MailItemDeliver.Diag.TraceDebug<RoutingAddress>(0L, "Promote DRMServerLicenseCompressed property for the B2B case. Recipient {0}", this.recipient.Email);
				using (Stream stream = this.currentItem.OpenPropertyStream(MessageItemSchema.DRMServerLicenseCompressed, PropertyOpenMode.Create))
				{
					DrmEmailCompression.CompressUseLicense(text, stream);
				}
			}
			ReadOnlyCollection<byte> readOnlyCollection;
			int num;
			if (this.recipient.ExtendedProperties.TryGetListValue<byte>("Microsoft.Exchange.RightsManagement.DRMLicense", out readOnlyCollection) && readOnlyCollection != null)
			{
				MailItemDeliver.Diag.TraceDebug<RoutingAddress>(0L, "Promote License for recipient {0}", this.recipient.Email);
				byte[][] array = new byte[][]
				{
					new byte[readOnlyCollection.Count]
				};
				readOnlyCollection.CopyTo(array[0], 0);
				this.currentItem.SetProperty(MessageItemSchema.DRMLicense, array);
			}
			else if (this.recipient.ExtendedProperties.TryGetValue<int>("Microsoft.Exchange.RightsManagement.DRMFailure", out num))
			{
				MailItemDeliver.Diag.TraceDebug<RoutingAddress>(0L, "Promote DRMPrelicenseFailure property for recipient {0}", this.recipient.Email);
				this.currentItem.SetProperty(MessageItemSchema.DRMPrelicenseFailure, num);
			}
			int num2;
			if (this.recipient.ExtendedProperties.TryGetValue<int>("Microsoft.Exchange.RightsManagement.DRMRights", out num2))
			{
				MailItemDeliver.Diag.TraceDebug<RoutingAddress>(0L, "Promote DRMRights property for recipient {0}", this.recipient.Email);
				this.currentItem.SetProperty(MessageItemSchema.DRMRights, num2);
			}
			DateTime dateTime;
			if (this.recipient.ExtendedProperties.TryGetValue<DateTime>("Microsoft.Exchange.RightsManagement.DRMExpiryTime", out dateTime))
			{
				MailItemDeliver.Diag.TraceDebug<RoutingAddress>(0L, "Promote DRMExpiryTime property for recipient {0}", this.recipient.Email);
				this.currentItem.SetProperty(MessageItemSchema.DRMExpiryTime, new ExDateTime(ExTimeZone.TimeZoneFromKind(DateTimeKind.Utc), dateTime));
			}
			ReadOnlyCollection<byte> readOnlyCollection2;
			if (this.recipient.ExtendedProperties.TryGetListValue<byte>("Microsoft.Exchange.RightsManagement.DRMPropsSignature", out readOnlyCollection2))
			{
				MailItemDeliver.Diag.TraceDebug<RoutingAddress>(0L, "Promote DRMPropsSignature property for recipient {0}", this.recipient.Email);
				byte[] array2 = new byte[readOnlyCollection2.Count];
				readOnlyCollection2.CopyTo(array2, 0);
				this.currentItem.SetProperty(MessageItemSchema.DRMPropsSignature, array2);
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005FA0 File Offset: 0x000041A0
		private void GetInboundConversionOptions()
		{
			this.conversionOptions = new InboundConversionOptions(Components.Configuration.FirstOrgAcceptedDomainTable.DefaultDomainName);
			this.conversionOptions.IsSenderTrusted = MultilevelAuth.IsInternalMail(this.MbxTransportMailItem);
			this.conversionOptions.ServerSubmittedSecurely = MultilevelAuth.IsAuthenticated(this.MbxTransportMailItem);
			this.conversionOptions.ClearCategories = this.MbxTransportMailItem.TransportSettings.ClearCategories;
			this.conversionOptions.Limits.MimeLimits = MimeLimits.Unlimited;
			this.conversionOptions.PreserveReportBody = this.MbxTransportMailItem.TransportSettings.PreserveReportBodypart;
			this.conversionOptions.ConvertReportToMessage = this.MbxTransportMailItem.TransportSettings.ConvertReportToMessage;
			this.conversionOptions.LogDirectoryPath = Components.Configuration.LocalServer.ContentConversionTracingPath;
			this.conversionOptions.DetectionOptions.PreferredInternetCodePageForShiftJis = Components.TransportAppConfig.ContentConversion.PreferredInternetCodePageForShiftJis;
			this.conversionOptions.HeaderPromotion = Configuration.TransportConfigObject.HeaderPromotionModeSetting;
			if (this.MbxTransportMailItem.TransportSettings.OpenDomainRoutingEnabled)
			{
				this.conversionOptions.RecipientCache = new EmptyRecipientCache();
				this.conversionOptions.UserADSession = null;
			}
			else
			{
				this.conversionOptions.RecipientCache = this.MbxTransportMailItem.ADRecipientCache;
				this.conversionOptions.UserADSession = this.MbxTransportMailItem.ADRecipientCache.ADSession;
			}
			this.conversionOptions.TreatInlineDispositionAsAttachment = Components.TransportAppConfig.ContentConversion.TreatInlineDispositionAsAttachment;
			EmailRecipient sender = this.MbxTransportMailItem.Message.Sender;
			if (sender != null)
			{
				RoutingAddress smtpAddress = new RoutingAddress(sender.SmtpAddress);
				if (smtpAddress.IsValid)
				{
					RemoteDomainEntry domainContentConfig = ContentConverter.GetDomainContentConfig(smtpAddress, this.MbxTransportMailItem.OrganizationId);
					Charset defaultCharset = null;
					if (domainContentConfig != null)
					{
						this.conversionOptions.DetectionOptions.PreferredInternetCodePageForShiftJis = (int)domainContentConfig.PreferredInternetCodePageForShiftJis;
						if (domainContentConfig.RequiredCharsetCoverage != null)
						{
							this.conversionOptions.DetectionOptions.RequiredCoverage = domainContentConfig.RequiredCharsetCoverage.Value;
						}
						if (Charset.TryGetCharset(domainContentConfig.CharacterSet, out defaultCharset))
						{
							this.conversionOptions.DefaultCharset = defaultCharset;
						}
					}
				}
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000061C8 File Offset: 0x000043C8
		private void GetSpamConfidenceLevel()
		{
			try
			{
				Header header = this.MbxTransportMailItem.RootPart.Headers.FindFirst("X-MS-Exchange-Organization-SCL");
				int num;
				if (header != null && int.TryParse(header.Value, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num))
				{
					num = ((num < -1) ? -1 : ((num > 9) ? 9 : num));
					this.spamConfidenceLevel = num;
				}
			}
			catch (ExchangeDataException)
			{
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000623C File Offset: 0x0000443C
		private void PromoteDrmPropertiesToReplayItem()
		{
			if (this.replayItem == null)
			{
				throw new InvalidOperationException("replay item must be created first.");
			}
			string text;
			if (this.MbxTransportMailItem.ExtendedProperties.TryGetValue<string>("Microsoft.Exchange.RightsManagement.TransportDecryptionUL", out text) && !string.IsNullOrEmpty(text) && !DrmClientUtils.IsCachingOfLicenseDisabled(text))
			{
				MailItemDeliver.Diag.TraceDebug(0L, "Promote DRMServerLicenseCompressed property");
				using (Stream stream = this.replayItem.OpenPropertyStream(MessageItemSchema.DRMServerLicenseCompressed, PropertyOpenMode.Create))
				{
					DrmEmailCompression.CompressUseLicense(text, stream);
				}
			}
			if (this.replayItem.IsRestricted && this.replayItem.IconIndex == IconIndex.Default)
			{
				this.replayItem.IconIndex = IconIndex.MailIrm;
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000062F8 File Offset: 0x000044F8
		private void GetOriginalReceivedTime()
		{
			Header header = this.MbxTransportMailItem.RootPart.Headers.FindFirst("X-MS-Exchange-Organization-Original-Received-Time");
			DateTime value;
			if (header != null && Util.TryParseOrganizationalMessageArrivalTime(header.Value, out value))
			{
				this.originalReceivedTime = new DateTime?(value);
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00006340 File Offset: 0x00004540
		private void AckDeliveredRecipient()
		{
			MailItemDeliver.Diag.TracePfd<int, string>(0L, "PFD ESD {0} Ack Delivered Recipient {1}", 31899, this.recipient.Email.ToString());
			this.MbxTransportMailItem.AckRecipient(AckStatus.Success, SmtpResponse.Empty);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00006390 File Offset: 0x00004590
		private void AckFailedRecipient(SmtpResponse response)
		{
			MailItemDeliver.Diag.TraceDebug<string, string>(0L, "Ack Failed Recipient {0} SmtpResponse: {1}", this.recipient.Email.ToString(), response.EnhancedStatusCode);
			SmtpResponse smtpResponse = this.FormatOneLineResponse(response);
			this.MbxTransportMailItem.AckRecipient(AckStatus.Fail, smtpResponse);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000063E4 File Offset: 0x000045E4
		private void AckRetryRecipient(SmtpResponse response)
		{
			MailItemDeliver.Diag.TraceDebug<string>(0L, "Ack Retry Recipient {0}", this.recipient.Email.ToString());
			SmtpResponse smtpResponse = this.FormatOneLineResponse(response);
			this.MbxTransportMailItem.AckRecipient(AckStatus.Retry, smtpResponse);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00006430 File Offset: 0x00004630
		private void AckRerouteRecipient(SmtpResponse response)
		{
			MailItemDeliver.Diag.TraceDebug<string>(0L, "Ack Rerouted Recipient {0}", this.recipient.Email.ToString());
			this.MbxTransportMailItem.AckRecipient(AckStatus.Resubmit, response);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00006474 File Offset: 0x00004674
		private void AckDuplicateRecipient()
		{
			MailItemDeliver.Diag.TraceDebug<string>(0L, "Ack Duplicate Recipient {0}", this.recipient.Email.ToString());
			this.MbxTransportMailItem.AckRecipient(AckStatus.SuccessNoDsn, SmtpResponse.Empty);
			MailItemDeliver.AddFolderNameIfNeeded(ref this.duplicateRecipientsFolderNames, this.deliverToFolderName, this.duplicateRecipients.Count);
			this.duplicateRecipients.Add(this.recipient.Email.ToString());
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000064FC File Offset: 0x000046FC
		private SmtpResponse FormatOneLineResponse(SmtpResponse response)
		{
			string text = this.FormatStoreDriverContext();
			string text2 = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
			{
				SmtpResponseGenerator.FlattenStatusText(response),
				text
			});
			return new SmtpResponse(response.StatusCode, response.EnhancedStatusCode, new string[]
			{
				text2
			});
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00006554 File Offset: 0x00004754
		private void AddDeliveryErrors(ref List<string> errorCollection, List<string> newErrors)
		{
			if (newErrors == null || newErrors.Count == 0)
			{
				return;
			}
			if (errorCollection == null)
			{
				errorCollection = new List<string>(newErrors.Count);
			}
			errorCollection.AddRange(newErrors);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000657B File Offset: 0x0000477B
		private string GetMailboxServerName()
		{
			return StoreDriverDelivery.MailboxServerName ?? this.MbxTransportMailItem.DatabaseName;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006594 File Offset: 0x00004794
		private void WriteDeliveredAndProcessedTrackingLog()
		{
			this.MbxTransportMailItem.FinalizeDeliveryLatencyTracking(LatencyComponent.StoreDriverDelivery);
			if (this.ExtraTrackingEventData == null)
			{
				this.ExtraTrackingEventData = new List<KeyValuePair<string, string>>();
			}
			string databaseName = this.MbxTransportMailItem.DatabaseName;
			if (!string.IsNullOrEmpty(databaseName))
			{
				this.ExtraTrackingEventData.Add(new KeyValuePair<string, string>("MailboxDatabaseName", databaseName));
			}
			string value = MailItemDeliver.CombineDeliveryErrors(this.deliveredRecipientsErrors);
			if (!string.IsNullOrEmpty(value))
			{
				this.ExtraTrackingEventData.Add(new KeyValuePair<string, string>("DeliveryErrors", value));
			}
			if (this.databaseHealthMeasureToLog != null)
			{
				int value2 = this.databaseHealthMeasureToLog.Value;
				if (value2 != 100)
				{
					this.ExtraTrackingEventData.Add(new KeyValuePair<string, string>("DatabaseHealth", string.Format("{0:D}", value2)));
				}
			}
			if (ObjectClass.IsDsnNegative(this.messageClass))
			{
				this.ExtraTrackingEventData.Add(new KeyValuePair<string, string>("NDR", "1"));
			}
			this.ExtraTrackingEventData.Add(new KeyValuePair<string, string>("Mailboxes", string.Join(";", this.deliveredRecipientsMailboxInfo)));
			this.ExtraTrackingEventData.Add(new KeyValuePair<string, string>("ToEntity", RoutingEndpoint.Hosted.ToString()));
			Header header = this.MbxTransportMailItem.RootPart.Headers.FindFirst("X-MS-Exchange-Organization-FromEntityHeader");
			if (header != null && !string.IsNullOrEmpty(header.Value))
			{
				this.ExtraTrackingEventData.Add(new KeyValuePair<string, string>("FromEntity", header.Value));
			}
			string value3 = this.MbxTransportMailItem.ExtendedProperties.GetValue<string>("Microsoft.Exchange.Transport.DeliveryQueueMailboxSubComponent", LatencyComponent.None.ToString());
			LatencyComponent previousHopSubComponent;
			if (!Enum.TryParse<LatencyComponent>(value3, out previousHopSubComponent))
			{
				previousHopSubComponent = LatencyComponent.None;
			}
			long value4 = this.MbxTransportMailItem.ExtendedProperties.GetValue<long>("Microsoft.Exchange.Transport.DeliveryQueueMailboxSubComponentLatency", 0L);
			TimeSpan previousHopSubComponentLatency = TimeSpan.FromSeconds((double)value4);
			LatencyHeaderManager.HandleLatencyHeaders(this.MbxTransportMailItem.TransportSettings.InternalSMTPServers, this.MbxTransportMailItem.RootPart.Headers, this.MbxTransportMailItem.DateReceived, LatencyComponent.DeliveryQueueMailbox, previousHopSubComponent, previousHopSubComponentLatency);
			LatencyFormatter latencyFormatter = new LatencyFormatter(this.MbxTransportMailItem, Components.Configuration.LocalServer.TransportServer.Fqdn, true);
			string arg = "ClientSubmitTime:";
			if (this.MbxTransportMailItem.ClientSubmitTime != DateTime.MinValue)
			{
				arg = string.Format(CultureInfo.InvariantCulture, "ClientSubmitTime:{0:yyyy-MM-ddTHH\\:mm\\:ss.fffZ}", new object[]
				{
					this.MbxTransportMailItem.ClientSubmitTime
				});
			}
			string sourceContext = string.Format("{0};{1}", this.sessionSourceContext, arg);
			string value5 = this.MbxTransportMailItem.ExtendedProperties.GetValue<string>("Microsoft.Exchange.Transport.MailboxTransport.SmtpInClientHostname", string.Empty);
			if (this.deliveredRecipients.Count != 0)
			{
				MailItemDeliver.Diag.TraceDebug<int>(0L, "Writing message tracking delivered entry for {0} recipients", this.deliveredRecipients.Count);
				SystemProbe.TracePass<int>(this.MbxTransportMailItem, "StoreDriver", "Message delivered for {0} recipients", this.deliveredRecipients.Count);
				MessageTrackingLog.TrackDelivered(MessageTrackingSource.STOREDRIVER, this.MbxTransportMailItem, this.deliveredRecipients, this.deliveredRecipientsFolderNames, value5, this.GetMailboxServerName(), latencyFormatter, sourceContext, this.ExtraTrackingEventData);
				ExTraceGlobals.FaultInjectionTracer.TraceTest(54880U);
			}
			if (this.duplicateRecipients.Count != 0)
			{
				MailItemDeliver.Diag.TraceDebug<int>(0L, "Writing message tracking duplicate delivery entry for {0} recipients", this.duplicateRecipients.Count);
				SystemProbe.TracePass<int>(this.MbxTransportMailItem, "StoreDriver", "Message duplicate delivery for {0} recipients", this.duplicateRecipients.Count);
				MessageTrackingLog.TrackDuplicateDelivery(MessageTrackingSource.STOREDRIVER, this.MbxTransportMailItem, this.duplicateRecipients, this.duplicateRecipientsFolderNames, value5, this.GetMailboxServerName(), latencyFormatter, sourceContext, this.ExtraTrackingEventData);
			}
			if (this.processedRecipients != null)
			{
				MailItemDeliver.Diag.TraceDebug<int>(0L, "Writing message tracking processed entry for {0} recipients", this.processedRecipients.Count);
				SystemProbe.TracePass<int>(this.MbxTransportMailItem, "StoreDriver", "Message processed for {0} recipients", this.processedRecipients.Count);
				MessageTrackingLog.TrackProcessed(MessageTrackingSource.STOREDRIVER, this.MbxTransportMailItem, this.processedRecipients, this.processedRecipientsSources, latencyFormatter, this.sessionSourceContext, this.ExtraTrackingEventData);
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00006994 File Offset: 0x00004B94
		private void Initialize()
		{
			if (this.initialized)
			{
				this.eventArguments.ResetPerRecipientState();
				this.currentItem.DeliverToFolder = null;
				this.deliverToFolderName = null;
				this.deliveryErrors = null;
				return;
			}
			this.eventArguments = new StoreDriverDeliveryEventArgsImpl(this);
			this.messageClass = this.MbxTransportMailItem.Message.MapiMessageClass;
			this.mailItemWrapper = new MbxMailItemWrapper(this.MbxTransportMailItem);
			this.messageClass = this.MbxTransportMailItem.Message.MapiMessageClass;
			this.initialized = true;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00006A20 File Offset: 0x00004C20
		private void GetDeliveryFlags()
		{
			this.deliveryFlags = OpenTransportSessionFlags.OpenForNormalMessageDelivery;
			if (!MultilevelAuth.IsInternalMail(this.MbxTransportMailItem))
			{
				return;
			}
			if (ObjectClass.IsOfClass(this.messageClass, "IPM.Note.StorageQuotaWarning"))
			{
				this.deliveryFlags = OpenTransportSessionFlags.OpenForQuotaMessageDelivery;
				return;
			}
			if (ObjectClass.IsDsn(this.messageClass))
			{
				this.deliveryFlags = OpenTransportSessionFlags.OpenForSpecialMessageDelivery;
				return;
			}
			if (ObjectClass.IsOfClass(this.messageClass, "IPM.Note.Microsoft.Voicemail.UM.CA") || ObjectClass.IsOfClass(this.messageClass, "IPM.Note.rpmsg.Microsoft.Voicemail.UM.CA") || ObjectClass.IsOfClass(this.messageClass, "IPM.Note.rpmsg.Microsoft.Voicemail.UM") || ObjectClass.IsOfClass(this.messageClass, "IPM.Note.Microsoft.Partner.UM") || ObjectClass.IsOfClass(this.messageClass, "IPM.Note.Microsoft.Fax.CA"))
			{
				this.deliveryFlags = OpenTransportSessionFlags.OpenForSpecialMessageDelivery;
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00006AD4 File Offset: 0x00004CD4
		private void GetVirusScanningStamps()
		{
			Header[] array = this.MbxTransportMailItem.RootPart.Headers.FindAll("X-MS-Exchange-Organization-AVStamp-Mailbox");
			if (array != null && array.Length > 0)
			{
				this.virusScanningStamps = new string[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.virusScanningStamps[i] = array[i].Value;
				}
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00006B34 File Offset: 0x00004D34
		private void CreateReplayItem()
		{
			MailItemDeliver.Diag.TraceDebug(0L, "Create Content Conversion Replay Item");
			if (this.replayItem != null)
			{
				this.replayItem.Dispose();
				this.replayItem = null;
			}
			this.replayItem = MessageItem.CreateInMemory(StoreObjectSchema.ContentConversionProperties);
			this.replayItem.SetNoMessageDecoding(true);
			ItemConversion.ConvertAnyMimeToItem(this.replayItem, this.MbxTransportMailItem.Message, this.conversionOptions);
			this.replayItem.Save(SaveMode.NoConflictResolution);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00006BE4 File Offset: 0x00004DE4
		private void ProcessDuplicateDelivery()
		{
			StorageExceptionHandler.RunUnderExceptionHandler(this, delegate
			{
				if (this.replayItem != null && !this.replayItem.WasDeliveredViaBcc)
				{
					MailItemDeliver.Diag.TraceDebug((long)this.GetHashCode(), "Duplicate delivery happened, and the new item was not BCCed.");
					this.RemoveBccFlagsFromDuplicates();
				}
			});
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006BFC File Offset: 0x00004DFC
		private void RemoveBccFlagsFromDuplicates()
		{
			MailboxSession mailboxSession = this.currentItem.Session as MailboxSession;
			if (mailboxSession == null)
			{
				MailItemDeliver.Diag.TraceDebug<string>((long)this.GetHashCode(), "BCC duplicate detection not supported for: {0}", this.currentItem.Session.GetType().FullName);
				return;
			}
			if (string.IsNullOrEmpty(this.replayItem.InternetMessageId))
			{
				return;
			}
			IEnumerable<IStorePropertyBag> enumerable = AllItemsFolderHelper.FindItemsFromInternetId(mailboxSession, this.replayItem.InternetMessageId, ItemQueryType.NoNotifications, new PropertyDefinition[]
			{
				ItemSchema.Id
			});
			int num = 0;
			foreach (IStorePropertyBag storePropertyBag in enumerable)
			{
				num++;
				if (num >= MailItemDeliver.MaxDuplicatesChecked)
				{
					break;
				}
				StoreObjectId objectId = ((VersionedId)storePropertyBag[ItemSchema.Id]).ObjectId;
				using (MessageItem messageItem = MessageItem.Bind(this.currentItem.Session, objectId, new PropertyDefinition[]
				{
					MessageItemSchema.MessageBccMe
				}))
				{
					if (messageItem == null)
					{
						MailItemDeliver.Diag.TraceDebug((long)this.GetHashCode(), "Duplicate item was not found, presumably the user deleted it.");
					}
					else
					{
						bool valueOrDefault = messageItem.GetValueOrDefault<bool>(MessageItemSchema.MessageBccMe);
						if (valueOrDefault)
						{
							MailItemDeliver.Diag.TraceDebug((long)this.GetHashCode(), "Existing duplicate item was BCC, new non-BCC state must take precedence.");
							messageItem.OpenAsReadWrite();
							messageItem[MessageItemSchema.MessageBccMe] = false;
							ConflictResolutionResult conflictResolutionResult = messageItem.Save(SaveMode.ResolveConflicts);
							MailItemDeliver.Diag.TraceDebug<SaveResult>((long)this.GetHashCode(), "Existing duplicate item saved: {0}", conflictResolutionResult.SaveStatus);
						}
						else
						{
							MailItemDeliver.Diag.TraceDebug((long)this.GetHashCode(), "Existing duplicate item was not BCC");
						}
					}
				}
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006DE8 File Offset: 0x00004FE8
		private void AddStoreLatency(TimeSpan additionalLatency)
		{
			this.storeLatency += additionalLatency;
			this.breadcrumb.StoreLatency = this.storeLatency;
			MailItemDeliver.Diag.TraceDebug<TimeSpan, string, TimeSpan>(0L, "Store time in server {0} added to store latency for message {1}, making total store latency {2}", additionalLatency, this.MbxTransportMailItem.InternetMessageId ?? "NoMessageId", this.storeLatency);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006E44 File Offset: 0x00005044
		private void DecrementThrottlingForCurrentRecipient()
		{
			ulong smtpSessionId = 0UL;
			if (this.MbxTransportMailItem.ExtendedProperties.TryGetValue<ulong>("Microsoft.Exchange.Transport.SmtpInSessionId", out smtpSessionId))
			{
				DeliveryThrottling.Instance.DecrementRecipient((long)smtpSessionId, this.recipient.Email);
				return;
			}
			MailItemDeliver.Diag.TraceWarning<string>(0L, "SmtpInSessionId is not found in mailitem properties for message {0}. Throttling value will be decremented only on SMTP disconnect", this.MbxTransportMailItem.InternetMessageId ?? "NoMessageId");
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006EAC File Offset: 0x000050AC
		private void IncrementDeliveryFailure(MessageAction messageAction)
		{
			StoreDriverDeliveryPerfCounters.Instance.IncrementDeliveryFailure(this.IsPermanentFailure(messageAction), false);
			StoreDriverDatabasePerfCounters.IncrementDeliveryFailure(this.MbxTransportMailItem.DatabaseGuid.ToString(), false);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006EEC File Offset: 0x000050EC
		private bool IsPermanentFailure(MessageAction messageAction)
		{
			switch (messageAction)
			{
			case MessageAction.NDR:
				return true;
			}
			return false;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006F2E File Offset: 0x0000512E
		private void ProcessDeliveryForRecipient()
		{
			this.AckDeliveredRecipient();
			MSExchangeStoreDriver.SuccessfulDeliveries.Increment();
			MSExchangeStoreDriver.BytesDelivered.IncrementBy(this.MbxTransportMailItem.MimeSize);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006F57 File Offset: 0x00005157
		private void ProcessDuplicateDeliveryForRecipient()
		{
			this.AckDuplicateRecipient();
			MSExchangeStoreDriver.DuplicateDelivery.Increment();
			this.IncrementDeliveryFailure(MessageAction.Success);
			this.ProcessDuplicateDelivery();
		}

		// Token: 0x04000032 RID: 50
		private const int DeliverRecords = 128;

		// Token: 0x04000033 RID: 51
		private static readonly int MaxDuplicatesChecked = 5;

		// Token: 0x04000034 RID: 52
		private static readonly Trace Diag = ExTraceGlobals.MapiDeliverTracer;

		// Token: 0x04000035 RID: 53
		private static DeliveryRecipientThreadMap recipientThreadMap = new DeliveryRecipientThreadMap(MailItemDeliver.Diag);

		// Token: 0x04000036 RID: 54
		private static Breadcrumbs<MailItemDeliver.Breadcrumb> deliveryHistory = new Breadcrumbs<MailItemDeliver.Breadcrumb>(128);

		// Token: 0x04000037 RID: 55
		private static StoreDriverEventSource eventSource = new StoreDriverEventSourceImpl();

		// Token: 0x04000038 RID: 56
		private readonly ulong sessionId;

		// Token: 0x04000039 RID: 57
		private readonly string sessionSourceContext;

		// Token: 0x0400003A RID: 58
		private InboundConversionOptions conversionOptions;

		// Token: 0x0400003B RID: 59
		private object spamConfidenceLevel;

		// Token: 0x0400003C RID: 60
		private DateTime? originalReceivedTime;

		// Token: 0x0400003D RID: 61
		private MultiValuedProperty<CultureInfo> recipientLanguages;

		// Token: 0x0400003E RID: 62
		private string messageClass;

		// Token: 0x0400003F RID: 63
		private OpenTransportSessionFlags deliveryFlags;

		// Token: 0x04000040 RID: 64
		private IDeliveryItem currentItem;

		// Token: 0x04000041 RID: 65
		private List<string> deliveredRecipients;

		// Token: 0x04000042 RID: 66
		private List<string> deliveredRecipientsFolderNames;

		// Token: 0x04000043 RID: 67
		private List<string> deliveredRecipientsErrors;

		// Token: 0x04000044 RID: 68
		private List<string> deliveredRecipientsMailboxInfo;

		// Token: 0x04000045 RID: 69
		private List<string> duplicateRecipients;

		// Token: 0x04000046 RID: 70
		private List<string> duplicateRecipientsFolderNames;

		// Token: 0x04000047 RID: 71
		private List<string> processedRecipients;

		// Token: 0x04000048 RID: 72
		private List<string> processedRecipientsSources;

		// Token: 0x04000049 RID: 73
		private MessageItem replayItem;

		// Token: 0x0400004A RID: 74
		private MailRecipient recipient;

		// Token: 0x0400004B RID: 75
		private bool isJournalReport;

		// Token: 0x0400004C RID: 76
		private string[] virusScanningStamps;

		// Token: 0x0400004D RID: 77
		private bool initialized;

		// Token: 0x0400004E RID: 78
		private bool replayCreated;

		// Token: 0x0400004F RID: 79
		private MailItemDeliver.Breadcrumb breadcrumb;

		// Token: 0x04000050 RID: 80
		private IMExSession mexSession;

		// Token: 0x04000051 RID: 81
		private AgentLatencyTracker agentLatencyTracker;

		// Token: 0x04000052 RID: 82
		private StoreDriverDeliveryEventArgsImpl eventArguments;

		// Token: 0x04000053 RID: 83
		private string deliverToFolderName;

		// Token: 0x04000054 RID: 84
		private List<string> deliveryErrors;

		// Token: 0x04000055 RID: 85
		private TimeSpan? retryInterval;

		// Token: 0x04000056 RID: 86
		private bool wasSessionOpenedForLastRecipient;

		// Token: 0x04000057 RID: 87
		private int? databaseHealthMeasureToLog;

		// Token: 0x04000058 RID: 88
		private MailItemDeliver.DeliveryStage deliveryStage;

		// Token: 0x04000059 RID: 89
		private TimeSpan storeLatency;

		// Token: 0x0400005A RID: 90
		private TimeSpan rpcLatency;

		// Token: 0x0400005B RID: 91
		private MbxTransportMailItem mbxTransportMailItem;

		// Token: 0x0400005C RID: 92
		private MbxMailItemWrapper mailItemWrapper;

		// Token: 0x02000009 RID: 9
		internal enum DeliveryStage
		{
			// Token: 0x04000063 RID: 99
			NotStarted,
			// Token: 0x04000064 RID: 100
			Start,
			// Token: 0x04000065 RID: 101
			OnInitializedEvent,
			// Token: 0x04000066 RID: 102
			CreateReplay,
			// Token: 0x04000067 RID: 103
			CreateSession,
			// Token: 0x04000068 RID: 104
			OnPromotedEvent,
			// Token: 0x04000069 RID: 105
			CreateMessage,
			// Token: 0x0400006A RID: 106
			PromoteProperties,
			// Token: 0x0400006B RID: 107
			OnCreatedEvent,
			// Token: 0x0400006C RID: 108
			PostCreate,
			// Token: 0x0400006D RID: 109
			Delivery,
			// Token: 0x0400006E RID: 110
			OnDeliveredEvent,
			// Token: 0x0400006F RID: 111
			Done
		}

		// Token: 0x0200000A RID: 10
		internal class Breadcrumb
		{
			// Token: 0x06000113 RID: 275 RVA: 0x00006FB1 File Offset: 0x000051B1
			internal Breadcrumb()
			{
				this.RecordCreation = ExDateTime.UtcNow;
				this.DeliveredToAll = TimeSpan.MinValue;
				this.MailboxServer = "?";
			}

			// Token: 0x1700006F RID: 111
			// (get) Token: 0x06000114 RID: 276 RVA: 0x00006FDA File Offset: 0x000051DA
			// (set) Token: 0x06000115 RID: 277 RVA: 0x00006FE2 File Offset: 0x000051E2
			internal ExDateTime RecordCreation { get; set; }

			// Token: 0x17000070 RID: 112
			// (get) Token: 0x06000116 RID: 278 RVA: 0x00006FEB File Offset: 0x000051EB
			// (set) Token: 0x06000117 RID: 279 RVA: 0x00006FF3 File Offset: 0x000051F3
			internal string Sender { get; set; }

			// Token: 0x17000071 RID: 113
			// (get) Token: 0x06000118 RID: 280 RVA: 0x00006FFC File Offset: 0x000051FC
			// (set) Token: 0x06000119 RID: 281 RVA: 0x00007004 File Offset: 0x00005204
			internal string MessageId { get; set; }

			// Token: 0x17000072 RID: 114
			// (get) Token: 0x0600011A RID: 282 RVA: 0x0000700D File Offset: 0x0000520D
			// (set) Token: 0x0600011B RID: 283 RVA: 0x00007015 File Offset: 0x00005215
			internal int RecipientCount { get; set; }

			// Token: 0x17000073 RID: 115
			// (get) Token: 0x0600011C RID: 284 RVA: 0x0000701E File Offset: 0x0000521E
			// (set) Token: 0x0600011D RID: 285 RVA: 0x00007026 File Offset: 0x00005226
			internal TimeSpan DeliveredToAll { get; set; }

			// Token: 0x17000074 RID: 116
			// (get) Token: 0x0600011E RID: 286 RVA: 0x0000702F File Offset: 0x0000522F
			// (set) Token: 0x0600011F RID: 287 RVA: 0x00007037 File Offset: 0x00005237
			internal string MailboxDatabase { get; set; }

			// Token: 0x17000075 RID: 117
			// (get) Token: 0x06000120 RID: 288 RVA: 0x00007040 File Offset: 0x00005240
			// (set) Token: 0x06000121 RID: 289 RVA: 0x00007048 File Offset: 0x00005248
			internal string MailboxServer { get; set; }

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x06000122 RID: 290 RVA: 0x00007051 File Offset: 0x00005251
			// (set) Token: 0x06000123 RID: 291 RVA: 0x00007059 File Offset: 0x00005259
			internal LatencyTracker LatencyTracker { get; set; }

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x06000124 RID: 292 RVA: 0x00007062 File Offset: 0x00005262
			// (set) Token: 0x06000125 RID: 293 RVA: 0x0000706A File Offset: 0x0000526A
			internal TimeSpan RpcLatency { get; set; }

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x06000126 RID: 294 RVA: 0x00007073 File Offset: 0x00005273
			// (set) Token: 0x06000127 RID: 295 RVA: 0x0000707B File Offset: 0x0000527B
			internal TimeSpan StoreLatency { get; set; }

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x06000128 RID: 296 RVA: 0x00007084 File Offset: 0x00005284
			// (set) Token: 0x06000129 RID: 297 RVA: 0x0000708C File Offset: 0x0000528C
			internal TimeSpan LdapLatency { get; set; }

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x0600012A RID: 298 RVA: 0x00007095 File Offset: 0x00005295
			// (set) Token: 0x0600012B RID: 299 RVA: 0x0000709D File Offset: 0x0000529D
			internal TimeSpan ContentConversionLatency { get; set; }

			// Token: 0x0600012C RID: 300 RVA: 0x000070A8 File Offset: 0x000052A8
			public override string ToString()
			{
				return string.Format("Created: {0}, MbxDatabase: {1}, MbxServer: {2}, Sender: {3}, MessageId: {4}, RecipientCount: {5}, DeliveryStage: {6}, Completed: {7}, Elapsed: {8}, RpcLatency: {9}, StoreLatency: {10}, LdapLatency: {11}, ContentConversionLatency: {12}", new object[]
				{
					this.RecordCreation,
					this.MailboxDatabase,
					this.MailboxServer,
					this.Sender,
					this.MessageId,
					this.RecipientCount,
					this.deliveryStage,
					(this.DeliveredToAll == TimeSpan.MinValue) ? "No" : "Yes",
					(this.DeliveredToAll == TimeSpan.MinValue) ? ExDateTime.UtcNow.Subtract(this.RecordCreation) : this.DeliveredToAll,
					this.RpcLatency,
					this.StoreLatency,
					this.LdapLatency,
					this.ContentConversionLatency
				});
			}

			// Token: 0x0600012D RID: 301 RVA: 0x000071A8 File Offset: 0x000053A8
			public XElement GetDiagnosticInfo()
			{
				XElement xelement = new XElement("Delivery", new object[]
				{
					new XElement("creationTimestamp", this.RecordCreation),
					new XElement("mailboxDatabase", this.MailboxDatabase),
					new XElement("mailboxServer", this.MailboxServer),
					new XElement("sender", this.Sender),
					new XElement("recipientCount", this.RecipientCount),
					new XElement("deliveryStage", this.deliveryStage),
					new XElement("completed", this.DeliveredToAll != TimeSpan.MinValue),
					new XElement("elapsed", (this.DeliveredToAll == TimeSpan.MinValue) ? ExDateTime.UtcNow.Subtract(this.RecordCreation) : this.DeliveredToAll),
					new XElement("rpcLatency", this.RpcLatency),
					new XElement("storeLatency", this.StoreLatency),
					new XElement("ldapLatency", this.LdapLatency),
					new XElement("contentConversionLatency", this.ContentConversionLatency)
				});
				xelement.Add(LatencyFormatter.GetDiagnosticInfo(this.LatencyTracker));
				xelement.SetAttributeValue("messageId", this.MessageId);
				return xelement;
			}

			// Token: 0x0600012E RID: 302 RVA: 0x00007377 File Offset: 0x00005577
			internal MailItemDeliver.DeliveryStage SetStage(MailItemDeliver.DeliveryStage deliveryStage)
			{
				this.deliveryStage = deliveryStage;
				return deliveryStage;
			}

			// Token: 0x04000070 RID: 112
			private MailItemDeliver.DeliveryStage deliveryStage;
		}
	}
}
