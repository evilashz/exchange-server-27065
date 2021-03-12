using System;
using System.Runtime.Serialization;
using AjaxControlToolkit;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000483 RID: 1155
	[DataContract]
	[ClientScriptResource("OwaOptions", "Microsoft.Exchange.Management.ControlPanel.Client.OwaOptions.js")]
	public class SmsOptions : BaseRow
	{
		// Token: 0x060039CE RID: 14798 RVA: 0x000AF7DB File Offset: 0x000AD9DB
		public SmsOptions(TextMessagingAccount account) : base(account)
		{
			this.account = account;
		}

		// Token: 0x170022D4 RID: 8916
		// (get) Token: 0x060039CF RID: 14799 RVA: 0x000AF7EB File Offset: 0x000AD9EB
		// (set) Token: 0x060039D0 RID: 14800 RVA: 0x000AF7F8 File Offset: 0x000AD9F8
		[DataMember]
		public bool EasEnabled
		{
			get
			{
				return this.account.EasEnabled;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022D5 RID: 8917
		// (get) Token: 0x060039D1 RID: 14801 RVA: 0x000AF7FF File Offset: 0x000AD9FF
		// (set) Token: 0x060039D2 RID: 14802 RVA: 0x000AF821 File Offset: 0x000ADA21
		[DataMember]
		public bool NotificationEnabled
		{
			get
			{
				return this.account.NotificationPhoneNumber != null && this.account.NotificationPhoneNumberVerified;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022D6 RID: 8918
		// (get) Token: 0x060039D3 RID: 14803 RVA: 0x000AF828 File Offset: 0x000ADA28
		// (set) Token: 0x060039D4 RID: 14804 RVA: 0x000AF853 File Offset: 0x000ADA53
		[DataMember]
		public string NotificationPhoneCountryCode
		{
			get
			{
				if (!(this.account.NotificationPhoneNumber != null))
				{
					return string.Empty;
				}
				return this.account.NotificationPhoneNumber.CountryCode;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022D7 RID: 8919
		// (get) Token: 0x060039D5 RID: 14805 RVA: 0x000AF85A File Offset: 0x000ADA5A
		// (set) Token: 0x060039D6 RID: 14806 RVA: 0x000AF885 File Offset: 0x000ADA85
		[DataMember]
		public string NotificationPhoneNumber
		{
			get
			{
				if (!(this.account.NotificationPhoneNumber != null))
				{
					return string.Empty;
				}
				return this.account.NotificationPhoneNumber.SignificantNumber;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022D8 RID: 8920
		// (get) Token: 0x060039D7 RID: 14807 RVA: 0x000AF88C File Offset: 0x000ADA8C
		// (set) Token: 0x060039D8 RID: 14808 RVA: 0x000AF899 File Offset: 0x000ADA99
		[DataMember]
		public bool NotificationPhoneNumberVerified
		{
			get
			{
				return this.account.NotificationPhoneNumberVerified;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022D9 RID: 8921
		// (get) Token: 0x060039D9 RID: 14809 RVA: 0x000AF8A0 File Offset: 0x000ADAA0
		// (set) Token: 0x060039DA RID: 14810 RVA: 0x000AF8BF File Offset: 0x000ADABF
		[DataMember]
		public string Description
		{
			get
			{
				if (this.EasEnabled)
				{
					return OwaOptionStrings.TextMessagingSlabMessage;
				}
				return OwaOptionStrings.TextMessagingSlabMessageNotificationOnly;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022DA RID: 8922
		// (get) Token: 0x060039DB RID: 14811 RVA: 0x000AF8C6 File Offset: 0x000ADAC6
		// (set) Token: 0x060039DC RID: 14812 RVA: 0x000AF8E5 File Offset: 0x000ADAE5
		[DataMember]
		public string StatusPrefix
		{
			get
			{
				if (this.NotificationEnabled)
				{
					return OwaOptionStrings.TextMessagingStatusPrefixNotifications;
				}
				return OwaOptionStrings.TextMessagingStatusPrefixStatus;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022DB RID: 8923
		// (get) Token: 0x060039DD RID: 14813 RVA: 0x000AF8EC File Offset: 0x000ADAEC
		// (set) Token: 0x060039DE RID: 14814 RVA: 0x000AF924 File Offset: 0x000ADB24
		[DataMember]
		public string StatusDetails
		{
			get
			{
				if (this.EasEnabled)
				{
					return OwaOptionStrings.TextMessagingTurnedOnViaEas;
				}
				if (this.NotificationEnabled)
				{
					return OwaOptionStrings.ReceiveNotificationsUsingFormat(this.NotificationPhoneNumber);
				}
				return OwaOptionStrings.TextMessagingOff;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022DC RID: 8924
		// (get) Token: 0x060039DF RID: 14815 RVA: 0x000AF92B File Offset: 0x000ADB2B
		// (set) Token: 0x060039E0 RID: 14816 RVA: 0x000AF950 File Offset: 0x000ADB50
		[DataMember]
		public string CountryRegionId
		{
			get
			{
				if (this.account.CountryRegionId != null)
				{
					return this.account.CountryRegionId.Name;
				}
				return string.Empty;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022DD RID: 8925
		// (get) Token: 0x060039E1 RID: 14817 RVA: 0x000AF958 File Offset: 0x000ADB58
		// (set) Token: 0x060039E2 RID: 14818 RVA: 0x000AF98C File Offset: 0x000ADB8C
		[DataMember]
		public string MobileOperatorId
		{
			get
			{
				if (this.account.MobileOperatorId > 0)
				{
					return this.account.MobileOperatorId.ToString();
				}
				return string.Empty;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x040026C4 RID: 9924
		private readonly TextMessagingAccount account;
	}
}
