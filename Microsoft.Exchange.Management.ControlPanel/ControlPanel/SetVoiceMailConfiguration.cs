using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000C7 RID: 199
	[DataContract]
	public class SetVoiceMailConfiguration : SetVoiceMailBase
	{
		// Token: 0x06001D1A RID: 7450 RVA: 0x000598B8 File Offset: 0x00057AB8
		public SetVoiceMailConfiguration()
		{
			this.OnDeserializing(default(StreamingContext));
		}

		// Token: 0x17001939 RID: 6457
		// (get) Token: 0x06001D1B RID: 7451 RVA: 0x000598DA File Offset: 0x00057ADA
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-UMMailbox";
			}
		}

		// Token: 0x1700193A RID: 6458
		// (get) Token: 0x06001D1C RID: 7452 RVA: 0x000598E1 File Offset: 0x00057AE1
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}

		// Token: 0x1700193B RID: 6459
		// (get) Token: 0x06001D1D RID: 7453 RVA: 0x000598E8 File Offset: 0x00057AE8
		// (set) Token: 0x06001D1E RID: 7454 RVA: 0x000598F0 File Offset: 0x00057AF0
		public SetVoiceMailPIN SetVoiceMailPIN { get; private set; }

		// Token: 0x1700193C RID: 6460
		// (get) Token: 0x06001D1F RID: 7455 RVA: 0x000598F9 File Offset: 0x00057AF9
		// (set) Token: 0x06001D20 RID: 7456 RVA: 0x00059901 File Offset: 0x00057B01
		public SetSmsOptions SetSmsOptions { get; private set; }

		// Token: 0x1700193D RID: 6461
		// (get) Token: 0x06001D21 RID: 7457 RVA: 0x0005990A File Offset: 0x00057B0A
		// (set) Token: 0x06001D22 RID: 7458 RVA: 0x0005991C File Offset: 0x00057B1C
		[DataMember]
		public string PhoneNumber
		{
			get
			{
				return (string)base[UMMailboxSchema.PhoneNumber];
			}
			set
			{
				base[UMMailboxSchema.PhoneNumber] = value;
			}
		}

		// Token: 0x1700193E RID: 6462
		// (get) Token: 0x06001D23 RID: 7459 RVA: 0x0005992A File Offset: 0x00057B2A
		// (set) Token: 0x06001D24 RID: 7460 RVA: 0x00059937 File Offset: 0x00057B37
		[DataMember]
		public string PIN
		{
			get
			{
				return this.SetVoiceMailPIN.PIN;
			}
			set
			{
				this.SetVoiceMailPIN.PIN = value;
			}
		}

		// Token: 0x1700193F RID: 6463
		// (get) Token: 0x06001D25 RID: 7461 RVA: 0x00059945 File Offset: 0x00057B45
		// (set) Token: 0x06001D26 RID: 7462 RVA: 0x00059957 File Offset: 0x00057B57
		[DataMember]
		public string PhoneProviderId
		{
			get
			{
				return (string)base[UMMailboxSchema.PhoneProviderId];
			}
			set
			{
				base[UMMailboxSchema.PhoneProviderId] = value;
			}
		}

		// Token: 0x17001940 RID: 6464
		// (get) Token: 0x06001D27 RID: 7463 RVA: 0x00059965 File Offset: 0x00057B65
		// (set) Token: 0x06001D28 RID: 7464 RVA: 0x00059981 File Offset: 0x00057B81
		[DataMember]
		public bool VerifyGlobalRoutingEntry
		{
			get
			{
				return (bool)(base["VerifyGlobalRoutingEntry"] ?? false);
			}
			set
			{
				base["VerifyGlobalRoutingEntry"] = value;
			}
		}

		// Token: 0x17001941 RID: 6465
		// (get) Token: 0x06001D29 RID: 7465 RVA: 0x00059994 File Offset: 0x00057B94
		// (set) Token: 0x06001D2A RID: 7466 RVA: 0x000599A1 File Offset: 0x00057BA1
		[DataMember]
		public string VerificationCode
		{
			get
			{
				return this.SetSmsOptions.VerificationCode;
			}
			set
			{
				this.SetSmsOptions.VerificationCode = value;
			}
		}

		// Token: 0x17001942 RID: 6466
		// (get) Token: 0x06001D2B RID: 7467 RVA: 0x000599AF File Offset: 0x00057BAF
		// (set) Token: 0x06001D2C RID: 7468 RVA: 0x000599BC File Offset: 0x00057BBC
		[DataMember]
		public string SMSNotificationPhoneNumber
		{
			get
			{
				return this.SetSmsOptions.NotificationPhoneNumber;
			}
			set
			{
				this.SetSmsOptions.NotificationPhoneNumber = value;
			}
		}

		// Token: 0x17001943 RID: 6467
		// (get) Token: 0x06001D2D RID: 7469 RVA: 0x000599CA File Offset: 0x00057BCA
		// (set) Token: 0x06001D2E RID: 7470 RVA: 0x000599D7 File Offset: 0x00057BD7
		[DataMember]
		public string SMSNotificationPhoneProviderId
		{
			get
			{
				return this.SetSmsOptions.MobileOperatorId;
			}
			set
			{
				this.SetSmsOptions.MobileOperatorId = value;
			}
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x000599E5 File Offset: 0x00057BE5
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.SetVoiceMailPIN = new SetVoiceMailPIN();
			this.SetSmsOptions = new SetSmsOptions();
		}

		// Token: 0x04001BBD RID: 7101
		private const string VerifyGlobalRoutingEntryParameter = "VerifyGlobalRoutingEntry";
	}
}
