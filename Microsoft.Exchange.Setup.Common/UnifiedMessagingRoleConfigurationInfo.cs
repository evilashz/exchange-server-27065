using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200005D RID: 93
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UnifiedMessagingRoleConfigurationInfo : InstallableUnitConfigurationInfo
	{
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0000FCFD File Offset: 0x0000DEFD
		public override string Name
		{
			get
			{
				return "UnifiedMessagingRole";
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0000FD04 File Offset: 0x0000DF04
		public override LocalizedString DisplayName
		{
			get
			{
				return Strings.UnifiedMessagingRoleDisplayName;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0000FD0C File Offset: 0x0000DF0C
		public override decimal Size
		{
			get
			{
				decimal num = RequiredDiskSpaceStatistics.UnifiedMessagingRole + UmLanguagePackConfigurationInfo.GetUmLanguagePackSizeForCultureInfo(UnifiedMessagingRoleConfigurationInfo.ExchangeCultureForEnUs);
				foreach (CultureInfo umlang in this.SelectedCultures)
				{
					num += UmLanguagePackConfigurationInfo.GetUmLanguagePackSizeForCultureInfo(umlang);
				}
				return num;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0000FD7C File Offset: 0x0000DF7C
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x0000FD88 File Offset: 0x0000DF88
		public List<CultureInfo> SelectedCultures
		{
			get
			{
				return InstallableUnitConfigurationInfo.SetupContext.SelectedCultures;
			}
			set
			{
				InstallableUnitConfigurationInfo.SetupContext.SelectedCultures = value;
			}
		}

		// Token: 0x0400018E RID: 398
		private static CultureInfo ExchangeCultureForEnUs = CultureInfo.CreateSpecificCulture("en-us");
	}
}
