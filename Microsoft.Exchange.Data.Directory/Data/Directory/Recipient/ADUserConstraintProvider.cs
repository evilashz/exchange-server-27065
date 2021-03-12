using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000283 RID: 643
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ADUserConstraintProvider : UserConstraintProvider
	{
		// Token: 0x06001E9C RID: 7836 RVA: 0x00089463 File Offset: 0x00087663
		public ADUserConstraintProvider(ADUser user, string rampId, bool isFirstRelease) : this(user, rampId, isFirstRelease, UserConstraintProvider.SupportedLocales)
		{
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x00089474 File Offset: 0x00087674
		internal ADUserConstraintProvider(ADUser user, string rampId, bool isFirstRelease, List<CultureInfo> supportedLocales) : base(ADUserConstraintProvider.GetUserName(user), ADUserConstraintProvider.GetOrganization(user), ADUserConstraintProvider.GetLocale(user, supportedLocales), rampId, isFirstRelease, user.ReleaseTrack == ReleaseTrack.Preview, ADUserConstraintProvider.GetUserType(user))
		{
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x000894BE File Offset: 0x000876BE
		private static VariantConfigurationUserType GetUserType(ADUser user)
		{
			if (!(user.OrganizationId != null))
			{
				return VariantConfigurationUserType.None;
			}
			if (!user.IsConsumerOrganization())
			{
				return VariantConfigurationUserType.Business;
			}
			return VariantConfigurationUserType.Consumer;
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x000894DC File Offset: 0x000876DC
		private static string GetLocale(ADUser user, List<CultureInfo> supportedLocales)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			foreach (CultureInfo cultureInfo in user.Languages)
			{
				if (supportedLocales.Contains(cultureInfo))
				{
					return cultureInfo.Name;
				}
			}
			return "en-US";
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x00089550 File Offset: 0x00087750
		private static string GetOrganization(ADUser user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (user.OrganizationId != null && user.OrganizationId.OrganizationalUnit != null)
			{
				return user.OrganizationId.OrganizationalUnit.Name;
			}
			return string.Empty;
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x0008959C File Offset: 0x0008779C
		private static string GetUserName(ADUser user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (user.UserPrincipalName != null)
			{
				return user.UserPrincipalName.Split(new char[]
				{
					'@'
				})[0];
			}
			return string.Empty;
		}
	}
}
