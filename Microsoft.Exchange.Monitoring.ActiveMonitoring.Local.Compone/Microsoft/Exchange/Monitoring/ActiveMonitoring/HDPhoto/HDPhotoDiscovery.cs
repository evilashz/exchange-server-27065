using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.HDPhoto.Probes;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HDPhoto
{
	// Token: 0x02000182 RID: 386
	public sealed class HDPhotoDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06000B33 RID: 2867 RVA: 0x00047774 File Offset: 0x00045974
		protected override void DoWork(CancellationToken cancellationToken)
		{
			this.Configure(base.TraceContext);
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			if (!LocalEndpointManager.IsDataCenter && !this.isEnabledOnPrem)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.HDPhotoTracer, base.TraceContext, "HDPhotoDiscovery.DoWork: Skipping work since we are running onprem and 'EnableOnPrem' config setting is 'false'.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HDPhoto\\hdphotodiscovery.cs", 205);
				return;
			}
			if (instance.ExchangeServerRoleEndpoint.IsCafeRoleInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.HDPhotoTracer, base.TraceContext, "HDPhotoDiscovery.DoWork: {0} role is running on a Cafe server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HDPhoto\\hdphotodiscovery.cs", 212);
				if (instance.MailboxDatabaseEndpoint != null && instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForCafe.Count != 0)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.HDPhotoTracer, base.TraceContext, "HDPhotoDiscovery.DoWork: Found MailboxDatabases on the Cafe Server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HDPhoto\\hdphotodiscovery.cs", 217);
					IEnumerable<MailboxDatabaseInfo> mbxDbs = instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForCafe;
					HDPhotoDiscovery.ProbeInfo probeInfo = new HDPhotoDiscovery.ProbeInfo
					{
						Attributes = HDPhotoDiscovery.StandardAttributes,
						TypeName = "HDPhotoCTPTest",
						ProbeName = "HDPhotoCafeProbe",
						MonitorName = "HDPhotoCafeMonitor",
						AlertResponderName = "HDPhotoCafeResponder"
					};
					if (this.isCafeProbeEnabled)
					{
						this.CreateDefinitions(base.TraceContext, mbxDbs, probeInfo);
					}
				}
			}
			if (instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.HDPhotoTracer, base.TraceContext, "HDPhotoDiscovery.DoWork: {0} role is running on a Mailbox server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HDPhoto\\hdphotodiscovery.cs", 243);
				if (instance.MailboxDatabaseEndpoint != null && instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend.Count != 0)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.HDPhotoTracer, base.TraceContext, "HDPhotoDiscovery.DoWork: Found MailboxDatabases on the Mailbox Server.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HDPhoto\\hdphotodiscovery.cs", 248);
					IEnumerable<MailboxDatabaseInfo> mbxDbs = instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend;
					HDPhotoDiscovery.ProbeInfo probeInfo2 = new HDPhotoDiscovery.ProbeInfo
					{
						Attributes = HDPhotoDiscovery.StandardAttributes,
						TypeName = "HDPhotoDeepTest",
						ProbeName = "HDPhotoMailboxProbe",
						MonitorName = "HDPhotoMailboxMonitor",
						AlertResponderName = "HDPhotoMailboxResponder"
					};
					if (this.isMailboxProbeEnabled)
					{
						this.CreateDefinitions(base.TraceContext, mbxDbs, probeInfo2);
					}
				}
			}
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x000479A8 File Offset: 0x00045BA8
		private void CreateDefinitions(TracingContext traceContext, IEnumerable<MailboxDatabaseInfo> mbxDbs, HDPhotoDiscovery.ProbeInfo probeInfo)
		{
			mbxDbs = from x in mbxDbs
			where !string.IsNullOrEmpty(x.MonitoringAccount) && !string.IsNullOrEmpty(x.MonitoringAccountPassword)
			select x;
			if (mbxDbs == null || mbxDbs.Count<MailboxDatabaseInfo>() == 0)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.HDPhotoTracer, base.TraceContext, "HDPhotoDiscovery.DoWork: No mailbox databases were found with a monitoring mailbox", null, "CreateDefinitions", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HDPhoto\\hdphotodiscovery.cs", 286);
				return;
			}
			this.CreateProbes(base.TraceContext, mbxDbs, probeInfo);
			MonitorDefinition monitor = this.CreateMonitor(base.TraceContext, probeInfo);
			this.CreateResponder(base.TraceContext, monitor, probeInfo);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00047A38 File Offset: 0x00045C38
		private void Configure(TracingContext traceContext)
		{
			this.isAlertResponderEnabled = this.ReadAttribute("AlertResponderEnabled", false);
			this.isCafeProbeEnabled = this.ReadAttribute("IsCafeProbeEnabled", true);
			this.isMailboxProbeEnabled = this.ReadAttribute("IsMailboxProbeEnabled", true);
			this.isEnabledOnPrem = this.ReadAttribute("EnableOnPrem", false);
			this.failedProbeThreshold = this.ReadAttribute("FailedProbeThreshold", 75);
			this.probeRecurrenceInterval = this.ReadAttribute("ProbeRecurrenceInterval", HDPhotoDiscovery.DefaultProbeRecurrenceInterval);
			this.monitorRecurrenceInterval = this.ReadAttribute("MonitorRecurrenceInterval", HDPhotoDiscovery.DefaultMonitorRecurrenceInterval);
			this.monitoringInterval = this.ReadAttribute("MonitoringInterval", HDPhotoDiscovery.DefaultProbeRecurrenceInterval);
			this.probeTimeoutSeconds = (int)this.ReadAttribute("ProbeTimeoutSpan", HDPhotoDiscovery.DefaultProbeTimeout).TotalSeconds;
			this.degradedTransitionSpanSeconds = (int)this.ReadAttribute("DegradedTransitionSpan", HDPhotoDiscovery.DefaultDegradedTransitionSpan).TotalSeconds;
			this.unhealthyTransitionSpanSeconds = (int)this.ReadAttribute("UnhealthyTransitionSpan", HDPhotoDiscovery.DefaultUnhealthyTransitionSpan).TotalSeconds;
			this.maxRetryAttempts = this.ReadAttribute("MaxRetryAttempts", 3);
			this.minErrorCount = this.ReadAttribute("MinimumErrorCount", 5);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00047B64 File Offset: 0x00045D64
		private void CreateProbes(TracingContext traceContext, IEnumerable<MailboxDatabaseInfo> dbInfos, HDPhotoDiscovery.ProbeInfo probeInfo)
		{
			foreach (MailboxDatabaseInfo mailboxDatabaseInfo in dbInfos)
			{
				if (DirectoryAccessor.Instance.IsDatabaseCopyActiveOnLocalServer(mailboxDatabaseInfo.MailboxDatabaseGuid))
				{
					ProbeDefinition probeDefinition = new ProbeDefinition();
					probeDefinition.AssemblyPath = Assembly.GetExecutingAssembly().Location;
					probeDefinition.TypeName = typeof(HDPhotoLocalProbe).FullName;
					probeDefinition.Name = probeInfo.ProbeName;
					probeDefinition.Account = mailboxDatabaseInfo.MonitoringAccount + "@" + mailboxDatabaseInfo.MonitoringAccountDomain;
					probeDefinition.AccountPassword = mailboxDatabaseInfo.MonitoringAccountPassword;
					probeDefinition.AccountDisplayName = mailboxDatabaseInfo.MonitoringAccount;
					probeDefinition.SecondaryAccount = mailboxDatabaseInfo.MonitoringAccount + "@" + mailboxDatabaseInfo.MonitoringAccountDomain;
					probeDefinition.SecondaryAccountPassword = mailboxDatabaseInfo.MonitoringAccountPassword;
					probeDefinition.SecondaryAccountDisplayName = mailboxDatabaseInfo.MonitoringAccount;
					probeDefinition.ServiceName = ExchangeComponent.HDPhoto.Name;
					probeDefinition.MaxRetryAttempts = this.maxRetryAttempts;
					probeDefinition.TargetResource = mailboxDatabaseInfo.MailboxDatabaseName;
					probeDefinition.Attributes["DatabaseGuid"] = mailboxDatabaseInfo.MailboxDatabaseGuid.ToString();
					if (probeInfo.TypeName == "HDPhotoCTPTest")
					{
						probeDefinition.Endpoint = (probeDefinition.SecondaryEndpoint = HDPhotoDiscovery.ProbeEndPoint.TrimEnd(new char[]
						{
							'/'
						}) + "/ews/exchange.asmx");
					}
					else
					{
						probeDefinition.Endpoint = (probeDefinition.SecondaryEndpoint = HDPhotoDiscovery.ProbeEndPoint.TrimEnd(new char[]
						{
							'/'
						}) + ":444/ews/exchange.asmx");
					}
					probeDefinition.RecurrenceIntervalSeconds = (int)this.probeRecurrenceInterval.TotalSeconds * dbInfos.Count<MailboxDatabaseInfo>();
					probeDefinition.TimeoutSeconds = this.probeTimeoutSeconds;
					this.CopyAttributes(probeInfo.Attributes, probeDefinition);
					WTFDiagnostics.TraceInformation(ExTraceGlobals.HDPhotoTracer, base.TraceContext, "Configured probe " + probeInfo.ProbeName, null, "CreateProbes", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HDPhoto\\hdphotodiscovery.cs", 372);
					base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
				}
			}
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00047DAC File Offset: 0x00045FAC
		private MonitorDefinition CreateMonitor(TracingContext traceContext, HDPhotoDiscovery.ProbeInfo probeInfo)
		{
			MonitorDefinition monitorDefinition = OverallPercentSuccessMonitor.CreateDefinition(probeInfo.MonitorName, probeInfo.ProbeName, ExchangeComponent.HDPhoto.Name, ExchangeComponent.HDPhoto, (double)this.failedProbeThreshold, this.monitoringInterval, true);
			monitorDefinition.MaxRetryAttempts = 0;
			monitorDefinition.RecurrenceIntervalSeconds = (int)this.monitorRecurrenceInterval.TotalSeconds;
			monitorDefinition.TimeoutSeconds = this.probeTimeoutSeconds;
			monitorDefinition.MinimumErrorCount = this.minErrorCount;
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, this.degradedTransitionSpanSeconds),
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, this.unhealthyTransitionSpanSeconds)
			};
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Validate HD Photo health is not impacted any issues";
			WTFDiagnostics.TraceInformation(ExTraceGlobals.HDPhotoTracer, base.TraceContext, "Configured monitor " + monitorDefinition.Name, null, "CreateMonitor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HDPhoto\\hdphotodiscovery.cs", 412);
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			return monitorDefinition;
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00047E9C File Offset: 0x0004609C
		private void CreateResponder(TracingContext traceContext, MonitorDefinition monitor, HDPhotoDiscovery.ProbeInfo probeInfo)
		{
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(probeInfo.AlertResponderName, ExchangeComponent.HDPhoto.Name, monitor.Name, monitor.ConstructWorkItemResultName(), null, ServiceHealthStatus.Unhealthy, ExchangeComponent.HDPhoto.EscalationTeam, "The GetUserPhoto request failed on {Probe.MachineName}. Success rate is {Monitor.TotalValue}%", "The GetUserPhoto request failed with the error below.\n\n Error: {Probe.Error} \n Exception: {Probe.Exception} \n FailureContext: {Probe.FailureContext} \n Execution Context: {Probe.ExecutionContext} \n ResultName : {Probe.ResultName} \n Photo Url : {Probe.StateAttribute11} \n Users : {Probe.StateAttribute2}", true, NotificationServiceClass.UrgentInTraining, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			responderDefinition.Enabled = this.isAlertResponderEnabled;
			responderDefinition.RecurrenceIntervalSeconds = monitor.RecurrenceIntervalSeconds;
			WTFDiagnostics.TraceInformation(ExTraceGlobals.HDPhotoTracer, base.TraceContext, "Configured escalate responder " + responderDefinition.Name, null, "CreateResponder", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HDPhoto\\hdphotodiscovery.cs", 445);
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
		}

		// Token: 0x04000875 RID: 2165
		public const string HDPhotoCafeProbeType = "HDPhotoCTPTest";

		// Token: 0x04000876 RID: 2166
		public const string HDPhotoCafeProbe = "HDPhotoCafeProbe";

		// Token: 0x04000877 RID: 2167
		public const string HDPhotoCafeMonitor = "HDPhotoCafeMonitor";

		// Token: 0x04000878 RID: 2168
		public const string HDPhotoCafeResponder = "HDPhotoCafeResponder";

		// Token: 0x04000879 RID: 2169
		public const string HDPhotoMailboxProbeType = "HDPhotoDeepTest";

		// Token: 0x0400087A RID: 2170
		public const string HDPhotoMailboxProbe = "HDPhotoMailboxProbe";

		// Token: 0x0400087B RID: 2171
		public const string HDPhotoMailboxMonitor = "HDPhotoMailboxMonitor";

		// Token: 0x0400087C RID: 2172
		public const string HDPhotoMailboxResponder = "HDPhotoMailboxResponder";

		// Token: 0x0400087D RID: 2173
		public const string MailboxDatabaseGuidParameterName = "DatabaseGuid";

		// Token: 0x0400087E RID: 2174
		private const string HDPhotoFailedEscalationSubject = "The GetUserPhoto request failed on {Probe.MachineName}. Success rate is {Monitor.TotalValue}%";

		// Token: 0x0400087F RID: 2175
		private const string HDPhotoFailedEscalationMessage = "The GetUserPhoto request failed with the error below.\n\n Error: {Probe.Error} \n Exception: {Probe.Exception} \n FailureContext: {Probe.FailureContext} \n Execution Context: {Probe.ExecutionContext} \n ResultName : {Probe.ResultName} \n Photo Url : {Probe.StateAttribute11} \n Users : {Probe.StateAttribute2}";

		// Token: 0x04000880 RID: 2176
		private const int DefaultFailedProbeThreshold = 75;

		// Token: 0x04000881 RID: 2177
		private const int DefaultMaxRetryAttempts = 3;

		// Token: 0x04000882 RID: 2178
		private const int DefaultMinErrorCount = 5;

		// Token: 0x04000883 RID: 2179
		private static readonly string[] StandardAttributes = new string[]
		{
			"IsOutsideInMonitoring",
			"PrimaryAuthN",
			"TargetPort",
			"TrustAnySslCertificate",
			"UserAgentPart",
			"Verbose"
		};

		// Token: 0x04000884 RID: 2180
		private static readonly TimeSpan DefaultProbeTimeout = TimeSpan.FromMinutes(4.0);

		// Token: 0x04000885 RID: 2181
		private static readonly TimeSpan DefaultProbeRecurrenceInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000886 RID: 2182
		private static readonly TimeSpan DefaultMonitorRecurrenceInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x04000887 RID: 2183
		private static readonly TimeSpan DefaultMonitoringInterval = TimeSpan.FromHours(1.0);

		// Token: 0x04000888 RID: 2184
		private static readonly TimeSpan DefaultDegradedTransitionSpan = TimeSpan.FromMinutes(0.0);

		// Token: 0x04000889 RID: 2185
		private static readonly TimeSpan DefaultUnhealthyTransitionSpan = TimeSpan.FromMinutes(20.0);

		// Token: 0x0400088A RID: 2186
		private static readonly string ProbeEndPoint = Uri.UriSchemeHttps + "://localhost/";

		// Token: 0x0400088B RID: 2187
		private bool isAlertResponderEnabled;

		// Token: 0x0400088C RID: 2188
		private bool isCafeProbeEnabled;

		// Token: 0x0400088D RID: 2189
		private bool isMailboxProbeEnabled;

		// Token: 0x0400088E RID: 2190
		private bool isEnabledOnPrem;

		// Token: 0x0400088F RID: 2191
		private int failedProbeThreshold;

		// Token: 0x04000890 RID: 2192
		private TimeSpan monitoringInterval;

		// Token: 0x04000891 RID: 2193
		private TimeSpan probeRecurrenceInterval;

		// Token: 0x04000892 RID: 2194
		private TimeSpan monitorRecurrenceInterval;

		// Token: 0x04000893 RID: 2195
		private int probeTimeoutSeconds;

		// Token: 0x04000894 RID: 2196
		private int degradedTransitionSpanSeconds;

		// Token: 0x04000895 RID: 2197
		private int unhealthyTransitionSpanSeconds;

		// Token: 0x04000896 RID: 2198
		private int maxRetryAttempts;

		// Token: 0x04000897 RID: 2199
		private int minErrorCount;

		// Token: 0x02000183 RID: 387
		private class ProbeInfo
		{
			// Token: 0x17000258 RID: 600
			// (get) Token: 0x06000B3C RID: 2876 RVA: 0x00048024 File Offset: 0x00046224
			// (set) Token: 0x06000B3D RID: 2877 RVA: 0x0004802C File Offset: 0x0004622C
			public string TypeName { get; set; }

			// Token: 0x17000259 RID: 601
			// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00048035 File Offset: 0x00046235
			// (set) Token: 0x06000B3F RID: 2879 RVA: 0x0004803D File Offset: 0x0004623D
			public string[] Attributes { get; set; }

			// Token: 0x1700025A RID: 602
			// (get) Token: 0x06000B40 RID: 2880 RVA: 0x00048046 File Offset: 0x00046246
			// (set) Token: 0x06000B41 RID: 2881 RVA: 0x0004804E File Offset: 0x0004624E
			public string ProbeName { get; set; }

			// Token: 0x1700025B RID: 603
			// (get) Token: 0x06000B42 RID: 2882 RVA: 0x00048057 File Offset: 0x00046257
			// (set) Token: 0x06000B43 RID: 2883 RVA: 0x0004805F File Offset: 0x0004625F
			public string MonitorName { get; set; }

			// Token: 0x1700025C RID: 604
			// (get) Token: 0x06000B44 RID: 2884 RVA: 0x00048068 File Offset: 0x00046268
			// (set) Token: 0x06000B45 RID: 2885 RVA: 0x00048070 File Offset: 0x00046270
			public string AlertResponderName { get; set; }
		}
	}
}
