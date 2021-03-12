using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability.Probes
{
	// Token: 0x020001B2 RID: 434
	internal class ServerWideOfflineProbe : ProbeWorkItem
	{
		// Token: 0x06000C73 RID: 3187 RVA: 0x000509F4 File Offset: 0x0004EBF4
		public static ProbeDefinition CreateDefinition(string name, string serviceName, double thresholdInHours, int recurrenceInterval, int timeout, int maxRetry)
		{
			ProbeDefinition probeDefinition = new ProbeDefinition();
			probeDefinition.AssemblyPath = Assembly.GetExecutingAssembly().Location;
			probeDefinition.ServiceName = serviceName;
			probeDefinition.TypeName = typeof(ServerWideOfflineProbe).FullName;
			probeDefinition.Name = name;
			probeDefinition.RecurrenceIntervalSeconds = recurrenceInterval;
			probeDefinition.TimeoutSeconds = timeout;
			probeDefinition.MaxRetryAttempts = maxRetry;
			probeDefinition.TargetResource = Environment.MachineName;
			probeDefinition.Attributes[ServerWideOfflineProbe.ThresholddAttrName] = thresholdInHours.ToString();
			return probeDefinition;
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x00050A74 File Offset: 0x0004EC74
		public static ProbeDefinition CreateDefinition(string name, string serviceName, double thresholdInHours, int recurrenceInterval)
		{
			return ServerWideOfflineProbe.CreateDefinition(name, serviceName, thresholdInHours, recurrenceInterval, recurrenceInterval / 2, 3);
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00050A84 File Offset: 0x0004EC84
		public override void PopulateDefinition<ProbeDefinition>(ProbeDefinition pDef, Dictionary<string, string> propertyBag)
		{
			if (pDef == null)
			{
				throw new ArgumentException("Please specify a value for probeDefinition");
			}
			if (propertyBag.ContainsKey(ServerWideOfflineProbe.ThresholddAttrName))
			{
				pDef.Attributes[ServerWideOfflineProbe.ThresholddAttrName] = propertyBag[ServerWideOfflineProbe.ThresholddAttrName].ToString().Trim();
				return;
			}
			throw new ArgumentException("Please specify value for" + ServerWideOfflineProbe.ThresholddAttrName);
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00050AF4 File Offset: 0x0004ECF4
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (HighAvailabilityUtility.CheckCancellationRequested(cancellationToken))
			{
				base.Result.StateAttribute1 = "Cancellation Requested!";
				return;
			}
			double num = 0.0;
			if (string.IsNullOrWhiteSpace(base.Definition.Attributes["threshold"]) || !double.TryParse(base.Definition.Attributes["threshold"], out num))
			{
				throw new HighAvailabilityMAProbeException("Required attribute 'threshold' is either in wrong format or is empty");
			}
			DateTime utcNow = DateTime.UtcNow;
			base.Result.StateAttribute1 = string.Format("TargetResource='{0}'", base.Definition.TargetResource);
			base.Result.StateAttribute4 = string.Format("Threshold={0:0.00} hours. CurrentTime={1}", num, utcNow.ToString());
			ServerWideOfflineProbe.ComponentState componentState = ServerWideOfflineProbe.ComponentState.ConstructFromLocalRegistry(base.TraceContext);
			base.Result.StateAttribute2 = string.Format("ServerWideStatus={0}", componentState.IsOnline ? "Online" : "Offline");
			base.Result.StateAttribute3 = string.Format("StateTimestamp={0}", componentState.Timestamp.ToString());
			if (!componentState.IsOnline && utcNow - componentState.Timestamp > TimeSpan.FromHours(num))
			{
				base.Result.StateAttribute5 = "Fail.";
				throw new HighAvailabilityMAProbeRedResultException(string.Format("Server '{0}' is offline since {1} which is longer than {2:0.00} hours. Current time is {3}", new object[]
				{
					base.Definition.TargetResource,
					componentState.Timestamp,
					num,
					utcNow.ToString()
				}));
			}
			base.Result.StateAttribute5 = string.Format("Pass", new object[0]);
		}

		// Token: 0x04000936 RID: 2358
		public static readonly string ThresholddAttrName = "threshold";

		// Token: 0x020001B3 RID: 435
		private class ComponentState
		{
			// Token: 0x06000C79 RID: 3193 RVA: 0x00050CC8 File Offset: 0x0004EEC8
			private ComponentState(ServerComponentEnum component, TracingContext traceContext)
			{
				this.traceContext = traceContext;
				this.component = component;
				this.RetrieveAndUpdateIfNecessary();
			}

			// Token: 0x17000291 RID: 657
			// (get) Token: 0x06000C7A RID: 3194 RVA: 0x00050CE4 File Offset: 0x0004EEE4
			public DateTime Timestamp
			{
				get
				{
					return this.timestamp;
				}
			}

			// Token: 0x17000292 RID: 658
			// (get) Token: 0x06000C7B RID: 3195 RVA: 0x00050CEC File Offset: 0x0004EEEC
			public bool IsOnline
			{
				get
				{
					return this.isOnline;
				}
			}

			// Token: 0x17000293 RID: 659
			// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00050CF4 File Offset: 0x0004EEF4
			public ServerComponentEnum Component
			{
				get
				{
					return this.component;
				}
			}

			// Token: 0x06000C7D RID: 3197 RVA: 0x00050CFC File Offset: 0x0004EEFC
			public static ServerWideOfflineProbe.ComponentState ConstructFromLocalRegistry(TracingContext traceContext)
			{
				return ServerWideOfflineProbe.ComponentState.ConstructFromLocalRegistry(ServerComponentEnum.ServerWideOffline, traceContext);
			}

			// Token: 0x06000C7E RID: 3198 RVA: 0x00050D05 File Offset: 0x0004EF05
			public static ServerWideOfflineProbe.ComponentState ConstructFromLocalRegistry(ServerComponentEnum component, TracingContext traceContext)
			{
				return new ServerWideOfflineProbe.ComponentState(component, traceContext);
			}

			// Token: 0x06000C7F RID: 3199 RVA: 0x00050D10 File Offset: 0x0004EF10
			private void RetrieveAndUpdateIfNecessary()
			{
				bool flag = ServerComponentStateManager.IsOnline(this.component);
				DateTime utcNow = DateTime.UtcNow;
				string value = HighAvailabilityUtility.NonCachedRegReader.GetValue<string>(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\HighAvailability\\States\\ServerComponentStates", this.component.ToString(), string.Empty);
				if (!this.TryDeserializeFromRegistry(value, out this.isOnline, out this.timestamp) || flag != this.isOnline)
				{
					this.isOnline = flag;
					this.timestamp = utcNow;
					HighAvailabilityUtility.RegWriter.CreateSubKey(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\HighAvailability\\States\\ServerComponentStates");
					HighAvailabilityUtility.RegWriter.SetValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\HighAvailability\\States\\ServerComponentStates", this.component.ToString(), this.SerializeToRegistry(this.isOnline, this.timestamp), RegistryValueKind.String);
				}
			}

			// Token: 0x06000C80 RID: 3200 RVA: 0x00050DD0 File Offset: 0x0004EFD0
			private bool TryDeserializeFromRegistry(string s, out bool isOnline, out DateTime timestamp)
			{
				isOnline = false;
				timestamp = DateTime.MinValue;
				if (string.IsNullOrWhiteSpace(s))
				{
					WTFDiagnostics.TraceError(ExTraceGlobals.HighAvailabilityTracer, this.traceContext, "Unable to deserialize state from registry - either NULL or Empty", null, "TryDeserializeFromRegistry", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HighAvailability\\Probes\\ServerWideOfflineProbe.cs", 296);
					return false;
				}
				string[] array = s.Split(new char[]
				{
					';'
				});
				if (array.Length != 2)
				{
					WTFDiagnostics.TraceError<string>(ExTraceGlobals.HighAvailabilityTracer, this.traceContext, "Unable to deserialize state from registry - value '{0}' is not in correct format", s, null, "TryDeserializeFromRegistry", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HighAvailability\\Probes\\ServerWideOfflineProbe.cs", 308);
					return false;
				}
				if (!bool.TryParse(array[0], out isOnline) || !DateTime.TryParse(array[1], out timestamp))
				{
					WTFDiagnostics.TraceError<string>(ExTraceGlobals.HighAvailabilityTracer, this.traceContext, "Unable to deserialize state from registry - value '{0}' cannot be deserialized to respective data types", s, null, "TryDeserializeFromRegistry", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HighAvailability\\Probes\\ServerWideOfflineProbe.cs", 319);
					return false;
				}
				return true;
			}

			// Token: 0x06000C81 RID: 3201 RVA: 0x00050E9C File Offset: 0x0004F09C
			private string SerializeToRegistry(bool isOnline, DateTime timestamp)
			{
				return string.Join(';'.ToString(), new string[]
				{
					isOnline.ToString(),
					timestamp.ToString()
				});
			}

			// Token: 0x04000937 RID: 2359
			private const char RegValueSeperator = ';';

			// Token: 0x04000938 RID: 2360
			private DateTime timestamp;

			// Token: 0x04000939 RID: 2361
			private ServerComponentEnum component;

			// Token: 0x0400093A RID: 2362
			private bool isOnline;

			// Token: 0x0400093B RID: 2363
			private TracingContext traceContext;
		}
	}
}
