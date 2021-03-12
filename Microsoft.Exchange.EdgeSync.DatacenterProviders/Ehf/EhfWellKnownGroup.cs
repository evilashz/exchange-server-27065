using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.EdgeSync.Datacenter;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x02000030 RID: 48
	internal class EhfWellKnownGroup
	{
		// Token: 0x06000220 RID: 544 RVA: 0x0000EC81 File Offset: 0x0000CE81
		public EhfWellKnownGroup(string groupName)
		{
			this.wellknownGroupName = groupName;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000ECB1 File Offset: 0x0000CEB1
		public EhfWellKnownGroup(string groupName, Guid externalDirectoryObjectId) : this(groupName)
		{
			this.externalDirectoryObjectId = externalDirectoryObjectId;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000ECC1 File Offset: 0x0000CEC1
		public Dictionary<Guid, MailboxAdminSyncUser> GroupMembers
		{
			get
			{
				return this.groupMembers;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000ECC9 File Offset: 0x0000CEC9
		public Dictionary<Guid, AdminSyncUser> SubGroups
		{
			get
			{
				return this.subGroups;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000ECD1 File Offset: 0x0000CED1
		public Dictionary<Guid, PartnerGroupAdminSyncUser> LinkedRoleGroups
		{
			get
			{
				return this.linkedRoleGroups;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000ECD9 File Offset: 0x0000CED9
		public Guid ExternalDirectoryObjectId
		{
			get
			{
				return this.externalDirectoryObjectId;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000ECE1 File Offset: 0x0000CEE1
		public string WellKnownGroupName
		{
			get
			{
				return this.wellknownGroupName;
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000ECE9 File Offset: 0x0000CEE9
		public static bool IsViewOnlyOrganizationManagementGroup(ExSearchResultEntry change)
		{
			return change.DistinguishedName.StartsWith(EhfWellKnownGroup.VomRoleGroupPrefix, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000ECFC File Offset: 0x0000CEFC
		public static bool IsOrganizationManagementGroup(ExSearchResultEntry change)
		{
			return change.DistinguishedName.StartsWith(EhfWellKnownGroup.OMRoleGroupPrefix, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000ED0F File Offset: 0x0000CF0F
		public static bool IsWellKnownPartnerGroupDN(string dn)
		{
			return EhfWellKnownGroup.IsAdminAgentGroup(dn) || EhfWellKnownGroup.IsHelpdeskAgentGroup(dn);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000ED21 File Offset: 0x0000CF21
		public static bool IsAdminAgentGroup(string dn)
		{
			return dn.StartsWith(EhfWellKnownGroup.AdminAgentGroupDnPrefix, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000ED2F File Offset: 0x0000CF2F
		public static bool IsHelpdeskAgentGroup(string dn)
		{
			return dn.StartsWith(EhfWellKnownGroup.HelpdeskAgentGroupDnPrefix, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000ED40 File Offset: 0x0000CF40
		public string[] GetWlidsOfGroupMembers(int maxAdmins, EdgeSyncDiag diagSession)
		{
			List<string> list = new List<string>(this.groupMembers.Count);
			foreach (MailboxAdminSyncUser mailboxAdminSyncUser in this.groupMembers.Values)
			{
				if (!string.IsNullOrEmpty(mailboxAdminSyncUser.WindowsLiveId))
				{
					if (!EhfWellKnownGroup.ValidateWindowsLiveId(mailboxAdminSyncUser))
					{
						diagSession.LogAndTraceError("WLID <{0}> for user <{1}>:<{2}> is not valid. The admin will be ignored from group <{3}>.", new object[]
						{
							mailboxAdminSyncUser.WindowsLiveId,
							mailboxAdminSyncUser.DistinguishedName,
							mailboxAdminSyncUser.UserGuid,
							this.wellknownGroupName
						});
					}
					else
					{
						list.Add(mailboxAdminSyncUser.WindowsLiveId);
					}
					if (list.Count == maxAdmins)
					{
						diagSession.LogAndTraceError("The group <{0}> has more than the maximum allowed number ({1}) of members. Only the first <{1}> will be taken.", new object[]
						{
							this.wellknownGroupName,
							maxAdmins
						});
						break;
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000EE48 File Offset: 0x0000D048
		public Guid[] GetLinkedPartnerGroupGuidsOfLinkedRoleGroups(int maxMembers, EdgeSyncDiag diagSession)
		{
			if (this.linkedRoleGroups == null)
			{
				return null;
			}
			Guid[] array = new Guid[Math.Min(this.linkedRoleGroups.Count, maxMembers)];
			int num = 0;
			foreach (PartnerGroupAdminSyncUser partnerGroupAdminSyncUser in this.linkedRoleGroups.Values)
			{
				array[num++] = partnerGroupAdminSyncUser.PartnerGroupGuid;
				if (num == maxMembers)
				{
					diagSession.LogAndTraceError("The group <{0}> has more than the maximum allowed number ({1}) of partner linked role groups. Only the first <{1}> will be taken.", new object[]
					{
						this.wellknownGroupName,
						maxMembers
					});
					break;
				}
			}
			return array;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000EF04 File Offset: 0x0000D104
		public void ToString(StringBuilder sb)
		{
			EhfWellKnownGroup.AddAdminToList<MailboxAdminSyncUser>(this.groupMembers, sb, "Local Admins: ");
			EhfWellKnownGroup.AddAdminToList<PartnerGroupAdminSyncUser>(this.linkedRoleGroups, sb, "Partner Admins: ");
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000EF28 File Offset: 0x0000D128
		private static bool ValidateWindowsLiveId(MailboxAdminSyncUser user)
		{
			return user.WindowsLiveId.Trim().Length >= 6 && user.WindowsLiveId.Trim().Length <= 320 && EhfWellKnownGroup.EmailAddressRegex.Match(user.WindowsLiveId).Success;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000EF78 File Offset: 0x0000D178
		private static void AddAdminToList<T>(Dictionary<Guid, T> admins, StringBuilder stringBuilder, string adminName) where T : AdminSyncUser
		{
			stringBuilder.Append("; ");
			stringBuilder.Append(adminName);
			if (admins != null)
			{
				bool flag = false;
				using (Dictionary<Guid, T>.Enumerator enumerator = admins.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<Guid, T> keyValuePair = enumerator.Current;
						if (flag)
						{
							stringBuilder.Append(",");
						}
						else
						{
							flag = true;
						}
						stringBuilder.Append(keyValuePair.Value);
					}
					return;
				}
			}
			stringBuilder.Append("<NoChange> ");
		}

		// Token: 0x040000D5 RID: 213
		public const int MaxAdminsPerRole = 1000;

		// Token: 0x040000D6 RID: 214
		public const int MaxGroupUsers = 2000;

		// Token: 0x040000D7 RID: 215
		public const int MinEmailLength = 6;

		// Token: 0x040000D8 RID: 216
		public const int MaxEmailLength = 320;

		// Token: 0x040000D9 RID: 217
		public static readonly string AdminAgentGroupDnPrefix = string.Format("CN={0}", EhfCompanyAdmins.AdminAgentGroupNamePrefix);

		// Token: 0x040000DA RID: 218
		public static readonly string HelpdeskAgentGroupDnPrefix = string.Format("CN={0}", EhfCompanyAdmins.HelpdeskAgentGroupNamePrefix);

		// Token: 0x040000DB RID: 219
		private static readonly string OMRoleGroupPrefix = string.Format("CN={0},", "Organization Management");

		// Token: 0x040000DC RID: 220
		private static readonly string VomRoleGroupPrefix = string.Format("CN={0},", "View-Only Organization Management");

		// Token: 0x040000DD RID: 221
		private static readonly Regex EmailAddressRegex = new Regex("^(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@([a-z0-9]([a-z0-9\\-]{0,61}[a-z0-9])?\\.)+[a-z]{2,8}$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x040000DE RID: 222
		private string wellknownGroupName;

		// Token: 0x040000DF RID: 223
		private Dictionary<Guid, MailboxAdminSyncUser> groupMembers = new Dictionary<Guid, MailboxAdminSyncUser>();

		// Token: 0x040000E0 RID: 224
		private Dictionary<Guid, AdminSyncUser> subGroups = new Dictionary<Guid, AdminSyncUser>();

		// Token: 0x040000E1 RID: 225
		private Dictionary<Guid, PartnerGroupAdminSyncUser> linkedRoleGroups = new Dictionary<Guid, PartnerGroupAdminSyncUser>();

		// Token: 0x040000E2 RID: 226
		private Guid externalDirectoryObjectId;
	}
}
