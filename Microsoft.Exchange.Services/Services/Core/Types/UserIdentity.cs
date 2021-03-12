using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.ApplicationLogic.Directory;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008BC RID: 2236
	internal class UserIdentity : RecipientIdentity
	{
		// Token: 0x06003F5D RID: 16221 RVA: 0x000DB498 File Offset: 0x000D9698
		public UserIdentity(ADUser adUser)
		{
			this.adUser = adUser;
			this.adRecipient = adUser;
			this.masterAccountSid = RecipientHelper.TryGetMasterAccountSid(adUser);
			this.objectSid = adUser.Sid;
			this.sid = (this.masterAccountSid ?? this.objectSid);
		}

		// Token: 0x06003F5E RID: 16222 RVA: 0x000DB4E8 File Offset: 0x000D96E8
		public static bool TryCreate(ADRecipient adRecipient, out UserIdentity userIdentity)
		{
			userIdentity = null;
			ADUser aduser = adRecipient as ADUser;
			if (aduser != null)
			{
				userIdentity = new UserIdentity(aduser);
			}
			else
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<ADRecipient, string>(0L, "adRecipient {0} is not ADUser. Type is {1}.", adRecipient, adRecipient.GetType().ToString());
			}
			return userIdentity != null;
		}

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06003F5F RID: 16223 RVA: 0x000DB531 File Offset: 0x000D9731
		public SecurityIdentifier ObjectSid
		{
			get
			{
				return this.objectSid;
			}
		}

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06003F60 RID: 16224 RVA: 0x000DB539 File Offset: 0x000D9739
		public ADUser ADUser
		{
			get
			{
				return this.adUser;
			}
		}

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06003F61 RID: 16225 RVA: 0x000DB541 File Offset: 0x000D9741
		// (set) Token: 0x06003F62 RID: 16226 RVA: 0x000DB549 File Offset: 0x000D9749
		public bool IsClientSecurityContextCreatedFromAccountGroupInformation
		{
			get
			{
				return this.isClientSecurityContextCreatedFromAccountGroupInformation;
			}
			set
			{
				this.isClientSecurityContextCreatedFromAccountGroupInformation = value;
			}
		}

		// Token: 0x06003F63 RID: 16227 RVA: 0x000DB552 File Offset: 0x000D9752
		public override IRecipientSession GetADRecipientSession()
		{
			return Directory.CreateADRecipientSessionForOrganization(this.adUser.QueryBaseDN, this.adUser.OrganizationId);
		}

		// Token: 0x06003F64 RID: 16228 RVA: 0x000DB570 File Offset: 0x000D9770
		public override IRecipientSession GetGALScopedADRecipientSession(ClientSecurityContext clientSecurityContext)
		{
			ADObjectId searchRoot;
			if (this.adUser.AddressBookPolicy != null)
			{
				searchRoot = this.adUser.GlobalAddressListFromAddressBookPolicy;
			}
			else
			{
				searchRoot = this.adUser.QueryBaseDN;
			}
			return Directory.CreateGALScopedADRecipientSessionForOrganization(searchRoot, 0, this.adUser.OrganizationId, clientSecurityContext);
		}

		// Token: 0x04002449 RID: 9289
		private ADUser adUser;

		// Token: 0x0400244A RID: 9290
		private SecurityIdentifier objectSid;

		// Token: 0x0400244B RID: 9291
		private bool isClientSecurityContextCreatedFromAccountGroupInformation;
	}
}
