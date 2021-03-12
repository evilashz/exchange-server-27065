using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Configuration
{
	// Token: 0x02000463 RID: 1123
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	internal class MobileDevicePolicyData : SerializableDataBase, IEquatable<MobileDevicePolicyData>
	{
		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06003201 RID: 12801 RVA: 0x000CCED2 File Offset: 0x000CB0D2
		// (set) Token: 0x06003202 RID: 12802 RVA: 0x000CCEDA File Offset: 0x000CB0DA
		[DataMember]
		public bool AlphanumericDevicePasswordRequired { get; set; }

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06003203 RID: 12803 RVA: 0x000CCEE3 File Offset: 0x000CB0E3
		// (set) Token: 0x06003204 RID: 12804 RVA: 0x000CCEEB File Offset: 0x000CB0EB
		[DataMember]
		public bool DeviceEncryptionRequired { get; set; }

		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x06003205 RID: 12805 RVA: 0x000CCEF4 File Offset: 0x000CB0F4
		// (set) Token: 0x06003206 RID: 12806 RVA: 0x000CCEFC File Offset: 0x000CB0FC
		[DataMember]
		public bool DevicePasswordRequired { get; set; }

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x06003207 RID: 12807 RVA: 0x000CCF05 File Offset: 0x000CB105
		// (set) Token: 0x06003208 RID: 12808 RVA: 0x000CCF0D File Offset: 0x000CB10D
		[DataMember]
		public int MinDevicePasswordComplexCharacters { get; set; }

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x06003209 RID: 12809 RVA: 0x000CCF16 File Offset: 0x000CB116
		// (set) Token: 0x0600320A RID: 12810 RVA: 0x000CCF1E File Offset: 0x000CB11E
		[DataMember]
		public int MinDevicePasswordHistory { get; set; }

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x0600320B RID: 12811 RVA: 0x000CCF27 File Offset: 0x000CB127
		// (set) Token: 0x0600320C RID: 12812 RVA: 0x000CCF2F File Offset: 0x000CB12F
		[DataMember]
		public int? MinDevicePasswordLength { get; set; }

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x0600320D RID: 12813 RVA: 0x000CCF38 File Offset: 0x000CB138
		// (set) Token: 0x0600320E RID: 12814 RVA: 0x000CCF40 File Offset: 0x000CB140
		[DataMember]
		public bool SimpleDevicePasswordAllowed { get; set; }

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x0600320F RID: 12815 RVA: 0x000CCF49 File Offset: 0x000CB149
		// (set) Token: 0x06003210 RID: 12816 RVA: 0x000CCF51 File Offset: 0x000CB151
		[DataMember]
		public bool AllowApplePushNotifications { get; set; }

		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x06003211 RID: 12817 RVA: 0x000CCF5A File Offset: 0x000CB15A
		// (set) Token: 0x06003212 RID: 12818 RVA: 0x000CCF62 File Offset: 0x000CB162
		[DataMember]
		public bool AllowMicrosoftPushNotifications { get; set; }

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x06003213 RID: 12819 RVA: 0x000CCF6B File Offset: 0x000CB16B
		// (set) Token: 0x06003214 RID: 12820 RVA: 0x000CCF73 File Offset: 0x000CB173
		[DataMember]
		public bool AllowGooglePushNotifications { get; set; }

		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x06003215 RID: 12821 RVA: 0x000CCF7C File Offset: 0x000CB17C
		// (set) Token: 0x06003216 RID: 12822 RVA: 0x000CCFC0 File Offset: 0x000CB1C0
		[DataMember(Name = "MaxInactivityTimeDeviceLock")]
		public string MaxInactivityTimeDeviceLockString
		{
			get
			{
				if (this.MaxInactivityTimeDeviceLock.IsUnlimited)
				{
					return string.Empty;
				}
				return ((int)this.MaxInactivityTimeDeviceLock.Value.TotalSeconds).ToString();
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.MaxInactivityTimeDeviceLock = Unlimited<EnhancedTimeSpan>.UnlimitedValue;
					return;
				}
				int num = int.Parse(value);
				this.MaxInactivityTimeDeviceLock = EnhancedTimeSpan.FromSeconds((double)num);
			}
		}

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x06003217 RID: 12823 RVA: 0x000CCFFC File Offset: 0x000CB1FC
		// (set) Token: 0x06003218 RID: 12824 RVA: 0x000CD040 File Offset: 0x000CB240
		[DataMember(Name = "MaxDevicePasswordExpiration")]
		public string MaxDevicePasswordExpirationString
		{
			get
			{
				if (this.MaxDevicePasswordExpiration.IsUnlimited)
				{
					return string.Empty;
				}
				return ((int)this.MaxDevicePasswordExpiration.Value.TotalDays).ToString();
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.MaxDevicePasswordExpiration = Unlimited<EnhancedTimeSpan>.UnlimitedValue;
					return;
				}
				int num = int.Parse(value);
				this.MaxDevicePasswordExpiration = EnhancedTimeSpan.FromDays((double)num);
			}
		}

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x06003219 RID: 12825 RVA: 0x000CD07C File Offset: 0x000CB27C
		// (set) Token: 0x0600321A RID: 12826 RVA: 0x000CD0B5 File Offset: 0x000CB2B5
		[DataMember(Name = "MaxDevicePasswordFailedAttempts")]
		public string MaxDevicePasswordFailedAttemptsString
		{
			get
			{
				if (this.MaxDevicePasswordFailedAttempts.IsUnlimited)
				{
					return string.Empty;
				}
				return this.MaxDevicePasswordFailedAttempts.Value.ToString();
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.MaxDevicePasswordFailedAttempts = Unlimited<int>.UnlimitedValue;
					return;
				}
				this.MaxDevicePasswordFailedAttempts = int.Parse(value);
			}
		}

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x0600321B RID: 12827 RVA: 0x000CD0DC File Offset: 0x000CB2DC
		public string PolicyIdentifier
		{
			get
			{
				return this.GetHashCode().ToString();
			}
		}

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x0600321C RID: 12828 RVA: 0x000CD0F7 File Offset: 0x000CB2F7
		// (set) Token: 0x0600321D RID: 12829 RVA: 0x000CD0FF File Offset: 0x000CB2FF
		public Unlimited<EnhancedTimeSpan> MaxDevicePasswordExpiration { get; set; }

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x0600321E RID: 12830 RVA: 0x000CD108 File Offset: 0x000CB308
		// (set) Token: 0x0600321F RID: 12831 RVA: 0x000CD110 File Offset: 0x000CB310
		public Unlimited<int> MaxDevicePasswordFailedAttempts { get; set; }

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06003220 RID: 12832 RVA: 0x000CD119 File Offset: 0x000CB319
		// (set) Token: 0x06003221 RID: 12833 RVA: 0x000CD121 File Offset: 0x000CB321
		public Unlimited<EnhancedTimeSpan> MaxInactivityTimeDeviceLock { get; set; }

		// Token: 0x06003222 RID: 12834 RVA: 0x000CD12C File Offset: 0x000CB32C
		public bool Equals(MobileDevicePolicyData obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (obj.AlphanumericDevicePasswordRequired == this.AlphanumericDevicePasswordRequired && obj.DeviceEncryptionRequired == this.DeviceEncryptionRequired && obj.DevicePasswordRequired == this.DevicePasswordRequired && obj.MaxDevicePasswordExpiration == this.MaxDevicePasswordExpiration && obj.MaxDevicePasswordFailedAttempts == this.MaxDevicePasswordFailedAttempts && obj.MaxInactivityTimeDeviceLock == this.MaxInactivityTimeDeviceLock && obj.MinDevicePasswordComplexCharacters == this.MinDevicePasswordComplexCharacters && obj.MinDevicePasswordHistory == this.MinDevicePasswordHistory && obj.MinDevicePasswordLength == this.MinDevicePasswordLength && obj.SimpleDevicePasswordAllowed == this.SimpleDevicePasswordAllowed && obj.AllowApplePushNotifications == this.AllowApplePushNotifications && obj.AllowMicrosoftPushNotifications == this.AllowMicrosoftPushNotifications && obj.AllowGooglePushNotifications == this.AllowGooglePushNotifications));
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x000CD250 File Offset: 0x000CB450
		public override string ToString()
		{
			if (this.toStringValue == null)
			{
				this.toStringValue = string.Join(",", new object[]
				{
					this.AlphanumericDevicePasswordRequired,
					this.DeviceEncryptionRequired,
					this.DevicePasswordRequired,
					this.MaxDevicePasswordExpiration,
					this.MaxDevicePasswordFailedAttempts,
					this.MaxInactivityTimeDeviceLock,
					this.MinDevicePasswordComplexCharacters,
					this.MinDevicePasswordHistory,
					this.MinDevicePasswordLength,
					this.SimpleDevicePasswordAllowed,
					this.AllowApplePushNotifications,
					this.AllowMicrosoftPushNotifications,
					this.AllowGooglePushNotifications
				});
			}
			return this.toStringValue;
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x000CD341 File Offset: 0x000CB541
		protected override int InternalGetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x000CD34E File Offset: 0x000CB54E
		protected override bool InternalEquals(object other)
		{
			return this.Equals(other as MobileDevicePolicyData);
		}

		// Token: 0x04001B04 RID: 6916
		private string toStringValue;
	}
}
