using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.Exchange.MessageDepot;
using Microsoft.Exchange.Transport.Common;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020002D9 RID: 729
	internal class MessageDepotConfig : TransportAppConfig
	{
		// Token: 0x0600204C RID: 8268 RVA: 0x0007BB98 File Offset: 0x00079D98
		public MessageDepotConfig(NameValueCollection appSettings = null) : base(appSettings)
		{
			this.isMessageDepotEnabled = base.GetConfigBool("MessageDepotEnabled", false);
			if (!this.IsMessageDepotEnabled)
			{
				IEnumerable<Microsoft.Exchange.MessageDepot.DayOfWeek> enabledOnDaysOfWeek = VariantConfiguration.InvariantNoFlightingSnapshot.Transport.MessageDepot.EnabledOnDaysOfWeek;
				if (VariantConfiguration.InvariantNoFlightingSnapshot.Transport.MessageDepot.Enabled && enabledOnDaysOfWeek != null)
				{
					if (enabledOnDaysOfWeek.Any((Microsoft.Exchange.MessageDepot.DayOfWeek dayOfWeek) => string.Equals(dayOfWeek.ToString("G"), DateTime.UtcNow.DayOfWeek.ToString("G"))))
					{
						this.isMessageDepotEnabledByVariantConfig = true;
						this.isMessageDepotEnabled = true;
					}
				}
			}
			this.delayNotificationTimeout = base.GetConfigTimeSpan("DelayNotificationTimeout", MessageDepotConfig.MinDelayNotificationTimeout, MessageDepotConfig.MaxDelayNotificationTimeout, MessageDepotConfig.DefaultDelayNotificationTimeout);
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x0600204D RID: 8269 RVA: 0x0007BC4D File Offset: 0x00079E4D
		public bool IsMessageDepotEnabled
		{
			get
			{
				return this.isMessageDepotEnabled;
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x0600204E RID: 8270 RVA: 0x0007BC55 File Offset: 0x00079E55
		public TimeSpan DelayNotificationTimeout
		{
			get
			{
				return this.delayNotificationTimeout;
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x0600204F RID: 8271 RVA: 0x0007BC5D File Offset: 0x00079E5D
		public bool IsMessageDepotEnabledByVariantConfig
		{
			get
			{
				return this.isMessageDepotEnabledByVariantConfig;
			}
		}

		// Token: 0x040010E8 RID: 4328
		public const string MessageDepotEnabledLabel = "MessageDepotEnabled";

		// Token: 0x040010E9 RID: 4329
		public const string DelayNotificationTimeoutLabel = "DelayNotificationTimeout";

		// Token: 0x040010EA RID: 4330
		public const bool DefaultMessageDepotEnabled = false;

		// Token: 0x040010EB RID: 4331
		public static readonly TimeSpan MinDelayNotificationTimeout = TimeSpan.FromHours(1.0);

		// Token: 0x040010EC RID: 4332
		public static readonly TimeSpan DefaultDelayNotificationTimeout = TimeSpan.FromHours(4.0);

		// Token: 0x040010ED RID: 4333
		public static readonly TimeSpan MaxDelayNotificationTimeout = TimeSpan.FromDays(2.0);

		// Token: 0x040010EE RID: 4334
		private readonly bool isMessageDepotEnabled;

		// Token: 0x040010EF RID: 4335
		private readonly TimeSpan delayNotificationTimeout;

		// Token: 0x040010F0 RID: 4336
		private readonly bool isMessageDepotEnabledByVariantConfig;
	}
}
