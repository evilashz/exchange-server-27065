using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000036 RID: 54
	public class Breadcrumb
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000C1FF File Offset: 0x0000A3FF
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x0000C207 File Offset: 0x0000A407
		public BreadcrumbKind Kind { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000C210 File Offset: 0x0000A410
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x0000C218 File Offset: 0x0000A418
		public byte Source { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000C221 File Offset: 0x0000A421
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x0000C229 File Offset: 0x0000A429
		public byte Operation { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000C232 File Offset: 0x0000A432
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x0000C23A File Offset: 0x0000A43A
		public byte Client { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0000C243 File Offset: 0x0000A443
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x0000C24B File Offset: 0x0000A44B
		public int Database { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0000C254 File Offset: 0x0000A454
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x0000C25C File Offset: 0x0000A45C
		public int Mailbox { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0000C265 File Offset: 0x0000A465
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x0000C26D File Offset: 0x0000A46D
		public DateTime Time { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0000C276 File Offset: 0x0000A476
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x0000C27E File Offset: 0x0000A47E
		public int DataValue { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000C287 File Offset: 0x0000A487
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x0000C28F File Offset: 0x0000A48F
		public object DataObject { get; set; }
	}
}
