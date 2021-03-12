using System;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000234 RID: 564
	public class Notification
	{
		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x00036F8E File Offset: 0x0003518E
		// (set) Token: 0x060012AC RID: 4780 RVA: 0x00036F96 File Offset: 0x00035196
		internal string Category { get; set; }

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x00036F9F File Offset: 0x0003519F
		// (set) Token: 0x060012AE RID: 4782 RVA: 0x00036FA7 File Offset: 0x000351A7
		internal string Type { get; set; }

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x00036FB0 File Offset: 0x000351B0
		// (set) Token: 0x060012B0 RID: 4784 RVA: 0x00036FB8 File Offset: 0x000351B8
		internal string Value { get; set; }

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x00036FC1 File Offset: 0x000351C1
		// (set) Token: 0x060012B2 RID: 4786 RVA: 0x00036FC9 File Offset: 0x000351C9
		internal bool Mandatory { get; set; }

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x00036FD2 File Offset: 0x000351D2
		// (set) Token: 0x060012B4 RID: 4788 RVA: 0x00036FDA File Offset: 0x000351DA
		internal bool MatchExpected { get; set; }

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x00036FE3 File Offset: 0x000351E3
		// (set) Token: 0x060012B6 RID: 4790 RVA: 0x00036FEB File Offset: 0x000351EB
		internal MatchType Method { get; set; }

		// Token: 0x060012B7 RID: 4791 RVA: 0x00036FF4 File Offset: 0x000351F4
		public override string ToString()
		{
			return string.Format("Type={0}, Value={1}, Method={2}, Category={3}, Mandatory={4}, MatchExpected={5}", new object[]
			{
				this.Type,
				this.Value,
				this.Method,
				this.Category,
				this.Mandatory,
				this.MatchExpected
			});
		}
	}
}
