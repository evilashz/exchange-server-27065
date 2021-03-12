using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000557 RID: 1367
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class WellKnownNetworkNames
	{
		// Token: 0x060039B5 RID: 14773 RVA: 0x000EC88C File Offset: 0x000EAA8C
		static WellKnownNetworkNames()
		{
			WellKnownNetworkNames.WellKnownExternalNetworkName = new HashSet<string>
			{
				WellKnownNetworkNames.Facebook,
				WellKnownNetworkNames.LinkedIn,
				WellKnownNetworkNames.Sharepoint,
				WellKnownNetworkNames.GAL,
				WellKnownNetworkNames.QuickContacts,
				WellKnownNetworkNames.RecipientCache
			};
		}

		// Token: 0x060039B6 RID: 14774 RVA: 0x000EC933 File Offset: 0x000EAB33
		internal static bool IsWellKnownExternalNetworkName(string name)
		{
			return WellKnownNetworkNames.WellKnownExternalNetworkName.Contains(name);
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x000EC940 File Offset: 0x000EAB40
		internal static bool IsHiddenSourceNetworkName(string partnerNetworkId, string parentFolderName)
		{
			return StringComparer.OrdinalIgnoreCase.Equals(partnerNetworkId, WellKnownNetworkNames.RecipientCache) || (StringComparer.OrdinalIgnoreCase.Equals(partnerNetworkId, WellKnownNetworkNames.QuickContacts) && StringComparer.OrdinalIgnoreCase.Equals(parentFolderName, "{06967759-274D-40B2-A3EB-D7F9E73727D7}"));
		}

		// Token: 0x04001ED1 RID: 7889
		private static readonly HashSet<string> WellKnownExternalNetworkName;

		// Token: 0x04001ED2 RID: 7890
		public static readonly string Facebook = "Facebook";

		// Token: 0x04001ED3 RID: 7891
		public static readonly string LinkedIn = "LinkedIn";

		// Token: 0x04001ED4 RID: 7892
		public static readonly string Sharepoint = "Sharepoint";

		// Token: 0x04001ED5 RID: 7893
		public static readonly string GAL = "GAL";

		// Token: 0x04001ED6 RID: 7894
		public static readonly string QuickContacts = "QuickContacts";

		// Token: 0x04001ED7 RID: 7895
		public static readonly string RecipientCache = "RecipientCache";

		// Token: 0x04001ED8 RID: 7896
		public static readonly string Outlook = "Outlook";
	}
}
