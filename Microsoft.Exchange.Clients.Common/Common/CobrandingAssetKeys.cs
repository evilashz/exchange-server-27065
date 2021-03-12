using System;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x02000015 RID: 21
	internal static class CobrandingAssetKeys
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00006A80 File Offset: 0x00004C80
		public static string GetAssetKeyString(CobrandingAssetKey assetKey)
		{
			return CobrandingAssetKeys.CobrandingAssetKeyMap[(int)assetKey];
		}

		// Token: 0x04000217 RID: 535
		private static readonly string[] CobrandingAssetKeyMap = new string[]
		{
			"Exchange.Identity.OrganizationName",
			"Exchange.Identity.LogoR3.Path",
			"Exchange.Identity.LogoR3.AltText",
			"Exchange.Navigation.Signout.URL",
			"Exchange.Theme.EnableCustomTheme",
			"Exchange.Theme.Application.HoverColor",
			"Exchange.Theme.Content.SignOutColor",
			"Exchange.Theme.BrandBarR3.TextColor",
			"Exchange.Theme.Content.PrimaryLinkColor",
			"Exchange.Theme.Application.SelectedBorderColor",
			"Exchange.Theme.ActiveText.Hex",
			"Exchange.Theme.BrandBarR3.Path",
			"Exchange.Theme.BrandBarBackgroundImageR3.Path"
		};
	}
}
