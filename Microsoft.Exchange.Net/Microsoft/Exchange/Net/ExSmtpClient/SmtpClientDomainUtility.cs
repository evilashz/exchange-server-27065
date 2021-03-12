using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.ExSmtpClient
{
	// Token: 0x02000704 RID: 1796
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class SmtpClientDomainUtility
	{
		// Token: 0x060021C4 RID: 8644 RVA: 0x00043AD0 File Offset: 0x00041CD0
		internal static bool IsValidDomain(string domain)
		{
			if (domain == null || domain.Length > 255)
			{
				return false;
			}
			SmtpClientDomainUtility.ValidationStage validationStage = SmtpClientDomainUtility.ValidationStage.DOMAIN;
			int num = 0;
			int num2 = 0;
			while (num2 < domain.Length && validationStage != SmtpClientDomainUtility.ValidationStage.ERROR)
			{
				char c = domain[num2];
				num2++;
				switch (validationStage)
				{
				case SmtpClientDomainUtility.ValidationStage.DOMAIN:
					if ((c < '\u0080' && SmtpClientDomainUtility.IsLetterOrDigit(c)) || c == '-' || c == '_')
					{
						num = num2;
						validationStage = SmtpClientDomainUtility.ValidationStage.DOMAIN_SUB;
						continue;
					}
					break;
				case SmtpClientDomainUtility.ValidationStage.DOMAIN_SUB:
					if (c == '.')
					{
						if (num2 - num > 63)
						{
							return false;
						}
						validationStage = SmtpClientDomainUtility.ValidationStage.DOMAIN_DOT;
						continue;
					}
					else if ((c < '\u0080' && SmtpClientDomainUtility.IsLetterOrDigit(c)) || c == '-' || c == '_')
					{
						validationStage = SmtpClientDomainUtility.ValidationStage.DOMAIN_SUB;
						continue;
					}
					break;
				case SmtpClientDomainUtility.ValidationStage.DOMAIN_DOT:
					if ((c < '\u0080' && SmtpClientDomainUtility.IsLetterOrDigit(c)) || c == '-' || c == '_')
					{
						num = num2;
						validationStage = SmtpClientDomainUtility.ValidationStage.DOMAIN_SUB;
						continue;
					}
					break;
				default:
					throw new NotSupportedException("Unexpected value of ValidationStage: " + validationStage.ToString());
				}
				validationStage = SmtpClientDomainUtility.ValidationStage.ERROR;
			}
			return validationStage == SmtpClientDomainUtility.ValidationStage.DOMAIN_SUB && num2 - num < 63;
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x00043BC8 File Offset: 0x00041DC8
		private static bool IsLetterOrDigit(char character)
		{
			return (character >= 'a' && character <= 'z') || (character >= 'A' && character <= 'Z') || (character >= '0' && character <= '9');
		}

		// Token: 0x04002068 RID: 8296
		private const int MaxDomainName = 255;

		// Token: 0x04002069 RID: 8297
		private const int MaxSubDomainName = 63;

		// Token: 0x02000705 RID: 1797
		private enum ValidationStage
		{
			// Token: 0x0400206B RID: 8299
			DOMAIN,
			// Token: 0x0400206C RID: 8300
			DOMAIN_SUB,
			// Token: 0x0400206D RID: 8301
			DOMAIN_DOT,
			// Token: 0x0400206E RID: 8302
			ERROR
		}
	}
}
