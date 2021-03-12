using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Metering.ResourceMonitoring;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Categorizer;

namespace Microsoft.Exchange.Transport.ResourceThrottling
{
	// Token: 0x0200001C RID: 28
	internal class EnhancedDnsResourceLevelObserver : IResourceLevelObserver
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00003C00 File Offset: 0x00001E00
		public EnhancedDnsResourceLevelObserver(EnhancedDns enhancedDns, IComponentsWrapper componentsWrapper)
		{
			ArgumentValidator.ThrowIfNull("enhancedDns", enhancedDns);
			ArgumentValidator.ThrowIfNull("componentsWrapper", componentsWrapper);
			this.enhancedDns = enhancedDns;
			this.componentsWrapper = componentsWrapper;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003C4C File Offset: 0x00001E4C
		public virtual void HandleResourceChange(IEnumerable<ResourceUse> allResourceUses, IEnumerable<ResourceUse> changedResourceUses, IEnumerable<ResourceUse> rawResourceUses)
		{
			ArgumentValidator.ThrowIfNull("allResourceUses", allResourceUses);
			ArgumentValidator.ThrowIfNull("changedResourceUses", changedResourceUses);
			ArgumentValidator.ThrowIfNull("rawResourceUses", rawResourceUses);
			UseLevel useLevel = ResourceHelper.TryGetCurrentUseLevel(allResourceUses, this.privateBytesResource, UseLevel.Low);
			if (useLevel != UseLevel.Low)
			{
				this.enhancedDns.FlushCache();
				if (this.componentsWrapper.IsBridgeHead)
				{
					Schema.FlushCache();
				}
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003CA8 File Offset: 0x00001EA8
		public string Name
		{
			get
			{
				return "EnhancedDns";
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003CAF File Offset: 0x00001EAF
		public bool Paused
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003CB2 File Offset: 0x00001EB2
		public string SubStatus
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x0400004A RID: 74
		internal const string ResourceObserverName = "EnhancedDns";

		// Token: 0x0400004B RID: 75
		private readonly EnhancedDns enhancedDns;

		// Token: 0x0400004C RID: 76
		private readonly IComponentsWrapper componentsWrapper;

		// Token: 0x0400004D RID: 77
		private readonly ResourceIdentifier privateBytesResource = new ResourceIdentifier("PrivateBytes", "");
	}
}
