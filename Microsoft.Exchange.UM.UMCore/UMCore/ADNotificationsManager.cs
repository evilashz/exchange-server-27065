using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000016 RID: 22
	internal sealed class ADNotificationsManager : DisposableBase
	{
		// Token: 0x0600012D RID: 301 RVA: 0x00005B41 File Offset: 0x00003D41
		private ADNotificationsManager()
		{
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00005B54 File Offset: 0x00003D54
		internal static ADNotificationsManager Instance
		{
			get
			{
				return ADNotificationsManager.instance;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00005B5C File Offset: 0x00003D5C
		internal ADNotificationHandler Server
		{
			get
			{
				lock (this.syncLock)
				{
					if (this.server == null)
					{
						this.server = new ServerNotificationHandler();
					}
				}
				return this.server;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00005BB0 File Offset: 0x00003DB0
		internal ADNotificationHandler CallRouterSettings
		{
			get
			{
				lock (this.syncLock)
				{
					if (this.callRouterSettings == null)
					{
						this.callRouterSettings = new CallRouterSettingsNotificationHandler();
					}
				}
				return this.callRouterSettings;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00005C04 File Offset: 0x00003E04
		internal ADNotificationHandler UMDialPlan
		{
			get
			{
				lock (this.syncLock)
				{
					if (this.dialPlan == null)
					{
						this.dialPlan = new UMDialPlanNotificationHandler();
					}
				}
				return this.dialPlan;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00005C58 File Offset: 0x00003E58
		internal ADNotificationHandler UMIPGateway
		{
			get
			{
				lock (this.syncLock)
				{
					if (this.gateway == null)
					{
						this.gateway = new UMIPGatewayNotificationHandler();
					}
				}
				return this.gateway;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00005CAC File Offset: 0x00003EAC
		internal ADNotificationHandler UMHuntGroup
		{
			get
			{
				lock (this.syncLock)
				{
					if (this.huntGroup == null)
					{
						this.huntGroup = new UMHuntGroupNotificationHandler();
					}
				}
				return this.huntGroup;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00005D00 File Offset: 0x00003F00
		internal ADNotificationHandler UMAutoAttendant
		{
			get
			{
				lock (this.syncLock)
				{
					if (this.autoAttendant == null)
					{
						this.autoAttendant = new UMAutoAttendantNotificationHandler();
					}
				}
				return this.autoAttendant;
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005D54 File Offset: 0x00003F54
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.server != null)
				{
					this.server.Dispose();
				}
				if (this.callRouterSettings != null)
				{
					this.callRouterSettings.Dispose();
				}
				if (this.dialPlan != null)
				{
					this.dialPlan.Dispose();
				}
				if (this.gateway != null)
				{
					this.gateway.Dispose();
				}
				if (this.huntGroup != null)
				{
					this.huntGroup.Dispose();
				}
				if (this.autoAttendant != null)
				{
					this.autoAttendant.Dispose();
				}
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005DD6 File Offset: 0x00003FD6
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ADNotificationsManager>(this);
		}

		// Token: 0x0400005A RID: 90
		private static ADNotificationsManager instance = new ADNotificationsManager();

		// Token: 0x0400005B RID: 91
		private object syncLock = new object();

		// Token: 0x0400005C RID: 92
		private ADNotificationHandler callRouterSettings;

		// Token: 0x0400005D RID: 93
		private ADNotificationHandler server;

		// Token: 0x0400005E RID: 94
		private ADNotificationHandler dialPlan;

		// Token: 0x0400005F RID: 95
		private ADNotificationHandler gateway;

		// Token: 0x04000060 RID: 96
		private ADNotificationHandler autoAttendant;

		// Token: 0x04000061 RID: 97
		private ADNotificationHandler huntGroup;
	}
}
