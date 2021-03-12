using System;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UserThrottling
{
	// Token: 0x020004D2 RID: 1234
	public sealed class UserThrottlingProtocolConfiguration
	{
		// Token: 0x06001EA8 RID: 7848 RVA: 0x000B8815 File Offset: 0x000B6A15
		public UserThrottlingProtocolConfiguration(string protocol, string process) : this(protocol, process, ExchangeComponent.UserThrottling.EscalationTeam, UserThrottlingProtocolConfiguration.DefaultLockedOutUsersAlertingThreshold, UserThrottlingProtocolConfiguration.DefaultLockedOutUsersAlertingDuration)
		{
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x000B8833 File Offset: 0x000B6A33
		public UserThrottlingProtocolConfiguration(string protocol, string process, string escalationTeam) : this(protocol, process, escalationTeam, UserThrottlingProtocolConfiguration.DefaultLockedOutUsersAlertingThreshold, UserThrottlingProtocolConfiguration.DefaultLockedOutUsersAlertingDuration)
		{
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x000B8848 File Offset: 0x000B6A48
		public UserThrottlingProtocolConfiguration(string protocol, string process, string escalationTeam, int lockedOutUsersAlertingThreshold, int lockedOutUsersAlertingSampleCount)
		{
			this.Protocol = protocol;
			this.Process = process;
			this.EscalationTeam = escalationTeam;
			this.LockedOutUsersAlertingThreshold = lockedOutUsersAlertingThreshold;
			this.LockedOutUsersAlertingSampleCount = lockedOutUsersAlertingSampleCount;
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001EAB RID: 7851 RVA: 0x000B8875 File Offset: 0x000B6A75
		// (set) Token: 0x06001EAC RID: 7852 RVA: 0x000B887D File Offset: 0x000B6A7D
		public string Protocol { get; private set; }

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001EAD RID: 7853 RVA: 0x000B8886 File Offset: 0x000B6A86
		// (set) Token: 0x06001EAE RID: 7854 RVA: 0x000B888E File Offset: 0x000B6A8E
		public string Process { get; private set; }

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001EAF RID: 7855 RVA: 0x000B8897 File Offset: 0x000B6A97
		// (set) Token: 0x06001EB0 RID: 7856 RVA: 0x000B889F File Offset: 0x000B6A9F
		public string EscalationTeam { get; private set; }

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001EB1 RID: 7857 RVA: 0x000B88A8 File Offset: 0x000B6AA8
		// (set) Token: 0x06001EB2 RID: 7858 RVA: 0x000B88B0 File Offset: 0x000B6AB0
		public int LockedOutUsersAlertingThreshold { get; private set; }

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001EB3 RID: 7859 RVA: 0x000B88B9 File Offset: 0x000B6AB9
		// (set) Token: 0x06001EB4 RID: 7860 RVA: 0x000B88C1 File Offset: 0x000B6AC1
		public int LockedOutUsersAlertingSampleCount { get; private set; }

		// Token: 0x040015D5 RID: 5589
		private static readonly int DefaultLockedOutUsersAlertingThreshold = 10;

		// Token: 0x040015D6 RID: 5590
		private static readonly int DefaultLockedOutUsersAlertingDuration = 12;
	}
}
