using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x02000023 RID: 35
	internal class ResourceTracker : IResourceTracker
	{
		// Token: 0x0600015E RID: 350 RVA: 0x00006990 File Offset: 0x00004B90
		public ResourceTracker(IEnumerable<IResourceMeter> resourceMeters, TimeSpan trackingInterval, TimeSpan operationTimeout, ResourceLog resourceLog, TimeSpan logInterval, Func<IEnumerable<ResourceUse>, UseLevel> aggregationFunc, int maxTransientExceptions = 5)
		{
			ArgumentValidator.ThrowIfNull("resourceMeters", resourceMeters);
			ArgumentValidator.ThrowIfInvalidValue<IEnumerable<IResourceMeter>>("resourceMeters", resourceMeters, (IEnumerable<IResourceMeter> meters) => meters.Any<IResourceMeter>());
			ArgumentValidator.ThrowIfNull("resourceLog", resourceLog);
			ArgumentValidator.ThrowIfInvalidValue<TimeSpan>("logInterval", logInterval, (TimeSpan timeout) => timeout > TimeSpan.Zero);
			ArgumentValidator.ThrowIfInvalidValue<TimeSpan>("operationTimeout", operationTimeout, (TimeSpan timeout) => timeout > TimeSpan.Zero);
			ArgumentValidator.ThrowIfInvalidValue<TimeSpan>("trackingInterval", trackingInterval, (TimeSpan interval) => interval > TimeSpan.Zero);
			ArgumentValidator.ThrowIfZeroOrNegative("maxTransientExceptions", maxTransientExceptions);
			if (aggregationFunc != null)
			{
				this.aggregationFunc = aggregationFunc;
			}
			this.operationTimeout = operationTimeout;
			this.trackingInterval = trackingInterval;
			this.resourceLog = resourceLog;
			this.logInterval = logInterval;
			this.isTracking = false;
			this.resourceMeters = resourceMeters;
			this.currentResourseUses = new List<ResourceUse>
			{
				new ResourceUse(this.AggregateResourceUse.Resource, UseLevel.Low, UseLevel.Low)
			};
			this.lastUpdateTime = DateTime.MinValue;
			foreach (IResourceMeter resourceMeter in resourceMeters)
			{
				if (resourceMeter != null)
				{
					if (this.resourceTrackingOperations.ContainsKey(resourceMeter.Resource))
					{
						throw new ArgumentException(string.Format("Duplicate Resource Meter for {0} : {1}", resourceMeter.Resource.Name, resourceMeter.Resource.InstanceName), "resourceMeters");
					}
					DelegatingInfoCollector executionInfo = new DelegatingInfoCollector(new List<IExecutionInfo>
					{
						new ExecutionTimeInfo()
					});
					ResourceTrackingOperation value = new ResourceTrackingOperation(resourceMeter, executionInfo, maxTransientExceptions);
					this.resourceTrackingOperations.Add(resourceMeter.Resource, value);
				}
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600015F RID: 351 RVA: 0x00006BE8 File Offset: 0x00004DE8
		// (remove) Token: 0x06000160 RID: 352 RVA: 0x00006C20 File Offset: 0x00004E20
		public event ResourceUseChangedHandler ResourceUseChanged;

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00006C55 File Offset: 0x00004E55
		public ResourceUse AggregateResourceUse
		{
			get
			{
				return this.aggregateResourceUse;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00006C5D File Offset: 0x00004E5D
		public bool IsTracking
		{
			get
			{
				return this.isTracking;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00006C65 File Offset: 0x00004E65
		public DateTime LastUpdateTime
		{
			get
			{
				return this.lastUpdateTime;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00006C6D File Offset: 0x00004E6D
		public IEnumerable<IResourceMeter> ResourceMeters
		{
			get
			{
				return this.resourceMeters;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00006C75 File Offset: 0x00004E75
		public IEnumerable<ResourceUse> ResourceUses
		{
			get
			{
				return this.currentResourseUses;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00006C7D File Offset: 0x00004E7D
		public TimeSpan TrackingInterval
		{
			get
			{
				return this.trackingInterval;
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006E98 File Offset: 0x00005098
		[DebuggerStepThrough]
		public Task StartResourceTrackingAsync(CancellationToken cancellationToken)
		{
			ResourceTracker.<StartResourceTrackingAsync>d__d <StartResourceTrackingAsync>d__d;
			<StartResourceTrackingAsync>d__d.<>4__this = this;
			<StartResourceTrackingAsync>d__d.cancellationToken = cancellationToken;
			<StartResourceTrackingAsync>d__d.<>t__builder = AsyncTaskMethodBuilder.Create();
			<StartResourceTrackingAsync>d__d.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <StartResourceTrackingAsync>d__d.<>t__builder;
			<>t__builder.Start<ResourceTracker.<StartResourceTrackingAsync>d__d>(ref <StartResourceTrackingAsync>d__d);
			return <StartResourceTrackingAsync>d__d.<>t__builder.Task;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000071B8 File Offset: 0x000053B8
		public async Task TrackResourceUseAsync()
		{
			if (this.resourceTrackingOperations.Any<KeyValuePair<ResourceIdentifier, ResourceTrackingOperation>>())
			{
				IList<ResourceUse> updatedResourceUses = await this.GetUpdatedResourceUsesAsync();
				UseLevel tempAggregateUseLevel = this.aggregationFunc(updatedResourceUses);
				this.aggregateResourceUse = new ResourceUse(this.aggregateResourceUse.Resource, tempAggregateUseLevel, this.aggregateResourceUse.CurrentUseLevel);
				updatedResourceUses.Add(this.aggregateResourceUse);
				this.currentResourseUses = updatedResourceUses;
				this.lastUpdateTime = DateTime.UtcNow;
				IEnumerable<ResourceUse> changedResourceUses = from resourceUse in this.currentResourseUses
				where resourceUse.CurrentUseLevel != resourceUse.PreviousUseLevel
				select resourceUse;
				IEnumerable<ResourceUse> changedRawResourceUses = from resourceUse in this.currentRawResourseUses
				where resourceUse.CurrentUseLevel != resourceUse.PreviousUseLevel
				select resourceUse;
				if (changedResourceUses.Any<ResourceUse>() || changedRawResourceUses.Any<ResourceUse>())
				{
					this.LogResourceChange(changedResourceUses);
					await this.RaiseResourceUseChangedEventAsync(changedResourceUses, this.currentRawResourseUses);
				}
				else
				{
					this.LogPeriodicResourceUse();
				}
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00007200 File Offset: 0x00005400
		public XElement GetDiagnosticInfo(bool verbose)
		{
			XElement xelement = new XElement("ResourceTracker");
			if (verbose)
			{
				this.UpdateDiagnostics();
			}
			if (this.aggregateResourceUse != null)
			{
				xelement.Add(new XElement("AggregateUse", this.aggregateResourceUse.CurrentUseLevel));
			}
			foreach (IResourceMeter resourceMeter in this.resourceMeters)
			{
				XElement xelement2 = new XElement("ResourceMeter", new XAttribute("Resource", resourceMeter.Resource.ToString()));
				xelement2.Add(new XElement("CurrentResourceUse", resourceMeter.ResourceUse.CurrentUseLevel));
				xelement2.Add(new XElement("PreviousResourceUse", resourceMeter.ResourceUse.PreviousUseLevel));
				xelement2.Add(new XElement("PressureTransitions", resourceMeter.PressureTransitions));
				xelement2.Add(new XElement("Pressure", resourceMeter.Pressure));
				if (verbose)
				{
					xelement2.Add(new XElement("CallDuration", this.diagnosticsData.GetResourceMeterCallDuration(resourceMeter.Resource).TotalMilliseconds));
				}
				xelement.Add(xelement2);
			}
			return xelement;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00007394 File Offset: 0x00005594
		public ResourceTrackerDiagnosticsData GetDiagnosticsData()
		{
			return this.diagnosticsData;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000739C File Offset: 0x0000559C
		private void LogPeriodicResourceUse()
		{
			if (DateTime.UtcNow - this.lastLogTime >= this.logInterval)
			{
				foreach (ResourceUse resourceUse in this.currentResourseUses)
				{
					this.resourceLog.LogResourceUsePeriodic(resourceUse, null);
				}
				this.lastLogTime = DateTime.UtcNow;
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00007438 File Offset: 0x00005638
		private void LogResourceChange(IEnumerable<ResourceUse> changedResourceUses)
		{
			using (IEnumerator<ResourceUse> enumerator = changedResourceUses.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ResourceUse changedResourceUse = enumerator.Current;
					Dictionary<string, object> dictionary = null;
					if (changedResourceUse.Resource.Name != "Aggregate")
					{
						PressureTransitions pressureTransitions = this.resourceMeters.Single((IResourceMeter resourceMeter) => resourceMeter.Resource.Equals(changedResourceUse.Resource)).PressureTransitions;
						dictionary = new Dictionary<string, object>();
						dictionary.Add("PressureTransition", pressureTransitions.ToString());
					}
					this.resourceLog.LogResourceUseChange(changedResourceUse, dictionary);
					this.lastLogTime = DateTime.UtcNow;
				}
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000076A0 File Offset: 0x000058A0
		private async Task<IList<ResourceUse>> GetUpdatedResourceUsesAsync()
		{
			await Operation.InvokeOperationsAsync(this.resourceTrackingOperations.Values, this.operationTimeout);
			this.currentRawResourseUses = from operation in this.resourceTrackingOperations.Values
			select operation.ResourceMeter.RawResourceUse;
			this.UpdateDiagnostics();
			return (from operation in this.resourceTrackingOperations.Values
			select operation.ResourceMeter.ResourceUse).ToList<ResourceUse>();
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000076E8 File Offset: 0x000058E8
		private void UpdateDiagnostics()
		{
			foreach (ResourceIdentifier resourceIdentifier in this.resourceTrackingOperations.Keys)
			{
				DelegatingInfoCollector delegatingInfoCollector = this.resourceTrackingOperations[resourceIdentifier].ExecutionInfo as DelegatingInfoCollector;
				foreach (IExecutionInfo executionInfo in delegatingInfoCollector.ExecutionInfos)
				{
					ExecutionTimeInfo executionTimeInfo = executionInfo as ExecutionTimeInfo;
					if (executionTimeInfo != null)
					{
						this.diagnosticsData.SetResourceMeterCallDuration(resourceIdentifier, executionTimeInfo.CallDuration);
					}
				}
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00007944 File Offset: 0x00005B44
		private async Task RaiseResourceUseChangedEventAsync(IEnumerable<ResourceUse> changedResourceUses, IEnumerable<ResourceUse> rawResourceUses)
		{
			ResourceUseChangedHandler resourceUseChanged = this.ResourceUseChanged;
			if (resourceUseChanged != null)
			{
				await Task.WhenAll(from ResourceUseChangedHandler handler in resourceUseChanged.GetInvocationList()
				select handler(this.currentResourseUses, changedResourceUses, rawResourceUses));
			}
		}

		// Token: 0x040000A5 RID: 165
		private readonly TimeSpan operationTimeout;

		// Token: 0x040000A6 RID: 166
		private readonly Dictionary<ResourceIdentifier, ResourceTrackingOperation> resourceTrackingOperations = new Dictionary<ResourceIdentifier, ResourceTrackingOperation>();

		// Token: 0x040000A7 RID: 167
		private readonly TimeSpan trackingInterval;

		// Token: 0x040000A8 RID: 168
		private readonly ResourceLog resourceLog;

		// Token: 0x040000A9 RID: 169
		private readonly TimeSpan logInterval;

		// Token: 0x040000AA RID: 170
		private DateTime lastLogTime = DateTime.MinValue;

		// Token: 0x040000AB RID: 171
		private ResourceUse aggregateResourceUse = new ResourceUse(new ResourceIdentifier("Aggregate", ""), UseLevel.Low, UseLevel.Low);

		// Token: 0x040000AC RID: 172
		private IEnumerable<ResourceUse> currentResourseUses;

		// Token: 0x040000AD RID: 173
		private IEnumerable<ResourceUse> currentRawResourseUses;

		// Token: 0x040000AE RID: 174
		private bool isTracking;

		// Token: 0x040000AF RID: 175
		private DateTime lastUpdateTime;

		// Token: 0x040000B0 RID: 176
		private ResourceTrackerDiagnosticsData diagnosticsData = new ResourceTrackerDiagnosticsData();

		// Token: 0x040000B1 RID: 177
		private IEnumerable<IResourceMeter> resourceMeters;

		// Token: 0x040000B2 RID: 178
		private Func<IEnumerable<ResourceUse>, UseLevel> aggregationFunc = delegate(IEnumerable<ResourceUse> resourceUses)
		{
			if (resourceUses.Any<ResourceUse>())
			{
				return resourceUses.Max((ResourceUse use) => use.CurrentUseLevel);
			}
			return UseLevel.Low;
		};
	}
}
