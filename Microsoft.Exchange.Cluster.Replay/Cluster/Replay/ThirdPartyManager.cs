using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.ThirdPartyReplication;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000380 RID: 896
	internal class ThirdPartyManager : IServiceComponent
	{
		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x060023FA RID: 9210 RVA: 0x000A810C File Offset: 0x000A630C
		public static ThirdPartyManager Instance
		{
			get
			{
				return ThirdPartyManager.s_manager;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x060023FB RID: 9211 RVA: 0x000A8113 File Offset: 0x000A6313
		public static bool IsInitialized
		{
			get
			{
				return ThirdPartyManager.Instance.m_initialized;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x060023FC RID: 9212 RVA: 0x000A811F File Offset: 0x000A631F
		public static bool IsThirdPartyReplicationEnabled
		{
			get
			{
				if (!ThirdPartyManager.Instance.m_initialized)
				{
					throw new TPRInitException(ThirdPartyManager.Instance.m_initFailMsg);
				}
				return ThirdPartyManager.Instance.m_tprEnabled;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x060023FD RID: 9213 RVA: 0x000A8147 File Offset: 0x000A6347
		public bool IsAmeListening
		{
			get
			{
				return this.m_isAmeListening;
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x060023FE RID: 9214 RVA: 0x000A814F File Offset: 0x000A634F
		public string Name
		{
			get
			{
				return "Third Party Replication Manager";
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x060023FF RID: 9215 RVA: 0x000A8156 File Offset: 0x000A6356
		public FacilityEnum Facility
		{
			get
			{
				return FacilityEnum.ThirdPartyReplicationManager;
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06002400 RID: 9216 RVA: 0x000A8159 File Offset: 0x000A6359
		public bool IsCritical
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06002401 RID: 9217 RVA: 0x000A815C File Offset: 0x000A635C
		public bool IsEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06002402 RID: 9218 RVA: 0x000A815F File Offset: 0x000A635F
		public bool IsRetriableOnError
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x000A8162 File Offset: 0x000A6362
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public void Invoke(Action toInvoke)
		{
			toInvoke();
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x000A816C File Offset: 0x000A636C
		public static string GetPrimaryActiveManager()
		{
			AmConfig config = AmSystemManager.Instance.Config;
			if (config.IsPamOrSam)
			{
				return config.DagConfig.CurrentPAM.Fqdn;
			}
			throw new NoPAMDesignatedException();
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x000A81A2 File Offset: 0x000A63A2
		public static void LogInvalidOperation(Exception e)
		{
			ReplayCrimsonEvents.TPREnabledInvalidOperation.Log<string, string>(e.Message, e.StackTrace);
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x000A81BC File Offset: 0x000A63BC
		public static void PreventOperationWhenTPREnabled(string operationName)
		{
			if (ThirdPartyManager.IsThirdPartyReplicationEnabled)
			{
				TPREnabledInvalidOperationException ex = new TPREnabledInvalidOperationException(operationName);
				try
				{
					throw ex;
				}
				finally
				{
					ThirdPartyManager.LogInvalidOperation(ex);
				}
			}
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x000A81F0 File Offset: 0x000A63F0
		public bool Start()
		{
			if (!this.m_initialized)
			{
				this.Init();
			}
			return this.m_initialized;
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x000A8208 File Offset: 0x000A6408
		public void Stop()
		{
			lock (this.m_serviceLock)
			{
				this.m_fShutdown = true;
			}
			this.StopServiceStarter();
			lock (this.m_serviceLock)
			{
				this.StopService();
			}
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x000A8280 File Offset: 0x000A6480
		public void AmRoleNotify(AmConfig amConfig)
		{
			if (this.m_tprEnabled || (amConfig.DagConfig != null && amConfig.DagConfig.IsThirdPartyReplEnabled))
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.HandleAmRoleChange), amConfig);
			}
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x000A82B2 File Offset: 0x000A64B2
		public Notify OpenNotifyChannel()
		{
			if (!ThirdPartyManager.IsThirdPartyReplicationEnabled)
			{
				throw new TPRNotEnabledException();
			}
			if (!this.m_isAmeListening)
			{
				throw new TPRProviderNotListeningException();
			}
			return Notify.Open(this.m_openTimeout, this.m_sendTimeout, this.m_receiveTimeout);
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x000A8368 File Offset: 0x000A6568
		public void ImmediateDismountMailboxDatabase(Guid databaseId)
		{
			ExTraceGlobals.ThirdPartyManagerTracer.TraceDebug(0L, "ImmediateDismountMailboxDatabase called");
			this.CheckForPam("ImmediateDismountMailboxDatabase");
			Exception ex = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
			{
				this.CheckForPam("ImmediateDismountMailboxDatabase");
				IADDatabase db = this.LookupDatabase(databaseId);
				AmDbActionCode actionCode = new AmDbActionCode(AmDbActionInitiator.Admin, AmDbActionReason.FailureItem, AmDbActionCategory.Dismount);
				AmConfig config = AmSystemManager.Instance.Config;
				new AmDbPamAction(config, db, actionCode, this.GenerateUniqueDbActionId())
				{
					LockTimeout = new TimeSpan?(this.m_openTimeout)
				}.Dismount(UnmountFlags.SkipCacheFlush);
			});
			if (ex == null)
			{
				return;
			}
			if (ex is ThirdPartyReplicationException)
			{
				throw ex;
			}
			throw new ImmediateDismountMailboxDatabaseException(databaseId, ex.Message);
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x000A8464 File Offset: 0x000A6664
		public void ChangeActiveServer(Guid databaseId, string newActiveServerName)
		{
			ExTraceGlobals.ThirdPartyManagerTracer.TraceDebug(0L, "ChangeActiveServer called");
			Exception ex = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
			{
				this.CheckForPam("ChangeActiveServer");
				IADDatabase db = this.LookupDatabase(databaseId);
				this.CheckServerForCopy(db, newActiveServerName);
				AmDbActionCode actionCode = new AmDbActionCode(AmDbActionInitiator.Admin, AmDbActionReason.FailureItem, AmDbActionCategory.Move);
				AmConfig config = AmSystemManager.Instance.Config;
				AmDbPamAction amDbPamAction = new AmDbPamAction(config, db, actionCode, this.GenerateUniqueDbActionId());
				amDbPamAction.ChangeActiveServerForThirdParty(newActiveServerName, this.m_openTimeout);
			});
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x000A84B4 File Offset: 0x000A66B4
		public void AmeIsStarting(TimeSpan retryDelay, TimeSpan openTimeout, TimeSpan sendTimeout, TimeSpan receiveTimeout)
		{
			lock (this)
			{
				this.SetTimeouts(retryDelay, openTimeout, sendTimeout, receiveTimeout);
				this.m_isAmeListening = true;
				AmConfig config = AmSystemManager.Instance.Config;
				if (config.IsPAM)
				{
					if (this.BecomePame())
					{
					}
				}
				else
				{
					this.RevokePame();
				}
			}
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x000A8520 File Offset: 0x000A6720
		public void AmeIsStopping()
		{
			lock (this)
			{
				this.m_isAmeListening = false;
			}
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x000A8604 File Offset: 0x000A6804
		public bool CheckHealth(out string errMsg)
		{
			errMsg = string.Empty;
			lock (this.m_serviceLock)
			{
				if (this.m_service == null)
				{
					if (this.m_serviceStartFailure == null)
					{
						errMsg = ReplayStrings.TPRNotYetStarted;
						return false;
					}
					errMsg = ReplayStrings.TPRExchangeNotListening(this.m_serviceStartFailure.Message);
					return false;
				}
			}
			TimeSpan timeout = new TimeSpan(0, 0, 3);
			Exception ex = this.DoTPRCommunication(delegate(object param0, EventArgs param1)
			{
				using (Request request = Request.Open(timeout, timeout, timeout))
				{
					request.GetPrimaryActiveManager();
				}
			});
			if (ex != null)
			{
				errMsg = ReplayStrings.TPRExchangeListenerNotResponding(ex.Message);
				return false;
			}
			ex = this.DoTPRCommunication(delegate(object param0, EventArgs param1)
			{
				using (Notify notify = Notify.Open(timeout, timeout, timeout))
				{
					TimeSpan timeSpan = default(TimeSpan);
					notify.GetTimeouts(out timeSpan, out timeSpan, out timeSpan, out timeSpan);
				}
			});
			if (ex != null)
			{
				errMsg = ReplayStrings.TPRProviderNotResponding(ex.Message);
				return false;
			}
			return true;
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x000A86F8 File Offset: 0x000A68F8
		internal static void TestSetTPRInitialized()
		{
			ThirdPartyManager.Instance.m_initialized = true;
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x000A8788 File Offset: 0x000A6988
		internal NotificationResponse DatabaseMoveNeeded(Guid dbId, string currentActiveFqdn, bool mountDesired)
		{
			NotificationResponse response = NotificationResponse.Incomplete;
			Exception ex = this.DoTPRCommunication(delegate(object param0, EventArgs param1)
			{
				using (Notify notify = this.OpenNotifyChannel())
				{
					response = notify.DatabaseMoveNeeded(dbId, currentActiveFqdn, mountDesired);
					ReplayCrimsonEvents.TPRDatabaseMoveNeededResponse.Log<Guid, string, bool, NotificationResponse>(dbId, currentActiveFqdn, mountDesired, response);
				}
			});
			if (ex != null)
			{
				ReplayCrimsonEvents.TPRDatabaseMoveNeededCommunicationFailed.Log<Guid, string, bool, string>(dbId, currentActiveFqdn, mountDesired, ex.Message);
			}
			return response;
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x000A881C File Offset: 0x000A6A1C
		private void Init()
		{
			bool flag = false;
			try
			{
				object serviceLock;
				Monitor.Enter(serviceLock = this.m_serviceLock, ref flag);
				if (!this.m_fShutdown)
				{
					IADDatabaseAvailabilityGroup dag = null;
					Exception exception = null;
					TimeSpan invokeTimeout = TimeSpan.FromSeconds((double)RegistryParameters.MonitoringADGetConfigTimeoutInSec);
					try
					{
						InvokeWithTimeout.Invoke(delegate()
						{
							dag = DagHelper.GetLocalServerDatabaseAvailabilityGroup(null, out exception);
						}, invokeTimeout);
					}
					catch (TimeoutException exception)
					{
						TimeoutException exception2;
						exception = exception2;
					}
					if (exception != null)
					{
						ExTraceGlobals.ThirdPartyManagerTracer.TraceError<Exception>(0L, "TPR Init fails due to AD problems: {0}", exception);
						this.m_initFailMsg = exception.Message;
						ReplayEventLogConstants.Tuple_TPRManagerInitFailure.LogEvent(this.Name, new object[]
						{
							this.m_initFailMsg
						});
					}
					else
					{
						if (dag == null)
						{
							ExTraceGlobals.ThirdPartyManagerTracer.TraceDebug(0L, "TPR not enabled because we are not in a DAG");
						}
						else
						{
							if (dag.ThirdPartyReplication == ThirdPartyReplicationMode.Enabled)
							{
								this.m_tprEnabled = true;
							}
							ExTraceGlobals.ThirdPartyManagerTracer.TraceDebug<string>(0L, "TPR is {0}enabled", this.m_tprEnabled ? string.Empty : "not ");
						}
						this.m_initialized = true;
					}
				}
			}
			finally
			{
				if (flag)
				{
					object serviceLock;
					Monitor.Exit(serviceLock);
				}
			}
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x000A8984 File Offset: 0x000A6B84
		private void HandleAmRoleChange(object amConfigObj)
		{
			AmConfig amConfig = (AmConfig)amConfigObj;
			lock (this.m_serviceLock)
			{
				if (!this.m_fShutdown)
				{
					if (amConfig.IsPamOrSam)
					{
						this.m_tprEnabled = amConfig.DagConfig.IsThirdPartyReplEnabled;
						if (!this.m_initialized)
						{
							this.m_initialized = true;
						}
						if (!this.m_tprEnabled)
						{
							this.StopService();
						}
						else if (this.m_service == null)
						{
							this.TryToStartService();
						}
						else if (amConfig.IsPAM)
						{
							this.BecomePame();
						}
						else
						{
							this.RevokePame();
						}
					}
					else if (amConfig.IsStandalone)
					{
						this.m_tprEnabled = false;
						if (!this.m_initialized)
						{
							this.m_initialized = true;
						}
						this.StopService();
					}
					else if (this.IsAmeListening)
					{
						this.RevokePame();
					}
				}
			}
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x000A8A6C File Offset: 0x000A6C6C
		private void StopService()
		{
			this.m_isAmeListening = false;
			if (this.m_service != null)
			{
				this.m_service.StopListening();
				this.m_service = null;
			}
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x000A8A90 File Offset: 0x000A6C90
		private void TryToStartService()
		{
			bool flag = false;
			lock (this.m_serviceLock)
			{
				if (this.m_fShutdown)
				{
					return;
				}
				if (this.m_tprEnabled)
				{
					if (this.m_service == null)
					{
						this.StartService();
						if (this.m_service == null)
						{
							if (this.m_serviceStarter == null)
							{
								this.ScheduleServiceStartupRetry();
							}
						}
						else
						{
							flag = true;
						}
					}
				}
				else
				{
					flag = true;
				}
			}
			if (flag)
			{
				this.StopServiceStarter();
			}
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x000A8B98 File Offset: 0x000A6D98
		private void StartService()
		{
			Exception ex = null;
			this.m_service = ThirdPartyService.StartListening(out ex);
			if (this.m_service == null)
			{
				this.m_serviceStartFailure = ex;
				return;
			}
			ex = this.DoTPRCommunication(delegate(object param0, EventArgs param1)
			{
				TimeSpan timeSpan = new TimeSpan(0, 0, 2);
				using (Notify notify = Notify.Open(timeSpan, timeSpan, timeSpan))
				{
					notify.GetTimeouts(out this.m_retryDelay, out this.m_openTimeout, out this.m_sendTimeout, out this.m_receiveTimeout);
					this.m_isAmeListening = true;
					AmConfig config = AmSystemManager.Instance.Config;
					if (config.IsPAM)
					{
						this.BecomePame();
					}
					else
					{
						this.RevokePame();
					}
				}
			});
			if (ex != null)
			{
				ExTraceGlobals.ThirdPartyManagerTracer.TraceError<Exception>(0L, "StartService fails to contact the AME: {0}", ex);
			}
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x000A8BEC File Offset: 0x000A6DEC
		private Exception DoADAction(EventHandler ev)
		{
			Exception ex = null;
			try
			{
				ev(null, null);
			}
			catch (ADTransientException ex2)
			{
				ex = ex2;
			}
			catch (ADExternalException ex3)
			{
				ex = ex3;
			}
			catch (ADOperationException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				StackFrame stackFrame = new StackFrame(1);
				ExTraceGlobals.ThirdPartyServiceTracer.TraceError<string, string, Exception>(0L, "DoAction({0}) fails: {1}, {2}", stackFrame.GetMethod().Name, ex.Message, ex);
			}
			return ex;
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x000A8C6C File Offset: 0x000A6E6C
		private Exception DoTPRCommunication(EventHandler ev)
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
			catch (TPRProviderNotListeningException ex3)
			{
				ex = ex3;
			}
			catch (TPRNotEnabledException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				StackFrame stackFrame = new StackFrame(1);
				ExTraceGlobals.ThirdPartyServiceTracer.TraceError<string, string, Exception>(0L, "DoAction({0}) fails: {1}, {2}", stackFrame.GetMethod().Name, ex.Message, ex);
			}
			return ex;
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x000A8D24 File Offset: 0x000A6F24
		private bool BecomePame()
		{
			Exception ex = this.DoTPRCommunication(delegate(object param0, EventArgs param1)
			{
				using (Notify notify = this.OpenNotifyChannel())
				{
					notify.BecomePame();
				}
			});
			if (ex != null)
			{
				ExTraceGlobals.ThirdPartyManagerTracer.TraceError<string, Exception>(0L, "BecomePame notification failed: {0}, {1}", ex.Message, ex);
				return false;
			}
			return true;
		}

		// Token: 0x0600241A RID: 9242 RVA: 0x000A8D9C File Offset: 0x000A6F9C
		private bool RevokePame()
		{
			Exception ex = this.DoTPRCommunication(delegate(object param0, EventArgs param1)
			{
				using (Notify notify = this.OpenNotifyChannel())
				{
					notify.RevokePame();
				}
			});
			if (ex != null)
			{
				ExTraceGlobals.ThirdPartyManagerTracer.TraceError<string, Exception>(0L, "RevokePame notification failed: {0}, {1}", ex.Message, ex);
				return false;
			}
			return true;
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x000A8DDC File Offset: 0x000A6FDC
		private string GenerateUniqueDbActionId()
		{
			return string.Format("{0}.TPR", ExDateTime.Now.ToString("yyyy.MM.dd.hh.mm.ss.fff"));
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x000A8E08 File Offset: 0x000A7008
		private void CheckForPam(string methodName)
		{
			AmConfig config = AmSystemManager.Instance.Config;
			if (!config.IsPAM)
			{
				throw new NotThePamException(methodName);
			}
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x000A8E60 File Offset: 0x000A7060
		private IADDatabase LookupDatabase(Guid databaseId)
		{
			IADDatabase db = null;
			Exception ex = this.DoADAction(delegate(object param0, EventArgs param1)
			{
				IADToplogyConfigurationSession iadtoplogyConfigurationSession = ADSessionFactory.CreateIgnoreInvalidRootOrgSession(true);
				db = iadtoplogyConfigurationSession.FindDatabaseByGuid(databaseId);
			});
			if (db != null)
			{
				return db;
			}
			ExTraceGlobals.ThirdPartyManagerTracer.TraceDebug<Guid, Exception>(0L, "LookupDatabase({0}) failed. Ex={1}", databaseId, ex);
			if (ex == null)
			{
				throw new NoSuchDatabaseException(databaseId);
			}
			throw ex;
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x000A8F28 File Offset: 0x000A7128
		private void CheckServerForCopy(IADDatabase db, string serverName)
		{
			AmServerName amServerName = new AmServerName(serverName);
			IADDatabaseCopy dbCopy = null;
			Exception ex = this.DoADAction(delegate(object param0, EventArgs param1)
			{
				foreach (IADDatabaseCopy iaddatabaseCopy in db.DatabaseCopies)
				{
					if (MachineName.Comparer.Equals(iaddatabaseCopy.HostServerName, amServerName.NetbiosName))
					{
						dbCopy = iaddatabaseCopy;
						return;
					}
				}
			});
			if (dbCopy != null)
			{
				return;
			}
			ExTraceGlobals.ThirdPartyManagerTracer.TraceDebug(0L, "CheckServerForCopy:no copy of {0} guid={1} on {2}. Ex={3}", new object[]
			{
				db.Name,
				db.Guid,
				serverName,
				ex
			});
			if (ex == null)
			{
				throw new NoCopyOnServerException(db.Guid, db.Name, serverName);
			}
			throw ex;
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x000A8FD6 File Offset: 0x000A71D6
		private void SetTimeouts(TimeSpan retryDelay, TimeSpan openTimeout, TimeSpan sendTimeout, TimeSpan receiveTimeout)
		{
			this.m_retryDelay = retryDelay;
			this.m_openTimeout = openTimeout;
			this.m_sendTimeout = sendTimeout;
			this.m_receiveTimeout = receiveTimeout;
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x000A8FF5 File Offset: 0x000A71F5
		private void ScheduleServiceStartupRetry()
		{
			this.m_serviceStarter = new ThirdPartyManager.PeriodicTPRStarter(TimeSpan.FromMilliseconds((double)RegistryParameters.ConfigUpdaterTimerIntervalSlow));
			this.m_serviceStarter.Start();
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x000A9018 File Offset: 0x000A7218
		private void StopServiceStarter()
		{
			ThirdPartyManager.PeriodicTPRStarter periodicTPRStarter = null;
			lock (this.m_starterLock)
			{
				if (this.m_serviceStarter != null)
				{
					ExTraceGlobals.ThirdPartyManagerTracer.TraceDebug((long)this.GetHashCode(), "PeriodicStarter is being stopped.");
					periodicTPRStarter = this.m_serviceStarter;
					this.m_serviceStarter = null;
				}
			}
			if (periodicTPRStarter != null)
			{
				periodicTPRStarter.Stop();
			}
		}

		// Token: 0x04000F54 RID: 3924
		private static ThirdPartyManager s_manager = new ThirdPartyManager();

		// Token: 0x04000F55 RID: 3925
		private ThirdPartyService m_service;

		// Token: 0x04000F56 RID: 3926
		private object m_serviceLock = new object();

		// Token: 0x04000F57 RID: 3927
		private Exception m_serviceStartFailure;

		// Token: 0x04000F58 RID: 3928
		private bool m_tprEnabled;

		// Token: 0x04000F59 RID: 3929
		private bool m_initialized;

		// Token: 0x04000F5A RID: 3930
		private string m_initFailMsg;

		// Token: 0x04000F5B RID: 3931
		private bool m_fShutdown;

		// Token: 0x04000F5C RID: 3932
		private bool m_isAmeListening;

		// Token: 0x04000F5D RID: 3933
		private TimeSpan m_retryDelay;

		// Token: 0x04000F5E RID: 3934
		private TimeSpan m_openTimeout;

		// Token: 0x04000F5F RID: 3935
		private TimeSpan m_sendTimeout;

		// Token: 0x04000F60 RID: 3936
		private TimeSpan m_receiveTimeout;

		// Token: 0x04000F61 RID: 3937
		private ThirdPartyManager.PeriodicTPRStarter m_serviceStarter;

		// Token: 0x04000F62 RID: 3938
		private object m_starterLock = new object();

		// Token: 0x02000381 RID: 897
		[ClassAccessLevel(AccessLevel.MSInternal)]
		internal class PeriodicTPRStarter : TimerComponent
		{
			// Token: 0x06002427 RID: 9255 RVA: 0x000A90B6 File Offset: 0x000A72B6
			public PeriodicTPRStarter(TimeSpan periodicStartInterval) : base(periodicStartInterval, periodicStartInterval, "PeriodicTPRStarter")
			{
			}

			// Token: 0x06002428 RID: 9256 RVA: 0x000A90C5 File Offset: 0x000A72C5
			protected override void TimerCallbackInternal()
			{
				ThirdPartyManager.Instance.TryToStartService();
			}
		}
	}
}
