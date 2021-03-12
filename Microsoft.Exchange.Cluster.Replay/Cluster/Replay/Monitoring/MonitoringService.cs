using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.DagManagement;

namespace Microsoft.Exchange.Cluster.Replay.Monitoring
{
	// Token: 0x020001D7 RID: 471
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
	internal class MonitoringService : IInternalMonitoringService
	{
		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x0004BD99 File Offset: 0x00049F99
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.MonitoringWcfServiceTracer;
			}
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x0004BDA0 File Offset: 0x00049FA0
		private MonitoringService(IDatabaseHealthTracker healthTracker)
		{
			MonitoringService.Tracer.TraceDebug((long)this.GetHashCode(), "Creating MonitoringService instance.");
			this.m_healthTracker = healthTracker;
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0004BDC8 File Offset: 0x00049FC8
		public static MonitoringService StartListening(IDatabaseHealthTracker healthTracker, out Exception exception)
		{
			MonitoringService.Tracer.TraceDebug(0L, "Starting Monitoring WCF Service listener.");
			exception = null;
			MonitoringService monitoringService = null;
			try
			{
				monitoringService = new MonitoringService(healthTracker);
				monitoringService.m_host = new ServiceHost(monitoringService, new Uri[]
				{
					MonitoringService.baseAddress
				});
				NetTcpBinding netTcpBinding = new NetTcpBinding();
				netTcpBinding.PortSharingEnabled = true;
				netTcpBinding.MaxBufferPoolSize = 16777216L;
				netTcpBinding.MaxBufferSize = 16777216;
				netTcpBinding.MaxConnections = 100;
				netTcpBinding.MaxReceivedMessageSize = 16777216L;
				netTcpBinding.ReaderQuotas.MaxDepth = 128;
				netTcpBinding.ReaderQuotas.MaxArrayLength = int.MaxValue;
				netTcpBinding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
				netTcpBinding.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;
				netTcpBinding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
				monitoringService.m_host.AddServiceEndpoint(typeof(IInternalMonitoringService), netTcpBinding, string.Empty);
				ServiceDebugBehavior serviceDebugBehavior = monitoringService.m_host.Description.Behaviors.Find<ServiceDebugBehavior>();
				if (serviceDebugBehavior != null)
				{
					serviceDebugBehavior.IncludeExceptionDetailInFaults = true;
				}
				else
				{
					monitoringService.m_host.Description.Behaviors.Add(new ServiceDebugBehavior
					{
						IncludeExceptionDetailInFaults = true
					});
				}
				monitoringService.m_host.OpenTimeout = TimeSpan.FromSeconds(30.0);
				monitoringService.m_host.Open();
				return monitoringService;
			}
			catch (CommunicationException ex)
			{
				exception = ex;
			}
			catch (ConfigurationException ex2)
			{
				exception = ex2;
			}
			catch (TimeoutException ex3)
			{
				exception = ex3;
			}
			catch (InvalidOperationException ex4)
			{
				exception = ex4;
			}
			if (exception != null)
			{
				MonitoringService.Tracer.TraceError<Exception>(0L, "StartListening() failed to register WCF Monitoring service with exception: {0}", exception);
				ReplayCrimsonEvents.MonitoringServiceFailedToRegister.LogPeriodic<string, string>(Environment.MachineName, DateTimeHelper.OneHour, exception.Message, exception.StackTrace);
			}
			if (monitoringService != null && monitoringService.m_host != null)
			{
				monitoringService.m_host.Abort();
			}
			return null;
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0004BFFC File Offset: 0x0004A1FC
		public void StopListening()
		{
			MonitoringService.Tracer.TraceDebug((long)this.GetHashCode(), "MonitoringService.StopListening() called!");
			this.m_host.Close();
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x0004C068 File Offset: 0x0004A268
		public ServiceVersion GetVersion()
		{
			ServiceVersion version = null;
			this.RunOperation(delegate
			{
				MonitoringService.Tracer.TraceDebug((long)this.GetHashCode(), "GetVersion() called.");
				version = new ServiceVersion
				{
					Version = 1L
				};
			});
			return version;
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x0004C0DC File Offset: 0x0004A2DC
		public void PublishDagHealthInfo(HealthInfoPersisted healthInfo)
		{
			this.RunOperation(delegate
			{
				MonitoringService.Tracer.TraceDebug((long)this.GetHashCode(), "PublishDagHealthInfo() called.");
				this.m_healthTracker.UpdateHealthInfo(healthInfo);
			});
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x0004C14C File Offset: 0x0004A34C
		public DateTime GetDagHealthInfoUpdateTimeUtc()
		{
			DateTime lastUpdateTime = DateTime.MinValue;
			this.RunOperation(delegate
			{
				MonitoringService.Tracer.TraceDebug((long)this.GetHashCode(), "GetDagHealthInfoUpdateTimeUtc() called.");
				lastUpdateTime = this.m_healthTracker.GetDagHealthInfoUpdateTimeUtc();
			});
			return lastUpdateTime;
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x0004C1C4 File Offset: 0x0004A3C4
		public HealthInfoPersisted GetDagHealthInfo()
		{
			HealthInfoPersisted hip = null;
			this.RunOperation(delegate
			{
				MonitoringService.Tracer.TraceDebug((long)this.GetHashCode(), "GetDagHealthInfo() called.");
				hip = this.m_healthTracker.GetDagHealthInfo();
			});
			return hip;
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0004C200 File Offset: 0x0004A400
		private void RunOperation(Action operation)
		{
			Exception ex = null;
			try
			{
				operation();
			}
			catch (DatabaseHealthTrackerException ex2)
			{
				ex = ex2;
			}
			catch (Exception ex3)
			{
				ExWatson.SendReportAndCrashOnAnotherThread(ex3);
				ex = ex3;
			}
			if (ex != null)
			{
				MonitoringService.Tracer.TraceError<Exception>((long)this.GetHashCode(), "RunOperation() is rethrowing exception: {0}", ex);
				ExceptionDetail detail = new ExceptionDetail(ex);
				throw new FaultException<ExceptionDetail>(detail, ex.Message);
			}
		}

		// Token: 0x0400072D RID: 1837
		private static Uri baseAddress = new Uri(string.Format("net.tcp://localhost:{0}/Microsoft.Exchange.HA.Monitoring", RegistryParameters.MonitoringWebServicePort));

		// Token: 0x0400072E RID: 1838
		private ServiceHost m_host;

		// Token: 0x0400072F RID: 1839
		private IDatabaseHealthTracker m_healthTracker;
	}
}
