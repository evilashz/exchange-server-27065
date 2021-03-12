using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Configuration
{
	// Token: 0x0200046B RID: 1131
	[DataContract]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UMSettingsData : SerializableDataBase, IEquatable<UMSettingsData>
	{
		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x060032C8 RID: 13000 RVA: 0x000CE66E File Offset: 0x000CC86E
		// (set) Token: 0x060032C9 RID: 13001 RVA: 0x000CE676 File Offset: 0x000CC876
		[DataMember]
		public string PlayOnPhoneDialString { get; set; }

		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x060032CA RID: 13002 RVA: 0x000CE67F File Offset: 0x000CC87F
		// (set) Token: 0x060032CB RID: 13003 RVA: 0x000CE687 File Offset: 0x000CC887
		[DataMember]
		public bool IsRequireProtectedPlayOnPhone { get; set; }

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x060032CC RID: 13004 RVA: 0x000CE690 File Offset: 0x000CC890
		// (set) Token: 0x060032CD RID: 13005 RVA: 0x000CE698 File Offset: 0x000CC898
		[DataMember]
		public bool IsUMEnabled { get; set; }

		// Token: 0x060032CE RID: 13006 RVA: 0x000CE6A4 File Offset: 0x000CC8A4
		public bool Equals(UMSettingsData other)
		{
			return !object.ReferenceEquals(other, null) && (object.ReferenceEquals(other, this) || (this.IsRequireProtectedPlayOnPhone == other.IsRequireProtectedPlayOnPhone && this.IsUMEnabled == other.IsUMEnabled && this.PlayOnPhoneDialString == other.PlayOnPhoneDialString));
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x000CE6F6 File Offset: 0x000CC8F6
		protected override bool InternalEquals(object other)
		{
			return this.Equals(other as UMSettingsData);
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x000CE704 File Offset: 0x000CC904
		protected override int InternalGetHashCode()
		{
			int num = 17;
			num = (num * 397 ^ this.IsUMEnabled.GetHashCode());
			num = (num * 397 ^ this.IsRequireProtectedPlayOnPhone.GetHashCode());
			return num * 397 ^ (this.PlayOnPhoneDialString ?? string.Empty).GetHashCode();
		}
	}
}
