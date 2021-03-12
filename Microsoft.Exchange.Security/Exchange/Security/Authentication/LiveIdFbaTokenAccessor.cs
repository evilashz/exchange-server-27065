using System;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200003F RID: 63
	internal class LiveIdFbaTokenAccessor : CommonAccessTokenAccessor
	{
		// Token: 0x060001BF RID: 447 RVA: 0x0000CBB3 File Offset: 0x0000ADB3
		private LiveIdFbaTokenAccessor(CommonAccessToken token) : base(token)
		{
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000CBBC File Offset: 0x0000ADBC
		public override AccessTokenType TokenType
		{
			get
			{
				return AccessTokenType.LiveId;
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000CBC0 File Offset: 0x0000ADC0
		public static LiveIdFbaTokenAccessor Create(ADRawEntry adRawEntry)
		{
			if (adRawEntry == null)
			{
				throw new ArgumentNullException("adRawEntry");
			}
			CommonAccessToken commonAccessToken = new CommonAccessToken(AccessTokenType.LiveId);
			LiveIdFbaTokenAccessor liveIdFbaTokenAccessor = new LiveIdFbaTokenAccessor(commonAccessToken);
			liveIdFbaTokenAccessor.UserSid = ((SecurityIdentifier)adRawEntry[ADMailboxRecipientSchema.Sid]).ToString();
			liveIdFbaTokenAccessor.UserPrincipalName = (string)adRawEntry[ADUserSchema.UserPrincipalName];
			liveIdFbaTokenAccessor.LiveIdMemberName = ((SmtpAddress)adRawEntry[ADRecipientSchema.WindowsLiveID]).ToString();
			OrganizationId organizationId = (OrganizationId)adRawEntry[ADObjectSchema.OrganizationId];
			liveIdFbaTokenAccessor.OrganizationId = organizationId;
			liveIdFbaTokenAccessor.LiveIdHasAcceptedAccruals = true;
			commonAccessToken.ExtensionData["Partition"] = organizationId.PartitionId.ToString();
			commonAccessToken.ExtensionData["OrganizationName"] = organizationId.ConfigurationUnit.Parent.Name;
			return liveIdFbaTokenAccessor;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000CC9C File Offset: 0x0000AE9C
		public static LiveIdFbaTokenAccessor Create(LiveIDIdentity liveIdIdentity)
		{
			if (liveIdIdentity == null)
			{
				throw new ArgumentNullException("liveIdIdentity");
			}
			CommonAccessToken commonAccessToken = new CommonAccessToken(AccessTokenType.LiveId);
			LiveIdFbaTokenAccessor liveIdFbaTokenAccessor = new LiveIdFbaTokenAccessor(commonAccessToken);
			liveIdFbaTokenAccessor.UserSid = liveIdIdentity.Sid.ToString();
			liveIdFbaTokenAccessor.UserPrincipalName = liveIdIdentity.PrincipalName;
			liveIdFbaTokenAccessor.LiveIdMemberName = liveIdIdentity.MemberName;
			liveIdFbaTokenAccessor.OrganizationId = liveIdIdentity.UserOrganizationId;
			liveIdFbaTokenAccessor.LiveIdHasAcceptedAccruals = liveIdIdentity.HasAcceptedAccruals;
			commonAccessToken.ExtensionData["LoginAttributes"] = liveIdIdentity.LoginAttributes.Value.ToString();
			commonAccessToken.ExtensionData["Partition"] = liveIdIdentity.PartitionId;
			commonAccessToken.ExtensionData["OrganizationName"] = liveIdIdentity.UserOrganizationId.ConfigurationUnit.Parent.Name;
			return liveIdFbaTokenAccessor;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000CD65 File Offset: 0x0000AF65
		public static LiveIdFbaTokenAccessor Attach(CommonAccessToken token)
		{
			if (token == null)
			{
				throw new ArgumentNullException("token");
			}
			return new LiveIdFbaTokenAccessor(token);
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000CD7B File Offset: 0x0000AF7B
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x0000CD88 File Offset: 0x0000AF88
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

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000CD96 File Offset: 0x0000AF96
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x0000CDA3 File Offset: 0x0000AFA3
		public string LiveIdMemberName
		{
			get
			{
				return base.SafeGetValue("MemberName");
			}
			set
			{
				base.SafeSetValue("MemberName", value);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000CDB1 File Offset: 0x0000AFB1
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x0000CDBE File Offset: 0x0000AFBE
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

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000CDCC File Offset: 0x0000AFCC
		// (set) Token: 0x060001CB RID: 459 RVA: 0x0000CDDE File Offset: 0x0000AFDE
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

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000CDF4 File Offset: 0x0000AFF4
		// (set) Token: 0x060001CD RID: 461 RVA: 0x0000CE1D File Offset: 0x0000B01D
		public bool LiveIdHasAcceptedAccruals
		{
			get
			{
				string value = base.SafeGetValue("LiveIdHasAcceptedAccruals");
				return string.IsNullOrEmpty(value) || bool.Parse(value);
			}
			set
			{
				base.SafeSetValue("LiveIdHasAcceptedAccruals", value.ToString());
			}
		}
	}
}
