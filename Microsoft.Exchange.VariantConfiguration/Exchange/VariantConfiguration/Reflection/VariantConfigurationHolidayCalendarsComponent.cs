using System;
using Microsoft.Exchange.HolidayCalendars;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000109 RID: 265
	public sealed class VariantConfigurationHolidayCalendarsComponent : VariantConfigurationComponent
	{
		// Token: 0x06000C16 RID: 3094 RVA: 0x0001CC12 File Offset: 0x0001AE12
		internal VariantConfigurationHolidayCalendarsComponent() : base("HolidayCalendars")
		{
			base.Add(new VariantConfigurationSection("HolidayCalendars.settings.ini", "HostConfiguration", typeof(IHostSettings), true));
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x0001CC3F File Offset: 0x0001AE3F
		public VariantConfigurationSection HostConfiguration
		{
			get
			{
				return base["HostConfiguration"];
			}
		}
	}
}
