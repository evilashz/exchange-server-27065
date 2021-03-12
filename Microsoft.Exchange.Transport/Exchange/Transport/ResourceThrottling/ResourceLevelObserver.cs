using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Metering.ResourceMonitoring;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.ResourceThrottling
{
	// Token: 0x0200000E RID: 14
	internal class ResourceLevelObserver : IResourceLevelObserver
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public ResourceLevelObserver(string componentName, IStartableTransportComponent transportComponent, IEnumerable<ResourceIdentifier> observedResources = null)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("componentName", componentName);
			ArgumentValidator.ThrowIfNull("transportComponent", transportComponent);
			this.resourceObserverName = componentName;
			this.transportComponent = transportComponent;
			if (observedResources != null && observedResources.Any<ResourceIdentifier>())
			{
				this.observedResources = observedResources;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002B53 File Offset: 0x00000D53
		public IStartableTransportComponent TransportComponent
		{
			get
			{
				return this.transportComponent;
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002BB4 File Offset: 0x00000DB4
		public virtual void HandleResourceChange(IEnumerable<ResourceUse> allResourceUses, IEnumerable<ResourceUse> changedResourceUses, IEnumerable<ResourceUse> rawResourceUses)
		{
			ArgumentValidator.ThrowIfNull("allResourceUses", allResourceUses);
			ArgumentValidator.ThrowIfNull("changedResourceUses", changedResourceUses);
			ArgumentValidator.ThrowIfNull("rawResourceUses", rawResourceUses);
			IEnumerable<ResourceUse> source = from resourceUse in allResourceUses
			where this.observedResources.Any((ResourceIdentifier resource) => resourceUse.Resource.Equals(resource))
			select resourceUse;
			if (source.Any<ResourceUse>())
			{
				if (source.All((ResourceUse resourceUse) => resourceUse.CurrentUseLevel == UseLevel.Low))
				{
					if (this.componentPaused)
					{
						this.transportComponent.Continue();
						this.componentPaused = false;
						return;
					}
				}
				else if (!this.componentPaused)
				{
					this.transportComponent.Pause();
					this.componentPaused = true;
				}
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002C57 File Offset: 0x00000E57
		public string Name
		{
			get
			{
				return this.resourceObserverName;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002C5F File Offset: 0x00000E5F
		public bool Paused
		{
			get
			{
				return this.componentPaused;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002C67 File Offset: 0x00000E67
		public virtual string SubStatus
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x04000023 RID: 35
		private readonly IStartableTransportComponent transportComponent;

		// Token: 0x04000024 RID: 36
		private readonly string resourceObserverName;

		// Token: 0x04000025 RID: 37
		private bool componentPaused;

		// Token: 0x04000026 RID: 38
		private readonly IEnumerable<ResourceIdentifier> observedResources = new List<ResourceIdentifier>
		{
			new ResourceIdentifier("Aggregate", "")
		};
	}
}
