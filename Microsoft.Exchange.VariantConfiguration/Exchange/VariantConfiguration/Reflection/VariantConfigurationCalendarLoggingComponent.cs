using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x020000FD RID: 253
	public sealed class VariantConfigurationCalendarLoggingComponent : VariantConfigurationComponent
	{
		// Token: 0x06000B09 RID: 2825 RVA: 0x00019DD0 File Offset: 0x00017FD0
		internal VariantConfigurationCalendarLoggingComponent() : base("CalendarLogging")
		{
			base.Add(new VariantConfigurationSection("CalendarLogging.settings.ini", "FixMissingMeetingBody", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("CalendarLogging.settings.ini", "CalendarLoggingIncludeSeriesMeetingMessagesInCVS", typeof(IFeature), false));
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x00019E28 File Offset: 0x00018028
		public VariantConfigurationSection FixMissingMeetingBody
		{
			get
			{
				return base["FixMissingMeetingBody"];
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x00019E35 File Offset: 0x00018035
		public VariantConfigurationSection CalendarLoggingIncludeSeriesMeetingMessagesInCVS
		{
			get
			{
				return base["CalendarLoggingIncludeSeriesMeetingMessagesInCVS"];
			}
		}
	}
}
