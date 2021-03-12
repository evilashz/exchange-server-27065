using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007C2 RID: 1986
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PermissionSecurityPrincipal
	{
		// Token: 0x06004A96 RID: 19094 RVA: 0x00138278 File Offset: 0x00136478
		public PermissionSecurityPrincipal(ADRecipient adRecipient)
		{
			this.type = PermissionSecurityPrincipal.SecurityPrincipalType.ADRecipientPrincipal;
			this.adRecipient = adRecipient;
		}

		// Token: 0x06004A97 RID: 19095 RVA: 0x0013828E File Offset: 0x0013648E
		public PermissionSecurityPrincipal(ExternalUser externalUser)
		{
			this.type = PermissionSecurityPrincipal.SecurityPrincipalType.ExternalUserPrincipal;
			this.externalUser = externalUser;
		}

		// Token: 0x06004A98 RID: 19096 RVA: 0x001382A4 File Offset: 0x001364A4
		public PermissionSecurityPrincipal(PermissionSecurityPrincipal.SpecialPrincipalType specialPrincipalType)
		{
			EnumValidator.ThrowIfInvalid<PermissionSecurityPrincipal.SpecialPrincipalType>(specialPrincipalType, "specialPrincipalType");
			this.type = PermissionSecurityPrincipal.SecurityPrincipalType.SpecialPrincipal;
			this.specialPrincipalType = specialPrincipalType;
		}

		// Token: 0x06004A99 RID: 19097 RVA: 0x001382C5 File Offset: 0x001364C5
		public PermissionSecurityPrincipal(string memberName, byte[] entryId, long memberId)
		{
			this.type = PermissionSecurityPrincipal.SecurityPrincipalType.UnknownPrincipal;
			this.memberName = memberName;
			this.memberEntryId = entryId;
			this.memberId = memberId;
		}

		// Token: 0x17001543 RID: 5443
		// (get) Token: 0x06004A9A RID: 19098 RVA: 0x001382E9 File Offset: 0x001364E9
		public PermissionSecurityPrincipal.SecurityPrincipalType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17001544 RID: 5444
		// (get) Token: 0x06004A9B RID: 19099 RVA: 0x001382F1 File Offset: 0x001364F1
		public string UnknownPrincipalMemberName
		{
			get
			{
				if (this.type != PermissionSecurityPrincipal.SecurityPrincipalType.UnknownPrincipal)
				{
					throw new InvalidOperationException("Incorrect principal type.");
				}
				return this.memberName;
			}
		}

		// Token: 0x17001545 RID: 5445
		// (get) Token: 0x06004A9C RID: 19100 RVA: 0x0013830D File Offset: 0x0013650D
		public byte[] UnknownPrincipalEntryId
		{
			get
			{
				if (this.type != PermissionSecurityPrincipal.SecurityPrincipalType.UnknownPrincipal)
				{
					throw new InvalidOperationException("Incorrect principal type.");
				}
				return this.memberEntryId;
			}
		}

		// Token: 0x17001546 RID: 5446
		// (get) Token: 0x06004A9D RID: 19101 RVA: 0x00138329 File Offset: 0x00136529
		public long UnknownPrincipalMemberId
		{
			get
			{
				if (this.type != PermissionSecurityPrincipal.SecurityPrincipalType.UnknownPrincipal)
				{
					throw new InvalidOperationException("Incorrect principal type.");
				}
				return this.memberId;
			}
		}

		// Token: 0x17001547 RID: 5447
		// (get) Token: 0x06004A9E RID: 19102 RVA: 0x00138345 File Offset: 0x00136545
		public PermissionSecurityPrincipal.SpecialPrincipalType SpecialType
		{
			get
			{
				if (this.type != PermissionSecurityPrincipal.SecurityPrincipalType.SpecialPrincipal)
				{
					throw new InvalidOperationException("Incorrect principal type.");
				}
				return this.specialPrincipalType;
			}
		}

		// Token: 0x17001548 RID: 5448
		// (get) Token: 0x06004A9F RID: 19103 RVA: 0x00138361 File Offset: 0x00136561
		public ADRecipient ADRecipient
		{
			get
			{
				if (this.type != PermissionSecurityPrincipal.SecurityPrincipalType.ADRecipientPrincipal)
				{
					throw new InvalidOperationException("Incorrect principal type.");
				}
				return this.adRecipient;
			}
		}

		// Token: 0x17001549 RID: 5449
		// (get) Token: 0x06004AA0 RID: 19104 RVA: 0x0013837C File Offset: 0x0013657C
		public ExternalUser ExternalUser
		{
			get
			{
				if (this.type != PermissionSecurityPrincipal.SecurityPrincipalType.ExternalUserPrincipal)
				{
					throw new InvalidOperationException("Incorrect principal type.");
				}
				return this.externalUser;
			}
		}

		// Token: 0x06004AA1 RID: 19105 RVA: 0x00138398 File Offset: 0x00136598
		public override string ToString()
		{
			return this.IndexString;
		}

		// Token: 0x1700154A RID: 5450
		// (get) Token: 0x06004AA2 RID: 19106 RVA: 0x001383A0 File Offset: 0x001365A0
		public string IndexString
		{
			get
			{
				switch (this.type)
				{
				case PermissionSecurityPrincipal.SecurityPrincipalType.ADRecipientPrincipal:
					return this.adRecipient.LegacyExchangeDN;
				case PermissionSecurityPrincipal.SecurityPrincipalType.ExternalUserPrincipal:
					return this.externalUser.SmtpAddress.ToString();
				case PermissionSecurityPrincipal.SecurityPrincipalType.UnknownPrincipal:
					return this.memberId.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x04002874 RID: 10356
		private readonly PermissionSecurityPrincipal.SecurityPrincipalType type;

		// Token: 0x04002875 RID: 10357
		private readonly ADRecipient adRecipient;

		// Token: 0x04002876 RID: 10358
		private readonly ExternalUser externalUser;

		// Token: 0x04002877 RID: 10359
		private readonly PermissionSecurityPrincipal.SpecialPrincipalType specialPrincipalType;

		// Token: 0x04002878 RID: 10360
		private readonly string memberName;

		// Token: 0x04002879 RID: 10361
		private readonly byte[] memberEntryId;

		// Token: 0x0400287A RID: 10362
		private readonly long memberId;

		// Token: 0x020007C3 RID: 1987
		public enum SecurityPrincipalType
		{
			// Token: 0x0400287C RID: 10364
			ADRecipientPrincipal,
			// Token: 0x0400287D RID: 10365
			ExternalUserPrincipal,
			// Token: 0x0400287E RID: 10366
			UnknownPrincipal,
			// Token: 0x0400287F RID: 10367
			SpecialPrincipal
		}

		// Token: 0x020007C4 RID: 1988
		public enum SpecialPrincipalType
		{
			// Token: 0x04002881 RID: 10369
			Anonymous,
			// Token: 0x04002882 RID: 10370
			Default
		}
	}
}
