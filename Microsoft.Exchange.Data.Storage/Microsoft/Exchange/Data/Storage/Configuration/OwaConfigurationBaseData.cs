using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Configuration
{
	// Token: 0x02000465 RID: 1125
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	internal class OwaConfigurationBaseData : SerializableDataBase, IEquatable<OwaConfigurationBaseData>
	{
		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x06003257 RID: 12887 RVA: 0x000CD8A9 File Offset: 0x000CBAA9
		// (set) Token: 0x06003258 RID: 12888 RVA: 0x000CD8B1 File Offset: 0x000CBAB1
		[DataMember]
		public OwaAttachmentPolicyData AttachmentPolicy { get; set; }

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x06003259 RID: 12889 RVA: 0x000CD8BA File Offset: 0x000CBABA
		// (set) Token: 0x0600325A RID: 12890 RVA: 0x000CD8C2 File Offset: 0x000CBAC2
		[DataMember]
		public ulong SegmentationFlags { get; set; }

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x0600325B RID: 12891 RVA: 0x000CD8CB File Offset: 0x000CBACB
		// (set) Token: 0x0600325C RID: 12892 RVA: 0x000CD8D3 File Offset: 0x000CBAD3
		[DataMember]
		public string DefaultTheme { get; set; }

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x0600325D RID: 12893 RVA: 0x000CD8DC File Offset: 0x000CBADC
		// (set) Token: 0x0600325E RID: 12894 RVA: 0x000CD8E4 File Offset: 0x000CBAE4
		[DataMember]
		public bool UseGB18030 { get; set; }

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x0600325F RID: 12895 RVA: 0x000CD8ED File Offset: 0x000CBAED
		// (set) Token: 0x06003260 RID: 12896 RVA: 0x000CD8F5 File Offset: 0x000CBAF5
		[DataMember]
		public bool UseISO885915 { get; set; }

		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06003261 RID: 12897 RVA: 0x000CD8FE File Offset: 0x000CBAFE
		// (set) Token: 0x06003262 RID: 12898 RVA: 0x000CD906 File Offset: 0x000CBB06
		[DataMember]
		public string OutboundCharset { get; set; }

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06003263 RID: 12899 RVA: 0x000CD90F File Offset: 0x000CBB0F
		// (set) Token: 0x06003264 RID: 12900 RVA: 0x000CD917 File Offset: 0x000CBB17
		[DataMember]
		public string InstantMessagingType { get; set; }

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x06003265 RID: 12901 RVA: 0x000CD920 File Offset: 0x000CBB20
		// (set) Token: 0x06003266 RID: 12902 RVA: 0x000CD928 File Offset: 0x000CBB28
		[DataMember]
		public bool InstantMessagingEnabled { get; set; }

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x06003267 RID: 12903 RVA: 0x000CD931 File Offset: 0x000CBB31
		// (set) Token: 0x06003268 RID: 12904 RVA: 0x000CD939 File Offset: 0x000CBB39
		[DataMember]
		public bool PlacesEnabled { get; set; }

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x06003269 RID: 12905 RVA: 0x000CD942 File Offset: 0x000CBB42
		// (set) Token: 0x0600326A RID: 12906 RVA: 0x000CD94A File Offset: 0x000CBB4A
		[DataMember]
		public bool WeatherEnabled { get; set; }

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x0600326B RID: 12907 RVA: 0x000CD953 File Offset: 0x000CBB53
		// (set) Token: 0x0600326C RID: 12908 RVA: 0x000CD95B File Offset: 0x000CBB5B
		[DataMember]
		public bool AllowCopyContactsToDeviceAddressBook { get; set; }

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x0600326D RID: 12909 RVA: 0x000CD964 File Offset: 0x000CBB64
		// (set) Token: 0x0600326E RID: 12910 RVA: 0x000CD96C File Offset: 0x000CBB6C
		[DataMember]
		public string AllowOfflineOn { get; set; }

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x0600326F RID: 12911 RVA: 0x000CD975 File Offset: 0x000CBB75
		// (set) Token: 0x06003270 RID: 12912 RVA: 0x000CD97D File Offset: 0x000CBB7D
		[DataMember]
		public bool RecoverDeletedItemsEnabled { get; set; }

		// Token: 0x06003271 RID: 12913 RVA: 0x000CD988 File Offset: 0x000CBB88
		public bool Equals(OwaConfigurationBaseData other)
		{
			if (object.ReferenceEquals(other, null))
			{
				return false;
			}
			if (object.ReferenceEquals(other, this))
			{
				return true;
			}
			if (this.AttachmentPolicy != null)
			{
				return this.AttachmentPolicy.Equals(other.AttachmentPolicy) && this.SegmentationFlags == other.SegmentationFlags && this.DefaultTheme == other.DefaultTheme && this.UseGB18030 == other.UseGB18030 && this.UseISO885915 == other.UseISO885915 && this.OutboundCharset == other.OutboundCharset && this.InstantMessagingType == other.InstantMessagingType && this.InstantMessagingEnabled == other.InstantMessagingEnabled && this.PlacesEnabled == other.PlacesEnabled && this.WeatherEnabled == other.WeatherEnabled && this.AllowCopyContactsToDeviceAddressBook == other.AllowCopyContactsToDeviceAddressBook && this.AllowOfflineOn == other.AllowOfflineOn && this.RecoverDeletedItemsEnabled == other.RecoverDeletedItemsEnabled;
			}
			return null == other.AttachmentPolicy;
		}

		// Token: 0x06003272 RID: 12914 RVA: 0x000CDAA0 File Offset: 0x000CBCA0
		protected override bool InternalEquals(object other)
		{
			return this.Equals(other as OwaConfigurationBaseData);
		}

		// Token: 0x06003273 RID: 12915 RVA: 0x000CDAB0 File Offset: 0x000CBCB0
		protected override int InternalGetHashCode()
		{
			int num = 17;
			num = (num * 397 ^ ((this.AttachmentPolicy == null) ? 0 : this.AttachmentPolicy.GetHashCode()));
			num = (num * 397 ^ this.SegmentationFlags.GetHashCode());
			num = (num * 397 ^ ((this.DefaultTheme == null) ? 0 : this.DefaultTheme.GetHashCode()));
			num = (num * 397 ^ this.UseGB18030.GetHashCode());
			num = (num * 397 ^ this.UseISO885915.GetHashCode());
			num = (num * 397 ^ ((this.OutboundCharset == null) ? 0 : this.OutboundCharset.GetHashCode()));
			num = (num * 397 ^ ((this.InstantMessagingType == null) ? 0 : this.InstantMessagingType.GetHashCode()));
			num = (num * 397 ^ this.InstantMessagingEnabled.GetHashCode());
			num = (num * 397 ^ this.PlacesEnabled.GetHashCode());
			num = (num * 397 ^ this.WeatherEnabled.GetHashCode());
			num = (num * 397 ^ this.AllowCopyContactsToDeviceAddressBook.GetHashCode());
			num = (num * 397 ^ ((this.AllowOfflineOn == null) ? 0 : this.AllowOfflineOn.GetHashCode()));
			return num * 397 ^ this.RecoverDeletedItemsEnabled.GetHashCode();
		}
	}
}
