using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Metering.ResourceMonitoring;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.ResourceThrottling
{
	// Token: 0x02000002 RID: 2
	internal class ResourceLevelMediator
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020E8 File Offset: 0x000002E8
		public ResourceLevelMediator(IResourceTracker resourceTracker, IEnumerable<IResourceLevelObserver> resourceLevelObservers, TimeSpan operationTimeout, int maxTransientExceptions = 5)
		{
			ArgumentValidator.ThrowIfNull("resourceTracker", resourceTracker);
			ArgumentValidator.ThrowIfNull("resourceLevelObservers", resourceLevelObservers);
			ArgumentValidator.ThrowIfInvalidValue<IEnumerable<IResourceLevelObserver>>("resourceLevelObservers", resourceLevelObservers, (IEnumerable<IResourceLevelObserver> observers) => observers.Any<IResourceLevelObserver>());
			ArgumentValidator.ThrowIfInvalidValue<TimeSpan>("operationTimeout", operationTimeout, (TimeSpan timeout) => timeout > TimeSpan.Zero);
			ArgumentValidator.ThrowIfZeroOrNegative("maxTransientExceptions", maxTransientExceptions);
			this.resourceLevelObservers = resourceLevelObservers;
			this.operationTimeout = operationTimeout;
			foreach (IResourceLevelObserver resourceLevelObserver in resourceLevelObservers)
			{
				if (resourceLevelObserver != null)
				{
					if (this.resourceObserverOperations.ContainsKey(resourceLevelObserver.Name))
					{
						throw new ArgumentException(string.Format("Duplicate Resource Level Observer : {0}", resourceLevelObserver.Name), "resourceLevelObservers");
					}
					DelegatingInfoCollector executionInfo = new DelegatingInfoCollector(new List<IExecutionInfo>
					{
						new ExecutionTimeInfo()
					});
					this.resourceObserverOperations.Add(resourceLevelObserver.Name, new ResourceObserverOperation(resourceLevelObserver, executionInfo, maxTransientExceptions));
				}
			}
			resourceTracker.ResourceUseChanged += this.OnResourceUseChanged;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x0000223C File Offset: 0x0000043C
		public IEnumerable<IResourceLevelObserver> ResourceLevelObservers
		{
			get
			{
				return this.resourceLevelObservers;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002244 File Offset: 0x00000444
		public Task OnResourceUseChanged(IEnumerable<ResourceUse> allResourceUses, IEnumerable<ResourceUse> changedResourceUses, IEnumerable<ResourceUse> rawResourceUses)
		{
			return ResourceObserverOperation.InvokeResourceObserverOperations(this.resourceObserverOperations.Values, allResourceUses, changedResourceUses, rawResourceUses, this.operationTimeout);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002260 File Offset: 0x00000460
		public XElement GetDiagnosticInfo(bool verbose)
		{
			if (verbose)
			{
				this.UpdateDiagnostics();
			}
			XElement xelement = new XElement("ResourceLevelMediator");
			foreach (IResourceLevelObserver resourceLevelObserver in this.resourceLevelObservers)
			{
				XElement xelement2 = new XElement("ResourceLevelObserver", new XAttribute("Name", resourceLevelObserver.Name));
				xelement2.Add(new XElement("Paused", resourceLevelObserver.Paused));
				xelement2.Add(new XElement("SubStatus", resourceLevelObserver.SubStatus));
				if (verbose)
				{
					xelement2.Add(new XElement("CallDuration", this.diagnosticsData.GetResourceObserverCallDuration(resourceLevelObserver.Name).TotalMilliseconds));
				}
				xelement.Add(xelement2);
			}
			return xelement;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002364 File Offset: 0x00000564
		public ResourceLevelMediatorDiagnosticsData GetDiagnosticsData()
		{
			this.UpdateDiagnostics();
			return this.diagnosticsData;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002374 File Offset: 0x00000574
		private void UpdateDiagnostics()
		{
			foreach (string text in this.resourceObserverOperations.Keys)
			{
				DelegatingInfoCollector delegatingInfoCollector = this.resourceObserverOperations[text].ExecutionInfo as DelegatingInfoCollector;
				foreach (IExecutionInfo executionInfo in delegatingInfoCollector.ExecutionInfos)
				{
					ExecutionTimeInfo executionTimeInfo = executionInfo as ExecutionTimeInfo;
					if (executionTimeInfo != null)
					{
						this.diagnosticsData.SetResourceObserverCallDuration(text, executionTimeInfo.CallDuration);
					}
				}
			}
		}

		// Token: 0x04000001 RID: 1
		private readonly IEnumerable<IResourceLevelObserver> resourceLevelObservers;

		// Token: 0x04000002 RID: 2
		private readonly TimeSpan operationTimeout;

		// Token: 0x04000003 RID: 3
		private Dictionary<string, ResourceObserverOperation> resourceObserverOperations = new Dictionary<string, ResourceObserverOperation>();

		// Token: 0x04000004 RID: 4
		private ResourceLevelMediatorDiagnosticsData diagnosticsData = new ResourceLevelMediatorDiagnosticsData();
	}
}
