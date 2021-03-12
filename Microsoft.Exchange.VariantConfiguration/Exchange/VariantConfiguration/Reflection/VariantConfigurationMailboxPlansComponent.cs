using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x0200010F RID: 271
	public sealed class VariantConfigurationMailboxPlansComponent : VariantConfigurationComponent
	{
		// Token: 0x06000C88 RID: 3208 RVA: 0x0001DF9C File Offset: 0x0001C19C
		internal VariantConfigurationMailboxPlansComponent() : base("MailboxPlans")
		{
			base.Add(new VariantConfigurationSection("MailboxPlans.settings.ini", "CloneLimitedSetOfMailboxPlanProperties", typeof(IFeature), false));
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x0001DFC9 File Offset: 0x0001C1C9
		public VariantConfigurationSection CloneLimitedSetOfMailboxPlanProperties
		{
			get
			{
				return base["CloneLimitedSetOfMailboxPlanProperties"];
			}
		}
	}
}
