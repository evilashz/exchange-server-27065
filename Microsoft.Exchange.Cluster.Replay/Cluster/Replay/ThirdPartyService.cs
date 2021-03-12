using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Security.Principal;
using System.ServiceModel;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.ThirdPartyReplication;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000382 RID: 898
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
	internal class ThirdPartyService : IInternalRequest
	{
		// Token: 0x06002429 RID: 9257 RVA: 0x000A90D4 File Offset: 0x000A72D4
		public static ThirdPartyService StartListening(out Exception exception)
		{
			ExTraceGlobals.ThirdPartyServiceTracer.TraceDebug(0L, "Starting listener");
			exception = null;
			ThirdPartyService thirdPartyService = null;
			try
			{
				thirdPartyService = new ThirdPartyService();
				EndpointAddress endpointAddress = new EndpointAddress("net.pipe://localhost/Microsoft.Exchange.ThirdPartyReplication.RequestService");
				thirdPartyService.m_host = new ServiceHost(thirdPartyService, new Uri[]
				{
					endpointAddress.Uri
				});
				thirdPartyService.m_host.AddServiceEndpoint(typeof(IInternalRequest), new NetNamedPipeBinding(), string.Empty);
				thirdPartyService.m_host.Open();
				ReplayEventLogConstants.Tuple_TPRExchangeListenerStarted.LogEvent(null, new object[0]);
				ReplayCrimsonEvents.TPRExchangeListenerStarted.Log();
				ExTraceGlobals.ThirdPartyServiceTracer.TraceDebug(0L, "Started successfully");
				return thirdPartyService;
			}
			catch (CommunicationException ex)
			{
				exception = ex;
			}
			catch (ConfigurationException ex2)
			{
				exception = ex2;
			}
			catch (InvalidOperationException ex3)
			{
				exception = ex3;
			}
			ReplayCrimsonEvents.TPRExchangeListenerFailedToStart.Log<string>(exception.ToString());
			ExTraceGlobals.ThirdPartyServiceTracer.TraceError<string, Exception>(0L, "Failed to start listener: {0} {1}", exception.Message, exception);
			if (thirdPartyService != null && thirdPartyService.m_host != null)
			{
				thirdPartyService.m_host.Abort();
			}
			return null;
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x000A9228 File Offset: 0x000A7428
		public string GetPrimaryActiveManager(out byte[] exBytes)
		{
			ExTraceGlobals.ThirdPartyServiceTracer.TraceDebug(0L, "GetPrimaryActiveManager called");
			exBytes = null;
			string pam = null;
			Exception ex = this.DoAction(delegate(object param0, EventArgs param1)
			{
				ThirdPartyService.CheckSecurity("GetPrimaryActiveManager");
				pam = ThirdPartyManager.GetPrimaryActiveManager();
			});
			if (ex != null)
			{
				ExTraceGlobals.ThirdPartyServiceTracer.TraceError<Exception>((long)this.GetHashCode(), "GetPrimaryActiveManager fails: {0}", ex);
				if (!(ex is ThirdPartyReplicationException))
				{
					ex = new GetPrimaryActiveManagerException(ex.Message);
				}
				exBytes = this.SerializeException(ex);
			}
			return pam;
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x000A92D0 File Offset: 0x000A74D0
		public byte[] ChangeActiveServer(Guid databaseId, string newActiveServerName)
		{
			ExTraceGlobals.ThirdPartyServiceTracer.TraceDebug<Guid, string>(0L, "ChangeActiveServer:db {0} moving to {1}", databaseId, newActiveServerName);
			Exception ex = this.DoAction(delegate(object param0, EventArgs param1)
			{
				ThirdPartyService.CheckSecurity("ChangeActiveServer");
				ThirdPartyManager.Instance.ChangeActiveServer(databaseId, newActiveServerName);
			});
			if (ex != null)
			{
				ReplayCrimsonEvents.TPRChangeActiveServerFailed.Log<Guid, string, string, string>(databaseId, newActiveServerName, ex.Message, ex.StackTrace);
				if (!(ex is ThirdPartyReplicationException))
				{
					ex = new ChangeActiveServerException(databaseId, newActiveServerName, ex.Message);
				}
				return this.SerializeException(ex);
			}
			return null;
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x000A9394 File Offset: 0x000A7594
		public byte[] ImmediateDismountMailboxDatabase(Guid databaseId)
		{
			ExTraceGlobals.ThirdPartyServiceTracer.TraceDebug<Guid>(0L, "ImmediateDismountMailboxDatabase:db {0}", databaseId);
			Exception ex = this.DoAction(delegate(object param0, EventArgs param1)
			{
				ThirdPartyService.CheckSecurity("ImmediateDismountMailboxDatabase");
				ThirdPartyManager.Instance.ImmediateDismountMailboxDatabase(databaseId);
			});
			if (ex != null)
			{
				ReplayCrimsonEvents.TPRImmediateDismountFailed.Log<Guid, string, string>(databaseId, ex.Message, ex.StackTrace);
				if (!(ex is ThirdPartyReplicationException))
				{
					ex = new ImmediateDismountMailboxDatabaseException(databaseId, ex.Message);
				}
				return this.SerializeException(ex);
			}
			return null;
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x000A9450 File Offset: 0x000A7650
		public void AmeIsStarting(TimeSpan retryDelay, TimeSpan openTimeout, TimeSpan sendTimeout, TimeSpan receiveTimeout)
		{
			this.DoAction(delegate(object param0, EventArgs param1)
			{
				ThirdPartyService.CheckSecurity("AmeIsStarting");
				ThirdPartyManager.Instance.AmeIsStarting(retryDelay, openTimeout, sendTimeout, receiveTimeout);
			});
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x000A94A9 File Offset: 0x000A76A9
		public void AmeIsStopping()
		{
			this.DoAction(delegate(object param0, EventArgs param1)
			{
				ThirdPartyService.CheckSecurity("AmeIsStopping");
				ThirdPartyManager.Instance.AmeIsStopping();
			});
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x000A94CF File Offset: 0x000A76CF
		public void StopListening()
		{
			ReplayCrimsonEvents.TPRExchangeListenerStopped.Log();
			this.m_host.Close();
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x000A94E8 File Offset: 0x000A76E8
		private static bool AuthorizeRequest(WindowsIdentity wid)
		{
			IdentityReferenceCollection groups = wid.Groups;
			foreach (IdentityReference left in groups)
			{
				if (left == ThirdPartyService.s_localAdminsSid)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x000A9544 File Offset: 0x000A7744
		private static void CheckSecurity(string methodName)
		{
			ExTraceGlobals.ThirdPartyServiceTracer.TraceDebug<string, string>(0L, "Received '{0}' from '{1}'", methodName, ServiceSecurityContext.Current.PrimaryIdentity.Name);
			WindowsIdentity windowsIdentity = ServiceSecurityContext.Current.PrimaryIdentity as WindowsIdentity;
			if (windowsIdentity == null)
			{
				ExTraceGlobals.ThirdPartyServiceTracer.TraceError(0L, "Not a windows Identity");
				throw new NotAuthorizedException();
			}
			if (!ThirdPartyService.AuthorizeRequest(windowsIdentity))
			{
				ExTraceGlobals.ThirdPartyServiceTracer.TraceError(0L, "Caller in not Local admin");
				if (!ThirdPartyService.s_authFailedLogged)
				{
					ThirdPartyService.s_authFailedLogged = true;
					ReplayCrimsonEvents.TPRAuthorizationFailed.Log<string>(ServiceSecurityContext.Current.PrimaryIdentity.Name);
				}
				throw new NotAuthorizedException();
			}
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x000A95E4 File Offset: 0x000A77E4
		private Exception DoAction(EventHandler ev)
		{
			Exception ex = null;
			try
			{
				ev(null, null);
			}
			catch (ThirdPartyReplicationException ex2)
			{
				ex = ex2;
			}
			catch (ADTransientException ex3)
			{
				ex = ex3;
			}
			catch (ADExternalException ex4)
			{
				ex = ex4;
			}
			catch (ADOperationException ex5)
			{
				ex = ex5;
			}
			catch (AmServiceShuttingDownException ex6)
			{
				ex = ex6;
			}
			catch (MapiPermanentException ex7)
			{
				ex = ex7;
			}
			catch (MapiRetryableException ex8)
			{
				ex = ex8;
			}
			catch (DataSourceTransientException ex9)
			{
				ex = ex9;
			}
			catch (DataSourceOperationException ex10)
			{
				ex = ex10;
			}
			catch (AmCommonException ex11)
			{
				ex = ex11;
			}
			catch (AmServerException ex12)
			{
				ex = ex12;
			}
			catch (ClusterException ex13)
			{
				ex = ex13;
			}
			catch (TransientException ex14)
			{
				ex = ex14;
			}
			catch (Win32Exception ex15)
			{
				ex = ex15;
			}
			if (ex != null)
			{
				StackFrame stackFrame = new StackFrame(1);
				ExTraceGlobals.ThirdPartyServiceTracer.TraceError<string, string, Exception>(0L, "DoAction({0}) fails: {1}, {2}", stackFrame.GetMethod().Name, ex.Message, ex);
			}
			return ex;
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x000A9738 File Offset: 0x000A7938
		private byte[] SerializeException(Exception exception)
		{
			return Serialization.ObjectToBytes(exception);
		}

		// Token: 0x04000F63 RID: 3939
		private static SecurityIdentifier s_localAdminsSid = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);

		// Token: 0x04000F64 RID: 3940
		private static bool s_authFailedLogged = false;

		// Token: 0x04000F65 RID: 3941
		private ServiceHost m_host;
	}
}
