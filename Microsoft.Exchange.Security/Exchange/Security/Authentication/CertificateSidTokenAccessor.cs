using System;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000041 RID: 65
	internal class CertificateSidTokenAccessor : CommonAccessTokenAccessor
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x0000CE8D File Offset: 0x0000B08D
		private CertificateSidTokenAccessor(CommonAccessToken token) : base(token)
		{
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000CE96 File Offset: 0x0000B096
		public override AccessTokenType TokenType
		{
			get
			{
				return AccessTokenType.CertificateSid;
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000CE99 File Offset: 0x0000B099
		public static CertificateSidTokenAccessor Create(ADRawEntry adRawEntry)
		{
			return CertificateSidTokenAccessor.Create(adRawEntry, null);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000CEA4 File Offset: 0x0000B0A4
		public static CertificateSidTokenAccessor Create(ADRawEntry adRawEntry, X509Identifier certId)
		{
			if (adRawEntry == null)
			{
				throw new ArgumentNullException("adRawEntry");
			}
			CommonAccessToken token = new CommonAccessToken(AccessTokenType.CertificateSid);
			CertificateSidTokenAccessor certificateSidTokenAccessor = new CertificateSidTokenAccessor(token);
			certificateSidTokenAccessor.UserSid = ((SecurityIdentifier)adRawEntry[ADMailboxRecipientSchema.Sid]).ToString();
			OrganizationId organizationId = (OrganizationId)adRawEntry[ADObjectSchema.OrganizationId];
			if (organizationId != null && !organizationId.Equals(OrganizationId.ForestWideOrgId) && organizationId.PartitionId != null)
			{
				certificateSidTokenAccessor.PartitionId = organizationId.PartitionId.ToString();
			}
			if (certId != null)
			{
				certificateSidTokenAccessor.CertificateSubject = certId.ToString();
			}
			return certificateSidTokenAccessor;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000CF44 File Offset: 0x0000B144
		public static CertificateSidTokenAccessor Create(GenericSidIdentity sidIdentity)
		{
			if (sidIdentity == null)
			{
				throw new ArgumentNullException("sidIdentity");
			}
			CommonAccessToken token = new CommonAccessToken(AccessTokenType.CertificateSid);
			return new CertificateSidTokenAccessor(token)
			{
				UserSid = sidIdentity.Sid.ToString(),
				PartitionId = sidIdentity.PartitionId
			};
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000CF8B File Offset: 0x0000B18B
		public static CertificateSidTokenAccessor Attach(CommonAccessToken token)
		{
			if (token == null)
			{
				throw new ArgumentNullException("token");
			}
			return new CertificateSidTokenAccessor(token);
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000CFA1 File Offset: 0x0000B1A1
		// (set) Token: 0x060001DA RID: 474 RVA: 0x0000CFAE File Offset: 0x0000B1AE
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

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000CFBC File Offset: 0x0000B1BC
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0000CFC9 File Offset: 0x0000B1C9
		public string PartitionId
		{
			get
			{
				return base.SafeGetValue("Partition");
			}
			set
			{
				base.SafeSetValue("Partition", value);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000CFD7 File Offset: 0x0000B1D7
		// (set) Token: 0x060001DE RID: 478 RVA: 0x0000CFE4 File Offset: 0x0000B1E4
		public string CertificateSubject
		{
			get
			{
				return base.SafeGetValue("CertificateSubject");
			}
			set
			{
				base.SafeSetValue("CertificateSubject", value);
			}
		}
	}
}
