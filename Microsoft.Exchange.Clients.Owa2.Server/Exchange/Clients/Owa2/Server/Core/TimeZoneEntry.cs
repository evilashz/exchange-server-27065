using System;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001E7 RID: 487
	[DataContract]
	public class TimeZoneEntry
	{
		// Token: 0x0600112A RID: 4394 RVA: 0x000420AF File Offset: 0x000402AF
		public TimeZoneEntry(string value, string name)
		{
			this.Value = value;
			this.Name = name;
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x000420C8 File Offset: 0x000402C8
		public TimeZoneEntry(ExTimeZone timezone) : this(timezone.Id, timezone.LocalizableDisplayName.ToString(CultureInfo.CurrentUICulture))
		{
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x000420F4 File Offset: 0x000402F4
		// (set) Token: 0x0600112D RID: 4397 RVA: 0x000420FC File Offset: 0x000402FC
		[DataMember]
		public string Name { get; set; }

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x00042105 File Offset: 0x00040305
		// (set) Token: 0x0600112F RID: 4399 RVA: 0x0004210D File Offset: 0x0004030D
		[DataMember]
		public string Value { get; set; }
	}
}
