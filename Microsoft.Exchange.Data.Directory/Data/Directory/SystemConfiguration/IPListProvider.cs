using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004C5 RID: 1221
	[Serializable]
	public abstract class IPListProvider : ADConfigurationObject
	{
		// Token: 0x06003762 RID: 14178 RVA: 0x000D8653 File Offset: 0x000D6853
		public IPListProvider()
		{
		}

		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x06003763 RID: 14179 RVA: 0x000D865B File Offset: 0x000D685B
		internal override ADObjectSchema Schema
		{
			get
			{
				return IPListProvider.schema;
			}
		}

		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x06003764 RID: 14180 RVA: 0x000D8662 File Offset: 0x000D6862
		// (set) Token: 0x06003765 RID: 14181 RVA: 0x000D8674 File Offset: 0x000D6874
		[Parameter(Mandatory = false)]
		public SmtpDomain LookupDomain
		{
			get
			{
				return (SmtpDomain)this[IPListProviderSchema.LookupDomain];
			}
			set
			{
				this[IPListProviderSchema.LookupDomain] = value;
			}
		}

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x06003766 RID: 14182 RVA: 0x000D8682 File Offset: 0x000D6882
		// (set) Token: 0x06003767 RID: 14183 RVA: 0x000D8694 File Offset: 0x000D6894
		[Parameter(Mandatory = false)]
		public bool Enabled
		{
			get
			{
				return (bool)this[IPListProviderSchema.Enabled];
			}
			set
			{
				this[IPListProviderSchema.Enabled] = value;
			}
		}

		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x06003768 RID: 14184 RVA: 0x000D86A7 File Offset: 0x000D68A7
		// (set) Token: 0x06003769 RID: 14185 RVA: 0x000D86B9 File Offset: 0x000D68B9
		[Parameter(Mandatory = false)]
		public bool AnyMatch
		{
			get
			{
				return (bool)this[IPListProviderSchema.AnyMatch];
			}
			set
			{
				this[IPListProviderSchema.AnyMatch] = value;
			}
		}

		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x0600376A RID: 14186 RVA: 0x000D86CC File Offset: 0x000D68CC
		// (set) Token: 0x0600376B RID: 14187 RVA: 0x000D86DE File Offset: 0x000D68DE
		[Parameter(Mandatory = false)]
		public IPAddress BitmaskMatch
		{
			get
			{
				return (IPAddress)this[IPListProviderSchema.Bitmask];
			}
			set
			{
				this[IPListProviderSchema.Bitmask] = value;
			}
		}

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x0600376C RID: 14188 RVA: 0x000D86EC File Offset: 0x000D68EC
		// (set) Token: 0x0600376D RID: 14189 RVA: 0x000D86FE File Offset: 0x000D68FE
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPAddress> IPAddressesMatch
		{
			get
			{
				return (MultiValuedProperty<IPAddress>)this[IPListProviderSchema.IPAddress];
			}
			set
			{
				this[IPListProviderSchema.IPAddress] = value;
			}
		}

		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x0600376E RID: 14190 RVA: 0x000D870C File Offset: 0x000D690C
		// (set) Token: 0x0600376F RID: 14191 RVA: 0x000D871E File Offset: 0x000D691E
		[Parameter(Mandatory = false)]
		public int Priority
		{
			get
			{
				return (int)this[IPListProviderSchema.Priority];
			}
			set
			{
				this[IPListProviderSchema.Priority] = value;
			}
		}

		// Token: 0x06003770 RID: 14192 RVA: 0x000D8731 File Offset: 0x000D6931
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (!this.AnyMatch && this.BitmaskMatch == null && MultiValuedPropertyBase.IsNullOrEmpty(this.IPAddressesMatch))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.BitMaskOrIpAddressMatchMustBeSet, IPListProviderSchema.AnyMatch, this));
			}
		}

		// Token: 0x0400256E RID: 9582
		private static IPListProviderSchema schema = ObjectSchema.GetInstance<IPListProviderSchema>();
	}
}
