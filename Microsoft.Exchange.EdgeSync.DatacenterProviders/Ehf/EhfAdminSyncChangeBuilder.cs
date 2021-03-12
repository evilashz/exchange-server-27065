using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x02000010 RID: 16
	internal class EhfAdminSyncChangeBuilder
	{
		// Token: 0x06000085 RID: 133 RVA: 0x000051A0 File Offset: 0x000033A0
		public EhfAdminSyncChangeBuilder(string tenantOU, string tenantConfigUnitDN, EhfTargetConnection targetConnection)
		{
			if (string.IsNullOrEmpty(tenantOU))
			{
				throw new ArgumentNullException("tenantOU");
			}
			if (string.IsNullOrEmpty(tenantConfigUnitDN))
			{
				throw new ArgumentNullException("tenantConfigUnitDN");
			}
			this.tenantOU = tenantOU;
			this.tenantConfigUnitDN = tenantConfigUnitDN;
			this.ehfTargetConnection = targetConnection;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000520F File Offset: 0x0000340F
		public string TenantOU
		{
			get
			{
				return this.tenantOU;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00005217 File Offset: 0x00003417
		public string ConfigUnitDN
		{
			get
			{
				return this.tenantConfigUnitDN;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000088 RID: 136 RVA: 0x0000521F File Offset: 0x0000341F
		public bool UpdateOrgManagementGroup
		{
			get
			{
				return this.updateOrgManagementGroup;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00005227 File Offset: 0x00003427
		public bool UpdateViewOnlyOrgManagementGroup
		{
			get
			{
				return this.updateViewOnlyOrgManagementGroup;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000522F File Offset: 0x0000342F
		public bool UpdateAdminAgentGroup
		{
			get
			{
				return this.updateAdminAgentGroup;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00005237 File Offset: 0x00003437
		public bool UpdateHelpdeskAgentGroup
		{
			get
			{
				return this.updateHelpdeskAgentGroup;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000523F File Offset: 0x0000343F
		public List<Guid> DeletedObjects
		{
			get
			{
				return this.deletedObjects;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00005247 File Offset: 0x00003447
		public List<ExSearchResultEntry> GroupChanges
		{
			get
			{
				return this.groupChanges;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600008E RID: 142 RVA: 0x0000524F File Offset: 0x0000344F
		public List<ExSearchResultEntry> LiveIdChanges
		{
			get
			{
				return this.liveIdChanges;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00005258 File Offset: 0x00003458
		public bool ChangeExists
		{
			get
			{
				return this.updateOrgManagementGroup || this.updateViewOnlyOrgManagementGroup || this.updateHelpdeskAgentGroup || this.updateAdminAgentGroup || (this.groupChanges.Count != 0 || this.liveIdChanges.Count != 0 || this.deletedObjects.Count != 0);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000052B1 File Offset: 0x000034B1
		public EhfTargetConnection EhfTargetConnection
		{
			get
			{
				return this.ehfTargetConnection;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000052BC File Offset: 0x000034BC
		public void AddGroupAdditionChange(ExSearchResultEntry change)
		{
			if (EhfWellKnownGroup.IsOrganizationManagementGroup(change) || EhfWellKnownGroup.IsViewOnlyOrganizationManagementGroup(change))
			{
				this.updateOrgManagementGroup = true;
				this.updateViewOnlyOrgManagementGroup = true;
			}
			else if (EhfWellKnownGroup.IsAdminAgentGroup(change.DistinguishedName))
			{
				this.updateAdminAgentGroup = true;
			}
			else if (EhfWellKnownGroup.IsHelpdeskAgentGroup(change.DistinguishedName))
			{
				this.updateHelpdeskAgentGroup = true;
			}
			if (this.IsFullTenantAdminSyncRequired())
			{
				this.ClearCachedChanges();
			}
			this.SetFullTenantAdminSyncIfTooManyCachedChanges();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005328 File Offset: 0x00003528
		public void AddGroupMembershipChange(ExSearchResultEntry change)
		{
			if (this.IsFullTenantAdminSyncRequired())
			{
				return;
			}
			if (EhfWellKnownGroup.IsOrganizationManagementGroup(change))
			{
				this.updateOrgManagementGroup = true;
			}
			else if (EhfWellKnownGroup.IsViewOnlyOrganizationManagementGroup(change))
			{
				this.updateViewOnlyOrgManagementGroup = true;
			}
			else if (EhfWellKnownGroup.IsAdminAgentGroup(change.DistinguishedName))
			{
				this.updateAdminAgentGroup = true;
			}
			else if (EhfWellKnownGroup.IsHelpdeskAgentGroup(change.DistinguishedName))
			{
				this.updateHelpdeskAgentGroup = true;
			}
			if (this.IsFullTenantAdminSyncRequired())
			{
				this.ClearCachedChanges();
			}
			else
			{
				this.groupChanges.Add(change);
			}
			this.SetFullTenantAdminSyncIfTooManyCachedChanges();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000053AC File Offset: 0x000035AC
		public void AddWlidChanges(ExSearchResultEntry change)
		{
			if (this.IsFullTenantAdminSyncRequired())
			{
				return;
			}
			this.liveIdChanges.Add(change);
			this.SetFullTenantAdminSyncIfTooManyCachedChanges();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000053C9 File Offset: 0x000035C9
		public void HandleGroupDeletedEvent(ExSearchResultEntry entry)
		{
			if (this.IsFullTenantAdminSyncRequired())
			{
				return;
			}
			this.deletedObjects.Add(entry.GetObjectGuid());
			this.SetFullTenantAdminSyncIfTooManyCachedChanges();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000053EB File Offset: 0x000035EB
		public void HandleWlidDeletedEvent(ExSearchResultEntry entry)
		{
			if (this.IsFullTenantAdminSyncRequired())
			{
				return;
			}
			this.deletedObjects.Add(entry.GetObjectGuid());
			this.SetFullTenantAdminSyncIfTooManyCachedChanges();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005410 File Offset: 0x00003610
		private void SetFullTenantAdminSyncIfTooManyCachedChanges()
		{
			EhfSyncAppConfig ehfSyncAppConfig = this.ehfTargetConnection.Config.EhfSyncAppConfig;
			if (this.groupChanges.Count + this.liveIdChanges.Count > ehfSyncAppConfig.EhfAdminSyncMaxAccumulatedChangeSize || this.deletedObjects.Count > ehfSyncAppConfig.EhfAdminSyncMaxAccumulatedDeleteChangeSize)
			{
				this.ehfTargetConnection.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Setting the tenant <{0}> for FullAdminSync since there are too many cached changes. GroupChanges:<{1}>; LiveidChanges:<{2}>; DeletedObject:<{3}>", new object[]
				{
					this.tenantOU,
					this.groupChanges.Count,
					this.liveIdChanges.Count,
					this.deletedObjects.Count
				});
				this.SetFullTenantAdminSyncRequired();
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000054C8 File Offset: 0x000036C8
		public void HandleOrganizationChangedEvent(ExSearchResultEntry entry)
		{
			this.ehfTargetConnection.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.High, "Encountered a MODIFY organization event. The org '{0}' is set to do a complete adminsync", new object[]
			{
				this.tenantOU
			});
			this.SetFullTenantAdminSyncRequired();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005504 File Offset: 0x00003704
		public void HandleOrganizationAddedEvent(ExSearchResultEntry entry)
		{
			this.ehfTargetConnection.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.High, "Encountered a ADD organization event. The org '{0}' is set to do a complete adminsync", new object[]
			{
				this.tenantOU
			});
			this.SetFullTenantAdminSyncRequired();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000553E File Offset: 0x0000373E
		public void ClearCachedChanges()
		{
			this.groupChanges.Clear();
			this.liveIdChanges.Clear();
			this.deletedObjects.Clear();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00005561 File Offset: 0x00003761
		public EhfCompanyAdmins Flush(EhfADAdapter configADAdapter)
		{
			if (this.flushed)
			{
				throw new InvalidOperationException("Flush() should be called only once");
			}
			this.flushed = true;
			return EhfCompanyAdmins.CreateEhfCompanyAdmins(this, this.ehfTargetConnection, configADAdapter);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000558A File Offset: 0x0000378A
		public bool IsFullTenantAdminSyncRequired()
		{
			return this.updateOrgManagementGroup && this.updateViewOnlyOrgManagementGroup && this.updateAdminAgentGroup && this.updateHelpdeskAgentGroup;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000055AC File Offset: 0x000037AC
		public bool HasDirectChangeForGroup(string distinguishedName)
		{
			foreach (ExSearchResultEntry exSearchResultEntry in this.groupChanges)
			{
				if (exSearchResultEntry.DistinguishedName.Equals(distinguishedName, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005610 File Offset: 0x00003810
		private void SetFullTenantAdminSyncRequired()
		{
			this.updateOrgManagementGroup = true;
			this.updateViewOnlyOrgManagementGroup = true;
			this.updateAdminAgentGroup = true;
			this.updateHelpdeskAgentGroup = true;
			this.ClearCachedChanges();
		}

		// Token: 0x04000029 RID: 41
		private bool updateOrgManagementGroup;

		// Token: 0x0400002A RID: 42
		private bool updateViewOnlyOrgManagementGroup;

		// Token: 0x0400002B RID: 43
		private bool updateAdminAgentGroup;

		// Token: 0x0400002C RID: 44
		private bool updateHelpdeskAgentGroup;

		// Token: 0x0400002D RID: 45
		private bool flushed;

		// Token: 0x0400002E RID: 46
		private string tenantOU;

		// Token: 0x0400002F RID: 47
		private string tenantConfigUnitDN;

		// Token: 0x04000030 RID: 48
		private EhfTargetConnection ehfTargetConnection;

		// Token: 0x04000031 RID: 49
		private List<ExSearchResultEntry> groupChanges = new List<ExSearchResultEntry>();

		// Token: 0x04000032 RID: 50
		private List<ExSearchResultEntry> liveIdChanges = new List<ExSearchResultEntry>();

		// Token: 0x04000033 RID: 51
		private List<Guid> deletedObjects = new List<Guid>();
	}
}
