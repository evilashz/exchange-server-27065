using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Exchange.Entities.HolidayCalendars.Configuration
{
	// Token: 0x02000006 RID: 6
	internal class EndpointInformation
	{
		// Token: 0x06000014 RID: 20 RVA: 0x0000237F File Offset: 0x0000057F
		public EndpointInformation(Uri baseUrl, IReadOnlyDictionary<int, int> calendarVersionMapping, IReadOnlyList<CultureInfo> availableCultures, Version dropVersion)
		{
			this.BaseUrl = baseUrl;
			this.CalendarVersionMapping = calendarVersionMapping;
			this.AvailableCultures = availableCultures;
			this.DropVersion = dropVersion;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000023A4 File Offset: 0x000005A4
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000023AC File Offset: 0x000005AC
		public Uri BaseUrl { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000023B5 File Offset: 0x000005B5
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000023BD File Offset: 0x000005BD
		public IReadOnlyDictionary<int, int> CalendarVersionMapping { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000023C6 File Offset: 0x000005C6
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000023CE File Offset: 0x000005CE
		public IReadOnlyList<CultureInfo> AvailableCultures { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000023D7 File Offset: 0x000005D7
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000023DF File Offset: 0x000005DF
		public Version DropVersion { get; private set; }
	}
}
