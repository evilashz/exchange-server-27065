using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009A6 RID: 2470
	public class BudgetHandlerMetadata
	{
		// Token: 0x17002843 RID: 10307
		// (get) Token: 0x06007208 RID: 29192 RVA: 0x00179EC0 File Offset: 0x001780C0
		// (set) Token: 0x06007209 RID: 29193 RVA: 0x00179EC8 File Offset: 0x001780C8
		public int OutstandingActions { get; set; }

		// Token: 0x17002844 RID: 10308
		// (get) Token: 0x0600720A RID: 29194 RVA: 0x00179ED1 File Offset: 0x001780D1
		// (set) Token: 0x0600720B RID: 29195 RVA: 0x00179ED9 File Offset: 0x001780D9
		public string Key { get; set; }

		// Token: 0x17002845 RID: 10309
		// (get) Token: 0x0600720C RID: 29196 RVA: 0x00179EE2 File Offset: 0x001780E2
		// (set) Token: 0x0600720D RID: 29197 RVA: 0x00179EEA File Offset: 0x001780EA
		public string Snapshot { get; set; }

		// Token: 0x17002846 RID: 10310
		// (get) Token: 0x0600720E RID: 29198 RVA: 0x00179EF3 File Offset: 0x001780F3
		// (set) Token: 0x0600720F RID: 29199 RVA: 0x00179EFB File Offset: 0x001780FB
		[XmlAttribute]
		public bool Locked { get; set; }

		// Token: 0x17002847 RID: 10311
		// (get) Token: 0x06007210 RID: 29200 RVA: 0x00179F04 File Offset: 0x00178104
		// (set) Token: 0x06007211 RID: 29201 RVA: 0x00179F0C File Offset: 0x0017810C
		[XmlAttribute]
		public string LockedAt { get; set; }

		// Token: 0x17002848 RID: 10312
		// (get) Token: 0x06007212 RID: 29202 RVA: 0x00179F15 File Offset: 0x00178115
		// (set) Token: 0x06007213 RID: 29203 RVA: 0x00179F1D File Offset: 0x0017811D
		[XmlAttribute]
		public string LockedUntil { get; set; }
	}
}
