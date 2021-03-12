using System;
using System.Web;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000205 RID: 517
	internal class GoHelpHandler : IHttpHandler
	{
		// Token: 0x17001BEA RID: 7146
		// (get) Token: 0x060026AF RID: 9903 RVA: 0x000783E8 File Offset: 0x000765E8
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x000783EC File Offset: 0x000765EC
		public void ProcessRequest(HttpContext context)
		{
			string text = context.Request.QueryString["helpid"];
			if (string.IsNullOrEmpty(text) || (!Enum.IsDefined(typeof(EACHelpId), text) && !Enum.IsDefined(typeof(OptionsHelpId), text)))
			{
				throw new BadRequestException(new Exception("Invalid HelpId: \"" + text + "\" ."));
			}
			context.Response.Redirect(HelpUtil.BuildEhcHref(text));
		}
	}
}
