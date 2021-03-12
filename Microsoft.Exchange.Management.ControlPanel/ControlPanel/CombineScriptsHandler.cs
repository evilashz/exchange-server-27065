using System;
using System.Web;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001F5 RID: 501
	public class CombineScriptsHandler : Page
	{
		// Token: 0x0600265C RID: 9820 RVA: 0x000766E4 File Offset: 0x000748E4
		public CombineScriptsHandler()
		{
			if (ToolkitScriptManager.CacheScriptBuckets == null)
			{
				ToolkitScriptManager toolkitScriptManager = new ToolkitScriptManager();
				toolkitScriptManager.SkinID = (this.Theme = "Default");
				this.Controls.Add(toolkitScriptManager);
			}
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x00076724 File Offset: 0x00074924
		public override void ProcessRequest(HttpContext context)
		{
			if (ToolkitScriptManager.CacheScriptBuckets == null)
			{
				base.ProcessRequest(context);
			}
			try
			{
				if (!ToolkitScriptManager.OutputCombineScriptResourcesFile(context))
				{
					throw new BadRequestException(new Exception("'ToolkitScriptManager' could not generate combined script resources file."));
				}
			}
			catch (Exception ex)
			{
				EcpEventLogConstants.Tuple_ScriptRequestFailed.LogPeriodicFailure(EcpEventLogExtensions.GetUserNameToLog(), context.GetRequestUrlForLog(), ex, EcpEventLogExtensions.GetFlightInfoForLog());
				throw new BadRequestException(ex);
			}
		}
	}
}
