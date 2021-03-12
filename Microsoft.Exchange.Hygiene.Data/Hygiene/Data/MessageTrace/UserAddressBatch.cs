using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x020001B0 RID: 432
	internal class UserAddressBatch : ConfigurablePropertyBag
	{
		// Token: 0x06001236 RID: 4662 RVA: 0x0003797A File Offset: 0x00035B7A
		public UserAddressBatch(Guid organizationalUnitRoot)
		{
			this[UserAddressBatchSchema.OrganizationalUnitRootProperty] = organizationalUnitRoot;
			this[UserAddressBatchSchema.BatchAddressesProperty] = this.batchAddresses;
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x000379AF File Offset: 0x00035BAF
		public override ObjectId Identity
		{
			get
			{
				return new MessageTraceObjectId((Guid)this[UserAddressBatchSchema.OrganizationalUnitRootProperty], Guid.Empty);
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x000379CB File Offset: 0x00035BCB
		// (set) Token: 0x06001239 RID: 4665 RVA: 0x000379DD File Offset: 0x00035BDD
		public int FssCopyId
		{
			get
			{
				return (int)this[UserAddressBatchSchema.FssCopyIdProp];
			}
			set
			{
				this[UserAddressBatchSchema.FssCopyIdProp] = value;
			}
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x000379F0 File Offset: 0x00035BF0
		public void Add(UserAddress address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.batchAddresses.AddPropertyValue(address.UserAddressId, UserAddressSchema.EmailDomainProperty, address.EmailDomain);
			this.batchAddresses.AddPropertyValue(address.UserAddressId, UserAddressSchema.EmailPrefixProperty, address.EmailPrefix);
			if (address.DigestFrequency != null)
			{
				this.batchAddresses.AddPropertyValue(address.UserAddressId, UserAddressSchema.DigestFrequencyProperty, address.DigestFrequency);
			}
			if (address.LastNotified != null)
			{
				this.batchAddresses.AddPropertyValue(address.UserAddressId, UserAddressSchema.LastNotifiedProperty, address.LastNotified);
			}
			if (address.BlockEsn != null)
			{
				this.batchAddresses.AddPropertyValue(address.UserAddressId, UserAddressSchema.BlockEsnProperty, address.BlockEsn);
			}
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00037AD6 File Offset: 0x00035CD6
		public override Type GetSchemaType()
		{
			return typeof(UserAddressBatchSchema);
		}

		// Token: 0x040008B2 RID: 2226
		private BatchPropertyTable batchAddresses = new BatchPropertyTable();
	}
}
