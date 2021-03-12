using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000D0 RID: 208
	internal sealed class OAuthAppPoolLevelPolicy
	{
		// Token: 0x0600071B RID: 1819 RVA: 0x000328C4 File Offset: 0x00030AC4
		private OAuthAppPoolLevelPolicy() : this(new StringAppSettingsEntry("OAuthHttpModule.Profiles", string.Empty, ExTraceGlobals.OAuthTracer).Value, new StringAppSettingsEntry("OAuthHttpModule.V1AppScopes", Constants.ClaimValues.UserImpersonation, ExTraceGlobals.OAuthTracer).Value, new StringAppSettingsEntry("OAuthHttpModule.V1AppRoles", string.Empty, ExTraceGlobals.OAuthTracer).Value)
		{
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00032930 File Offset: 0x00030B30
		internal OAuthAppPoolLevelPolicy(string profiles, string scopes, string roles)
		{
			this.allowedProfiles = (from t in profiles.Split(new char[]
			{
				OAuthAppPoolLevelPolicy.seperator
			})
			where Constants.TokenCategories.All.Contains(t)
			select t).ToArray<string>();
			this.allowAnyV1AppScope = string.Equals(scopes, OAuthAppPoolLevelPolicy.allowAnything);
			if (!this.allowAnyV1AppScope)
			{
				this.allowedV1AppScopes = scopes.Split(new char[]
				{
					OAuthAppPoolLevelPolicy.seperator
				});
			}
			this.allowAnyV1AppRole = string.Equals(roles, OAuthAppPoolLevelPolicy.allowAnything);
			if (!this.allowAnyV1AppRole)
			{
				this.allowedV1AppRoles = roles.Split(new char[]
				{
					OAuthAppPoolLevelPolicy.seperator
				});
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x000329F0 File Offset: 0x00030BF0
		public static OAuthAppPoolLevelPolicy Instance
		{
			get
			{
				return OAuthAppPoolLevelPolicy.instance;
			}
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x000329F7 File Offset: 0x00030BF7
		public bool IsAllowedProfiles(string givenProfile)
		{
			return this.allowedProfiles.Contains(givenProfile, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00032A0A File Offset: 0x00030C0A
		public List<string> GetV1AppScope(string[] givenScope)
		{
			return this.GetFilteredPermissions(givenScope, this.allowAnyV1AppScope, this.allowedV1AppScopes);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00032A1F File Offset: 0x00030C1F
		public List<string> GetV1AppRole(string[] givenRole)
		{
			return this.GetFilteredPermissions(givenRole, this.allowAnyV1AppRole, this.allowedV1AppRoles);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00032A34 File Offset: 0x00030C34
		private List<string> GetFilteredPermissions(string[] permissions, bool allowAny, string[] allowedValues)
		{
			List<string> list = new List<string>();
			if (allowAny)
			{
				list.AddRange(permissions);
			}
			else
			{
				foreach (string text in permissions)
				{
					if (allowedValues.Contains(text, StringComparer.OrdinalIgnoreCase))
					{
						list.Add(text);
					}
				}
			}
			list.Sort();
			return list;
		}

		// Token: 0x04000687 RID: 1671
		private static readonly char seperator = '|';

		// Token: 0x04000688 RID: 1672
		private static readonly char[] claimValueSeperators = new char[]
		{
			' '
		};

		// Token: 0x04000689 RID: 1673
		private static readonly string allowAnything = "*";

		// Token: 0x0400068A RID: 1674
		private static OAuthAppPoolLevelPolicy instance = new OAuthAppPoolLevelPolicy();

		// Token: 0x0400068B RID: 1675
		private readonly string[] allowedProfiles;

		// Token: 0x0400068C RID: 1676
		private readonly bool allowAnyV1AppScope;

		// Token: 0x0400068D RID: 1677
		private readonly string[] allowedV1AppScopes;

		// Token: 0x0400068E RID: 1678
		private readonly bool allowAnyV1AppRole;

		// Token: 0x0400068F RID: 1679
		private readonly string[] allowedV1AppRoles;
	}
}
