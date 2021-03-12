using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Common;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000017 RID: 23
	[Serializable]
	public class SidBasedIdentity : GenericSidIdentity
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00007493 File Offset: 0x00005693
		public SidBasedIdentity(string userPrincipal, string userSid, string memberName, string authType, string partitionId = null) : base(userSid, authType, new SecurityIdentifier(userSid), partitionId)
		{
			this.userPrincipalName = userPrincipal;
			this.memberName = memberName;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000074B4 File Offset: 0x000056B4
		public SidBasedIdentity(string userPrincipal, string userSid, string memberName) : this(userPrincipal, userSid, memberName, "", null)
		{
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000074C5 File Offset: 0x000056C5
		public string PrincipalName
		{
			get
			{
				return this.userPrincipalName;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000074CD File Offset: 0x000056CD
		public string MemberName
		{
			get
			{
				return this.memberName;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000074D5 File Offset: 0x000056D5
		public IEnumerable<string> PrepopulatedGroupSidIds
		{
			get
			{
				return this.prepopulatedGroupSidIds;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000074DD File Offset: 0x000056DD
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x000074E5 File Offset: 0x000056E5
		public OrganizationId UserOrganizationId
		{
			get
			{
				return this.userOrganizationId;
			}
			protected internal set
			{
				this.userOrganizationId = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000074EE File Offset: 0x000056EE
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x000074F6 File Offset: 0x000056F6
		public OrganizationProperties UserOrganizationProperties
		{
			get
			{
				return this.userOrganizationProperties;
			}
			protected internal set
			{
				this.userOrganizationProperties = value;
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000074FF File Offset: 0x000056FF
		internal void PrepopulateGroupSidIds(List<string> groupSidIds)
		{
			if (groupSidIds == null)
			{
				throw new ArgumentNullException("groupSidIds");
			}
			this.prepopulatedGroupSidIds = groupSidIds;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00007520 File Offset: 0x00005720
		internal override ClientSecurityContext CreateClientSecurityContext()
		{
			SecurityAccessToken securityAccessToken = new SecurityAccessToken();
			securityAccessToken.UserSid = this.Sid.Value;
			ClientSecurityContext result;
			if (this.prepopulatedGroupSidIds != null)
			{
				ExTraceGlobals.CommonTracer.TraceDebug<string>(0L, "[SidBasedIdentity::CreateClientSecurityContext] Attempting to explicitly populate group SIDs with values: '{0}'.", string.Join(", ", this.prepopulatedGroupSidIds));
				securityAccessToken.GroupSids = (from s in this.prepopulatedGroupSidIds
				select new SidStringAndAttributes(s, 4U)).ToArray<SidStringAndAttributes>();
				result = new ClientSecurityContext(securityAccessToken, AuthzFlags.AuthzSkipTokenGroups);
			}
			else
			{
				result = new ClientSecurityContext(securityAccessToken);
			}
			return result;
		}

		// Token: 0x0400013C RID: 316
		private readonly string userPrincipalName;

		// Token: 0x0400013D RID: 317
		private readonly string memberName;

		// Token: 0x0400013E RID: 318
		private OrganizationId userOrganizationId;

		// Token: 0x0400013F RID: 319
		private OrganizationProperties userOrganizationProperties;

		// Token: 0x04000140 RID: 320
		protected IEnumerable<string> prepopulatedGroupSidIds;
	}
}
