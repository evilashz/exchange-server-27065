using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using Microsoft.Exchange.Approval.Applications;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Approval;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Approval;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Mapi;
using Microsoft.Win32;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Approval
{
	// Token: 0x0200012D RID: 301
	internal sealed class ApprovalAssistant : EventBasedAssistant, IEventBasedAssistant, IAssistantBase
	{
		// Token: 0x06000C0D RID: 3085 RVA: 0x0004E563 File Offset: 0x0004C763
		public ApprovalAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
			this.approvalApplications = ApprovalApplication.CreateApprovalApplications();
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0004E584 File Offset: 0x0004C784
		internal static int ReadTestRegistryValue(string keyName, int defaultValue)
		{
			try
			{
				object value = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Exchange_Test\\v15\\BCM", keyName, defaultValue);
				if (value is int)
				{
					return (int)value;
				}
			}
			catch (SecurityException)
			{
			}
			catch (IOException)
			{
			}
			return defaultValue;
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0004E5DC File Offset: 0x0004C7DC
		public bool IsEventInteresting(MapiEvent mapiEvent)
		{
			if (mapiEvent.ItemType != ObjectType.MAPI_STORE && mapiEvent.ItemType != ObjectType.MAPI_MESSAGE)
			{
				return false;
			}
			if (this.IsApprovalStatusModified(mapiEvent) || this.IsRequestExpired(mapiEvent))
			{
				return true;
			}
			bool flag = false;
			try
			{
				flag = (VariantConfiguration.InvariantNoFlightingSnapshot.MailboxAssistants.ApprovalAssistantCheckRateLimit.Enabled || Datacenter.IsPartnerHostedOnly(true));
			}
			catch (CannotDetermineExchangeModeException)
			{
			}
			return flag && this.IsNewInitiationMessageCreated(mapiEvent) && this.IncrementAndCheckRateLimit(mapiEvent);
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0004E660 File Offset: 0x0004C860
		protected override void HandleEventInternal(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			DateTime utcNow = DateTime.UtcNow;
			bool flag = false;
			if (this.IsApprovalStatusModified(mapiEvent) || this.IsRequestExpired(mapiEvent))
			{
				bool flag2;
				MessageItem messageItem = this.GetMessageItem(mapiEvent, itemStore, item, out flag2);
				if (messageItem == null)
				{
					goto IL_315;
				}
				try
				{
					ApprovalStatus? valueAsNullable = messageItem.GetValueAsNullable<ApprovalStatus>(MessageItemSchema.ApprovalStatus);
					ApprovalApplicationId? valueAsNullable2 = messageItem.GetValueAsNullable<ApprovalApplicationId>(MessageItemSchema.ApprovalApplicationId);
					if (valueAsNullable2 != null && valueAsNullable != null)
					{
						if (this.IsApprovalStatusModified(mapiEvent) && (valueAsNullable.Value & ApprovalStatus.Succeeded) == (ApprovalStatus)0 && (valueAsNullable.Value & ApprovalStatus.Failed) == (ApprovalStatus)0)
						{
							bool? flag3 = null;
							bool flag4 = false;
							if ((valueAsNullable.Value & ApprovalStatus.Approved) != (ApprovalStatus)0)
							{
								ApprovalAssistantPerformanceCounters.ApprovalRequestsApproved.Increment();
								this.SendApprovalRequestUpdate(messageItem, itemStore, ApprovalStatus.Approved);
								flag3 = new bool?(this.approvalApplications[(int)valueAsNullable2.Value].OnApprove(messageItem));
								flag = true;
							}
							else if ((valueAsNullable.Value & ApprovalStatus.Rejected) != (ApprovalStatus)0)
							{
								ApprovalAssistantPerformanceCounters.ApprovalRequestsRejected.Increment();
								this.SendApprovalRequestUpdate(messageItem, itemStore, ApprovalStatus.Rejected);
								flag3 = new bool?(this.approvalApplications[(int)valueAsNullable2.Value].OnReject(messageItem));
								flag = true;
							}
							else if ((valueAsNullable.Value & ApprovalStatus.OofOrNdrHandled) == (ApprovalStatus)0)
							{
								if ((valueAsNullable.Value & ApprovalStatus.Ndred) != (ApprovalStatus)0)
								{
									this.approvalApplications[(int)valueAsNullable2.Value].OnAllDecisionMakersNdred(messageItem);
									flag4 = true;
								}
								else if ((valueAsNullable.Value & ApprovalStatus.Oofed) != (ApprovalStatus)0)
								{
									this.approvalApplications[(int)valueAsNullable2.Value].OnAllDecisionMakersOof(messageItem);
									flag4 = true;
								}
							}
							if (flag3 != null || flag4)
							{
								ApprovalStatus approvalStatus = valueAsNullable.Value;
								if (flag3 != null)
								{
									approvalStatus |= (flag3.Value ? ApprovalStatus.Succeeded : ApprovalStatus.Failed);
								}
								if (flag4)
								{
									approvalStatus |= ApprovalStatus.OofOrNdrHandled;
								}
								messageItem[MessageItemSchema.ApprovalStatus] = approvalStatus;
								messageItem.Save(SaveMode.ResolveConflicts);
								messageItem.Load();
							}
						}
						else if (this.IsRequestExpired(mapiEvent) && (valueAsNullable.Value & ApprovalStatus.Expired) == (ApprovalStatus)0 && (valueAsNullable.Value & ApprovalStatus.Succeeded) == (ApprovalStatus)0 && (valueAsNullable.Value & ApprovalStatus.Failed) == (ApprovalStatus)0)
						{
							ApprovalAssistantPerformanceCounters.ApprovalRequestsExpired.Increment();
							bool flag5;
							this.approvalApplications[(int)valueAsNullable2.Value].OnExpire(messageItem, out flag5);
							if (flag5)
							{
								this.SendApprovalRequestExpiry(messageItem, itemStore);
							}
							flag = true;
						}
					}
					goto IL_315;
				}
				finally
				{
					if (flag2)
					{
						messageItem.Dispose();
					}
				}
			}
			if (this.IsNewInitiationMessageCreated(mapiEvent))
			{
				int num = 0;
				lock (this.deliveryCountSyncLock)
				{
					if (this.deliveryCountTable != null && this.deliveryCountTable.TryGetValue(mapiEvent.MailboxGuid, out num))
					{
						this.deliveryCountTable[mapiEvent.MailboxGuid] = 0;
					}
				}
				if (num > ApprovalAssistant.deliveryRatePerPeriodToAlert)
				{
					Globals.Logger.LogEvent(itemStore.MailboxOwner.MailboxInfo.OrganizationId, InfoWorkerEventLogConstants.Tuple_DeliveryToArbitrationMailboxExceededRateLimits, null, itemStore.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), itemStore.MailboxOwner.MailboxInfo.OrganizationId, num, ApprovalAssistant.deliveryRatePerPeriodToAlert);
				}
			}
			IL_315:
			if (flag)
			{
				TimeSpan timeSpan = DateTime.UtcNow.Subtract(utcNow);
				ApprovalAssistantPerformanceCounters.ApprovalRequestsProcessed.Increment();
				ApprovalAssistantPerformanceCounters.LastApprovalAssistantProcessingTime.RawValue = (long)timeSpan.TotalMilliseconds;
				ApprovalAssistantPerformanceCounters.AverageApprovalAssistantProcessingTime.IncrementBy((long)timeSpan.TotalMilliseconds);
				ApprovalAssistantPerformanceCounters.AverageApprovalAssistantProcessingTimeBase.Increment();
			}
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0004EA08 File Offset: 0x0004CC08
		private bool IncrementAndCheckRateLimit(MapiEvent mapiEvent)
		{
			int num = 0;
			lock (this.deliveryCountSyncLock)
			{
				if (this.deliveryCountTable == null || this.deliveryCountTableCreatedTime.Add(ApprovalAssistant.deliveryRateCountingPeriod) < ExDateTime.UtcNow)
				{
					int capacity = (this.deliveryCountTable != null) ? this.deliveryCountTable.Count : 50;
					this.deliveryCountTable = new Dictionary<Guid, int>(capacity);
					this.deliveryCountTableCreatedTime = ExDateTime.UtcNow;
				}
				if (this.deliveryCountTable.TryGetValue(mapiEvent.MailboxGuid, out num))
				{
					num++;
				}
				else
				{
					if (this.deliveryCountTable.Count > 3000)
					{
						ApprovalAssistant.GeneralTracer.TraceDebug<int, Guid>((long)this.GetHashCode(), "There are {0} in the entries in delivery count table. New init message to {1} is not entered.", this.deliveryCountTable.Count, mapiEvent.MailboxGuid);
						return false;
					}
					num = 1;
				}
				this.deliveryCountTable[mapiEvent.MailboxGuid] = num;
			}
			return num > ApprovalAssistant.deliveryRatePerPeriodToAlert;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0004EB14 File Offset: 0x0004CD14
		private void SendApprovalRequestUpdate(MessageItem initiationMessage, MailboxSession session, ApprovalStatus status)
		{
			initiationMessage.Load(ApprovalAssistant.ApprovalRequestUpdateProperties);
			string valueOrDefault = initiationMessage.GetValueOrDefault<string>(MessageItemSchema.ApprovalDecisionMaker);
			ExDateTime? valueAsNullable = initiationMessage.GetValueAsNullable<ExDateTime>(MessageItemSchema.ApprovalDecisionTime);
			if (string.IsNullOrEmpty(valueOrDefault))
			{
				ApprovalAssistant.GeneralTracer.TraceError((long)this.GetHashCode(), "has decision, but no idea who. No update");
				return;
			}
			if (valueAsNullable == null)
			{
				ApprovalAssistant.GeneralTracer.TraceError((long)this.GetHashCode(), "No decision time stored. using UtcNow");
				valueAsNullable = new ExDateTime?(ExDateTime.UtcNow);
			}
			this.SendExpiryOrUpdateMessage(initiationMessage, session, valueOrDefault, status, valueAsNullable.Value);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0004EB9F File Offset: 0x0004CD9F
		private void SendApprovalRequestExpiry(MessageItem initiationMessage, MailboxSession session)
		{
			initiationMessage.Load(ApprovalAssistant.ApprovalRequestExpiryProperties);
			this.SendExpiryOrUpdateMessage(initiationMessage, session, null, ApprovalStatus.Expired, ExDateTime.UtcNow);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0004EBBC File Offset: 0x0004CDBC
		private void SendExpiryOrUpdateMessage(MessageItem initiationMessage, MailboxSession session, string decisionMakerAddress, ApprovalStatus status, ExDateTime handledTime)
		{
			if (status != ApprovalStatus.Expired && status != ApprovalStatus.Approved && status != ApprovalStatus.Rejected)
			{
				throw new ArgumentException("Unexpected status");
			}
			StoreObjectId defaultFolderId = session.GetDefaultFolderId(DefaultFolderType.Outbox);
			using (MessageItem messageItem = MessageItem.Create(session, defaultFolderId))
			{
				messageItem.ClassName = "IPM.Note.Microsoft.Approval.Request.Recall";
				string valueOrDefault = initiationMessage.GetValueOrDefault<string>(MessageItemSchema.ApprovalAllowedDecisionMakers);
				if (string.IsNullOrEmpty(valueOrDefault))
				{
					ApprovalAssistant.GeneralTracer.TraceError((long)this.GetHashCode(), "No list of decisionmakers. No updates");
				}
				else
				{
					string valueOrDefault2 = initiationMessage.GetValueOrDefault<string>(MessageItemSchema.ApprovalRequestMessageId);
					if (string.IsNullOrEmpty(valueOrDefault2))
					{
						ApprovalAssistant.GeneralTracer.TraceError((long)this.GetHashCode(), "The approval request message id is not found.");
					}
					else
					{
						Participant valueOrDefault3 = initiationMessage.GetValueOrDefault<Participant>(MessageItemSchema.ReceivedBy);
						if (valueOrDefault3 == null)
						{
							ApprovalAssistant.GeneralTracer.TraceError((long)this.GetHashCode(), "Cannot get the arbitration mailbox information.");
						}
						else
						{
							messageItem.Sender = valueOrDefault3;
							RoutingAddress[] routingAddresses;
							if (!ApprovalUtils.TryGetDecisionMakers(valueOrDefault, out routingAddresses))
							{
								ApprovalAssistant.GeneralTracer.TraceError((long)this.GetHashCode(), "initiation message has invalid decision makers.");
							}
							else
							{
								IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, session.GetADSessionSettings(), 555, "SendExpiryOrUpdateMessage", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\Approval\\ApprovalAssistant.cs");
								string text;
								RoutingAddress[] array = this.FilterByServerVersionAndLookupDisplayName(routingAddresses, tenantOrRootOrgRecipientSession, decisionMakerAddress, out text);
								if (array.Length == 0)
								{
									ApprovalAssistant.GeneralTracer.TraceDebug((long)this.GetHashCode(), "There is no E14 or later moderator in the list.");
								}
								else
								{
									foreach (RoutingAddress address in array)
									{
										Participant participant = new Participant(string.Empty, (string)address, "SMTP");
										messageItem.Recipients.Add(participant, RecipientItemType.To);
									}
									messageItem[MessageItemSchema.ApprovalDecisionTime] = handledTime;
									messageItem[MessageItemSchema.ApprovalRequestMessageId] = valueOrDefault2;
									messageItem[MessageItemSchema.IsNonDeliveryReceiptRequested] = false;
									messageItem[MessageItemSchema.IsDeliveryReceiptRequested] = false;
									byte[] conversationIndex = initiationMessage.ConversationIndex;
									messageItem.ConversationIndex = ConversationIndex.CreateFromParent(conversationIndex).ToByteArray();
									messageItem[ItemSchema.NormalizedSubject] = initiationMessage.ConversationTopic;
									DsnHumanReadableWriter defaultDsnHumanReadableWriter = DsnHumanReadableWriter.DefaultDsnHumanReadableWriter;
									ApprovalInformation approvalInformation;
									if (status != ApprovalStatus.Expired)
									{
										if (string.IsNullOrEmpty(text))
										{
											text = decisionMakerAddress;
										}
										messageItem[MessageItemSchema.ApprovalDecision] = ((status == ApprovalStatus.Approved) ? 1 : 2);
										messageItem[MessageItemSchema.ApprovalDecisionMaker] = text;
										bool? decision = new bool?(status == ApprovalStatus.Approved);
										approvalInformation = defaultDsnHumanReadableWriter.GetDecisionUpdateInformation(initiationMessage.Subject, text, decision, new ExDateTime?(handledTime));
									}
									else
									{
										messageItem[MessageItemSchema.ApprovalDecision] = 0;
										approvalInformation = defaultDsnHumanReadableWriter.GetApprovalRequestExpiryInformation(initiationMessage.Subject);
									}
									messageItem.Subject = approvalInformation.Subject;
									BodyWriteConfiguration configuration = new BodyWriteConfiguration(BodyFormat.TextHtml, approvalInformation.MessageCharset.Name);
									using (Stream stream = messageItem.Body.OpenWriteStream(configuration))
									{
										defaultDsnHumanReadableWriter.WriteHtmlModerationBody(stream, approvalInformation);
										stream.Flush();
									}
									messageItem.SendWithoutSavingMessage();
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0004EEF0 File Offset: 0x0004D0F0
		private RoutingAddress[] FilterByServerVersionAndLookupDisplayName(RoutingAddress[] routingAddresses, IRecipientSession recipientSession, string decisionMakerAddress, out string decisionMakerDisplayName)
		{
			List<RoutingAddress> list = new List<RoutingAddress>(routingAddresses.Length);
			ProxyAddress[] array = new ProxyAddress[routingAddresses.Length];
			decisionMakerDisplayName = string.Empty;
			for (int i = 0; i < routingAddresses.Length; i++)
			{
				array[i] = new SmtpProxyAddress(routingAddresses[i].ToString(), true);
			}
			Result<ADRawEntry>[] array2 = recipientSession.FindByProxyAddresses(array, ApprovalAssistant.ExchangeVersionDisplayNameProperties);
			for (int j = 0; j < array2.Length; j++)
			{
				ADRawEntry data = array2[j].Data;
				if (data == null)
				{
					ApprovalAssistant.GeneralTracer.TraceDebug<RoutingAddress>((long)this.GetHashCode(), "AD entry not found for '{0}', will not send update to this address.", routingAddresses[j]);
				}
				else
				{
					ExchangeObjectVersion exchangeObjectVersion = (ExchangeObjectVersion)data[ADObjectSchema.ExchangeVersion];
					if (exchangeObjectVersion != null && !exchangeObjectVersion.IsOlderThan(ExchangeObjectVersion.Exchange2010))
					{
						list.Add(routingAddresses[j]);
						ProxyAddressCollection proxyAddressCollection = (ProxyAddressCollection)data[ADRecipientSchema.EmailAddresses];
						foreach (ProxyAddress proxyAddress in proxyAddressCollection)
						{
							if (string.Equals(proxyAddress.AddressString, decisionMakerAddress, StringComparison.OrdinalIgnoreCase))
							{
								decisionMakerDisplayName = (string)data[ADRecipientSchema.DisplayName];
							}
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0004F05C File Offset: 0x0004D25C
		private bool IsApprovalStatusModified(MapiEvent mapiEvent)
		{
			return (mapiEvent.ClientType == MapiEventClientTypes.ApprovalAPI || mapiEvent.ClientType == MapiEventClientTypes.Transport) && (mapiEvent.EventMask & MapiEventTypeFlags.ObjectModified) != (MapiEventTypeFlags)0 && mapiEvent.EventFlags == MapiEventFlags.None && mapiEvent.ItemType == ObjectType.MAPI_MESSAGE && string.Equals(mapiEvent.ObjectClass, "IPM.Microsoft.Approval.Initiation", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0004F0AB File Offset: 0x0004D2AB
		private bool IsRequestExpired(MapiEvent mapiEvent)
		{
			return (mapiEvent.ClientType == MapiEventClientTypes.ELC || mapiEvent.ClientType == MapiEventClientTypes.TimeBasedAssistants) && (mapiEvent.EventMask & MapiEventTypeFlags.ObjectMoved) != (MapiEventTypeFlags)0 && mapiEvent.ItemType == ObjectType.MAPI_MESSAGE && string.Equals(mapiEvent.ObjectClass, "IPM.Microsoft.Approval.Initiation", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0004F0E8 File Offset: 0x0004D2E8
		private bool IsNewInitiationMessageCreated(MapiEvent mapiEvent)
		{
			return mapiEvent.ClientType == MapiEventClientTypes.Transport && (mapiEvent.EventMask & MapiEventTypeFlags.NewMail) != (MapiEventTypeFlags)0 && mapiEvent.ItemType == ObjectType.MAPI_MESSAGE && string.Equals(mapiEvent.ObjectClass, "IPM.Microsoft.Approval.Initiation", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0004F119 File Offset: 0x0004D319
		private MessageItem GetMessageItem(MapiEvent mapiEvent, MailboxSession session, StoreObject item, out bool loaded)
		{
			loaded = false;
			if (item != null)
			{
				return item as MessageItem;
			}
			return null;
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0004F12C File Offset: 0x0004D32C
		protected override void OnStartInternal(EventBasedStartInfo startInfo)
		{
			foreach (ApprovalApplication approvalApplication in this.approvalApplications)
			{
				approvalApplication.OnStart();
			}
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0004F158 File Offset: 0x0004D358
		protected override void OnShutdownInternal()
		{
			foreach (ApprovalApplication approvalApplication in this.approvalApplications)
			{
				approvalApplication.OnStop();
			}
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0004F25C File Offset: 0x0004D45C
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0004F264 File Offset: 0x0004D464
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0004F26C File Offset: 0x0004D46C
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000778 RID: 1912
		private const int MaxArbitrationMailboxCountForRateLimitTracking = 3000;

		// Token: 0x04000779 RID: 1913
		private const int InitialCapacityRateLimitTrackingDictionary = 50;

		// Token: 0x0400077A RID: 1914
		private static readonly Trace GeneralTracer = ExTraceGlobals.GeneralTracer;

		// Token: 0x0400077B RID: 1915
		private static readonly Trace CachedStateTracer = ExTraceGlobals.CachedStateTracer;

		// Token: 0x0400077C RID: 1916
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x0400077D RID: 1917
		private static readonly PropertyDefinition[] ApprovalRequestUpdateProperties = new PropertyDefinition[]
		{
			MessageItemSchema.ApprovalRequestMessageId,
			MessageItemSchema.ReceivedBy,
			MessageItemSchema.ApprovalAllowedDecisionMakers,
			MessageItemSchema.ApprovalDecisionTime,
			MessageItemSchema.ApprovalDecisionMaker,
			MessageItemSchema.ApprovalDecision
		};

		// Token: 0x0400077E RID: 1918
		private static readonly ADPropertyDefinition[] ExchangeVersionDisplayNameProperties = new ADPropertyDefinition[]
		{
			ADObjectSchema.ExchangeVersion,
			ADRecipientSchema.DisplayName,
			ADRecipientSchema.EmailAddresses
		};

		// Token: 0x0400077F RID: 1919
		private static readonly PropertyDefinition[] ApprovalRequestExpiryProperties = new PropertyDefinition[]
		{
			MessageItemSchema.ApprovalRequestMessageId,
			MessageItemSchema.ReceivedBy
		};

		// Token: 0x04000780 RID: 1920
		private ApprovalApplication[] approvalApplications;

		// Token: 0x04000781 RID: 1921
		private static readonly TimeSpan deliveryRateCountingPeriod = TimeSpan.FromMinutes((double)ApprovalAssistant.ReadTestRegistryValue("ArbitrationMailboxCountRefresh", 1440));

		// Token: 0x04000782 RID: 1922
		private static readonly int deliveryRatePerPeriodToAlert = ApprovalAssistant.ReadTestRegistryValue("ArbitrationMailboxCountBeforeEventLog", 10000);

		// Token: 0x04000783 RID: 1923
		private Dictionary<Guid, int> deliveryCountTable;

		// Token: 0x04000784 RID: 1924
		private ExDateTime deliveryCountTableCreatedTime;

		// Token: 0x04000785 RID: 1925
		private object deliveryCountSyncLock = new object();
	}
}
