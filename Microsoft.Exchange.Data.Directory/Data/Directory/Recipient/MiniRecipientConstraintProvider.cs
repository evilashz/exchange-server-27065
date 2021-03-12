using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000285 RID: 645
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MiniRecipientConstraintProvider : UserConstraintProvider
	{
		// Token: 0x06001EA6 RID: 7846 RVA: 0x0008965D File Offset: 0x0008785D
		public MiniRecipientConstraintProvider(MiniRecipient recipient, string rampId, bool isFirstRelease) : this(recipient, rampId, isFirstRelease, UserConstraintProvider.SupportedLocales)
		{
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x00089670 File Offset: 0x00087870
		internal MiniRecipientConstraintProvider(MiniRecipient recipient, string rampId, bool isFirstRelease, List<CultureInfo> supportedLocales) : base(MiniRecipientConstraintProvider.GetUserName(recipient), MiniRecipientConstraintProvider.GetOrganization(recipient), MiniRecipientConstraintProvider.GetLocale(recipient, supportedLocales), rampId, isFirstRelease, recipient.ReleaseTrack == ReleaseTrack.Preview, MiniRecipientConstraintProvider.GetUserType(recipient))
		{
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x000896BA File Offset: 0x000878BA
		private static VariantConfigurationUserType GetUserType(MiniRecipient recipient)
		{
			if (!(recipient.OrganizationId != null))
			{
				return VariantConfigurationUserType.None;
			}
			if (!recipient.IsConsumerOrganization())
			{
				return VariantConfigurationUserType.Business;
			}
			return VariantConfigurationUserType.Consumer;
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x000896D8 File Offset: 0x000878D8
		private static string GetLocale(MiniRecipient recipient, List<CultureInfo> supportedLocales)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			foreach (CultureInfo cultureInfo in recipient.Languages)
			{
				if (supportedLocales.Contains(cultureInfo))
				{
					return cultureInfo.Name;
				}
			}
			return "en-US";
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x0008974C File Offset: 0x0008794C
		private static string GetOrganization(MiniRecipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			if (recipient.OrganizationId != null && recipient.OrganizationId.OrganizationalUnit != null)
			{
				return recipient.OrganizationId.OrganizationalUnit.Name;
			}
			return string.Empty;
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x00089798 File Offset: 0x00087998
		private static string GetUserName(MiniRecipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			if (recipient.UserPrincipalName != null)
			{
				return recipient.UserPrincipalName.Split(new char[]
				{
					'@'
				})[0];
			}
			return string.Empty;
		}
	}
}
