using System;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200003B RID: 59
	internal class CorrelatedMonitorInfo
	{
		// Token: 0x060004AD RID: 1197 RVA: 0x00011CED File Offset: 0x0000FEED
		internal CorrelatedMonitorInfo(string identity, string matchString = null, CorrelatedMonitorInfo.MatchMode matchMode = CorrelatedMonitorInfo.MatchMode.Wildcard)
		{
			this.ParseIdentityAndAssignProperties(identity);
			this.identityStr = this.GetIdentityAsString();
			this.MatchString = (matchString ?? string.Empty);
			this.ModeOfMatch = matchMode;
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00011D1F File Offset: 0x0000FF1F
		internal string Identity
		{
			get
			{
				return this.identityStr;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x00011D27 File Offset: 0x0000FF27
		// (set) Token: 0x060004B0 RID: 1200 RVA: 0x00011D2F File Offset: 0x0000FF2F
		internal Component Component { get; private set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00011D38 File Offset: 0x0000FF38
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x00011D40 File Offset: 0x0000FF40
		internal string MonitorName { get; private set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00011D49 File Offset: 0x0000FF49
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x00011D51 File Offset: 0x0000FF51
		internal string TargetResource { get; private set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00011D5A File Offset: 0x0000FF5A
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x00011D62 File Offset: 0x0000FF62
		internal string MatchString { get; private set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00011D6B File Offset: 0x0000FF6B
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x00011D73 File Offset: 0x0000FF73
		internal CorrelatedMonitorInfo.MatchMode ModeOfMatch { get; private set; }

		// Token: 0x060004B9 RID: 1209 RVA: 0x00011D7C File Offset: 0x0000FF7C
		internal void ParseIdentityAndAssignProperties(string identityStr)
		{
			string[] array = identityStr.Split(new char[]
			{
				'\\'
			});
			int num = array.Length;
			if (num < 2)
			{
				throw new InvalidOperationException(string.Format("Invalid monitor identity string {0}", identityStr));
			}
			string key = array[0];
			Component component = null;
			if (!ExchangeComponent.WellKnownComponents.TryGetValue(key, out component))
			{
				throw new InvalidOperationException(string.Format("Invalid health set name specified in the monitor identity {0}", identityStr));
			}
			this.Component = component;
			this.MonitorName = array[1];
			this.TargetResource = ((array.Length > 2) ? array[2] : string.Empty);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00011E08 File Offset: 0x00010008
		private string GetIdentityAsString()
		{
			if (string.IsNullOrEmpty(this.TargetResource))
			{
				return string.Format("{0}\\{1}", this.Component.Name, this.MonitorName);
			}
			return string.Format("{0}\\{1}\\{2}", this.Component.Name, this.MonitorName, this.TargetResource);
		}

		// Token: 0x04000345 RID: 837
		private readonly string identityStr;

		// Token: 0x0200003C RID: 60
		internal enum MatchMode
		{
			// Token: 0x0400034C RID: 844
			Wildcard,
			// Token: 0x0400034D RID: 845
			RegEx
		}
	}
}
