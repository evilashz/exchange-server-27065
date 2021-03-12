using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Mime.Internal;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Data.Transport.Storage;
using Microsoft.Exchange.Data.Transport.StoreDriver;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x02000013 RID: 19
	internal static class InterceptorAgentRuleEvaluator
	{
		// Token: 0x060000EE RID: 238 RVA: 0x000056D8 File Offset: 0x000038D8
		public static InterceptorAgentRule Evaluate(IEnumerable<InterceptorAgentRule> rules, MailCommandEventArgs arg)
		{
			if (rules == null || !rules.Any<InterceptorAgentRule>() || arg == null)
			{
				return null;
			}
			return InterceptorAgentRule.InternalEvaluate(rules, InterceptorAgentEvent.OnMailFrom, string.Empty, arg.FromAddress.ToString(), string.Empty, null, RoutingAddress.Empty, null, arg.SmtpSession.HelloDomain, InterceptorAgentRuleEvaluator.GetTenantId(arg.SmtpSession), InterceptorAgentRuleEvaluator.GetDirectionality(arg.SmtpSession), null);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005748 File Offset: 0x00003948
		public static InterceptorAgentRule Evaluate(IEnumerable<InterceptorAgentRule> rules, RcptCommandEventArgs arg)
		{
			if (rules == null || !rules.Any<InterceptorAgentRule>() || arg == null)
			{
				return null;
			}
			TransportMailItem transportMailItem = InterceptorAgentRuleEvaluator.GetTransportMailItem(arg.MailItem);
			return InterceptorAgentRule.InternalEvaluate(rules, InterceptorAgentEvent.OnRcptTo, string.Empty, arg.MailItem.FromAddress.ToString(), string.Empty, arg.MailItem.Recipients, arg.RecipientAddress, null, arg.SmtpSession.HelloDomain, InterceptorAgentRuleEvaluator.GetTenantId(transportMailItem), InterceptorAgentRuleEvaluator.GetDirectionality(transportMailItem), InterceptorAgentRuleEvaluator.GetAccountForest(transportMailItem));
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000057CC File Offset: 0x000039CC
		public static InterceptorAgentRule Evaluate(IEnumerable<InterceptorAgentRule> rules, EndOfHeadersEventArgs arg)
		{
			if (rules == null || !rules.Any<InterceptorAgentRule>() || arg == null)
			{
				return null;
			}
			HeaderList headers = arg.Headers;
			TransportMailItem transportMailItem = InterceptorAgentRuleEvaluator.GetTransportMailItem(arg.MailItem);
			return InterceptorAgentRule.InternalEvaluate(rules, InterceptorAgentEvent.OnEndOfHeaders, InterceptorAgentRuleEvaluator.GetValueFromHeaderId(HeaderId.Subject, headers), arg.MailItem.FromAddress.ToString(), InterceptorAgentRuleEvaluator.GetValueFromHeaderId(HeaderId.MessageId, headers), arg.MailItem.Recipients, RoutingAddress.Empty, headers, arg.SmtpSession.HelloDomain, InterceptorAgentRuleEvaluator.GetTenantId(transportMailItem), InterceptorAgentRuleEvaluator.GetDirectionality(transportMailItem), InterceptorAgentRuleEvaluator.GetAccountForest(transportMailItem));
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000585C File Offset: 0x00003A5C
		public static InterceptorAgentRule Evaluate(IEnumerable<InterceptorAgentRule> rules, EndOfDataEventArgs arg)
		{
			if (rules == null || !rules.Any<InterceptorAgentRule>() || arg == null)
			{
				return null;
			}
			HeaderList headerList = InterceptorAgentRuleEvaluator.GetHeaderList(arg.MailItem);
			TransportMailItem transportMailItem = InterceptorAgentRuleEvaluator.GetTransportMailItem(arg.MailItem);
			return InterceptorAgentRule.InternalEvaluate(rules, InterceptorAgentEvent.OnEndOfData, InterceptorAgentRuleEvaluator.GetValueFromHeaderId(HeaderId.Subject, headerList), arg.MailItem.FromAddress.ToString(), InterceptorAgentRuleEvaluator.GetValueFromHeaderId(HeaderId.MessageId, headerList), arg.MailItem.Recipients, RoutingAddress.Empty, headerList, arg.SmtpSession.HelloDomain, InterceptorAgentRuleEvaluator.GetTenantId(transportMailItem), InterceptorAgentRuleEvaluator.GetDirectionality(transportMailItem), InterceptorAgentRuleEvaluator.GetAccountForest(transportMailItem));
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000058F0 File Offset: 0x00003AF0
		public static InterceptorAgentRule Evaluate(IEnumerable<InterceptorAgentRule> rules, QueuedMessageEventArgs arg, InterceptorAgentEvent evt)
		{
			if (rules == null || !rules.Any<InterceptorAgentRule>() || arg == null)
			{
				return null;
			}
			HeaderList headerList = InterceptorAgentRuleEvaluator.GetHeaderList(arg.MailItem);
			TransportMailItem transportMailItem = InterceptorAgentRuleEvaluator.GetTransportMailItem(arg.MailItem);
			return InterceptorAgentRule.InternalEvaluate(rules, evt, InterceptorAgentRuleEvaluator.GetValueFromHeaderId(HeaderId.Subject, headerList), arg.MailItem.FromAddress.ToString(), InterceptorAgentRuleEvaluator.GetValueFromHeaderId(HeaderId.MessageId, headerList), arg.MailItem.Recipients, RoutingAddress.Empty, headerList, null, InterceptorAgentRuleEvaluator.GetTenantId(transportMailItem), InterceptorAgentRuleEvaluator.GetDirectionality(transportMailItem), InterceptorAgentRuleEvaluator.GetAccountForest(transportMailItem));
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000597C File Offset: 0x00003B7C
		internal static InterceptorAgentRule Evaluate(IEnumerable<InterceptorAgentRule> rules, StoreDriverDeliveryEventArgs arg, InterceptorAgentEvent evt)
		{
			if (rules == null || !rules.Any<InterceptorAgentRule>() || arg == null)
			{
				return null;
			}
			MailItem mailItem = new DeliverableMailItemWrapper(arg.MailItem);
			HeaderList headerList = InterceptorAgentRuleEvaluator.GetHeaderList(mailItem);
			TransportMailItem transportMailItem = InterceptorAgentRuleEvaluator.GetTransportMailItem(mailItem);
			return InterceptorAgentRule.InternalEvaluate(rules, evt, InterceptorAgentRuleEvaluator.GetValueFromHeaderId(HeaderId.Subject, headerList), arg.MailItem.FromAddress.ToString(), InterceptorAgentRuleEvaluator.GetValueFromHeaderId(HeaderId.MessageId, headerList), new EnvelopeRecipientCollectionWrapper(arg.MailItem.Recipients), RoutingAddress.Empty, headerList, null, InterceptorAgentRuleEvaluator.GetTenantId(transportMailItem), InterceptorAgentRuleEvaluator.GetDirectionality(transportMailItem), InterceptorAgentRuleEvaluator.GetAccountForest(transportMailItem));
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005A10 File Offset: 0x00003C10
		internal static InterceptorAgentRule Evaluate(IEnumerable<InterceptorAgentRule> rules, StorageEventArgs arg)
		{
			if (rules == null || !rules.Any<InterceptorAgentRule>() || arg == null)
			{
				return null;
			}
			HeaderList headerList = InterceptorAgentRuleEvaluator.GetHeaderList(arg.MailItem);
			TransportMailItem transportMailItem = InterceptorAgentRuleEvaluator.GetTransportMailItem(arg.MailItem);
			return InterceptorAgentRule.InternalEvaluate(rules, InterceptorAgentEvent.OnLoadedMessage, InterceptorAgentRuleEvaluator.GetValueFromHeaderId(HeaderId.Subject, headerList), arg.MailItem.FromAddress.ToString(), InterceptorAgentRuleEvaluator.GetValueFromHeaderId(HeaderId.MessageId, headerList), new EnvelopeRecipientCollectionWrapper(arg.MailItem.Recipients), RoutingAddress.Empty, headerList, null, InterceptorAgentRuleEvaluator.GetTenantId(transportMailItem), InterceptorAgentRuleEvaluator.GetDirectionality(transportMailItem), InterceptorAgentRuleEvaluator.GetAccountForest(transportMailItem));
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005AA4 File Offset: 0x00003CA4
		internal static InterceptorAgentRule Evaluate(IEnumerable<InterceptorAgentRule> rules, StoreDriverSubmissionEventArgs arg)
		{
			if (rules == null || !rules.Any<InterceptorAgentRule>() || arg == null)
			{
				return null;
			}
			HeaderList headerList = InterceptorAgentRuleEvaluator.GetHeaderList(arg.MailItem);
			TransportMailItem transportMailItem = InterceptorAgentRuleEvaluator.GetTransportMailItem(arg.MailItem);
			return InterceptorAgentRule.InternalEvaluate(rules, InterceptorAgentEvent.OnDemotedMessage, InterceptorAgentRuleEvaluator.GetValueFromHeaderId(HeaderId.Subject, headerList), arg.MailItem.FromAddress.ToString(), InterceptorAgentRuleEvaluator.GetValueFromHeaderId(HeaderId.MessageId, headerList), new EnvelopeRecipientCollectionWrapper(arg.MailItem.Recipients), RoutingAddress.Empty, headerList, null, InterceptorAgentRuleEvaluator.GetTenantId(transportMailItem), InterceptorAgentRuleEvaluator.GetDirectionality(transportMailItem), InterceptorAgentRuleEvaluator.GetAccountForest(transportMailItem));
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005B38 File Offset: 0x00003D38
		private static Guid GetTenantId(TransportMailItem transportMailItem)
		{
			Guid result = Guid.Empty;
			if (transportMailItem != null)
			{
				result = transportMailItem.ExternalOrganizationId;
			}
			return result;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005B58 File Offset: 0x00003D58
		private static Guid GetTenantId(SmtpSession smtpSession)
		{
			Guid empty = Guid.Empty;
			object obj;
			if (smtpSession != null && smtpSession.Properties.TryGetValue("X-MS-Exchange-Organization-Id", out obj))
			{
				Guid.TryParse(obj.ToString(), out empty);
			}
			return empty;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005B94 File Offset: 0x00003D94
		private static MailDirectionality GetDirectionality(TransportMailItem transportMailItem)
		{
			MailDirectionality result = MailDirectionality.Undefined;
			if (transportMailItem != null)
			{
				result = transportMailItem.Directionality;
			}
			return result;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005BB0 File Offset: 0x00003DB0
		private static MailDirectionality GetDirectionality(SmtpSession smtpSession)
		{
			MailDirectionality result = MailDirectionality.Undefined;
			object obj;
			if (smtpSession != null && smtpSession.Properties.TryGetValue("X-MS-Exchange-Organization-MessageDirectionality", out obj))
			{
				InterceptorAgentCondition.ValidateDirectionality(obj.ToString(), out result);
			}
			return result;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005BE5 File Offset: 0x00003DE5
		private static string GetAccountForest(TransportMailItem transportMailItem)
		{
			if (transportMailItem != null)
			{
				return transportMailItem.ExoAccountForest;
			}
			return null;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005BF4 File Offset: 0x00003DF4
		private static TransportMailItem GetTransportMailItem(MailItem mailItem)
		{
			TransportMailItem result = null;
			ITransportMailItemWrapperFacade transportMailItemWrapperFacade = mailItem as ITransportMailItemWrapperFacade;
			if (transportMailItemWrapperFacade != null)
			{
				result = (transportMailItemWrapperFacade.TransportMailItem as TransportMailItem);
			}
			return result;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005C1C File Offset: 0x00003E1C
		private static HeaderList GetHeaderList(MailItem mailItem)
		{
			HeaderList result = null;
			if (mailItem.MimeDocument != null && mailItem.MimeDocument.RootPart != null)
			{
				result = mailItem.MimeDocument.RootPart.Headers;
			}
			return result;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005C54 File Offset: 0x00003E54
		private static HeaderList GetHeaderList(DeliverableMailItem mailItem)
		{
			HeaderList result = null;
			if (mailItem.Message != null && mailItem.Message.RootPart != null)
			{
				result = mailItem.Message.RootPart.Headers;
			}
			return result;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005C8C File Offset: 0x00003E8C
		private static string GetValueFromHeaderId(HeaderId headerId, HeaderList headers)
		{
			string empty = string.Empty;
			if (headers == null)
			{
				return empty;
			}
			Header header = headers.FindFirst(headerId);
			return EmailMessageHelpers.GetHeaderValue(header) ?? string.Empty;
		}
	}
}
