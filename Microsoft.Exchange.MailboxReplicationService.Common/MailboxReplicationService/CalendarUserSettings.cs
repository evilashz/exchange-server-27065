using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000006 RID: 6
	[DataContract]
	internal sealed class CalendarUserSettings : ItemPropertiesBase
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000027B8 File Offset: 0x000009B8
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000027C0 File Offset: 0x000009C0
		[DataMember]
		public bool IsClock24Hour { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000027C9 File Offset: 0x000009C9
		// (set) Token: 0x0600006F RID: 111 RVA: 0x000027D1 File Offset: 0x000009D1
		[DataMember]
		public string FirstDayOfWeek { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000027DA File Offset: 0x000009DA
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000027E2 File Offset: 0x000009E2
		[DataMember]
		public string StartTimeOfDay { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000027EB File Offset: 0x000009EB
		// (set) Token: 0x06000073 RID: 115 RVA: 0x000027F3 File Offset: 0x000009F3
		[DataMember]
		public bool IsWeatherInCelsius { get; set; }
	}
}
