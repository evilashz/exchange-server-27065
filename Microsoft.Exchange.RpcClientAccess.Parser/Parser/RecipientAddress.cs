using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200007A RID: 122
	internal abstract class RecipientAddress
	{
		// Token: 0x06000318 RID: 792 RVA: 0x0000BCCF File Offset: 0x00009ECF
		protected RecipientAddress(RecipientAddressType recipientAddressType)
		{
			this.recipientAddressType = recipientAddressType;
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000BCDE File Offset: 0x00009EDE
		internal RecipientAddressType RecipientAddressType
		{
			get
			{
				return this.recipientAddressType;
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000BCE8 File Offset: 0x00009EE8
		internal static RecipientAddress Parse(Reader reader, RecipientAddressType recipientAddressType)
		{
			if (recipientAddressType == RecipientAddressType.Exchange)
			{
				return new RecipientAddress.ExchangeRecipientAddress(reader, recipientAddressType);
			}
			switch (recipientAddressType)
			{
			case RecipientAddressType.MapiPrivateDistributionList:
			case RecipientAddressType.DosPrivateDistributionList:
				return new RecipientAddress.DistributionListRecipientAddress(reader, recipientAddressType);
			default:
				if (recipientAddressType == RecipientAddressType.Other)
				{
					return new RecipientAddress.OtherRecipientAddress(reader, recipientAddressType);
				}
				return new RecipientAddress.EmptyRecipientAddress(reader, recipientAddressType);
			}
		}

		// Token: 0x0600031B RID: 795
		internal abstract void Serialize(Writer writer);

		// Token: 0x04000197 RID: 407
		private readonly RecipientAddressType recipientAddressType;

		// Token: 0x0200007B RID: 123
		internal class DistributionListRecipientAddress : RecipientAddress
		{
			// Token: 0x0600031C RID: 796 RVA: 0x0000BD34 File Offset: 0x00009F34
			public DistributionListRecipientAddress(RecipientAddressType recipientAddressType, byte[] entryId, byte[] searchKey) : base(recipientAddressType)
			{
				if (recipientAddressType != RecipientAddressType.DosPrivateDistributionList && recipientAddressType != RecipientAddressType.MapiPrivateDistributionList)
				{
					throw new ArgumentException(string.Format("Invalid address type: {0}", recipientAddressType), "recipientAddressType");
				}
				Util.ThrowOnNullArgument(entryId, "entryId");
				Util.ThrowOnNullArgument(searchKey, "searchKey");
				this.entryId = entryId;
				this.searchKey = searchKey;
			}

			// Token: 0x0600031D RID: 797 RVA: 0x0000BD8F File Offset: 0x00009F8F
			internal DistributionListRecipientAddress(Reader reader, RecipientAddressType recipientAddressType) : base(recipientAddressType)
			{
				this.entryId = reader.ReadSizeAndByteArray();
				this.searchKey = reader.ReadSizeAndByteArray();
			}

			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x0600031E RID: 798 RVA: 0x0000BDB0 File Offset: 0x00009FB0
			internal byte[] EntryId
			{
				get
				{
					return this.entryId;
				}
			}

			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x0600031F RID: 799 RVA: 0x0000BDB8 File Offset: 0x00009FB8
			internal byte[] SearchKey
			{
				get
				{
					return this.searchKey;
				}
			}

			// Token: 0x06000320 RID: 800 RVA: 0x0000BDC0 File Offset: 0x00009FC0
			internal override void Serialize(Writer writer)
			{
				writer.WriteSizedBytes(this.entryId);
				writer.WriteSizedBytes(this.searchKey);
			}

			// Token: 0x04000198 RID: 408
			private readonly byte[] entryId;

			// Token: 0x04000199 RID: 409
			private readonly byte[] searchKey;
		}

		// Token: 0x0200007C RID: 124
		internal class ExchangeRecipientAddress : RecipientAddress
		{
			// Token: 0x06000321 RID: 801 RVA: 0x0000BDDC File Offset: 0x00009FDC
			public ExchangeRecipientAddress(RecipientAddressType recipientAddressType, RecipientDisplayType recipientDisplayType, string addressPrefix, string address) : base(recipientAddressType)
			{
				if (recipientAddressType != RecipientAddressType.Exchange)
				{
					throw new ArgumentException(string.Format("Invalid address type: {0}", recipientAddressType), "recipientAddressType");
				}
				Util.ThrowOnNullArgument(addressPrefix, "addressPrefix");
				Util.ThrowOnNullArgument(address, "address");
				this.recipientDisplayType = recipientDisplayType;
				this.addressPrefixLengthUsed = 0;
				while ((int)this.addressPrefixLengthUsed < addressPrefix.Length && (int)this.addressPrefixLengthUsed < address.Length && addressPrefix[(int)this.addressPrefixLengthUsed] == address[(int)this.addressPrefixLengthUsed] && this.addressPrefixLengthUsed < 255)
				{
					this.addressPrefixLengthUsed += 1;
				}
				this.address = address.Substring((int)this.addressPrefixLengthUsed);
			}

			// Token: 0x06000322 RID: 802 RVA: 0x0000BE9D File Offset: 0x0000A09D
			internal ExchangeRecipientAddress(Reader reader, RecipientAddressType recipientAddressType) : base(recipientAddressType)
			{
				this.addressPrefixLengthUsed = reader.ReadByte();
				this.recipientDisplayType = (RecipientDisplayType)reader.ReadByte();
				this.address = reader.ReadAsciiString(StringFlags.IncludeNull);
			}

			// Token: 0x170000B6 RID: 182
			// (get) Token: 0x06000323 RID: 803 RVA: 0x0000BECB File Offset: 0x0000A0CB
			internal byte AddressPrefixLengthUsed
			{
				get
				{
					return this.addressPrefixLengthUsed;
				}
			}

			// Token: 0x170000B7 RID: 183
			// (get) Token: 0x06000324 RID: 804 RVA: 0x0000BED3 File Offset: 0x0000A0D3
			internal string Address
			{
				get
				{
					return this.address;
				}
			}

			// Token: 0x06000325 RID: 805 RVA: 0x0000BEDC File Offset: 0x0000A0DC
			internal bool TryGetFullAddress(string addressPrefix, out string fullAddress)
			{
				Util.ThrowOnNullArgument(addressPrefix, "addressPrefix");
				if (this.AddressPrefixLengthUsed == 0)
				{
					fullAddress = this.Address;
					return true;
				}
				if (addressPrefix.Length == (int)this.AddressPrefixLengthUsed)
				{
					fullAddress = addressPrefix + this.Address;
					return true;
				}
				if (addressPrefix.Length > (int)this.AddressPrefixLengthUsed)
				{
					fullAddress = addressPrefix.Substring((int)this.AddressPrefixLengthUsed) + this.Address;
					return true;
				}
				fullAddress = null;
				return false;
			}

			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x06000326 RID: 806 RVA: 0x0000BF51 File Offset: 0x0000A151
			internal RecipientDisplayType RecipientDisplayType
			{
				get
				{
					return this.recipientDisplayType;
				}
			}

			// Token: 0x06000327 RID: 807 RVA: 0x0000BF59 File Offset: 0x0000A159
			internal override void Serialize(Writer writer)
			{
				writer.WriteByte(this.addressPrefixLengthUsed);
				writer.WriteByte((byte)this.recipientDisplayType);
				writer.WriteAsciiString(this.address, StringFlags.IncludeNull);
			}

			// Token: 0x0400019A RID: 410
			private readonly RecipientDisplayType recipientDisplayType;

			// Token: 0x0400019B RID: 411
			private readonly byte addressPrefixLengthUsed;

			// Token: 0x0400019C RID: 412
			private readonly string address;
		}

		// Token: 0x0200007D RID: 125
		internal class OtherRecipientAddress : RecipientAddress
		{
			// Token: 0x06000328 RID: 808 RVA: 0x0000BF80 File Offset: 0x0000A180
			public OtherRecipientAddress(RecipientAddressType recipientAddressType, string addressType) : base(recipientAddressType)
			{
				if (recipientAddressType != RecipientAddressType.Other)
				{
					throw new ArgumentException(string.Format("Invalid address type: {0}", recipientAddressType), "recipientAddressType");
				}
				Util.ThrowOnNullArgument(addressType, "addressType");
				this.addressType = addressType;
			}

			// Token: 0x06000329 RID: 809 RVA: 0x0000BFBE File Offset: 0x0000A1BE
			internal OtherRecipientAddress(Reader reader, RecipientAddressType recipientAddressType) : base(recipientAddressType)
			{
				this.addressType = reader.ReadAsciiString(StringFlags.IncludeNull);
			}

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x0600032A RID: 810 RVA: 0x0000BFD4 File Offset: 0x0000A1D4
			internal string AddressType
			{
				get
				{
					return this.addressType;
				}
			}

			// Token: 0x0600032B RID: 811 RVA: 0x0000BFDC File Offset: 0x0000A1DC
			internal override void Serialize(Writer writer)
			{
				writer.WriteAsciiString(this.addressType, StringFlags.IncludeNull);
			}

			// Token: 0x0400019D RID: 413
			private readonly string addressType;
		}

		// Token: 0x0200007E RID: 126
		internal class EmptyRecipientAddress : RecipientAddress
		{
			// Token: 0x0600032C RID: 812 RVA: 0x0000BFEB File Offset: 0x0000A1EB
			public EmptyRecipientAddress(RecipientAddressType recipientAddressType) : base(recipientAddressType)
			{
				if (recipientAddressType == RecipientAddressType.DosPrivateDistributionList || recipientAddressType == RecipientAddressType.MapiPrivateDistributionList || recipientAddressType == RecipientAddressType.Exchange || recipientAddressType == RecipientAddressType.Other)
				{
					throw new ArgumentException(string.Format("Invalid address type: {0}", recipientAddressType), "recipientAddressType");
				}
			}

			// Token: 0x0600032D RID: 813 RVA: 0x0000C023 File Offset: 0x0000A223
			internal EmptyRecipientAddress(Reader reader, RecipientAddressType recipientAddressType) : base(recipientAddressType)
			{
			}

			// Token: 0x0600032E RID: 814 RVA: 0x0000C02C File Offset: 0x0000A22C
			internal override void Serialize(Writer writer)
			{
			}
		}
	}
}
