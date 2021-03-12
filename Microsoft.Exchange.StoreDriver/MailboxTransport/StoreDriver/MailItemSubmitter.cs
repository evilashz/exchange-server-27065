using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriver;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Configuration;
using Microsoft.Exchange.Transport.Storage;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.MailboxTransport.StoreDriver
{
	// Token: 0x02000004 RID: 4
	internal class MailItemSubmitter
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000213C File Offset: 0x0000033C
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002144 File Offset: 0x00000344
		internal LatencyTracker LatencyTracker { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000214D File Offset: 0x0000034D
		internal TimeSpan RpcLatency
		{
			get
			{
				return this.rpcLatency;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002155 File Offset: 0x00000355
		public MailItemSubmitter(ulong submissionConnectionId, SubmissionInfo submissionInfo)
		{
			this.LatencyTracker = LatencyTracker.CreateInstance(LatencyComponent.StoreDriverSubmit);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000216C File Offset: 0x0000036C
		internal static bool IsValidP2RecipientType(int? recipientType)
		{
			if (recipientType == null)
			{
				MailItemSubmitter.diag.TraceError(0L, "Property PR_RECIPIENT_TYPE doesn't exist");
				return false;
			}
			bool flag = 0 <= recipientType.Value && recipientType.Value <= 3;
			if (!flag)
			{
				MailItemSubmitter.diag.TraceError<int>(0L, "Unexpected value {0} for property PR_RECIPIENT_TYPE", recipientType.Value);
			}
			return flag;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021CC File Offset: 0x000003CC
		internal static void CopySenderTo(SubmissionItem submissionItem, TransportMailItem message)
		{
			string messageClass = submissionItem.MessageClass;
			if (messageClass.StartsWith("IPM.Note.Rules.OofTemplate.", StringComparison.OrdinalIgnoreCase) || messageClass.StartsWith("IPM.Note.Rules.ExternalOofTemplate.", StringComparison.OrdinalIgnoreCase) || messageClass.StartsWith("IPM.Recall.Report.", StringComparison.OrdinalIgnoreCase) || messageClass.StartsWith("IPM.Conflict.Message", StringComparison.OrdinalIgnoreCase) || messageClass.StartsWith("IPM.Conflict.Folder", StringComparison.OrdinalIgnoreCase))
			{
				MailItemSubmitter.diag.TraceDebug<string>(0L, "Message class is {0}, setting <> as the P1 reverse path", submissionItem.MessageClass);
				message.From = RoutingAddress.NullReversePath;
				return;
			}
			RoutingAddress from;
			if (MailItemSubmitter.TryGetRoutingAddressFromParticipant(message.ADRecipientCache, submissionItem.Sender, "Sender", out from))
			{
				message.From = from;
				return;
			}
			throw new InvalidSenderException(submissionItem.Sender);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002278 File Offset: 0x00000478
		internal static bool PatchQuarantineSender(TransportMailItem mailItem, string quarantineSender)
		{
			if (string.IsNullOrEmpty(quarantineSender) || Components.DsnGenerator.QuarantineConfig == null || string.IsNullOrEmpty(Components.DsnGenerator.QuarantineConfig.Mailbox))
			{
				return true;
			}
			quarantineSender = quarantineSender.Trim(new char[]
			{
				'<',
				'>'
			});
			RoutingAddress routingAddress = new RoutingAddress(quarantineSender);
			if (!routingAddress.IsValid || !StoreDriverConfig.Instance.IsUserQuarantineMailbox(mailItem.From.ToString(), Components.DsnGenerator.QuarantineConfig.Mailbox, mailItem.OrganizationId))
			{
				MailItemSubmitter.diag.TraceError<RoutingAddress>(0L, "Invalid quarantine sender {0} for released quarantine-DSN. Sender unpatched", routingAddress);
				return false;
			}
			mailItem.From = routingAddress;
			mailItem.Message.From = new EmailRecipient(null, quarantineSender);
			return false;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002348 File Offset: 0x00000548
		internal static int CopyRecipientsTo(SubmissionItem submissionItem, TransportMailItem mailItem, SubmissionRecipientHandler recipientHandler, ref List<string> unresolvableRecipients, ref List<string> notResponsibleRecipientsList)
		{
			MailItemSubmitter.diag.TraceDebug<long>(0L, "MailItemSubmitter.CopyRecipientsTo: Copy recipients to mailitem {0}", mailItem.RecordId);
			bool resubmittedMessage = submissionItem.ResubmittedMessage;
			mailItem.ExtendedProperties.SetValue<bool>("Microsoft.Exchange.Transport.ResentMapiMessage", resubmittedMessage);
			List<MimeRecipient> list = new List<MimeRecipient>();
			List<string> list2 = null;
			List<string> list3 = null;
			int num = 0;
			MailItemSubmitter.diag.TraceDebug<string>(0L, "Copying recipients for {0} message", resubmittedMessage ? "resubmitted" : "regular");
			foreach (Recipient recipient in submissionItem.Recipients)
			{
				string emailAddress = recipient.Participant.EmailAddress;
				MailItemSubmitter.diag.TraceDebug<string>(0L, "Processing recipient: {0}", emailAddress);
				int? num2 = null;
				RoutingAddress arg;
				if (!MailItemSubmitter.GetP2RecipientType(resubmittedMessage, recipient, out num2))
				{
					MailItemSubmitter.diag.TraceDebug<int>(0L, "MailItemSubmitter.CopyRecipientsTo: Saving recipient type {0} of P2 recipient on resubmitted message", num2.Value);
					MailItemSubmitter.SaveP2Recipient(mailItem, emailAddress, num2.Value, ref list2, ref list3);
				}
				else if (!SubmissionItem.GetValueTypePropValue<bool>(recipient, ItemSchema.Responsibility).GetValueOrDefault())
				{
					MailItemSubmitter.diag.TraceDebug<Participant>(0L, "MailItemSubmitter.CopyRecipientsTo: Skip recipient {0} since PR_RESPONSIBILITY is not true", recipient.Participant);
					if (notResponsibleRecipientsList == null)
					{
						notResponsibleRecipientsList = new List<string>(1);
					}
					string item = MailItemSubmitter.BuildParticipantString(recipient.Participant);
					notResponsibleRecipientsList.Add(item);
				}
				else if (!MailItemSubmitter.TryGetRoutingAddressFromParticipant(mailItem.ADRecipientCache, recipient.Participant, "Recipient", out arg))
				{
					if (recipientHandler != null)
					{
						recipientHandler(num2, recipient, mailItem, null);
					}
					if (unresolvableRecipients == null)
					{
						unresolvableRecipients = new List<string>(1);
					}
					string text = MailItemSubmitter.BuildParticipantString(recipient.Participant);
					unresolvableRecipients.Add(text);
					MailItemSubmitter.diag.TraceError<string>(0L, "MailItemSubmitter.CopyRecipientsTo: Invalid recipient {0}", text);
				}
				else
				{
					MailItemSubmitter.diag.TraceDebug<RoutingAddress, int?>(0L, "Added recipient: {0}, Type: {1}", arg, num2);
					MailRecipient mailRecipient = mailItem.Recipients.Add(arg.ToString());
					num++;
					if (recipientHandler != null)
					{
						recipientHandler(num2, recipient, mailItem, mailRecipient);
					}
					string refTypePropValue = SubmissionItem.GetRefTypePropValue<string>(recipient, StoreObjectSchema.DisplayName);
					MailItemSubmitter.CopyRecipientPropsFromXSOToTransport(submissionItem, recipient, mailRecipient, num2, refTypePropValue);
					if (num2 != null && num2 == 3)
					{
						MimeRecipient item2 = new MimeRecipient(refTypePropValue, arg.ToString());
						list.Add(item2);
					}
				}
			}
			MailItemSubmitter.AddExchangeOrganizationBccHeader(list, mailItem);
			if (list2 != null)
			{
				mailItem.ExtendedProperties.SetValue<List<string>>("Microsoft.Exchange.Transport.ResentMapiP2ToRecipients", list2);
			}
			if (list3 != null)
			{
				mailItem.ExtendedProperties.SetValue<List<string>>("Microsoft.Exchange.Transport.ResentMapiP2CcRecipients", list3);
			}
			return num;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000025FC File Offset: 0x000007FC
		internal static string BuildParticipantString(Participant participant)
		{
			if (participant == null)
			{
				return string.Empty;
			}
			string text = participant.RoutingType ?? string.Empty;
			string text2 = participant.EmailAddress ?? string.Empty;
			string text3 = participant.DisplayName ?? string.Empty;
			StringBuilder stringBuilder = new StringBuilder(text.Length + text2.Length + text3.Length + 6);
			stringBuilder.Append('"');
			stringBuilder.Append(text3);
			stringBuilder.Append("\" (");
			stringBuilder.Append(text);
			stringBuilder.Append(':');
			stringBuilder.Append(text2);
			stringBuilder.Append(')');
			return stringBuilder.ToString();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000026AC File Offset: 0x000008AC
		internal static void CopyRecipientPropsFromXSOToTransport(SubmissionItem submissionItem, Recipient recipient, MailRecipient transportRecipient, int? recipientType, string displayName)
		{
			if (!string.IsNullOrEmpty(displayName))
			{
				transportRecipient.ExtendedProperties.SetValue<string>("Microsoft.Exchange.MapiDisplayName", displayName);
			}
			if (recipientType != null)
			{
				transportRecipient.ExtendedProperties.SetValue<int>("Microsoft.Exchange.Transport.RecipientP2Type", recipientType.Value);
			}
			int? valueTypePropValue = SubmissionItem.GetValueTypePropValue<int>(recipient, ItemSchema.SendInternetEncoding);
			if (valueTypePropValue != null)
			{
				transportRecipient.ExtendedProperties.SetValue<int>("Microsoft.Exchange.Transport.ClientRequestedInternetEncoding", valueTypePropValue.Value);
			}
			bool? valueTypePropValue2 = SubmissionItem.GetValueTypePropValue<bool>(recipient, ItemSchema.SendRichInfo);
			if (valueTypePropValue2 != null)
			{
				transportRecipient.ExtendedProperties.SetValue<bool>("Microsoft.Exchange.Transport.ClientRequestedSendRichInfo", valueTypePropValue2.Value);
			}
			bool? valueTypePropValue3 = SubmissionItem.GetValueTypePropValue<bool>(recipient, MessageItemSchema.IsDeliveryReceiptRequested);
			if (valueTypePropValue3 == null)
			{
				valueTypePropValue3 = submissionItem.GetValueTypePropValue<bool>(MessageItemSchema.IsDeliveryReceiptRequested);
			}
			if (valueTypePropValue3.GetValueOrDefault())
			{
				transportRecipient.DsnRequested = (DsnRequestedFlags.Success | DsnRequestedFlags.Failure | DsnRequestedFlags.Delay);
			}
			bool? valueTypePropValue4 = SubmissionItem.GetValueTypePropValue<bool>(recipient, MessageItemSchema.IsNonDeliveryReceiptRequested);
			if (valueTypePropValue4 == null)
			{
				valueTypePropValue4 = submissionItem.GetValueTypePropValue<bool>(MessageItemSchema.IsNonDeliveryReceiptRequested);
			}
			if (valueTypePropValue4.GetValueOrDefault())
			{
				transportRecipient.DsnRequested |= DsnRequestedFlags.Failure;
			}
			bool? flag = new bool?(false);
			if ((valueTypePropValue3 == flag && valueTypePropValue4 == flag) || string.Equals(submissionItem.MessageClass, "ipm.replication", StringComparison.OrdinalIgnoreCase))
			{
				transportRecipient.DsnRequested = DsnRequestedFlags.Never;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002830 File Offset: 0x00000A30
		internal static bool TryGetRoutingAddress(ADRecipientCache<TransportMiniRecipient> recipientCache, string address, string type, string context, out RoutingAddress result)
		{
			result = RoutingAddress.Empty;
			MailItemSubmitter.diag.TraceDebug<string, string>(0L, "Try to get routing address for {0}:{1}.", type, address);
			string text = MailItemSubmitter.StripSingleQuotes(address);
			if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(text))
			{
				MailItemSubmitter.diag.TraceDebug<string>(0L, "Required {0} properties are empty", context);
				return false;
			}
			if (type.Equals("SMTP", StringComparison.OrdinalIgnoreCase))
			{
				result = new RoutingAddress(text);
				if (result.IsValid)
				{
					return true;
				}
				PerTenantAcceptedDomainTable perTenantAcceptedDomainTable;
				if (Components.Configuration.TryGetAcceptedDomainTable(recipientCache.OrganizationId, out perTenantAcceptedDomainTable) && perTenantAcceptedDomainTable.AcceptedDomainTable.DefaultDomain != null)
				{
					result = new RoutingAddress(text, perTenantAcceptedDomainTable.AcceptedDomainTable.DefaultDomain.DomainName.Domain);
					if (result.IsValid)
					{
						return true;
					}
				}
				result = RoutingAddress.Empty;
				MailItemSubmitter.diag.TraceDebug<string, string>(0L, "{0} Smtp Address is invalid {1}", context, text);
				return false;
			}
			else
			{
				if (MailItemSubmitter.TryGetRoutingAddressFromAD(recipientCache, address, type, out result))
				{
					return true;
				}
				if (type.Equals("X400", StringComparison.OrdinalIgnoreCase))
				{
					bool flag;
					if (!X400AddressParser.TryGetCanonical(address, false, out text, out flag))
					{
						return false;
					}
					if (string.IsNullOrEmpty(text))
					{
						return false;
					}
				}
				SmtpProxyAddress smtpProxyAddress;
				if (SmtpProxyAddress.TryEncapsulate(type, text, Components.Configuration.FirstOrgAcceptedDomainTable.DefaultDomainName, out smtpProxyAddress))
				{
					result = (RoutingAddress)smtpProxyAddress.SmtpAddress;
					return true;
				}
				MailItemSubmitter.diag.TraceDebug<string, string>(0L, "Couldn't encapsulate address {0}:{1}.", type, address);
				return false;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002998 File Offset: 0x00000B98
		internal static bool GetP2RecipientType(bool resubmittedMessage, Recipient recipient, out int? recipientType)
		{
			recipientType = SubmissionItem.GetValueTypePropValue<int>(recipient, ItemSchema.RecipientType);
			if (resubmittedMessage && recipientType != null && recipientType.Value == 268435456)
			{
				recipientType = new int?(0);
				MailItemSubmitter.diag.TraceDebug(0L, "PR_RECIPIENT_TYPE is MAPI_P1. This is a P1 (resent) recipient");
				return true;
			}
			if (!MailItemSubmitter.IsValidP2RecipientType(recipientType))
			{
				MailItemSubmitter.diag.TraceError<int?>(0L, "PR_RECIPIENT_TYPE for resubmitted message is not set or invalid: {0}. RecipientP2Type will be assumed to be \"Unknown\"", recipientType);
				recipientType = new int?(0);
			}
			MailItemSubmitter.diag.TraceDebug<string>(0L, "This is recipient is {0}", resubmittedMessage ? "a P2 (non-deliverable) recipient on resubmitted message" : "an ordinary recipient on an ordinary (non-resubmitted) message");
			return !resubmittedMessage;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002A41 File Offset: 0x00000C41
		internal static bool TryGetRoutingAddressFromParticipant(ADRecipientCache<TransportMiniRecipient> recipientCache, Participant participant, string context, out RoutingAddress result)
		{
			if (participant == null)
			{
				MailItemSubmitter.diag.TraceDebug(0L, "Null participant");
				return false;
			}
			return MailItemSubmitter.TryGetRoutingAddress(recipientCache, participant.EmailAddress, participant.RoutingType, context, out result);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002A74 File Offset: 0x00000C74
		internal static void TraceLatency(SubmissionInfo submissionInfo, string latencyType, TimeSpan latency)
		{
			if (submissionInfo.IsShadowSubmission)
			{
				ShadowSubmissionInfo shadowSubmissionInfo = (ShadowSubmissionInfo)submissionInfo;
				MailItemSubmitter.diag.TraceDebug(0L, "MessageID {0}, Sender {1}, Mdb {2}, {3} latency {4}", new object[]
				{
					shadowSubmissionInfo.InternetMessageId,
					shadowSubmissionInfo.Sender,
					shadowSubmissionInfo.MdbGuid,
					latencyType,
					latency
				});
				return;
			}
			MapiSubmissionInfo mapiSubmissionInfo = (MapiSubmissionInfo)submissionInfo;
			MailItemSubmitter.diag.TraceDebug(0L, "Event {0}, Mailbox {1}, Mdb {2}, {3} latency {4}", new object[]
			{
				mapiSubmissionInfo.EventCounter,
				mapiSubmissionInfo.MailboxGuid,
				mapiSubmissionInfo.MdbGuid,
				latencyType,
				latency
			});
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002B2E File Offset: 0x00000D2E
		internal void AddRpcLatency(TimeSpan additionalLatency, string rpcType)
		{
			this.rpcLatency += additionalLatency;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002B44 File Offset: 0x00000D44
		private static void AddExchangeOrganizationBccHeader(List<MimeRecipient> bccRecipientList, TransportMailItem mailItem)
		{
			AddressHeader addressHeader = new AddressHeader("X-MS-Exchange-Organization-BCC");
			foreach (MimeRecipient newChild in bccRecipientList)
			{
				addressHeader.AppendChild(newChild);
			}
			HeaderList headers = mailItem.Message.MimeDocument.RootPart.Headers;
			headers.AppendChild(addressHeader);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002BBC File Offset: 0x00000DBC
		private static DsnRecipientInfo CreateDsnRecipientInfo(Participant participant)
		{
			if (participant != null)
			{
				return DsnGenerator.CreateDsnRecipientInfo(participant.DisplayName, participant.EmailAddress, participant.RoutingType, AckReason.OutboundInvalidAddress);
			}
			return DsnGenerator.CreateDsnRecipientInfo(string.Empty, string.Empty, string.Empty, AckReason.OutboundInvalidAddress);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002C08 File Offset: 0x00000E08
		private static string StripSingleQuotes(string emailAddress)
		{
			if (emailAddress == null || emailAddress.Length < 3)
			{
				return emailAddress;
			}
			if (emailAddress[0] == '\'' && emailAddress[emailAddress.Length - 1] == '\'')
			{
				return emailAddress.Substring(1, emailAddress.Length - 2);
			}
			return emailAddress;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002C48 File Offset: 0x00000E48
		private static bool TryGetSendAsSubscription(MessageItem item, out ISendAsSource subscription)
		{
			subscription = null;
			MailboxSession mailboxSession = item.Session as MailboxSession;
			object obj = item.TryGetProperty(MessageItemSchema.SharingInstanceGuid);
			if (PropertyError.IsPropertyNotFound(obj))
			{
				if (mailboxSession != null && mailboxSession.MailboxOwner != null && mailboxSession.MailboxOwner.MailboxInfo.IsAggregated)
				{
					MailItemSubmitter.diag.TraceDebug(0L, "The message was submitted from an aggregated mailbox. There must be one associated send as subscription.");
					bool flag;
					if (!MailItemSubmitter.sendAsManager.TryGetSendAsSubscription(mailboxSession, out subscription, out flag))
					{
						MailItemSubmitter.diag.TraceError(0L, "Could not find a unique send as subscription.  Rejecting the message.");
						if (flag)
						{
							throw new SmtpResponseException(AckReason.AmbiguousSubscription);
						}
						throw new SmtpResponseException(AckReason.SubscriptionNotFound);
					}
				}
			}
			else
			{
				MailItemSubmitter.diag.TraceDebug(0L, "The message has a subscription id on it. Subscription id: {0}", new object[]
				{
					obj
				});
				if (mailboxSession == null)
				{
					MailItemSubmitter.diag.TraceError<Type>(0L, "The session is not the right type. Actual type: {0}", item.Session.GetType());
					throw new SmtpResponseException(AckReason.UnrecognizedSendAsMessage);
				}
				if (!MailItemSubmitter.sendAsManager.TryGetSendAsSubscription(item, mailboxSession, out subscription))
				{
					MailItemSubmitter.diag.TraceError(0L, "The subscription could not be found. Rejecting the message. Subscription id: {0}", new object[]
					{
						obj
					});
					throw new SmtpResponseException(AckReason.SubscriptionNotFound);
				}
				if (!MailItemSubmitter.sendAsManager.IsSubscriptionEnabled(subscription))
				{
					MailItemSubmitter.diag.TraceError(0L, "The subscription is not enabled. Rejecting the message. Subscription id: {0}", new object[]
					{
						obj
					});
					throw new SmtpResponseException(AckReason.SubscriptionDisabled);
				}
				if (!MailItemSubmitter.sendAsManager.IsSendAsEnabled(subscription))
				{
					MailItemSubmitter.diag.TraceError(0L, "The subscription is not enabled for send as. Rejecting the message. Subscription id: {0}", new object[]
					{
						obj
					});
					throw new SmtpResponseException(AckReason.SubscriptionNotEnabledForSendAs);
				}
				if (!MailItemSubmitter.sendAsManager.IsValidSendAsMessage(subscription, item))
				{
					MailItemSubmitter.diag.TraceError(0L, "The message does not have valid sent representing properties. Rejecting the message. Subscription id: {0}", new object[]
					{
						obj
					});
					throw new SmtpResponseException(AckReason.InvalidSendAsProperties);
				}
			}
			return subscription != null;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002E24 File Offset: 0x00001024
		private static void SaveP2Recipient(TransportMailItem mailItem, string recipientAddress, int recipientType, ref List<string> mapiToRecipients, ref List<string> mapiCcRecipients)
		{
			List<string> list;
			if (recipientType == 1)
			{
				if (mapiToRecipients == null)
				{
					mapiToRecipients = new List<string>();
				}
				list = mapiToRecipients;
			}
			else
			{
				if (recipientType != 2)
				{
					MailItemSubmitter.diag.TraceError<int>(0L, "Error, recipient type of MAPI_P2 recipient was not To or Cc: {0}", recipientType);
					return;
				}
				if (mapiCcRecipients == null)
				{
					mapiCcRecipients = new List<string>();
				}
				list = mapiCcRecipients;
			}
			int num = list.BinarySearch(recipientAddress, StringComparer.OrdinalIgnoreCase);
			if (num < 0)
			{
				num = ~num;
			}
			list.Insert(num, recipientAddress);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002E8C File Offset: 0x0000108C
		private static string GetPrimarySmtpAddress(TransportMiniRecipient entry)
		{
			SmtpAddress primarySmtpAddress = entry.PrimarySmtpAddress;
			if (primarySmtpAddress == SmtpAddress.Empty)
			{
				MailItemSubmitter.diag.TraceDebug(0L, "no primary SMTP address");
				return null;
			}
			if (!primarySmtpAddress.IsValidAddress)
			{
				MailItemSubmitter.diag.TraceDebug<string>(0L, "Invalid Smtp Address {0}", primarySmtpAddress.ToString());
				return null;
			}
			return primarySmtpAddress.ToString();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002EF8 File Offset: 0x000010F8
		private static bool TryGetRoutingAddressFromAD(ADRecipientCache<TransportMiniRecipient> recipientCache, string address, string type, out RoutingAddress result)
		{
			ProxyAddress proxyAddress = ProxyAddress.Parse(type, address);
			if (proxyAddress is InvalidProxyAddress)
			{
				MailItemSubmitter.diag.TraceDebug(0L, "Proxy address is invalid");
				return false;
			}
			Result<TransportMiniRecipient> result2 = recipientCache.FindAndCacheRecipient(proxyAddress);
			if (result2.Data != null)
			{
				string primarySmtpAddress = MailItemSubmitter.GetPrimarySmtpAddress(result2.Data);
				if (string.IsNullOrEmpty(primarySmtpAddress))
				{
					MailItemSubmitter.diag.TraceDebug<string, string>(0L, "Primary SMTP address for \"{0}:{1}\" is invalid or missing", address, type);
					return false;
				}
				MailItemSubmitter.diag.TraceDebug<string>(0L, "Use primary smtp address {0}", primarySmtpAddress);
				result = new RoutingAddress(primarySmtpAddress);
				return true;
			}
			else
			{
				if (result2.Error != null && result2.Error != ProviderError.NotFound)
				{
					MailItemSubmitter.diag.TraceDebug<ProviderError>(0L, "Failed to look up due to error :{0}", result2.Error);
					return false;
				}
				MailItemSubmitter.diag.TraceDebug(0L, "The address doesn't exist in AD");
				return false;
			}
		}

		// Token: 0x04000003 RID: 3
		private const int MapiP1 = 268435456;

		// Token: 0x04000004 RID: 4
		private const int SubmitterRecords = 128;

		// Token: 0x04000005 RID: 5
		private static readonly Trace diag = ExTraceGlobals.MapiSubmitTracer;

		// Token: 0x04000006 RID: 6
		private static readonly byte[] EmptyByteArray = new byte[0];

		// Token: 0x04000007 RID: 7
		private static SendAsManager sendAsManager = new SendAsManager();

		// Token: 0x04000008 RID: 8
		private static Breadcrumbs<Breadcrumb> submitterHistory = new Breadcrumbs<Breadcrumb>(128);

		// Token: 0x04000009 RID: 9
		private TimeSpan rpcLatency;

		// Token: 0x02000005 RID: 5
		internal enum SubmissionStage
		{
			// Token: 0x0400000C RID: 12
			Start,
			// Token: 0x0400000D RID: 13
			LoadADEntry,
			// Token: 0x0400000E RID: 14
			CheckThrottle,
			// Token: 0x0400000F RID: 15
			LoadItem,
			// Token: 0x04000010 RID: 16
			SendAsCheck,
			// Token: 0x04000011 RID: 17
			CreateMailItem,
			// Token: 0x04000012 RID: 18
			OnDemotedEvent,
			// Token: 0x04000013 RID: 19
			SubmitNdrForInvalidRecipients,
			// Token: 0x04000014 RID: 20
			CommitMailItem,
			// Token: 0x04000015 RID: 21
			CommitOrarMailItem,
			// Token: 0x04000016 RID: 22
			SubmitMailItem,
			// Token: 0x04000017 RID: 23
			SubmitOrarMailItem,
			// Token: 0x04000018 RID: 24
			DoneWithMessage,
			// Token: 0x04000019 RID: 25
			GenerateNdr,
			// Token: 0x0400001A RID: 26
			OpenStore
		}
	}
}
