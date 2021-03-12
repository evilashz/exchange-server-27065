using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync
{
	// Token: 0x02000076 RID: 118
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class DeltaSyncSettings
	{
		// Token: 0x06000553 RID: 1363 RVA: 0x00018FD1 File Offset: 0x000171D1
		internal DeltaSyncSettings(ServiceSettingsPropertiesType serviceProperties, PropertiesType accountProperties)
		{
			SyncUtilities.ThrowIfArgumentNull("serviceProperties", serviceProperties);
			SyncUtilities.ThrowIfArgumentNull("accountProperties", accountProperties);
			this.serviceProperties = serviceProperties;
			this.accountProperties = accountProperties;
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x00018FFD File Offset: 0x000171FD
		internal int MinSyncPollInterval
		{
			get
			{
				return this.serviceProperties.MinSyncPollInterval;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0001900A File Offset: 0x0001720A
		internal int MinSettingsPollInterval
		{
			get
			{
				return this.serviceProperties.MinSettingsPollInterval;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00019017 File Offset: 0x00017217
		internal double SyncMultiplier
		{
			get
			{
				return this.serviceProperties.SyncMultiplier;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00019024 File Offset: 0x00017224
		internal int MaxObjectsInSync
		{
			get
			{
				return this.serviceProperties.MaxObjectsInSync;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x00019031 File Offset: 0x00017231
		internal int MaxNumberOfEmailAdds
		{
			get
			{
				return this.serviceProperties.MaxNumberOfEmailAdds;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0001903E File Offset: 0x0001723E
		internal int MaxNumberOfFolderAdds
		{
			get
			{
				return this.serviceProperties.MaxNumberOfFolderAdds;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0001904B File Offset: 0x0001724B
		internal int MaxAttachments
		{
			get
			{
				return this.accountProperties.MaxAttachments;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00019058 File Offset: 0x00017258
		internal long MaxMessageSize
		{
			get
			{
				return this.accountProperties.MaxMessageSize;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x00019065 File Offset: 0x00017265
		internal int MaxRecipients
		{
			get
			{
				return this.accountProperties.MaxRecipients;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00019072 File Offset: 0x00017272
		internal AccountStatusType AccountStatus
		{
			get
			{
				return this.accountProperties.AccountStatus;
			}
		}

		// Token: 0x040002E5 RID: 741
		private ServiceSettingsPropertiesType serviceProperties;

		// Token: 0x040002E6 RID: 742
		private PropertiesType accountProperties;
	}
}
