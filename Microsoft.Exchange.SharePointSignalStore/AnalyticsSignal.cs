using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.SharePointSignalStore
{
	// Token: 0x02000002 RID: 2
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract(Namespace = "http://www.microsoft.com/sharepoint/search/KnownTypes/2011/01")]
	[Serializable]
	internal sealed class AnalyticsSignal
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		// (set) Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		[DataMember]
		public string Source { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020E1 File Offset: 0x000002E1
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020E9 File Offset: 0x000002E9
		[DataMember]
		public AnalyticsSignal.AnalyticsActor Actor { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020F2 File Offset: 0x000002F2
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020FA File Offset: 0x000002FA
		[DataMember]
		public AnalyticsSignal.AnalyticsAction Action { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002103 File Offset: 0x00000303
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000210B File Offset: 0x0000030B
		[DataMember]
		public AnalyticsSignal.AnalyticsItem Item { get; set; }

		// Token: 0x02000003 RID: 3
		[DataContract(Namespace = "http://www.microsoft.com/sharepoint/search/KnownTypes/2011/01")]
		[Serializable]
		internal sealed class AnalyticsActor
		{
			// Token: 0x17000005 RID: 5
			// (get) Token: 0x0600000A RID: 10 RVA: 0x0000211C File Offset: 0x0000031C
			// (set) Token: 0x0600000B RID: 11 RVA: 0x00002124 File Offset: 0x00000324
			[DataMember]
			public string Id { get; set; }

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x0600000C RID: 12 RVA: 0x0000212D File Offset: 0x0000032D
			// (set) Token: 0x0600000D RID: 13 RVA: 0x00002135 File Offset: 0x00000335
			[DataMember]
			public Dictionary<string, object> Properties { get; set; }

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600000E RID: 14 RVA: 0x0000213E File Offset: 0x0000033E
			// (set) Token: 0x0600000F RID: 15 RVA: 0x00002146 File Offset: 0x00000346
			[DataMember]
			public Guid TenantId { get; set; }
		}

		// Token: 0x02000004 RID: 4
		[DataContract(Namespace = "http://www.microsoft.com/sharepoint/search/KnownTypes/2011/01")]
		[Serializable]
		internal sealed class AnalyticsAction
		{
			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000011 RID: 17 RVA: 0x00002157 File Offset: 0x00000357
			// (set) Token: 0x06000012 RID: 18 RVA: 0x0000215F File Offset: 0x0000035F
			[DataMember]
			public string ActionType { get; set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000013 RID: 19 RVA: 0x00002168 File Offset: 0x00000368
			// (set) Token: 0x06000014 RID: 20 RVA: 0x00002170 File Offset: 0x00000370
			[DataMember]
			public DateTime UserTime { get; set; }

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000015 RID: 21 RVA: 0x00002179 File Offset: 0x00000379
			// (set) Token: 0x06000016 RID: 22 RVA: 0x00002181 File Offset: 0x00000381
			[DataMember]
			public DateTime ExpireTime { get; set; }

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000017 RID: 23 RVA: 0x0000218A File Offset: 0x0000038A
			// (set) Token: 0x06000018 RID: 24 RVA: 0x00002192 File Offset: 0x00000392
			[DataMember]
			public Dictionary<string, object> Properties { get; set; }
		}

		// Token: 0x02000005 RID: 5
		[DataContract(Namespace = "http://www.microsoft.com/sharepoint/search/KnownTypes/2011/01")]
		[Serializable]
		internal sealed class AnalyticsItem
		{
			// Token: 0x1700000C RID: 12
			// (get) Token: 0x0600001A RID: 26 RVA: 0x000021A3 File Offset: 0x000003A3
			// (set) Token: 0x0600001B RID: 27 RVA: 0x000021AB File Offset: 0x000003AB
			[DataMember]
			public string Id { get; set; }

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x0600001C RID: 28 RVA: 0x000021B4 File Offset: 0x000003B4
			// (set) Token: 0x0600001D RID: 29 RVA: 0x000021BC File Offset: 0x000003BC
			[DataMember]
			public Dictionary<string, object> Properties { get; set; }
		}
	}
}
