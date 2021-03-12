using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000796 RID: 1942
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GenericADUserConstraintProvider : UserConstraintProvider
	{
		// Token: 0x0600491A RID: 18714 RVA: 0x00131D3D File Offset: 0x0012FF3D
		public GenericADUserConstraintProvider(IGenericADUser adUser, string rampId, bool isFirstRelease) : this(adUser, rampId, isFirstRelease, UserConstraintProvider.SupportedLocales)
		{
		}

		// Token: 0x0600491B RID: 18715 RVA: 0x00131D4D File Offset: 0x0012FF4D
		internal GenericADUserConstraintProvider(IGenericADUser adUser, string rampId, bool isFirstRelease, List<CultureInfo> supportedLocales) : base(GenericADUserConstraintProvider.GetUserName(adUser), GenericADUserConstraintProvider.GetOrganization(adUser), GenericADUserConstraintProvider.GetLocale(adUser, supportedLocales), rampId, isFirstRelease, false, GenericADUserConstraintProvider.GetUserType(adUser))
		{
		}

		// Token: 0x0600491C RID: 18716 RVA: 0x00131D74 File Offset: 0x0012FF74
		private static string GetUserName(IGenericADUser adUser)
		{
			if (adUser == null)
			{
				throw new ArgumentNullException("adUser");
			}
			if (adUser.UserPrincipalName != null)
			{
				return adUser.UserPrincipalName.Split(new char[]
				{
					'@'
				})[0];
			}
			SmtpAddress primarySmtpAddress = adUser.PrimarySmtpAddress;
			if (adUser.PrimarySmtpAddress.Local != null)
			{
				return adUser.PrimarySmtpAddress.Local;
			}
			return string.Empty;
		}

		// Token: 0x0600491D RID: 18717 RVA: 0x00131DE0 File Offset: 0x0012FFE0
		private static string GetOrganization(IGenericADUser adUser)
		{
			if (adUser == null)
			{
				throw new ArgumentNullException("adUser");
			}
			if (adUser.OrganizationId != null && adUser.OrganizationId.OrganizationalUnit != null && adUser.OrganizationId.OrganizationalUnit.Name != null)
			{
				return adUser.OrganizationId.OrganizationalUnit.Name;
			}
			return string.Empty;
		}

		// Token: 0x0600491E RID: 18718 RVA: 0x00131E40 File Offset: 0x00130040
		private static string GetLocale(IGenericADUser adUser, List<CultureInfo> supportedLocales)
		{
			if (adUser == null)
			{
				throw new ArgumentNullException("adUser");
			}
			foreach (CultureInfo cultureInfo in adUser.Languages)
			{
				if (supportedLocales.Contains(cultureInfo))
				{
					return cultureInfo.Name;
				}
			}
			return "en-US";
		}

		// Token: 0x0600491F RID: 18719 RVA: 0x00131EB0 File Offset: 0x001300B0
		private static VariantConfigurationUserType GetUserType(IGenericADUser adUser)
		{
			if (adUser == null || adUser.OrganizationId == null)
			{
				return VariantConfigurationUserType.None;
			}
			if (!Globals.IsConsumerOrganization(adUser.OrganizationId))
			{
				return VariantConfigurationUserType.Business;
			}
			return VariantConfigurationUserType.Consumer;
		}
	}
}
