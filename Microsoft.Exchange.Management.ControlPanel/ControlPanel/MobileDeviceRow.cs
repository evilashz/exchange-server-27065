using System;
using System.Runtime.Serialization;
using System.Web;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200049A RID: 1178
	[DataContract]
	public class MobileDeviceRow : BaseRow
	{
		// Token: 0x06003A7D RID: 14973 RVA: 0x000B19C0 File Offset: 0x000AFBC0
		public MobileDeviceRow(MobileDeviceConfiguration mobileDevice) : base(mobileDevice.ToIdentity(mobileDevice.DeviceFriendlyName), mobileDevice)
		{
			this.MobileDevice = mobileDevice;
		}

		// Token: 0x17002319 RID: 8985
		// (get) Token: 0x06003A7E RID: 14974 RVA: 0x000B19DC File Offset: 0x000AFBDC
		// (set) Token: 0x06003A7F RID: 14975 RVA: 0x000B19E4 File Offset: 0x000AFBE4
		public MobileDeviceConfiguration MobileDevice { get; set; }

		// Token: 0x1700231A RID: 8986
		// (get) Token: 0x06003A80 RID: 14976 RVA: 0x000B19ED File Offset: 0x000AFBED
		// (set) Token: 0x06003A81 RID: 14977 RVA: 0x000B1A08 File Offset: 0x000AFC08
		[DataMember]
		public string DeviceID
		{
			get
			{
				return this.MobileDevice.DeviceID ?? OwaOptionStrings.NotAvailable;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700231B RID: 8987
		// (get) Token: 0x06003A82 RID: 14978 RVA: 0x000B1A0F File Offset: 0x000AFC0F
		// (set) Token: 0x06003A83 RID: 14979 RVA: 0x000B1A2A File Offset: 0x000AFC2A
		[DataMember]
		public string DeviceType
		{
			get
			{
				return this.MobileDevice.DeviceType ?? OwaOptionStrings.NotAvailable;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700231C RID: 8988
		// (get) Token: 0x06003A84 RID: 14980 RVA: 0x000B1A31 File Offset: 0x000AFC31
		// (set) Token: 0x06003A85 RID: 14981 RVA: 0x000B1A4C File Offset: 0x000AFC4C
		[DataMember]
		public string DeviceModel
		{
			get
			{
				return this.MobileDevice.DeviceModel ?? OwaOptionStrings.NotAvailable;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700231D RID: 8989
		// (get) Token: 0x06003A86 RID: 14982 RVA: 0x000B1A53 File Offset: 0x000AFC53
		// (set) Token: 0x06003A87 RID: 14983 RVA: 0x000B1A6E File Offset: 0x000AFC6E
		[DataMember]
		public string DevicePhoneNumber
		{
			get
			{
				return this.MobileDevice.DevicePhoneNumber ?? OwaOptionStrings.NotAvailable;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700231E RID: 8990
		// (get) Token: 0x06003A88 RID: 14984 RVA: 0x000B1A75 File Offset: 0x000AFC75
		// (set) Token: 0x06003A89 RID: 14985 RVA: 0x000B1AAE File Offset: 0x000AFCAE
		[DataMember]
		public string DevicePhoneNumber_LtrSpan
		{
			get
			{
				if (!string.IsNullOrEmpty(this.MobileDevice.DevicePhoneNumber))
				{
					return string.Format("<span dir=\"ltr\">{0}</span>", HttpUtility.HtmlEncode(this.MobileDevice.DevicePhoneNumber));
				}
				return OwaOptionStrings.NotAvailable;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700231F RID: 8991
		// (get) Token: 0x06003A8A RID: 14986 RVA: 0x000B1AB8 File Offset: 0x000AFCB8
		// (set) Token: 0x06003A8B RID: 14987 RVA: 0x000B1AF5 File Offset: 0x000AFCF5
		[DataMember]
		public string LastSyncAttemptTime
		{
			get
			{
				if (this.MobileDevice.LastSyncAttemptTime != null)
				{
					return this.MobileDevice.LastSyncAttemptTime.UtcToUserDateTimeString();
				}
				return OwaOptionStrings.NotAvailable;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002320 RID: 8992
		// (get) Token: 0x06003A8C RID: 14988 RVA: 0x000B1AFC File Offset: 0x000AFCFC
		// (set) Token: 0x06003A8D RID: 14989 RVA: 0x000B1B37 File Offset: 0x000AFD37
		public DateTime LastSyncAttemptUTCDateTime
		{
			get
			{
				if (this.MobileDevice.LastSyncAttemptTime == null)
				{
					return DateTime.MinValue;
				}
				return this.MobileDevice.LastSyncAttemptTime.Value;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002321 RID: 8993
		// (get) Token: 0x06003A8E RID: 14990 RVA: 0x000B1B3E File Offset: 0x000AFD3E
		// (set) Token: 0x06003A8F RID: 14991 RVA: 0x000B1B4E File Offset: 0x000AFD4E
		[DataMember]
		public bool DeviceStatusIsOK
		{
			get
			{
				return this.MobileDevice.Status == DeviceRemoteWipeStatus.DeviceOk;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002322 RID: 8994
		// (get) Token: 0x06003A90 RID: 14992 RVA: 0x000B1B55 File Offset: 0x000AFD55
		// (set) Token: 0x06003A91 RID: 14993 RVA: 0x000B1B81 File Offset: 0x000AFD81
		[DataMember]
		public string DeviceStatus
		{
			get
			{
				return LocalizedDescriptionAttribute.FromEnumForOwaOption(this.MobileDevice.Status.GetType(), this.MobileDevice.Status);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002323 RID: 8995
		// (get) Token: 0x06003A92 RID: 14994 RVA: 0x000B1B88 File Offset: 0x000AFD88
		// (set) Token: 0x06003A93 RID: 14995 RVA: 0x000B1BC3 File Offset: 0x000AFDC3
		[DataMember]
		public string DeviceAccessState
		{
			get
			{
				if (!this.DeviceStatusIsOK)
				{
					return this.DeviceStatus;
				}
				return LocalizedDescriptionAttribute.FromEnumForOwaOption(this.MobileDevice.DeviceAccessState.GetType(), this.MobileDevice.DeviceAccessState);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002324 RID: 8996
		// (get) Token: 0x06003A94 RID: 14996 RVA: 0x000B1BCA File Offset: 0x000AFDCA
		// (set) Token: 0x06003A95 RID: 14997 RVA: 0x000B1BDF File Offset: 0x000AFDDF
		[DataMember]
		public bool HasRecoveryPassword
		{
			get
			{
				return !string.IsNullOrEmpty(this.MobileDevice.RecoveryPassword);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002325 RID: 8997
		// (get) Token: 0x06003A96 RID: 14998 RVA: 0x000B1BE6 File Offset: 0x000AFDE6
		// (set) Token: 0x06003A97 RID: 14999 RVA: 0x000B1BF3 File Offset: 0x000AFDF3
		[DataMember]
		public string RecoveryPassword
		{
			get
			{
				return this.MobileDevice.RecoveryPassword;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002326 RID: 8998
		// (get) Token: 0x06003A98 RID: 15000 RVA: 0x000B1BFA File Offset: 0x000AFDFA
		// (set) Token: 0x06003A99 RID: 15001 RVA: 0x000B1C07 File Offset: 0x000AFE07
		[DataMember]
		public bool IsRemoteWipeSupported
		{
			get
			{
				return this.MobileDevice.IsRemoteWipeSupported;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002327 RID: 8999
		// (get) Token: 0x06003A9A RID: 15002 RVA: 0x000B1C0E File Offset: 0x000AFE0E
		// (set) Token: 0x06003A9B RID: 15003 RVA: 0x000B1C16 File Offset: 0x000AFE16
		[DataMember]
		public bool IsLoggingRunning { get; set; }

		// Token: 0x04002721 RID: 10017
		private const string PhoneNumberLtrFmt = "<span dir=\"ltr\">{0}</span>";
	}
}
