using System;
using System.Management.Automation.Runspaces;
using Microsoft.Exchange.PowerShell.RbacHostingTools.Asp.Net;

namespace Microsoft.Exchange.Management.ReportingWebService.PowerShell
{
	// Token: 0x02000016 RID: 22
	internal class ReportingWebServiceInitialSessionStateFactory : RbacInitialSessionStateFactory
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00003334 File Offset: 0x00001534
		public override InitialSessionState GetInitialSessionState()
		{
			InitialSessionState state = null;
			ElapsedTimeWatcher.Watch(RequestStatistics.RequestStatItem.GetInitialSessionStateLatency, delegate
			{
				state = this.<>n__FabricatedMethod3();
			});
			return state;
		}
	}
}
