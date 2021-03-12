using System;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.Authentication.FederatedAuthService;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200003E RID: 62
	internal class LiveIdBasicTokenAccessor : CommonAccessTokenAccessor
	{
		// Token: 0x060001AE RID: 430 RVA: 0x0000C970 File Offset: 0x0000AB70
		private LiveIdBasicTokenAccessor(CommonAccessToken token) : base(token)
		{
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000C979 File Offset: 0x0000AB79
		public override AccessTokenType TokenType
		{
			get
			{
				return AccessTokenType.LiveIdBasic;
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000C97C File Offset: 0x0000AB7C
		public static LiveIdBasicTokenAccessor Create(ADRawEntry adRawEntry)
		{
			if (adRawEntry == null)
			{
				throw new ArgumentNullException("adRawEntry");
			}
			CommonAccessToken commonAccessToken = new CommonAccessToken(AccessTokenType.LiveIdBasic);
			LiveIdBasicTokenAccessor liveIdBasicTokenAccessor = new LiveIdBasicTokenAccessor(commonAccessToken);
			liveIdBasicTokenAccessor.UserSid = ((SecurityIdentifier)adRawEntry[ADMailboxRecipientSchema.Sid]).ToString();
			liveIdBasicTokenAccessor.UserPrincipalName = (string)adRawEntry[ADUserSchema.UserPrincipalName];
			liveIdBasicTokenAccessor.LiveIdMemberName = ((SmtpAddress)adRawEntry[ADRecipientSchema.WindowsLiveID]).ToString();
			OrganizationId organizationId = (OrganizationId)adRawEntry[ADObjectSchema.OrganizationId];
			string arg = (string)adRawEntry[ADMailboxRecipientSchema.SamAccountName];
			liveIdBasicTokenAccessor.ImplicitUpn = string.Format("{0}@{1}", arg, organizationId.PartitionId.ForestFQDN);
			liveIdBasicTokenAccessor.OrganizationId = organizationId;
			commonAccessToken.ExtensionData["Partition"] = organizationId.PartitionId.ToString();
			if (adRawEntry[ADUserSchema.NetID] != null)
			{
				commonAccessToken.ExtensionData["Puid"] = adRawEntry[ADUserSchema.NetID].ToString();
			}
			if (AuthServiceHelper.IsMailboxRole && ConfigBase<AdDriverConfigSchema>.GetConfig<bool>("AccountValidationEnabled"))
			{
				commonAccessToken.ExtensionData.Add("CreateTime", ExDateTime.UtcNow.ToString());
			}
			return liveIdBasicTokenAccessor;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000CAC0 File Offset: 0x0000ACC0
		public static LiveIdBasicTokenAccessor Create(string puid, string memberName)
		{
			return new LiveIdBasicTokenAccessor(new CommonAccessToken(AccessTokenType.LiveIdBasic)
			{
				Version = 2
			})
			{
				Puid = puid,
				LiveIdMemberName = memberName
			};
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000CAF1 File Offset: 0x0000ACF1
		public static LiveIdBasicTokenAccessor Attach(CommonAccessToken token)
		{
			if (token == null)
			{
				throw new ArgumentNullException("token");
			}
			return new LiveIdBasicTokenAccessor(token);
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000CB07 File Offset: 0x0000AD07
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x0000CB14 File Offset: 0x0000AD14
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

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000CB22 File Offset: 0x0000AD22
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x0000CB2F File Offset: 0x0000AD2F
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

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000CB3D File Offset: 0x0000AD3D
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x0000CB4A File Offset: 0x0000AD4A
		public string ImplicitUpn
		{
			get
			{
				return base.SafeGetValue("ImplicitUpn");
			}
			set
			{
				base.SafeSetValue("ImplicitUpn", value);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000CB58 File Offset: 0x0000AD58
		// (set) Token: 0x060001BA RID: 442 RVA: 0x0000CB6A File Offset: 0x0000AD6A
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

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000CB7D File Offset: 0x0000AD7D
		// (set) Token: 0x060001BC RID: 444 RVA: 0x0000CB8A File Offset: 0x0000AD8A
		public string Puid
		{
			get
			{
				return base.SafeGetValue("Puid");
			}
			set
			{
				base.SafeSetValue("Puid", value);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000CB98 File Offset: 0x0000AD98
		// (set) Token: 0x060001BE RID: 446 RVA: 0x0000CBA5 File Offset: 0x0000ADA5
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
	}
}
