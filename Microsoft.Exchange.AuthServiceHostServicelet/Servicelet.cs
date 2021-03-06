using System;
using System.ServiceModel;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authentication.FederatedAuthService;
using Microsoft.Exchange.ServiceHost;

namespace Microsoft.Exchange.Servicelets.AuthServiceHost
{
	// Token: 0x02000002 RID: 2
	public class Servicelet : Servicelet
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		public override void Work()
		{
			ExEventLog eventLogger = AuthService.EventLogger;
			bool flag = false;
			ServiceHost serviceHost = null;
			eventLogger.LogEvent(SecurityEventLogConstants.Tuple_AuthServiceStarting, "LiveIdServerStarting", null);
			try
			{
				serviceHost = new ServiceHost(typeof(AuthService), new Uri[0]);
				try
				{
					serviceHost.Open();
					flag = true;
					eventLogger.LogEvent(SecurityEventLogConstants.Tuple_AuthServiceStarted, "LiveIdServerStarted", null);
				}
				catch (AddressAlreadyInUseException ex)
				{
					eventLogger.LogEvent(SecurityEventLogConstants.Tuple_AuthServiceFailedToRegisterEndpoint, "AuthServiceFailedToStart", new object[]
					{
						ex.Message
					});
				}
				base.StopEvent.WaitOne();
			}
			finally
			{
				if (flag)
				{
					serviceHost.Abort();
					eventLogger.LogEvent(SecurityEventLogConstants.Tuple_AuthServiceStopped, "AuthServiceStopped", null);
				}
				if (serviceHost != null)
				{
					serviceHost.Close();
					serviceHost = null;
				}
			}
		}
	}
}
