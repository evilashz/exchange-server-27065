using System;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000943 RID: 2371
	internal static class ValidateModernGroupInputHelper
	{
		// Token: 0x06004493 RID: 17555 RVA: 0x000EC8B9 File Offset: 0x000EAAB9
		public static bool IsAliasValid(IRecipientSession recipientSession, OrganizationId organizationId, string alias, Task.TaskVerboseLoggingDelegate logHandler, Task.ErrorLoggerDelegate writeError, ExchangeErrorCategory errorLoggerCategory)
		{
			return !string.IsNullOrWhiteSpace(alias) && alias.Length <= 64 && ValidateModernGroupInputHelper.ValidAliasRegex.IsMatch(alias) && RecipientTaskHelper.IsAliasUnique(recipientSession, organizationId, null, alias, logHandler, writeError, errorLoggerCategory);
		}

		// Token: 0x06004494 RID: 17556 RVA: 0x000EC8EC File Offset: 0x000EAAEC
		public static bool IsSmtpAddressUnique(IRecipientSession recipientSession, string alias, string domain)
		{
			ADRecipient adrecipient = recipientSession.FindByProxyAddress(ProxyAddress.Parse(alias + "@" + domain));
			return adrecipient == null;
		}

		// Token: 0x06004495 RID: 17557 RVA: 0x000EC918 File Offset: 0x000EAB18
		public static bool IsNameUnique(IRecipientSession recipientSession, OrganizationId organizationId, string name, Task.TaskVerboseLoggingDelegate logHandler, Task.ErrorLoggerDelegate writeError, ExchangeErrorCategory errorLoggerCategory)
		{
			ArgumentValidator.ThrowIfNull("organizationId", organizationId);
			ArgumentValidator.ThrowIfNull("writeError", writeError);
			ADScope scope = null;
			if (organizationId.OrganizationalUnit != null)
			{
				scope = new ADScope(organizationId.OrganizationalUnit, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.OrganizationalUnitRoot, organizationId.OrganizationalUnit));
			}
			return RecipientTaskHelper.IsPropertyValueUnique(recipientSession, scope, null, new ADPropertyDefinition[]
			{
				ADObjectSchema.Name
			}, ADObjectSchema.Name, name, false, logHandler, writeError, errorLoggerCategory, false);
		}

		// Token: 0x040027F7 RID: 10231
		private const int MaximumAliasLength = 64;

		// Token: 0x040027F8 RID: 10232
		private static readonly Regex ValidAliasRegex = new Regex("^[A-Za-z0-9-_\\.]+$", RegexOptions.Compiled);
	}
}
