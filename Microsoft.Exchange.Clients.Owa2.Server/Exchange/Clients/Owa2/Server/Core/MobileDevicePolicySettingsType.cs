using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003E6 RID: 998
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MobileDevicePolicySettingsType
	{
		// Token: 0x0600201E RID: 8222 RVA: 0x000786E8 File Offset: 0x000768E8
		public MobileDevicePolicySettingsType()
		{
			this.AlphanumericDevicePasswordRequired = false;
			this.DeviceEncryptionRequired = false;
			this.DevicePasswordRequired = false;
			this.MaxDevicePasswordExpirationString = string.Empty;
			this.MaxDevicePasswordFailedAttemptsString = string.Empty;
			this.MaxInactivityTimeDeviceLockString = string.Empty;
			this.MinDevicePasswordComplexCharacters = 1;
			this.MinDevicePasswordHistory = 0;
			this.MinDevicePasswordLength = null;
			this.policyIdentifier = string.Empty;
			this.simpleDevicePasswordAllowed = true;
			this.allowApplePushNotifications = true;
			this.allowMicrosoftPushNotifications = true;
			this.allowGooglePushNotifications = true;
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x0600201F RID: 8223 RVA: 0x00078775 File Offset: 0x00076975
		// (set) Token: 0x06002020 RID: 8224 RVA: 0x0007877D File Offset: 0x0007697D
		[DataMember]
		public bool AlphanumericDevicePasswordRequired
		{
			get
			{
				return this.alphanumericDevicePasswordRequired;
			}
			set
			{
				this.alphanumericDevicePasswordRequired = value;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06002021 RID: 8225 RVA: 0x00078786 File Offset: 0x00076986
		// (set) Token: 0x06002022 RID: 8226 RVA: 0x0007878E File Offset: 0x0007698E
		[DataMember]
		public bool DeviceEncryptionRequired
		{
			get
			{
				return this.deviceEncryptionRequired;
			}
			set
			{
				this.deviceEncryptionRequired = value;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06002023 RID: 8227 RVA: 0x00078797 File Offset: 0x00076997
		// (set) Token: 0x06002024 RID: 8228 RVA: 0x0007879F File Offset: 0x0007699F
		[DataMember]
		public bool DevicePasswordRequired
		{
			get
			{
				return this.devicePasswordRequired;
			}
			set
			{
				this.devicePasswordRequired = value;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06002025 RID: 8229 RVA: 0x000787A8 File Offset: 0x000769A8
		// (set) Token: 0x06002026 RID: 8230 RVA: 0x000787B0 File Offset: 0x000769B0
		[DataMember(Name = "MaxDevicePasswordExpiration")]
		public string MaxDevicePasswordExpirationString
		{
			get
			{
				return this.maxDevicePasswordExpirationString;
			}
			set
			{
				this.maxDevicePasswordExpirationString = value;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06002027 RID: 8231 RVA: 0x000787B9 File Offset: 0x000769B9
		// (set) Token: 0x06002028 RID: 8232 RVA: 0x000787C1 File Offset: 0x000769C1
		[DataMember(Name = "MaxDevicePasswordFailedAttempts")]
		public string MaxDevicePasswordFailedAttemptsString
		{
			get
			{
				return this.maxDevicePasswordFailedAttemptsString;
			}
			set
			{
				this.maxDevicePasswordFailedAttemptsString = value;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06002029 RID: 8233 RVA: 0x000787CA File Offset: 0x000769CA
		// (set) Token: 0x0600202A RID: 8234 RVA: 0x000787D2 File Offset: 0x000769D2
		[DataMember(Name = "MaxInactivityTimeDeviceLock")]
		public string MaxInactivityTimeDeviceLockString
		{
			get
			{
				return this.maxInactivityTimeDeviceLockString;
			}
			set
			{
				this.maxInactivityTimeDeviceLockString = value;
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x0600202B RID: 8235 RVA: 0x000787DB File Offset: 0x000769DB
		// (set) Token: 0x0600202C RID: 8236 RVA: 0x000787E3 File Offset: 0x000769E3
		[DataMember]
		public int MinDevicePasswordComplexCharacters
		{
			get
			{
				return this.minDevicePasswordComplexCharacters;
			}
			set
			{
				this.minDevicePasswordComplexCharacters = value;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x0600202D RID: 8237 RVA: 0x000787EC File Offset: 0x000769EC
		// (set) Token: 0x0600202E RID: 8238 RVA: 0x000787F4 File Offset: 0x000769F4
		[DataMember]
		public int MinDevicePasswordHistory
		{
			get
			{
				return this.minDevicePasswordHistory;
			}
			set
			{
				this.minDevicePasswordHistory = value;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x0600202F RID: 8239 RVA: 0x000787FD File Offset: 0x000769FD
		// (set) Token: 0x06002030 RID: 8240 RVA: 0x00078805 File Offset: 0x00076A05
		public int? MinDevicePasswordLength
		{
			get
			{
				return this.minDevicePasswordLength;
			}
			set
			{
				this.minDevicePasswordLength = value;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06002031 RID: 8241 RVA: 0x00078810 File Offset: 0x00076A10
		// (set) Token: 0x06002032 RID: 8242 RVA: 0x0007884C File Offset: 0x00076A4C
		[DataMember(Name = "MinDevicePasswordLength")]
		public string MinDevicePasswordLengthString
		{
			get
			{
				if (this.MinDevicePasswordLength == null)
				{
					return string.Empty;
				}
				return this.MinDevicePasswordLength.Value.ToString();
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.MinDevicePasswordLength = null;
					return;
				}
				this.MinDevicePasswordLength = new int?(int.Parse(value));
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06002033 RID: 8243 RVA: 0x00078882 File Offset: 0x00076A82
		// (set) Token: 0x06002034 RID: 8244 RVA: 0x0007888A File Offset: 0x00076A8A
		[DataMember]
		public string PolicyIdentifier
		{
			get
			{
				return this.policyIdentifier;
			}
			set
			{
				this.policyIdentifier = value;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06002035 RID: 8245 RVA: 0x00078893 File Offset: 0x00076A93
		// (set) Token: 0x06002036 RID: 8246 RVA: 0x0007889B File Offset: 0x00076A9B
		[DataMember]
		public bool SimpleDevicePasswordAllowed
		{
			get
			{
				return this.simpleDevicePasswordAllowed;
			}
			set
			{
				this.simpleDevicePasswordAllowed = value;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06002037 RID: 8247 RVA: 0x000788A4 File Offset: 0x00076AA4
		// (set) Token: 0x06002038 RID: 8248 RVA: 0x000788AC File Offset: 0x00076AAC
		[DataMember]
		public bool AllowApplePushNotifications
		{
			get
			{
				return this.allowApplePushNotifications;
			}
			set
			{
				this.allowApplePushNotifications = value;
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06002039 RID: 8249 RVA: 0x000788B5 File Offset: 0x00076AB5
		// (set) Token: 0x0600203A RID: 8250 RVA: 0x000788BD File Offset: 0x00076ABD
		[DataMember]
		public bool AllowMicrosoftPushNotifications
		{
			get
			{
				return this.allowMicrosoftPushNotifications;
			}
			set
			{
				this.allowMicrosoftPushNotifications = value;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x0600203B RID: 8251 RVA: 0x000788C6 File Offset: 0x00076AC6
		// (set) Token: 0x0600203C RID: 8252 RVA: 0x000788CE File Offset: 0x00076ACE
		[DataMember]
		public bool AllowGooglePushNotifications
		{
			get
			{
				return this.allowGooglePushNotifications;
			}
			set
			{
				this.allowGooglePushNotifications = value;
			}
		}

		// Token: 0x04001228 RID: 4648
		private bool alphanumericDevicePasswordRequired;

		// Token: 0x04001229 RID: 4649
		private bool deviceEncryptionRequired;

		// Token: 0x0400122A RID: 4650
		private bool devicePasswordRequired;

		// Token: 0x0400122B RID: 4651
		private string maxDevicePasswordExpirationString;

		// Token: 0x0400122C RID: 4652
		private string maxDevicePasswordFailedAttemptsString;

		// Token: 0x0400122D RID: 4653
		private string maxInactivityTimeDeviceLockString;

		// Token: 0x0400122E RID: 4654
		private int minDevicePasswordComplexCharacters;

		// Token: 0x0400122F RID: 4655
		private int minDevicePasswordHistory;

		// Token: 0x04001230 RID: 4656
		private int? minDevicePasswordLength;

		// Token: 0x04001231 RID: 4657
		private string policyIdentifier;

		// Token: 0x04001232 RID: 4658
		private bool simpleDevicePasswordAllowed;

		// Token: 0x04001233 RID: 4659
		private bool allowApplePushNotifications;

		// Token: 0x04001234 RID: 4660
		private bool allowMicrosoftPushNotifications;

		// Token: 0x04001235 RID: 4661
		private bool allowGooglePushNotifications;
	}
}
