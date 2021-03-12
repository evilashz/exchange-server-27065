using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Metering.ResourceMonitoring;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.RecipientAPI;

namespace Microsoft.Exchange.Transport.ResourceThrottling
{
	// Token: 0x02000024 RID: 36
	internal class IsMofRResourceLevelObserver : IResourceLevelObserver
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x00003E68 File Offset: 0x00002068
		public IsMofRResourceLevelObserver(IsMemberOfResolverComponent<RoutingAddress> isMemberOfResolver, IComponentsWrapper componentsWrapper)
		{
			ArgumentValidator.ThrowIfNull("isMemberOfResolver", isMemberOfResolver);
			ArgumentValidator.ThrowIfNull("componentsWrapper", componentsWrapper);
			this.isMemberOfResolver = isMemberOfResolver;
			this.componentsWrapper = componentsWrapper;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003EB4 File Offset: 0x000020B4
		public virtual void HandleResourceChange(IEnumerable<ResourceUse> allResourceUses, IEnumerable<ResourceUse> changedResourceUses, IEnumerable<ResourceUse> rawResourceUses)
		{
			ArgumentValidator.ThrowIfNull("allResourceUses", allResourceUses);
			ArgumentValidator.ThrowIfNull("changedResourceUses", changedResourceUses);
			ArgumentValidator.ThrowIfNull("rawResourceUses", rawResourceUses);
			ResourceUse resourceUse = ResourceHelper.TryGetResourceUse(allResourceUses, this.privateBytesResource);
			if (resourceUse == null)
			{
				return;
			}
			if (resourceUse.CurrentUseLevel != UseLevel.Low && resourceUse.CurrentUseLevel > resourceUse.PreviousUseLevel && !this.componentsWrapper.IsShuttingDown && this.componentsWrapper.IsActive)
			{
				lock (this.componentsWrapper.SyncRoot)
				{
					if (this.isMemberOfResolver != null && !this.componentsWrapper.IsShuttingDown && this.componentsWrapper.IsActive)
					{
						this.isMemberOfResolver.ClearCache();
					}
				}
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003F84 File Offset: 0x00002184
		public string Name
		{
			get
			{
				return "IsMemberOfResolver";
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003F8B File Offset: 0x0000218B
		public bool Paused
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003F8E File Offset: 0x0000218E
		public string SubStatus
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x04000050 RID: 80
		internal const string ResourceObserverName = "IsMemberOfResolver";

		// Token: 0x04000051 RID: 81
		private readonly IsMemberOfResolverComponent<RoutingAddress> isMemberOfResolver;

		// Token: 0x04000052 RID: 82
		private readonly IComponentsWrapper componentsWrapper;

		// Token: 0x04000053 RID: 83
		private readonly ResourceIdentifier privateBytesResource = new ResourceIdentifier("PrivateBytes", "");
	}
}
