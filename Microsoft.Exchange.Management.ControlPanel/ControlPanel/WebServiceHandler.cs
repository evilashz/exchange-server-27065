using System;
using System.Security;
using System.Web;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006EB RID: 1771
	internal class WebServiceHandler : IHttpHandler
	{
		// Token: 0x06004A75 RID: 19061 RVA: 0x000E3AA6 File Offset: 0x000E1CA6
		[SecurityCritical]
		public void ProcessRequest(HttpContext context)
		{
			this.actualHandler.ProcessRequest(context);
		}

		// Token: 0x17002829 RID: 10281
		// (get) Token: 0x06004A76 RID: 19062 RVA: 0x000E3AB4 File Offset: 0x000E1CB4
		public bool IsReusable
		{
			get
			{
				return this.actualHandler.IsReusable;
			}
		}

		// Token: 0x040031AC RID: 12716
		private IHttpHandler actualHandler = (IHttpHandler)Activator.CreateInstance("System.ServiceModel.Activation, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.ServiceModel.Activation.HttpHandler").Unwrap();
	}
}
