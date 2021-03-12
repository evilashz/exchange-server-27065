using System;
using System.Data.Services;
using System.ServiceModel;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.ReportingWebService;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000027 RID: 39
	public class RbacAuthorizationManager : ServiceAuthorizationManager
	{
		// Token: 0x060000CE RID: 206 RVA: 0x000040CC File Offset: 0x000022CC
		protected override bool CheckAccessCore(OperationContext operationContext)
		{
			if (RbacAuthorizationManager.NewAuthZMethodEnabled.Value)
			{
				if (HttpContext.Current.Items.Contains(RbacAuthorizationManager.DataServiceExceptionKey))
				{
					throw (DataServiceException)HttpContext.Current.Items[RbacAuthorizationManager.DataServiceExceptionKey];
				}
				operationContext.SetRbacPrincipal((RbacPrincipal)HttpContext.Current.User);
				return base.CheckAccessCore(operationContext);
			}
			else
			{
				RbacPrincipal rbacPrincipal = null;
				ElapsedTimeWatcher.Watch(RequestStatistics.RequestStatItem.RbacPrincipalAcquireLatency, delegate
				{
					rbacPrincipal = RbacPrincipalManager.Instance.AcquireRbacPrincipal(HttpContext.Current);
				});
				if (rbacPrincipal == null)
				{
					return false;
				}
				HttpContext.Current.User = rbacPrincipal;
				rbacPrincipal.SetCurrentThreadPrincipal();
				if (OperationContext.Current != null)
				{
					OperationContext.Current.SetRbacPrincipal(rbacPrincipal);
				}
				return base.CheckAccessCore(operationContext);
			}
		}

		// Token: 0x04000055 RID: 85
		public static readonly string DataServiceExceptionKey = "DataServiceExceptionKey";

		// Token: 0x04000056 RID: 86
		private static readonly BoolAppSettingsEntry NewAuthZMethodEnabled = new BoolAppSettingsEntry("NewAuthZMethodEnabled", false, ExTraceGlobals.ReportingWebServiceTracer);
	}
}
