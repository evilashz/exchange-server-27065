using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.MailTips;
using Microsoft.Exchange.InfoWorker.Common.Availability;

namespace Microsoft.Exchange.InfoWorker.Common.MailTips
{
	// Token: 0x02000127 RID: 295
	internal static class MailTipsUtility
	{
		// Token: 0x0600080F RID: 2063 RVA: 0x000246EC File Offset: 0x000228EC
		public static string MakeSafeHtml(int traceId, string unsafeHtml)
		{
			MailTipsUtility.GetMailTipsTracer.TraceDebug((long)traceId, "Entering MakeSafeHtml");
			HtmlToHtml htmlToHtml = new HtmlToHtml();
			htmlToHtml.FilterHtml = true;
			htmlToHtml.OutputHtmlFragment = true;
			string result;
			using (TextReader textReader = new StringReader(unsafeHtml))
			{
				using (TextWriter textWriter = new StringWriter())
				{
					try
					{
						htmlToHtml.Convert(textReader, textWriter);
					}
					catch (ExchangeDataException ex)
					{
						MailTipsUtility.GetMailTipsTracer.TraceDebug<string>((long)traceId, "Exception thrown while filtering HTML: {0}", ex.Message);
						return string.Empty;
					}
					result = textWriter.ToString().Trim();
				}
			}
			return result;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x000247A4 File Offset: 0x000229A4
		public static string GetTargetAddressDomain(ProxyAddress externalEmailAddress)
		{
			SmtpProxyAddress smtpProxyAddress = externalEmailAddress as SmtpProxyAddress;
			if (smtpProxyAddress == null)
			{
				return null;
			}
			string addressString = smtpProxyAddress.AddressString;
			if (string.IsNullOrEmpty(addressString))
			{
				return null;
			}
			string domain = ((SmtpAddress)addressString).Domain;
			if (string.IsNullOrEmpty(domain))
			{
				return null;
			}
			return domain;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00024818 File Offset: 0x00022A18
		public static ADRawEntry GetSender(IRecipientSession session, ProxyAddress sendingAs, ADPropertyDefinition[] properties)
		{
			ADRawEntry sender = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				sender = session.FindByProxyAddress(sendingAs, properties);
			});
			if (!adoperationResult.Succeeded)
			{
				if (adoperationResult.Exception is NonUniqueRecipientException)
				{
					string message = Strings.descMailTipsSenderNotUnique(sendingAs.ToString());
					throw new SenderNotUniqueException(message);
				}
				throw adoperationResult.Exception;
			}
			else
			{
				if (sender == null)
				{
					string message2 = Strings.descMailTipsSenderNotFound(sendingAs.ToString());
					throw new SenderNotFoundException(message2);
				}
				return sender;
			}
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x000248C0 File Offset: 0x00022AC0
		public static string GetCustomMailTip(RecipientData recipientData, int traceId, int lcid)
		{
			MailTipsUtility.GetMailTipsTracer.TraceFunction((long)traceId, "Entering GetBestCustomMailTip");
			CultureInfo cultureInfo = null;
			try
			{
				cultureInfo = new CultureInfo(lcid);
			}
			catch (ArgumentException)
			{
				MailTipsUtility.GetMailTipsTracer.TraceDebug((long)traceId, "Exiting GetBestCustomMailTip - invalid culture, returning default");
				return ADRecipient.DefaultMailTipGetter(recipientData.MailTipTranslations) as string;
			}
			object mailTipTranslations = recipientData.MailTipTranslations;
			if (mailTipTranslations == null)
			{
				return null;
			}
			IList<string> list = (IList<string>)mailTipTranslations;
			if (list.Count == 0)
			{
				MailTipsUtility.GetMailTipsTracer.TraceDebug((long)traceId, "Exiting GetBestCustomMailTip - no values, returning null");
				return null;
			}
			string result;
			if (list.Count != 1)
			{
				for (int i = 0; i < 10; i++)
				{
					string name = cultureInfo.Name;
					MailTipsUtility.GetMailTipsTracer.TraceDebug<string>((long)traceId, "Checking for custom MailTip for {0}", name);
					if (MailTipsUtility.TryGetTranslation(list, name, out result))
					{
						MailTipsUtility.GetMailTipsTracer.TraceDebug((long)traceId, "Exiting GetBestCustomMailTip with match");
						return result;
					}
					if (cultureInfo == CultureInfo.InvariantCulture)
					{
						MailTipsUtility.GetMailTipsTracer.TraceDebug((long)traceId, "Exiting GetBestCustomMailTip with default");
						return ADRecipient.DefaultMailTipGetter(recipientData.MailTipTranslations) as string;
					}
					cultureInfo = cultureInfo.Parent;
				}
				return ADRecipient.DefaultMailTipGetter(recipientData.MailTipTranslations) as string;
			}
			string text;
			if (ADRecipient.TryGetMailTipParts(list[0], out text, out result))
			{
				MailTipsUtility.GetMailTipsTracer.TraceDebug((long)traceId, "Exiting GetBestCustomMailTip - returning the sole value");
				return result;
			}
			MailTipsUtility.GetMailTipsTracer.TraceDebug((long)traceId, "Exiting GetBestCustomMailTip - sole value corrupt, returning null");
			return null;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00024A20 File Offset: 0x00022C20
		private static bool TryGetTranslation(IList<string> translations, string cultureName, out string result)
		{
			foreach (string text in translations)
			{
				string text2;
				if (text.StartsWith(cultureName + ":", StringComparison.OrdinalIgnoreCase) && ADRecipient.TryGetMailTipParts(text, out text2, out result))
				{
					return true;
				}
			}
			result = string.Empty;
			return false;
		}

		// Token: 0x040004DA RID: 1242
		private static readonly Trace GetMailTipsTracer = ExTraceGlobals.GetMailTipsTracer;
	}
}
