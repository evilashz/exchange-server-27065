using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000794 RID: 1940
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ExchangePrincipalConstraintProvider : UserConstraintProvider
	{
		// Token: 0x06004912 RID: 18706 RVA: 0x00131BB2 File Offset: 0x0012FDB2
		public ExchangePrincipalConstraintProvider(IExchangePrincipal exchangePrincipal, string rampId, bool isFirstRelease) : this(exchangePrincipal, rampId, isFirstRelease, UserConstraintProvider.SupportedLocales)
		{
		}

		// Token: 0x06004913 RID: 18707 RVA: 0x00131BC2 File Offset: 0x0012FDC2
		internal ExchangePrincipalConstraintProvider(IExchangePrincipal exchangePrincipal, string rampId, bool isFirstRelease, List<CultureInfo> supportedLocales) : base(exchangePrincipal.PrincipalId, ExchangePrincipalConstraintProvider.GetOrganization(exchangePrincipal), ExchangePrincipalConstraintProvider.GetLocale(exchangePrincipal, supportedLocales), rampId, isFirstRelease, false, ExchangePrincipalConstraintProvider.GetUserType(exchangePrincipal))
		{
		}

		// Token: 0x06004914 RID: 18708 RVA: 0x00131BE7 File Offset: 0x0012FDE7
		private static VariantConfigurationUserType GetUserType(IExchangePrincipal exchangePrincipal)
		{
			if (exchangePrincipal.MailboxInfo == null || exchangePrincipal.MailboxInfo.OrganizationId == null)
			{
				return VariantConfigurationUserType.None;
			}
			if (!Globals.IsConsumerOrganization(exchangePrincipal.MailboxInfo.OrganizationId))
			{
				return VariantConfigurationUserType.Business;
			}
			return VariantConfigurationUserType.Consumer;
		}

		// Token: 0x06004915 RID: 18709 RVA: 0x00131C1C File Offset: 0x0012FE1C
		private static string GetOrganization(IExchangePrincipal exchangePrincipal)
		{
			if (exchangePrincipal == null)
			{
				throw new ArgumentNullException("exchangePrincipal");
			}
			if (exchangePrincipal.MailboxInfo != null && exchangePrincipal.MailboxInfo.OrganizationId != null && exchangePrincipal.MailboxInfo.OrganizationId.OrganizationalUnit != null)
			{
				return exchangePrincipal.MailboxInfo.OrganizationId.OrganizationalUnit.Name;
			}
			return string.Empty;
		}

		// Token: 0x06004916 RID: 18710 RVA: 0x00131C80 File Offset: 0x0012FE80
		private static string GetLocale(IExchangePrincipal exchangePrincipal, List<CultureInfo> supportedLocales)
		{
			if (exchangePrincipal == null)
			{
				throw new ArgumentNullException("exchangePrincipal");
			}
			foreach (CultureInfo cultureInfo in exchangePrincipal.PreferredCultures)
			{
				if (supportedLocales.Contains(cultureInfo))
				{
					return cultureInfo.Name;
				}
			}
			return "en-US";
		}
	}
}
