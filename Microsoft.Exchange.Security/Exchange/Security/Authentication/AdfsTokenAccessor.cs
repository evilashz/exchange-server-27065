using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200003D RID: 61
	internal class AdfsTokenAccessor : CommonAccessTokenAccessor
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x0000C854 File Offset: 0x0000AA54
		private AdfsTokenAccessor(CommonAccessToken token) : base(token)
		{
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000C85D File Offset: 0x0000AA5D
		public override AccessTokenType TokenType
		{
			get
			{
				return AccessTokenType.Adfs;
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000C860 File Offset: 0x0000AA60
		public static AdfsTokenAccessor Create(AdfsIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			CommonAccessToken token = new CommonAccessToken(AccessTokenType.Adfs);
			return new AdfsTokenAccessor(token)
			{
				UserSid = identity.Sid.ToString(),
				UserPrincipalName = identity.PrincipalName,
				OrganizationId = identity.UserOrganizationId,
				GroupMembershipSids = identity.PrepopulatedGroupSidIds,
				IsPublicSession = identity.IsPublicSession
			};
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000C8CB File Offset: 0x0000AACB
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x0000C8D8 File Offset: 0x0000AAD8
		public string UserSid
		{
			get
			{
				return base.SafeGetValue("UserSid");
			}
			set
			{
				base.SafeSetValue("UserSid", value);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000C8E6 File Offset: 0x0000AAE6
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x0000C8F3 File Offset: 0x0000AAF3
		public string UserPrincipalName
		{
			get
			{
				return base.SafeGetValue("UserPrincipalName");
			}
			set
			{
				base.SafeSetValue("UserPrincipalName", value);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000C901 File Offset: 0x0000AB01
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x0000C913 File Offset: 0x0000AB13
		public OrganizationId OrganizationId
		{
			get
			{
				return CommonAccessTokenAccessor.DeserializeOrganizationId(base.SafeGetValue("OrganizationIdBase64"));
			}
			set
			{
				base.SafeSetValue("OrganizationIdBase64", CommonAccessTokenAccessor.SerializeOrganizationId(value));
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000C926 File Offset: 0x0000AB26
		// (set) Token: 0x060001AB RID: 427 RVA: 0x0000C938 File Offset: 0x0000AB38
		public IEnumerable<string> GroupMembershipSids
		{
			get
			{
				return CommonAccessTokenAccessor.DeserializeGroupMembershipSids(base.SafeGetValue("GroupMembershipSids"));
			}
			set
			{
				base.SafeSetValue("GroupMembershipSids", CommonAccessTokenAccessor.SerializeGroupMembershipSids(value));
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000C94B File Offset: 0x0000AB4B
		// (set) Token: 0x060001AD RID: 429 RVA: 0x0000C95D File Offset: 0x0000AB5D
		public bool IsPublicSession
		{
			get
			{
				return CommonAccessTokenAccessor.DeserializIsPublicSession(base.SafeGetValue("IsPublicSession"));
			}
			set
			{
				base.SafeSetValue("IsPublicSession", CommonAccessTokenAccessor.SerializeIsPublicSession(value));
			}
		}
	}
}
