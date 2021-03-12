using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200001A RID: 26
	internal static class SmtpResponseGenerator
	{
		// Token: 0x0600019F RID: 415 RVA: 0x000091E0 File Offset: 0x000073E0
		public static SmtpResponse GenerateResponse(MessageAction messageLevelAction, IReadOnlyMailRecipientCollection recipients, SmtpResponse smtpResponse, TimeSpan? retryInterval)
		{
			if (recipients == null)
			{
				throw new ArgumentNullException("recipients", "Required argument is not provided");
			}
			bool flag = SmtpResponseGenerator.ShouldGenerateAllRecipients(recipients);
			if (!flag && retryInterval == null && messageLevelAction != MessageAction.RetryQueue)
			{
				return smtpResponse;
			}
			StringBuilder stringBuilder = new StringBuilder();
			SmtpResponseGenerator.GenerateBanner(stringBuilder, smtpResponse.StatusCode, smtpResponse.EnhancedStatusCode);
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}{1}{2} {3} {4}{5}{6}", new object[]
			{
				smtpResponse.StatusCode,
				flag ? "-" : " ",
				flag ? smtpResponse.StatusCode : string.Empty,
				smtpResponse.EnhancedStatusCode,
				SmtpResponseGenerator.FlattenStatusText(smtpResponse),
				SmtpResponseGenerator.GenerateAdditionalContextForMessage(messageLevelAction, retryInterval),
				"\r\n"
			});
			if (flag)
			{
				IEnumerator<MailRecipient> enumerator = recipients.GetEnumerator();
				bool flag2 = enumerator.MoveNext();
				while (flag2)
				{
					MailRecipient mailRecipient = enumerator.Current;
					flag2 = enumerator.MoveNext();
					bool value = mailRecipient.ExtendedProperties.GetValue<bool>("Microsoft.Exchange.Transport.MailboxTransport.RetryOnDuplicateDelivery ", false);
					mailRecipient.ExtendedProperties.Remove("Microsoft.Exchange.Transport.MailboxTransport.RetryOnDuplicateDelivery ");
					SmtpResponse responseForRecipient = SmtpResponseGenerator.GetResponseForRecipient(mailRecipient, value);
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}{1}{2} {3} {4}{5}{6}", new object[]
					{
						smtpResponse.StatusCode,
						flag2 ? "-" : " ",
						responseForRecipient.StatusCode,
						responseForRecipient.EnhancedStatusCode,
						SmtpResponseGenerator.FlattenStatusText(responseForRecipient),
						SmtpResponseGenerator.GenerateAdditionalContextForRecipient(mailRecipient, value),
						"\r\n"
					});
				}
			}
			return SmtpResponse.Parse(stringBuilder.ToString());
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00009384 File Offset: 0x00007584
		public static string FlattenStatusText(SmtpResponse response)
		{
			if (response.StatusText == null || response.StatusText.Length == 0)
			{
				return string.Empty;
			}
			if (response.StatusText.Length == 1)
			{
				return response.StatusText[0];
			}
			IEnumerator<string> enumerator = ((IEnumerable<string>)response.StatusText).GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(enumerator.Current);
			while (enumerator.MoveNext())
			{
				stringBuilder.Append("; ");
				stringBuilder.Append(enumerator.Current);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00009418 File Offset: 0x00007618
		private static string GenerateAdditionalContextForRecipient(MailRecipient recipient, bool retryOnDuplicateDelivery)
		{
			string text = "resubmit";
			bool flag = false;
			if (recipient.AckStatus == AckStatus.Resubmit)
			{
				text = "resubmit";
				flag = true;
			}
			else if (retryOnDuplicateDelivery)
			{
				text = "retryonduplicatedelivery";
				flag = true;
			}
			else if (recipient.AckStatus == AckStatus.SuccessNoDsn)
			{
				text = "skipdsn";
				flag = true;
			}
			ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(64608U, text);
			return string.Format(CultureInfo.InvariantCulture, "[{0}={1}]", new object[]
			{
				text,
				flag
			});
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00009494 File Offset: 0x00007694
		private static string GenerateAdditionalContextForMessage(MessageAction messageLevelAction, TimeSpan? retryInterval)
		{
			string text = "resubmit";
			object obj = false;
			if (messageLevelAction == MessageAction.RetryQueue)
			{
				text = "retryqueue";
				obj = true;
			}
			else if (retryInterval != null)
			{
				text = "retryinterval";
				obj = retryInterval.Value.ToString();
			}
			ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(48224U, text);
			return string.Format(CultureInfo.InvariantCulture, "[{0}={1}]", new object[]
			{
				text,
				obj
			});
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00009518 File Offset: 0x00007718
		private static bool ShouldGenerateAllRecipients(IReadOnlyMailRecipientCollection recipients)
		{
			foreach (MailRecipient mailRecipient in recipients)
			{
				if (mailRecipient.AckStatus != AckStatus.Success || mailRecipient.ExtendedProperties.GetValue<bool>("Microsoft.Exchange.Transport.MailboxTransport.RetryOnDuplicateDelivery ", false))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000957C File Offset: 0x0000777C
		private static SmtpResponse CreateStoreDriverRetireResponse()
		{
			StringBuilder stringBuilder = new StringBuilder();
			SmtpResponseGenerator.GenerateBanner(stringBuilder, "432", "4.3.2");
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}{1}{2} {3} {4}{5}{6}", new object[]
			{
				"432",
				" ",
				"432",
				"4.3.2",
				"STOREDRV.Deliver; Component has been retired",
				string.Format(CultureInfo.InvariantCulture, "[{0}={1}]", new object[]
				{
					"resubmit",
					true
				}),
				"\r\n"
			});
			return SmtpResponse.Parse(stringBuilder.ToString());
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00009620 File Offset: 0x00007820
		private static void GenerateBanner(StringBuilder responseBuilder, string statusCode, string enhancedStatusCode)
		{
			responseBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}{1}{2} {3} {4}{5}{6}", new object[]
			{
				statusCode,
				"-",
				statusCode,
				enhancedStatusCode,
				"STOREDRV.Deliver; delivery result banner",
				string.Empty,
				"\r\n"
			});
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00009674 File Offset: 0x00007874
		private static SmtpResponse GetResponseForRecipient(MailRecipient recipient, bool retryOnDuplicateDelivery)
		{
			SmtpResponse result = recipient.SmtpResponse;
			if (result.Equals(SmtpResponse.Empty))
			{
				result = SmtpResponse.NoopOk;
			}
			if (retryOnDuplicateDelivery)
			{
				result = AckReason.DeliverAgentTransientFailure;
				if (recipient.ExtendedProperties.Contains("ExceptionAgentName"))
				{
					result.StatusText[0] = string.Format("{0}[Agent: {1}]", AckReason.DeliverAgentTransientFailure.StatusText[0], recipient.ExtendedProperties.GetValue<string>("ExceptionAgentName", string.Empty));
				}
			}
			return result;
		}

		// Token: 0x040000B2 RID: 178
		private const string CRLF = "\r\n";

		// Token: 0x040000B3 RID: 179
		public static readonly SmtpResponse StoreDriverRetireResponse = SmtpResponseGenerator.CreateStoreDriverRetireResponse();
	}
}
