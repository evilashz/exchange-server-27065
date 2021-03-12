using System;
using System.Xml.Linq;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x0200003B RID: 59
	internal class LocalThrottlingResult
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00007260 File Offset: 0x00005460
		// (set) Token: 0x0600020C RID: 524 RVA: 0x00007268 File Offset: 0x00005468
		internal bool IsPassed { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00007271 File Offset: 0x00005471
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00007279 File Offset: 0x00005479
		internal RecoveryActionHelper.RecoveryActionEntrySerializable MostRecentEntry { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00007282 File Offset: 0x00005482
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000728A File Offset: 0x0000548A
		internal int MinimumMinutes { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00007293 File Offset: 0x00005493
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0000729B File Offset: 0x0000549B
		internal int TotalInOneHour { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000213 RID: 531 RVA: 0x000072A4 File Offset: 0x000054A4
		// (set) Token: 0x06000214 RID: 532 RVA: 0x000072AC File Offset: 0x000054AC
		internal int MaxAllowedInOneHour { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000215 RID: 533 RVA: 0x000072B5 File Offset: 0x000054B5
		// (set) Token: 0x06000216 RID: 534 RVA: 0x000072BD File Offset: 0x000054BD
		internal int TotalInOneDay { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000217 RID: 535 RVA: 0x000072C6 File Offset: 0x000054C6
		// (set) Token: 0x06000218 RID: 536 RVA: 0x000072CE File Offset: 0x000054CE
		internal int MaxAllowedInOneDay { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000219 RID: 537 RVA: 0x000072D7 File Offset: 0x000054D7
		// (set) Token: 0x0600021A RID: 538 RVA: 0x000072DF File Offset: 0x000054DF
		internal bool IsThrottlingInProgress { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600021B RID: 539 RVA: 0x000072E8 File Offset: 0x000054E8
		// (set) Token: 0x0600021C RID: 540 RVA: 0x000072F0 File Offset: 0x000054F0
		internal bool IsRecoveryInProgress { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600021D RID: 541 RVA: 0x000072F9 File Offset: 0x000054F9
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00007301 File Offset: 0x00005501
		internal string ChecksFailed { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000730A File Offset: 0x0000550A
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00007312 File Offset: 0x00005512
		internal DateTime TimeToRetryAfter { get; set; }

		// Token: 0x06000221 RID: 545 RVA: 0x0000731C File Offset: 0x0000551C
		internal string ToXml(bool isForce = false)
		{
			if (!isForce && this.xml != null)
			{
				return this.xml;
			}
			XElement xelement = new XElement("LocalThrottlingResult", new object[]
			{
				new XAttribute("IsPassed", this.IsPassed),
				new XAttribute("MinimumMinutes", this.MinimumMinutes),
				new XAttribute("TotalInOneHour", this.TotalInOneHour),
				new XAttribute("MaxAllowedInOneHour", this.MaxAllowedInOneHour),
				new XAttribute("TotalInOneDay", this.TotalInOneDay),
				new XAttribute("MaxAllowedInOneDay", this.MaxAllowedInOneDay),
				new XAttribute("IsThrottlingInProgress", this.IsThrottlingInProgress),
				new XAttribute("IsRecoveryInProgress", this.IsRecoveryInProgress),
				new XAttribute("ChecksFailed", this.ChecksFailed),
				new XAttribute("TimeToRetryAfter", this.TimeToRetryAfter.ToString("o"))
			});
			if (this.MostRecentEntry != null)
			{
				xelement.Add(new XElement("MostRecentEntry", new object[]
				{
					new XAttribute("Requester", this.MostRecentEntry.RequestorName),
					new XAttribute("StartTime", this.MostRecentEntry.StartTime),
					new XAttribute("EndTime", this.MostRecentEntry.EndTime),
					new XAttribute("State", this.MostRecentEntry.State),
					new XAttribute("Result", this.MostRecentEntry.Result)
				}));
			}
			this.xml = xelement.ToString();
			return this.xml;
		}

		// Token: 0x04000129 RID: 297
		private string xml;
	}
}
