using System;
using System.ComponentModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.MapiTasks
{
	// Token: 0x02000CCB RID: 3275
	[ImmutableObject(true)]
	[Serializable]
	public sealed class PublicFolderUserId : ObjectId, IEquatable<PublicFolderUserId>
	{
		// Token: 0x06007E35 RID: 32309 RVA: 0x0020400A File Offset: 0x0020220A
		private PublicFolderUserId(bool isDefault, bool isAnonymous)
		{
			this.isDefault = isDefault;
			this.isAnonymous = isAnonymous;
		}

		// Token: 0x06007E36 RID: 32310 RVA: 0x00204020 File Offset: 0x00202220
		public PublicFolderUserId(ADObjectId activeDirectoryId, string exchangeLegacyDN, MapiEntryId exchangeAddressBookEntryId, string exchangeAddressBookDisplayName)
		{
			if (null == exchangeAddressBookEntryId)
			{
				throw new ArgumentNullException("exchangeAddressBookEntryId");
			}
			if (exchangeAddressBookDisplayName == null)
			{
				throw new ArgumentNullException("exchangeAddressBookDisplayName");
			}
			if (activeDirectoryId != null && string.IsNullOrEmpty(activeDirectoryId.DistinguishedName) && Guid.Empty == activeDirectoryId.ObjectGuid)
			{
				throw new ArgumentException("activeDirectoryId is Invalid", "activeDirectoryId");
			}
			this.activeDirectoryId = activeDirectoryId;
			this.exchangeLegacyDN = exchangeLegacyDN;
			this.exchangeAddressBookEntryId = exchangeAddressBookEntryId;
			this.exchangeAddressBookDisplayName = exchangeAddressBookDisplayName;
		}

		// Token: 0x06007E37 RID: 32311 RVA: 0x002040A5 File Offset: 0x002022A5
		public override byte[] GetBytes()
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700273E RID: 10046
		// (get) Token: 0x06007E38 RID: 32312 RVA: 0x002040AC File Offset: 0x002022AC
		// (set) Token: 0x06007E39 RID: 32313 RVA: 0x002040B4 File Offset: 0x002022B4
		public ADObjectId ActiveDirectoryIdentity
		{
			get
			{
				return this.activeDirectoryId;
			}
			internal set
			{
				this.activeDirectoryId = value;
			}
		}

		// Token: 0x1700273F RID: 10047
		// (get) Token: 0x06007E3A RID: 32314 RVA: 0x002040BD File Offset: 0x002022BD
		// (set) Token: 0x06007E3B RID: 32315 RVA: 0x002040C5 File Offset: 0x002022C5
		public string ExchangeLegacyDN
		{
			get
			{
				return this.exchangeLegacyDN;
			}
			internal set
			{
				this.exchangeLegacyDN = value;
			}
		}

		// Token: 0x17002740 RID: 10048
		// (get) Token: 0x06007E3C RID: 32316 RVA: 0x002040CE File Offset: 0x002022CE
		public MapiEntryId ExchangeAddressBookEntryId
		{
			get
			{
				return this.exchangeAddressBookEntryId;
			}
		}

		// Token: 0x17002741 RID: 10049
		// (get) Token: 0x06007E3D RID: 32317 RVA: 0x002040D6 File Offset: 0x002022D6
		public string ExchangeAddressBookDisplayName
		{
			get
			{
				return this.exchangeAddressBookDisplayName;
			}
		}

		// Token: 0x17002742 RID: 10050
		// (get) Token: 0x06007E3E RID: 32318 RVA: 0x002040DE File Offset: 0x002022DE
		public bool IsDefault
		{
			get
			{
				return this.isDefault;
			}
		}

		// Token: 0x17002743 RID: 10051
		// (get) Token: 0x06007E3F RID: 32319 RVA: 0x002040E6 File Offset: 0x002022E6
		public bool IsAnonymous
		{
			get
			{
				return this.isAnonymous;
			}
		}

		// Token: 0x06007E40 RID: 32320 RVA: 0x002040F0 File Offset: 0x002022F0
		public override string ToString()
		{
			if (this.isAnonymous)
			{
				return Strings.AnonymousUser;
			}
			if (this.isDefault)
			{
				return Strings.DefaultUser;
			}
			if (this.ActiveDirectoryIdentity != null)
			{
				return this.ActiveDirectoryIdentity.ToString();
			}
			if (!string.IsNullOrEmpty(this.ExchangeLegacyDN))
			{
				return this.ExchangeLegacyDN;
			}
			if (string.IsNullOrEmpty(this.ExchangeAddressBookDisplayName))
			{
				return this.ExchangeAddressBookEntryId.ToString();
			}
			return this.ExchangeAddressBookDisplayName;
		}

		// Token: 0x06007E41 RID: 32321 RVA: 0x0020416A File Offset: 0x0020236A
		public override int GetHashCode()
		{
			if (!(null == this.ExchangeAddressBookEntryId))
			{
				return this.ExchangeAddressBookEntryId.GetHashCode();
			}
			if (!this.IsDefault)
			{
				return 32767;
			}
			return 2147418112;
		}

		// Token: 0x06007E42 RID: 32322 RVA: 0x00204199 File Offset: 0x00202399
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PublicFolderUserId);
		}

		// Token: 0x06007E43 RID: 32323 RVA: 0x002041A8 File Offset: 0x002023A8
		public bool Equals(PublicFolderUserId other)
		{
			if (null == other)
			{
				return false;
			}
			if (!string.IsNullOrEmpty(this.ExchangeLegacyDN) && !string.IsNullOrEmpty(other.ExchangeLegacyDN))
			{
				return string.Equals(this.ExchangeLegacyDN, other.ExchangeLegacyDN, StringComparison.InvariantCultureIgnoreCase);
			}
			if (null == this.ExchangeAddressBookEntryId)
			{
				return this.IsAnonymous == other.IsAnonymous && this.IsDefault == other.IsDefault;
			}
			return this.ExchangeAddressBookEntryId == other.ExchangeAddressBookEntryId;
		}

		// Token: 0x06007E44 RID: 32324 RVA: 0x0020422B File Offset: 0x0020242B
		public static bool operator ==(PublicFolderUserId operand1, PublicFolderUserId operand2)
		{
			return object.Equals(operand1, operand2);
		}

		// Token: 0x06007E45 RID: 32325 RVA: 0x00204234 File Offset: 0x00202434
		public static bool operator !=(PublicFolderUserId operand1, PublicFolderUserId operand2)
		{
			return !object.Equals(operand1, operand2);
		}

		// Token: 0x04003E1D RID: 15901
		private string exchangeLegacyDN;

		// Token: 0x04003E1E RID: 15902
		private ADObjectId activeDirectoryId;

		// Token: 0x04003E1F RID: 15903
		private MapiEntryId exchangeAddressBookEntryId;

		// Token: 0x04003E20 RID: 15904
		private readonly string exchangeAddressBookDisplayName;

		// Token: 0x04003E21 RID: 15905
		private readonly bool isDefault;

		// Token: 0x04003E22 RID: 15906
		private readonly bool isAnonymous;

		// Token: 0x04003E23 RID: 15907
		public static PublicFolderUserId DefaultUserId = new PublicFolderUserId(true, false);

		// Token: 0x04003E24 RID: 15908
		public static PublicFolderUserId AnonymousUserId = new PublicFolderUserId(false, true);
	}
}
