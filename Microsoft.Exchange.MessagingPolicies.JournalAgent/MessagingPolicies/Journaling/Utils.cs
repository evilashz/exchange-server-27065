using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x02000025 RID: 37
	internal static class Utils
	{
		// Token: 0x060000DE RID: 222 RVA: 0x0000C068 File Offset: 0x0000A268
		internal static ProxyAddress RoutingAddressToProxyAddress(string routingAddress)
		{
			ProxyAddress proxyAddress;
			if (SmtpProxyAddress.IsEncapsulatedAddress(routingAddress))
			{
				if (!SmtpProxyAddress.TryDeencapsulate(routingAddress, out proxyAddress))
				{
					ExTraceGlobals.JournalingTracer.TraceError(0L, "Failed to de-encapsulate recipient address");
					throw new ArgumentException(string.Format("Could not de-encapsulate: {0}", routingAddress));
				}
			}
			else
			{
				proxyAddress = ProxyAddress.Parse(ProxyAddressPrefix.Smtp.PrimaryPrefix, routingAddress);
			}
			if (proxyAddress is InvalidProxyAddress)
			{
				ExTraceGlobals.JournalingTracer.TraceError(0L, "Recipient address was an invalid proxy address");
				throw new ArgumentException(string.Format("{0} was unable to be parsed as a RoutingAddress", routingAddress));
			}
			return proxyAddress;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000C0E8 File Offset: 0x0000A2E8
		internal static object[] ADLookupUser(MailItem mailItem, ProxyAddress proxyAddress, params PropertyDefinition[] propertiesToGet)
		{
			IADRecipientCache iadrecipientCache = (IADRecipientCache)((ITransportMailItemWrapperFacade)mailItem).TransportMailItem.ADRecipientCacheAsObject;
			Result<ADRawEntry> result = default(Result<ADRawEntry>);
			result = iadrecipientCache.FindAndCacheRecipient(proxyAddress);
			if (result.Error == ProviderError.NotFound || result.Data == null)
			{
				return null;
			}
			return Utils.GetADProperties(result, propertiesToGet);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000C13C File Offset: 0x0000A33C
		internal static List<object[]> ADLookupUsers(MailItem mailItem, List<ProxyAddress> proxyAddresses, params PropertyDefinition[] propertiesToGet)
		{
			IADRecipientCache iadrecipientCache = (IADRecipientCache)((ITransportMailItemWrapperFacade)mailItem).TransportMailItem.ADRecipientCacheAsObject;
			IList<Result<ADRawEntry>> list = iadrecipientCache.FindAndCacheRecipients(proxyAddresses);
			List<object[]> list2 = new List<object[]>(list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				Result<ADRawEntry> result = list[i];
				object[] item;
				if (result.Error == ProviderError.NotFound || result.Data == null)
				{
					item = null;
				}
				else
				{
					item = Utils.GetADProperties(list[i], propertiesToGet);
				}
				list2.Add(item);
			}
			return list2;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000C1C4 File Offset: 0x0000A3C4
		private static object[] GetADProperties(Result<ADRawEntry> result, params PropertyDefinition[] propertiesToGet)
		{
			object[] properties = result.Data.GetProperties(propertiesToGet);
			if (properties == null || properties.Length != propertiesToGet.Length)
			{
				ExTraceGlobals.JournalingTracer.TraceError(0L, "Failed to get mandatory recipient properties from AD");
				throw new ArgumentException(string.Format("Expected {0} properties in AD for recipient, but found only {1}", propertiesToGet.Length, (properties == null) ? 0 : properties.Length));
			}
			return properties;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000C222 File Offset: 0x0000A422
		internal static bool IsNdr(MailItem mailItem)
		{
			return mailItem.FromAddress == RoutingAddress.NullReversePath && Utils.IsNdr(mailItem.Message.RootPart);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000C248 File Offset: 0x0000A448
		internal static bool IsNdr(MimePart mimePart)
		{
			if (mimePart == null || mimePart.Headers == null)
			{
				return false;
			}
			HeaderList headers = mimePart.Headers;
			ContentTypeHeader contentTypeHeader = (ContentTypeHeader)headers.FindFirst(HeaderId.ContentType);
			return "multipart/report".Equals(contentTypeHeader.Value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000C288 File Offset: 0x0000A488
		internal static bool IsMessageAttachment(Attachment attachment)
		{
			return attachment != null && !string.IsNullOrEmpty(attachment.ContentType) && attachment.ContentType.Equals("message/rfc822");
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000C2AC File Offset: 0x0000A4AC
		internal static bool IsJournalReport(MimePart mimePart)
		{
			if (mimePart == null || mimePart.Headers == null)
			{
				return false;
			}
			HeaderList headers = mimePart.Headers;
			TextHeader textHeader = headers.FindFirst("X-MS-Journal-Report") as TextHeader;
			return textHeader != null;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000C2E8 File Offset: 0x0000A4E8
		internal static string[] ParseJournaledToHeader(MailItem mailItem)
		{
			HeaderList headers = mailItem.Message.RootPart.Headers;
			TextHeader textHeader = headers.FindFirst("X-MS-Exchange-Organization-Journaled-To-Recipients") as TextHeader;
			ExTraceGlobals.JournalingTracer.TraceDebug<string>(0L, "XMSExchangeJournaledToRecipients = {0}", (textHeader == null) ? "<null>" : textHeader.Value);
			if (textHeader == null || string.IsNullOrEmpty(textHeader.Value))
			{
				return null;
			}
			string[] array = textHeader.Value.Split(new char[]
			{
				'+'
			});
			foreach (string text in array)
			{
				if (!RoutingAddress.IsValidAddress(text))
				{
					ExTraceGlobals.JournalingTracer.TraceError<string>(0L, "Invalid SMTP address: {0}", text);
					return null;
				}
			}
			return array;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000C3A8 File Offset: 0x0000A5A8
		internal static void WriteJournaledToHeader(MailItem mailItem, List<string> journalTargetRecips)
		{
			if (journalTargetRecips == null || journalTargetRecips.Count == 0)
			{
				return;
			}
			HeaderList headers = mailItem.Message.RootPart.Headers;
			headers.RemoveAll("X-MS-Exchange-Organization-Journaled-To-Recipients");
			StringBuilder stringBuilder = new StringBuilder(mailItem.Recipients.Count * 32);
			bool flag = true;
			foreach (string value in journalTargetRecips)
			{
				if (!flag)
				{
					stringBuilder.Append('+');
				}
				else
				{
					flag = false;
				}
				stringBuilder.Append(value);
			}
			string text = stringBuilder.ToString();
			ExTraceGlobals.JournalingTracer.TraceDebug<string>(0L, "Writing XMSExchangeJournaledToRecipients: {0}", text);
			Header newChild = new TextHeader("X-MS-Exchange-Organization-Journaled-To-Recipients", text);
			mailItem.Message.RootPart.Headers.AppendChild(newChild);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000C488 File Offset: 0x0000A688
		internal static bool IsSmtpAddressSenderOrRecipientOnMessage(SmtpAddress address, MailItem mailItem, UserComparer userComparer)
		{
			if (address == SmtpAddress.Empty)
			{
				return false;
			}
			if (mailItem.Message.From != null && RuleUtils.CompareStringValues(address.ToString(), mailItem.Message.From.SmtpAddress, userComparer, ConditionEvaluationMode.Optimized, null))
			{
				return true;
			}
			foreach (EnvelopeRecipient envelopeRecipient in mailItem.Recipients)
			{
				if (RuleUtils.CompareStringValues(address.ToString(), envelopeRecipient.Address.ToString(), userComparer, ConditionEvaluationMode.Optimized, null))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000C54C File Offset: 0x0000A74C
		internal static void AddRecipSortedToList(string recipientEmail, ref List<string> sortedRecipientList)
		{
			if (sortedRecipientList == null)
			{
				sortedRecipientList = new List<string>();
			}
			int num = sortedRecipientList.BinarySearch(recipientEmail, StringComparer.OrdinalIgnoreCase);
			if (num < 0)
			{
				sortedRecipientList.Insert(~num, recipientEmail);
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000C580 File Offset: 0x0000A780
		internal static bool TryGetADRawEntryByEmailAddress(IADRecipientCache cache, string email, out ADRawEntry recipientEntry)
		{
			recipientEntry = null;
			Result<ADRawEntry> result = default(Result<ADRawEntry>);
			if (ProxyAddressBase.IsAddressStringValid(email) && RoutingAddress.IsValidAddress(email))
			{
				SmtpProxyAddress proxyAddress = new SmtpProxyAddress(email, true);
				result = cache.FindAndCacheRecipient(proxyAddress);
				recipientEntry = result.Data;
			}
			return recipientEntry != null && result.Error != ProviderError.NotFound;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000C5D8 File Offset: 0x0000A7D8
		internal static bool IsAuthoritativeDomain(string address, OrganizationId orgId)
		{
			if (SmtpAddress.IsValidSmtpAddress(address))
			{
				AcceptedDomainTable acceptedDomainTable = Components.Configuration.GetAcceptedDomainTable(orgId).AcceptedDomainTable;
				SmtpAddress smtpAddress = new SmtpAddress(address);
				return acceptedDomainTable.CheckAuthoritative(new SmtpDomain(smtpAddress.Domain));
			}
			return false;
		}

		// Token: 0x040000D1 RID: 209
		private const char JournalToSeparator = '+';
	}
}
