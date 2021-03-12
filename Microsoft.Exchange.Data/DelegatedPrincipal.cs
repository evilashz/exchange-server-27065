using System;
using System.Security.Principal;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000134 RID: 308
	internal class DelegatedPrincipal : GenericPrincipal
	{
		// Token: 0x06000AA4 RID: 2724 RVA: 0x00021070 File Offset: 0x0001F270
		internal DelegatedPrincipal(IIdentity identity, string[] roles) : base(identity, roles)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (string.IsNullOrEmpty(identity.Name))
			{
				throw new ArgumentNullException("identity.Name");
			}
			if (!(identity is GenericIdentity) || !DelegatedPrincipal.DelegatedAuthenticationType.Equals(identity.AuthenticationType, StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException("identity");
			}
			if (!DelegatedPrincipal.TryParseDelegatedString(identity.Name, out this.userId, out this.userOrgId, out this.delegatedOrg, out this.displayName, out this.groups))
			{
				throw new ArgumentException("identity.Name");
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00021108 File Offset: 0x0001F308
		private DelegatedPrincipal(string delegatedIdentity, string userId, string userOrgId, string delegatedOrg, string displayName, string[] groups) : base(new GenericIdentity(delegatedIdentity, DelegatedPrincipal.DelegatedAuthenticationType), null)
		{
			if (string.IsNullOrEmpty(delegatedIdentity))
			{
				throw new ArgumentNullException("delegatedIdentity");
			}
			if (string.IsNullOrEmpty(userId))
			{
				throw new ArgumentNullException("userId");
			}
			if (string.IsNullOrEmpty(userOrgId))
			{
				throw new ArgumentNullException("userOrgId");
			}
			if (string.IsNullOrEmpty(delegatedOrg))
			{
				throw new ArgumentNullException("delegatedOrg");
			}
			if (string.IsNullOrEmpty(displayName))
			{
				throw new ArgumentNullException("displayName");
			}
			this.userId = userId;
			this.userOrgId = userOrgId;
			this.delegatedOrg = delegatedOrg;
			this.displayName = displayName;
			this.groups = groups;
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x000211AE File Offset: 0x0001F3AE
		internal string[] Roles
		{
			get
			{
				return this.groups;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x000211B6 File Offset: 0x0001F3B6
		internal string UserId
		{
			get
			{
				return this.userId;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x000211BE File Offset: 0x0001F3BE
		internal string UserOrganizationId
		{
			get
			{
				return this.userOrgId;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x000211C6 File Offset: 0x0001F3C6
		internal string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x000211CE File Offset: 0x0001F3CE
		internal string DelegatedOrganization
		{
			get
			{
				return this.delegatedOrg;
			}
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000211D6 File Offset: 0x0001F3D6
		internal static IIdentity GetDelegatedIdentity(string userId, string userOrgId, string targetOrg, string displayName, string[] groups)
		{
			return new GenericIdentity(DelegatedPrincipal.ToString(userId, userOrgId, targetOrg, displayName, groups), DelegatedPrincipal.DelegatedAuthenticationType);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x000211F0 File Offset: 0x0001F3F0
		internal static IIdentity GetDelegatedIdentity(string delegatedStr)
		{
			string text;
			string text2;
			string text3;
			string text4;
			string[] array;
			if (!DelegatedPrincipal.TryParseDelegatedString(delegatedStr, out text, out text2, out text3, out text4, out array))
			{
				throw new ArgumentException("delegatedStr");
			}
			return new GenericIdentity(DelegatedPrincipal.ToString(text, text2, text3, text4, array), DelegatedPrincipal.DelegatedAuthenticationType);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002122F File Offset: 0x0001F42F
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = string.Format("{0}\\{1}", this.delegatedOrg, this.userId);
			}
			return this.toString;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0002125C File Offset: 0x0001F45C
		public string GetUserName()
		{
			if (this.ToString().Length <= DelegatedPrincipal.MaxNameLength)
			{
				return this.ToString();
			}
			if (this.userId.Length <= DelegatedPrincipal.MaxNameLength)
			{
				return this.userId;
			}
			return this.userId.Substring(0, DelegatedPrincipal.MaxNameLength);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000212AC File Offset: 0x0001F4AC
		internal static bool TryParseDelegatedString(string delegatedStr, out DelegatedPrincipal principal)
		{
			principal = null;
			string text;
			string text2;
			string text3;
			string text4;
			string[] array;
			if (!DelegatedPrincipal.TryParseDelegatedString(delegatedStr, out text, out text2, out text3, out text4, out array))
			{
				return false;
			}
			principal = new DelegatedPrincipal(delegatedStr, text, text2, text3, text4, array);
			return true;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000212E0 File Offset: 0x0001F4E0
		private static bool TryParseDelegatedString(string delegatedStr, out string userId, out string userOrgId, out string delegatedOrg, out string displayName, out string[] groups)
		{
			userId = null;
			userOrgId = null;
			delegatedOrg = null;
			displayName = null;
			groups = null;
			if (string.IsNullOrEmpty(delegatedStr))
			{
				return false;
			}
			string[] array = delegatedStr.Split(new char[]
			{
				DelegatedPrincipal.Separator
			});
			if (array.Length < DelegatedPrincipal.MinNumberOfComponents)
			{
				return false;
			}
			if (array.Length > DelegatedPrincipal.MinNumberOfComponents)
			{
				groups = new string[array.Length - DelegatedPrincipal.MinNumberOfComponents];
				try
				{
					Array.ConstrainedCopy(array, DelegatedPrincipal.MinNumberOfComponents, groups, 0, array.Length - DelegatedPrincipal.MinNumberOfComponents);
					goto IL_81;
				}
				catch (Exception)
				{
					return false;
				}
			}
			groups = new string[0];
			IL_81:
			userId = Uri.UnescapeDataString(array[0]);
			userOrgId = array[1];
			delegatedOrg = Uri.UnescapeDataString(array[2]);
			displayName = Uri.UnescapeDataString(array[3]);
			return true;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x000213A8 File Offset: 0x0001F5A8
		private static string ToString(string userId, string userOrgId, string delegatedOrg, string displayName, string[] groups)
		{
			if (string.IsNullOrEmpty(userId))
			{
				throw new ArgumentNullException("userId");
			}
			if (string.IsNullOrEmpty(userOrgId))
			{
				throw new ArgumentNullException("userOrgId");
			}
			if (string.IsNullOrEmpty(delegatedOrg))
			{
				throw new ArgumentNullException("delegatedOrg");
			}
			if (displayName == null)
			{
				throw new ArgumentNullException("displayName");
			}
			if (groups == null)
			{
				throw new ArgumentNullException("groups");
			}
			string text = Uri.EscapeDataString(userId);
			string text2 = Uri.EscapeDataString(delegatedOrg);
			string text3 = Uri.EscapeDataString(displayName);
			StringBuilder stringBuilder = new StringBuilder(text.Length + userOrgId.Length + text2.Length + text3.Length + groups.Length * 32 + groups.Length + 4);
			stringBuilder.Append(text);
			stringBuilder.Append(DelegatedPrincipal.Separator);
			stringBuilder.Append(userOrgId);
			stringBuilder.Append(DelegatedPrincipal.Separator);
			stringBuilder.Append(text2);
			stringBuilder.Append(DelegatedPrincipal.Separator);
			stringBuilder.Append(text3);
			stringBuilder.Append(DelegatedPrincipal.Separator);
			for (int i = 0; i < groups.Length; i++)
			{
				stringBuilder.Append(groups[i]);
				if (i + 1 < groups.Length)
				{
					stringBuilder.Append(DelegatedPrincipal.Separator);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400066E RID: 1646
		internal static readonly string DelegatedAuthenticationType = "DelegatedPartnerTenant";

		// Token: 0x0400066F RID: 1647
		internal static readonly char Separator = ',';

		// Token: 0x04000670 RID: 1648
		internal static readonly char ExpirationSeparator = '&';

		// Token: 0x04000671 RID: 1649
		internal static readonly int MaxNameLength = 64;

		// Token: 0x04000672 RID: 1650
		private static int MinNumberOfComponents = 4;

		// Token: 0x04000673 RID: 1651
		private string[] groups;

		// Token: 0x04000674 RID: 1652
		private string userId;

		// Token: 0x04000675 RID: 1653
		private string userOrgId;

		// Token: 0x04000676 RID: 1654
		private string delegatedOrg;

		// Token: 0x04000677 RID: 1655
		private string displayName;

		// Token: 0x04000678 RID: 1656
		private string toString;
	}
}
