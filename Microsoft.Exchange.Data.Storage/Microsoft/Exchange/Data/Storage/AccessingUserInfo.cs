using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007D2 RID: 2002
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class AccessingUserInfo
	{
		// Token: 0x06004B15 RID: 19221 RVA: 0x0013A1F0 File Offset: 0x001383F0
		public AccessingUserInfo(string legacyExchangeDN, string externalDirectoryObjectId, OrganizationId organizationId, ADObjectId userObjectId)
		{
			if (legacyExchangeDN == null && externalDirectoryObjectId == null)
			{
				throw new ArgumentException("Either legacyExchangeDN or externalDirectoryObjectId should be provided");
			}
			ArgumentValidator.ThrowIfNull("organizationId", organizationId);
			this.legacyExchangeDN = legacyExchangeDN;
			this.externalDirectoryObjectId = externalDirectoryObjectId;
			this.organizationId = organizationId;
			this.userObjectId = userObjectId;
		}

		// Token: 0x17001567 RID: 5479
		// (get) Token: 0x06004B16 RID: 19222 RVA: 0x0013A23C File Offset: 0x0013843C
		public string LegacyExchangeDN
		{
			get
			{
				return this.legacyExchangeDN;
			}
		}

		// Token: 0x17001568 RID: 5480
		// (get) Token: 0x06004B17 RID: 19223 RVA: 0x0013A244 File Offset: 0x00138444
		public string ExternalDirectoryObjectId
		{
			get
			{
				return this.externalDirectoryObjectId;
			}
		}

		// Token: 0x17001569 RID: 5481
		// (get) Token: 0x06004B18 RID: 19224 RVA: 0x0013A24C File Offset: 0x0013844C
		public ADObjectId UserObjectId
		{
			get
			{
				if (this.userObjectId == null)
				{
					this.userObjectId = this.FindUserObjectId();
				}
				return this.userObjectId;
			}
		}

		// Token: 0x1700156A RID: 5482
		// (get) Token: 0x06004B19 RID: 19225 RVA: 0x0013A268 File Offset: 0x00138468
		public string Identity
		{
			get
			{
				return this.LegacyExchangeDN ?? this.ExternalDirectoryObjectId;
			}
		}

		// Token: 0x06004B1A RID: 19226 RVA: 0x0013A27A File Offset: 0x0013847A
		public static IRecipientSession GetRecipientSession(OrganizationId organizationId)
		{
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.FullyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 120, "GetRecipientSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\Groups\\AccessingUserInfo.cs");
		}

		// Token: 0x06004B1B RID: 19227 RVA: 0x0013A29C File Offset: 0x0013849C
		private ADObjectId FindUserObjectId()
		{
			ADRecipient adrecipient = null;
			IRecipientSession recipientSession = AccessingUserInfo.GetRecipientSession(this.organizationId);
			ArgumentValidator.ThrowIfNull("recipientSession", recipientSession);
			if (this.LegacyExchangeDN != null)
			{
				adrecipient = recipientSession.FindByLegacyExchangeDN(this.LegacyExchangeDN);
			}
			else if (this.ExternalDirectoryObjectId != null)
			{
				adrecipient = recipientSession.FindADUserByExternalDirectoryObjectId(this.ExternalDirectoryObjectId);
			}
			if (adrecipient == null)
			{
				return null;
			}
			return adrecipient.Id;
		}

		// Token: 0x040028EA RID: 10474
		private ADObjectId userObjectId;

		// Token: 0x040028EB RID: 10475
		private readonly string legacyExchangeDN;

		// Token: 0x040028EC RID: 10476
		private readonly string externalDirectoryObjectId;

		// Token: 0x040028ED RID: 10477
		private OrganizationId organizationId;
	}
}
