using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000120 RID: 288
	public sealed class VariantConfigurationSharedMailboxComponent : VariantConfigurationComponent
	{
		// Token: 0x06000D7F RID: 3455 RVA: 0x0002098C File Offset: 0x0001EB8C
		internal VariantConfigurationSharedMailboxComponent() : base("SharedMailbox")
		{
			base.Add(new VariantConfigurationSection("SharedMailbox.settings.ini", "SharedMailboxSentItemCopy", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("SharedMailbox.settings.ini", "SharedMailboxSentItemsRoutingAgent", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("SharedMailbox.settings.ini", "SharedMailboxSentItemsDeliveryAgent", typeof(IFeature), false));
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x00020A04 File Offset: 0x0001EC04
		public VariantConfigurationSection SharedMailboxSentItemCopy
		{
			get
			{
				return base["SharedMailboxSentItemCopy"];
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x00020A11 File Offset: 0x0001EC11
		public VariantConfigurationSection SharedMailboxSentItemsRoutingAgent
		{
			get
			{
				return base["SharedMailboxSentItemsRoutingAgent"];
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x00020A1E File Offset: 0x0001EC1E
		public VariantConfigurationSection SharedMailboxSentItemsDeliveryAgent
		{
			get
			{
				return base["SharedMailboxSentItemsDeliveryAgent"];
			}
		}
	}
}
