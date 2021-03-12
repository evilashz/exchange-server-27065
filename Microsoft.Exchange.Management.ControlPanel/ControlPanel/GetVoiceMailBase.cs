using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000C2 RID: 194
	[DataContract]
	public abstract class GetVoiceMailBase : BaseRow
	{
		// Token: 0x06001CD9 RID: 7385 RVA: 0x000592A1 File Offset: 0x000574A1
		public GetVoiceMailBase(UMMailbox mailbox) : base(mailbox)
		{
			this.UMMailbox = mailbox;
			this.UMDialPlan = mailbox.GetDialPlan();
		}

		// Token: 0x1700191E RID: 6430
		// (get) Token: 0x06001CDA RID: 7386 RVA: 0x000592BD File Offset: 0x000574BD
		// (set) Token: 0x06001CDB RID: 7387 RVA: 0x000592C5 File Offset: 0x000574C5
		public UMMailbox UMMailbox { get; private set; }

		// Token: 0x1700191F RID: 6431
		// (get) Token: 0x06001CDC RID: 7388 RVA: 0x000592CE File Offset: 0x000574CE
		// (set) Token: 0x06001CDD RID: 7389 RVA: 0x000592D6 File Offset: 0x000574D6
		public UMDialPlan UMDialPlan { get; private set; }

		// Token: 0x17001920 RID: 6432
		// (get) Token: 0x06001CDE RID: 7390 RVA: 0x000592DF File Offset: 0x000574DF
		// (set) Token: 0x06001CDF RID: 7391 RVA: 0x000592E7 File Offset: 0x000574E7
		public SmsOptions SmsOptions { get; internal set; }

		// Token: 0x17001921 RID: 6433
		// (get) Token: 0x06001CE0 RID: 7392 RVA: 0x000592F0 File Offset: 0x000574F0
		// (set) Token: 0x06001CE1 RID: 7393 RVA: 0x00059305 File Offset: 0x00057505
		[DataMember]
		public bool IsConfigured
		{
			get
			{
				return !string.IsNullOrEmpty(this.UMMailbox.PhoneNumber);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001922 RID: 6434
		// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x0005930C File Offset: 0x0005750C
		// (set) Token: 0x06001CE3 RID: 7395 RVA: 0x00059319 File Offset: 0x00057519
		[DataMember]
		public bool PinlessAccessToVoiceMailEnabled
		{
			get
			{
				return this.UMMailbox.PinlessAccessToVoiceMailEnabled;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001923 RID: 6435
		// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x00059320 File Offset: 0x00057520
		// (set) Token: 0x06001CE5 RID: 7397 RVA: 0x00059337 File Offset: 0x00057537
		[DataMember]
		public string SubscriberType
		{
			get
			{
				return this.UMDialPlan.SubscriberType.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001924 RID: 6436
		// (get) Token: 0x06001CE6 RID: 7398 RVA: 0x0005933E File Offset: 0x0005753E
		// (set) Token: 0x06001CE7 RID: 7399 RVA: 0x00059355 File Offset: 0x00057555
		[DataMember]
		public string SMSNotificationOption
		{
			get
			{
				return this.UMMailbox.UMSMSNotificationOption.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001925 RID: 6437
		// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x0005935C File Offset: 0x0005755C
		// (set) Token: 0x06001CE9 RID: 7401 RVA: 0x00059388 File Offset: 0x00057588
		[DataMember]
		public bool SMSNotificationConfigured
		{
			get
			{
				return this.SmsOptions != null && this.SmsOptions.NotificationPhoneNumberVerified && !string.IsNullOrEmpty(this.SmsOptions.NotificationPhoneNumber);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
