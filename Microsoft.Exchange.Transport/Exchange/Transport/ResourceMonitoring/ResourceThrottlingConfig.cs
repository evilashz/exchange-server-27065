using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.Exchange.Transport.Common;

namespace Microsoft.Exchange.Transport.ResourceMonitoring
{
	// Token: 0x020002ED RID: 749
	internal class ResourceThrottlingConfig : TransportAppConfig
	{
		// Token: 0x06002141 RID: 8513 RVA: 0x0007E1F8 File Offset: 0x0007C3F8
		public ResourceThrottlingConfig(NameValueCollection appSettings = null) : base(appSettings)
		{
			this.isResourceThrottlingEnabled = base.GetConfigBool("ResourceThrottlingEnabled", false);
			if (this.isResourceThrottlingEnabled)
			{
				this.disabledObserverNames = this.GetDisabledResourceLevelObservers();
				this.maxTransientExceptionsAllowed = base.GetConfigInt("MaxTransientExceptionsAllowed", 1, 100, 5);
				this.resourceObserverTimeout = base.GetConfigTimeSpan("ResourceLevelObserverTimeout", TimeSpan.FromMilliseconds(500.0), TimeSpan.FromMinutes(30.0), TimeSpan.FromMinutes(5.0));
				this.maxThrottlingDelayInterval = base.GetConfigTimeSpan("SmtpMaxThrottlingDelayInterval", TimeSpan.Zero, TimeSpan.FromMinutes(10.0), TimeSpan.FromSeconds(55.0));
				this.baseThrottlingDelayInterval = base.GetConfigTimeSpan("SmtpBaseThrottlingDelayInterval", TimeSpan.Zero, this.maxThrottlingDelayInterval, TimeSpan.Zero);
				this.stepThrottlingDelayInterval = base.GetConfigTimeSpan("SmtpStepThrottlingDelayInterval", TimeSpan.Zero, this.maxThrottlingDelayInterval, TimeSpan.FromSeconds(1.0));
				this.startThrottlingDelayInterval = base.GetConfigTimeSpan("SmtpStartThrottlingDelayInterval", TimeSpan.Zero, this.maxThrottlingDelayInterval, TimeSpan.FromSeconds(1.0));
				this.dehydrateMessagesUnderMemoryPressure = base.GetConfigBool("DehydrateMessagesUnderMemoryPressure", true);
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06002142 RID: 8514 RVA: 0x0007E33E File Offset: 0x0007C53E
		public IEnumerable<string> DisabledResourceLevelObservers
		{
			get
			{
				return this.disabledObserverNames;
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06002143 RID: 8515 RVA: 0x0007E346 File Offset: 0x0007C546
		public TimeSpan MaxThrottlingDelayInterval
		{
			get
			{
				return this.maxThrottlingDelayInterval;
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06002144 RID: 8516 RVA: 0x0007E34E File Offset: 0x0007C54E
		public TimeSpan BaseThrottlingDelayInterval
		{
			get
			{
				return this.baseThrottlingDelayInterval;
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06002145 RID: 8517 RVA: 0x0007E356 File Offset: 0x0007C556
		public TimeSpan StepThrottlingDelayInterval
		{
			get
			{
				return this.stepThrottlingDelayInterval;
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06002146 RID: 8518 RVA: 0x0007E35E File Offset: 0x0007C55E
		public TimeSpan StartThrottlingDelayInterval
		{
			get
			{
				return this.startThrottlingDelayInterval;
			}
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06002147 RID: 8519 RVA: 0x0007E366 File Offset: 0x0007C566
		public TimeSpan ResourceObserverTimeout
		{
			get
			{
				return this.resourceObserverTimeout;
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06002148 RID: 8520 RVA: 0x0007E36E File Offset: 0x0007C56E
		public int MaxTransientExceptionsAllowed
		{
			get
			{
				return this.maxTransientExceptionsAllowed;
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06002149 RID: 8521 RVA: 0x0007E376 File Offset: 0x0007C576
		public bool DehydrateMessagesUnderMemoryPressure
		{
			get
			{
				return this.dehydrateMessagesUnderMemoryPressure;
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x0600214A RID: 8522 RVA: 0x0007E37E File Offset: 0x0007C57E
		public bool IsResourceThrottlingEnabled
		{
			get
			{
				return this.isResourceThrottlingEnabled;
			}
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x0007E388 File Offset: 0x0007C588
		private IEnumerable<string> GetDisabledResourceLevelObservers()
		{
			return base.GetConfigString("DisabledResourceLevelObservers", string.Empty).Split(new char[]
			{
				';'
			});
		}

		// Token: 0x0400116A RID: 4458
		private readonly bool isResourceThrottlingEnabled;

		// Token: 0x0400116B RID: 4459
		private readonly IEnumerable<string> disabledObserverNames;

		// Token: 0x0400116C RID: 4460
		private readonly int maxTransientExceptionsAllowed;

		// Token: 0x0400116D RID: 4461
		private readonly TimeSpan resourceObserverTimeout;

		// Token: 0x0400116E RID: 4462
		private readonly TimeSpan maxThrottlingDelayInterval;

		// Token: 0x0400116F RID: 4463
		private readonly TimeSpan baseThrottlingDelayInterval;

		// Token: 0x04001170 RID: 4464
		private readonly TimeSpan stepThrottlingDelayInterval;

		// Token: 0x04001171 RID: 4465
		private readonly TimeSpan startThrottlingDelayInterval;

		// Token: 0x04001172 RID: 4466
		private readonly bool dehydrateMessagesUnderMemoryPressure;
	}
}
