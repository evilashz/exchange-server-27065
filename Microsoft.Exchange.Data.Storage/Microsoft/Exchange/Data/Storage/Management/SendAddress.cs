using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A50 RID: 2640
	[Serializable]
	public class SendAddress : ConfigurableObject, IComparable<SendAddress>
	{
		// Token: 0x06006079 RID: 24697 RVA: 0x00196F98 File Offset: 0x00195198
		public SendAddress() : base(new SimplePropertyBag(SimpleProviderObjectSchema.Identity, SimpleProviderObjectSchema.ObjectState, SimpleProviderObjectSchema.ExchangeVersion))
		{
		}

		// Token: 0x0600607A RID: 24698 RVA: 0x00196FB4 File Offset: 0x001951B4
		public SendAddress(string addressId, string displayName, string mailboxIdParameterString) : this(addressId, displayName, new SendAddressIdentity(mailboxIdParameterString, addressId))
		{
		}

		// Token: 0x0600607B RID: 24699 RVA: 0x00196FC8 File Offset: 0x001951C8
		public SendAddress(string addressId, string displayName, SendAddressIdentity sendAddressIdentity) : base(new SimplePropertyBag(SimpleProviderObjectSchema.Identity, SimpleProviderObjectSchema.ObjectState, SimpleProviderObjectSchema.ExchangeVersion))
		{
			if (addressId == null)
			{
				throw new ArgumentNullException("addressId");
			}
			if (displayName == null)
			{
				throw new ArgumentNullException("displayName");
			}
			if (displayName.Length == 0)
			{
				throw new ArgumentException("display name was set to empty", "displayName");
			}
			if (sendAddressIdentity == null)
			{
				throw new ArgumentNullException("sendAddressIdentity");
			}
			this[this.propertyBag.ObjectIdentityPropertyDefinition] = sendAddressIdentity;
			this.propertyBag.ResetChangeTracking();
			this.AddressId = addressId;
			this.DisplayName = displayName;
		}

		// Token: 0x17001A8B RID: 6795
		// (get) Token: 0x0600607C RID: 24700 RVA: 0x0019705C File Offset: 0x0019525C
		// (set) Token: 0x0600607D RID: 24701 RVA: 0x0019706E File Offset: 0x0019526E
		public string AddressId
		{
			get
			{
				return (string)this[SendAddressSchema.AddressId];
			}
			private set
			{
				this[SendAddressSchema.AddressId] = value;
			}
		}

		// Token: 0x17001A8C RID: 6796
		// (get) Token: 0x0600607E RID: 24702 RVA: 0x0019707C File Offset: 0x0019527C
		// (set) Token: 0x0600607F RID: 24703 RVA: 0x0019708E File Offset: 0x0019528E
		public string DisplayName
		{
			get
			{
				return (string)this[SendAddressSchema.DisplayName];
			}
			private set
			{
				this[SendAddressSchema.DisplayName] = value;
			}
		}

		// Token: 0x17001A8D RID: 6797
		// (get) Token: 0x06006080 RID: 24704 RVA: 0x0019709C File Offset: 0x0019529C
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return SendAddress.schema;
			}
		}

		// Token: 0x17001A8E RID: 6798
		// (get) Token: 0x06006081 RID: 24705 RVA: 0x001970A3 File Offset: 0x001952A3
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x06006082 RID: 24706 RVA: 0x001970AC File Offset: 0x001952AC
		public static SendAddress CreateAutomaticSendAddressFor(string mailboxIdParameterString)
		{
			string empty = string.Empty;
			SendAddressIdentity sendAddressIdentity = new SendAddressIdentity(mailboxIdParameterString, empty);
			return new SendAddress(empty, ClientStrings.AutomaticDisplayName, sendAddressIdentity);
		}

		// Token: 0x06006083 RID: 24707 RVA: 0x001970D8 File Offset: 0x001952D8
		public int CompareTo(SendAddress other)
		{
			if (other == null)
			{
				return -1;
			}
			if (this.IsAutomatic() && other.IsAutomatic())
			{
				return 0;
			}
			if (this.IsAutomatic())
			{
				return -1;
			}
			if (other.IsAutomatic())
			{
				return 1;
			}
			return string.Compare(this.DisplayName, other.DisplayName, StringComparison.CurrentCultureIgnoreCase);
		}

		// Token: 0x06006084 RID: 24708 RVA: 0x00197117 File Offset: 0x00195317
		private bool IsAutomatic()
		{
			return this.AddressId.Equals(string.Empty);
		}

		// Token: 0x040036EE RID: 14062
		private static readonly SendAddressSchema schema = ObjectSchema.GetInstance<SendAddressSchema>();
	}
}
