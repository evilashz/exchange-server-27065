using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000CD RID: 205
	[Serializable]
	public sealed class SmtpProxyAddressTemplate : ProxyAddressTemplate
	{
		// Token: 0x06000559 RID: 1369 RVA: 0x00012DE3 File Offset: 0x00010FE3
		public SmtpProxyAddressTemplate(string addressTemplate, bool isPrimaryAddress) : base(ProxyAddressPrefix.Smtp, addressTemplate, isPrimaryAddress)
		{
			if (!SmtpProxyAddressTemplate.IsValidSmtpAddressTemplate(addressTemplate))
			{
				throw new ArgumentOutOfRangeException(DataStrings.InvalidSMTPAddressTemplateFormat(addressTemplate), null);
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00012E0C File Offset: 0x0001100C
		public static bool IsValidSmtpAddressTemplate(string smtpAddressTemplate)
		{
			if (smtpAddressTemplate == null)
			{
				throw new ArgumentNullException("smtpAddressTemplate");
			}
			int num = smtpAddressTemplate.LastIndexOf('@');
			return num != -1 && SmtpAddress.IsValidDomain(smtpAddressTemplate.Substring(num + 1));
		}
	}
}
