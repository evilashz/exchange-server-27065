using System;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000089 RID: 137
	internal class HygieneIPRange
	{
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00010C34 File Offset: 0x0000EE34
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x00010C3C File Offset: 0x0000EE3C
		public byte IPA { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00010C45 File Offset: 0x0000EE45
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x00010C4D File Offset: 0x0000EE4D
		public byte IPB { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x00010C56 File Offset: 0x0000EE56
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x00010C5E File Offset: 0x0000EE5E
		public byte IPC { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x00010C67 File Offset: 0x0000EE67
		// (set) Token: 0x060004FD RID: 1277 RVA: 0x00010C6F File Offset: 0x0000EE6F
		public byte IPD { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00010C78 File Offset: 0x0000EE78
		// (set) Token: 0x060004FF RID: 1279 RVA: 0x00010C80 File Offset: 0x0000EE80
		public byte? CIDR { get; set; }
	}
}
