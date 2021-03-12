using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x020001AF RID: 431
	internal class UserAddress : ConfigurablePropertyBag
	{
		// Token: 0x06001226 RID: 4646 RVA: 0x0003784A File Offset: 0x00035A4A
		public UserAddress()
		{
			this.UserAddressId = CombGuidGenerator.NewGuid();
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0003785D File Offset: 0x00035A5D
		public UserAddress(string emailPrefix, string emailDomain) : this()
		{
			this.EmailPrefix = emailPrefix;
			this.EmailDomain = emailDomain;
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x00037873 File Offset: 0x00035A73
		// (set) Token: 0x06001229 RID: 4649 RVA: 0x00037885 File Offset: 0x00035A85
		public Guid UserAddressId
		{
			get
			{
				return (Guid)this[UserAddressSchema.UserAddressIdProperty];
			}
			set
			{
				this[UserAddressSchema.UserAddressIdProperty] = value;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x00037898 File Offset: 0x00035A98
		// (set) Token: 0x0600122B RID: 4651 RVA: 0x000378AA File Offset: 0x00035AAA
		public string EmailDomain
		{
			get
			{
				return (string)this[UserAddressSchema.EmailDomainProperty];
			}
			set
			{
				this[UserAddressSchema.EmailDomainProperty] = value;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x000378B8 File Offset: 0x00035AB8
		// (set) Token: 0x0600122D RID: 4653 RVA: 0x000378CA File Offset: 0x00035ACA
		public string EmailPrefix
		{
			get
			{
				return (string)this[UserAddressSchema.EmailPrefixProperty];
			}
			set
			{
				this[UserAddressSchema.EmailPrefixProperty] = value;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x000378D8 File Offset: 0x00035AD8
		// (set) Token: 0x0600122F RID: 4655 RVA: 0x000378EA File Offset: 0x00035AEA
		public int? DigestFrequency
		{
			get
			{
				return (int?)this[UserAddressSchema.DigestFrequencyProperty];
			}
			set
			{
				this[UserAddressSchema.DigestFrequencyProperty] = value;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x000378FD File Offset: 0x00035AFD
		// (set) Token: 0x06001231 RID: 4657 RVA: 0x0003790F File Offset: 0x00035B0F
		public DateTime? LastNotified
		{
			get
			{
				return (DateTime?)this[UserAddressSchema.LastNotifiedProperty];
			}
			set
			{
				this[UserAddressSchema.LastNotifiedProperty] = value;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x00037922 File Offset: 0x00035B22
		// (set) Token: 0x06001233 RID: 4659 RVA: 0x00037934 File Offset: 0x00035B34
		public bool? BlockEsn
		{
			get
			{
				return (bool?)this[UserAddressSchema.BlockEsnProperty];
			}
			set
			{
				this[UserAddressSchema.BlockEsnProperty] = value;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x00037948 File Offset: 0x00035B48
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.UserAddressId.ToString());
			}
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0003796E File Offset: 0x00035B6E
		public override Type GetSchemaType()
		{
			return typeof(UserAddressSchema);
		}
	}
}
