using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Configuration
{
	// Token: 0x0200046C RID: 1132
	[DataContract]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class WacConfigData : SerializableDataBase, IEquatable<WacConfigData>
	{
		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x060032D2 RID: 13010 RVA: 0x000CE768 File Offset: 0x000CC968
		// (set) Token: 0x060032D3 RID: 13011 RVA: 0x000CE770 File Offset: 0x000CC970
		[DataMember]
		public string[] WacViewableFileTypes { get; set; }

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x060032D4 RID: 13012 RVA: 0x000CE779 File Offset: 0x000CC979
		// (set) Token: 0x060032D5 RID: 13013 RVA: 0x000CE781 File Offset: 0x000CC981
		[DataMember]
		public string[] WacEditableFileTypes { get; set; }

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x060032D6 RID: 13014 RVA: 0x000CE78A File Offset: 0x000CC98A
		// (set) Token: 0x060032D7 RID: 13015 RVA: 0x000CE792 File Offset: 0x000CC992
		[DataMember]
		public bool IsWacEditingEnabled { get; set; }

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x060032D8 RID: 13016 RVA: 0x000CE79B File Offset: 0x000CC99B
		// (set) Token: 0x060032D9 RID: 13017 RVA: 0x000CE7A3 File Offset: 0x000CC9A3
		[DataMember]
		public bool WacDiscoverySucceeded { get; set; }

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x060032DA RID: 13018 RVA: 0x000CE7AC File Offset: 0x000CC9AC
		// (set) Token: 0x060032DB RID: 13019 RVA: 0x000CE7B4 File Offset: 0x000CC9B4
		[DataMember]
		public string OneDriveDocumentsUrl { get; set; }

		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x060032DC RID: 13020 RVA: 0x000CE7BD File Offset: 0x000CC9BD
		// (set) Token: 0x060032DD RID: 13021 RVA: 0x000CE7C5 File Offset: 0x000CC9C5
		[DataMember]
		public string OneDriveDocumentsDisplayName { get; set; }

		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x060032DE RID: 13022 RVA: 0x000CE7CE File Offset: 0x000CC9CE
		// (set) Token: 0x060032DF RID: 13023 RVA: 0x000CE7D6 File Offset: 0x000CC9D6
		[DataMember]
		public bool OneDriveDiscoverySucceeded { get; set; }

		// Token: 0x060032E0 RID: 13024 RVA: 0x000CE7E0 File Offset: 0x000CC9E0
		public WacConfigData()
		{
			this.WacViewableFileTypes = new string[0];
			this.WacEditableFileTypes = new string[0];
			this.IsWacEditingEnabled = false;
			this.OneDriveDocumentsUrl = string.Empty;
			this.OneDriveDocumentsDisplayName = string.Empty;
			this.OneDriveDiscoverySucceeded = false;
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x000CE830 File Offset: 0x000CCA30
		public bool Equals(WacConfigData other)
		{
			return !object.ReferenceEquals(other, null) && (object.ReferenceEquals(other, this) || (SerializableDataBase.ArrayContentsEquals<string>(this.WacViewableFileTypes, other.WacViewableFileTypes) && SerializableDataBase.ArrayContentsEquals<string>(this.WacEditableFileTypes, other.WacEditableFileTypes) && this.IsWacEditingEnabled == other.IsWacEditingEnabled && this.WacDiscoverySucceeded == other.WacDiscoverySucceeded && string.CompareOrdinal(this.OneDriveDocumentsUrl, other.OneDriveDocumentsUrl) == 0 && string.CompareOrdinal(this.OneDriveDocumentsDisplayName, other.OneDriveDocumentsDisplayName) == 0 && this.OneDriveDiscoverySucceeded == other.OneDriveDiscoverySucceeded));
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x000CE8CB File Offset: 0x000CCACB
		protected override bool InternalEquals(object other)
		{
			return this.Equals(other as WacConfigData);
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x000CE8DC File Offset: 0x000CCADC
		protected override int InternalGetHashCode()
		{
			int num = 17;
			num = (num * 397 ^ SerializableDataBase.ArrayContentsHash<string>(this.WacViewableFileTypes));
			num = (num * 397 ^ SerializableDataBase.ArrayContentsHash<string>(this.WacEditableFileTypes));
			num = (num * 397 ^ this.OneDriveDocumentsUrl.GetHashCode());
			num = (num * 397 ^ this.OneDriveDocumentsDisplayName.GetHashCode());
			num <<= 3;
			num |= (this.IsWacEditingEnabled ? 4 : 0);
			num |= (this.WacDiscoverySucceeded ? 2 : 0);
			return num | (this.OneDriveDiscoverySucceeded ? 1 : 0);
		}
	}
}
