using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.EdgeSync.Datacenter;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x02000012 RID: 18
	internal class EhfCompanyAdmins
	{
		// Token: 0x060000AC RID: 172 RVA: 0x00005BA0 File Offset: 0x00003DA0
		public static bool TryGetExternalDirectoryObjectId(ExSearchResultEntry entry, EdgeSyncDiag diagSession, out Guid externalDirectoryObjectId)
		{
			externalDirectoryObjectId = Guid.Empty;
			DirectoryAttribute attribute = entry.GetAttribute("msExchExternalDirectoryObjectId");
			if (attribute == null || attribute.Count == 0)
			{
				diagSession.LogAndTraceError("msExchExternalDirectoryObjectId attribute is not set on '{0}'", new object[]
				{
					entry.DistinguishedName
				});
				return false;
			}
			string text = (string)attribute[0];
			if (!GuidHelper.TryParseGuid(text, out externalDirectoryObjectId))
			{
				diagSession.LogAndTraceError("msExchExternalDirectoryObjectId attribute in '{0}' is set to an invalid Guid '{1}'", new object[]
				{
					entry.DistinguishedName,
					text
				});
				return false;
			}
			return true;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005C28 File Offset: 0x00003E28
		public static EhfCompanyAdmins CreateEhfCompanyAdmins(EhfAdminSyncChangeBuilder ehfAdminSyncChangeBuilder, EhfTargetConnection ehfTargetConnection, EhfADAdapter configADAdapter)
		{
			ExSearchResultEntry exSearchResultEntry = configADAdapter.ReadObjectEntry(ehfAdminSyncChangeBuilder.ConfigUnitDN, false, EhfCompanyAdmins.OtherWellKnownObjectsAttribute);
			if (exSearchResultEntry == null)
			{
				ehfTargetConnection.DiagSession.LogAndTraceError("Could not find Configuration Unit for company {0}. The config naming context is either not replicated or the organization is deleted", new object[]
				{
					ehfAdminSyncChangeBuilder.TenantOU
				});
				return null;
			}
			string text = null;
			string text2 = null;
			DirectoryAttribute attribute = exSearchResultEntry.GetAttribute("otherWellKnownObjects");
			if (attribute == null)
			{
				ehfTargetConnection.DiagSession.LogAndTraceError("Could not find OtherWellKnownObjects attribute in Configuration Unit object for company {0}.", new object[]
				{
					ehfAdminSyncChangeBuilder.TenantOU
				});
				return null;
			}
			foreach (object obj in attribute.GetValues(typeof(string)))
			{
				DNWithBinary dnwithBinary;
				if (DNWithBinary.TryParse(obj as string, out dnwithBinary))
				{
					try
					{
						Guid b = new Guid(dnwithBinary.Binary);
						if (WellKnownGuid.EoaWkGuid == b)
						{
							text = dnwithBinary.DistinguishedName;
						}
						if (WellKnownGuid.EraWkGuid == b)
						{
							text2 = dnwithBinary.DistinguishedName;
						}
						if (text != null && text2 != null)
						{
							break;
						}
					}
					catch (ArgumentException exception)
					{
						ehfTargetConnection.DiagSession.LogAndTraceException(exception, "OtherWellKnownObjects attribute for company {0} contains an entry with invalid Binary part.", new object[]
						{
							ehfAdminSyncChangeBuilder.TenantOU
						});
					}
				}
			}
			return new EhfCompanyAdmins(ehfAdminSyncChangeBuilder, ehfTargetConnection, text, text2, configADAdapter);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005D78 File Offset: 0x00003F78
		private EhfCompanyAdmins(EhfAdminSyncChangeBuilder builder, EhfTargetConnection targetConnection, string orgManagementGroupDN, string viewOnlyOrgManagementGroupDN, EhfADAdapter configADAdapter)
		{
			EhfWellKnownGroup ehfWellKnownGroup = null;
			EhfWellKnownGroup ehfWellKnownGroup2 = null;
			EhfWellKnownGroup ehfWellKnownGroup3 = null;
			EhfWellKnownGroup ehfWellKnownGroup4 = null;
			this.tenantOU = builder.TenantOU;
			this.ehfTargetConnection = targetConnection;
			if (builder.DeletedObjects.Count != 0)
			{
				this.CacheAdminSyncState(configADAdapter);
			}
			bool flag = builder.UpdateOrgManagementGroup || builder.HasDirectChangeForGroup(orgManagementGroupDN) || (this.ehfAdminSyncState != null && this.AdminGroupMemberDeleted(builder, this.ehfAdminSyncState.OrganizationManagmentMembers, orgManagementGroupDN));
			bool flag2 = builder.UpdateViewOnlyOrgManagementGroup || builder.HasDirectChangeForGroup(viewOnlyOrgManagementGroupDN) || (this.ehfAdminSyncState != null && this.AdminGroupMemberDeleted(builder, this.ehfAdminSyncState.ViewOnlyOrganizationManagmentMembers, viewOnlyOrgManagementGroupDN));
			bool flag3 = builder.UpdateAdminAgentGroup || (this.ehfAdminSyncState != null && this.AdminGroupMemberDeleted(builder, this.ehfAdminSyncState.AdminAgentMembers, EhfCompanyAdmins.AdminAgentGroupNamePrefix));
			bool flag4 = builder.UpdateHelpdeskAgentGroup || (this.ehfAdminSyncState != null && this.AdminGroupMemberDeleted(builder, this.ehfAdminSyncState.HelpdeskAgentMembers, EhfCompanyAdmins.HelpdeskAgentGroupNamePrefix));
			if ((flag || builder.GroupChanges.Count != 0 || builder.LiveIdChanges.Count != 0) && orgManagementGroupDN != null)
			{
				ehfWellKnownGroup = this.GetMembersOfGroupFromDN(orgManagementGroupDN, false, targetConnection.DiagSession);
			}
			if ((flag2 || builder.GroupChanges.Count != 0 || builder.LiveIdChanges.Count != 0) && viewOnlyOrgManagementGroupDN != null)
			{
				ehfWellKnownGroup2 = this.GetMembersOfGroupFromDN(viewOnlyOrgManagementGroupDN, false, targetConnection.DiagSession);
			}
			if (builder.GroupChanges.Count != 0 || builder.LiveIdChanges.Count != 0 || flag3 || flag4)
			{
				string text = null;
				string text2 = null;
				foreach (ExSearchResultEntry exSearchResultEntry in this.ehfTargetConnection.ADAdapter.PagedScan(this.tenantOU, EhfCompanyAdmins.PartnerAdminGroupFilter, new string[0]))
				{
					targetConnection.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Found a Partner Admin group {0}", new object[]
					{
						exSearchResultEntry.DistinguishedName
					});
					if (exSearchResultEntry.DistinguishedName.StartsWith(EhfWellKnownGroup.AdminAgentGroupDnPrefix))
					{
						text = exSearchResultEntry.DistinguishedName;
					}
					else if (exSearchResultEntry.DistinguishedName.StartsWith(EhfWellKnownGroup.HelpdeskAgentGroupDnPrefix))
					{
						text2 = exSearchResultEntry.DistinguishedName;
					}
				}
				targetConnection.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "AdminAgentPartnerGroup={0}; HelpDeskAgentPartnerGroup = {1}", new object[]
				{
					text ?? "<null>",
					text2 ?? "<null>"
				});
				if (flag3 && text != null)
				{
					ehfWellKnownGroup3 = this.GetMembersOfGroupFromDN(text, true, targetConnection.DiagSession);
				}
				if (flag4 && text2 != null)
				{
					ehfWellKnownGroup4 = this.GetMembersOfGroupFromDN(text2, true, targetConnection.DiagSession);
				}
			}
			EdgeSyncDiag diagSession = builder.EhfTargetConnection.DiagSession;
			if (!flag && ehfWellKnownGroup != null && (EhfCompanyAdmins.RelevantChangePresent<AdminSyncUser>(builder.GroupChanges, ehfWellKnownGroup.SubGroups, diagSession) || EhfCompanyAdmins.RelevantChangePresent<MailboxAdminSyncUser>(builder.LiveIdChanges, ehfWellKnownGroup.GroupMembers, diagSession)))
			{
				flag = true;
			}
			if (!flag2 && ehfWellKnownGroup2 != null && (EhfCompanyAdmins.RelevantChangePresent<AdminSyncUser>(builder.GroupChanges, ehfWellKnownGroup2.SubGroups, diagSession) || EhfCompanyAdmins.RelevantChangePresent<MailboxAdminSyncUser>(builder.LiveIdChanges, ehfWellKnownGroup2.GroupMembers, diagSession)))
			{
				flag2 = true;
			}
			if (!flag3 && ehfWellKnownGroup3 != null && (EhfCompanyAdmins.RelevantChangePresent<AdminSyncUser>(builder.GroupChanges, ehfWellKnownGroup3.SubGroups, diagSession) || EhfCompanyAdmins.RelevantChangePresent<MailboxAdminSyncUser>(builder.LiveIdChanges, ehfWellKnownGroup3.GroupMembers, diagSession)))
			{
				flag3 = true;
			}
			if (!flag4 && ehfWellKnownGroup4 != null && (EhfCompanyAdmins.RelevantChangePresent<AdminSyncUser>(builder.GroupChanges, ehfWellKnownGroup4.SubGroups, diagSession) || EhfCompanyAdmins.RelevantChangePresent<MailboxAdminSyncUser>(builder.LiveIdChanges, ehfWellKnownGroup4.GroupMembers, diagSession)))
			{
				flag4 = true;
			}
			if (flag)
			{
				this.organizationManagement = ehfWellKnownGroup;
			}
			if (flag2)
			{
				this.viewOnlyOrganizationManagement = ehfWellKnownGroup2;
			}
			if (flag3)
			{
				this.adminAgent = ehfWellKnownGroup3;
			}
			if (flag4)
			{
				this.helpdeskAgent = ehfWellKnownGroup4;
			}
			if (this.IsSyncRequired)
			{
				this.CacheAdminSyncState(configADAdapter);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000614C File Offset: 0x0000434C
		public int CompanyId
		{
			get
			{
				if (this.EhfCompanyIdentity == null)
				{
					return 0;
				}
				return this.EhfCompanyIdentity.EhfCompanyId;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00006163 File Offset: 0x00004363
		public EhfCompanyIdentity EhfCompanyIdentity
		{
			get
			{
				if (this.ehfAdminSyncState == null)
				{
					return null;
				}
				return this.ehfAdminSyncState.EhfCompanyIdentity;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000617A File Offset: 0x0000437A
		public EhfAdminSyncState EhfAdminSyncState
		{
			get
			{
				return this.ehfAdminSyncState;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00006182 File Offset: 0x00004382
		public bool PerimeterConfigNotReplicatedOrIsDeleted
		{
			get
			{
				return this.EhfCompanyIdentity == null;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000618D File Offset: 0x0000438D
		public bool IsSyncRequired
		{
			get
			{
				return this.HasLocalAdminChanges || this.HasPartnerAdminGroupChanges;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000619F File Offset: 0x0000439F
		public bool HasLocalAdminChanges
		{
			get
			{
				return this.organizationManagement != null || this.viewOnlyOrganizationManagement != null;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000061B7 File Offset: 0x000043B7
		public bool HasPartnerAdminGroupChanges
		{
			get
			{
				return this.adminAgent != null || this.helpdeskAgent != null;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000061CF File Offset: 0x000043CF
		public string TenantOU
		{
			get
			{
				return this.tenantOU;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000061D7 File Offset: 0x000043D7
		public EhfWellKnownGroup OrganizationMangement
		{
			get
			{
				return this.organizationManagement;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000061DF File Offset: 0x000043DF
		public EhfWellKnownGroup ViewonlyOrganizationManagement
		{
			get
			{
				return this.viewOnlyOrganizationManagement;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000061E7 File Offset: 0x000043E7
		public EhfWellKnownGroup AdminAgent
		{
			get
			{
				return this.adminAgent;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000061EF File Offset: 0x000043EF
		public EhfWellKnownGroup HelpdeskAgent
		{
			get
			{
				return this.helpdeskAgent;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000061F8 File Offset: 0x000043F8
		public override string ToString()
		{
			if (!this.IsSyncRequired)
			{
				return string.Format("No sync required for {0}", this.tenantOU);
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("OrgName: ");
			stringBuilder.Append(this.tenantOU);
			stringBuilder.Append("(");
			stringBuilder.Append(this.CompanyId);
			stringBuilder.Append(")");
			stringBuilder.Append("OM Admins: ");
			if (this.organizationManagement != null)
			{
				this.organizationManagement.ToString(stringBuilder);
			}
			else
			{
				stringBuilder.Append("<NoChange>");
			}
			stringBuilder.Append("VOM Admins: ");
			if (this.viewOnlyOrganizationManagement != null)
			{
				this.viewOnlyOrganizationManagement.ToString(stringBuilder);
			}
			else
			{
				stringBuilder.Append("<NoChange>");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000062C4 File Offset: 0x000044C4
		public CompanyAdministrators GetLocalAdminsToSync(EdgeSyncDiag diagSession)
		{
			string[] array = null;
			Guid[] array2 = null;
			if (this.organizationManagement != null)
			{
				array = this.organizationManagement.GetWlidsOfGroupMembers(1000, diagSession);
				array2 = this.organizationManagement.GetLinkedPartnerGroupGuidsOfLinkedRoleGroups(1000, diagSession);
			}
			string[] array3 = null;
			Guid[] array4 = null;
			if (this.viewOnlyOrganizationManagement != null)
			{
				array3 = this.viewOnlyOrganizationManagement.GetWlidsOfGroupMembers(1000, diagSession);
				array4 = this.viewOnlyOrganizationManagement.GetLinkedPartnerGroupGuidsOfLinkedRoleGroups(1000, diagSession);
			}
			Dictionary<Role, string[]> dictionary = new Dictionary<Role, string[]>();
			if (array != null)
			{
				dictionary[Role.Administrator] = array;
			}
			if (array3 != null)
			{
				dictionary[Role.ReadOnlyAdministrator] = array3;
			}
			Dictionary<Role, Guid[]> dictionary2 = new Dictionary<Role, Guid[]>();
			if (array2 != null)
			{
				dictionary2[Role.Administrator] = array2;
			}
			if (array4 != null)
			{
				dictionary2[Role.ReadOnlyAdministrator] = array4;
			}
			CompanyAdministrators companyAdministrators = new CompanyAdministrators();
			companyAdministrators.CompanyId = this.CompanyId;
			if (dictionary.Count != 0)
			{
				companyAdministrators.AdminUsers = dictionary;
			}
			if (dictionary2.Count != 0)
			{
				companyAdministrators.AdminGroups = dictionary2;
			}
			return companyAdministrators;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000063AC File Offset: 0x000045AC
		private static bool RelevantChangePresent<TUser>(List<ExSearchResultEntry> changedEntries, Dictionary<Guid, TUser> relevantEntries, EdgeSyncDiag diagSession) where TUser : AdminSyncUser
		{
			if (relevantEntries.Count == 0)
			{
				return false;
			}
			foreach (ExSearchResultEntry exSearchResultEntry in changedEntries)
			{
				if (relevantEntries.ContainsKey(exSearchResultEntry.GetObjectGuid()))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00006414 File Offset: 0x00004614
		private static bool IsGroup(ExSearchResultEntry searchEntry)
		{
			return searchEntry.MultiValuedAttributeContain("objectClass", "group");
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00006428 File Offset: 0x00004628
		private static bool IsPartnerManagedLinkedRoleGroup(ExSearchResultEntry searchEntry, EdgeSyncDiag diagSession, out Guid groupGuid)
		{
			groupGuid = Guid.Empty;
			if (!EhfCompanyAdmins.IsPartnerManagedGroup(searchEntry, diagSession))
			{
				return false;
			}
			DirectoryAttribute directoryAttribute;
			if (!searchEntry.Attributes.TryGetValue("msExchPartnerGroupID", out directoryAttribute))
			{
				diagSession.LogAndTraceError("msExchPartnerGroupID attribute is not present in the partner managed group '{0}'", new object[]
				{
					searchEntry.DistinguishedName
				});
				return false;
			}
			if (directoryAttribute == null || directoryAttribute.Count == 0)
			{
				diagSession.LogAndTraceError("msExchPartnerGroupID attribute is not set on the partner managed group '{0}'", new object[]
				{
					searchEntry.DistinguishedName
				});
				return false;
			}
			string text = directoryAttribute[0] as string;
			if (string.IsNullOrEmpty(text))
			{
				diagSession.LogAndTraceError("msExchPartnerGroupID attribute is empty for the partner managed group '{0}'", new object[]
				{
					searchEntry.DistinguishedName
				});
				return false;
			}
			LinkedPartnerGroupInformation linkedPartnerGroupInformation;
			try
			{
				linkedPartnerGroupInformation = LinkedPartnerGroupInformation.Parse(text);
			}
			catch (ArgumentException)
			{
				diagSession.LogAndTraceError("msExchPartnerGroupID attribute value '{0}' is not in the expected format for '{1}'", new object[]
				{
					text,
					searchEntry.DistinguishedName
				});
				return false;
			}
			if (string.IsNullOrEmpty(linkedPartnerGroupInformation.LinkedPartnerGroupId))
			{
				diagSession.LogAndTraceError("msExchPartnerGroupID attribute value '{0}' has an empty LinkdedPartnetGroupId:  '{1}'", new object[]
				{
					text,
					searchEntry.DistinguishedName
				});
				return false;
			}
			if (GuidHelper.TryParseGuid(linkedPartnerGroupInformation.LinkedPartnerGroupId, out groupGuid))
			{
				return true;
			}
			diagSession.LogAndTraceError("msExchPartnerGroupID attribute value '{0}' is not a valid Guid: '{1}'", new object[]
			{
				text,
				searchEntry.DistinguishedName
			});
			return false;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00006594 File Offset: 0x00004794
		private static bool IsPartnerManagedGroup(ExSearchResultEntry searchEntry, EdgeSyncDiag diagSession)
		{
			DirectoryAttribute directoryAttribute;
			if (!searchEntry.Attributes.TryGetValue("msExchCapabilityIdentifiers", out directoryAttribute) || directoryAttribute == null)
			{
				return false;
			}
			foreach (object obj in directoryAttribute.GetValues(typeof(string)))
			{
				string text = obj as string;
				if (!string.IsNullOrEmpty(text))
				{
					Capability capability;
					if (EnumValidator.TryParse<Capability>(text, EnumParseOptions.AllowNumericConstants | EnumParseOptions.IgnoreCase, out capability))
					{
						if (capability == Capability.Partner_Managed)
						{
							return true;
						}
					}
					else
					{
						diagSession.LogAndTraceError("Capability value '{0}' is not in the expected format on the object '{1}'", new object[]
						{
							text,
							searchEntry.DistinguishedName
						});
					}
				}
			}
			return false;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00006634 File Offset: 0x00004834
		private bool TryGetCompanyState(string ouDN, EhfTargetConnection targetConnection, EhfADAdapter configADAdapter, out EhfAdminSyncState adminSyncState)
		{
			EhfADAdapter adadapter = this.ehfTargetConnection.ADAdapter;
			adminSyncState = null;
			ExSearchResultEntry exSearchResultEntry = adadapter.ReadObjectEntry(ouDN, false, EhfCompanyAdmins.ConfigUnitAttribute);
			if (exSearchResultEntry == null)
			{
				targetConnection.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Could not load object with DN <{0}>; Cannot determine the CompanyId", new object[]
				{
					ouDN
				});
				return false;
			}
			DirectoryAttribute attribute = exSearchResultEntry.GetAttribute("msExchCU");
			if (attribute == null)
			{
				targetConnection.DiagSession.LogAndTraceError("Could not load the Configuration Containter for {0}", new object[]
				{
					ouDN
				});
				return false;
			}
			string configUnitDN = (string)attribute[0];
			return EhfCompanySynchronizer.TryGetEhfAdminSyncState(configUnitDN, configADAdapter, targetConnection, "Ignoring admin account changes, will retry in next sync cycle", out adminSyncState);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000066D6 File Offset: 0x000048D6
		private void CacheAdminSyncState(EhfADAdapter configADAdapter)
		{
			if (this.ehfAdminSyncState != null)
			{
				return;
			}
			this.TryGetCompanyState(this.tenantOU, this.ehfTargetConnection, configADAdapter, out this.ehfAdminSyncState);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000066FC File Offset: 0x000048FC
		private EhfWellKnownGroup GetMembersOfWellKnownGroup(string groupWellKnownName, bool isPartnerAdminGroup, EdgeSyncDiag diagSession)
		{
			string groupDistinguishedName = string.Format("CN={0},{1}", groupWellKnownName, this.tenantOU);
			return this.GetMembersOfGroupFromDN(groupDistinguishedName, isPartnerAdminGroup, diagSession);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00006724 File Offset: 0x00004924
		private EhfWellKnownGroup GetMembersOfGroupFromDN(string groupDistinguishedName, bool isPartnerAdminGroup, EdgeSyncDiag diagSession)
		{
			EhfWellKnownGroup ehfWellKnownGroup;
			if (isPartnerAdminGroup)
			{
				ExSearchResultEntry exSearchResultEntry = this.ehfTargetConnection.ADAdapter.ReadObjectEntry(groupDistinguishedName, false, EhfCompanyAdmins.AttributesToFetchFromMembers);
				if (exSearchResultEntry == null)
				{
					diagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Could not find wellknown partner admin group {0}", new object[]
					{
						groupDistinguishedName
					});
					return null;
				}
				if (!EhfCompanyAdmins.IsPartnerManagedGroup(exSearchResultEntry, diagSession))
				{
					diagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Found the partner group {0}, but it is no partner Managed", new object[]
					{
						groupDistinguishedName
					});
					return null;
				}
				Guid externalDirectoryObjectId;
				if (!EhfCompanyAdmins.TryGetExternalDirectoryObjectId(exSearchResultEntry, diagSession, out externalDirectoryObjectId))
				{
					return null;
				}
				ehfWellKnownGroup = new EhfWellKnownGroup(groupDistinguishedName, externalDirectoryObjectId);
			}
			else
			{
				ehfWellKnownGroup = new EhfWellKnownGroup(groupDistinguishedName);
			}
			Stack<string> stack = new Stack<string>();
			stack.Push(groupDistinguishedName);
			while (stack.Count != 0)
			{
				string text = stack.Pop();
				string query = string.Format("(memberOf={0})", text);
				diagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.High, "Expanding group {0}", new object[]
				{
					text
				});
				IEnumerable<ExSearchResultEntry> enumerable = this.ehfTargetConnection.ADAdapter.PagedScan(this.tenantOU, query, EhfCompanyAdmins.AttributesToFetchFromMembers);
				int num = 0;
				foreach (ExSearchResultEntry exSearchResultEntry2 in enumerable)
				{
					num++;
					if (!exSearchResultEntry2.IsDeleted)
					{
						Guid objectGuid = exSearchResultEntry2.GetObjectGuid();
						if (EhfCompanyAdmins.IsGroup(exSearchResultEntry2))
						{
							Guid partnerGroupGuid;
							if (ehfWellKnownGroup.SubGroups.ContainsKey(objectGuid) || ehfWellKnownGroup.LinkedRoleGroups.ContainsKey(objectGuid))
							{
								this.ehfTargetConnection.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Group {0} is already processed. Ignoring it.", new object[]
								{
									exSearchResultEntry2.DistinguishedName
								});
							}
							else if (EhfCompanyAdmins.IsPartnerManagedLinkedRoleGroup(exSearchResultEntry2, diagSession, out partnerGroupGuid))
							{
								ehfWellKnownGroup.LinkedRoleGroups.Add(objectGuid, new PartnerGroupAdminSyncUser(exSearchResultEntry2.DistinguishedName, objectGuid, partnerGroupGuid));
							}
							else
							{
								ehfWellKnownGroup.SubGroups.Add(objectGuid, new AdminSyncUser(exSearchResultEntry2.DistinguishedName, objectGuid));
								stack.Push(exSearchResultEntry2.DistinguishedName);
							}
						}
						else
						{
							string text2 = string.Empty;
							if (exSearchResultEntry2.Attributes.ContainsKey("msExchWindowsLiveID") && exSearchResultEntry2.Attributes["msExchWindowsLiveID"].Count != 0)
							{
								text2 = (string)exSearchResultEntry2.Attributes["msExchWindowsLiveID"][0];
								if (text2 != null)
								{
									text2 = text2.Trim();
								}
							}
							else
							{
								diagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "WindowsLiveID is not set for {0}", new object[]
								{
									exSearchResultEntry2.DistinguishedName
								});
							}
							if (!ehfWellKnownGroup.GroupMembers.ContainsKey(objectGuid))
							{
								ehfWellKnownGroup.GroupMembers.Add(objectGuid, new MailboxAdminSyncUser(text2, objectGuid, exSearchResultEntry2.DistinguishedName));
							}
						}
					}
					else
					{
						diagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "{0} is deleted, ignoring...", new object[]
						{
							exSearchResultEntry2.DistinguishedName
						});
					}
				}
				diagSession.Tracer.TraceDebug<string, int>((long)this.GetHashCode(), "Expanded group {0}. Found {1} children", text, num);
			}
			return ehfWellKnownGroup;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00006A2C File Offset: 0x00004C2C
		private bool AdminGroupMemberDeleted(EhfAdminSyncChangeBuilder builder, HashSet<Guid> previousSyncState, string groupName)
		{
			if (previousSyncState == null)
			{
				return false;
			}
			if (previousSyncState.Count == 1 && previousSyncState.Contains(EhfCompanyAdmins.SyncStateFullGuid))
			{
				this.ehfTargetConnection.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Admin State in PerimeterConfig is full for group <{0}> in company <{1}>. Treating the deleted object as an admin.", new object[]
				{
					groupName,
					builder.TenantOU
				});
				return true;
			}
			foreach (Guid guid in builder.DeletedObjects)
			{
				if (previousSyncState.Contains(guid))
				{
					this.ehfTargetConnection.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Deleted object with Guid <{0}> is a member of admin group <{1}> in company <{2}>.", new object[]
					{
						guid,
						groupName,
						builder.TenantOU
					});
					return true;
				}
			}
			this.ehfTargetConnection.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "None of the Deleted objects is a member of admin group <{0}> in company <{1}>.", new object[]
			{
				groupName,
				builder.TenantOU
			});
			return false;
		}

		// Token: 0x0400003A RID: 58
		public static readonly string AdminAgentGroupNamePrefix = "TenantAdmins_";

		// Token: 0x0400003B RID: 59
		public static readonly string HelpdeskAgentGroupNamePrefix = "HelpdeskAdmins_";

		// Token: 0x0400003C RID: 60
		public static readonly Guid SyncStateFullGuid = Guid.Empty;

		// Token: 0x0400003D RID: 61
		private static readonly string PartnerAdminGroupFilter = "(&(objectClass=group)(groupType:1.2.840.113556.1.4.803:=2147483656)(msExchCU=*)(msExchOURoot=*)(|(name=TenantAdmins_*)(name=HelpdeskAdmins_*)))";

		// Token: 0x0400003E RID: 62
		private static readonly string[] ConfigUnitAttribute = new string[]
		{
			"msExchCU"
		};

		// Token: 0x0400003F RID: 63
		private static readonly string[] AttributesToFetchFromMembers = new string[]
		{
			"msExchCU",
			"objectGUID",
			"objectClass",
			"msExchWindowsLiveID",
			"msExchCapabilityIdentifiers",
			"msExchPartnerGroupID",
			"msExchExternalDirectoryObjectId"
		};

		// Token: 0x04000040 RID: 64
		private static readonly string[] OtherWellKnownObjectsAttribute = new string[]
		{
			"otherWellKnownObjects"
		};

		// Token: 0x04000041 RID: 65
		private string tenantOU;

		// Token: 0x04000042 RID: 66
		private EhfAdminSyncState ehfAdminSyncState;

		// Token: 0x04000043 RID: 67
		private EhfWellKnownGroup organizationManagement;

		// Token: 0x04000044 RID: 68
		private EhfWellKnownGroup viewOnlyOrganizationManagement;

		// Token: 0x04000045 RID: 69
		private EhfWellKnownGroup adminAgent;

		// Token: 0x04000046 RID: 70
		private EhfWellKnownGroup helpdeskAgent;

		// Token: 0x04000047 RID: 71
		private EhfTargetConnection ehfTargetConnection;
	}
}
