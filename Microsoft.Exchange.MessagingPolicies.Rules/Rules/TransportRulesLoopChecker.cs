using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000038 RID: 56
	internal class TransportRulesLoopChecker
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x00009BB8 File Offset: 0x00007DB8
		protected TransportRulesLoopChecker()
		{
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00009BC0 File Offset: 0x00007DC0
		internal static bool Fork(QueuedMessageEventSource eventSource, MailItem mailItem, IList<EnvelopeRecipient> recipients)
		{
			ITransportMailItemWrapperFacade transportMailItemWrapperFacade = mailItem as ITransportMailItemWrapperFacade;
			if (transportMailItemWrapperFacade != null)
			{
				TransportMailItem transportMailItem = transportMailItemWrapperFacade.TransportMailItem as TransportMailItem;
				if (transportMailItem != null)
				{
					if (transportMailItem.TransportRulesForkCount == null)
					{
						transportMailItem.TransportRulesForkCount = new ForkCount();
					}
					int num = transportMailItem.TransportRulesForkCount.Increment();
					if (num > Components.TransportAppConfig.TransportRuleConfig.TransportRuleMaxForkCount)
					{
						ExTraceGlobals.TransportRulesEngineTracer.TraceDebug(0L, "Message fork loop is detected by the Loop Checker. Fork skipped.");
						TransportAction.Logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_RuleDetectedExcessiveBifurcation, null, new object[]
						{
							num,
							mailItem.InternetMessageId
						});
						mailItem.Recipients.Clear();
						return false;
					}
				}
			}
			eventSource.Fork(recipients);
			return true;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00009C6C File Offset: 0x00007E6C
		internal static void ForkAddedRecipients(QueuedMessageEventSource eventSource, MailItem mailItem)
		{
			OrganizationId organizationID = TransportUtils.GetOrganizationID(mailItem);
			if (organizationID == OrganizationId.ForestWideOrgId)
			{
				return;
			}
			int num = TransportRulesLoopChecker.GetMessageLoopCount(mailItem);
			List<EnvelopeRecipient> recipientsAddedByRules = TransportRulesLoopChecker.GetRecipientsAddedByRules(mailItem);
			if (recipientsAddedByRules.Count == 0)
			{
				return;
			}
			if (mailItem.Recipients.Count > recipientsAddedByRules.Count && !TransportRulesLoopChecker.Fork(eventSource, mailItem, recipientsAddedByRules))
			{
				return;
			}
			num++;
			if (TransportRulesLoopChecker.IsLoopCountExceeded(num))
			{
				ExTraceGlobals.TransportRulesEngineTracer.TraceDebug<int>(0L, "Message Transport Loop Count exceeded. Message rejected: {0}", num);
				TransportRulesLoopChecker.RejectLoopedMessage(mailItem);
				return;
			}
			TransportRulesLoopChecker.StampLoopCountHeader(num, mailItem);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00009CF4 File Offset: 0x00007EF4
		internal static List<EnvelopeRecipient> GetRecipientsAddedByRules(MailItem mailItem)
		{
			List<EnvelopeRecipient> list = new List<EnvelopeRecipient>();
			foreach (EnvelopeRecipient envelopeRecipient in mailItem.Recipients)
			{
				string value = null;
				object obj;
				if (envelopeRecipient.Properties.TryGetValue("Microsoft.Exchange.Transport.AddedByTransportRule", out obj))
				{
					value = (string)obj;
				}
				if (!string.IsNullOrEmpty(value))
				{
					list.Add(envelopeRecipient);
				}
			}
			return list;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00009D74 File Offset: 0x00007F74
		internal static int GetMessageLoopCount(MailItem mailItem)
		{
			int result = 0;
			string s;
			if (TransportUtils.TryGetHeaderValue(mailItem.Message, "X-MS-Exchange-Transport-Rules-Loop", out s))
			{
				int.TryParse(s, out result);
			}
			return result;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00009DA1 File Offset: 0x00007FA1
		internal static bool IsLoopCountExceeded(int loopCount)
		{
			return loopCount > TransportRulesLoopChecker.MaxLoopCount;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00009DAB File Offset: 0x00007FAB
		internal static bool IsLoopCountExceeded(MailItem mailItem)
		{
			return TransportRulesLoopChecker.IsLoopCountExceeded(TransportRulesLoopChecker.GetMessageLoopCount(mailItem));
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00009DB8 File Offset: 0x00007FB8
		internal static bool IsIncidentReportLoopCountExceeded(MailItem mailItem)
		{
			int num = (TransportRulesLoopChecker.MaxLoopCount == 1) ? 2 : TransportRulesLoopChecker.MaxLoopCount;
			return TransportRulesLoopChecker.GetMessageLoopCount(mailItem) > num;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00009DDF File Offset: 0x00007FDF
		internal static void RejectLoopedMessage(MailItem mailItem)
		{
			RejectMessage.Reject(mailItem, TransportRulesLoopChecker.LoopExceededStatusCode, TransportRulesLoopChecker.LoopExceededEnhancedStatus, TransportRulesLoopChecker.LoopExceededReasonText);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00009DF8 File Offset: 0x00007FF8
		internal static void StampLoopCountHeader(int loopCount, MailItem mailItem)
		{
			HeaderList headers = mailItem.Message.MimeDocument.RootPart.Headers;
			Header header = headers.FindFirst("X-MS-Exchange-Transport-Rules-Loop");
			if (header != null)
			{
				header.Value = loopCount.ToString();
				return;
			}
			TransportUtils.AddHeaderToMail(mailItem.Message, "X-MS-Exchange-Transport-Rules-Loop", loopCount.ToString());
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00009E50 File Offset: 0x00008050
		internal static void StampLoopCountHeader(int loopCount, TransportMailItem mailItem)
		{
			HeaderList headers = mailItem.RootPart.Headers;
			Header header = headers.FindFirst("X-MS-Exchange-Transport-Rules-Loop");
			if (header != null)
			{
				header.Value = loopCount.ToString();
				return;
			}
			header = Header.Create("X-MS-Exchange-Transport-Rules-Loop");
			header.Value = loopCount.ToString();
			mailItem.MimeDocument.RootPart.Headers.AppendChild(header);
		}

		// Token: 0x0400017A RID: 378
		private static readonly int MaxLoopCount = 1;

		// Token: 0x0400017B RID: 379
		private static readonly string LoopExceededStatusCode = "550";

		// Token: 0x0400017C RID: 380
		private static readonly string LoopExceededReasonText = "Transport rules loop count exceeded";

		// Token: 0x0400017D RID: 381
		private static readonly string LoopExceededEnhancedStatus = "5.7.1";
	}
}
