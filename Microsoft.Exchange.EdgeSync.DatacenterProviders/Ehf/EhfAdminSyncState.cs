using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x02000011 RID: 17
	internal sealed class EhfAdminSyncState
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00005634 File Offset: 0x00003834
		private EhfAdminSyncState(EhfCompanyIdentity companyIdentity, EhfTargetConnection targetConnection)
		{
			this.ehfTargetConnection = targetConnection;
			this.ehfCompanyIdentity = companyIdentity;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000564C File Offset: 0x0000384C
		public static EhfAdminSyncState Create(EhfCompanyIdentity companyIdentity, ExSearchResultEntry entry, EhfTargetConnection targetConnection)
		{
			return new EhfAdminSyncState(companyIdentity, targetConnection)
			{
				orgAdminMembers = EhfAdminSyncState.GetAdminStateFromAttribute(entry, "msExchTargetServerAdmins"),
				viewOnlyOrgAdminMembers = EhfAdminSyncState.GetAdminStateFromAttribute(entry, "msExchTargetServerViewOnlyAdmins"),
				adminAgentMembers = EhfAdminSyncState.GetAdminStateFromAttribute(entry, "msExchTargetServerPartnerAdmins"),
				helpDeskAgentMembers = EhfAdminSyncState.GetAdminStateFromAttribute(entry, "msExchTargetServerPartnerViewOnlyAdmins")
			};
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000056A8 File Offset: 0x000038A8
		public static EhfAdminSyncState Create(EhfCompanyAdmins admins, bool addOrgAdminState, bool addViewOnlyOrgAdminState, bool addAdminAgentState, bool addHelpDeskAgentState, EhfTargetConnection targetConnection)
		{
			EhfAdminSyncState ehfAdminSyncState = admins.EhfAdminSyncState;
			EhfAdminSyncState ehfAdminSyncState2 = new EhfAdminSyncState(admins.EhfCompanyIdentity, targetConnection);
			ehfAdminSyncState2.orgAdminMembers = ehfAdminSyncState2.GetNewState(admins.OrganizationMangement, ehfAdminSyncState.orgAdminMembers, addOrgAdminState);
			ehfAdminSyncState2.viewOnlyOrgAdminMembers = ehfAdminSyncState2.GetNewState(admins.ViewonlyOrganizationManagement, ehfAdminSyncState.viewOnlyOrgAdminMembers, addViewOnlyOrgAdminState);
			ehfAdminSyncState2.adminAgentMembers = ehfAdminSyncState2.GetNewState(admins.AdminAgent, ehfAdminSyncState.adminAgentMembers, addAdminAgentState);
			ehfAdminSyncState2.helpDeskAgentMembers = ehfAdminSyncState2.GetNewState(admins.HelpdeskAgent, ehfAdminSyncState.helpDeskAgentMembers, addHelpDeskAgentState);
			return ehfAdminSyncState2;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00005730 File Offset: 0x00003930
		public bool IsEmpty
		{
			get
			{
				return this.orgAdminMembers == null && this.viewOnlyOrgAdminMembers == null && this.adminAgentMembers == null && this.helpDeskAgentMembers == null;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00005755 File Offset: 0x00003955
		public EhfCompanyIdentity EhfCompanyIdentity
		{
			get
			{
				return this.ehfCompanyIdentity;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000575D File Offset: 0x0000395D
		public HashSet<Guid> OrganizationManagmentMembers
		{
			get
			{
				return this.orgAdminMembers;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00005765 File Offset: 0x00003965
		public HashSet<Guid> ViewOnlyOrganizationManagmentMembers
		{
			get
			{
				return this.viewOnlyOrgAdminMembers;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000576D File Offset: 0x0000396D
		public HashSet<Guid> AdminAgentMembers
		{
			get
			{
				return this.adminAgentMembers;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00005775 File Offset: 0x00003975
		public HashSet<Guid> HelpdeskAgentMembers
		{
			get
			{
				return this.helpDeskAgentMembers;
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005938 File Offset: 0x00003B38
		public IEnumerable<KeyValuePair<string, List<byte[]>>> GetStatesToUpdate()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException("Should not update the state in AD if there is no change to update.");
			}
			if (this.orgAdminMembers != null)
			{
				yield return EhfAdminSyncState.GetStateToUpdateForAttribute(this.orgAdminMembers, "msExchTargetServerAdmins");
			}
			if (this.viewOnlyOrgAdminMembers != null)
			{
				yield return EhfAdminSyncState.GetStateToUpdateForAttribute(this.viewOnlyOrgAdminMembers, "msExchTargetServerViewOnlyAdmins");
			}
			if (this.adminAgentMembers != null)
			{
				yield return EhfAdminSyncState.GetStateToUpdateForAttribute(this.adminAgentMembers, "msExchTargetServerPartnerAdmins");
			}
			if (this.helpDeskAgentMembers != null)
			{
				yield return EhfAdminSyncState.GetStateToUpdateForAttribute(this.helpDeskAgentMembers, "msExchTargetServerPartnerViewOnlyAdmins");
			}
			yield break;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005958 File Offset: 0x00003B58
		private static KeyValuePair<string, List<byte[]>> GetStateToUpdateForAttribute(HashSet<Guid> hashSet, string attributeName)
		{
			List<byte[]> list = new List<byte[]>(hashSet.Count);
			foreach (Guid guid in hashSet)
			{
				list.Add(guid.ToByteArray());
			}
			return new KeyValuePair<string, List<byte[]>>(attributeName, list);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000059C0 File Offset: 0x00003BC0
		private static HashSet<Guid> GetAdminStateFromAttribute(ExSearchResultEntry perimeterConfigEntry, string attributeName)
		{
			DirectoryAttribute attribute = perimeterConfigEntry.GetAttribute(attributeName);
			if (attribute == null)
			{
				return null;
			}
			HashSet<Guid> hashSet = new HashSet<Guid>();
			foreach (object obj in attribute.GetValues(typeof(byte[])))
			{
				byte[] array = obj as byte[];
				if (array != null && array.Length == 16)
				{
					hashSet.Add(new Guid(array));
				}
			}
			return hashSet;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005A2C File Offset: 0x00003C2C
		private void AddToAdminSet<TAdminSyncUser>(Dictionary<Guid, TAdminSyncUser> members, HashSet<Guid> adminSet) where TAdminSyncUser : AdminSyncUser
		{
			if (members == null)
			{
				return;
			}
			foreach (Guid item in members.Keys)
			{
				if (adminSet.Count == this.ehfTargetConnection.Config.EhfSyncAppConfig.EhfAdminSyncMaxTargetAdminStateSize)
				{
					break;
				}
				adminSet.Add(item);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00005AA4 File Offset: 0x00003CA4
		private HashSet<Guid> GetNewState(EhfWellKnownGroup wellKnownGroup, HashSet<Guid> existingAdmins, bool updateState)
		{
			if (wellKnownGroup == null || !updateState)
			{
				return null;
			}
			HashSet<Guid> hashSet = new HashSet<Guid>();
			this.AddToAdminSet<MailboxAdminSyncUser>(wellKnownGroup.GroupMembers, hashSet);
			this.AddToAdminSet<PartnerGroupAdminSyncUser>(wellKnownGroup.LinkedRoleGroups, hashSet);
			this.AddToAdminSet<AdminSyncUser>(wellKnownGroup.SubGroups, hashSet);
			if (existingAdmins != null && existingAdmins.Count == hashSet.Count && existingAdmins.IsSubsetOf(hashSet))
			{
				this.ehfTargetConnection.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "The admin-state for group <{0}> is up to date in PerimeterConfig. No need to overwrite the state.", new object[]
				{
					wellKnownGroup.WellKnownGroupName
				});
				return null;
			}
			EhfSyncAppConfig ehfSyncAppConfig = this.ehfTargetConnection.Config.EhfSyncAppConfig;
			if (hashSet.Count >= ehfSyncAppConfig.EhfAdminSyncMaxTargetAdminStateSize)
			{
				this.ehfTargetConnection.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Number of members (<{0}>) of group <{1}> is greater than or equal to the maximum number <{2}> we store in AD. An Empty Guid will be stored in the state indicating that the max limit has reached.", new object[]
				{
					hashSet.Count,
					wellKnownGroup.WellKnownGroupName,
					ehfSyncAppConfig.EhfAdminSyncMaxTargetAdminStateSize
				});
				hashSet.Clear();
				hashSet.Add(EhfCompanyAdmins.SyncStateFullGuid);
			}
			return hashSet;
		}

		// Token: 0x04000034 RID: 52
		private EhfCompanyIdentity ehfCompanyIdentity;

		// Token: 0x04000035 RID: 53
		private EhfTargetConnection ehfTargetConnection;

		// Token: 0x04000036 RID: 54
		private HashSet<Guid> orgAdminMembers;

		// Token: 0x04000037 RID: 55
		private HashSet<Guid> viewOnlyOrgAdminMembers;

		// Token: 0x04000038 RID: 56
		private HashSet<Guid> adminAgentMembers;

		// Token: 0x04000039 RID: 57
		private HashSet<Guid> helpDeskAgentMembers;
	}
}
