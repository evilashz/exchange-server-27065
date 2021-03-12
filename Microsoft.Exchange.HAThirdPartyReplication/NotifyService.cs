using System;
using System.Diagnostics;
using System.Security.Principal;
using System.ServiceModel;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.ThirdPartyReplication
{
	// Token: 0x0200000F RID: 15
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
	internal class NotifyService : IInternalNotify
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002A64 File Offset: 0x00000C64
		public NotifyService(INotifyCallback callback, NotificationListener listener)
		{
			this.m_callback = callback;
			this.m_listener = listener;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002A7A File Offset: 0x00000C7A
		public void BecomePame()
		{
			if (!NotifyService.CheckSecurity())
			{
				return;
			}
			ReplayCrimsonEvents.TPRNotificationListenerReceivedBecomePame.Log();
			this.m_callback.BecomePame();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002A99 File Offset: 0x00000C99
		public void RevokePame()
		{
			if (!NotifyService.CheckSecurity())
			{
				return;
			}
			ReplayCrimsonEvents.TPRNotificationListenerReceivedRevokePame.Log();
			this.m_callback.RevokePame();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public NotificationResponse DatabaseMoveNeeded(Guid dbId, string currentActiveFqdn, bool mountDesired)
		{
			if (!NotifyService.CheckSecurity())
			{
				return NotificationResponse.Incomplete;
			}
			ReplayCrimsonEvents.TPRNotificationListenerReceivedDatabaseMoveNeeded.Log<Guid, string, bool>(dbId, currentActiveFqdn, mountDesired);
			NotificationResponse notificationResponse = this.m_callback.DatabaseMoveNeeded(dbId, currentActiveFqdn, mountDesired);
			ExTraceGlobals.ThirdPartyClientTracer.TraceDebug(0L, "NotificationListener is responded with {0} to DatabaseMoveNeeded({1},{2},{3})", new object[]
			{
				notificationResponse,
				dbId,
				currentActiveFqdn,
				mountDesired
			});
			return notificationResponse;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B24 File Offset: 0x00000D24
		public int GetTimeouts(out TimeSpan retryDelay, out TimeSpan openTimeout, out TimeSpan sendTimeout, out TimeSpan receiveTimeout)
		{
			ExTraceGlobals.ThirdPartyClientTracer.TraceDebug(0L, "NotificationListener is responding to GetTimeouts");
			retryDelay = this.m_listener.RetryDelay;
			openTimeout = this.m_listener.OpenTimeout;
			sendTimeout = this.m_listener.SendTimeout;
			receiveTimeout = this.m_listener.ReceiveTimeout;
			return 0;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B88 File Offset: 0x00000D88
		private static bool AuthorizeRequest()
		{
			WindowsIdentity windowsIdentity = ServiceSecurityContext.Current.PrimaryIdentity as WindowsIdentity;
			if (windowsIdentity == null)
			{
				return false;
			}
			IdentityReferenceCollection groups = windowsIdentity.Groups;
			foreach (IdentityReference left in groups)
			{
				if (left == NotifyService.s_localAdminsSid)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002C00 File Offset: 0x00000E00
		private static bool CheckSecurity()
		{
			if (!NotifyService.AuthorizeRequest())
			{
				string name = ServiceSecurityContext.Current.PrimaryIdentity.Name;
				StackFrame stackFrame = new StackFrame(1);
				string name2 = stackFrame.GetMethod().Name;
				ExTraceGlobals.ThirdPartyClientTracer.TraceError<string, string>(0L, "Access denied to user '{0}'. MethodCalled='{1}'", name, name2);
				if (!NotifyService.s_authFailedLogged)
				{
					NotifyService.s_authFailedLogged = true;
					ReplayCrimsonEvents.TPRAuthorizationFailed.Log<string>(ServiceSecurityContext.Current.PrimaryIdentity.Name);
				}
				return false;
			}
			return true;
		}

		// Token: 0x0400000C RID: 12
		private static SecurityIdentifier s_localAdminsSid = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);

		// Token: 0x0400000D RID: 13
		private static bool s_authFailedLogged = false;

		// Token: 0x0400000E RID: 14
		private INotifyCallback m_callback;

		// Token: 0x0400000F RID: 15
		private NotificationListener m_listener;
	}
}
