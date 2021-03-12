using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001E8 RID: 488
	[DataContract]
	public class TimeZoneConfiguration
	{
		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001131 RID: 4401 RVA: 0x0004211E File Offset: 0x0004031E
		// (set) Token: 0x06001132 RID: 4402 RVA: 0x00042126 File Offset: 0x00040326
		[DataMember(IsRequired = false)]
		public string CurrentTimeZone { get; set; }

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x0004212F File Offset: 0x0004032F
		// (set) Token: 0x06001134 RID: 4404 RVA: 0x00042137 File Offset: 0x00040337
		[DataMember]
		public TimeZoneEntry[] TimeZoneList { get; set; }
	}
}
