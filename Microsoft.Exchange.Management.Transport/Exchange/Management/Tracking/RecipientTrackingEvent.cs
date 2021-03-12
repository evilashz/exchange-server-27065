using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Tracking;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.InfoWorker.Common.MessageTracking;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging.Search;

namespace Microsoft.Exchange.Management.Tracking
{
	// Token: 0x020000A6 RID: 166
	[Serializable]
	public class RecipientTrackingEvent : MessageTrackingConfigurableObject
	{
		// Token: 0x060005C8 RID: 1480 RVA: 0x0001705C File Offset: 0x0001525C
		private static RecipientTrackingEvent GetIntermediateTransferredEvent(RecipientTrackingEvent originalInternalEvent)
		{
			if (originalInternalEvent.EventDescription != Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.TransferredToPartnerOrg && originalInternalEvent.EventDescription != Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.SmtpSendCrossForest)
			{
				throw new ArgumentException("GetIntermediateTransferredEvent can only be called for TransferredToPartnerOrg or SmtpSendCrossForest");
			}
			return new RecipientTrackingEvent(CoreStrings.EventTransferredIntermediate.ToString(Thread.CurrentThread.CurrentCulture), originalInternalEvent.EventDescription, originalInternalEvent.InternalMessageId, originalInternalEvent.BccRecipient, originalInternalEvent.Date, _DeliveryStatus.Transferred, null, EventType.Transferred, originalInternalEvent.RecipientAddress, originalInternalEvent.RecipientDisplayName, originalInternalEvent.Server);
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x000170D2 File Offset: 0x000152D2
		public DateTime Date
		{
			get
			{
				return (DateTime)this[RecipientTrackingEventSchema.Date];
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x000170E4 File Offset: 0x000152E4
		// (set) Token: 0x060005CB RID: 1483 RVA: 0x000170F6 File Offset: 0x000152F6
		public SmtpAddress RecipientAddress
		{
			get
			{
				return (SmtpAddress)this[RecipientTrackingEventSchema.RecipientAddress];
			}
			internal set
			{
				this[RecipientTrackingEventSchema.RecipientAddress] = value;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00017109 File Offset: 0x00015309
		// (set) Token: 0x060005CD RID: 1485 RVA: 0x0001711B File Offset: 0x0001531B
		public string RecipientDisplayName
		{
			get
			{
				return (string)this[RecipientTrackingEventSchema.RecipientDisplayName];
			}
			internal set
			{
				this[RecipientTrackingEventSchema.RecipientDisplayName] = value;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x00017129 File Offset: 0x00015329
		public _DeliveryStatus Status
		{
			get
			{
				return (_DeliveryStatus)this[RecipientTrackingEventSchema.DeliveryStatus];
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0001713B File Offset: 0x0001533B
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x0001714D File Offset: 0x0001534D
		public EventType EventType
		{
			get
			{
				return (EventType)this[RecipientTrackingEventSchema.EventTypeValue];
			}
			private set
			{
				this[RecipientTrackingEventSchema.EventTypeValue] = value;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x00017160 File Offset: 0x00015360
		public string EventDescription
		{
			get
			{
				return (string)this[RecipientTrackingEventSchema.EventDescription];
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x00017172 File Offset: 0x00015372
		// (set) Token: 0x060005D3 RID: 1491 RVA: 0x00017184 File Offset: 0x00015384
		public string[] EventData
		{
			get
			{
				return (string[])this[RecipientTrackingEventSchema.EventData];
			}
			internal set
			{
				this[RecipientTrackingEventSchema.EventData] = value;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00017192 File Offset: 0x00015392
		// (set) Token: 0x060005D5 RID: 1493 RVA: 0x000171A4 File Offset: 0x000153A4
		public string Server
		{
			get
			{
				return (string)this[RecipientTrackingEventSchema.Server];
			}
			internal set
			{
				this[RecipientTrackingEventSchema.Server] = value;
			}
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000171B4 File Offset: 0x000153B4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.AppendFormat("{0},{1},{2},{3}", new object[]
			{
				this.RecipientAddress,
				Names<Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription>.Map[(int)this.eventDescriptionEnum],
				this.Server,
				this.Date.ToString("o", CultureInfo.InvariantCulture)
			});
			if (this.EventData != null && this.EventData.Length > 0)
			{
				stringBuilder.Append(",");
				stringBuilder.Append("Data=");
				for (int i = 0; i < this.EventData.Length; i++)
				{
					stringBuilder.Append(this.EventData[i]);
					if (i != this.EventData.Length - 1)
					{
						stringBuilder.Append(';');
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00017287 File Offset: 0x00015487
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return RecipientTrackingEvent.schema;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0001728E File Offset: 0x0001548E
		internal long InternalMessageId
		{
			get
			{
				return this.internalMessageId;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00017296 File Offset: 0x00015496
		internal bool BccRecipient
		{
			get
			{
				return this.bccRecipient;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x0001729E File Offset: 0x0001549E
		internal EventDescription EventDescriptionEnum
		{
			get
			{
				return this.eventDescriptionEnum;
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000172A8 File Offset: 0x000154A8
		internal static RecipientTrackingEvent Create(bool isLastKnownStatus, IConfigurationSession session, MultiValuedProperty<CultureInfo> userLanguages, bool returnHelpDeskMessages, RecipientTrackingEvent internalRecipientTrackingEvent)
		{
			if (isLastKnownStatus && (internalRecipientTrackingEvent.EventDescription == Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.SmtpSendCrossForest || internalRecipientTrackingEvent.EventDescription == Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.TransferredToPartnerOrg))
			{
				return RecipientTrackingEvent.GetIntermediateTransferredEvent(internalRecipientTrackingEvent);
			}
			RecipientTrackingEvent.FormatterMethod[] array = returnHelpDeskMessages ? RecipientTrackingEvent.helpDeskFormatters : RecipientTrackingEvent.iWFormatters;
			RecipientTrackingEvent.FormatterSource source = new RecipientTrackingEvent.FormatterSource(session, userLanguages, internalRecipientTrackingEvent);
			RecipientTrackingEvent.FormatterMethod formatterMethod = array[(int)internalRecipientTrackingEvent.EventDescription];
			if (formatterMethod != null)
			{
				LocalizedString localizedString;
				try
				{
					localizedString = formatterMethod(source, internalRecipientTrackingEvent.EventData);
				}
				catch (FormatException ex)
				{
					ExTraceGlobals.SearchLibraryTracer.TraceError(0L, ex.Message);
					return null;
				}
				string eventDescription = localizedString.ToString(Thread.CurrentThread.CurrentCulture);
				RecipientTrackingEvent recipientTrackingEvent = new RecipientTrackingEvent(internalRecipientTrackingEvent, eventDescription);
				EventType eventType;
				if (RecipientTrackingEvent.descriptionToTypeMap.TryGetValue(internalRecipientTrackingEvent.EventDescription, out eventType))
				{
					recipientTrackingEvent.EventType = eventType;
				}
				else
				{
					recipientTrackingEvent.EventType = EventType.Pending;
				}
				return recipientTrackingEvent;
			}
			return null;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00017388 File Offset: 0x00015588
		internal static RecipientTrackingEvent GetExplanatoryMessage(IList<RecipientTrackingEvent> events)
		{
			if (events.Count == 0)
			{
				return null;
			}
			DateTime utcNow = DateTime.UtcNow;
			RecipientTrackingEvent recipientTrackingEvent = events[0];
			RecipientTrackingEvent recipientTrackingEvent2 = events[events.Count - 1];
			if (recipientTrackingEvent2.EventDescription == Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.SmtpSendCrossSite || recipientTrackingEvent2.EventDescription == Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.SmtpSend)
			{
				if (utcNow > recipientTrackingEvent2.Date && utcNow - recipientTrackingEvent2.Date > RecipientTrackingEvent.SmtpHandshakeMaximumSkew)
				{
					return new RecipientTrackingEvent(CoreStrings.TrackingExplanationLogsDeleted.ToString(Thread.CurrentThread.CurrentCulture), Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.Pending, recipientTrackingEvent2.InternalMessageId, recipientTrackingEvent2.BccRecipient, utcNow, _DeliveryStatus.Pending, null, EventType.Pending, recipientTrackingEvent2.RecipientAddress, recipientTrackingEvent2.RecipientDisplayName, recipientTrackingEvent2.Server);
				}
			}
			else if (recipientTrackingEvent2.Status == DeliveryStatus.Pending)
			{
				if (recipientTrackingEvent2.EventDescription == Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.PendingModeration || recipientTrackingEvent2.EventDescription == Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.SmtpSendCrossForest || recipientTrackingEvent2.EventDescription == Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.TransferredToPartnerOrg)
				{
					return null;
				}
				TimeSpan t = utcNow.Subtract(recipientTrackingEvent.Date);
				if (t < RecipientTrackingEvent.MaximumDelayForNormalMessage)
				{
					if (recipientTrackingEvent2.EventDescription == Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.QueueRetry || recipientTrackingEvent2.EventDescription == Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.QueueRetryNoRetryTime)
					{
						return null;
					}
					return new RecipientTrackingEvent(CoreStrings.TrackingExplanationNormalTimeSpan.ToString(Thread.CurrentThread.CurrentCulture), Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.Pending, recipientTrackingEvent2.InternalMessageId, recipientTrackingEvent2.BccRecipient, utcNow, _DeliveryStatus.Pending, null, EventType.Pending, recipientTrackingEvent2.RecipientAddress, recipientTrackingEvent2.RecipientDisplayName, recipientTrackingEvent2.Server);
				}
				else if (t >= RecipientTrackingEvent.ExcessiveDelayTimeSpan)
				{
					return new RecipientTrackingEvent(CoreStrings.TrackingExplanationExcessiveTimeSpan.ToString(Thread.CurrentThread.CurrentCulture), Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.Pending, recipientTrackingEvent2.InternalMessageId, recipientTrackingEvent2.BccRecipient, utcNow, _DeliveryStatus.Pending, null, EventType.Pending, recipientTrackingEvent2.RecipientAddress, recipientTrackingEvent2.RecipientDisplayName, recipientTrackingEvent2.Server);
				}
			}
			return null;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00017554 File Offset: 0x00015754
		internal static void FillDisplayNames(BulkRecipientLookupCache recipientCache, RecipientTrackingEvent[] recipientEvents, IRecipientSession session)
		{
			if (recipientEvents == null || recipientEvents.Length == 0)
			{
				return;
			}
			IEnumerable<string> addresses = from recipientEvent in recipientEvents
			select recipientEvent.RecipientAddress.ToString();
			IEnumerable<string> enumerable = recipientCache.Resolve(addresses, session);
			int num = 0;
			foreach (string recipientDisplayName in enumerable)
			{
				recipientEvents[num].RecipientDisplayName = recipientDisplayName;
				num++;
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x000175E0 File Offset: 0x000157E0
		private RecipientTrackingEvent(RecipientTrackingEvent internalRecipientTrackingEvent, string eventDescription)
		{
			this.eventDescriptionEnum = internalRecipientTrackingEvent.EventDescription;
			this.internalMessageId = internalRecipientTrackingEvent.InternalMessageId;
			this.bccRecipient = internalRecipientTrackingEvent.BccRecipient;
			this[RecipientTrackingEventSchema.EventDescription] = eventDescription;
			this[RecipientTrackingEventSchema.Date] = internalRecipientTrackingEvent.Date;
			this[RecipientTrackingEventSchema.DeliveryStatus] = (_DeliveryStatus)internalRecipientTrackingEvent.Status;
			this[RecipientTrackingEventSchema.EventData] = internalRecipientTrackingEvent.EventData;
			this[RecipientTrackingEventSchema.EventTypeValue] = internalRecipientTrackingEvent.EventType;
			this[RecipientTrackingEventSchema.RecipientAddress] = internalRecipientTrackingEvent.RecipientAddress;
			this[RecipientTrackingEventSchema.RecipientDisplayName] = internalRecipientTrackingEvent.RecipientDisplayName;
			this[RecipientTrackingEventSchema.Server] = internalRecipientTrackingEvent.Server;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000176B0 File Offset: 0x000158B0
		private RecipientTrackingEvent(string eventDescription, EventDescription eventDescriptionEnum, long internalMessageId, bool bccRecipient, DateTime date, _DeliveryStatus deliveryStatus, string[] eventData, EventType eventType, SmtpAddress recipientAddress, string recipientDisplayName, string server)
		{
			this.eventDescriptionEnum = eventDescriptionEnum;
			this.internalMessageId = internalMessageId;
			this.bccRecipient = bccRecipient;
			this[RecipientTrackingEventSchema.EventDescription] = eventDescription;
			this[RecipientTrackingEventSchema.Date] = date;
			this[RecipientTrackingEventSchema.DeliveryStatus] = deliveryStatus;
			this[RecipientTrackingEventSchema.EventData] = eventData;
			this[RecipientTrackingEventSchema.EventTypeValue] = eventType;
			this[RecipientTrackingEventSchema.RecipientAddress] = recipientAddress;
			this[RecipientTrackingEventSchema.RecipientDisplayName] = recipientDisplayName;
			this[RecipientTrackingEventSchema.Server] = server;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001778C File Offset: 0x0001598C
		private static RecipientTrackingEvent.FormatterMethod[] CreateIWFormatTable()
		{
			RecipientTrackingEvent.FormatterMethod[] array = new RecipientTrackingEvent.FormatterMethod[RecipientTrackingEvent.EventDescriptionsLength];
			array[0] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventSubmitted);
			array[1] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventSubmittedCrossSite);
			array[2] = null;
			array[3] = delegate(RecipientTrackingEvent.FormatterSource source, string[] args)
			{
				if (args.Length < 2)
				{
					throw new FormatException("Expanded must have group name and group email address arguments");
				}
				return CoreStrings.EventExpanded(args[1]);
			};
			array[4] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventDelivered);
			array[5] = delegate(RecipientTrackingEvent.FormatterSource source, string[] args)
			{
				if (args.Length < 1)
				{
					throw new FormatException("MovedToFolderByInboxRule must have folder name argument");
				}
				return CoreStrings.EventMovedToFolderByInboxRuleIW(args[0]);
			};
			array[6] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventRulesCc);
			array[7] = RecipientTrackingEvent.FailedGeneralDelegate();
			array[8] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventFailedModerationIW);
			array[9] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventFailedTransportRulesIW);
			array[10] = null;
			array[11] = null;
			array[12] = null;
			array[13] = null;
			array[14] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventForwarded);
			array[15] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventPending);
			array[16] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventPendingModerationIW);
			array[17] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventApprovedModerationIW);
			array[18] = null;
			array[20] = null;
			array[21] = RecipientTrackingEvent.ForeignOrgDelegate();
			array[22] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventTransferredToLegacyExchangeServer);
			array[24] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventDelayedAfterTransferToPartnerOrgIW);
			array[25] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventRead);
			array[26] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventNotRead);
			array[27] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventForwardedToDelegateAndDeleted);
			array[28] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventModerationExpired);
			return array;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00017A6C File Offset: 0x00015C6C
		private static RecipientTrackingEvent.FormatterMethod[] CreateHelpDeskFormatTable()
		{
			RecipientTrackingEvent.FormatterMethod[] array = new RecipientTrackingEvent.FormatterMethod[RecipientTrackingEvent.EventDescriptionsLength];
			array[0] = delegate(RecipientTrackingEvent.FormatterSource source, string[] args)
			{
				if (args == null || args.Length < 1)
				{
					return CoreStrings.EventSubmitted;
				}
				return CoreStrings.EventSubmittedHelpDesk(args[0]);
			};
			array[1] = delegate(RecipientTrackingEvent.FormatterSource source, string[] args)
			{
				if (args == null || args.Length < 1)
				{
					return CoreStrings.EventSubmittedCrossSite;
				}
				return CoreStrings.EventSubmittedCrossSiteHelpDesk(args[0]);
			};
			array[2] = delegate(RecipientTrackingEvent.FormatterSource source, string[] args)
			{
				if (args == null || args.Length < 2)
				{
					return CoreStrings.EventResolvedHelpDesk;
				}
				return CoreStrings.EventResolvedWithDetailsHelpDesk(args[0], args[1]);
			};
			array[3] = delegate(RecipientTrackingEvent.FormatterSource source, string[] args)
			{
				if (args.Length < 2)
				{
					throw new FormatException("Expanded must have group name and group email address arguments");
				}
				return CoreStrings.EventExpanded(args[1]);
			};
			array[4] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventDelivered);
			array[5] = delegate(RecipientTrackingEvent.FormatterSource source, string[] args)
			{
				if (args.Length < 1)
				{
					throw new FormatException("MovedToFolderByInboxRule must have folder name argument");
				}
				return CoreStrings.EventMovedToFolderByInboxRuleHelpDesk(args[0]);
			};
			array[6] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventRulesCc);
			array[7] = RecipientTrackingEvent.FailedGeneralDelegate();
			array[8] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventFailedModerationHelpDesk);
			array[9] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventFailedTransportRulesHelpDesk);
			array[10] = delegate(RecipientTrackingEvent.FormatterSource source, string[] args)
			{
				if (args.Length < 2)
				{
					throw new FormatException("SmtpSend must have sending and receiving server names");
				}
				return CoreStrings.EventSmtpSendHelpDesk(args[0], args[1]);
			};
			array[11] = delegate(RecipientTrackingEvent.FormatterSource source, string[] args)
			{
				if (args.Length < 2)
				{
					throw new FormatException("SmtpSendCrossSite must have sending and receiving server names");
				}
				return CoreStrings.EventSmtpSendHelpDesk(args[0], args[1]);
			};
			array[12] = ((RecipientTrackingEvent.FormatterSource source, string[] args) => CoreStrings.EventSmtpSendHelpDesk(args[0], args[1]));
			array[13] = delegate(RecipientTrackingEvent.FormatterSource source, string[] args)
			{
				if (args.Length < 2)
				{
					throw new FormatException("SmtpReceive must have sending and receiving server names");
				}
				return CoreStrings.EventSmtpReceiveHelpDesk(args[0], args[1]);
			};
			array[14] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventForwarded);
			array[15] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventPending);
			array[16] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventPendingModerationHelpDesk);
			array[17] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventApprovedModerationHelpDesk);
			array[18] = delegate(RecipientTrackingEvent.FormatterSource source, string[] args)
			{
				if (args.Length < 5)
				{
					throw new FormatException("QueueRetry must have server, inRetrySinceTime, lastAttemptTime, serverTimeZone, errorMessage arguments");
				}
				if (!string.IsNullOrEmpty(args[4]))
				{
					return CoreStrings.EventQueueRetryHelpDesk(args[0], args[1], args[2], args[3], args[4]);
				}
				return CoreStrings.EventQueueRetryNoErrorHelpDesk(args[0], args[1], args[2], args[3]);
			};
			array[19] = delegate(RecipientTrackingEvent.FormatterSource source, string[] args)
			{
				if (args.Length < 2)
				{
					throw new FormatException("QueueRetryNoRetryTime must have queue name and error message");
				}
				return CoreStrings.EventQueueRetryNoRetryTimeHelpDesk(args[0], args[1]);
			};
			array[20] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventMessageDefer);
			array[21] = RecipientTrackingEvent.ForeignOrgDelegate();
			array[22] = delegate(RecipientTrackingEvent.FormatterSource source, string[] args)
			{
				if (args.Length < 2)
				{
					throw new FormatException("TransferredToLegacyExchangeServer must have both local exchange server name and remote legacy exchange server name");
				}
				return CoreStrings.EventTransferredToLegacyExchangeServerHelpDesk(args[0], args[1]);
			};
			array[25] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventRead);
			array[26] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventNotRead);
			array[27] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventForwardedToDelegateAndDeleted);
			array[28] = RecipientTrackingEvent.SimpleDelegate(CoreStrings.EventModerationExpired);
			return array;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00017CD4 File Offset: 0x00015ED4
		private static Dictionary<EventDescription, EventType> CreateDescriptionToTypeMap()
		{
			return new Dictionary<EventDescription, EventType>
			{
				{
					Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.Submitted,
					EventType.Submit
				},
				{
					Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.Expanded,
					EventType.Expand
				},
				{
					Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.Delivered,
					EventType.Deliver
				},
				{
					Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.FailedGeneral,
					EventType.Fail
				},
				{
					Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.FailedModeration,
					EventType.Fail
				},
				{
					Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.FailedTransportRules,
					EventType.Fail
				},
				{
					Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.TransferredToForeignOrg,
					EventType.Transferred
				},
				{
					Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.TransferredToLegacyExchangeServer,
					EventType.Transferred
				},
				{
					Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.Read,
					EventType.Deliver
				},
				{
					Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.NotRead,
					EventType.Deliver
				},
				{
					Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.MovedToFolderByInboxRule,
					EventType.Deliver
				},
				{
					Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.ForwardedToDelegateAndDeleted,
					EventType.Deliver
				},
				{
					Microsoft.Exchange.InfoWorker.Common.MessageTracking.EventDescription.ExpiredWithNoModerationDecision,
					EventType.Fail
				}
			};
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00017D6C File Offset: 0x00015F6C
		private static RecipientTrackingEvent.FormatterMethod SimpleDelegate(LocalizedString message)
		{
			return (RecipientTrackingEvent.FormatterSource source, string[] args) => message;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00017DCC File Offset: 0x00015FCC
		private static RecipientTrackingEvent.FormatterMethod ForeignOrgDelegate()
		{
			return delegate(RecipientTrackingEvent.FormatterSource source, string[] args)
			{
				if (Parse.IsSMSRecipient(source.RecipientTrackingEvent.RecipientAddress.ToString()))
				{
					return CoreStrings.EventTransferredToSMSMessage;
				}
				return CoreStrings.EventTransferredToForeignOrgIW;
			};
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00017EAA File Offset: 0x000160AA
		private static RecipientTrackingEvent.FormatterMethod FailedGeneralDelegate()
		{
			return delegate(RecipientTrackingEvent.FormatterSource source, string[] args)
			{
				if (args != null && args.Length > 0)
				{
					string text;
					if (!LastError.TryParseSmtpResponseString(args[0], out text))
					{
						text = args[0];
					}
					SmtpResponse key;
					if (SmtpResponse.TryParse(text, out key))
					{
						if (key.SmtpResponseType == SmtpResponseType.PermanentError && args.Length >= 2 && string.Equals(args[1], "Content Filter Agent"))
						{
							return CoreStrings.RejectedExplanationContentFiltering;
						}
						LocalizedString result;
						if (AckReason.EnhancedTextGetter.TryGetValue(key, out result))
						{
							return result;
						}
						if (!string.IsNullOrEmpty(key.EnhancedStatusCode))
						{
							if (DsnShortMessages.TryGetResourceRecipientExplanation(key.EnhancedStatusCode, out result))
							{
								return result;
							}
							LocalizedString? customDsnCode = RecipientTrackingEvent.GetCustomDsnCode(key.EnhancedStatusCode, source.ConfigSession, source.UserLanguages);
							if (customDsnCode != null)
							{
								return customDsnCode.Value;
							}
						}
					}
				}
				return CoreStrings.EventFailedGeneral;
			};
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00017ECC File Offset: 0x000160CC
		private static LocalizedString? GetCustomDsnCode(string enhancedStatus, IConfigurationSession session, IEnumerable<CultureInfo> userLanguages)
		{
			if (string.IsNullOrEmpty(enhancedStatus))
			{
				throw new InvalidOperationException("Cannot find custom text without EnhancedStatus");
			}
			ADObjectId orgContainerId = session.GetOrgContainerId();
			ObjectId dsnCustomizationContainer = SystemMessage.GetDsnCustomizationContainer(orgContainerId);
			QueryFilter filter = new TextFilter(ADObjectSchema.Name, enhancedStatus, MatchOptions.FullString, MatchFlags.Default);
			SystemMessage[] array = (SystemMessage[])session.Find<SystemMessage>(filter, dsnCustomizationContainer, true, null);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			if (userLanguages != null)
			{
				int num = 0;
				foreach (CultureInfo cultureInfo in userLanguages)
				{
					if (num++ >= 3)
					{
						break;
					}
					foreach (SystemMessage systemMessage in array)
					{
						if (systemMessage.Internal && (systemMessage.Language.LCID == cultureInfo.LCID || systemMessage.Language.LCID == cultureInfo.Parent.LCID))
						{
							return new LocalizedString?(new LocalizedString(systemMessage.Text));
						}
					}
				}
			}
			return null;
		}

		// Token: 0x04000219 RID: 537
		private static readonly TimeSpan SmtpHandshakeMaximumSkew = new TimeSpan(0, 15, 0);

		// Token: 0x0400021A RID: 538
		private static readonly TimeSpan MaximumDelayForNormalMessage = new TimeSpan(0, 5, 0);

		// Token: 0x0400021B RID: 539
		private static readonly TimeSpan ExcessiveDelayTimeSpan = new TimeSpan(0, 30, 0);

		// Token: 0x0400021C RID: 540
		private static RecipientTrackingEventSchema schema = ObjectSchema.GetInstance<RecipientTrackingEventSchema>();

		// Token: 0x0400021D RID: 541
		internal static int EventDescriptionsLength = Enum.GetValues(typeof(EventDescription)).Length;

		// Token: 0x0400021E RID: 542
		private static RecipientTrackingEvent.FormatterMethod[] iWFormatters = RecipientTrackingEvent.CreateIWFormatTable();

		// Token: 0x0400021F RID: 543
		private static RecipientTrackingEvent.FormatterMethod[] helpDeskFormatters = RecipientTrackingEvent.CreateHelpDeskFormatTable();

		// Token: 0x04000220 RID: 544
		private static Dictionary<EventDescription, EventType> descriptionToTypeMap = RecipientTrackingEvent.CreateDescriptionToTypeMap();

		// Token: 0x04000221 RID: 545
		private EventDescription eventDescriptionEnum;

		// Token: 0x04000222 RID: 546
		private readonly long internalMessageId;

		// Token: 0x04000223 RID: 547
		private readonly bool bccRecipient;

		// Token: 0x020000A7 RID: 167
		// (Invoke) Token: 0x060005FA RID: 1530
		private delegate LocalizedString FormatterMethod(RecipientTrackingEvent.FormatterSource source, string[] eventData);

		// Token: 0x020000A8 RID: 168
		internal class FormatterSource
		{
			// Token: 0x1700021C RID: 540
			// (get) Token: 0x060005FD RID: 1533 RVA: 0x0001806F File Offset: 0x0001626F
			// (set) Token: 0x060005FE RID: 1534 RVA: 0x00018077 File Offset: 0x00016277
			internal IConfigurationSession ConfigSession { get; private set; }

			// Token: 0x1700021D RID: 541
			// (get) Token: 0x060005FF RID: 1535 RVA: 0x00018080 File Offset: 0x00016280
			// (set) Token: 0x06000600 RID: 1536 RVA: 0x00018088 File Offset: 0x00016288
			internal MultiValuedProperty<CultureInfo> UserLanguages { get; private set; }

			// Token: 0x1700021E RID: 542
			// (get) Token: 0x06000601 RID: 1537 RVA: 0x00018091 File Offset: 0x00016291
			// (set) Token: 0x06000602 RID: 1538 RVA: 0x00018099 File Offset: 0x00016299
			internal RecipientTrackingEvent RecipientTrackingEvent { get; private set; }

			// Token: 0x06000603 RID: 1539 RVA: 0x000180A2 File Offset: 0x000162A2
			internal FormatterSource(IConfigurationSession configSession, MultiValuedProperty<CultureInfo> userLanguages, RecipientTrackingEvent recipientTrackingEvent)
			{
				this.ConfigSession = configSession;
				this.UserLanguages = userLanguages;
				this.RecipientTrackingEvent = recipientTrackingEvent;
			}
		}
	}
}
