using System;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000016 RID: 22
	public interface IThrottleHelper
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600008B RID: 139
		ThrottleSettingsBase Settings { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008C RID: 140
		GlobalTunables Tunables { get; }

		// Token: 0x0600008D RID: 141
		string[] GetServersInGroup(string groupName);

		// Token: 0x0600008E RID: 142
		int GetServerVersion(string serverName);
	}
}
