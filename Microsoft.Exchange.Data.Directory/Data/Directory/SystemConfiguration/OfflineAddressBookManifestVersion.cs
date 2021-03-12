using System;
using System.Text;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000519 RID: 1305
	[XmlType(TypeName = "OfflineAddressBookManifestVersion")]
	public class OfflineAddressBookManifestVersion : XMLSerializableBase, IEquatable<OfflineAddressBookManifestVersion>
	{
		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x060039B1 RID: 14769 RVA: 0x000DEA7D File Offset: 0x000DCC7D
		// (set) Token: 0x060039B2 RID: 14770 RVA: 0x000DEA85 File Offset: 0x000DCC85
		[XmlElement(ElementName = "AddressLists")]
		public AddressListSequence[] AddressLists
		{
			get
			{
				return this.addressLists;
			}
			set
			{
				this.addressLists = value;
			}
		}

		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x060039B3 RID: 14771 RVA: 0x000DEA8E File Offset: 0x000DCC8E
		public bool HasValue
		{
			get
			{
				return this.addressLists != null && this.addressLists.Length > 0;
			}
		}

		// Token: 0x060039B4 RID: 14772 RVA: 0x000DEAA8 File Offset: 0x000DCCA8
		public override int GetHashCode()
		{
			int num = 0;
			foreach (AddressListSequence addressListSequence in this.addressLists)
			{
				num ^= ((addressListSequence == null) ? 0 : addressListSequence.GetHashCode());
			}
			return num;
		}

		// Token: 0x060039B5 RID: 14773 RVA: 0x000DEAE0 File Offset: 0x000DCCE0
		public override bool Equals(object other)
		{
			return other != null && other is OfflineAddressBookManifestVersion && this.Equals((OfflineAddressBookManifestVersion)other);
		}

		// Token: 0x060039B6 RID: 14774 RVA: 0x000DEAFC File Offset: 0x000DCCFC
		public bool Equals(OfflineAddressBookManifestVersion other)
		{
			if (other == null)
			{
				return false;
			}
			if (!this.HasValue && !other.HasValue)
			{
				return true;
			}
			if (!this.HasValue || !other.HasValue)
			{
				return false;
			}
			if (this.addressLists.Length != other.addressLists.Length)
			{
				return false;
			}
			foreach (AddressListSequence addressListSequence in this.addressLists)
			{
				bool flag = false;
				foreach (AddressListSequence addressListSequence2 in other.addressLists)
				{
					if (string.Compare(addressListSequence.AddressListId, addressListSequence2.AddressListId, StringComparison.OrdinalIgnoreCase) == 0 && addressListSequence.Sequence == addressListSequence2.Sequence)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x000DEBBC File Offset: 0x000DCDBC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			foreach (AddressListSequence addressListSequence in this.addressLists)
			{
				stringBuilder.Append(string.Format("({0},{1})", addressListSequence.AddressListId, addressListSequence.Sequence));
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x0400276D RID: 10093
		private AddressListSequence[] addressLists;
	}
}
