using System;
using System.Web;
using Microsoft.Online.BOX.UI.Shell;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200024A RID: 586
	public class NavBarDiagnostics : DiagnosticsPage
	{
		// Token: 0x0600287C RID: 10364 RVA: 0x0007F844 File Offset: 0x0007DA44
		protected void RenderNavBarDetails()
		{
			base.Response.Write("<div class='diagBlock'>");
			NavBarDiagnosticsDetails navBarDiagnosticsDetails = new NavBarDiagnosticsDetails();
			HttpContext.Current.Cache["NavBarDiagnostics.Details"] = navBarDiagnosticsDetails;
			base.Write("Use the information below to troubleshoot problems and report issues to technical support.");
			base.Write("NavBar configuration information");
			NavBarClientBase.RenderConfigInformation(base.Response.Output);
			Identity identity = (base.Request.QueryString["flight"] == "geminishellux") ? new Identity("myself", "myself") : new Identity("myorg", "myorg");
			Exception ex = null;
			try
			{
				PowerShellResults<NavBarPack> @object = new NavBar().GetObject(identity);
				base.Write("NavBar data:", @object.HasValue ? @object.Output[0].ToJsonString(null) : null);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			finally
			{
				if (navBarDiagnosticsDetails.Exception != null)
				{
					base.Write("Exception:", navBarDiagnosticsDetails.Exception.ToString());
				}
				if (ex != null)
				{
					base.Write("Additional Exception:", ex.ToString());
				}
				base.Response.Write("</div>");
			}
		}

		// Token: 0x04002065 RID: 8293
		internal const string DetailsKey = "NavBarDiagnostics.Details";
	}
}
