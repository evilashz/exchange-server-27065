using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Mime.Internal;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Transport.Configuration;
using Microsoft.Exchange.Transport.Logging.MessageTracking;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000143 RID: 323
	internal class ApprovalInitiation
	{
		// Token: 0x06000E4E RID: 3662 RVA: 0x00037138 File Offset: 0x00035338
		public static void CreateAndSubmitApprovalInitiation(TransportMailItem initiationMailItem, TransportMailItem transportMailItem, MailRecipient moderatedRecipient, string originalSenderAddress, string approverAddresses, RoutingAddress arbitrationMailboxAddress, bool sendNotification)
		{
			string text;
			if (!arbitrationMailboxAddress.IsValid || arbitrationMailboxAddress == RoutingAddress.NullReversePath)
			{
				Exception ex;
				text = ApprovalInitiation.GetArbitrationMailboxForTenant(initiationMailItem.OrganizationId, initiationMailItem.ADRecipientCache, transportMailItem.InternetMessageId, out ex);
				if (string.IsNullOrEmpty(text))
				{
					string text2 = (initiationMailItem.OrganizationId == OrganizationId.ForestWideOrgId) ? string.Empty : string.Format(CultureInfo.InvariantCulture, " (Tenant: {0})", new object[]
					{
						initiationMailItem.OrganizationId
					});
					ApprovalInitiation.eventLogger.LogEvent(TransportEventLogConstants.Tuple_ModeratedTransportNoArbitrationMailbox, initiationMailItem.OrganizationId.ToString(), new object[]
					{
						text2,
						ex
					});
					ApprovalInitiation.diag.TraceError<string, Exception>(0L, "No arbitration mailbox available in the organization{0}. Exception: {1}", text2, ex);
					throw new ApprovalInitiation.ApprovalInitiationFailedException(ApprovalInitiation.ApprovalInitiationFailedException.FailureType.NoArbitrationMailbox);
				}
			}
			else
			{
				text = (string)arbitrationMailboxAddress;
			}
			ApprovalInitiation.PopulateAndSubmitApprovalInitiation(initiationMailItem, transportMailItem, new MailRecipient[]
			{
				moderatedRecipient
			}, originalSenderAddress, approverAddresses, text, sendNotification, true, MessageTrackingSource.ROUTING, "Approval Framework");
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00037234 File Offset: 0x00035434
		public static SmtpResponse CreateAndSubmitApprovalInitiationForTransportRules(ITransportMailItemFacade transportMailItemFacade, string originalSenderAddress, string approverAddresses, string transportRuleName)
		{
			try
			{
				TransportMailItem transportMailItem = (TransportMailItem)transportMailItemFacade;
				TransportMailItem transportMailItem2 = TransportMailItem.NewSideEffectMailItem(transportMailItem, transportMailItem.ADRecipientCache.OrganizationId, LatencyComponent.Agent, MailDirectionality.Originating, transportMailItem.ExternalOrganizationId);
				Exception ex;
				string arbitrationMailboxForTenant = ApprovalInitiation.GetArbitrationMailboxForTenant(transportMailItem2.OrganizationId, transportMailItem2.ADRecipientCache, transportMailItem.InternetMessageId, out ex);
				if (string.IsNullOrEmpty(arbitrationMailboxForTenant) || !RoutingAddress.IsValidAddress(arbitrationMailboxForTenant) || RoutingAddress.NullReversePath.Equals((RoutingAddress)arbitrationMailboxForTenant))
				{
					string text = (transportMailItem2.OrganizationId == OrganizationId.ForestWideOrgId) ? string.Empty : string.Format(CultureInfo.InvariantCulture, " (Tenant: {0})", new object[]
					{
						transportMailItem2.OrganizationId
					});
					ApprovalInitiation.eventLogger.LogEvent(TransportEventLogConstants.Tuple_ModeratedTransportNoArbitrationMailbox, transportMailItem2.OrganizationId.ToString(), new object[]
					{
						text,
						ex
					});
					ApprovalInitiation.diag.TraceError<string, Exception>(0L, "No arbitration mailbox available in the organization{0}. Exception: {1}", text, ex);
					return AckReason.ModerationNoArbitrationAddress;
				}
				if (OrganizationId.ForestWideOrgId != transportMailItem2.OrganizationId)
				{
					transportMailItem2.ExtendedProperties.SetValue<string>("Microsoft.Exchange.Transport.ModeratedByTransportRule", transportRuleName);
				}
				ApprovalInitiation.PopulateAndSubmitApprovalInitiation(transportMailItem2, transportMailItem, transportMailItem.Recipients, originalSenderAddress, approverAddresses, arbitrationMailboxForTenant, true, false, MessageTrackingSource.AGENT, "Approval Framework - Transport Rules");
			}
			catch (ApprovalInitiation.ApprovalInitiationFailedException ex2)
			{
				ApprovalInitiation.diag.TraceError<ApprovalInitiation.ApprovalInitiationFailedException>(0L, "Approval initiation not created due to ApprovalInitiationFailedException {0}.  NDRing the message.", ex2);
				if (ex2.ExceptionType != ApprovalInitiation.ApprovalInitiationFailedException.FailureType.ModerationLoop)
				{
					return ex2.ExceptionSmtpResponse;
				}
			}
			return SmtpResponse.Empty;
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x000373C0 File Offset: 0x000355C0
		internal static bool IsArbitrationMailbox(ADRecipientCache<TransportMiniRecipient> cache, RoutingAddress address)
		{
			ProxyAddress proxyAddress = new SmtpProxyAddress((string)address, true);
			TransportMiniRecipient data = cache.FindAndCacheRecipient(proxyAddress).Data;
			if (data == null)
			{
				ApprovalInitiation.diag.TraceDebug<RoutingAddress>(0L, "Cannot find {0}. Assume it's not an arbitration mailbox", address);
				return false;
			}
			if (MultiValuedPropertyBase.IsNullOrEmpty(data.ApprovalApplications))
			{
				ApprovalInitiation.diag.TraceDebug<RoutingAddress>(0L, "{0} is not an arbitration mailbox used for moderation or autogroup.", address);
				return false;
			}
			RecipientTypeDetails recipientTypeDetailsValue = data.RecipientTypeDetailsValue;
			return recipientTypeDetailsValue == RecipientTypeDetails.ArbitrationMailbox;
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x00037498 File Offset: 0x00035698
		internal static string GetArbitrationMailboxForTenant(OrganizationId organizationId, ADRecipientCache<TransportMiniRecipient> adRecipientCache, string originalMessageId, out Exception exception)
		{
			string arbitrationAddress = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				if (!ApprovalInitiation.TryGetArbitrationAddress(organizationId, adRecipientCache, originalMessageId, out arbitrationAddress))
				{
					ApprovalInitiation.organizationArbitritionMailboxCache.Remove(organizationId);
					ApprovalInitiation.TryGetArbitrationAddress(organizationId, adRecipientCache, originalMessageId, out arbitrationAddress);
				}
			});
			exception = adoperationResult.Exception;
			ApprovalInitiation.diag.TraceDebug(0L, "Getting arb mbx for org '{0}' results in '{1}', error code '{2}' with exception '{3}'", new object[]
			{
				arbitrationAddress,
				organizationId,
				adoperationResult.ErrorCode,
				exception
			});
			if (exception is ADTransientException)
			{
				throw exception;
			}
			return arbitrationAddress;
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00037534 File Offset: 0x00035734
		private static bool TryGetArbitrationAddress(OrganizationId organizationId, ADRecipientCache<TransportMiniRecipient> adRecipientCache, string originalMessageId, out string arbitrationAddress)
		{
			List<ADObjectId> list = ApprovalInitiation.organizationArbitritionMailboxCache.Get(organizationId);
			if (list.Count == 0)
			{
				arbitrationAddress = string.Empty;
				return true;
			}
			int num = (originalMessageId == null) ? 0 : (originalMessageId.GetHashCode() % list.Count);
			if (num < 0)
			{
				num += list.Count;
			}
			ADRawEntry data = adRecipientCache.FindAndCacheRecipient(list[num]).Data;
			if (data == null)
			{
				arbitrationAddress = string.Empty;
				return false;
			}
			arbitrationAddress = data[ADRecipientSchema.PrimarySmtpAddress].ToString();
			return true;
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x000375B4 File Offset: 0x000357B4
		internal static MimeDocument CreateApprovalInitiation(IReadOnlyMailItem transportMailItem, ICollection<MailRecipient> moderatedRecipients, string originalSenderAddress, string approverAddresses, string fromAddress, string toAddress, bool sendNotification, bool useDuplicateDetection, out string initiationIdentifier)
		{
			EmailMessage message = transportMailItem.Message;
			HeaderList headers = message.RootPart.Headers;
			Header acceptLanguageHeader = headers.FindFirst("Accept-Language");
			Header contentLanguageHeader = headers.FindFirst(HeaderId.ContentLanguage);
			initiationIdentifier = null;
			LinkedList<string> linkedList = new LinkedList<string>();
			for (TextHeader textHeader = headers.FindFirst("X-MS-Exchange-Moderation-Loop") as TextHeader; textHeader != null; textHeader = (headers.FindNext(textHeader) as TextHeader))
			{
				string value = textHeader.Value;
				if (!string.IsNullOrEmpty(value))
				{
					if (string.Equals(fromAddress, value, StringComparison.OrdinalIgnoreCase))
					{
						throw new ApprovalInitiation.ApprovalInitiationFailedException(ApprovalInitiation.ApprovalInitiationFailedException.FailureType.ModerationLoop);
					}
					linkedList.AddLast(value);
					if (linkedList.Count >= 1)
					{
						throw new ApprovalInitiation.ApprovalInitiationFailedException(ApprovalInitiation.ApprovalInitiationFailedException.FailureType.ModerationLoop);
					}
				}
			}
			string arg;
			EmailMessage emailMessage = ModerationHelper.EncapsulateOriginalMessage(transportMailItem, moderatedRecipients, fromAddress, toAddress, ApprovalInitiation.diag, new Action<Exception>(ApprovalInitiation.OnReEncryptionError), out arg);
			ApprovalInitiation.AddModeratedRecipientsToCc(transportMailItem.ADRecipientCache, emailMessage, moderatedRecipients);
			HeaderList headers2 = emailMessage.RootPart.Headers;
			MimeInternalHelpers.CopyHeaderBetweenList(headers, headers2, "X-MS-Exchange-Transport-Rules-Loop", true);
			MimeInternalHelpers.CopyHeaderBetweenList(headers, headers2, "X-MS-Exchange-Inbox-Rules-Loop");
			foreach (string value2 in linkedList)
			{
				headers2.AppendChild(new TextHeader("X-MS-Exchange-Moderation-Loop", value2));
			}
			headers2.AppendChild(new TextHeader("X-MS-Exchange-Moderation-Loop", fromAddress));
			TextHeader textHeader2 = (TextHeader)Header.Create("X-MS-Exchange-Organization-Approval-Allowed-Decision-Makers");
			textHeader2.Value = approverAddresses;
			headers2.AppendChild(textHeader2);
			textHeader2 = (TextHeader)Header.Create("X-MS-Exchange-Organization-Approval-Requestor");
			textHeader2.Value = originalSenderAddress;
			headers2.AppendChild(textHeader2);
			textHeader2 = (TextHeader)Header.Create("X-MS-Exchange-Organization-Approval-Initiator");
			textHeader2.Value = "ModeratedTransport";
			headers2.AppendChild(textHeader2);
			textHeader2 = (TextHeader)Header.Create("X-MS-Exchange-Organization-SCL");
			textHeader2.Value = "-1";
			headers2.AppendChild(textHeader2);
			textHeader2 = (TextHeader)Header.Create("X-MS-Exchange-Organization-Do-Not-Journal");
			textHeader2.Value = "ModeratedRecipients";
			headers2.AppendChild(textHeader2);
			string value3 = ApprovalInitiation.BuildInitationMessageAcceptLanguages(contentLanguageHeader, acceptLanguageHeader, transportMailItem, (RoutingAddress)originalSenderAddress);
			if (!string.IsNullOrEmpty(value3))
			{
				Header header = Header.Create("Accept-Language");
				header.Value = value3;
				headers2.AppendChild(header);
			}
			Header header2 = headers.FindFirst("X-MS-Exchange-Organization-OriginalArrivalTime");
			string text = null;
			if (header2 != null)
			{
				text = header2.Value;
				if (!string.IsNullOrEmpty(text))
				{
					headers2.AppendChild(new AsciiTextHeader("X-MS-Exchange-Organization-OriginalArrivalTime", text));
				}
			}
			Header header3 = headers.FindFirst("Thread-Index");
			if (header3 != null)
			{
				string value4 = header3.Value;
				if (!string.IsNullOrEmpty(value4))
				{
					textHeader2 = (TextHeader)Header.Create("Thread-Index");
					textHeader2.Value = value4;
					headers2.AppendChild(textHeader2);
				}
			}
			Header header4 = headers.FindFirst("Thread-Topic");
			if (header4 != null)
			{
				string value5 = header4.Value;
				if (!string.IsNullOrEmpty(value5))
				{
					textHeader2 = (TextHeader)Header.Create("Thread-Topic");
					textHeader2.Value = value5;
					headers2.AppendChild(textHeader2);
				}
			}
			textHeader2 = (TextHeader)Header.Create("X-MS-Exchange-Organization-Approval-Allowed-Actions");
			textHeader2.Value = (sendNotification ? MimeConstant.ApprovalAllowedAction.ApproveRejectCommentOnReject.ToString() : MimeConstant.ApprovalAllowedAction.ApproveReject.ToString());
			headers2.AppendChild(textHeader2);
			string initiationMessageIdentifier = null;
			if (useDuplicateDetection)
			{
				Header header5 = headers.FindFirst("X-MS-Exchange-Organization-Moderation-SavedArrivalTime");
				if (header5 != null)
				{
					text = header5.Value;
				}
				initiationIdentifier = ApprovalInitiation.CreateInitiationMessageIdentifier(transportMailItem.InternetMessageId, text, originalSenderAddress, moderatedRecipients, out initiationMessageIdentifier);
				string arg2 = ApprovalInitiation.CreateInitiationMessageIdUniquePart();
				if (!string.IsNullOrEmpty(initiationIdentifier))
				{
					emailMessage.MessageId = string.Format("<{0}{1}@{2}>", initiationIdentifier, arg2, arg);
				}
			}
			textHeader2 = (TextHeader)Header.Create("X-MS-Exchange-Organization-Moderation-Data");
			textHeader2.Value = ApprovalInitiation.FormatApprovalDataValue(sendNotification, initiationMessageIdentifier);
			headers2.AppendChild(textHeader2);
			AsciiTextHeader asciiTextHeader = (AsciiTextHeader)Header.Create(HeaderId.References);
			asciiTextHeader.Value = transportMailItem.InternetMessageId;
			headers2.AppendChild(asciiTextHeader);
			ApprovalInitiation.AddApprovalAttachmentHeaders(emailMessage);
			return emailMessage.MimeDocument;
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x000379CC File Offset: 0x00035BCC
		private static void AddApprovalAttachmentHeaders(EmailMessage message)
		{
			foreach (Attachment attachment in message.Attachments)
			{
				string value = "Never";
				if ("OriginalMessage" == attachment.FileName)
				{
					value = "AsMessage";
				}
				TextHeader textHeader = (TextHeader)Header.Create("X-MS-Exchange-Organization-Approval-AttachToApprovalRequest");
				textHeader.Value = value;
				attachment.MimePart.Headers.AppendChild(textHeader);
			}
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00037A60 File Offset: 0x00035C60
		private static void OnReEncryptionError(Exception exception)
		{
			if (exception == null)
			{
				throw new ApprovalInitiation.ApprovalInitiationFailedException(ApprovalInitiation.ApprovalInitiationFailedException.FailureType.ReEncryption);
			}
			throw new ApprovalInitiation.ApprovalInitiationFailedException(ApprovalInitiation.ApprovalInitiationFailedException.FailureType.ReEncryption, exception);
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00037A74 File Offset: 0x00035C74
		private static void PopulateAndSubmitApprovalInitiation(TransportMailItem initiationMailItem, TransportMailItem transportMailItem, ICollection<MailRecipient> moderatedRecipients, string originalSenderAddress, string approverAddresses, string arbitrationMailboxAddress, bool sendNotification, bool useDuplicateDetection, MessageTrackingSource source, string messageSource)
		{
			string initiationMessageIdentifier;
			MimeDocument mimeDocument = ApprovalInitiation.CreateApprovalInitiation(transportMailItem, moderatedRecipients, originalSenderAddress, approverAddresses, arbitrationMailboxAddress, arbitrationMailboxAddress, sendNotification, useDuplicateDetection, out initiationMessageIdentifier);
			initiationMailItem.MimeDocument = mimeDocument;
			initiationMailItem.From = transportMailItem.From;
			initiationMailItem.Recipients.Add(arbitrationMailboxAddress);
			initiationMailItem.PerfCounterAttribution = "InQueue";
			initiationMailItem.ReceiveConnectorName = "Moderated-Transport";
			MultilevelAuth.EnsureSecurityAttributes(initiationMailItem, SubmitAuthCategory.Internal, MultilevelAuthMechanism.SecureInternalSubmit, null);
			initiationMailItem.CommitLazy();
			MessageTrackingLog.TrackInitMessageCreated(source, moderatedRecipients, transportMailItem, initiationMailItem, initiationMessageIdentifier, arbitrationMailboxAddress);
			Components.CategorizerComponent.EnqueueSideEffectMessage(transportMailItem, initiationMailItem, messageSource);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00037AFC File Offset: 0x00035CFC
		private static string BuildInitationMessageAcceptLanguages(Header contentLanguageHeader, Header acceptLanguageHeader, IReadOnlyMailItem transportMailItem, RoutingAddress originalSenderAddress)
		{
			SmtpDomain domainPart = SmtpDomain.GetDomainPart(originalSenderAddress);
			if (domainPart == null)
			{
				ApprovalInitiation.diag.TraceDebug<RoutingAddress>(0L, "Sender without domain {0}, not putting in language header", originalSenderAddress);
				return null;
			}
			PerTenantAcceptedDomainTable perTenantAcceptedDomainTable;
			CultureInfo cultureInfo = (Components.Configuration.TryGetAcceptedDomainTable(transportMailItem.OrganizationId, out perTenantAcceptedDomainTable) && perTenantAcceptedDomainTable.AcceptedDomainTable.CheckInternal(domainPart)) ? transportMailItem.TransportSettings.InternalDsnDefaultLanguage : transportMailItem.TransportSettings.ExternalDsnDefaultLanguage;
			string empty;
			if (contentLanguageHeader == null || !contentLanguageHeader.TryGetValue(out empty))
			{
				empty = string.Empty;
			}
			string empty2;
			if (acceptLanguageHeader == null || !acceptLanguageHeader.TryGetValue(out empty2))
			{
				empty2 = string.Empty;
			}
			bool flag = !string.IsNullOrEmpty(empty);
			bool flag2 = !string.IsNullOrEmpty(empty2);
			bool flag3 = cultureInfo != null;
			string text = flag3 ? cultureInfo.Name : string.Empty;
			if (!flag && !flag2 && !flag3)
			{
				ApprovalInitiation.diag.TraceDebug(0L, "No content language, accept language or org setting for culture.");
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(empty2.Length + empty.Length + text.Length + ", ".Length * 2 + ";q=0.001".Length);
			if (flag2)
			{
				stringBuilder.Append(empty2);
			}
			if (flag)
			{
				if (flag2)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(empty);
			}
			if (flag3)
			{
				if (flag2 || flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(cultureInfo.Name);
				stringBuilder.Append(";q=0.001");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00037C80 File Offset: 0x00035E80
		private static void AddModeratedRecipientsToCc(ADRecipientCache<TransportMiniRecipient> recipCache, EmailMessage message, ICollection<MailRecipient> moderatedRecipients)
		{
			foreach (MailRecipient mailRecipient in moderatedRecipients)
			{
				if (mailRecipient.Status != Status.Handled && mailRecipient.Status != Status.Complete)
				{
					string text = null;
					Result<TransportMiniRecipient> result;
					if (recipCache.TryGetValue(ProxyAddress.Parse((string)mailRecipient.Email), out result) && result.Data != null)
					{
						text = result.Data.DisplayName;
					}
					if (string.IsNullOrEmpty(text))
					{
						text = mailRecipient.Email.ToString();
					}
					message.Cc.Add(new EmailRecipient(text, (string)mailRecipient.Email));
				}
			}
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00037D48 File Offset: 0x00035F48
		private static string CreateInitiationMessageIdentifier(string originalMessageId, string submissionTime, string senderAddress, ICollection<MailRecipient> recipientList, out string fullIdentifier)
		{
			int num = 0;
			RoutingAddress email;
			foreach (MailRecipient mailRecipient in recipientList)
			{
				if (++num > 1)
				{
					break;
				}
				email = mailRecipient.Email;
			}
			if (string.IsNullOrEmpty(originalMessageId) || string.IsNullOrEmpty(submissionTime) || !email.IsValid)
			{
				fullIdentifier = null;
				return null;
			}
			string text = originalMessageId.Trim(ApprovalInitiation.MessageIdCharactersToTrim);
			text = text.Replace('@', '~');
			if (text.Length < 40)
			{
				text = text.PadRight(40, '~');
			}
			else if (text.Length > 40)
			{
				text = text.Substring(0, 40);
			}
			byte[] array = new byte[12];
			int hashCode = StringComparer.Ordinal.GetHashCode(originalMessageId);
			int value = StringComparer.OrdinalIgnoreCase.GetHashCode(submissionTime) ^ StringComparer.OrdinalIgnoreCase.GetHashCode(senderAddress);
			int hashCode2 = email.GetHashCode();
			BitConverter.GetBytes(hashCode).CopyTo(array, 0);
			BitConverter.GetBytes(value).CopyTo(array, 4);
			BitConverter.GetBytes(hashCode2).CopyTo(array, 8);
			fullIdentifier = originalMessageId + submissionTime + senderAddress + email.ToString();
			return text + Convert.ToBase64String(array, Base64FormattingOptions.None);
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00037E94 File Offset: 0x00036094
		private static string CreateInitiationMessageIdUniquePart()
		{
			byte[] array = new byte[6];
			ApprovalInitiation.rand.NextBytes(array);
			return Convert.ToBase64String(array, Base64FormattingOptions.None);
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00037EBC File Offset: 0x000360BC
		private static string FormatApprovalDataValue(bool sendNotification, string initiationMessageIdentifier)
		{
			char c = sendNotification ? '1' : '0';
			if (string.IsNullOrEmpty(initiationMessageIdentifier))
			{
				return new string(c, 1);
			}
			string arg = initiationMessageIdentifier.Replace(';', ')');
			return c + ';' + arg;
		}

		// Token: 0x040006F0 RID: 1776
		public const string ApprovalInitiator = "ModeratedTransport";

		// Token: 0x040006F1 RID: 1777
		public const string ModeratedRecipients = "ModeratedRecipients";

		// Token: 0x040006F2 RID: 1778
		public const char MessageIdAtCharacter = '@';

		// Token: 0x040006F3 RID: 1779
		private const char ApprovalDataSeparator = ';';

		// Token: 0x040006F4 RID: 1780
		private const char ApprovalDataSeparatorReplacement = ')';

		// Token: 0x040006F5 RID: 1781
		private const int InitiationIdentifierOriginalMessageIdSize = 40;

		// Token: 0x040006F6 RID: 1782
		private const char MessageIdPaddingCharacter = '~';

		// Token: 0x040006F7 RID: 1783
		private const int ModXLoopMaximumCount = 1;

		// Token: 0x040006F8 RID: 1784
		private static readonly char[] MessageIdCharactersToTrim = new char[]
		{
			'<',
			'>'
		};

		// Token: 0x040006F9 RID: 1785
		private static readonly Random rand = new Random((int)DateTime.UtcNow.Ticks);

		// Token: 0x040006FA RID: 1786
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.ApprovalTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x040006FB RID: 1787
		private static Trace diag = ExTraceGlobals.ApprovalTracer;

		// Token: 0x040006FC RID: 1788
		private static OrganizationArbitritionMailboxCache organizationArbitritionMailboxCache = new OrganizationArbitritionMailboxCache();

		// Token: 0x02000144 RID: 324
		internal class ApprovalInitiationFailedException : LocalizedException
		{
			// Token: 0x170003D1 RID: 977
			// (get) Token: 0x06000E5E RID: 3678 RVA: 0x00037F75 File Offset: 0x00036175
			public ApprovalInitiation.ApprovalInitiationFailedException.FailureType ExceptionType
			{
				get
				{
					return this.failureType;
				}
			}

			// Token: 0x170003D2 RID: 978
			// (get) Token: 0x06000E5F RID: 3679 RVA: 0x00037F80 File Offset: 0x00036180
			public SmtpResponse ExceptionSmtpResponse
			{
				get
				{
					switch (this.failureType)
					{
					case ApprovalInitiation.ApprovalInitiationFailedException.FailureType.ReEncryption:
						return ApprovalInitiation.ApprovalInitiationFailedException.reEncryptionSmtpResponse;
					case ApprovalInitiation.ApprovalInitiationFailedException.FailureType.NoArbitrationMailbox:
						return ApprovalInitiation.ApprovalInitiationFailedException.missingArbitrationMailboxSmtpResponse;
					case ApprovalInitiation.ApprovalInitiationFailedException.FailureType.ModerationLoop:
						return ApprovalInitiation.ApprovalInitiationFailedException.moderationLoopSmtpResponse;
					default:
						throw new InvalidOperationException("Unexpected failue type.");
					}
				}
			}

			// Token: 0x06000E60 RID: 3680 RVA: 0x00037FC4 File Offset: 0x000361C4
			public ApprovalInitiationFailedException(ApprovalInitiation.ApprovalInitiationFailedException.FailureType failureType) : base(new LocalizedString("Approval initiation failure"))
			{
				this.failureType = failureType;
			}

			// Token: 0x06000E61 RID: 3681 RVA: 0x00037FDD File Offset: 0x000361DD
			public ApprovalInitiationFailedException(ApprovalInitiation.ApprovalInitiationFailedException.FailureType failureType, Exception innerException) : base(new LocalizedString("Approval initiation failure"), innerException)
			{
				this.failureType = failureType;
			}

			// Token: 0x040006FD RID: 1789
			private const string ExceptionText = "Approval initiation failure";

			// Token: 0x040006FE RID: 1790
			private static SmtpResponse reEncryptionSmtpResponse = AckReason.ModerationReencrptionFailed;

			// Token: 0x040006FF RID: 1791
			private static SmtpResponse missingArbitrationMailboxSmtpResponse = AckReason.ModerationNoArbitrationAddress;

			// Token: 0x04000700 RID: 1792
			private static SmtpResponse moderationLoopSmtpResponse = AckReason.ModerationLoop;

			// Token: 0x04000701 RID: 1793
			private ApprovalInitiation.ApprovalInitiationFailedException.FailureType failureType;

			// Token: 0x02000145 RID: 325
			internal enum FailureType
			{
				// Token: 0x04000703 RID: 1795
				ReEncryption,
				// Token: 0x04000704 RID: 1796
				NoArbitrationMailbox,
				// Token: 0x04000705 RID: 1797
				ModerationLoop
			}
		}
	}
}
