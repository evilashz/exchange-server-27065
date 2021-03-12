using System;
using Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;
using Microsoft.Online.BOX.Shell;
using Microsoft.Online.BOX.UI.Shell;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000248 RID: 584
	public class NavBarClient : NavBarClientBase
	{
		// Token: 0x0600286F RID: 10351 RVA: 0x0007F424 File Offset: 0x0007D624
		public NavBarClient(bool showAdminFeature, bool fallbackMode, bool forceReload) : base(showAdminFeature, fallbackMode, forceReload)
		{
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x0007F430 File Offset: 0x0007D630
		protected override NavBarInfoRequest CreateRequest()
		{
			return new NavBarInfoRequest
			{
				UserPuid = this.userPuid,
				UserPrincipalName = this.userPrincipalName,
				CultureName = this.cultureName,
				WorkloadId = (RbacPrincipal.Current.IsInRole("FFO") ? 7 : 1),
				CurrentMainLinkID = 5,
				TrackingGuid = Guid.NewGuid().ToString()
			};
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x0007F4A4 File Offset: 0x0007D6A4
		protected override NavBarPack TryGetNavBarPackFromCache()
		{
			NavBarPack navBarPack = NavBarCache.Get(this.userPuid, this.userPrincipalName, this.cultureName);
			if (navBarPack != null)
			{
				navBarPack.IsFresh = false;
			}
			return navBarPack;
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x0007F4D4 File Offset: 0x0007D6D4
		protected override void CallShellService(ShellServiceClient client, NavBarInfoRequest request)
		{
			this.navbarInfo = client.GetNavBarInfo(request);
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x0007F4E3 File Offset: 0x0007D6E3
		protected override NavBarPack GetMockNavBarPack()
		{
			return MockNavBar.GetMockNavBarPack(this.userPuid, this.userPrincipalName, this.cultureName, RtlUtil.IsRtl, this.isGallatin, false);
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x0007F508 File Offset: 0x0007D708
		protected override NavBarPack EndGetNavBarPack()
		{
			if (!base.LogException() && this.navbarInfo != null)
			{
				NavBarPack navBarPackFromInfo = NavBarClientBase.GetNavBarPackFromInfo(this.navbarInfo, this.cultureName);
				ExTraceGlobals.WebServiceTracer.TraceInformation(0, 0L, string.Format("NavBarData acquired. SessionId: {0}, CorrelationId: {1}", navBarPackFromInfo.NavBarData.SessionID, navBarPackFromInfo.NavBarData.CorrelationID));
				navBarPackFromInfo.IsGeminiShellEnabled = false;
				navBarPackFromInfo.IsFresh = true;
				NavBarCache.Set(this.userPuid, this.userPrincipalName, navBarPackFromInfo);
				return navBarPackFromInfo;
			}
			return null;
		}

		// Token: 0x0400205F RID: 8287
		private NavBarInfo navbarInfo;
	}
}
