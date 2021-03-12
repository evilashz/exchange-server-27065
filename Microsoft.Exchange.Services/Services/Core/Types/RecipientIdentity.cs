using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200072A RID: 1834
	internal abstract class RecipientIdentity : ADIdentityInformation
	{
		// Token: 0x06003791 RID: 14225 RVA: 0x000C59DC File Offset: 0x000C3BDC
		public static bool TryCreate(ADRecipient adRecipient, out RecipientIdentity recipientIdentity)
		{
			UserIdentity userIdentity = null;
			ContactIdentity contactIdentity = null;
			if (UserIdentity.TryCreate(adRecipient, out userIdentity))
			{
				recipientIdentity = userIdentity;
			}
			else if (ContactIdentity.TryCreate(adRecipient, out contactIdentity))
			{
				recipientIdentity = contactIdentity;
			}
			else
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<ADRecipient>(0L, "adRecipient {0} is not ADUser or ADContact", adRecipient);
				recipientIdentity = null;
			}
			return recipientIdentity != null;
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06003792 RID: 14226 RVA: 0x000C5A28 File Offset: 0x000C3C28
		public ADRecipient Recipient
		{
			get
			{
				return this.adRecipient;
			}
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06003793 RID: 14227 RVA: 0x000C5A30 File Offset: 0x000C3C30
		public override SecurityIdentifier Sid
		{
			get
			{
				return this.sid;
			}
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06003794 RID: 14228 RVA: 0x000C5A38 File Offset: 0x000C3C38
		public SecurityIdentifier MasterAccountSid
		{
			get
			{
				return this.masterAccountSid;
			}
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06003795 RID: 14229 RVA: 0x000C5A40 File Offset: 0x000C3C40
		public override string SmtpAddress
		{
			get
			{
				return this.adRecipient.PrimarySmtpAddress.ToString();
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06003796 RID: 14230 RVA: 0x000C5A66 File Offset: 0x000C3C66
		public override Guid ObjectGuid
		{
			get
			{
				return this.adRecipient.Id.ObjectGuid;
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06003797 RID: 14231 RVA: 0x000C5A78 File Offset: 0x000C3C78
		public override OrganizationId OrganizationId
		{
			get
			{
				return this.adRecipient.OrganizationId;
			}
		}

		// Token: 0x06003798 RID: 14232 RVA: 0x000C5A85 File Offset: 0x000C3C85
		public override IRecipientSession GetADRecipientSession()
		{
			return Directory.CreateADRecipientSessionForOrganization(null, 0, this.adRecipient.OrganizationId);
		}

		// Token: 0x06003799 RID: 14233 RVA: 0x000C5A99 File Offset: 0x000C3C99
		public override IRecipientSession GetGALScopedADRecipientSession(ClientSecurityContext clientSecurityContext)
		{
			return Directory.CreateGALScopedADRecipientSessionForOrganization(null, 0, this.adRecipient.OrganizationId, clientSecurityContext);
		}

		// Token: 0x04001EE2 RID: 7906
		protected ADRecipient adRecipient;

		// Token: 0x04001EE3 RID: 7907
		protected SecurityIdentifier sid;

		// Token: 0x04001EE4 RID: 7908
		protected SecurityIdentifier masterAccountSid;
	}
}
