using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.TextMessaging.MobileDriver.Resources;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000031 RID: 49
	internal static class EmailMessageHelper
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x0000638F File Offset: 0x0000458F
		public static EmailRecipient GetSender(EmailMessage email)
		{
			if (email.Sender != null)
			{
				return email.Sender;
			}
			return email.From;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000063A6 File Offset: 0x000045A6
		public static EmailRecipient GetFrom(EmailMessage email)
		{
			if (email.From != null)
			{
				return email.From;
			}
			return email.Sender;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000063C0 File Offset: 0x000045C0
		public static string GetBodyText(EmailMessage email)
		{
			Body body = email.Body;
			Encoding encoding = Encoding.GetEncoding(body.CharsetName);
			string original = string.Empty;
			using (Stream contentReadStream = body.GetContentReadStream())
			{
				using (StringWriter stringWriter = new StringWriter())
				{
					if (BodyFormat.Html == body.BodyFormat)
					{
						new HtmlToText
						{
							InputEncoding = encoding
						}.Convert(contentReadStream, stringWriter);
						original = stringWriter.ToString();
					}
					else if (BodyFormat.Rtf == body.BodyFormat)
					{
						new RtfToText().Convert(contentReadStream, stringWriter);
						original = stringWriter.ToString();
					}
					else if (BodyFormat.Text == body.BodyFormat)
					{
						using (StreamReader streamReader = new StreamReader(contentReadStream, encoding))
						{
							original = streamReader.ReadToEnd();
						}
					}
				}
			}
			return StringNormalizer.TrimTrailingAndNormalizeEol(original);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000064B0 File Offset: 0x000046B0
		public static MobileRecipient GetMobileRecipientFromImceaAddress(string address)
		{
			MobileRecipient result = null;
			ProxyAddress mobileProxyAddressFromImceaAddress = EmailMessageHelper.GetMobileProxyAddressFromImceaAddress(address);
			if (null == mobileProxyAddressFromImceaAddress)
			{
				return null;
			}
			MobileRecipient.TryParse(mobileProxyAddressFromImceaAddress.AddressString, out result);
			return result;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000064E0 File Offset: 0x000046E0
		public static ProxyAddress GetMobileProxyAddressFromImceaAddress(string address)
		{
			if (string.IsNullOrEmpty(address))
			{
				return null;
			}
			ProxyAddress proxyAddress = null;
			if (SmtpProxyAddress.IsEncapsulatedAddress(address))
			{
				if (!SmtpProxyAddress.TryDeencapsulate(address, out proxyAddress))
				{
					return null;
				}
			}
			else
			{
				proxyAddress = ProxyAddress.Parse(ProxyAddressPrefix.Smtp.PrimaryPrefix, address);
			}
			if (!string.Equals(proxyAddress.PrefixString, "MOBILE", StringComparison.OrdinalIgnoreCase))
			{
				return null;
			}
			return proxyAddress;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006534 File Offset: 0x00004734
		public static void ThrowErrorNoProviderForNotificationNDR(string textMessage, string notification)
		{
			throw new MobileDriverStateException(Strings.ErrorNoProviderForNotification(textMessage, notification));
		}
	}
}
