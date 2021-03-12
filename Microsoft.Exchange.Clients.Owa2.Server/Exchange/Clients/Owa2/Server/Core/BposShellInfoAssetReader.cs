using System;
using System.Globalization;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Online.BOX.Shell;
using Microsoft.Online.BOX.UI.Shell;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200008A RID: 138
	internal class BposShellInfoAssetReader : BposAssetReader<BposShellInfo>
	{
		// Token: 0x06000526 RID: 1318 RVA: 0x0000EA40 File Offset: 0x0000CC40
		internal BposShellInfoAssetReader(string userPrincipalName, CultureInfo culture, BposHeaderFlight currentHeaderFlight, UserContext userContext) : base(userPrincipalName, culture)
		{
			this.currentHeaderFlight = currentHeaderFlight;
			this.userContext = userContext;
			this.isGemini = (this.currentHeaderFlight == BposHeaderFlight.E16Gemini1 || this.currentHeaderFlight == BposHeaderFlight.E16Gemini2);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000EA74 File Offset: 0x0000CC74
		protected override BposShellInfo ExecuteRequest(ShellServiceClient client, string cultureName, string userPrincipalName, string userPuid, AuthZClientInfo effectiveCaller, string trackingGuid)
		{
			ShellBaseFlight value = ShellBaseFlight.V15Parity;
			if (this.currentHeaderFlight == BposHeaderFlight.E16Gemini1)
			{
				value = ShellBaseFlight.V16;
			}
			else if (this.currentHeaderFlight == BposHeaderFlight.E16Gemini2)
			{
				value = ShellBaseFlight.V16G2;
			}
			ShellInfoRequest shellInfoRequest = new ShellInfoRequest
			{
				BrandId = null,
				CultureName = cultureName,
				CurrentMainLinkID = NavBarMainLinkID.Outlook,
				UserPrincipalName = userPrincipalName,
				UserPuid = userPuid,
				WorkloadId = WorkloadAuthenticationId.Exchange,
				TrackingGuid = trackingGuid,
				ShellBaseFlight = new ShellBaseFlight?(value),
				UserThemeId = (this.isGemini ? this.userContext.Theme.FolderName : null)
			};
			shellInfoRequest.UserThemeId = null;
			ShellInfo shellInfo = client.GetShellInfo(shellInfoRequest);
			return this.CreateBposShellInfo(shellInfo, effectiveCaller);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000EB1C File Offset: 0x0000CD1C
		private BposShellInfo CreateBposShellInfo(ShellInfo info, AuthZClientInfo effectiveCaller)
		{
			if (info == null)
			{
				return null;
			}
			try
			{
				if (base.ShouldUpdateCache(info.Version))
				{
					NavBarData data = base.CreateNavBarData(info.NavBarDataJson);
					base.UpdateCachedAssets(new BposShellInfo(info.Version, data, info.SuiteServiceProxyOriginAllowedList, info.SuiteServiceProxyScriptUrl));
				}
			}
			catch (Exception)
			{
			}
			NavBarData navBarData = base.CreateNavBarData(info.NavBarDataJson);
			base.UpdateAppsLinks(navBarData, effectiveCaller);
			return new BposShellInfo(info.Version, navBarData, info.SuiteServiceProxyOriginAllowedList, info.SuiteServiceProxyScriptUrl);
		}

		// Token: 0x040002B2 RID: 690
		private readonly BposHeaderFlight currentHeaderFlight;

		// Token: 0x040002B3 RID: 691
		private readonly bool isGemini;

		// Token: 0x040002B4 RID: 692
		private readonly UserContext userContext;
	}
}
