using System;
using System.Configuration;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;
using Microsoft.Online.BOX.Shell;
using Microsoft.Online.BOX.UI.Shell;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200024C RID: 588
	public class ShellClient : NavBarClientBase
	{
		// Token: 0x06002881 RID: 10369 RVA: 0x0007F9A8 File Offset: 0x0007DBA8
		static ShellClient()
		{
			string text = ConfigurationManager.AppSettings["ShellServiceTimeout"];
			if (string.IsNullOrEmpty(text) || !int.TryParse(text, out ShellClient.shellServiceTimeout))
			{
				ShellClient.shellServiceTimeout = 2000;
			}
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x0007F9E4 File Offset: 0x0007DBE4
		public ShellClient(bool showAdminFeature, bool fallbackMode, bool forceReload) : base(showAdminFeature, fallbackMode, forceReload)
		{
		}

		// Token: 0x17001C5B RID: 7259
		// (get) Token: 0x06002883 RID: 10371 RVA: 0x0007F9EF File Offset: 0x0007DBEF
		protected override bool UseNavBarPackCache
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C5C RID: 7260
		// (get) Token: 0x06002884 RID: 10372 RVA: 0x0007F9F2 File Offset: 0x0007DBF2
		protected override string Office365Copyright
		{
			get
			{
				return HttpUtility.HtmlDecode(Strings.Office365Copyright);
			}
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x0007FA04 File Offset: 0x0007DC04
		protected override NavBarInfoRequest CreateRequest()
		{
			return new ShellInfoRequest
			{
				UserPuid = this.userPuid,
				UserPrincipalName = this.userPrincipalName,
				CultureName = this.cultureName,
				WorkloadId = (RbacPrincipal.Current.IsInRole("FFO") ? 7 : 1),
				CurrentMainLinkID = 5,
				TrackingGuid = Guid.NewGuid().ToString(),
				BrandId = null,
				ExcludeMSAjax = true,
				ShellBaseFlight = null
			};
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x0007FAB8 File Offset: 0x0007DCB8
		protected override void BeginGetNavBarPack(ShellServiceClient client, NavBarInfoRequest request)
		{
			string value = HttpContext.Current.Request.QueryString["flight"];
			if (!string.IsNullOrEmpty(value))
			{
				EndpointAddress address = client.Endpoint.Address;
				Uri uri = new Uri(EcpUrl.AppendQueryParameter(address.Uri.AbsoluteUri, "flight", value));
				client.Endpoint.Address = new EndpointAddress(uri, address.Identity, new AddressHeader[0]);
			}
			this.stopwatch = new Stopwatch();
			this.stopwatch.Start();
			this.shellServiceTask = new Task(delegate()
			{
				this.GetNavBarInfoAndHandleException(client, request);
			});
			this.shellServiceTask.Start();
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x0007FB8C File Offset: 0x0007DD8C
		protected override void CallShellService(ShellServiceClient client, NavBarInfoRequest request)
		{
			this.shellInfo = client.GetShellInfo((ShellInfoRequest)request);
			this.stopwatch.Stop();
			ExTraceGlobals.WebServiceTracer.TraceInformation(0, 0L, string.Format("Successfully called shell service in {0}ms", this.stopwatch.ElapsedMilliseconds));
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x0007FBDD File Offset: 0x0007DDDD
		protected override NavBarPack GetMockNavBarPack()
		{
			return MockNavBar.GetMockNavBarPack(this.userPuid, this.userPrincipalName, this.cultureName, RtlUtil.IsRtl, this.isGallatin, true);
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x0007FC04 File Offset: 0x0007DE04
		protected override NavBarPack EndGetNavBarPack()
		{
			ShellInfo shellInfo = null;
			if (this.shellServiceTask != null)
			{
				if (this.shellServiceTask.Wait(ShellClient.shellServiceTimeout))
				{
					if (!base.LogException())
					{
						shellInfo = this.shellInfo;
					}
				}
				else
				{
					this.stopwatch.Stop();
					ExTraceGlobals.WebServiceTracer.TraceInformation(0, 0L, string.Format("Timeout when calling Shell Service after {0}ms", this.stopwatch.ElapsedMilliseconds));
					EcpEventLogConstants.Tuple_Office365NavBarCallServiceTimeout.LogEvent(new object[0]);
				}
			}
			if (shellInfo != null)
			{
				NavBarPack navBarPackFromInfo = NavBarClientBase.GetNavBarPackFromInfo(shellInfo, this.cultureName);
				ExTraceGlobals.WebServiceTracer.TraceInformation(0, 0L, string.Format("NavBarData acquired. SessionId: {0}, CorrelationId: {1}", navBarPackFromInfo.NavBarData.SessionID, navBarPackFromInfo.NavBarData.CorrelationID));
				navBarPackFromInfo.IsGeminiShellEnabled = true;
				navBarPackFromInfo.IsFresh = true;
				return navBarPackFromInfo;
			}
			return null;
		}

		// Token: 0x04002067 RID: 8295
		private const int ShellServiceTimeoutDefault = 2000;

		// Token: 0x04002068 RID: 8296
		private static int shellServiceTimeout;

		// Token: 0x04002069 RID: 8297
		private Task shellServiceTask;

		// Token: 0x0400206A RID: 8298
		private ShellInfo shellInfo;

		// Token: 0x0400206B RID: 8299
		private Stopwatch stopwatch;
	}
}
