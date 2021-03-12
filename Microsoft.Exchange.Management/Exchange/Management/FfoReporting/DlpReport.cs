using System;
using Microsoft.Exchange.Management.FfoReporting.Common;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x0200038C RID: 908
	[Serializable]
	public class DlpReport : TrafficObject
	{
		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06001F9B RID: 8091 RVA: 0x00087C0B File Offset: 0x00085E0B
		// (set) Token: 0x06001F9C RID: 8092 RVA: 0x00087C13 File Offset: 0x00085E13
		[DalConversion("OrganizationFromTask", "Organization", new string[]
		{

		})]
		public string Organization { get; private set; }

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06001F9D RID: 8093 RVA: 0x00087C1C File Offset: 0x00085E1C
		// (set) Token: 0x06001F9E RID: 8094 RVA: 0x00087C24 File Offset: 0x00085E24
		[DalConversion("DateFromIntSerializer", "DateKey", new string[]
		{
			"HourKey"
		})]
		public DateTime Date { get; private set; }

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06001F9F RID: 8095 RVA: 0x00087C2D File Offset: 0x00085E2D
		// (set) Token: 0x06001FA0 RID: 8096 RVA: 0x00087C35 File Offset: 0x00085E35
		[DalConversion("DefaultSerializer", "PolicyName", new string[]
		{

		})]
		[ODataInput("DlpPolicy")]
		public string DlpPolicy { get; private set; }

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06001FA1 RID: 8097 RVA: 0x00087C3E File Offset: 0x00085E3E
		// (set) Token: 0x06001FA2 RID: 8098 RVA: 0x00087C46 File Offset: 0x00085E46
		[DalConversion("DefaultSerializer", "RuleName", new string[]
		{

		})]
		[ODataInput("TransportRule")]
		public string TransportRule { get; private set; }

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06001FA3 RID: 8099 RVA: 0x00087C4F File Offset: 0x00085E4F
		// (set) Token: 0x06001FA4 RID: 8100 RVA: 0x00087C57 File Offset: 0x00085E57
		[ODataInput("Action")]
		[DalConversion("DefaultSerializer", "Action", new string[]
		{

		})]
		public string Action { get; private set; }

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06001FA5 RID: 8101 RVA: 0x00087C60 File Offset: 0x00085E60
		// (set) Token: 0x06001FA6 RID: 8102 RVA: 0x00087C68 File Offset: 0x00085E68
		[ODataInput("EventType")]
		[DalConversion("DefaultSerializer", "EventType", new string[]
		{

		})]
		public string EventType { get; private set; }

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06001FA7 RID: 8103 RVA: 0x00087C71 File Offset: 0x00085E71
		// (set) Token: 0x06001FA8 RID: 8104 RVA: 0x00087C79 File Offset: 0x00085E79
		[DalConversion("DefaultSerializer", "MessageCount", new string[]
		{

		})]
		public int MessageCount { get; private set; }

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06001FA9 RID: 8105 RVA: 0x00087C82 File Offset: 0x00085E82
		// (set) Token: 0x06001FAA RID: 8106 RVA: 0x00087C8A File Offset: 0x00085E8A
		[ODataInput("SummarizeBy")]
		public string SummarizeBy { get; private set; }

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06001FAB RID: 8107 RVA: 0x00087C93 File Offset: 0x00085E93
		// (set) Token: 0x06001FAC RID: 8108 RVA: 0x00087C9B File Offset: 0x00085E9B
		[DalConversion("DefaultSerializer", "DataSource", new string[]
		{

		})]
		[ODataInput("Source")]
		public string Source { get; private set; }
	}
}
