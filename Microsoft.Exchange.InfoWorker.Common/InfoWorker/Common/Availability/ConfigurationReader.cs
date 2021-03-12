using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Availability;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.RequestDispatch;
using Microsoft.Exchange.Diagnostics.FaultInjection;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000F1 RID: 241
	internal static class ConfigurationReader
	{
		// Token: 0x06000671 RID: 1649 RVA: 0x0001C9F8 File Offset: 0x0001ABF8
		internal static void Start(RequestLogger requestLogger)
		{
			if (ConfigurationReader.timer == null)
			{
				lock (ConfigurationReader.locker)
				{
					if (ConfigurationReader.timer == null)
					{
						ConfigurationReader.Initialize(requestLogger);
						using (ActivityContext.SuppressThreadScope())
						{
							ConfigurationReader.timer = new Timer(new TimerCallback(ConfigurationReader.RefreshTimer), null, ConfigurationReader.dueTime, ConfigurationReader.refreshInterval);
						}
						ConfigurationReader.ConfigurationTracer.TraceDebug(0L, "Timer object for refreshing configuration has been created successfully.");
					}
				}
			}
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001CA98 File Offset: 0x0001AC98
		private static void RefreshTimer(object notUsed)
		{
			ThreadContext.SetWithExceptionHandling("ConfigurationReader.RefreshTimer", DummyApplication.Instance.Worker, null, null, new ThreadContext.ExecuteDelegate(ConfigurationReader.Refresh));
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001CABC File Offset: 0x0001ACBC
		internal static void Refresh()
		{
			ConfigurationReader.ConfigurationTracer.TraceDebug(0L, "ConfigurationReader is now refreshing TargetForestConfigurationCache.");
			DateTime populateDeadline = DateTime.UtcNow + ConfigurationReader.refreshTimeout;
			TargetForestConfigurationCache.Populate(populateDeadline);
			ConfigurationReader.ConfigurationTracer.TraceDebug(0L, "ConfigurationReader is done refreshing TargetForestConfigurationCache.");
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001CB04 File Offset: 0x0001AD04
		private static void Initialize(RequestLogger requestLogger)
		{
			requestLogger.CaptureRequestStage("CRInit");
			ConfigurationReader.ConfigurationTracer.TraceDebug(0L, "ConfigurationReader is now initializing NetworkServiceImpersonator.");
			NetworkServiceImpersonator.Initialize();
			requestLogger.CaptureRequestStage("CRNSInit");
			ConfigurationReader.ConfigurationTracer.TraceDebug(0L, "ConfigurationReader is done initializing NetworkServiceImpersonator.");
			ConfigurationReader.ConfigurationTracer.TraceDebug(0L, "ConfigurationReader is now initializing Dns for AS discovery.");
			AutoDiscoverDnsReader.Initialize();
			requestLogger.CaptureRequestStage("CRAD");
			ConfigurationReader.ConfigurationTracer.TraceDebug(0L, "ConfigurationReader is done initializing Dns for AS discovery.");
			DateTime populateDeadline = DateTime.UtcNow + ConfigurationReader.initializeTimeInterval;
			ConfigurationReader.ConfigurationTracer.TraceDebug(0L, "ConfigurationReader is now refreshing TargetForestConfigurationCache.");
			TargetForestConfigurationCache.Populate(populateDeadline);
			ConfigurationReader.ConfigurationTracer.TraceDebug(0L, "ConfigurationReader is done refreshing TargetForestConfigurationCache.");
			requestLogger.CaptureRequestStage("CRTC");
			ConfigurationReader.ASFaultInjectionTracer.RegisterExceptionInjectionCallback(new ExceptionInjectionCallback(FaultInjection.Callback));
			ConfigurationReader.RequestDispatchFaultInjectionTracer.RegisterExceptionInjectionCallback(new ExceptionInjectionCallback(FaultInjection.Callback));
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001CBEF File Offset: 0x0001ADEF
		internal static void HandleException(Exception e)
		{
			ConfigurationReader.ConfigurationTracer.TraceError<Exception>(0L, "Exception occurred while reading AD configuration: {0}", e);
		}

		// Token: 0x040003D5 RID: 981
		private static readonly TimeSpan refreshInterval = TimeSpan.FromMinutes((double)Configuration.ADRefreshIntervalInMinutes);

		// Token: 0x040003D6 RID: 982
		private static readonly TimeSpan initializeTimeInterval = TimeSpan.FromSeconds(50.0);

		// Token: 0x040003D7 RID: 983
		private static readonly TimeSpan refreshTimeout = TimeSpan.FromMinutes(5.0);

		// Token: 0x040003D8 RID: 984
		private static readonly TimeSpan dueTime = ConfigurationReader.refreshInterval;

		// Token: 0x040003D9 RID: 985
		private static readonly Trace ConfigurationTracer = Microsoft.Exchange.Diagnostics.Components.InfoWorker.Availability.ExTraceGlobals.ConfigurationTracer;

		// Token: 0x040003DA RID: 986
		private static readonly FaultInjectionTrace ASFaultInjectionTracer = Microsoft.Exchange.Diagnostics.Components.InfoWorker.Availability.ExTraceGlobals.FaultInjectionTracer;

		// Token: 0x040003DB RID: 987
		private static readonly FaultInjectionTrace RequestDispatchFaultInjectionTracer = Microsoft.Exchange.Diagnostics.Components.InfoWorker.RequestDispatch.ExTraceGlobals.FaultInjectionTracer;

		// Token: 0x040003DC RID: 988
		private static object locker = new object();

		// Token: 0x040003DD RID: 989
		private static Timer timer;
	}
}
