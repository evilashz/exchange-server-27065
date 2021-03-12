using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Diagnostics.Components.Common;
using Microsoft.Exchange.VariantConfiguration.Settings;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x0200013D RID: 317
	public abstract class UserConstraintProvider : IConstraintProvider
	{
		// Token: 0x06000EA1 RID: 3745 RVA: 0x00023A60 File Offset: 0x00021C60
		protected UserConstraintProvider(string userName, string organization, string locale, string rampId, bool isFirstRelease, bool isPreview, VariantConfigurationUserType userType)
		{
			if (userName == null)
			{
				throw new ArgumentNullException("userName");
			}
			if (organization == null)
			{
				throw new ArgumentNullException("organization");
			}
			if (locale == null)
			{
				throw new ArgumentNullException("locale");
			}
			if (rampId == null)
			{
				throw new ArgumentNullException("rampId");
			}
			this.constraints = ConstraintCollection.CreateGlobal();
			ExTraceGlobals.VariantConfigurationTracer.TraceDebug(0L, "User: {0}; Tenant: {1}; Locale: {2}; rampId: {3}; isFirstRelease: {4};", new object[]
			{
				userName,
				organization,
				locale,
				rampId,
				isFirstRelease
			});
			this.rampId = rampId;
			IOrganizationNameSettings microsoft = VariantConfiguration.InvariantNoFlightingSnapshot.VariantConfig.Microsoft;
			string item = organization.ToLowerInvariant();
			string userName2 = userName;
			if (microsoft.OrgNames.Contains(item))
			{
				organization = "Microsoft";
			}
			else
			{
				userName2 = this.FormatUserNameAndOrganization(userName, organization);
			}
			this.rotationId = ((!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(organization)) ? this.FormatUserNameAndOrganization(userName, organization) : string.Empty);
			bool isDogfood = microsoft.DogfoodOrgNames.Contains(item);
			this.PopulateConstraints(userName2, organization, locale, isDogfood, isFirstRelease, isPreview, userType);
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x00023B79 File Offset: 0x00021D79
		public static List<CultureInfo> SupportedLocales
		{
			get
			{
				if (UserConstraintProvider.supportedLocales == null)
				{
					UserConstraintProvider.supportedLocales = new List<CultureInfo>(LanguagePackInfo.GetInstalledLanguagePackSpecificCultures(LanguagePackType.Client));
				}
				return UserConstraintProvider.supportedLocales;
			}
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x00023B97 File Offset: 0x00021D97
		public ConstraintCollection Constraints
		{
			get
			{
				return this.constraints;
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x00023B9F File Offset: 0x00021D9F
		public string RampId
		{
			get
			{
				return this.rampId;
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x00023BA7 File Offset: 0x00021DA7
		public string RotationId
		{
			get
			{
				return this.rotationId;
			}
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x00023BB0 File Offset: 0x00021DB0
		private void PopulateConstraints(string userName, string organization, string locale, bool isDogfood, bool isFirstRelease, bool isPreview, VariantConfigurationUserType userType)
		{
			if (!userName.Equals(string.Empty))
			{
				this.constraints.Add(VariantType.User, userName);
			}
			this.constraints.Add(VariantType.Dogfood, isDogfood);
			if (!organization.Equals(string.Empty))
			{
				this.constraints.Add(VariantType.Organization, organization);
			}
			if (!locale.Equals(string.Empty))
			{
				this.constraints.Add(VariantType.Locale, locale);
			}
			if (userType != VariantConfigurationUserType.None)
			{
				this.constraints.Add(VariantType.UserType, userType.ToString());
			}
			this.constraints.Add(VariantType.FirstRelease, isFirstRelease);
			this.constraints.Add(VariantType.Preview, isPreview);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00023C6D File Offset: 0x00021E6D
		private string FormatUserNameAndOrganization(string userName, string organization)
		{
			return string.Format("{0}@{1}", userName, organization);
		}

		// Token: 0x040004B1 RID: 1201
		public const string DefaultLocale = "en-US";

		// Token: 0x040004B2 RID: 1202
		private const string MicrosoftOrganizationName = "Microsoft";

		// Token: 0x040004B3 RID: 1203
		private static List<CultureInfo> supportedLocales;

		// Token: 0x040004B4 RID: 1204
		private readonly ConstraintCollection constraints;

		// Token: 0x040004B5 RID: 1205
		private readonly string rampId;

		// Token: 0x040004B6 RID: 1206
		private readonly string rotationId;
	}
}
