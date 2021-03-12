using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x0200003A RID: 58
	internal class GroupThrottlingResult
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00006C56 File Offset: 0x00004E56
		// (set) Token: 0x060001EE RID: 494 RVA: 0x00006C5E File Offset: 0x00004E5E
		internal bool IsPassed { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00006C67 File Offset: 0x00004E67
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00006C6F File Offset: 0x00004E6F
		internal int TotalRequestsSent { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00006C78 File Offset: 0x00004E78
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x00006C80 File Offset: 0x00004E80
		internal int TotalRequestsSucceeded { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00006C89 File Offset: 0x00004E89
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x00006C91 File Offset: 0x00004E91
		internal RecoveryActionHelper.RecoveryActionEntrySerializable MostRecentEntry { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00006C9A File Offset: 0x00004E9A
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x00006CA2 File Offset: 0x00004EA2
		internal int MinimumMinutes { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00006CAB File Offset: 0x00004EAB
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x00006CB3 File Offset: 0x00004EB3
		internal int TotalInOneDay { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00006CBC File Offset: 0x00004EBC
		// (set) Token: 0x060001FA RID: 506 RVA: 0x00006CC4 File Offset: 0x00004EC4
		internal int MaxAllowedInOneDay { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00006CCD File Offset: 0x00004ECD
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00006CD5 File Offset: 0x00004ED5
		internal string[] ThrottlingInProgressServers { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00006CDE File Offset: 0x00004EDE
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00006CE6 File Offset: 0x00004EE6
		internal string[] RecoveryInProgressServers { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00006CEF File Offset: 0x00004EEF
		// (set) Token: 0x06000200 RID: 512 RVA: 0x00006CF7 File Offset: 0x00004EF7
		internal string ChecksFailed { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00006D00 File Offset: 0x00004F00
		// (set) Token: 0x06000202 RID: 514 RVA: 0x00006D08 File Offset: 0x00004F08
		internal DateTime TimeToRetryAfter { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00006D11 File Offset: 0x00004F11
		// (set) Token: 0x06000204 RID: 516 RVA: 0x00006D19 File Offset: 0x00004F19
		internal string Comment { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00006D22 File Offset: 0x00004F22
		// (set) Token: 0x06000206 RID: 518 RVA: 0x00006D2A File Offset: 0x00004F2A
		internal Dictionary<string, RpcGetThrottlingStatisticsImpl.ThrottlingStatistics> GroupStats { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00006D33 File Offset: 0x00004F33
		// (set) Token: 0x06000208 RID: 520 RVA: 0x00006D3B File Offset: 0x00004F3B
		internal ConcurrentDictionary<string, Exception> ExceptionsByServer { get; set; }

		// Token: 0x06000209 RID: 521 RVA: 0x00006D44 File Offset: 0x00004F44
		internal string ToXml(bool isForce = false)
		{
			if (!isForce && this.xml != null)
			{
				return this.xml;
			}
			string value = string.Empty;
			if (this.ThrottlingInProgressServers != null)
			{
				value = string.Join(",", this.ThrottlingInProgressServers);
			}
			string value2 = string.Empty;
			if (this.RecoveryInProgressServers != null)
			{
				value2 = string.Join(",", this.RecoveryInProgressServers);
			}
			XElement xelement = new XElement("GroupThrottlingResult", new object[]
			{
				new XAttribute("IsPassed", this.IsPassed),
				new XAttribute("TotalRequestsSent", this.TotalRequestsSent),
				new XAttribute("TotalRequestsSucceeded", this.TotalRequestsSucceeded),
				new XAttribute("MinimumMinutes", this.MinimumMinutes),
				new XAttribute("TotalInOneDay", this.TotalInOneDay),
				new XAttribute("MaxAllowedInOneDay", this.MaxAllowedInOneDay),
				new XAttribute("ThrottlingInProgressServers", value),
				new XAttribute("RecoveryInProgressServers", value2),
				new XAttribute("ChecksFailed", (this.ChecksFailed != null) ? this.ChecksFailed : string.Empty),
				new XAttribute("TimeToRetryAfter", this.TimeToRetryAfter.ToString("o")),
				new XAttribute("Comment", (this.Comment != null) ? this.Comment : string.Empty)
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
			XElement xelement2 = new XElement("ServerStats");
			if (this.GroupStats != null)
			{
				foreach (KeyValuePair<string, RpcGetThrottlingStatisticsImpl.ThrottlingStatistics> keyValuePair in this.GroupStats)
				{
					string key = keyValuePair.Key;
					RpcGetThrottlingStatisticsImpl.ThrottlingStatistics value3 = keyValuePair.Value;
					string text = null;
					Exception ex = null;
					if (this.ExceptionsByServer.TryGetValue(key, out ex) && ex != null)
					{
						text = ex.ToString().Replace("Microsoft.Exchange.Monitoring.ActiveMonitoring", "M.E.M.A");
						text = text.Substring(0, Math.Min(text.Length, 500));
					}
					XElement xelement3 = new XElement(key);
					if (!string.IsNullOrEmpty(text))
					{
						xelement3.Add(new XAttribute("Error", text));
					}
					RpcGetThrottlingStatisticsImpl.ThrottlingStatistics throttlingStatistics;
					if (this.GroupStats.TryGetValue(key, out throttlingStatistics) && throttlingStatistics != null)
					{
						xelement3.Add(new XAttribute("TotalSearched", throttlingStatistics.TotalEntriesSearched));
						xelement3.Add(new XAttribute("MostRecentEntryStartTimeUtc", (throttlingStatistics.MostRecentEntry != null) ? throttlingStatistics.MostRecentEntry.StartTimeUtc : DateTime.MinValue));
						xelement3.Add(new XAttribute("MostRecentEntryEndTimeUtc", (throttlingStatistics.MostRecentEntry != null) ? throttlingStatistics.MostRecentEntry.EndTimeUtc : DateTime.MinValue));
						xelement3.Add(new XAttribute("TotalActionsInADay", throttlingStatistics.NumberOfActionsInOneDay));
						xelement3.Add(new XAttribute("IsThrottlingInProgress", throttlingStatistics.IsThrottlingInProgress));
						xelement3.Add(new XAttribute("IsRecoveryInProgress", throttlingStatistics.IsRecoveryInProgress));
						xelement3.Add(new XAttribute("HostProcessStartTimeUtc", throttlingStatistics.HostProcessStartTimeUtc));
						xelement3.Add(new XAttribute("SystemBootTimeUtc", throttlingStatistics.SystemBootTimeUtc));
					}
					xelement2.Add(xelement3);
				}
			}
			xelement.Add(xelement2);
			this.xml = xelement.ToString();
			return this.xml;
		}

		// Token: 0x0400011A RID: 282
		private string xml;
	}
}
