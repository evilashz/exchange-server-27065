using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000115 RID: 277
	public sealed class VariantConfigurationNotificationBrokerServiceComponent : VariantConfigurationComponent
	{
		// Token: 0x06000CAA RID: 3242 RVA: 0x0001E508 File Offset: 0x0001C708
		internal VariantConfigurationNotificationBrokerServiceComponent() : base("NotificationBrokerService")
		{
			base.Add(new VariantConfigurationSection("NotificationBrokerService.settings.ini", "Service", typeof(IFeature), false));
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0001E535 File Offset: 0x0001C735
		public VariantConfigurationSection Service
		{
			get
			{
				return base["Service"];
			}
		}
	}
}
