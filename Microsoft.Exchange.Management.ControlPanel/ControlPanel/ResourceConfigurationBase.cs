using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000AC RID: 172
	[DataContract]
	public class ResourceConfigurationBase : BaseRow
	{
		// Token: 0x06001C3F RID: 7231 RVA: 0x0005863C File Offset: 0x0005683C
		public ResourceConfigurationBase(CalendarConfiguration calendarConfiguration) : base(calendarConfiguration)
		{
			this.CalendarConfiguration = calendarConfiguration;
		}

		// Token: 0x170018E2 RID: 6370
		// (get) Token: 0x06001C40 RID: 7232 RVA: 0x0005864C File Offset: 0x0005684C
		// (set) Token: 0x06001C41 RID: 7233 RVA: 0x00058654 File Offset: 0x00056854
		public CalendarConfiguration CalendarConfiguration { get; private set; }
	}
}
